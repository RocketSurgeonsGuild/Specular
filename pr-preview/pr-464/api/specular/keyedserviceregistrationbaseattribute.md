# KeyedServiceRegistrationBaseAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-464/api/specular.md)
- Version: `0.4.3-pr.22`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-464-merge.Sha.1c51e62778e1813db280ec8727b59cd4afa8515f/src/Specular/KeyedServiceRegistrationAttribute.cs)
- Inherits: `Attribute`

Attribute used to define the keyed service registration of a given type. Behaves the same as [ServiceRegistrationAttribute](/Specular/pr-preview/pr-464/api/specular/serviceregistrationattribute.md) except the resulting registration is a [keyed service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services).

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

- [Key](/Specular/pr-preview/pr-464/api/specular/keyedserviceregistrationbaseattribute/properties.md#key) : `object?` `get` -- The key the service is registered against
- [Lifetime](/Specular/pr-preview/pr-464/api/specular/keyedserviceregistrationbaseattribute/properties.md#lifetime) : `ServiceLifetime` `get` -- The lifetime
- [ServiceTypes](/Specular/pr-preview/pr-464/api/specular/keyedserviceregistrationbaseattribute/properties.md#servicetypes) : `ImmutableArray<Type>` `get` -- The service type
