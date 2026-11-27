# KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-464/api/specular.md)
- Version: `0.4.3-pr.22`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-464-merge.Sha.1c51e62778e1813db280ec8727b59cd4afa8515f/src/Specular/KeyedServiceRegistrationAttribute.cs)
- Inherits: [KeyedServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-464/api/specular/keyedserviceregistrationbaseattribute.md)

Attribute used to define the keyed service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>
    : Specular.KeyedServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the key and lifetime

## Constructors

- [KeyedServiceRegistrationAttribute(object?, ServiceLifetime)](/Specular/pr-preview/pr-464/api/specular/keyedserviceregistrationattribute-5/constructors.md#constructor-object-servicelifetime) -- Attribute used to define the keyed service registration of a given type
