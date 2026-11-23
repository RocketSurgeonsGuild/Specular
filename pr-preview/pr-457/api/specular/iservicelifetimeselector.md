# IServiceLifetimeSelector

- Kind: `interface`
- Package: [Specular](/Specular/pr-preview/pr-457/api/specular.md)
- Version: `0.4.3-pr.10`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-457-merge.Sha.63ef56f819f6729dc27767f4d81e41dd73901dc3/src/Specular/Abstractions/IServiceLifetimeSelector.cs)
- Implements: [IServiceTypeSelector](/Specular/pr-preview/pr-457/api/specular/iservicetypeselector.md)

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

- [WithLifetime(ServiceLifetime)](/Specular/pr-preview/pr-457/api/specular/iservicelifetimeselector/methods.md#withlifetime-servicelifetime) `abstract` -- Registers each matching concrete type with the specified `lifetime`.
- [WithScopedLifetime](/Specular/pr-preview/pr-457/api/specular/iservicelifetimeselector/methods.md#withscopedlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Scoped` lifetime.
- [WithSingletonLifetime](/Specular/pr-preview/pr-457/api/specular/iservicelifetimeselector/methods.md#withsingletonlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Singleton` lifetime.
- [WithTransientLifetime](/Specular/pr-preview/pr-457/api/specular/iservicelifetimeselector/methods.md#withtransientlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Transient` lifetime.
