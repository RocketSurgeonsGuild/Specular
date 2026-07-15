using System.Text.Json.Serialization;

namespace Specular.Analyzers.Configuration;

public record ReflectionCollectionData
(
    [property: JsonPropertyName("t")]
    TypeFilterData Type
);
