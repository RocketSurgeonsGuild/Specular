# KeyedServiceRegistrationBaseAttribute Properties

- Package: [Specular](/Specular/api/specular.md)
- Type: [KeyedServiceRegistrationBaseAttribute](/Specular/api/specular/keyedserviceregistrationbaseattribute.md)
- Kind: `Properties`
- Members: `3`

Attribute used to define the keyed service registration of a given type. Behaves the same as [ServiceRegistrationAttribute](/Specular/api/specular/serviceregistrationattribute.md) except the resulting registration is a [keyed service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services).

<a id="key"></a>

## Key

- Name: `Key`
- Modifiers: `nullable` `get`
- Returns: `object?`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.69eea945f0109ae65f9e3b8083ce5b388859c35a/src/Specular/KeyedServiceRegistrationAttribute.cs#L24)

The key the service is registered against

```csharp
public object? Key { get; }
```

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.69eea945f0109ae65f9e3b8083ce5b388859c35a/src/Specular/KeyedServiceRegistrationAttribute.cs#L34)

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```

<a id="servicetypes"></a>

## ServiceTypes

- Name: `ServiceTypes`
- Modifiers: `get`
- Returns: `ImmutableArray<Type>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.69eea945f0109ae65f9e3b8083ce5b388859c35a/src/Specular/KeyedServiceRegistrationAttribute.cs#L29)

The service type

```csharp
public ImmutableArray<Type> ServiceTypes { get; }
```
