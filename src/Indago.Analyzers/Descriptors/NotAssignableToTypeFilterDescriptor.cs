using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal record NotAssignableToTypeFilterDescriptor(INamedTypeSymbol Type) : ITypeFilterDescriptor;
