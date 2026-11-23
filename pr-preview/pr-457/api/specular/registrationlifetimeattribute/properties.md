# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-457/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-457/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-457/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-457-merge.Sha.63ef56f819f6729dc27767f4d81e41dd73901dc3/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
