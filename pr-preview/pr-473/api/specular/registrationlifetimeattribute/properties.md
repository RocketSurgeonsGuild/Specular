# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-473/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-473/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-473/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-473-merge.Sha.13efb43d460e375dec527fd173757fe0c150e8d3/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
