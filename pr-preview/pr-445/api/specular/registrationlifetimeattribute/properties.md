# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-445/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-445/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-445/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-445-merge.Sha.148444cdd6691571fa2f9e5421c54e07d5203dfc/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
