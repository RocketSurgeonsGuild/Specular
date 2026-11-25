# KeyedServiceRegistrationBaseAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-453/api/specular.md)
- Version: `0.4.3-pr.13`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-453-merge.Sha.e15ed429f3bf41f14f92fb73273b178497785fb7/src/Specular/KeyedServiceRegistrationAttribute.cs)
- Inherits: `Attribute`

Attribute used to define the keyed service registration of a given type. Behaves the same as [ServiceRegistrationAttribute](/Specular/pr-preview/pr-453/api/specular/serviceregistrationattribute.md) except the resulting registration is a [keyed service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services).

## Definition

```csharp
namespace Specular;

public abstract class KeyedServiceRegistrationBaseAttribute
    : System.Attribute
{
    // ...
}
```

## Remarks

Constructor to specify the key, service type including optional runtime

## Properties

- [Key](/Specular/pr-preview/pr-453/api/specular/keyedserviceregistrationbaseattribute/properties.md#key) : `object?` `get` -- The key the service is registered against
- [Lifetime](/Specular/pr-preview/pr-453/api/specular/keyedserviceregistrationbaseattribute/properties.md#lifetime) : `ServiceLifetime` `get` -- The lifetime
- [ServiceTypes](/Specular/pr-preview/pr-453/api/specular/keyedserviceregistrationbaseattribute/properties.md#servicetypes) : `ImmutableArray<Type>` `get` -- The service type
