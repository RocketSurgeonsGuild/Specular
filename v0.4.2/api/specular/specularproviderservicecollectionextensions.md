# SpecularProviderServiceCollectionExtensions

- Kind: `class`
- Package: [Specular](/Specular/v0.4.2/api/specular.md)
- Version: `0.4.2`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.cfc4e1e69ff7a6dcb9388579cde1a5879d0e3e7e/src/Specular/SpecularProviderServiceCollectionExtensions.cs)

Extension methods for the service collection using the compiled type provider

## Definition

```csharp
namespace Specular;

public static class SpecularProviderServiceCollectionExtensions
{
    // ...
}
```

## Methods

- [AddSpecularServiceRegistrations(IServiceCollection, ISpecularProvider)](/Specular/v0.4.2/api/specular/specularproviderservicecollectionextensions/methods.md#addspecularserviceregistrations-iservicecollection-ispecularprovider) : `IServiceCollection` `extension` -- Adds all the services with the [ServiceRegistrationAttribute](/Specular/v0.4.2/api/specular/serviceregistrationattribute.md) or [KeyedServiceRegistrationAttribute](/Specular/v0.4.2/api/specular/keyedserviceregistrationattribute.md) to the service collection
