#if SPECULAR_SCANNING_EXPLICIT_USINGS
using System;
#endif
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     Remembers a referenced assembly's scan results for as long as its <see cref="MetadataReference" /> lives, in front of the disk
///     cache. The pipeline reruns the scan on every compilation change, and in the IDE the disk cache is neither read back nor written
///     while typing; a reference instance always stands for the same immutable assembly, so its results can be reused until it goes away.
/// </summary>
internal sealed class MemoizingScanCache(Compilation compilation, ScanCache diskCache) : IAssemblyScanCache
{
    private static readonly ConditionalWeakTable<MetadataReference, ConcurrentDictionary<string, EquatableArray<ScannedType>>> memo = new();

    // Accessibility is computed relative to the consuming assembly, so the same reference can give different results to two consumers.
    private readonly string _consumer = compilation.Assembly.Identity.GetDisplayName(fullKey: true);

    public bool TryGetTypes(ScanRequest request, IAssemblySymbol assembly, out EquatableArray<ScannedType> types)
    {
        var entries = GetEntries(assembly);
        var key = GetKey(request);
        if (entries is { } && entries.TryGetValue(key, out types))
        {
            // Saving keeps only the entries a run touched, so a remembered result must still reach the disk cache.
            ( (IAssemblyScanCache)diskCache ).SetTypes(request, assembly, types);
            return true;
        }

        if (!( (IAssemblyScanCache)diskCache ).TryGetTypes(request, assembly, out types)) return false;

        if (entries is { }) entries[key] = types;
        return true;
    }

    public void SetTypes(ScanRequest request, IAssemblySymbol assembly, EquatableArray<ScannedType> types)
    {
        if (GetEntries(assembly) is { } entries) entries[GetKey(request)] = types;
        ( (IAssemblyScanCache)diskCache ).SetTypes(request, assembly, types);
    }

    private string GetKey(ScanRequest request) => request.CacheKey + "|" + _consumer;

    private ConcurrentDictionary<string, EquatableArray<ScannedType>>? GetEntries(IAssemblySymbol assembly) =>
        compilation.GetMetadataReference(assembly) is { } reference
            ? memo.GetValue(reference, static _ => new(StringComparer.Ordinal))
            : null;
}
