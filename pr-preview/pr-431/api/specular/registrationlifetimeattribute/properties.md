# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-431/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-431/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-431/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-431-merge.Sha.1fc516ea1b9500fed12a71dad9d2c95bf1783a47/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
