# KeyedServiceRegistrationAttribute<TService>

- Kind: `class`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.3-beta.1`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.54f1524cb3efb28751ffc9cedb4492113f164068/src/Specular/KeyedServiceRegistrationAttribute.cs)
- Inherits: [KeyedServiceRegistrationBaseAttribute](/Specular/api/specular/keyedserviceregistrationbaseattribute.md)

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

- [KeyedServiceRegistrationAttribute(object?, ServiceLifetime)](/Specular/api/specular/keyedserviceregistrationattribute-1/constructors.md#constructor-object-servicelifetime) -- Attribute used to define the keyed service registration of a given type
