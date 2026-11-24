using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>The equatable result of a scan.</summary>
/// <param name="Request">The request that produced it.</param>
/// <param name="Assemblies">The assemblies that passed the assembly filter, in a deterministic order.</param>
/// <param name="Types">For a types scan, the matched types in a deterministic order; empty for an assemblies scan.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed partial record ScanResult(ScanRequest Request, EquatableArray<ScannedAssembly> Assemblies, EquatableArray<ScannedType> Types)
{
    /// <summary>Resolves the result back to symbols in <paramref name="compilation" />.</summary>
    /// <param name="compilation">The compilation to resolve in.</param>
    /// <returns>The symbols; entries that no longer resolve are skipped.</returns>
    public ScanSymbols Resolve(Compilation compilation) => ResolveCore(compilation);

    private partial ScanSymbols ResolveCore(Compilation compilation);
}
