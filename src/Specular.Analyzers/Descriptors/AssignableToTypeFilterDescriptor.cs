using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Specular.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record AssignableToTypeFilterDescriptor(INamedTypeSymbol Type) : ITypeFilterDescriptor;
