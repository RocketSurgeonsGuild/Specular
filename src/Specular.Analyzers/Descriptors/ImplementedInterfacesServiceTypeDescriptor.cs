using Specular.Analyzers.AssemblyProviders;

namespace Specular.Analyzers.Descriptors;

internal sealed record ImplementedInterfacesServiceTypeDescriptor(CompiledTypeFilter? InterfaceFilter) : IServiceTypeDescriptor;
