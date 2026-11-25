using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>A scan's result as symbols. Not equatable: use it inside a source output step, never as a pipeline value.</summary>
/// <param name="assemblies">The assemblies that passed the assembly filter.</param>
/// <param name="types">For a types scan, the matched types; empty for an assemblies scan.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed class ScanSymbols(ImmutableArray<IAssemblySymbol> assemblies, ImmutableArray<INamedTypeSymbol> types)
{
    /// <summary>The assemblies that passed the assembly filter.</summary>
    public ImmutableArray<IAssemblySymbol> Assemblies { get; } = assemblies;

    /// <summary>For a types scan, the matched types; empty for an assemblies scan.</summary>
    public ImmutableArray<INamedTypeSymbol> Types { get; } = types;
}
