# KeyedServiceRegistrationAttribute<TService1, TService2, TService3> Constructors

- Package: [Specular](/Specular/pr-preview/pr-439/api/specular.md)
- Type: [KeyedServiceRegistrationAttribute<TService1, TService2, TService3>](/Specular/pr-preview/pr-439/api/specular/keyedserviceregistrationattribute-3.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the keyed service registration of a given type

<a id="constructor"></a>
<a id="constructor-object-servicelifetime"></a>

## KeyedServiceRegistrationAttribute(object?, ServiceLifetime)

- Name: `Constructor(object?, ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-439-merge.Sha.a00a386a02665437a1eaba7e40471f7bf42b5822/src/Specular/KeyedServiceRegistrationAttribute.cs#L51)

Attribute used to define the keyed service registration of a given type

```csharp
public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3>
{
    public KeyedServiceRegistrationAttribute<TService1, TService2, KeyedServiceRegistrationAttribute(object? key, ServiceLifetime lifetime)
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
