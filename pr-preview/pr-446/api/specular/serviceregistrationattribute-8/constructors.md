# ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8> Constructors

- Package: [Specular](/Specular/pr-preview/pr-446/api/specular.md)
- Type: [ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8>](/Specular/pr-preview/pr-446/api/specular/serviceregistrationattribute-8.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the service registration of a given type

<a id="constructor"></a>
<a id="constructor-servicelifetime"></a>

## ServiceRegistrationAttribute(ServiceLifetime)

- Name: `Constructor(ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-446-merge.Sha.bdccb36ec194143897302d3d96fd4fa575d90fa8/src/Specular/ServiceRegistrationAttribute.cs#L40)

Attribute used to define the service registration of a given type

```csharp
public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8>
{
    public ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7, ServiceRegistrationAttribute(ServiceLifetime lifetime)
    {
        // ...
    }
}
```

## Parameters

- `lifetime` (`ServiceLifetime`)

## Remarks

Constructor to specify the lifetime
