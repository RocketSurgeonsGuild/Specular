# RegistrationLifetimeAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-431/api/specular.md)
- Version: `0.4.2-pr.47`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-431-merge.Sha.e8ba1688856cc88c795c2a6c26416db05d34b225/src/Specular/RegistrationLifetimeAttribute.cs)
- Inherits: `Attribute`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-431/api/specular/ispecularprovider.md)

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

- [RegistrationLifetimeAttribute(ServiceLifetime)](/Specular/pr-preview/pr-431/api/specular/registrationlifetimeattribute/constructors.md#constructor-servicelifetime) -- Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-431/api/specular/ispecularprovider.md)

## Properties

- [Lifetime](/Specular/pr-preview/pr-431/api/specular/registrationlifetimeattribute/properties.md#lifetime) : `ServiceLifetime` `get` -- The lifetime
