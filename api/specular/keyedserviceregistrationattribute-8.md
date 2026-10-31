# KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8>

- Kind: `class`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.1-beta.3`
- Namespace: `Specular`
- Target framework: `net10.0`
- Inherits: [KeyedServiceRegistrationBaseAttribute](/Specular/api/specular/keyedserviceregistrationbaseattribute.md)

Attribute used to define the keyed service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8>
    : Specular.KeyedServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the key and lifetime

## Constructors

- [KeyedServiceRegistrationAttribute(object?, ServiceLifetime)](/Specular/api/specular/keyedserviceregistrationattribute-8/constructors.md#constructor-object-servicelifetime) -- Attribute used to define the keyed service registration of a given type
