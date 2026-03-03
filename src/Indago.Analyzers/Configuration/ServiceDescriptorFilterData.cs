using System.Collections.Immutable;

namespace Indago.Analyzers.Configuration;

public record ServiceDescriptorFilterData
(
    ImmutableArray<ServiceTypeData> ServiceTypeDescriptors,
    int Lifetime
);
