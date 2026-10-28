# IServiceDescriptorTypeSelector Methods

- Package: [Specular](/Specular/pr-preview/pr-341/api/specular.md)
- Type: [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-341/api/specular/iservicedescriptortypeselector.md)
- Kind: `Methods`
- Members: `4`

The Compiled Implementation Type Selector

<a id="addclasses"></a>

## AddClasses

- Name: `AddClasses`
- Modifiers: `abstract`
- Returns: [IServiceTypeSelector](/Specular/pr-preview/pr-341/api/specular/iservicetypeselector.md)

Adds all public, non-abstract classes from the selected assemblies to the `DependencyInjection.IServiceCollection`.

```csharp
public interface IServiceDescriptorTypeSelector
{
    public abstract IServiceTypeSelector AddClasses()
    {
        // ...
    }
}
```

<a id="addclasses-bool"></a>

## AddClasses(bool)

- Name: `AddClasses(bool)`
- Modifiers: `abstract`
- Returns: [IServiceTypeSelector](/Specular/pr-preview/pr-341/api/specular/iservicetypeselector.md)

Adds all non-abstract classes from the selected assemblies to the `DependencyInjection.IServiceCollection`.

```csharp
public interface IServiceDescriptorTypeSelector
{
    public abstract IServiceTypeSelector AddClasses(
        bool publicOnly)
    {
        // ...
    }
}
```

## Parameters

- `publicOnly` (`bool`)
  Specifies whether too add public types only.

<a id="addclasses-action-itypefilter"></a>

## AddClasses(Action<ITypeFilter>)

- Name: `AddClasses(Action<ITypeFilter>)`
- Modifiers: `abstract`
- Returns: [IServiceTypeSelector](/Specular/pr-preview/pr-341/api/specular/iservicetypeselector.md)

Adds all public, non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.

```csharp
public interface IServiceDescriptorTypeSelector
{
    public abstract IServiceTypeSelector AddClasses(
        Action<ITypeFilter> action)
    {
        // ...
    }
}
```

## Parameters

- `action` (`Action<ITypeFilter>`)
  The filtering action.

## Exceptions

- `ArgumentNullException` -- If the `action` argument is `null`.

<a id="addclasses-action-itypefilter-bool"></a>

## AddClasses(Action<ITypeFilter>, bool)

- Name: `AddClasses(Action<ITypeFilter>, bool)`
- Modifiers: `abstract`
- Returns: [IServiceTypeSelector](/Specular/pr-preview/pr-341/api/specular/iservicetypeselector.md)

Adds all non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.

```csharp
public interface IServiceDescriptorTypeSelector
{
    public abstract IServiceTypeSelector AddClasses(
        Action<ITypeFilter> action,
        bool publicOnly)
    {
        // ...
    }
}
```

## Parameters

- `action` (`Action<ITypeFilter>`)
  The filtering action.
- `publicOnly` (`bool`)
  Specifies whether too add public types only.

## Exceptions

- `ArgumentNullException` -- If the `action` argument is `null`.
