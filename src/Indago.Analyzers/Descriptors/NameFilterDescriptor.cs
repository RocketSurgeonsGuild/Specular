using System.Collections.Immutable;
using System.Diagnostics;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal record NameFilterDescriptor(bool Include, TextDirectionFilter Filter, ImmutableHashSet<string> Names) : ITypeFilterDescriptor;
