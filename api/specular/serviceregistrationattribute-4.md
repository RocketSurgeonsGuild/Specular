# ServiceRegistrationAttribute<TService1, TService2, TService3, TService4>

- Kind: `class`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.3-beta.5`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.b164b04e5bca8fcc8720179d844e43e9b939eae8/src/Specular/ServiceRegistrationAttribute.cs)
- Inherits: [ServiceRegistrationBaseAttribute](/Specular/api/specular/serviceregistrationbaseattribute.md)

Attribute used to define the service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4>
    : Specular.ServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the lifetime

## Constructors

- [ServiceRegistrationAttribute(ServiceLifetime)](/Specular/api/specular/serviceregistrationattribute-4/constructors.md#constructor-servicelifetime) -- Attribute used to define the service registration of a given type
