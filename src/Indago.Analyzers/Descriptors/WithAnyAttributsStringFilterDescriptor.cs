using System.Collections.Immutable;
using System.Diagnostics;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record WithAnyAttributeStringFilterDescriptor(ImmutableHashSet<string> AttributeClassNames) : ITypeFilterDescriptor;
