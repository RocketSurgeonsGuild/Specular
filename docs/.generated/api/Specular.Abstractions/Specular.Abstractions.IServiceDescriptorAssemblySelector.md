---
title: IServiceDescriptorAssemblySelector Interface
slug: api/specular.abstractions.iservicedescriptorassemblyselector
sidebar:
  label: IServiceDescriptorAssemblySelector
editUrl: false
description: The compiled assembly selector
---
## Definition

The compiled assembly selector

```csharp title="C#"
public interface IServiceDescriptorAssemblySelector
```


## Methods

### EntryAssembly()

Will scan for types from this assembly at compile time.

```csharp title="C#"
IServiceDescriptorTypeSelector EntryAssembly()
```

#### Returns

[IServiceDescriptorTypeSelector](../specular.abstractions.iservicedescriptortypeselector/)

### FromAssemblies()

Will scan for types from all metadata assembly at compile time.

```csharp title="C#"
IServiceDescriptorTypeSelector FromAssemblies()
```

#### Returns

[IServiceDescriptorTypeSelector](../specular.abstractions.iservicedescriptortypeselector/)

### DependenciesFromAssemblyOf\<T>()

Will load and scan from given types assembly

```csharp title="C#"
IServiceDescriptorTypeSelector DependenciesFromAssemblyOf<T>()
```

#### Returns

[IServiceDescriptorTypeSelector](../specular.abstractions.iservicedescriptortypeselector/)

### DependenciesFromAssemblyOf(Type)

Will load and scan from given types assembly

```csharp title="C#"
IServiceDescriptorTypeSelector DependenciesFromAssemblyOf(Type type)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  

#### Returns

[IServiceDescriptorTypeSelector](../specular.abstractions.iservicedescriptortypeselector/)

### FromAssemblyOf\<T>()

Will scan for types from the assembly of type T.

```csharp title="C#"
IServiceDescriptorTypeSelector FromAssemblyOf<T>()
```

#### Returns

[IServiceDescriptorTypeSelector](../specular.abstractions.iservicedescriptortypeselector/)

### FromAssemblyOf(Type)

Will scan for types from the assembly of type.

```csharp title="C#"
IServiceDescriptorTypeSelector FromAssemblyOf(Type type)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  

#### Returns

[IServiceDescriptorTypeSelector](../specular.abstractions.iservicedescriptortypeselector/)

### NotFromAssemblyOf\<T>()

Will not scan for types from the assembly of type T.

```csharp title="C#"
IServiceDescriptorTypeSelector NotFromAssemblyOf<T>()
```

#### Returns

[IServiceDescriptorTypeSelector](../specular.abstractions.iservicedescriptortypeselector/)

### NotFromAssemblyOf(Type)

Will not scan for types from the assembly of type.

```csharp title="C#"
IServiceDescriptorTypeSelector NotFromAssemblyOf(Type type)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  

#### Returns

[IServiceDescriptorTypeSelector](../specular.abstractions.iservicedescriptortypeselector/)

### IncludeSystemAssemblies()

Include system assemblies

```csharp title="C#"
IServiceDescriptorTypeSelector IncludeSystemAssemblies()
```

#### Returns

[IServiceDescriptorTypeSelector](../specular.abstractions.iservicedescriptortypeselector/)