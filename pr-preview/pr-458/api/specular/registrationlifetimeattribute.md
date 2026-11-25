# RegistrationLifetimeAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-458/api/specular.md)
- Version: `0.4.3-pr.51`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-458-merge.Sha.e349b4d07697a27d2f7d2f0edd92c5e34f9b8f68/src/Specular/RegistrationLifetimeAttribute.cs)
- Inherits: `Attribute`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-458/api/specular/ispecularprovider.md)

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

- [RegistrationLifetimeAttribute(ServiceLifetime)](/Specular/pr-preview/pr-458/api/specular/registrationlifetimeattribute/constructors.md#constructor-servicelifetime) -- Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-458/api/specular/ispecularprovider.md)

## Properties

- [Lifetime](/Specular/pr-preview/pr-458/api/specular/registrationlifetimeattribute/properties.md#lifetime) : `ServiceLifetime` `get` -- The lifetime
