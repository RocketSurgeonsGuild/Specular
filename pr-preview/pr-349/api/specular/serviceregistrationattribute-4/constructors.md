# ServiceRegistrationAttribute<TService1, TService2, TService3, TService4> Constructors

- Package: [Specular](/Specular/pr-preview/pr-349/api/specular.md)
- Type: [ServiceRegistrationAttribute<TService1, TService2, TService3, TService4>](/Specular/pr-preview/pr-349/api/specular/serviceregistrationattribute-4.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the service registration of a given type

<a id="constructor"></a>
<a id="constructor-servicelifetime"></a>

## ServiceRegistrationAttribute(ServiceLifetime)

- Name: `Constructor(ServiceLifetime)`

Attribute used to define the service registration of a given type

```csharp
public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4>
{
    public ServiceRegistrationAttribute<TService1, TService2, TService3, ServiceRegistrationAttribute(ServiceLifetime lifetime)
    {
        // ...
    }
}
```

## Parameters

- `lifetime` (`ServiceLifetime`)

## Remarks

Constructor to specify the lifetime
