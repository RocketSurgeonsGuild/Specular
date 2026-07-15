using Specular.Analyzers.Configuration;
using Microsoft.CodeAnalysis;

namespace Specular.Analyzers.Descriptors;

internal sealed record CompiledServiceTypeDescriptor(INamedTypeSymbol Type) : IServiceTypeDescriptor;

internal sealed record UnknownCompiledServiceTypeDescriptor(AnyTypeData Data) : IServiceTypeDescriptor;
