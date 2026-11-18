# ServiceRegistrationAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-406/api/specular.md)
- Version: `0.4.2-pr.52`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-406-merge.Sha.a5bb8c31ae5b3055ce3bbac8a150aa5acd0acd0a/src/Specular/ServiceRegistrationAttribute.cs)
- Inherits: [ServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-406/api/specular/serviceregistrationbaseattribute.md)

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

- [ServiceRegistrationAttribute(ServiceLifetime, Type[])](/Specular/pr-preview/pr-406/api/specular/serviceregistrationattribute/constructors.md#constructor-servicelifetime-type) -- Attribute used to define the service registration of a given type
