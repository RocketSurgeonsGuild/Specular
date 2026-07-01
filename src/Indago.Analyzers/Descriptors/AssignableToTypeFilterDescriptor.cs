using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record AssignableToTypeFilterDescriptor(INamedTypeSymbol Type) : ITypeFilterDescriptor;
