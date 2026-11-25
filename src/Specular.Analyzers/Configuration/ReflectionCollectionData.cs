using System.Text.Json.Serialization;

namespace Specular.Analyzers.Configuration;

internal sealed record ReflectionCollectionData
(
    [property: JsonPropertyName("t")]
    TypeFilterData Type
);
