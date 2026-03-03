using System.Collections.Immutable;
using System.Diagnostics;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal record WithAnyAttributeStringFilterDescriptor(ImmutableHashSet<string> AttributeClassNames) : ITypeFilterDescriptor;
