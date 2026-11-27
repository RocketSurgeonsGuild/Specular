# ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7> Constructors

- Package: [Specular](/Specular/api/specular.md)
- Type: [ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7>](/Specular/api/specular/serviceregistrationattribute-7.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the service registration of a given type

<a id="constructor"></a>
<a id="constructor-servicelifetime"></a>

## ServiceRegistrationAttribute(ServiceLifetime)

- Name: `Constructor(ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.fa706afb7f53c34a576fc835a76c7a7d2a9c6bae/src/Specular/ServiceRegistrationAttribute.cs#L40)

Attribute used to define the service registration of a given type

```csharp
public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7>
{
    public ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, ServiceRegistrationAttribute(ServiceLifetime lifetime)
    {
        // ...
    }
}
```

## Parameters

- `lifetime` (`ServiceLifetime`)

## Remarks

Constructor to specify the lifetime
