# ServiceRegistrationBaseAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-453/api/specular.md)
- Version: `0.4.3-pr.13`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-453-merge.Sha.e15ed429f3bf41f14f92fb73273b178497785fb7/src/Specular/ServiceRegistrationAttribute.cs)
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
