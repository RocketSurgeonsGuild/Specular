using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal record TypeKindFilterDescriptor(bool Include, ImmutableHashSet<TypeKind> TypeKinds) : ITypeFilterDescriptor;
