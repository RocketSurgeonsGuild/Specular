# ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6> Constructors

- Package: [Specular](/Specular/pr-preview/pr-453/api/specular.md)
- Type: [ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6>](/Specular/pr-preview/pr-453/api/specular/serviceregistrationattribute-6.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the service registration of a given type

<a id="constructor"></a>
<a id="constructor-servicelifetime"></a>

## ServiceRegistrationAttribute(ServiceLifetime)

- Name: `Constructor(ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-453-merge.Sha.ca2de83094300ad9fca8d4b18dd49afd62fc3ac1/src/Specular/ServiceRegistrationAttribute.cs#L40)

Attribute used to define the service registration of a given type

```csharp
public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6>
{
    public ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, ServiceRegistrationAttribute(ServiceLifetime lifetime)
    {
        // ...
    }
}
```

## Parameters

- `lifetime` (`ServiceLifetime`)

## Remarks

Constructor to specify the lifetime
