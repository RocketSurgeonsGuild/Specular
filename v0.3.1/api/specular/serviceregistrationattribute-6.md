# ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6>

- Kind: `class`
- Package: [Specular](/Specular/v0.3.1/api/specular.md)
- Version: `0.3.1`
- Namespace: `Specular`
- Target framework: `net10.0`
- Inherits: [ServiceRegistrationBaseAttribute](/Specular/v0.3.1/api/specular/serviceregistrationbaseattribute.md)

Attribute used to define the service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6>
    : Specular.ServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the lifetime

## Constructors

- [ServiceRegistrationAttribute(ServiceLifetime)](/Specular/v0.3.1/api/specular/serviceregistrationattribute-6/constructors.md#constructor-servicelifetime) -- Attribute used to define the service registration of a given type
