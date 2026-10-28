# IReflectionAssemblySelector Methods

- Package: [Specular](/Specular/api/specular.md)
- Type: [IReflectionAssemblySelector](/Specular/api/specular/ireflectionassemblyselector.md)
- Kind: `Methods`
- Members: `9`

The compiled assembly selector

<a id="dependenciesfromassemblyof"></a>

## DependenciesFromAssemblyOf

- Name: `DependenciesFromAssemblyOf`
- Modifiers: `abstract`
- Returns: [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md)

Will load and scan from given types assembly

```csharp
public interface IReflectionAssemblySelector
{
    public abstract IReflectionTypeSelector DependenciesFromAssemblyOf<T>()
    {
        // ...
    }
}
```

<a id="dependenciesfromassemblyof-type"></a>

## DependenciesFromAssemblyOf(Type)

- Name: `DependenciesFromAssemblyOf(Type)`
- Modifiers: `abstract`
- Returns: [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md)

Will load and scan from given types assembly

```csharp
public interface IReflectionAssemblySelector
{
    public abstract IReflectionTypeSelector DependenciesFromAssemblyOf(
        Type type)
    {
        // ...
    }
}
```

## Parameters

- `type` (`Type`)

<a id="entryassembly"></a>

## EntryAssembly

- Name: `EntryAssembly`
- Modifiers: `abstract`
- Returns: [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md)

Will scan for types from this assembly at compile time.

```csharp
public interface IReflectionAssemblySelector
{
    public abstract IReflectionTypeSelector EntryAssembly()
    {
        // ...
    }
}
```

<a id="fromassemblies"></a>

## FromAssemblies

- Name: `FromAssemblies`
- Modifiers: `abstract`
- Returns: [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md)

Will scan for types from all metadata assembly at compile time.

```csharp
public interface IReflectionAssemblySelector
{
    public abstract IReflectionTypeSelector FromAssemblies()
    {
        // ...
    }
}
```

<a id="fromassemblyof"></a>

## FromAssemblyOf

- Name: `FromAssemblyOf`
- Modifiers: `abstract`
- Returns: [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md)

Will scan for types from the assembly of type `T`.

```csharp
public interface IReflectionAssemblySelector
{
    public abstract IReflectionTypeSelector FromAssemblyOf<T>()
    {
        // ...
    }
}
```

<a id="fromassemblyof-type"></a>

## FromAssemblyOf(Type)

- Name: `FromAssemblyOf(Type)`
- Modifiers: `abstract`
- Returns: [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md)

Will scan for types from the assembly of type.

```csharp
public interface IReflectionAssemblySelector
{
    public abstract IReflectionTypeSelector FromAssemblyOf(
        Type type)
    {
        // ...
    }
}
```

## Parameters

- `type` (`Type`)

<a id="includesystemassemblies"></a>

## IncludeSystemAssemblies

- Name: `IncludeSystemAssemblies`
- Modifiers: `abstract`
- Returns: [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md)

Include system assemblies

```csharp
public interface IReflectionAssemblySelector
{
    public abstract IReflectionTypeSelector IncludeSystemAssemblies()
    {
        // ...
    }
}
```

<a id="notfromassemblyof"></a>

## NotFromAssemblyOf

- Name: `NotFromAssemblyOf`
- Modifiers: `abstract`
- Returns: [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md)

Will not scan for types from the assembly of type `T`.

```csharp
public interface IReflectionAssemblySelector
{
    public abstract IReflectionTypeSelector NotFromAssemblyOf<T>()
    {
        // ...
    }
}
```

<a id="notfromassemblyof-type"></a>

## NotFromAssemblyOf(Type)

- Name: `NotFromAssemblyOf(Type)`
- Modifiers: `abstract`
- Returns: [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md)

Will not scan for types from the assembly of type.

```csharp
public interface IReflectionAssemblySelector
{
    public abstract IReflectionTypeSelector NotFromAssemblyOf(
        Type type)
    {
        // ...
    }
}
```

## Parameters

- `type` (`Type`)
