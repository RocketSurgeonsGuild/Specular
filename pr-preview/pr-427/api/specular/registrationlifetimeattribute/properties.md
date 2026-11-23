# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-427/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-427/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-427/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-427-merge.Sha.a4c159b423ec8329cb17d6f587fab98a92167670/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
