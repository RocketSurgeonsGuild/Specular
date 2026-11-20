# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-446/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-446/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-446/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-446-merge.Sha.bdccb36ec194143897302d3d96fd4fa575d90fa8/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
