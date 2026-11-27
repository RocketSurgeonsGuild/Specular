# ServiceRegistrationBaseAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-464/api/specular.md)
- Version: `0.4.3-pr.22`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-464-merge.Sha.1c51e62778e1813db280ec8727b59cd4afa8515f/src/Specular/ServiceRegistrationAttribute.cs)
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

- [Lifetime](/Specular/pr-preview/pr-464/api/specular/serviceregistrationbaseattribute/properties.md#lifetime) : `ServiceLifetime` `get` -- The lifetime
- [ServiceTypes](/Specular/pr-preview/pr-464/api/specular/serviceregistrationbaseattribute/properties.md#servicetypes) : `ImmutableArray<Type>` `get` -- The service type
