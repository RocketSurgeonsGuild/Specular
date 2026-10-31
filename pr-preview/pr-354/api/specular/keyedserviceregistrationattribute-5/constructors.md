# KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5> Constructors

- Package: [Specular](/Specular/pr-preview/pr-354/api/specular.md)
- Type: [KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>](/Specular/pr-preview/pr-354/api/specular/keyedserviceregistrationattribute-5.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the keyed service registration of a given type

<a id="constructor"></a>
<a id="constructor-object-servicelifetime"></a>

## KeyedServiceRegistrationAttribute(object?, ServiceLifetime)

- Name: `Constructor(object?, ServiceLifetime)`

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
