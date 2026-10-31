# IReflectionTypeSelector Methods

- Package: [Specular](/Specular/pr-preview/pr-353/api/specular.md)
- Type: [IReflectionTypeSelector](/Specular/pr-preview/pr-353/api/specular/ireflectiontypeselector.md)
- Kind: `Methods`
- Members: `4`

The Compiled Implementation Type Selector

<a id="gettypes"></a>

## GetTypes

- Name: `GetTypes`
- Modifiers: `abstract`
- Returns: `IEnumerable<Type>`

Lists all of the classes in a given assembly

```csharp
public interface IReflectionTypeSelector
{
    public abstract IEnumerable<Type> GetTypes()
    {
        // ...
    }
}
```

<a id="gettypes-bool"></a>

## GetTypes(bool)

- Name: `GetTypes(bool)`
- Modifiers: `abstract`
- Returns: `IEnumerable<Type>`

Lists all of the public classes in a given assembly

```csharp
public interface IReflectionTypeSelector
{
    public abstract IEnumerable<Type> GetTypes(
        bool publicOnly)
    {
        // ...
    }
}
```

## Parameters

- `publicOnly` (`bool`)
  Specifies whether too add public types only.

<a id="gettypes-action-itypefilter"></a>

## GetTypes(Action<ITypeFilter>)

- Name: `GetTypes(Action<ITypeFilter>)`
- Modifiers: `abstract`
- Returns: `IEnumerable<Type>`

Adds all public, non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.

```csharp
public interface IReflectionTypeSelector
{
    public abstract IEnumerable<Type> GetTypes(
        Action<ITypeFilter> action)
    {
        // ...
    }
}
```

## Parameters

- `action` (`Action<ITypeFilter>`)
  The filtering action.

<a id="gettypes-bool-action-itypefilter"></a>

## GetTypes(bool, Action<ITypeFilter>)

- Name: `GetTypes(bool, Action<ITypeFilter>)`
- Modifiers: `abstract`
- Returns: `IEnumerable<Type>`

Adds all non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.

```csharp
public interface IReflectionTypeSelector
{
    public abstract IEnumerable<Type> GetTypes(
        bool publicOnly,
        Action<ITypeFilter> action)
    {
        // ...
    }
}
```

## Parameters

- `publicOnly` (`bool`)
  Specifies whether too add public types only.
- `action` (`Action<ITypeFilter>`)
  The filtering action.
