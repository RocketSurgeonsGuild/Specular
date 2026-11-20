# KeyedServiceRegistrationAttribute<TService>

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-443/api/specular.md)
- Version: `0.4.2-pr.63`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-443-merge.Sha.0033aac02b7565a1ecf99425ec7bdc404ad43560/src/Specular/KeyedServiceRegistrationAttribute.cs)
- Inherits: [KeyedServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-443/api/specular/keyedserviceregistrationbaseattribute.md)

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

- [KeyedServiceRegistrationAttribute(object?, ServiceLifetime)](/Specular/pr-preview/pr-443/api/specular/keyedserviceregistrationattribute-1/constructors.md#constructor-object-servicelifetime) -- Attribute used to define the keyed service registration of a given type
