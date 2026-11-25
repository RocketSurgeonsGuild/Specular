using System.Collections.Immutable;

namespace Specular.Analyzers.Configuration;

internal sealed record ServiceDescriptorFilterData
(
    ImmutableArray<ServiceTypeData> ServiceTypeDescriptors,
    int Lifetime
);
