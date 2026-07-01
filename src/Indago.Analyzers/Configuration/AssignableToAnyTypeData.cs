using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Indago.Analyzers.Configuration;

public record AssignableToAnyTypeData
(
    [property: JsonPropertyName("i")]
    bool Include,
    [property: JsonPropertyName("t")]
    ImmutableArray<AnyTypeData> Types);
