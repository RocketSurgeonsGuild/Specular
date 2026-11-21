# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-449/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-449/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-449/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-449-merge.Sha.a4e7451225f525b22271723fffa01211c276f62e/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
