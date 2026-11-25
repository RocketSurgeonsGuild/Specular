# ServiceRegistrationBaseAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-458/api/specular.md)
- Type: [ServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-458/api/specular/serviceregistrationbaseattribute.md)
- Kind: `Properties`
- Members: `2`

Attribute used to define the service registration of a given type

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-458-merge.Sha.f398584acaebb436a7dc6af0a2c4966988fca3fe/src/Specular/ServiceRegistrationAttribute.cs#L26)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```

<a id="servicetypes"></a>

## ServiceTypes

- Name: `ServiceTypes`
- Modifiers: `get`
- Returns: `ImmutableArray<Type>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-458-merge.Sha.f398584acaebb436a7dc6af0a2c4966988fca3fe/src/Specular/ServiceRegistrationAttribute.cs#L21)

The service type

```csharp
public ImmutableArray<Type> ServiceTypes { get; }
```
