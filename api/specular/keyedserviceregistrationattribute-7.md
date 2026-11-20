# KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7>

- Kind: `class`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.2-beta.64`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.92ad0972fc551fb7fe4da29ef78029c66b11b489/src/Specular/KeyedServiceRegistrationAttribute.cs)
- Inherits: [KeyedServiceRegistrationBaseAttribute](/Specular/api/specular/keyedserviceregistrationbaseattribute.md)

Attribute used to define the keyed service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7>
    : Specular.KeyedServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the key and lifetime

## Constructors

- [KeyedServiceRegistrationAttribute(object?, ServiceLifetime)](/Specular/api/specular/keyedserviceregistrationattribute-7/constructors.md#constructor-object-servicelifetime) -- Attribute used to define the keyed service registration of a given type
