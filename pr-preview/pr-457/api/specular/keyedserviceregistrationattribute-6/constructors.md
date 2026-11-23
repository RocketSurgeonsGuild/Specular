# KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6> Constructors

- Package: [Specular](/Specular/pr-preview/pr-457/api/specular.md)
- Type: [KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6>](/Specular/pr-preview/pr-457/api/specular/keyedserviceregistrationattribute-6.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the keyed service registration of a given type

<a id="constructor"></a>
<a id="constructor-object-servicelifetime"></a>

## KeyedServiceRegistrationAttribute(object?, ServiceLifetime)

- Name: `Constructor(object?, ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-457-merge.Sha.63ef56f819f6729dc27767f4d81e41dd73901dc3/src/Specular/KeyedServiceRegistrationAttribute.cs#L51)

Attribute used to define the keyed service registration of a given type

```csharp
public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6>
{
    public KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, KeyedServiceRegistrationAttribute(object? key, ServiceLifetime lifetime)
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
