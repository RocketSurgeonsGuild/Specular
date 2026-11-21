# KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4>

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-449/api/specular.md)
- Version: `0.4.3-pr.3`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-449-merge.Sha.a4e7451225f525b22271723fffa01211c276f62e/src/Specular/KeyedServiceRegistrationAttribute.cs)
- Inherits: [KeyedServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-449/api/specular/keyedserviceregistrationbaseattribute.md)

Attribute used to define the keyed service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4>
    : Specular.KeyedServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the key and lifetime

## Constructors

- [KeyedServiceRegistrationAttribute(object?, ServiceLifetime)](/Specular/pr-preview/pr-449/api/specular/keyedserviceregistrationattribute-4/constructors.md#constructor-object-servicelifetime) -- Attribute used to define the keyed service registration of a given type
