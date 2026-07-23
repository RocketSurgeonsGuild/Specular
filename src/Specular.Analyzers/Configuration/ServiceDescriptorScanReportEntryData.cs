using System.Text.Json.Serialization;

namespace Specular.Analyzers.Configuration;

public record ServiceDescriptorScanReportEntryData(
    [property: JsonPropertyName("l")]
    string Lifetime,
    [property: JsonPropertyName("s")]
    string ServiceType,
    [property: JsonPropertyName("i")]
    string ImplementationType);
