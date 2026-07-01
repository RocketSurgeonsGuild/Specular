namespace Indago.Analyzers.Descriptors;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated via the generic type-filter pipeline / reflection-free generics.")]
internal sealed record CompiledAbortServiceTypeDescriptor : IServiceTypeDescriptor;
