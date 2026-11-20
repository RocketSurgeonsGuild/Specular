# KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7> Constructors

- Package: [Specular](/Specular/pr-preview/pr-446/api/specular.md)
- Type: [KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7>](/Specular/pr-preview/pr-446/api/specular/keyedserviceregistrationattribute-7.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the keyed service registration of a given type

<a id="constructor"></a>
<a id="constructor-object-servicelifetime"></a>

## KeyedServiceRegistrationAttribute(object?, ServiceLifetime)

- Name: `Constructor(object?, ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-446-merge.Sha.bdccb36ec194143897302d3d96fd4fa575d90fa8/src/Specular/KeyedServiceRegistrationAttribute.cs#L51)

Attribute used to define the keyed service registration of a given type

```csharp
public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7>
{
    public KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, KeyedServiceRegistrationAttribute(object? key, ServiceLifetime lifetime)
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
