using System.Collections.Immutable;
using System.Diagnostics;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal record TypeInfoFilterDescriptor(bool Include, ImmutableHashSet<TypeInfoFilter> TypeInfos) : ITypeFilterDescriptor;
