# ServiceRegistrationBaseAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-453/api/specular.md)
- Version: `0.4.3-pr.10`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-453-merge.Sha.507a376b7125f9abab9ee28903b20cf32ac8ece3/src/Specular/ServiceRegistrationAttribute.cs)
- Inherits: `Attribute`

Attribute used to define the service registration of a given type

## Definition

```csharp
namespace Specular;

public abstract class ServiceRegistrationBaseAttribute
    : System.Attribute
{
    // ...
}
```

## Remarks

Constructor to specify the service type including optional runtime

## Properties

- [Lifetime](/Specular/pr-preview/pr-453/api/specular/serviceregistrationbaseattribute/properties.md#lifetime) : `ServiceLifetime` `get` -- The lifetime
- [ServiceTypes](/Specular/pr-preview/pr-453/api/specular/serviceregistrationbaseattribute/properties.md#servicetypes) : `ImmutableArray<Type>` `get` -- The service type
