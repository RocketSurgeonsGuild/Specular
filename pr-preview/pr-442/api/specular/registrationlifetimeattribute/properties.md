# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-442/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-442/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-442/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-442-merge.Sha.dca6b2e19ff475e66bd255b81efe7b8c43df4286/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
