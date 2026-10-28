# SpecularProviderServiceCollectionExtensions

- Kind: `class`
- Package: [Specular](/Specular/v0.3.2/api/specular.md)
- Version: `0.3.2`
- Namespace: `Specular`
- Target framework: `net10.0`

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

- [AddSpecularServiceRegistrations(IServiceCollection, ISpecularProvider)](/Specular/v0.3.2/api/specular/specularproviderservicecollectionextensions/methods.md#addspecularserviceregistrations-iservicecollection-ispecularprovider) : `IServiceCollection` `extension` -- Adds all the services with the [ServiceRegistrationAttribute](/Specular/v0.3.2/api/specular/serviceregistrationattribute.md) or [KeyedServiceRegistrationAttribute](/Specular/v0.3.2/api/specular/keyedserviceregistrationattribute.md) to the service collection
