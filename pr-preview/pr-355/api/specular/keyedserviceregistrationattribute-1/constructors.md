# KeyedServiceRegistrationAttribute<TService> Constructors

- Package: [Specular](/Specular/pr-preview/pr-355/api/specular.md)
- Type: [KeyedServiceRegistrationAttribute<TService>](/Specular/pr-preview/pr-355/api/specular/keyedserviceregistrationattribute-1.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the keyed service registration of a given type

<a id="constructor"></a>
<a id="constructor-object-servicelifetime"></a>

## KeyedServiceRegistrationAttribute(object?, ServiceLifetime)

- Name: `Constructor(object?, ServiceLifetime)`

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
