# SpecularProviderServiceCollectionExtensions

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-453/api/specular.md)
- Version: `0.4.3-pr.8`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-453-merge.Sha.ca2de83094300ad9fca8d4b18dd49afd62fc3ac1/src/Specular/SpecularProviderServiceCollectionExtensions.cs)

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

- [AddSpecularServiceRegistrations(IServiceCollection, ISpecularProvider)](/Specular/pr-preview/pr-453/api/specular/specularproviderservicecollectionextensions/methods.md#addspecularserviceregistrations-iservicecollection-ispecularprovider) : `IServiceCollection` `extension` -- Adds all the services with the [ServiceRegistrationAttribute](/Specular/pr-preview/pr-453/api/specular/serviceregistrationattribute.md) or [KeyedServiceRegistrationAttribute](/Specular/pr-preview/pr-453/api/specular/keyedserviceregistrationattribute.md) to the service collection
