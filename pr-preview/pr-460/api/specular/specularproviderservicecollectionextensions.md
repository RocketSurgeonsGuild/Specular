# SpecularProviderServiceCollectionExtensions

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-460/api/specular.md)
- Version: `0.4.3-pr.13`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-460-merge.Sha.d41c5fa5888078044d4dab2d9f0a772bbcacc924/src/Specular/SpecularProviderServiceCollectionExtensions.cs)

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

- [AddSpecularServiceRegistrations(IServiceCollection, ISpecularProvider)](/Specular/pr-preview/pr-460/api/specular/specularproviderservicecollectionextensions/methods.md#addspecularserviceregistrations-iservicecollection-ispecularprovider) : `IServiceCollection` `extension` -- Adds all the services with the [ServiceRegistrationAttribute](/Specular/pr-preview/pr-460/api/specular/serviceregistrationattribute.md) or [KeyedServiceRegistrationAttribute](/Specular/pr-preview/pr-460/api/specular/keyedserviceregistrationattribute.md) to the service collection
