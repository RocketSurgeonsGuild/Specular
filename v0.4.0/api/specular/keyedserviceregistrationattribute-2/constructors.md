# KeyedServiceRegistrationAttribute<TService1, TService2> Constructors

- Package: [Specular](/Specular/v0.4.0/api/specular.md)
- Type: [KeyedServiceRegistrationAttribute<TService1, TService2>](/Specular/v0.4.0/api/specular/keyedserviceregistrationattribute-2.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the keyed service registration of a given type

<a id="constructor"></a>
<a id="constructor-object-servicelifetime"></a>

## KeyedServiceRegistrationAttribute(object?, ServiceLifetime)

- Name: `Constructor(object?, ServiceLifetime)`

Attribute used to define the keyed service registration of a given type

```csharp
public sealed class KeyedServiceRegistrationAttribute<TService1, TService2>
{
    public KeyedServiceRegistrationAttribute<TService1, KeyedServiceRegistrationAttribute(object? key, ServiceLifetime lifetime)
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
