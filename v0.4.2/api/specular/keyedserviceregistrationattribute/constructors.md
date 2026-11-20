# KeyedServiceRegistrationAttribute Constructors

- Package: [Specular](/Specular/v0.4.2/api/specular.md)
- Type: [KeyedServiceRegistrationAttribute](/Specular/v0.4.2/api/specular/keyedserviceregistrationattribute.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the keyed service registration of a given type. Behaves the same as [ServiceRegistrationAttribute](/Specular/v0.4.2/api/specular/serviceregistrationattribute.md) except the resulting registration is a [keyed service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services).

<a id="constructor"></a>
<a id="constructor-object-servicelifetime-type"></a>

## KeyedServiceRegistrationAttribute(object?, ServiceLifetime, Type[])

- Name: `Constructor(object?, ServiceLifetime, Type[])`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.cfc4e1e69ff7a6dcb9388579cde1a5879d0e3e7e/src/Specular/KeyedServiceRegistrationAttribute.cs#L51)

Attribute used to define the keyed service registration of a given type. Behaves the same as [ServiceRegistrationAttribute](/Specular/v0.4.2/api/specular/serviceregistrationattribute.md) except the resulting registration is a [keyed service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services).

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
