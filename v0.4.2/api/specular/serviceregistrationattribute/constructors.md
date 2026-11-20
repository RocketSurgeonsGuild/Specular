# ServiceRegistrationAttribute Constructors

- Package: [Specular](/Specular/v0.4.2/api/specular.md)
- Type: [ServiceRegistrationAttribute](/Specular/v0.4.2/api/specular/serviceregistrationattribute.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the service registration of a given type

<a id="constructor"></a>
<a id="constructor-servicelifetime-type"></a>

## ServiceRegistrationAttribute(ServiceLifetime, Type[])

- Name: `Constructor(ServiceLifetime, Type[])`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.cfc4e1e69ff7a6dcb9388579cde1a5879d0e3e7e/src/Specular/ServiceRegistrationAttribute.cs#L40)

Attribute used to define the service registration of a given type

```csharp
public sealed class ServiceRegistrationAttribute
{
    public ServiceRegistrationAttribute(
        ServiceLifetime lifetime,
        params Type[] serviceTypes)
    {
        // ...
    }
}
```

## Parameters

- `lifetime` (`ServiceLifetime`)
- `serviceTypes` (`Type[]`)

## Remarks

Constructor to specify the service type including optional runtime
