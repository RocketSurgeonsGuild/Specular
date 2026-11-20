# KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6>

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-445/api/specular.md)
- Version: `0.4.2-pr.64`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-445-merge.Sha.148444cdd6691571fa2f9e5421c54e07d5203dfc/src/Specular/KeyedServiceRegistrationAttribute.cs)
- Inherits: [KeyedServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-445/api/specular/keyedserviceregistrationbaseattribute.md)

Attribute used to define the keyed service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6>
    : Specular.KeyedServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the key and lifetime

## Constructors

- [KeyedServiceRegistrationAttribute(object?, ServiceLifetime)](/Specular/pr-preview/pr-445/api/specular/keyedserviceregistrationattribute-6/constructors.md#constructor-object-servicelifetime) -- Attribute used to define the keyed service registration of a given type
