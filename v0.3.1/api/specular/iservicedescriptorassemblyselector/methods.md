# IServiceDescriptorAssemblySelector Methods

- Package: [Specular](/Specular/v0.3.1/api/specular.md)
- Type: [IServiceDescriptorAssemblySelector](/Specular/v0.3.1/api/specular/iservicedescriptorassemblyselector.md)
- Kind: `Methods`
- Members: `9`

The compiled assembly selector

<a id="dependenciesfromassemblyof"></a>

## DependenciesFromAssemblyOf

- Name: `DependenciesFromAssemblyOf`
- Modifiers: `abstract`
- Returns: [IServiceDescriptorTypeSelector](/Specular/v0.3.1/api/specular/iservicedescriptortypeselector.md)

Will load and scan from given types assembly

```csharp
public interface IServiceDescriptorAssemblySelector
{
    public abstract IServiceDescriptorTypeSelector DependenciesFromAssemblyOf<T>()
    {
        // ...
    }
}
```

<a id="dependenciesfromassemblyof-type"></a>

## DependenciesFromAssemblyOf(Type)

- Name: `DependenciesFromAssemblyOf(Type)`
- Modifiers: `abstract`
- Returns: [IServiceDescriptorTypeSelector](/Specular/v0.3.1/api/specular/iservicedescriptortypeselector.md)

Will load and scan from given types assembly

```csharp
public interface IServiceDescriptorAssemblySelector
{
    public abstract IServiceDescriptorTypeSelector DependenciesFromAssemblyOf(
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
- Returns: [IServiceDescriptorTypeSelector](/Specular/v0.3.1/api/specular/iservicedescriptortypeselector.md)

Will scan for types from this assembly at compile time.

```csharp
public interface IServiceDescriptorAssemblySelector
{
    public abstract IServiceDescriptorTypeSelector EntryAssembly()
    {
        // ...
    }
}
```

<a id="fromassemblies"></a>

## FromAssemblies

- Name: `FromAssemblies`
- Modifiers: `abstract`
- Returns: [IServiceDescriptorTypeSelector](/Specular/v0.3.1/api/specular/iservicedescriptortypeselector.md)

Will scan for types from all metadata assembly at compile time.

```csharp
public interface IServiceDescriptorAssemblySelector
{
    public abstract IServiceDescriptorTypeSelector FromAssemblies()
    {
        // ...
    }
}
```

<a id="fromassemblyof"></a>

## FromAssemblyOf

- Name: `FromAssemblyOf`
- Modifiers: `abstract`
- Returns: [IServiceDescriptorTypeSelector](/Specular/v0.3.1/api/specular/iservicedescriptortypeselector.md)

Will scan for types from the assembly of type `T`.

```csharp
public interface IServiceDescriptorAssemblySelector
{
    public abstract IServiceDescriptorTypeSelector FromAssemblyOf<T>()
    {
        // ...
    }
}
```

<a id="fromassemblyof-type"></a>

## FromAssemblyOf(Type)

- Name: `FromAssemblyOf(Type)`
- Modifiers: `abstract`
- Returns: [IServiceDescriptorTypeSelector](/Specular/v0.3.1/api/specular/iservicedescriptortypeselector.md)

Will scan for types from the assembly of type.

```csharp
public interface IServiceDescriptorAssemblySelector
{
    public abstract IServiceDescriptorTypeSelector FromAssemblyOf(
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
- Returns: [IServiceDescriptorTypeSelector](/Specular/v0.3.1/api/specular/iservicedescriptortypeselector.md)

Include system assemblies

```csharp
public interface IServiceDescriptorAssemblySelector
{
    public abstract IServiceDescriptorTypeSelector IncludeSystemAssemblies()
    {
        // ...
    }
}
```

<a id="notfromassemblyof"></a>

## NotFromAssemblyOf

- Name: `NotFromAssemblyOf`
- Modifiers: `abstract`
- Returns: [IServiceDescriptorTypeSelector](/Specular/v0.3.1/api/specular/iservicedescriptortypeselector.md)

Will not scan for types from the assembly of type `T`.

```csharp
public interface IServiceDescriptorAssemblySelector
{
    public abstract IServiceDescriptorTypeSelector NotFromAssemblyOf<T>()
    {
        // ...
    }
}
```

<a id="notfromassemblyof-type"></a>

## NotFromAssemblyOf(Type)

- Name: `NotFromAssemblyOf(Type)`
- Modifiers: `abstract`
- Returns: [IServiceDescriptorTypeSelector](/Specular/v0.3.1/api/specular/iservicedescriptortypeselector.md)

Will not scan for types from the assembly of type.

```csharp
public interface IServiceDescriptorAssemblySelector
{
    public abstract IServiceDescriptorTypeSelector NotFromAssemblyOf(
        Type type)
    {
        // ...
    }
}
```

## Parameters

- `type` (`Type`)
