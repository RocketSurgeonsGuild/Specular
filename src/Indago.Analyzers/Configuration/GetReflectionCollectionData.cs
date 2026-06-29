using System.Text.Json.Serialization;

namespace Indago.Analyzers.Configuration;

public record GetReflectionCollectionData
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
