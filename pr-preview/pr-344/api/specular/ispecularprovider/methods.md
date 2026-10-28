# ISpecularProvider Methods

- Package: [Specular](/Specular/pr-preview/pr-344/api/specular.md)
- Type: [ISpecularProvider](/Specular/pr-preview/pr-344/api/specular/ispecularprovider.md)
- Kind: `Methods`
- Members: `3`

A provider that gets a list of assemblies for a given context

<a id="getassemblies"></a>
<a id="getassemblies-action-ireflectionassemblyselector-int-string-string"></a>

## GetAssemblies(Action<IReflectionAssemblySelector>, int, string, string)

- Name: `GetAssemblies(Action<IReflectionAssemblySelector>, int, string, string)`
- Modifiers: `abstract`
- Returns: `IEnumerable<Assembly>`

Get the full list of assemblies

```csharp
public interface ISpecularProvider
{
    public abstract IEnumerable<Assembly> GetAssemblies(
        Action<IReflectionAssemblySelector> action,
        int lineNumber = 0,
        string filePath = "",
        string argumentExpression = "")
    {
        // ...
    }
}
```

## Parameters

- `action` (`Action<IReflectionAssemblySelector>`)
- `lineNumber` (`int`) `optional`
- `filePath` (`string`) `optional`
- `argumentExpression` (`string`) `optional`

## Returns

`IEnumerable<Assembly>` -- IEnumerable{Assembly}.

<a id="gettypes"></a>
<a id="gettypes-func-ireflectiontypeselector-ienumerable-type-int-string-string"></a>

## GetTypes(Func<IReflectionTypeSelector, IEnumerable<Type>>, int, string, string)

- Name: `GetTypes(Func<IReflectionTypeSelector, IEnumerable<Type>>, int, string, string)`
- Modifiers: `abstract`
- Returns: `IEnumerable<Type>`

Get the full list of types using the given selector

```csharp
public interface ISpecularProvider
{
    public abstract IEnumerable<Type> GetTypes(
        Func<IReflectionTypeSelector, IEnumerable<Type>> selector,
        int lineNumber = 0,
        string filePath = "",
        string argumentExpression = "")
    {
        // ...
    }
}
```

## Parameters

- `selector` (`Func<IReflectionTypeSelector, IEnumerable<Type>>`)
- `lineNumber` (`int`) `optional`
- `filePath` (`string`) `optional`
- `argumentExpression` (`string`) `optional`

## Returns

`IEnumerable<Type>` -- IEnumerable{Type}.

<a id="scan"></a>
<a id="scan-iservicecollection-action-iservicedescriptorassemblyselector-int-string-string"></a>

## Scan(IServiceCollection, Action<IServiceDescriptorAssemblySelector>, int, string, string)

- Name: `Scan(IServiceCollection, Action<IServiceDescriptorAssemblySelector>, int, string, string)`
- Modifiers: `abstract`
- Returns: `IServiceCollection`

Scan for types using the given selector

```csharp
public interface ISpecularProvider
{
    public abstract IServiceCollection Scan(
        IServiceCollection services,
        Action<IServiceDescriptorAssemblySelector> selector,
        int lineNumber = 0,
        string filePath = "",
        string argumentExpression = "")
    {
        // ...
    }
}
```

## Parameters

- `services` (`IServiceCollection`)
- `selector` (`Action<IServiceDescriptorAssemblySelector>`)
- `lineNumber` (`int`) `optional`
- `filePath` (`string`) `optional`
- `argumentExpression` (`string`) `optional`

## Returns

`IServiceCollection` -- IEnumerable{Type}.
