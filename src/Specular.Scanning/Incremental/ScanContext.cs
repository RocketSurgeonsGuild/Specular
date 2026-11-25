using System.Threading;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     What a <c>Scan</c> transform gets: the compilation, the scan results, and the run's cache for values of your own. Valid only
///     while the transform runs.
/// </summary>
/// <remarks>
///     It holds the compilation, so it is not equatable: never return it, or anything holding a symbol, from the transform. Values set
///     in <see cref="Cache" /> after the transform returns are not saved.
/// </remarks>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed class ScanContext
{
    internal ScanContext(Compilation compilation, EquatableArray<ScanResult> results, ScanCache cache, CancellationToken cancellationToken)
    {
        Compilation = compilation;
        Results = results;
        Cache = cache;
        CancellationToken = cancellationToken;
    }

    /// <summary>The compilation that was scanned.</summary>
    public Compilation Compilation { get; }

    /// <summary>One result per request, in request order.</summary>
    public EquatableArray<ScanResult> Results { get; }

    /// <summary>
    ///     The run's cache, for values you derive per referenced assembly (<see cref="ScanCache.TryGet{T}" />,
    ///     <see cref="ScanCache.Set{T}" />). They are saved in the same file as the scans, and remembered in memory between runs.
    /// </summary>
    /// <remarks>Saving is done for you: don't call <see cref="ScanCache.Save" />.</remarks>
    public ScanCache Cache { get; }

    /// <summary>Cancels the run.</summary>
    public CancellationToken CancellationToken { get; }
}
