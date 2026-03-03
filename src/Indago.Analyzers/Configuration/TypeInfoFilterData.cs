using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Indago.Analyzers.Descriptors;

namespace Indago.Analyzers.Configuration;

public record TypeInfoFilterData
(
    [property: JsonPropertyName("f")]
    bool Include,
    [property: JsonPropertyName("t")]
    ImmutableArray<TypeInfoFilter> TypeInfos);
