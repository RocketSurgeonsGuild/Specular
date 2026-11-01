# ServiceRegistrationAttribute<TService>

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-355/api/specular.md)
- Version: `0.4.1-pr.5`
- Namespace: `Specular`
- Target framework: `net10.0`
- Inherits: [ServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-355/api/specular/serviceregistrationbaseattribute.md)

Attribute used to define the service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class ServiceRegistrationAttribute<TService>
    : Specular.ServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the lifetime

## Constructors

- [ServiceRegistrationAttribute(ServiceLifetime)](/Specular/pr-preview/pr-355/api/specular/serviceregistrationattribute-1/constructors.md#constructor-servicelifetime) -- Attribute used to define the service registration of a given type
