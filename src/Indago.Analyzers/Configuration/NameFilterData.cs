using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Indago.Analyzers.Descriptors;

namespace Indago.Analyzers.Configuration;

public record NameFilterData
(
    [property: JsonPropertyName("i")]
    bool Include,
    [property: JsonPropertyName("f")]
    TextDirectionFilter Filter,
    [property: JsonPropertyName("n")]
    ImmutableArray<string> Names);
