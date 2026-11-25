# KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6> Constructors

- Package: [Specular](/Specular/pr-preview/pr-459/api/specular.md)
- Type: [KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6>](/Specular/pr-preview/pr-459/api/specular/keyedserviceregistrationattribute-6.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the keyed service registration of a given type

<a id="constructor"></a>
<a id="constructor-object-servicelifetime"></a>

## KeyedServiceRegistrationAttribute(object?, ServiceLifetime)

- Name: `Constructor(object?, ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-459-merge.Sha.ca8deea26180382e1d58a2391f0e4bcbcc0b2c9d/src/Specular/KeyedServiceRegistrationAttribute.cs#L51)

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
