# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-454/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-454/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-454/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-454-merge.Sha.1c38d2f83278fdc4e7e43fc824e3f3246ce56659/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
