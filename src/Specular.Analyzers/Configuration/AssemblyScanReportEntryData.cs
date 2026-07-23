using System.Text.Json.Serialization;

namespace Specular.Analyzers.Configuration;

public record AssemblyScanReportEntryData(
    [property: JsonPropertyName("a")]
    string Assembly);
