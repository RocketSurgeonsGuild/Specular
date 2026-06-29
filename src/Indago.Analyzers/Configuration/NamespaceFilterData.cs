using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Indago.Analyzers.Descriptors;

namespace Indago.Analyzers.Configuration;

public record NamespaceFilterData
(
    [property: JsonPropertyName("f")]
    NamespaceFilter Filter,
    [property: JsonPropertyName("n")]
    ImmutableArray<string> Namespaces);
