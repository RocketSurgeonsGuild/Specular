# IServiceLifetimeSelector

- Kind: `interface`
- Package: [Specular](/Specular/pr-preview/pr-447/api/specular.md)
- Version: `0.4.2-pr.66`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/src/Specular/Abstractions/IServiceLifetimeSelector.cs)
- Implements: [IServiceTypeSelector](/Specular/pr-preview/pr-447/api/specular/iservicetypeselector.md)

The Compiled Lifetime Selector

## Definition

```csharp
namespace Specular.Abstractions;

public interface IServiceLifetimeSelector
    : Specular.Abstractions.IServiceTypeSelector
{
    // ...
}
```

## Methods

- [WithLifetime(ServiceLifetime)](/Specular/pr-preview/pr-447/api/specular/iservicelifetimeselector/methods.md#withlifetime-servicelifetime) `abstract` -- Registers each matching concrete type with the specified `lifetime`.
- [WithScopedLifetime](/Specular/pr-preview/pr-447/api/specular/iservicelifetimeselector/methods.md#withscopedlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Scoped` lifetime.
- [WithSingletonLifetime](/Specular/pr-preview/pr-447/api/specular/iservicelifetimeselector/methods.md#withsingletonlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Singleton` lifetime.
- [WithTransientLifetime](/Specular/pr-preview/pr-447/api/specular/iservicelifetimeselector/methods.md#withtransientlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Transient` lifetime.
