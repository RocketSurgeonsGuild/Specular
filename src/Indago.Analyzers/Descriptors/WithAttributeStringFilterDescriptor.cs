using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record WithAttributeStringFilterDescriptor
(
    [property: JsonPropertyName("a")]
    string AttributeClassName) : ITypeFilterDescriptor;
