# ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5> Constructors

- Package: [Specular](/Specular/pr-preview/pr-447/api/specular.md)
- Type: [ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>](/Specular/pr-preview/pr-447/api/specular/serviceregistrationattribute-5.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the service registration of a given type

<a id="constructor"></a>
<a id="constructor-servicelifetime"></a>

## ServiceRegistrationAttribute(ServiceLifetime)

- Name: `Constructor(ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/src/Specular/ServiceRegistrationAttribute.cs#L40)

Attribute used to define the service registration of a given type

```csharp
public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>
{
    public ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, ServiceRegistrationAttribute(ServiceLifetime lifetime)
    {
        // ...
    }
}
```

## Parameters

- `lifetime` (`ServiceLifetime`)

## Remarks

Constructor to specify the lifetime
