---
title: IReflectionAssemblySelector Interface
slug: api/specular.abstractions.ireflectionassemblyselector
sidebar:
  label: IReflectionAssemblySelector
editUrl: false
description: The compiled assembly selector
---
## Definition

The compiled assembly selector

```csharp title="C#"
public interface IReflectionAssemblySelector
```


## Methods

### EntryAssembly()

Will scan for types from this assembly at compile time.

```csharp title="C#"
IReflectionTypeSelector EntryAssembly()
```

#### Returns

[IReflectionTypeSelector](../specular.abstractions.ireflectiontypeselector/)

### FromAssemblies()

Will scan for types from all metadata assembly at compile time.

```csharp title="C#"
IReflectionTypeSelector FromAssemblies()
```

#### Returns

[IReflectionTypeSelector](../specular.abstractions.ireflectiontypeselector/)

### DependenciesFromAssemblyOf\<T>()

Will load and scan from given types assembly

```csharp title="C#"
IReflectionTypeSelector DependenciesFromAssemblyOf<T>()
```

#### Returns

[IReflectionTypeSelector](../specular.abstractions.ireflectiontypeselector/)

### DependenciesFromAssemblyOf(Type)

Will load and scan from given types assembly

```csharp title="C#"
IReflectionTypeSelector DependenciesFromAssemblyOf(Type type)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  

#### Returns

[IReflectionTypeSelector](../specular.abstractions.ireflectiontypeselector/)

### FromAssemblyOf\<T>()

Will scan for types from the assembly of type T.

```csharp title="C#"
IReflectionTypeSelector FromAssemblyOf<T>()
```

#### Returns

[IReflectionTypeSelector](../specular.abstractions.ireflectiontypeselector/)

### FromAssemblyOf(Type)

Will scan for types from the assembly of type.

```csharp title="C#"
IReflectionTypeSelector FromAssemblyOf(Type type)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  

#### Returns

[IReflectionTypeSelector](../specular.abstractions.ireflectiontypeselector/)

### NotFromAssemblyOf\<T>()

Will not scan for types from the assembly of type T.

```csharp title="C#"
IReflectionTypeSelector NotFromAssemblyOf<T>()
```

#### Returns

[IReflectionTypeSelector](../specular.abstractions.ireflectiontypeselector/)

### NotFromAssemblyOf(Type)

Will not scan for types from the assembly of type.

```csharp title="C#"
IReflectionTypeSelector NotFromAssemblyOf(Type type)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  

#### Returns

[IReflectionTypeSelector](../specular.abstractions.ireflectiontypeselector/)

### IncludeSystemAssemblies()

Include system assemblies

```csharp title="C#"
IReflectionTypeSelector IncludeSystemAssemblies()
```

#### Returns

[IReflectionTypeSelector](../specular.abstractions.ireflectiontypeselector/)