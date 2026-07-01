using System.Collections.Immutable;
using System.Diagnostics;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record TypeInfoFilterDescriptor(bool Include, ImmutableHashSet<TypeInfoFilter> TypeInfos) : ITypeFilterDescriptor;
