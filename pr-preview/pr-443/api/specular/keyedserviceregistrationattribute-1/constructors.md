# KeyedServiceRegistrationAttribute<TService> Constructors

- Package: [Specular](/Specular/pr-preview/pr-443/api/specular.md)
- Type: [KeyedServiceRegistrationAttribute<TService>](/Specular/pr-preview/pr-443/api/specular/keyedserviceregistrationattribute-1.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the keyed service registration of a given type

<a id="constructor"></a>
<a id="constructor-object-servicelifetime"></a>

## KeyedServiceRegistrationAttribute(object?, ServiceLifetime)

- Name: `Constructor(object?, ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-443-merge.Sha.0033aac02b7565a1ecf99425ec7bdc404ad43560/src/Specular/KeyedServiceRegistrationAttribute.cs#L51)

Attribute used to define the keyed service registration of a given type

```csharp
public sealed class KeyedServiceRegistrationAttribute<TService>
{
    public KeyedServiceRegistrationAttribute(
        object? key,
        ServiceLifetime lifetime)
    {
        // ...
    }
}
```

## Parameters

- `key` (`object?`)
- `lifetime` (`ServiceLifetime`)

## Remarks

Constructor to specify the key and lifetime
