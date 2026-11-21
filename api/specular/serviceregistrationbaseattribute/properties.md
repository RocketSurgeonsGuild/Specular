# ServiceRegistrationBaseAttribute Properties

- Package: [Specular](/Specular/api/specular.md)
- Type: [ServiceRegistrationBaseAttribute](/Specular/api/specular/serviceregistrationbaseattribute.md)
- Kind: `Properties`
- Members: `2`

Attribute used to define the service registration of a given type

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.5e27d29a09b793f24c487188e4997683d2dde42b/src/Specular/ServiceRegistrationAttribute.cs#L26)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```

<a id="servicetypes"></a>

## ServiceTypes

- Name: `ServiceTypes`
- Modifiers: `get`
- Returns: `ImmutableArray<Type>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.5e27d29a09b793f24c487188e4997683d2dde42b/src/Specular/ServiceRegistrationAttribute.cs#L21)

The service type

```csharp
public ImmutableArray<Type> ServiceTypes { get; }
```
