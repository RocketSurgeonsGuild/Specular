using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Specular.Scanning;

namespace Specular.Analyzers.Configuration;

/// <summary>
///     Specular's build cache, <c>obj/ctp/SpecularProvider.ctpjson</c>, kept by Specular.Scanning's <see cref="ScanCache" />. Besides the
///     scan results per request and referenced assembly, Specular keeps two kinds of entries in its typed store, both tied to the
///     referenced assembly's MVID:
///     <list type="bullet">
///         <item><c>requests</c>: the scan requests the assembly carries in <c>[AssemblyMetadata]</c> (<see cref="CompiledAssemblyProviderData" />).</item>
///         <item><c>sources/&lt;hash&gt;</c>: what one call site generated for the assembly (<see cref="ResolvedSourceLocation" />).</item>
///     </list>
/// </summary>
internal sealed class SpecularCache
{
    /// <summary>The consumer id that names the cache file.</summary>
    public const string ConsumerId = "SpecularProvider";

    // Bump when the shape or meaning of either typed-store entry changes: entries written with another version are ignored.
    private const int DataVersion = 1;
    private const string RequestsKey = "requests";
    private const string SourcesKeyPrefix = "sources/";

    private readonly ScanCache _cache;
    private readonly bool _isAot;

    public SpecularCache(Compilation compilation, ScanCacheFileData previous, bool isAot)
    {
        _cache = new(ConsumerId, compilation, previous);
        _isAot = isAot;
        // In front of the file: a reference's scan results are reused in memory for as long as the reference lives.
        Scans = new MemoizingScanCache(compilation, _cache);
    }

    /// <summary>Scan results per request and referenced assembly.</summary>
    public IAssemblyScanCache Scans { get; }

    public static ScanCacheFileData Parse(string? text) => ScanCacheFile.Parse(text);

    public static bool IsCacheFile(AdditionalText file) => ScanCache.IsCacheFile(file, ConsumerId);

    public bool TryGetRequests(IAssemblySymbol assembly, [MaybeNullWhen(false)] out CompiledAssemblyProviderData requests) =>
        _cache.TryGet(RequestsKey, assembly, JsonSourceGenerationContext.Default.CompiledAssemblyProviderData, DataVersion, out requests);

    public void SetRequests(IAssemblySymbol assembly, CompiledAssemblyProviderData requests) =>
        _cache.Set(RequestsKey, assembly, requests, JsonSourceGenerationContext.Default.CompiledAssemblyProviderData, DataVersion);

    /// <summary>
    ///     What the call site at <paramref name="location" /> generated for <paramref name="assembly" /> on an earlier build, or else what
    ///     <paramref name="resolve" /> generates now. A result is kept only when there is one and generating it reported nothing:
    ///     diagnostics are not cached, so they must come from a fresh result.
    /// </summary>
    public ResolvedSourceLocation? GetOrAddSource(
        SourceLocation location,
        ScanRequest request,
        IAssemblySymbol assembly,
        Func<(ResolvedSourceLocation? Source, bool Cacheable)> resolve
    )
    {
        var key = GetSourceKey(location, request);
        if (_cache.TryGet(key, assembly, JsonSourceGenerationContext.Default.ResolvedSourceLocation, DataVersion, out var cached))
        {
            // Keeps the scan results in the file too, so a call site that moves (and so misses above) still skips walking the assembly.
            _ = Scans.TryGetTypes(request, assembly, out _);
            return cached;
        }

        var (source, cacheable) = resolve();
        if (source is { } && cacheable)
        {
            _cache.Set(key, assembly, source, JsonSourceGenerationContext.Default.ResolvedSourceLocation, DataVersion);
        }

        return source;
    }

    /// <summary>Writes the entries this run used; only the library's writer touches the file system.</summary>
    public void Save(AnalyzerConfigOptions globalOptions) => _cache.Save(globalOptions);

    // The whole location (two call sites can share a file, line and expression yet come from different assemblies), the request the
    // call site resolved to, and whether the build targets Native AOT, which changes what is generated.
    private string GetSourceKey(SourceLocation location, ScanRequest request)
    {
        var text = JsonSerializer.Serialize(location, JsonSourceGenerationContext.Default.SourceLocation) + "\n" + request.CacheKey + "\n" + ( _isAot ? "aot" : "jit" );
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
        return SourcesKeyPrefix + Convert.ToBase64String(hash).TrimEnd('=').Replace('+', '-').Replace('/', '_');
    }
}
