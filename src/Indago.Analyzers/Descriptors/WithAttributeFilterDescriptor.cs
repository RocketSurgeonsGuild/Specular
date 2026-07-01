using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record WithAttributeFilterDescriptor(INamedTypeSymbol Attribute) : ITypeFilterDescriptor;
