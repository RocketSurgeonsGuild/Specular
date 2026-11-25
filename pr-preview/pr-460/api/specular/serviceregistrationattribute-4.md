# ServiceRegistrationAttribute<TService1, TService2, TService3, TService4>

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-460/api/specular.md)
- Version: `0.4.3-pr.13`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-460-merge.Sha.d41c5fa5888078044d4dab2d9f0a772bbcacc924/src/Specular/ServiceRegistrationAttribute.cs)
- Inherits: [ServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-460/api/specular/serviceregistrationbaseattribute.md)

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

- [ServiceRegistrationAttribute(ServiceLifetime)](/Specular/pr-preview/pr-460/api/specular/serviceregistrationattribute-4/constructors.md#constructor-servicelifetime) -- Attribute used to define the service registration of a given type
