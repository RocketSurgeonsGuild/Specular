using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Specular.Scanning;

/// <summary>
///     A source generator's build cache for one run: per-assembly scan results and a typed store, persisted across builds in
///     <c>obj/ctp/&lt;ConsumerId&gt;.ctpjson</c>. Entries are tied to a referenced assembly's module version id (MVID) and hit only
///     while it is unchanged; references without a readable MVID (e.g. IDE project references) and the compilation's own assembly
///     are never cached.
/// </summary>
/// <remarks>
///     Per run: <see cref="Load" /> it from the additional file the build feeds back (filter
///     <c>AdditionalTextsProvider</c> with <see cref="IsCacheFile" />), use it, then <see cref="Save" /> it from the source
///     output. Only entries read or written during the run are saved, so entries for removed assemblies or requests drop out.
///     The consumer id names the file and must be unique to the generator; generators never see each other's entries. Safe for
///     concurrent use within a run.
/// </remarks>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed class ScanCache : IAssemblyScanCache
{
    /// <summary>Whether <paramref name="file" /> is <paramref name="consumerId" />'s cache file (matched by file name, ignoring case).</summary>
    /// <param name="file">An additional file.</param>
    /// <param name="consumerId">The generator's consumer id: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <returns>True for the consumer's cache file.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static bool IsCacheFile(AdditionalText file, string consumerId) =>
        file is null ? throw new ArgumentNullException(nameof(file)) : ScanCacheFile.IsCacheFile(file.Path, consumerId);

    /// <summary>
    ///     Loads the cache for this run from the consumer's cache file among <paramref name="additionalFiles" />. A missing, corrupt
    ///     or outdated file gives an empty cache.
    /// </summary>
    /// <param name="consumerId">The generator's consumer id: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="compilation">The compilation being generated for; cache entries are validated against its references.</param>
    /// <param name="additionalFiles">Additional files; only the one <see cref="IsCacheFile" /> matches is read.</param>
    /// <param name="cancellationToken">Cancels reading the file.</param>
    /// <returns>The cache.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static ScanCache Load(
        string consumerId,
        Compilation compilation,
        IEnumerable<AdditionalText> additionalFiles,
        CancellationToken cancellationToken = default
    )
    {
        _ = ScanCacheFile.ValidateConsumerId(consumerId);
        if (compilation is null) throw new ArgumentNullException(nameof(compilation));
        if (additionalFiles is null) throw new ArgumentNullException(nameof(additionalFiles));

        var file = additionalFiles.FirstOrDefault(z => ScanCacheFile.IsCacheFile(z.Path, consumerId));
        return new(consumerId, compilation, ScanCacheFile.Parse(file?.GetText(cancellationToken)?.ToString()));
    }

    private readonly Compilation _compilation;
    private readonly ScanCacheFileData _previous;
    private readonly ConcurrentDictionary<IAssemblySymbol, Guid?> _mvids = new(SymbolEqualityComparer.Default);
    private readonly object _gate = new();
    private readonly Dictionary<string, Dictionary<string, CachedScanEntry>> _scans = new(StringComparer.Ordinal);
    private readonly Dictionary<string, Dictionary<string, CachedStoreEntry>> _store = new(StringComparer.Ordinal);

    /// <summary>Creates the cache from an already parsed file, so incremental pipelines can parse once per file change.</summary>
    internal ScanCache(string consumerId, Compilation compilation, ScanCacheFileData previous)
    {
        ConsumerId = ScanCacheFile.ValidateConsumerId(consumerId);
        _compilation = compilation;
        _previous = previous;
    }

    /// <summary>The consumer id that names this cache's file.</summary>
    public string ConsumerId { get; }

    /// <summary>
    ///     Gets the value stored under <paramref name="key" /> for <paramref name="assembly" /> on an earlier build (or earlier in this
    ///     run), if the assembly is unchanged and the value was stored with <paramref name="dataVersion" />.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="key">The consumer's key for this kind of value.</param>
    /// <param name="assembly">A referenced assembly of the compilation.</param>
    /// <param name="typeInfo">Deserializes the value; use a source-generated <c>JsonSerializerContext</c>.</param>
    /// <param name="dataVersion">The consumer's version of the value's shape or meaning; bump it to invalidate stored values.</param>
    /// <param name="value">The stored value.</param>
    /// <returns>True on a hit.</returns>
    public bool TryGet<T>(string key, IAssemblySymbol assembly, JsonTypeInfo<T> typeInfo, int dataVersion, [MaybeNullWhen(false)] out T value)
    {
        if (key is null) throw new ArgumentNullException(nameof(key));
        if (assembly is null) throw new ArgumentNullException(nameof(assembly));
        if (typeInfo is null) throw new ArgumentNullException(nameof(typeInfo));

        value = default;
        if (GetMvid(assembly) is not { } mvid) return false;

        var assemblyName = assembly.MetadataName;
        CachedStoreEntry? current;
        lock (_gate)
        {
            current = Find(_store, key, assemblyName);
        }

        var entry = current ?? FindPrevious(_previous.Store, key, assemblyName);
        if (entry is null || entry.Mvid != mvid || entry.Version != dataVersion || !TryDeserialize(entry.Data, typeInfo, out value))
        {
            return false;
        }

        if (current is null)
        {
            lock (_gate)
            {
                AddIfMissing(GetOrAdd(_store, key), assemblyName, entry);
            }
        }

        return true;
    }

    /// <summary>Stores <paramref name="value" /> under <paramref name="key" /> for <paramref name="assembly" />, if it is cacheable.</summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="key">The consumer's key for this kind of value.</param>
    /// <param name="assembly">A referenced assembly of the compilation.</param>
    /// <param name="value">The value.</param>
    /// <param name="typeInfo">Serializes the value; use a source-generated <c>JsonSerializerContext</c>.</param>
    /// <param name="dataVersion">The consumer's version of the value's shape or meaning.</param>
    public void Set<T>(string key, IAssemblySymbol assembly, T value, JsonTypeInfo<T> typeInfo, int dataVersion)
    {
        if (key is null) throw new ArgumentNullException(nameof(key));
        if (assembly is null) throw new ArgumentNullException(nameof(assembly));
        if (typeInfo is null) throw new ArgumentNullException(nameof(typeInfo));

        if (GetMvid(assembly) is not { } mvid) return;

        var entry = new CachedStoreEntry(mvid, dataVersion, JsonSerializer.SerializeToElement(value, typeInfo));
        lock (_gate)
        {
            GetOrAdd(_store, key)[assembly.MetadataName] = entry;
        }
    }

    /// <summary>
    ///     Writes the entries read or written during this run to <c>$(IntermediateOutputPath)ctp/&lt;ConsumerId&gt;.ctpjson</c>, unless
    ///     the file already has that content. Does nothing outside MSBuild, and never throws on IO failures.
    /// </summary>
    /// <param name="globalOptions">The global analyzer config options (<c>AnalyzerConfigOptionsProvider.GlobalOptions</c>).</param>
    public void Save(AnalyzerConfigOptions globalOptions)
    {
        if (globalOptions is null) throw new ArgumentNullException(nameof(globalOptions));
        _ = ScanCacheWriter.Write(globalOptions, ConsumerId, Serialize());
    }

    bool IAssemblyScanCache.TryGetTypes(ScanRequest request, IAssemblySymbol assembly, out EquatableArray<ScannedType> types) =>
        TryGetTypes(request.CacheKey, assembly, out types);

    void IAssemblyScanCache.SetTypes(ScanRequest request, IAssemblySymbol assembly, EquatableArray<ScannedType> types) =>
        SetTypes(request.CacheKey, assembly, types);

    /// <summary>The types the request keyed <paramref name="requestKey" /> matched in <paramref name="assembly" />, if still valid.</summary>
    internal bool TryGetTypes(string requestKey, IAssemblySymbol assembly, out EquatableArray<ScannedType> types)
    {
        types = EquatableArray<ScannedType>.Empty;
        if (GetMvid(assembly) is not { } mvid) return false;

        var assemblyName = assembly.MetadataName;
        CachedScanEntry? current;
        lock (_gate)
        {
            current = Find(_scans, requestKey, assemblyName);
        }

        var entry = current ?? FindPrevious(_previous.Scans, requestKey, assemblyName);
        if (entry is null || entry.Mvid != mvid || !TryConvert(entry, assemblyName, out types))
        {
            return false;
        }

        if (current is null)
        {
            lock (_gate)
            {
                AddIfMissing(GetOrAdd(_scans, requestKey), assemblyName, entry);
            }
        }

        return true;
    }

    /// <summary>Records the types the request keyed <paramref name="requestKey" /> matched in <paramref name="assembly" />.</summary>
    internal void SetTypes(string requestKey, IAssemblySymbol assembly, EquatableArray<ScannedType> types)
    {
        if (GetMvid(assembly) is not { } mvid) return;

        var assemblyName = assembly.MetadataName;
        var entry = new CachedScanEntry(
            mvid,
            types
               .Select(type => (CachedType?)new CachedType(
                           type.MetadataName,
                           type.Kind,
                           type.DeclaredAccessibility,
                           type.IsAccessible,
                           type.Arity,
                           string.Equals(type.AssemblyName, assemblyName, StringComparison.Ordinal) ? null : type.AssemblyName
                       )
                )
               .ToList()
        );
        lock (_gate)
        {
            GetOrAdd(_scans, requestKey)[assemblyName] = entry;
        }
    }

    /// <summary>The cache file content for the entries used in this run.</summary>
    internal string Serialize()
    {
        lock (_gate)
        {
            return ScanCacheFile.Serialize(new(ScanCacheFile.SchemaVersion, Sorted(_scans), Sorted(_store)));
        }
    }

    private Guid? GetMvid(IAssemblySymbol assembly) => _mvids.GetOrAdd(assembly, z => ModuleVersionIds.Get(_compilation, z));

    private static TEntry? Find<TEntry>(Dictionary<string, Dictionary<string, TEntry>> entries, string key, string assemblyName)
        where TEntry : class =>
        entries.TryGetValue(key, out var assemblies) && assemblies.TryGetValue(assemblyName, out var entry) ? entry : null;

    private static TEntry? FindPrevious<TEntry>(Dictionary<string, Dictionary<string, TEntry?>?>? entries, string key, string assemblyName)
        where TEntry : class =>
        entries is { } && entries.TryGetValue(key, out var assemblies) && assemblies is { } && assemblies.TryGetValue(assemblyName, out var entry)
            ? entry
            : null;

    private static Dictionary<string, TEntry> GetOrAdd<TEntry>(Dictionary<string, Dictionary<string, TEntry>> entries, string key)
    {
        if (!entries.TryGetValue(key, out var assemblies))
        {
            assemblies = new(StringComparer.Ordinal);
            entries[key] = assemblies;
        }

        return assemblies;
    }

    // A concurrent Set may have landed since the lookup; it is newer than the carried-forward entry, so it wins.
    private static void AddIfMissing<TEntry>(Dictionary<string, TEntry> assemblies, string assemblyName, TEntry entry)
    {
        if (!assemblies.ContainsKey(assemblyName))
        {
            assemblies[assemblyName] = entry;
        }
    }

    // Dictionary enumerates in insertion order when nothing is removed, so inserting in ordinal key order keeps the file byte-stable
    // across runs; the writer then skips rewriting unchanged content.
    private static Dictionary<string, Dictionary<string, TEntry?>?> Sorted<TEntry>(Dictionary<string, Dictionary<string, TEntry>> entries)
        where TEntry : class =>
        entries
           .OrderBy(z => z.Key, StringComparer.Ordinal)
           .ToDictionary(
                z => z.Key,
                z => (Dictionary<string, TEntry?>?)z
                                                  .Value.OrderBy(a => a.Key, StringComparer.Ordinal)
                                                  .ToDictionary(a => a.Key, a => (TEntry?)a.Value, StringComparer.Ordinal),
                StringComparer.Ordinal
            );

    private static bool TryConvert(CachedScanEntry entry, string assemblyName, out EquatableArray<ScannedType> types)
    {
        types = EquatableArray<ScannedType>.Empty;
        if (entry.Types is null) return false;

        var builder = ImmutableArray.CreateBuilder<ScannedType>(entry.Types.Count);
        foreach (var type in entry.Types)
        {
            if (type?.Name is not { Length: > 0 } name) return false;
            builder.Add(new(type.Assembly ?? assemblyName, name, type.Kind, type.Access, type.Accessible, type.Arity));
        }

        types = builder.MoveToImmutable();
        return true;
    }

    private static bool TryDeserialize<T>(JsonElement data, JsonTypeInfo<T> typeInfo, [MaybeNullWhen(false)] out T value)
    {
        try
        {
            value = data.Deserialize(typeInfo)!;
            return true;
        }
#pragma warning disable CA1031 // A payload that no longer fits the consumer's type is a cache miss, never a build failure.
        catch (Exception)
#pragma warning restore CA1031
        {
            value = default;
            return false;
        }
    }
}
