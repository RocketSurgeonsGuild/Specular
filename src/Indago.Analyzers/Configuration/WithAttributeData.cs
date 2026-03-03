using System.Text.Json.Serialization;

namespace Indago.Analyzers.Configuration;

public record WithAttributeData
(
    [property: JsonPropertyName("i")]
    bool Include,
    [property: JsonPropertyName("a")]
    string Assembly,
    [property: JsonPropertyName("b")]
    string Attribute,
    [property: JsonPropertyName("u")]
    bool UnboundGenericType);
