# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-464/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-464/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-464/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-464-merge.Sha.1c51e62778e1813db280ec8727b59cd4afa8515f/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
