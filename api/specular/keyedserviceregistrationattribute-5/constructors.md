# KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5> Constructors

- Package: [Specular](/Specular/api/specular.md)
- Type: [KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>](/Specular/api/specular/keyedserviceregistrationattribute-5.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the keyed service registration of a given type

<a id="constructor"></a>
<a id="constructor-object-servicelifetime"></a>

## KeyedServiceRegistrationAttribute(object?, ServiceLifetime)

- Name: `Constructor(object?, ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.92ad0972fc551fb7fe4da29ef78029c66b11b489/src/Specular/KeyedServiceRegistrationAttribute.cs#L51)

Attribute used to define the keyed service registration of a given type

```csharp
public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>
{
    public KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, KeyedServiceRegistrationAttribute(object? key, ServiceLifetime lifetime)
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
