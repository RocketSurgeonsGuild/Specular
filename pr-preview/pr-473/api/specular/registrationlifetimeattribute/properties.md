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
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-473-merge.Sha.49ec794ba3b68aa9d860d0cf1fc1fc04dd37a414/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
