# RegistrationLifetimeAttribute

- Kind: `class`
- Package: [Specular](/Specular/v0.3.2/api/specular.md)
- Version: `0.3.2`
- Namespace: `Specular`
- Target framework: `net10.0`
- Inherits: `Attribute`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/v0.3.2/api/specular/ispecularprovider.md)

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

- [RegistrationLifetimeAttribute(ServiceLifetime)](/Specular/v0.3.2/api/specular/registrationlifetimeattribute/constructors.md#constructor-servicelifetime) -- Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/v0.3.2/api/specular/ispecularprovider.md)

## Properties

- [Lifetime](/Specular/v0.3.2/api/specular/registrationlifetimeattribute/properties.md#lifetime) : `ServiceLifetime` `get` -- The lifetime
