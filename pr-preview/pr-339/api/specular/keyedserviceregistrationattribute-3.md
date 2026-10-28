# KeyedServiceRegistrationAttribute<TService1, TService2, TService3>

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-339/api/specular.md)
- Version: `0.3.2-pr.2`
- Namespace: `Specular`
- Target framework: `net10.0`
- Inherits: [KeyedServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-339/api/specular/keyedserviceregistrationbaseattribute.md)

Attribute used to define the keyed service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3>
    : Specular.KeyedServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the key and lifetime

## Constructors

- [KeyedServiceRegistrationAttribute(object?, ServiceLifetime)](/Specular/pr-preview/pr-339/api/specular/keyedserviceregistrationattribute-3/constructors.md#constructor-object-servicelifetime) -- Attribute used to define the keyed service registration of a given type
