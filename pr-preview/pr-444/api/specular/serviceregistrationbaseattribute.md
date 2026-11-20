# ServiceRegistrationBaseAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-444/api/specular.md)
- Version: `0.4.2-pr.62`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-444-merge.Sha.91db23f0a6dd2aea896fb5788d96dd2d2a023c25/src/Specular/ServiceRegistrationAttribute.cs)
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

- [Lifetime](/Specular/pr-preview/pr-444/api/specular/serviceregistrationbaseattribute/properties.md#lifetime) : `ServiceLifetime` `get` -- The lifetime
- [ServiceTypes](/Specular/pr-preview/pr-444/api/specular/serviceregistrationbaseattribute/properties.md#servicetypes) : `ImmutableArray<Type>` `get` -- The service type
