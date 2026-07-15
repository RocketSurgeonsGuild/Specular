---
title: IReflectionTypeSelector Interface
slug: api/specular.abstractions.ireflectiontypeselector
sidebar:
  label: IReflectionTypeSelector
editUrl: false
description: The Compiled Implementation Type Selector
---
## Definition

The Compiled Implementation Type Selector

```csharp title="C#"
public interface IReflectionTypeSelector : IReflectionAssemblySelector
```


## Methods

### GetTypes()

Lists all of the classes in a given assembly

```csharp title="C#"
IEnumerable<Type> GetTypes()
```

#### Returns

[IEnumerable\<Type\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1/)

### GetTypes(bool)

Lists all of the public classes in a given assembly

```csharp title="C#"
IEnumerable<Type> GetTypes(bool publicOnly)
```

#### Parameters

`publicOnly` [bool](https://learn.microsoft.com/dotnet/api/system.boolean/)  
Specifies whether too add public types only.

#### Returns

[IEnumerable\<Type\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1/)

### GetTypes(Action\<ITypeFilter>)

Adds all public, non-abstract classes from the selected assemblies that matches the requirements specified in the action to the [IServiceCollection](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection/).

```csharp title="C#"
IEnumerable<Type> GetTypes(Action<ITypeFilter> action)
```

#### Parameters

`action` [Action\<ITypeFilter\>](https://learn.microsoft.com/dotnet/api/system.action-1/)  
The filtering action.

#### Returns

[IEnumerable\<Type\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1/)

### GetTypes(bool, Action\<ITypeFilter>)

Adds all non-abstract classes from the selected assemblies that matches the requirements specified in the action to the [IServiceCollection](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection/).

```csharp title="C#"
IEnumerable<Type> GetTypes(bool publicOnly, Action<ITypeFilter> action)
```

#### Parameters

`publicOnly` [bool](https://learn.microsoft.com/dotnet/api/system.boolean/)  
Specifies whether too add public types only.

`action` [Action\<ITypeFilter\>](https://learn.microsoft.com/dotnet/api/system.action-1/)  
The filtering action.

#### Returns

[IEnumerable\<Type\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1/)