using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.Configuration;

[SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "Renaming the internal AssemblyData property or GetAssemblyData method would ripple across the codebase and risk breakage.")]
public record GeneratedAssemblyProviderData
(
    ImmutableDictionary<string, CompiledAssemblyProviderData> AssemblyData,
    ImmutableHashSet<string> EmptyAssemblies,
    ImmutableDictionary<string, GeneratedLocationAssemblyResolvedSourceCollection> Partials
)
{
    public CompiledAssemblyProviderData? GetAssemblyData(IAssemblySymbol assembly)
    {
        return  assembly is null 
            ? throw new ArgumentNullException(nameof(assembly))
            :  AssemblyData.TryGetValue(assembly.MetadataName, out var data) && assembly.MatchesCachedVersion(data.CacheVersion)
            ? data
            : null;
    }

    public ResolvedSourceLocation? GetSourceLocation(IAssemblySymbol assembly, SourceLocation sourceLocation, Func<ResolvedSourceLocation?> factory)
    {
        if (assembly is null) throw new ArgumentNullException(nameof(assembly));
        if (sourceLocation is null) throw new ArgumentNullException(nameof(sourceLocation));
        return  factory is null 
            ? throw new ArgumentNullException(nameof(factory))
            :  Partials.TryGetValue(ResultingAssemblyProviderData.GetCacheFileHash(sourceLocation), out var resolvedSourceLocations)
     && resolvedSourceLocations.GetSourceLocation(assembly.MetadataName) is { } data
     && assembly.MatchesCachedVersion(data.CacheVersion)
            ? data
            : factory();
    }

    public bool DoesAssemblyContainExpressions(IAssemblySymbol assembly)
    {
        return  assembly is null  ? throw new ArgumentNullException(nameof(assembly)) :  EmptyAssemblies.Contains(assembly.MetadataName);
    }
}
