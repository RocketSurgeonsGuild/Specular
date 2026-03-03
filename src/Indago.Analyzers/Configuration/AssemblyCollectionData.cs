using System.Text.Json.Serialization;

namespace Indago.Analyzers.Configuration;

public record AssemblyCollectionData
(
    [property: JsonPropertyName("l")]
    SourceLocation Location,
    [property: JsonPropertyName("a")]
    AssemblyFilterData Assembly
);
