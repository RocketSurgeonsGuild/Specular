# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-439/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-439/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-439/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-439-merge.Sha.a00a386a02665437a1eaba7e40471f7bf42b5822/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
