using Indago.Analyzers.Configuration;
using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.Descriptors;

internal record CompiledServiceTypeDescriptor(INamedTypeSymbol Type) : IServiceTypeDescriptor;

internal record UnknownCompiledServiceTypeDescriptor(AnyTypeData Data) : IServiceTypeDescriptor;
