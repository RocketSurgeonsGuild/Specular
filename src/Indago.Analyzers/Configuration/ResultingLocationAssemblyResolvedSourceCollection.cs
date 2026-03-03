namespace Indago.Analyzers.Configuration;

public record ResultingLocationAssemblyResolvedSourceCollection(SourceLocation SourceLocation)
{
    public void AddSource(string assemblyName, ResolvedSourceLocation resolvedSource) => ResolvedSources[assemblyName] = resolvedSource;
    public Dictionary<string, ResolvedSourceLocation> ResolvedSources { get; } = [];
}
