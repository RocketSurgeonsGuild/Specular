# KeyedServiceRegistrationBaseAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-355/api/specular.md)
- Type: [KeyedServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-355/api/specular/keyedserviceregistrationbaseattribute.md)
- Kind: `Properties`
- Members: `3`

Attribute used to define the keyed service registration of a given type. Behaves the same as [ServiceRegistrationAttribute](/Specular/pr-preview/pr-355/api/specular/serviceregistrationattribute.md) except the resulting registration is a [keyed service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services).

<a id="key"></a>

## Key

- Name: `Key`
- Modifiers: `nullable` `get`
- Returns: `object?`

The key the service is registered against

```csharp
public object? Key { get; }
```

<a id="lifetime"></a>

## Lifetime

- Name: `Lifetime`
- Modifiers: `get`
- Returns: `ServiceLifetime`

The lifetime

```csharp
public ServiceLifetime Lifetime { get; }
```

<a id="servicetypes"></a>

## ServiceTypes

- Name: `ServiceTypes`
- Modifiers: `get`
- Returns: `ImmutableArray<Type>`

The service type

```csharp
public ImmutableArray<Type> ServiceTypes { get; }
```
