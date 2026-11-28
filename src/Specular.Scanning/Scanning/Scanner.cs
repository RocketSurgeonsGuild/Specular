#if SPECULAR_SCANNING_EXPLICIT_USINGS
using System;
using System.Threading;
#endif
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
        ScanCore(compilation, request, ScanAssemblyScope.Default, null, cancellationToken);

    /// <summary>Runs <paramref name="request" /> within <paramref name="scope" /> and returns an equatable result.</summary>
    /// <param name="compilation">The compilation to scan, including its references.</param>
    /// <param name="request">The scan.</param>
    /// <param name="scope">
    ///     The assemblies a selection that does not name them looks in: <see cref="ScanAssemblyScope.Default" /> leaves out the framework,
    ///     <see cref="ScanAssemblyScope.Everything" /> nothing, and <see cref="ScanAssemblyScope.FromOptions" /> reads a project's patterns.
    /// </param>
    /// <param name="cancellationToken">Cancels the scan.</param>
    /// <returns>The matched assemblies and types.</returns>
    public static ScanResult Scan(Compilation compilation, ScanRequest request, ScanAssemblyScope scope, CancellationToken cancellationToken = default) =>
        ScanCore(compilation, request, scope ?? throw new ArgumentNullException(nameof(scope)), null, cancellationToken);

    /// <summary>Runs <paramref name="request" /> within <paramref name="scope" /> and returns symbols.</summary>
    public static ScanSymbols Run(Compilation compilation, ScanRequest request, ScanAssemblyScope scope, CancellationToken cancellationToken = default) =>
        Scan(compilation, request, scope, cancellationToken).Resolve(compilation);

    /// <summary>The scan within the default scope, consulting <paramref name="cache" /> for referenced assemblies (never for the compilation's own).</summary>
    internal static ScanResult Scan(Compilation compilation, ScanRequest request, IAssemblyScanCache? cache, CancellationToken cancellationToken) =>
        ScanCore(compilation, request, ScanAssemblyScope.Default, cache, cancellationToken);

    /// <summary>The scan within <paramref name="scope" />, consulting <paramref name="cache" /> for referenced assemblies (never for the compilation's own).</summary>
    internal static ScanResult Scan(Compilation compilation, ScanRequest request, ScanAssemblyScope scope, IAssemblyScanCache? cache, CancellationToken cancellationToken) =>
        ScanCore(compilation, request, scope, cache, cancellationToken);

    private static partial ScanResult ScanCore(
        Compilation compilation,
        ScanRequest request,
        ScanAssemblyScope scope,
        IAssemblyScanCache? cache,
        CancellationToken cancellationToken
    );
}
