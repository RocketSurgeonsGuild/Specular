using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Specular.Analyzers.Configuration;

namespace Specular.Analyzers;

public enum SourceLocationKind
{
    Assembly, Reflection, ServiceDescriptor,
}

public record SourceLocation
(
    [property: JsonPropertyName("k")]
    SourceLocationKind Kind,
    [property: JsonPropertyName("l")]
    int LineNumber,
    string FilePath,
    [property: JsonPropertyName("e")]
    string ExpressionHash,
    [property: JsonPropertyName("sa")]
    string SourceAssemblyName = "",
    [property: JsonPropertyName("se")]
    string SourceExpression = "")
{
    [JsonIgnore]
    public string FileName => Path.GetFileName(FilePath);

    [JsonPropertyName("f")]
    public string FilePath { get; init; } = FilePath.Replace("\\", "/");
}

public record ResolvedSourceLocation
(
    SourceLocation Location,
    string Expression,
    ImmutableHashSet<string> PrivateAssemblies,
    ImmutableList<TypeScanReportEntryData> DiscoveredTypes,
    ImmutableList<AssemblyScanReportEntryData> DiscoveredAssemblies,
    ImmutableList<ServiceDescriptorScanReportEntryData> DiscoveredServiceDescriptors,
    string? ScannedAssemblyName
)
{
    // By value, collections included, so the pipeline can tell that a run produced the same results and skip regenerating the code.
    public virtual bool Equals(ResolvedSourceLocation? other) =>
        other is { }
     && Location == other.Location
     && Expression == other.Expression
     && ScannedAssemblyName == other.ScannedAssemblyName
     && PrivateAssemblies.SetEquals(other.PrivateAssemblies)
     && DiscoveredTypes.SequenceEqual(other.DiscoveredTypes)
     && DiscoveredAssemblies.SequenceEqual(other.DiscoveredAssemblies)
     && DiscoveredServiceDescriptors.SequenceEqual(other.DiscoveredServiceDescriptors);

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = Location.GetHashCode();
            hash = ( hash * 397 ) ^ StringComparer.Ordinal.GetHashCode(Expression);
            return ( hash * 397 ) ^ ( ScannedAssemblyName is { } name ? StringComparer.Ordinal.GetHashCode(name) : 0 );
        }
    }
}
