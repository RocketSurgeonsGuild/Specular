using System.Text.Json.Serialization;

namespace Indago.Analyzers.Configuration;

public record AnyTypeData
(
    [property: JsonPropertyName("a")]
    string Assembly,
    [property: JsonPropertyName("t")]
    string Type,
    [property: JsonPropertyName("u")]
    bool UnboundGenericType);
