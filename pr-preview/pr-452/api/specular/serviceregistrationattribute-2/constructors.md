# ServiceRegistrationAttribute<TService1, TService2> Constructors

- Package: [Specular](/Specular/pr-preview/pr-452/api/specular.md)
- Type: [ServiceRegistrationAttribute<TService1, TService2>](/Specular/pr-preview/pr-452/api/specular/serviceregistrationattribute-2.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the service registration of a given type

<a id="constructor"></a>
<a id="constructor-servicelifetime"></a>

## ServiceRegistrationAttribute(ServiceLifetime)

- Name: `Constructor(ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-452-merge.Sha.42edf2dec5a1bd56189429f7f6bf738a867fc895/src/Specular/ServiceRegistrationAttribute.cs#L40)

Attribute used to define the service registration of a given type

```csharp
public sealed class ServiceRegistrationAttribute<TService1, TService2>
{
    public ServiceRegistrationAttribute<TService1, ServiceRegistrationAttribute(ServiceLifetime lifetime)
    {
        // ...
    }
}
```

## Parameters

- `lifetime` (`ServiceLifetime`)

## Remarks

Constructor to specify the lifetime
