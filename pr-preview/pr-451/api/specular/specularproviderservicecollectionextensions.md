# SpecularProviderServiceCollectionExtensions

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-451/api/specular.md)
- Version: `0.4.3-pr.3`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-451-merge.Sha.3976afc911b40cb4a244b415afe8ec88dc2bcb05/src/Specular/SpecularProviderServiceCollectionExtensions.cs)

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

- [AddSpecularServiceRegistrations(IServiceCollection, ISpecularProvider)](/Specular/pr-preview/pr-451/api/specular/specularproviderservicecollectionextensions/methods.md#addspecularserviceregistrations-iservicecollection-ispecularprovider) : `IServiceCollection` `extension` -- Adds all the services with the [ServiceRegistrationAttribute](/Specular/pr-preview/pr-451/api/specular/serviceregistrationattribute.md) or [KeyedServiceRegistrationAttribute](/Specular/pr-preview/pr-451/api/specular/keyedserviceregistrationattribute.md) to the service collection
