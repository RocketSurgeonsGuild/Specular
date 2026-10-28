# IServiceTypeSelector Methods

- Package: [Specular](/Specular/api/specular.md)
- Type: [IServiceTypeSelector](/Specular/api/specular/iservicetypeselector.md)
- Kind: `Methods`
- Members: `8`

The Compiled Service Type Selector

<a id="as"></a>

## As

- Name: `As`
- Modifiers: `abstract`
- Returns: [IServiceLifetimeSelector](/Specular/api/specular/iservicelifetimeselector.md)

Registers each matching concrete type as `T`.

```csharp
public interface IServiceTypeSelector
{
    public abstract IServiceLifetimeSelector As<T>()
    {
        // ...
    }
}
```

<a id="as-type"></a>

## As(Type)

- Name: `As(Type)`
- Modifiers: `abstract`
- Returns: [IServiceLifetimeSelector](/Specular/api/specular/iservicelifetimeselector.md)

Registers each matching concrete type as type

```csharp
public interface IServiceTypeSelector
{
    public abstract IServiceLifetimeSelector As(
        Type type)
    {
        // ...
    }
}
```

## Parameters

- `type` (`Type`)

<a id="asimplementedinterfaces"></a>

## AsImplementedInterfaces

- Name: `AsImplementedInterfaces`
- Modifiers: `abstract`
- Returns: [IServiceLifetimeSelector](/Specular/api/specular/iservicelifetimeselector.md)

Registers each matching concrete type as all of its implemented interfaces.

```csharp
public interface IServiceTypeSelector
{
    public abstract IServiceLifetimeSelector AsImplementedInterfaces()
    {
        // ...
    }
}
```

<a id="asimplementedinterfaces-action-itypefilter"></a>

## AsImplementedInterfaces(Action<ITypeFilter>)

- Name: `AsImplementedInterfaces(Action<ITypeFilter>)`
- Modifiers: `abstract`
- Returns: [IServiceLifetimeSelector](/Specular/api/specular/iservicelifetimeselector.md)

Registers each matching concrete type as all of its implemented interfaces.

```csharp
public interface IServiceTypeSelector
{
    public abstract IServiceLifetimeSelector AsImplementedInterfaces(
        Action<ITypeFilter> action)
    {
        // ...
    }
}
```

## Parameters

- `action` (`Action<ITypeFilter>`)

<a id="asmatchinginterface"></a>

## AsMatchingInterface

- Name: `AsMatchingInterface`
- Modifiers: `abstract`
- Returns: [IServiceLifetimeSelector](/Specular/api/specular/iservicelifetimeselector.md)

Registers the type with the first found matching interface name. (e.g. ClassName is matched to IClassName)

```csharp
public interface IServiceTypeSelector
{
    public abstract IServiceLifetimeSelector AsMatchingInterface()
    {
        // ...
    }
}
```

<a id="asself"></a>

## AsSelf

- Name: `AsSelf`
- Modifiers: `abstract`
- Returns: [IServiceLifetimeSelector](/Specular/api/specular/iservicelifetimeselector.md)

Registers each matching concrete type as itself.

```csharp
public interface IServiceTypeSelector
{
    public abstract IServiceLifetimeSelector AsSelf()
    {
        // ...
    }
}
```

<a id="asselfwithinterfaces"></a>

## AsSelfWithInterfaces

- Name: `AsSelfWithInterfaces`
- Modifiers: `abstract`
- Returns: [IServiceLifetimeSelector](/Specular/api/specular/iservicelifetimeselector.md)

Registers each matching concrete type as all of its implemented interfaces, by returning an instance of the main type

```csharp
public interface IServiceTypeSelector
{
    public abstract IServiceLifetimeSelector AsSelfWithInterfaces()
    {
        // ...
    }
}
```

<a id="asselfwithinterfaces-action-itypefilter"></a>

## AsSelfWithInterfaces(Action<ITypeFilter>)

- Name: `AsSelfWithInterfaces(Action<ITypeFilter>)`
- Modifiers: `abstract`
- Returns: [IServiceLifetimeSelector](/Specular/api/specular/iservicelifetimeselector.md)

Registers each matching concrete type as all of its implemented interfaces, by returning an instance of the main type

```csharp
public interface IServiceTypeSelector
{
    public abstract IServiceLifetimeSelector AsSelfWithInterfaces(
        Action<ITypeFilter> action)
    {
        // ...
    }
}
```

## Parameters

- `action` (`Action<ITypeFilter>`)
