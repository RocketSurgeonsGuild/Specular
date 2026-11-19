# SpecularProviderServiceCollectionExtensions Methods

- Package: [Specular](/Specular/api/specular.md)
- Type: [SpecularProviderServiceCollectionExtensions](/Specular/api/specular/specularproviderservicecollectionextensions.md)
- Kind: `Methods`
- Members: `1`

Extension methods for the service collection using the compiled type provider

<a id="addspecularserviceregistrations"></a>
<a id="addspecularserviceregistrations-iservicecollection-ispecularprovider"></a>

## AddSpecularServiceRegistrations(IServiceCollection, ISpecularProvider)

- Name: `AddSpecularServiceRegistrations(IServiceCollection, ISpecularProvider)`
- Modifiers: `extension`
- Returns: `IServiceCollection`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.8bcc62d19c09284f6923e54e588877e41baf4070/src/Specular/SpecularProviderServiceCollectionExtensions.cs#L20-L49)

Adds all the services with the [ServiceRegistrationAttribute](/Specular/api/specular/serviceregistrationattribute.md) or [KeyedServiceRegistrationAttribute](/Specular/api/specular/keyedserviceregistrationattribute.md) to the service collection

```csharp
public static class SpecularProviderServiceCollectionExtensions
{
    public static IServiceCollection AddSpecularServiceRegistrations(
        this IServiceCollection services,
        ISpecularProvider provider)
    {
        // ...
    }
}
```

## Parameters

- `services` (`IServiceCollection`)
- `provider` ([ISpecularProvider](/Specular/api/specular/ispecularprovider.md))
