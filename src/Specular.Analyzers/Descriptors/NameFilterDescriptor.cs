using System.Collections.Immutable;
using System.Diagnostics;

namespace Specular.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record NameFilterDescriptor(bool Include, TextDirectionFilter Filter, ImmutableHashSet<string> Names) : ITypeFilterDescriptor;
