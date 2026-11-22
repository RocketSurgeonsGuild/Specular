# RegistrationLifetimeAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-455/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-455/api/specular/registrationlifetimeattribute.md)
- Kind: `Properties`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-455/api/specular/ispecularprovider.md)

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-455-merge.Sha.ad40fe1677bfb4ad95a1053b46feddf65f718b76/src/Specular/RegistrationLifetimeAttribute.cs#L20)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```
