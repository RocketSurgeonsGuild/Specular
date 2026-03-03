using Indago.Analyzers.AssemblyProviders;

namespace Indago.Analyzers.Descriptors;

internal record ImplementedInterfacesServiceTypeDescriptor(CompiledTypeFilter? InterfaceFilter) : IServiceTypeDescriptor;
