# ServiceRegistrationAttribute<TService1, TService2, TService3, TService4>

- Kind: `class`
- Package: [Specular](/Specular/v0.4.0/api/specular.md)
- Version: `0.4.0`
- Namespace: `Specular`
- Target framework: `net10.0`
- Inherits: [ServiceRegistrationBaseAttribute](/Specular/v0.4.0/api/specular/serviceregistrationbaseattribute.md)

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

- [ServiceRegistrationAttribute(ServiceLifetime)](/Specular/v0.4.0/api/specular/serviceregistrationattribute-4/constructors.md#constructor-servicelifetime) -- Attribute used to define the service registration of a given type
