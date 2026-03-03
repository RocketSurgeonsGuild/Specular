using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal record WithoutAttributeFilterDescriptor(INamedTypeSymbol Attribute) : ITypeFilterDescriptor;
