# RegistrationLifetimeAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-462/api/specular.md)
- Version: `0.4.3-pr.15`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-462-merge.Sha.65577d9b63559d60a65f5887b78a6f971676b065/src/Specular/RegistrationLifetimeAttribute.cs)
- Inherits: `Attribute`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-462/api/specular/ispecularprovider.md)

## Definition

```csharp
namespace Specular;

public class RegistrationLifetimeAttribute
    : System.Attribute
{
    // ...
}
```

## Remarks

Constructor to specify the lifetime

## Constructors

- [RegistrationLifetimeAttribute(ServiceLifetime)](/Specular/pr-preview/pr-462/api/specular/registrationlifetimeattribute/constructors.md#constructor-servicelifetime) -- Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-462/api/specular/ispecularprovider.md)

## Properties

- [Lifetime](/Specular/pr-preview/pr-462/api/specular/registrationlifetimeattribute/properties.md#lifetime) : `ServiceLifetime` `get` -- The lifetime
