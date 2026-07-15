using System.Text.Json.Serialization;

namespace Specular.Analyzers.Configuration;

public record WithAttributeStringData
(
    [property: JsonPropertyName("i")]
    bool Include,
    [property: JsonPropertyName("b")]
    string Attribute);
