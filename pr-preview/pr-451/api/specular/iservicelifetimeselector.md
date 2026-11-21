# IServiceLifetimeSelector

- Kind: `interface`
- Package: [Specular](/Specular/pr-preview/pr-451/api/specular.md)
- Version: `0.4.3-pr.3`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-451-merge.Sha.3976afc911b40cb4a244b415afe8ec88dc2bcb05/src/Specular/Abstractions/IServiceLifetimeSelector.cs)
- Implements: [IServiceTypeSelector](/Specular/pr-preview/pr-451/api/specular/iservicetypeselector.md)

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

- [WithLifetime(ServiceLifetime)](/Specular/pr-preview/pr-451/api/specular/iservicelifetimeselector/methods.md#withlifetime-servicelifetime) `abstract` -- Registers each matching concrete type with the specified `lifetime`.
- [WithScopedLifetime](/Specular/pr-preview/pr-451/api/specular/iservicelifetimeselector/methods.md#withscopedlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Scoped` lifetime.
- [WithSingletonLifetime](/Specular/pr-preview/pr-451/api/specular/iservicelifetimeselector/methods.md#withsingletonlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Singleton` lifetime.
- [WithTransientLifetime](/Specular/pr-preview/pr-451/api/specular/iservicelifetimeselector/methods.md#withtransientlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Transient` lifetime.
