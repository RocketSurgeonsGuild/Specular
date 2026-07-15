using System.Text.Json.Serialization;

namespace Specular.Analyzers.Configuration;

public record ServiceDescriptorCollectionData
(
    [property: JsonPropertyName("s")]
    ServiceDescriptorFilterData ServiceDescriptor,
    [property: JsonPropertyName("z")]
    int Lifetime
);
