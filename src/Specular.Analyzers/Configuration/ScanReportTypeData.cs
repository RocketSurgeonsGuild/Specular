using System.Text.Json.Serialization;

namespace Specular.Analyzers.Configuration;

public record ScanReportTypeData
(
    [property: JsonPropertyName("a")]
    string Assembly,
    [property: JsonPropertyName("t")]
    string Type);
