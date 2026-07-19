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
    string ExpressionHash)
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
    ImmutableArray<ScanReportTypeData> DiscoveredTypes = default,
    ImmutableArray<string> DiscoveredAssemblies = default
)
{
    // Default (uninitialized) ImmutableArray<T> throws when serialized by the source-generated JSON
    // converter; normalize here so every construction site (including ones that never mention these
    // params) is safe, without having to touch each call site individually.
    public ImmutableArray<ScanReportTypeData> DiscoveredTypes { get; init; } = DiscoveredTypes.IsDefault ? ImmutableArray<ScanReportTypeData>.Empty : DiscoveredTypes;
    public ImmutableArray<string> DiscoveredAssemblies { get; init; } = DiscoveredAssemblies.IsDefault ? ImmutableArray<string>.Empty : DiscoveredAssemblies;
}
