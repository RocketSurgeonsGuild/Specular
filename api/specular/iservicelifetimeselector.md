# IServiceLifetimeSelector

- Kind: `interface`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.3-beta.11`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.7198f229aa17bd9ae0e7bbde62dbec64e80e7d0a/src/Specular/Abstractions/IServiceLifetimeSelector.cs)
- Implements: [IServiceTypeSelector](/Specular/api/specular/iservicetypeselector.md)

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

- [WithLifetime(ServiceLifetime)](/Specular/api/specular/iservicelifetimeselector/methods.md#withlifetime-servicelifetime) `abstract` -- Registers each matching concrete type with the specified `lifetime`.
- [WithScopedLifetime](/Specular/api/specular/iservicelifetimeselector/methods.md#withscopedlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Scoped` lifetime.
- [WithSingletonLifetime](/Specular/api/specular/iservicelifetimeselector/methods.md#withsingletonlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Singleton` lifetime.
- [WithTransientLifetime](/Specular/api/specular/iservicelifetimeselector/methods.md#withtransientlifetime) `abstract` -- Registers each matching concrete type with `ServiceLifetime.Transient` lifetime.
