using System.Text.Json.Serialization;

namespace Specular.Analyzers.Configuration;

internal sealed record GetServiceDescriptorCollectionData
(
    [property: JsonPropertyName("a")]
    AssemblyCollectionData Assembly,
    [property: JsonPropertyName("r")]
    ReflectionCollectionData Reflection,
    [property: JsonPropertyName("s")]
    ServiceDescriptorCollectionData ServiceDescriptor)
{
    [JsonPropertyName("t")]
    public string Type => nameof(GetServiceDescriptorCollectionData);
}
