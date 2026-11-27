# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-472/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-472/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-472/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-472-merge.Sha.a46222835e40014b71e6fa98a0c0700389a121c5/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
