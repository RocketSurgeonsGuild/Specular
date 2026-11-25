# SpecularProviderServiceCollectionExtensions Methods

- Package: [Specular](/Specular/pr-preview/pr-458/api/specular.md)
- Type: [SpecularProviderServiceCollectionExtensions](/Specular/pr-preview/pr-458/api/specular/specularproviderservicecollectionextensions.md)
- Kind: `Methods`
- Members: `1`

Extension methods for the service collection using the compiled type provider

<a id="addspecularserviceregistrations"></a>
<a id="addspecularserviceregistrations-iservicecollection-ispecularprovider"></a>

## AddSpecularServiceRegistrations(IServiceCollection, ISpecularProvider)

- Name: `AddSpecularServiceRegistrations(IServiceCollection, ISpecularProvider)`
- Modifiers: `extension`
- Returns: `IServiceCollection`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-458-merge.Sha.0fc2d3763ad1af52ac3c220ba5c57d4f9a9198d7/src/Specular/SpecularProviderServiceCollectionExtensions.cs#L20-L49)

Adds all the services with the [ServiceRegistrationAttribute](/Specular/pr-preview/pr-458/api/specular/serviceregistrationattribute.md) or [KeyedServiceRegistrationAttribute](/Specular/pr-preview/pr-458/api/specular/keyedserviceregistrationattribute.md) to the service collection

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
- `provider` ([ISpecularProvider](/Specular/pr-preview/pr-458/api/specular/ispecularprovider.md))
