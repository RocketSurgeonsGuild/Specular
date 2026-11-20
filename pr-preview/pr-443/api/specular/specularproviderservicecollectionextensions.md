# SpecularProviderServiceCollectionExtensions

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-443/api/specular.md)
- Version: `0.4.2-pr.63`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-443-merge.Sha.0033aac02b7565a1ecf99425ec7bdc404ad43560/src/Specular/SpecularProviderServiceCollectionExtensions.cs)

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

- [AddSpecularServiceRegistrations(IServiceCollection, ISpecularProvider)](/Specular/pr-preview/pr-443/api/specular/specularproviderservicecollectionextensions/methods.md#addspecularserviceregistrations-iservicecollection-ispecularprovider) : `IServiceCollection` `extension` -- Adds all the services with the [ServiceRegistrationAttribute](/Specular/pr-preview/pr-443/api/specular/serviceregistrationattribute.md) or [KeyedServiceRegistrationAttribute](/Specular/pr-preview/pr-443/api/specular/keyedserviceregistrationattribute.md) to the service collection
