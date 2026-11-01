# ServiceRegistrationAttribute<TService> Constructors

- Package: [Specular](/Specular/pr-preview/pr-355/api/specular.md)
- Type: [ServiceRegistrationAttribute<TService>](/Specular/pr-preview/pr-355/api/specular/serviceregistrationattribute-1.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the service registration of a given type

<a id="constructor"></a>
<a id="constructor-servicelifetime"></a>

## ServiceRegistrationAttribute(ServiceLifetime)

- Name: `Constructor(ServiceLifetime)`

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
