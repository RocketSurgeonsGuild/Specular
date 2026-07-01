using Indago.Analyzers.AssemblyProviders;

namespace Indago.Analyzers.Descriptors;

internal sealed record ImplementedInterfacesServiceTypeDescriptor(CompiledTypeFilter? InterfaceFilter) : IServiceTypeDescriptor;
