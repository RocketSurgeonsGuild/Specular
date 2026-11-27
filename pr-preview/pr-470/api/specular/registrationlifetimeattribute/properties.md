# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-470/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-470/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-470/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-470-merge.Sha.9e8231dd8943f7f4db4653a342095fa749fa9265/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
