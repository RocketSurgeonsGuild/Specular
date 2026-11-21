# KeyedServiceRegistrationAttribute<TService>

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-451/api/specular.md)
- Version: `0.4.3-pr.3`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-451-merge.Sha.3976afc911b40cb4a244b415afe8ec88dc2bcb05/src/Specular/KeyedServiceRegistrationAttribute.cs)
- Inherits: [KeyedServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-451/api/specular/keyedserviceregistrationbaseattribute.md)

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

- [KeyedServiceRegistrationAttribute(object?, ServiceLifetime)](/Specular/pr-preview/pr-451/api/specular/keyedserviceregistrationattribute-1/constructors.md#constructor-object-servicelifetime) -- Attribute used to define the keyed service registration of a given type
