# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-471/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-471/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-471/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-471-merge.Sha.67f277419c23bfca099500012ad2d0da9bede75d/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
