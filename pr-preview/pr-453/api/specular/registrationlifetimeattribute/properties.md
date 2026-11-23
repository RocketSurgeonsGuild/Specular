# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-453/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-453/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-453/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-453-merge.Sha.507a376b7125f9abab9ee28903b20cf32ac8ece3/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
