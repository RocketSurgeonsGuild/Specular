using System.Text.Json.Serialization;

namespace Indago.Analyzers.Configuration;

public record GetAssemblyConfiguration
(
    [property: JsonPropertyName("a")]
    AssemblyCollectionData Assembly
)
{
    [JsonPropertyName("t")]
    public string Type => nameof(GetAssemblyConfiguration);
};
