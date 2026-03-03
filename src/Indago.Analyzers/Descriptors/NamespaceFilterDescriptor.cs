using System.Collections.Immutable;
using System.Diagnostics;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal record NamespaceFilterDescriptor(NamespaceFilter Filter, ImmutableHashSet<string> Namespaces) : ITypeFilterDescriptor;
