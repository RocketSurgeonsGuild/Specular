# ServiceRegistrationAttribute<TService1, TService2>

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-443/api/specular.md)
- Version: `0.4.2-pr.63`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-443-merge.Sha.0033aac02b7565a1ecf99425ec7bdc404ad43560/src/Specular/ServiceRegistrationAttribute.cs)
- Inherits: [ServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-443/api/specular/serviceregistrationbaseattribute.md)

Attribute used to define the service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class ServiceRegistrationAttribute<TService1, TService2>
    : Specular.ServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the lifetime

## Constructors

- [ServiceRegistrationAttribute(ServiceLifetime)](/Specular/pr-preview/pr-443/api/specular/serviceregistrationattribute-2/constructors.md#constructor-servicelifetime) -- Attribute used to define the service registration of a given type
