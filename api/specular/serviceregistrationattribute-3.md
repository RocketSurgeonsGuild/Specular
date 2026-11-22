# ServiceRegistrationAttribute<TService1, TService2, TService3>

- Kind: `class`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.3-beta.8`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.71a81f46ec8e0f8bc11d811fc85b38e1320f9d2a/src/Specular/ServiceRegistrationAttribute.cs)
- Inherits: [ServiceRegistrationBaseAttribute](/Specular/api/specular/serviceregistrationbaseattribute.md)

Attribute used to define the service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3>
    : Specular.ServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the lifetime

## Constructors

- [ServiceRegistrationAttribute(ServiceLifetime)](/Specular/api/specular/serviceregistrationattribute-3/constructors.md#constructor-servicelifetime) -- Attribute used to define the service registration of a given type
