using System.Threading;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>Runs <see cref="ScanRequest" />s against a compilation.</summary>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
static partial class Scanner
{
    /// <summary>Runs <paramref name="request" /> and returns symbols.</summary>
    /// <param name="compilation">The compilation to scan, including its references.</param>
    /// <param name="request">The scan.</param>
    /// <param name="cancellationToken">Cancels the scan.</param>
    /// <returns>The matched assemblies and types.</returns>
    public static ScanSymbols Run(Compilation compilation, ScanRequest request, CancellationToken cancellationToken = default) =>
        Scan(compilation, request, cancellationToken).Resolve(compilation);

    /// <summary>Runs <paramref name="request" /> and returns an equatable result.</summary>
    /// <param name="compilation">The compilation to scan, including its references.</param>
    /// <param name="request">The scan.</param>
    /// <param name="cancellationToken">Cancels the scan.</param>
    /// <returns>The matched assemblies and types.</returns>
    public static ScanResult Scan(Compilation compilation, ScanRequest request, CancellationToken cancellationToken = default) =>
        ScanCore(compilation, request, null, cancellationToken);

    /// <summary>The scan, consulting <paramref name="cache" /> for referenced assemblies (never for the compilation's own).</summary>
    internal static ScanResult Scan(Compilation compilation, ScanRequest request, IAssemblyScanCache? cache, CancellationToken cancellationToken) =>
        ScanCore(compilation, request, cache, cancellationToken);

    private static partial ScanResult ScanCore(Compilation compilation, ScanRequest request, IAssemblyScanCache? cache, CancellationToken cancellationToken);
}
