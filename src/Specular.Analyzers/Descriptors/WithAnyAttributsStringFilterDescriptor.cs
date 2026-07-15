using System.Collections.Immutable;
using System.Diagnostics;

namespace Specular.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record WithAnyAttributeStringFilterDescriptor(ImmutableHashSet<string> AttributeClassNames) : ITypeFilterDescriptor;
