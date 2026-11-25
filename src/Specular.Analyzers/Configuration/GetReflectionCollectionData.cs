using System.Text.Json.Serialization;

namespace Specular.Analyzers.Configuration;

internal sealed record GetReflectionCollectionData
(
    [property: JsonPropertyName("a")]
    AssemblyCollectionData Assembly,
    [property: JsonPropertyName("r")]
    ReflectionCollectionData Reflection
)
{
    [JsonPropertyName("t")]
    public string Type => nameof(GetReflectionCollectionData);
}
