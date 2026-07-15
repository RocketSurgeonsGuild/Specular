using System.Text.Json.Serialization;

namespace Specular.Analyzers.Configuration;

public record AssemblyCollectionData
(
    [property: JsonPropertyName("l")]
    SourceLocation Location,
    [property: JsonPropertyName("a")]
    AssemblyFilterData Assembly
);
