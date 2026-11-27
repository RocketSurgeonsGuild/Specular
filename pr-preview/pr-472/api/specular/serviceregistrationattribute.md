# ServiceRegistrationAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-472/api/specular.md)
- Version: `0.4.3-pr.21`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-472-merge.Sha.a46222835e40014b71e6fa98a0c0700389a121c5/src/Specular/ServiceRegistrationAttribute.cs)
- Inherits: [ServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-472/api/specular/serviceregistrationbaseattribute.md)

Attribute used to define the service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class ServiceRegistrationAttribute
    : Specular.ServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the service type including optional runtime

## Constructors

- [ServiceRegistrationAttribute(ServiceLifetime, Type[])](/Specular/pr-preview/pr-472/api/specular/serviceregistrationattribute/constructors.md#constructor-servicelifetime-type) -- Attribute used to define the service registration of a given type
