using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     An equatable description of a scan: the value that flows through incremental pipelines and keys the build cache. Build one
///     with <see cref="Build" />.
/// </summary>
/// <param name="Kind">Whether the scan returns assemblies or types.</param>
/// <param name="Assemblies">Which assemblies to look in.</param>
/// <param name="Types">Which types to return; null for <see cref="ScanKind.Assemblies" />.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed partial record ScanRequest(ScanKind Kind, AssemblyFilterData Assemblies, TypeFilterData? Types)
{
    /// <summary>
    ///     Builds a request with the symbol selector, the compile-time mirror of Specular's runtime selectors. The selection is an
    ///     assemblies scan unless <c>GetTypes</c> is called.
    /// </summary>
    /// <param name="compilation">The compilation the selector's symbols come from.</param>
    /// <param name="select">The selection, e.g. <c>s =&gt; s.FromAssemblies().GetTypes(f =&gt; f.AssignableTo(handler))</c>.</param>
    /// <returns>The request.</returns>
    public static ScanRequest Build(Compilation compilation, Action<IAssemblySymbolSelector> select) => BuildCore(compilation, select);

    /// <summary>A deterministic key for this request, stable across processes and builds; it keys the build cache.</summary>
    public string CacheKey => GetCacheKeyCore();

    private static partial ScanRequest BuildCore(Compilation compilation, Action<IAssemblySymbolSelector> select);

    private partial string GetCacheKeyCore();
}
