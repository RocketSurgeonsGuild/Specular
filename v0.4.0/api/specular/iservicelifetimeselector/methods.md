# IServiceLifetimeSelector Methods

- Package: [Specular](/Specular/v0.4.0/api/specular.md)
- Type: [IServiceLifetimeSelector](/Specular/v0.4.0/api/specular/iservicelifetimeselector.md)
- Kind: `Methods`
- Members: `4`

The Compiled Lifetime Selector

<a id="withlifetime"></a>
<a id="withlifetime-servicelifetime"></a>

## WithLifetime(ServiceLifetime)

- Name: `WithLifetime(ServiceLifetime)`
- Modifiers: `abstract`

Registers each matching concrete type with the specified `lifetime`.

```csharp
public interface IServiceLifetimeSelector
{
    public abstract void WithLifetime(
        ServiceLifetime lifetime)
    {
        // ...
    }
}
```

## Parameters

- `lifetime` (`ServiceLifetime`)

<a id="withscopedlifetime"></a>

## WithScopedLifetime

- Name: `WithScopedLifetime`
- Modifiers: `abstract`

Registers each matching concrete type with `ServiceLifetime.Scoped` lifetime.

```csharp
public interface IServiceLifetimeSelector
{
    public abstract void WithScopedLifetime()
    {
        // ...
    }
}
```

<a id="withsingletonlifetime"></a>

## WithSingletonLifetime

- Name: `WithSingletonLifetime`
- Modifiers: `abstract`

Registers each matching concrete type with `ServiceLifetime.Singleton` lifetime.

```csharp
public interface IServiceLifetimeSelector
{
    public abstract void WithSingletonLifetime()
    {
        // ...
    }
}
```

<a id="withtransientlifetime"></a>

## WithTransientLifetime

- Name: `WithTransientLifetime`
- Modifiers: `abstract`

Registers each matching concrete type with `ServiceLifetime.Transient` lifetime.

```csharp
public interface IServiceLifetimeSelector
{
    public abstract void WithTransientLifetime()
    {
        // ...
    }
}
```
