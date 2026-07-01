using System.Text.Json.Serialization;

namespace Indago.Analyzers.Configuration;

public record ReflectionCollectionData
(
    [property: JsonPropertyName("t")]
    TypeFilterData Type
);
