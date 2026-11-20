# ServiceRegistrationAttribute<TService> Constructors

- Package: [Specular](/Specular/pr-preview/pr-444/api/specular.md)
- Type: [ServiceRegistrationAttribute<TService>](/Specular/pr-preview/pr-444/api/specular/serviceregistrationattribute-1.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the service registration of a given type

<a id="constructor"></a>
<a id="constructor-servicelifetime"></a>

## ServiceRegistrationAttribute(ServiceLifetime)

- Name: `Constructor(ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-444-merge.Sha.91db23f0a6dd2aea896fb5788d96dd2d2a023c25/src/Specular/ServiceRegistrationAttribute.cs#L40)

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
