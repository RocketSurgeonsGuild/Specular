# KeyedServiceRegistrationAttribute Constructors

- Package: [Specular](/Specular/pr-preview/pr-350/api/specular.md)
- Type: [KeyedServiceRegistrationAttribute](/Specular/pr-preview/pr-350/api/specular/keyedserviceregistrationattribute.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the keyed service registration of a given type. Behaves the same as [ServiceRegistrationAttribute](/Specular/pr-preview/pr-350/api/specular/serviceregistrationattribute.md) except the resulting registration is a [keyed service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services).

<a id="constructor"></a>
<a id="constructor-object-servicelifetime-type"></a>

## KeyedServiceRegistrationAttribute(object?, ServiceLifetime, Type[])

- Name: `Constructor(object?, ServiceLifetime, Type[])`

Attribute used to define the keyed service registration of a given type. Behaves the same as [ServiceRegistrationAttribute](/Specular/pr-preview/pr-350/api/specular/serviceregistrationattribute.md) except the resulting registration is a [keyed service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services).

```csharp
public sealed class KeyedServiceRegistrationAttribute
{
    public KeyedServiceRegistrationAttribute(
        object? key,
        ServiceLifetime lifetime,
        params Type[] serviceTypes)
    {
        // ...
    }
}
```

## Parameters

- `key` (`object?`)
- `lifetime` (`ServiceLifetime`)
- `serviceTypes` (`Type[]`)

## Remarks

Constructor to specify the key, service type including optional runtime
