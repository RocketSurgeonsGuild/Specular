using System.Collections.Immutable;
using System.Diagnostics;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record NamespaceFilterDescriptor(NamespaceFilter Filter, ImmutableHashSet<string> Namespaces) : ITypeFilterDescriptor;
