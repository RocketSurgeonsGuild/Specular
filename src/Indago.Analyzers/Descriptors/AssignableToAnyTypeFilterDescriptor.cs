using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record AssignableToAnyTypeFilterDescriptor(ImmutableHashSet<INamedTypeSymbol> Types) : ITypeFilterDescriptor;
