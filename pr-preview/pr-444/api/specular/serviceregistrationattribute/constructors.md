# ServiceRegistrationAttribute Constructors

- Package: [Specular](/Specular/pr-preview/pr-444/api/specular.md)
- Type: [ServiceRegistrationAttribute](/Specular/pr-preview/pr-444/api/specular/serviceregistrationattribute.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the service registration of a given type

<a id="constructor"></a>
<a id="constructor-servicelifetime-type"></a>

## ServiceRegistrationAttribute(ServiceLifetime, Type[])

- Name: `Constructor(ServiceLifetime, Type[])`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-444-merge.Sha.91db23f0a6dd2aea896fb5788d96dd2d2a023c25/src/Specular/ServiceRegistrationAttribute.cs#L40)

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
