# RegistrationLifetimeAttribute

- Kind: `class`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.2-beta.50`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.d4a702693c72a74c7d2f15946a641a7a58285f09/src/Specular/RegistrationLifetimeAttribute.cs)
- Inherits: `Attribute`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/api/specular/ispecularprovider.md)

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

- [RegistrationLifetimeAttribute(ServiceLifetime)](/Specular/api/specular/registrationlifetimeattribute/constructors.md#constructor-servicelifetime) -- Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/api/specular/ispecularprovider.md)

## Properties

- [Lifetime](/Specular/api/specular/registrationlifetimeattribute/properties.md#lifetime) : `ServiceLifetime` `get` -- The lifetime
