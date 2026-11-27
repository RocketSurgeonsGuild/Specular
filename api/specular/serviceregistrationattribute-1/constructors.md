# ServiceRegistrationAttribute<TService> Constructors

- Package: [Specular](/Specular/api/specular.md)
- Type: [ServiceRegistrationAttribute<TService>](/Specular/api/specular/serviceregistrationattribute-1.md)
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
public sealed class ServiceRegistrationAttribute<TService>
{
    public ServiceRegistrationAttribute(
        ServiceLifetime lifetime)
    {
        // ...
    }
}
```

## Parameters

- `lifetime` (`ServiceLifetime`)

## Remarks

Constructor to specify the lifetime
