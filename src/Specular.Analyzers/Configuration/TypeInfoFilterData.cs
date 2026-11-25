using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Specular.Scanning;

namespace Specular.Analyzers.Configuration;

internal sealed record TypeInfoFilterData
(
    [property: JsonPropertyName("f")]
    bool Include,
    [property: JsonPropertyName("t")]
    ImmutableArray<TypeInfoFilter> TypeInfos);
