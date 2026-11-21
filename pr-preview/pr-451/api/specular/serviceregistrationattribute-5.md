# ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-451/api/specular.md)
- Version: `0.4.3-pr.3`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-451-merge.Sha.3976afc911b40cb4a244b415afe8ec88dc2bcb05/src/Specular/ServiceRegistrationAttribute.cs)
- Inherits: [ServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-451/api/specular/serviceregistrationbaseattribute.md)

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

- [ServiceRegistrationAttribute(ServiceLifetime)](/Specular/pr-preview/pr-451/api/specular/serviceregistrationattribute-5/constructors.md#constructor-servicelifetime) -- Attribute used to define the service registration of a given type
