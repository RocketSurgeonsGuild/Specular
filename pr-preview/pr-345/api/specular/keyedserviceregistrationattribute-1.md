# KeyedServiceRegistrationAttribute<TService>

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-345/api/specular.md)
- Version: `0.3.3-pr.5`
- Namespace: `Specular`
- Target framework: `net10.0`
- Inherits: [KeyedServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-345/api/specular/keyedserviceregistrationbaseattribute.md)

Attribute used to define the keyed service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class KeyedServiceRegistrationAttribute<TService>
    : Specular.KeyedServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the key and lifetime

## Constructors

- [KeyedServiceRegistrationAttribute(object?, ServiceLifetime)](/Specular/pr-preview/pr-345/api/specular/keyedserviceregistrationattribute-1/constructors.md#constructor-object-servicelifetime) -- Attribute used to define the keyed service registration of a given type
