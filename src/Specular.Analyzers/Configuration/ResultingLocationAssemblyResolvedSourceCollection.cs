namespace Specular.Analyzers.Configuration;

[SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "Renaming this internal type away from the Collection suffix would ripple across the codebase and risk breakage.")]
public record ResultingLocationAssemblyResolvedSourceCollection(SourceLocation SourceLocation)
{
    public void AddSource(string assemblyName, ResolvedSourceLocation resolvedSource) => ResolvedSources[assemblyName] = resolvedSource;
    public Dictionary<string, ResolvedSourceLocation> ResolvedSources { get; } = [];
}
