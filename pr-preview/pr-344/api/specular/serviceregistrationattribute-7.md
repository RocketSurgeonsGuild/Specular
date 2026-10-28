# ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7>

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-344/api/specular.md)
- Version: `0.3.3-pr.4`
- Namespace: `Specular`
- Target framework: `net10.0`
- Inherits: [ServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-344/api/specular/serviceregistrationbaseattribute.md)

Attribute used to define the service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7>
    : Specular.ServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the lifetime

## Constructors

- [ServiceRegistrationAttribute(ServiceLifetime)](/Specular/pr-preview/pr-344/api/specular/serviceregistrationattribute-7/constructors.md#constructor-servicelifetime) -- Attribute used to define the service registration of a given type
