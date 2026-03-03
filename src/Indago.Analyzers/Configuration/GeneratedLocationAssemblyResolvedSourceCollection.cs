using System.Collections.Immutable;

namespace Indago.Analyzers.Configuration;

public record GeneratedLocationAssemblyResolvedSourceCollection(SourceLocation SourceLocation, ImmutableDictionary<string, ResolvedSourceLocation> ResolvedSources)
{
    public ResolvedSourceLocation? GetSourceLocation(string assemblyName) =>
        ResolvedSources.TryGetValue(assemblyName, out var resolvedSourceLocation)
            ? resolvedSourceLocation
            : null;
}
