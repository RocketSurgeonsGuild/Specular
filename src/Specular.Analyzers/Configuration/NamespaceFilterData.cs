using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Specular.Scanning;

namespace Specular.Analyzers.Configuration;

internal sealed record NamespaceFilterData
(
    [property: JsonPropertyName("f")]
    NamespaceFilter Filter,
    [property: JsonPropertyName("n")]
    ImmutableArray<string> Namespaces);
