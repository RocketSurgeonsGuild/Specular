# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-451/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-451/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-451/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-451-merge.Sha.3976afc911b40cb4a244b415afe8ec88dc2bcb05/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
