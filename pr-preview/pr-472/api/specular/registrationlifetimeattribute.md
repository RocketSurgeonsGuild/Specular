# RegistrationLifetimeAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-472/api/specular.md)
- Version: `0.4.3-pr.21`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-472-merge.Sha.a46222835e40014b71e6fa98a0c0700389a121c5/src/Specular/RegistrationLifetimeAttribute.cs)
- Inherits: `Attribute`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-472/api/specular/ispecularprovider.md)

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

- [RegistrationLifetimeAttribute(ServiceLifetime)](/Specular/pr-preview/pr-472/api/specular/registrationlifetimeattribute/constructors.md#constructor-servicelifetime) -- Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-472/api/specular/ispecularprovider.md)

## Properties

- [Lifetime](/Specular/pr-preview/pr-472/api/specular/registrationlifetimeattribute/properties.md#lifetime) : `ServiceLifetime` `get` -- The lifetime
