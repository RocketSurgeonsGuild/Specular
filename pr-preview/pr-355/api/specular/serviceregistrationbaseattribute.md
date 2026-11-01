# ServiceRegistrationBaseAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-355/api/specular.md)
- Version: `0.4.1-pr.5`
- Namespace: `Specular`
- Target framework: `net10.0`
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

- [Lifetime](/Specular/pr-preview/pr-355/api/specular/serviceregistrationbaseattribute/properties.md#lifetime) : `ServiceLifetime` `get` -- The lifetime
- [ServiceTypes](/Specular/pr-preview/pr-355/api/specular/serviceregistrationbaseattribute/properties.md#servicetypes) : `ImmutableArray<Type>` `get` -- The service type
