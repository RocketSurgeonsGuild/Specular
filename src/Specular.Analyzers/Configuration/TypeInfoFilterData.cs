using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Specular.Analyzers.Descriptors;

namespace Specular.Analyzers.Configuration;

public record TypeInfoFilterData
(
    [property: JsonPropertyName("f")]
    bool Include,
    [property: JsonPropertyName("t")]
    ImmutableArray<TypeInfoFilter> TypeInfos);
