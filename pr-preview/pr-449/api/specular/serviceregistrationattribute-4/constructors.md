# ServiceRegistrationAttribute<TService1, TService2, TService3, TService4> Constructors

- Package: [Specular](/Specular/pr-preview/pr-449/api/specular.md)
- Type: [ServiceRegistrationAttribute<TService1, TService2, TService3, TService4>](/Specular/pr-preview/pr-449/api/specular/serviceregistrationattribute-4.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the service registration of a given type

<a id="constructor"></a>
<a id="constructor-servicelifetime"></a>

## ServiceRegistrationAttribute(ServiceLifetime)

- Name: `Constructor(ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-449-merge.Sha.a4e7451225f525b22271723fffa01211c276f62e/src/Specular/ServiceRegistrationAttribute.cs#L40)

Attribute used to define the service registration of a given type

```csharp
public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4>
{
    public ServiceRegistrationAttribute<TService1, TService2, TService3, ServiceRegistrationAttribute(ServiceLifetime lifetime)
    {
        // ...
    }
}
```

## Parameters

- `lifetime` (`ServiceLifetime`)

## Remarks

Constructor to specify the lifetime
