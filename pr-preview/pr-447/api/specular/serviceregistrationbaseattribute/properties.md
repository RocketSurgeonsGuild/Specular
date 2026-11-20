# ServiceRegistrationBaseAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-447/api/specular.md)
- Type: [ServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-447/api/specular/serviceregistrationbaseattribute.md)
- Kind: `Properties`
- Members: `2`

Attribute used to define the service registration of a given type

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/src/Specular/ServiceRegistrationAttribute.cs#L26)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```

<a id="servicetypes"></a>

## ServiceTypes

- Name: `ServiceTypes`
- Modifiers: `get`
- Returns: `ImmutableArray<Type>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/src/Specular/ServiceRegistrationAttribute.cs#L21)

The service type

```csharp
public ImmutableArray<Type> ServiceTypes { get; }
```
