# IServiceTypeSelector

- Kind: `interface`
- Package: [Specular](/Specular/pr-preview/pr-346/api/specular.md)
- Version: `0.3.3-pr.5`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`

The Compiled Service Type Selector

## Definition

```csharp
namespace Specular.Abstractions;

public interface IServiceTypeSelector
{
    // ...
}
```

## Methods

- [As](/Specular/pr-preview/pr-346/api/specular/iservicetypeselector/methods.md#as) : [IServiceLifetimeSelector](/Specular/pr-preview/pr-346/api/specular/iservicelifetimeselector.md) `abstract` -- Registers each matching concrete type as `T`.
- [As(Type)](/Specular/pr-preview/pr-346/api/specular/iservicetypeselector/methods.md#as-type) : [IServiceLifetimeSelector](/Specular/pr-preview/pr-346/api/specular/iservicelifetimeselector.md) `abstract` -- Registers each matching concrete type as type
- [AsImplementedInterfaces](/Specular/pr-preview/pr-346/api/specular/iservicetypeselector/methods.md#asimplementedinterfaces) : [IServiceLifetimeSelector](/Specular/pr-preview/pr-346/api/specular/iservicelifetimeselector.md) `abstract` -- Registers each matching concrete type as all of its implemented interfaces.
- [AsImplementedInterfaces(Action<ITypeFilter>)](/Specular/pr-preview/pr-346/api/specular/iservicetypeselector/methods.md#asimplementedinterfaces-action-itypefilter) : [IServiceLifetimeSelector](/Specular/pr-preview/pr-346/api/specular/iservicelifetimeselector.md) `abstract` -- Registers each matching concrete type as all of its implemented interfaces.
- [AsMatchingInterface](/Specular/pr-preview/pr-346/api/specular/iservicetypeselector/methods.md#asmatchinginterface) : [IServiceLifetimeSelector](/Specular/pr-preview/pr-346/api/specular/iservicelifetimeselector.md) `abstract` -- Registers the type with the first found matching interface name. (e.g. ClassName is matched to IClassName)
- [AsSelf](/Specular/pr-preview/pr-346/api/specular/iservicetypeselector/methods.md#asself) : [IServiceLifetimeSelector](/Specular/pr-preview/pr-346/api/specular/iservicelifetimeselector.md) `abstract` -- Registers each matching concrete type as itself.
- [AsSelfWithInterfaces](/Specular/pr-preview/pr-346/api/specular/iservicetypeselector/methods.md#asselfwithinterfaces) : [IServiceLifetimeSelector](/Specular/pr-preview/pr-346/api/specular/iservicelifetimeselector.md) `abstract` -- Registers each matching concrete type as all of its implemented interfaces, by returning an instance of the main type
- [AsSelfWithInterfaces(Action<ITypeFilter>)](/Specular/pr-preview/pr-346/api/specular/iservicetypeselector/methods.md#asselfwithinterfaces-action-itypefilter) : [IServiceLifetimeSelector](/Specular/pr-preview/pr-346/api/specular/iservicelifetimeselector.md) `abstract` -- Registers each matching concrete type as all of its implemented interfaces, by returning an instance of the main type
