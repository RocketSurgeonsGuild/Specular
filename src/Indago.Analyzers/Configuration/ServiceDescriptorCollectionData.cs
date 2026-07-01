using System.Text.Json.Serialization;

namespace Indago.Analyzers.Configuration;

public record ServiceDescriptorCollectionData
(
    [property: JsonPropertyName("s")]
    ServiceDescriptorFilterData ServiceDescriptor,
    [property: JsonPropertyName("z")]
    int Lifetime
);
