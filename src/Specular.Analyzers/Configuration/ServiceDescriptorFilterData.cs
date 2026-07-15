using System.Collections.Immutable;

namespace Specular.Analyzers.Configuration;

public record ServiceDescriptorFilterData
(
    ImmutableArray<ServiceTypeData> ServiceTypeDescriptors,
    int Lifetime
);
