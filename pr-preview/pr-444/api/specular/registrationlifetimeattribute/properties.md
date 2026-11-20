# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-444/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-444/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-444/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-444-merge.Sha.91db23f0a6dd2aea896fb5788d96dd2d2a023c25/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
