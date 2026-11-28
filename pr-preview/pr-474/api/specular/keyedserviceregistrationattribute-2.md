# KeyedServiceRegistrationAttribute<TService1, TService2>

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-474/api/specular.md)
- Version: `0.4.3-pr.25`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-474-merge.Sha.1aeada8e35256c38d58c0d80324e7d9dd7e6c8f3/src/Specular/KeyedServiceRegistrationAttribute.cs)
- Inherits: [KeyedServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-474/api/specular/keyedserviceregistrationbaseattribute.md)

Attribute used to define the keyed service registration of a given type

## Definition

```csharp
namespace Specular;

public sealed class KeyedServiceRegistrationAttribute<TService1, TService2>
    : Specular.KeyedServiceRegistrationBaseAttribute
{
    // ...
}
```

## Remarks

Constructor to specify the key and lifetime

## Constructors

- [KeyedServiceRegistrationAttribute(object?, ServiceLifetime)](/Specular/pr-preview/pr-474/api/specular/keyedserviceregistrationattribute-2/constructors.md#constructor-object-servicelifetime) -- Attribute used to define the keyed service registration of a given type
