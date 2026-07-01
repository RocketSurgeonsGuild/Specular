using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record WithoutAttributeFilterDescriptor(INamedTypeSymbol Attribute) : ITypeFilterDescriptor;
