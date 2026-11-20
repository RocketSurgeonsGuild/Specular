# KeyedServiceRegistrationAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-446/api/specular.md)
- Version: `0.4.2-pr.62`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-446-merge.Sha.bdccb36ec194143897302d3d96fd4fa575d90fa8/src/Specular/KeyedServiceRegistrationAttribute.cs)
- Inherits: [KeyedServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-446/api/specular/keyedserviceregistrationbaseattribute.md)

Attribute used to define the keyed service registration of a given type. Behaves the same as [ServiceRegistrationAttribute](/Specular/pr-preview/pr-446/api/specular/serviceregistrationattribute.md) except the resulting registration is a [keyed service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services).

## Definition

```csharp
namespace Specular;

public sealed class KeyedServiceRegistrationAttribute
    : Specular.KeyedServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the key, service type including optional runtime

## Constructors

- [KeyedServiceRegistrationAttribute(object?, ServiceLifetime, Type[])](/Specular/pr-preview/pr-446/api/specular/keyedserviceregistrationattribute/constructors.md#constructor-object-servicelifetime-type) -- Attribute used to define the keyed service registration of a given type. Behaves the same as [ServiceRegistrationAttribute](/Specular/pr-preview/pr-446/api/specular/serviceregistrationattribute.md) except the resulting registration is a [keyed service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services).
