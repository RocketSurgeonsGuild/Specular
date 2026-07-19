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
    string? CacheVersion,
    ImmutableList<ScanReportTypeData> DiscoveredTypes,
    ImmutableList<string> DiscoveredAssemblies,
    ImmutableList<ServiceDescriptorScanReportEntryData> DiscoveredServiceDescriptors,
    string? ScannedAssemblyName
);
