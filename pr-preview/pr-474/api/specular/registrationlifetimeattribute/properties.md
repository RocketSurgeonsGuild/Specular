# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-474/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-474/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-474/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-474-merge.Sha.1aeada8e35256c38d58c0d80324e7d9dd7e6c8f3/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
