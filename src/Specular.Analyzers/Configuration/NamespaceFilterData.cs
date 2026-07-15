using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Specular.Analyzers.Descriptors;

namespace Specular.Analyzers.Configuration;

public record NamespaceFilterData
(
    [property: JsonPropertyName("f")]
    NamespaceFilter Filter,
    [property: JsonPropertyName("n")]
    ImmutableArray<string> Namespaces);
