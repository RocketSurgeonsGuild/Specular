# ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>

- Kind: `class`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.2-beta.64`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.92ad0972fc551fb7fe4da29ef78029c66b11b489/src/Specular/ServiceRegistrationAttribute.cs)
- Inherits: [ServiceRegistrationBaseAttribute](/Specular/api/specular/serviceregistrationbaseattribute.md)

Attribute used to define the service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>
    : Specular.ServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the lifetime

## Constructors

- [ServiceRegistrationAttribute(ServiceLifetime)](/Specular/api/specular/serviceregistrationattribute-5/constructors.md#constructor-servicelifetime) -- Attribute used to define the service registration of a given type
