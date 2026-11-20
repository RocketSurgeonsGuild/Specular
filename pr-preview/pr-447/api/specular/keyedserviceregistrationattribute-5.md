# KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-447/api/specular.md)
- Version: `0.4.2-pr.66`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/src/Specular/KeyedServiceRegistrationAttribute.cs)
- Inherits: [KeyedServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-447/api/specular/keyedserviceregistrationbaseattribute.md)

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

- [KeyedServiceRegistrationAttribute(object?, ServiceLifetime)](/Specular/pr-preview/pr-447/api/specular/keyedserviceregistrationattribute-5/constructors.md#constructor-object-servicelifetime) -- Attribute used to define the keyed service registration of a given type
