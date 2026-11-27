# ServiceRegistrationAttribute<TService1, TService2, TService3> Constructors

- Package: [Specular](/Specular/pr-preview/pr-464/api/specular.md)
- Type: [ServiceRegistrationAttribute<TService1, TService2, TService3>](/Specular/pr-preview/pr-464/api/specular/serviceregistrationattribute-3.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the service registration of a given type

<a id="constructor"></a>
<a id="constructor-servicelifetime"></a>

## ServiceRegistrationAttribute(ServiceLifetime)

- Name: `Constructor(ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-464-merge.Sha.1c51e62778e1813db280ec8727b59cd4afa8515f/src/Specular/ServiceRegistrationAttribute.cs#L40)

Attribute used to define the service registration of a given type

```csharp
public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3>
{
    public ServiceRegistrationAttribute<TService1, TService2, ServiceRegistrationAttribute(ServiceLifetime lifetime)
    {
        // ...
    }
}
```

## Parameters

- `lifetime` (`ServiceLifetime`)

## Remarks

Constructor to specify the lifetime
