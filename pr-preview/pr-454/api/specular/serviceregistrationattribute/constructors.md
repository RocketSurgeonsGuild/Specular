# ServiceRegistrationAttribute Constructors

- Package: [Specular](/Specular/pr-preview/pr-454/api/specular.md)
- Type: [ServiceRegistrationAttribute](/Specular/pr-preview/pr-454/api/specular/serviceregistrationattribute.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the service registration of a given type

<a id="constructor"></a>
<a id="constructor-servicelifetime-type"></a>

## ServiceRegistrationAttribute(ServiceLifetime, Type[])

- Name: `Constructor(ServiceLifetime, Type[])`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-454-merge.Sha.1c38d2f83278fdc4e7e43fc824e3f3246ce56659/src/Specular/ServiceRegistrationAttribute.cs#L40)

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
