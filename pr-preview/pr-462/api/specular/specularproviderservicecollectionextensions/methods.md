# SpecularProviderServiceCollectionExtensions Methods

- Package: [Specular](/Specular/pr-preview/pr-462/api/specular.md)
- Type: [SpecularProviderServiceCollectionExtensions](/Specular/pr-preview/pr-462/api/specular/specularproviderservicecollectionextensions.md)
- Kind: `Methods`
- Members: `1`

Extension methods for the service collection using the compiled type provider

<a id="addspecularserviceregistrations"></a>
<a id="addspecularserviceregistrations-iservicecollection-ispecularprovider"></a>

## AddSpecularServiceRegistrations(IServiceCollection, ISpecularProvider)

- Name: `AddSpecularServiceRegistrations(IServiceCollection, ISpecularProvider)`
- Modifiers: `extension`
- Returns: `IServiceCollection`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-462-merge.Sha.65577d9b63559d60a65f5887b78a6f971676b065/src/Specular/SpecularProviderServiceCollectionExtensions.cs#L20-L49)

Adds all the services with the [ServiceRegistrationAttribute](/Specular/pr-preview/pr-462/api/specular/serviceregistrationattribute.md) or [KeyedServiceRegistrationAttribute](/Specular/pr-preview/pr-462/api/specular/keyedserviceregistrationattribute.md) to the service collection

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
- `provider` ([ISpecularProvider](/Specular/pr-preview/pr-462/api/specular/ispecularprovider.md))
