#if SPECULAR_SCANNING_EXPLICIT_USINGS
using System.Diagnostics.CodeAnalysis;
#endif
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     Remembers typed-store values for as long as the referenced assembly's <see cref="MetadataReference" /> lives, as
///     <see cref="MemoizingScanCache" /> does for scan results: in the IDE the cache file is neither read back nor written while typing,
///     so without it every edit would recompute every value the file doesn't hold.
/// </summary>
/// <remarks>
///     Values are kept as JSON text, so every read deserializes a fresh copy a generator may mutate, and nothing is shared across
///     threads but immutable strings. They are keyed by consumer id (generators never see each other's values), data version and the
///     consuming assembly (a value may depend on the compilation that derived it, as accessibility does).
/// </remarks>
internal sealed class MemoizingStore(Compilation compilation, string consumerId)
{
    private static readonly ConditionalWeakTable<MetadataReference, ConcurrentDictionary<(string ConsumerId, string Consumer, string Key, int DataVersion), string>>
        memo = new();

    private readonly string _consumer = compilation.Assembly.Identity.GetDisplayName(fullKey: true);

    /// <summary>The JSON remembered for <paramref name="key" /> and <paramref name="dataVersion" /> in <paramref name="assembly" />.</summary>
    public bool TryGet(IAssemblySymbol assembly, string key, int dataVersion, [NotNullWhen(true)] out string? json)
    {
        json = null;
        return GetEntries(assembly) is { } entries && entries.TryGetValue((consumerId, _consumer, key, dataVersion), out json);
    }

    /// <summary>Remembers <paramref name="json" /> for <paramref name="key" /> and <paramref name="dataVersion" /> in <paramref name="assembly" />.</summary>
    public void Set(IAssemblySymbol assembly, string key, int dataVersion, string json)
    {
        if (GetEntries(assembly) is { } entries) entries[(consumerId, _consumer, key, dataVersion)] = json;
    }

    // The compilation's own assembly has no reference, so it is never remembered: it changes with every edit.
    private ConcurrentDictionary<(string ConsumerId, string Consumer, string Key, int DataVersion), string>? GetEntries(IAssemblySymbol assembly) =>
        compilation.GetMetadataReference(assembly) is { } reference ? memo.GetValue(reference, static _ => new()) : null;
}
