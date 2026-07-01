using Indago.Analyzers.Configuration;
using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.Descriptors;

internal sealed record CompiledServiceTypeDescriptor(INamedTypeSymbol Type) : IServiceTypeDescriptor;

internal sealed record UnknownCompiledServiceTypeDescriptor(AnyTypeData Data) : IServiceTypeDescriptor;
