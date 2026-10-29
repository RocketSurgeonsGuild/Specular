# ServiceRegistrationAttribute<TService1, TService2, TService3>

- Kind: `class`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.3.3-beta.5`
- Namespace: `Specular`
- Target framework: `net10.0`
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
