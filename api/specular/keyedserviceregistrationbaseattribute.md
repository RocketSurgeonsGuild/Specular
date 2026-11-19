# KeyedServiceRegistrationBaseAttribute

- Kind: `class`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.2-beta.52`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.8bcc62d19c09284f6923e54e588877e41baf4070/src/Specular/KeyedServiceRegistrationAttribute.cs)
- Inherits: `Attribute`

Attribute used to define the keyed service registration of a given type. Behaves the same as [ServiceRegistrationAttribute](/Specular/api/specular/serviceregistrationattribute.md) except the resulting registration is a [keyed service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services).

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

- [Key](/Specular/api/specular/keyedserviceregistrationbaseattribute/properties.md#key) : `object?` `get` -- The key the service is registered against
- [Lifetime](/Specular/api/specular/keyedserviceregistrationbaseattribute/properties.md#lifetime) : `ServiceLifetime` `get` -- The lifetime
- [ServiceTypes](/Specular/api/specular/keyedserviceregistrationbaseattribute/properties.md#servicetypes) : `ImmutableArray<Type>` `get` -- The service type
