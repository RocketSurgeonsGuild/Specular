using System.Text.Json.Serialization;

namespace Specular.Analyzers.Configuration;

public record TypeScanReportEntryData(
    [property: JsonPropertyName("t")]
    string Type);
