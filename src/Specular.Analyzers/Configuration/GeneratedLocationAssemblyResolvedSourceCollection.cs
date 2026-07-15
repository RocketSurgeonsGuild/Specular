using System.Collections.Immutable;

namespace Specular.Analyzers.Configuration;

[SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "Renaming this internal type away from the Collection suffix would ripple across the codebase and risk breakage.")]
[SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "Renaming the internal SourceLocation property or GetSourceLocation method would ripple across the codebase and risk breakage.")]
public record GeneratedLocationAssemblyResolvedSourceCollection(SourceLocation SourceLocation, ImmutableDictionary<string, ResolvedSourceLocation> ResolvedSources)
{
    public ResolvedSourceLocation? GetSourceLocation(string assemblyName) =>
        ResolvedSources.TryGetValue(assemblyName, out var resolvedSourceLocation)
            ? resolvedSourceLocation
            : null;
}
