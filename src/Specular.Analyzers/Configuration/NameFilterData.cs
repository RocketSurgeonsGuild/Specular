using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Specular.Scanning;

namespace Specular.Analyzers.Configuration;

internal sealed record NameFilterData
(
    [property: JsonPropertyName("i")]
    bool Include,
    [property: JsonPropertyName("f")]
    TextDirectionFilter Filter,
    [property: JsonPropertyName("n")]
    ImmutableArray<string> Names);
