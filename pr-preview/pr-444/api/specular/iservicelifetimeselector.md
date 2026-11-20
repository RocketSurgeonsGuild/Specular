# IServiceLifetimeSelector

- Kind: `interface`
- Package: [Specular](/Specular/pr-preview/pr-444/api/specular.md)
- Version: `0.4.2-pr.62`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-444-merge.Sha.91db23f0a6dd2aea896fb5788d96dd2d2a023c25/src/Specular/Abstractions/IServiceLifetimeSelector.cs)
- Implements: [IServiceTypeSelector](/Specular/pr-preview/pr-444/api/specular/iservicetypeselector.md)

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

- [WithLifetime(ServiceLifetime)](/Specular/pr-preview/pr-444/api/specular/iservicelifetimeselector/methods.md#withlifetime-servicelifetime) `abstract` -- Registers each matching concrete type with the specified `lifetime`.
- [WithScopedLifetime](/Specular/pr-preview/pr-444/api/specular/iservicelifetimeselector/methods.md#withscopedlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Scoped` lifetime.
- [WithSingletonLifetime](/Specular/pr-preview/pr-444/api/specular/iservicelifetimeselector/methods.md#withsingletonlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Singleton` lifetime.
- [WithTransientLifetime](/Specular/pr-preview/pr-444/api/specular/iservicelifetimeselector/methods.md#withtransientlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Transient` lifetime.
