---
title: ISpecularProvider Interface
slug: api/specular.ispecularprovider
sidebar:
  label: ISpecularProvider
editUrl: false
description: A provider that gets a list of assemblies for a given context
---
## Definition

A provider that gets a list of assemblies for a given context

```csharp title="C#"
public interface ISpecularProvider
```


## Methods

### GetAssemblies(Action\<IReflectionAssemblySelector>, int, string, string)

Get the full list of assemblies

```csharp title="C#"
IEnumerable<Assembly> GetAssemblies(Action<IReflectionAssemblySelector> action, int lineNumber = 0, string filePath = "", string argumentExpression = "")
```

#### Parameters

`action` [Action\<IReflectionAssemblySelector\>](https://learn.microsoft.com/dotnet/api/system.action-1/)  

`lineNumber` [int](https://learn.microsoft.com/dotnet/api/system.int32/)  

`filePath` [string](https://learn.microsoft.com/dotnet/api/system.string/)  

`argumentExpression` [string](https://learn.microsoft.com/dotnet/api/system.string/)  

#### Returns

[IEnumerable\<Assembly\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1/)  
IEnumerable{Assembly}.

### GetTypes(Func\<IReflectionTypeSelector, IEnumerable\<Type>>, int, string, string)

Get the full list of types using the given selector

```csharp title="C#"
IEnumerable<Type> GetTypes(Func<IReflectionTypeSelector, IEnumerable<Type>> selector, int lineNumber = 0, string filePath = "", string argumentExpression = "")
```

#### Parameters

`selector` [Func\<IReflectionTypeSelector, IEnumerable\<Type\>\>](https://learn.microsoft.com/dotnet/api/system.func-2/)  

`lineNumber` [int](https://learn.microsoft.com/dotnet/api/system.int32/)  

`filePath` [string](https://learn.microsoft.com/dotnet/api/system.string/)  

`argumentExpression` [string](https://learn.microsoft.com/dotnet/api/system.string/)  

#### Returns

[IEnumerable\<Type\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1/)  
IEnumerable{Type}.

### Scan(IServiceCollection, Action\<IServiceDescriptorAssemblySelector>, int, string, string)

Scan for types using the given selector

```csharp title="C#"
IServiceCollection Scan(IServiceCollection services, Action<IServiceDescriptorAssemblySelector> selector, int lineNumber = 0, string filePath = "", string argumentExpression = "")
```

#### Parameters

`services` [IServiceCollection](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection/)  

`selector` [Action\<IServiceDescriptorAssemblySelector\>](https://learn.microsoft.com/dotnet/api/system.action-1/)  

`lineNumber` [int](https://learn.microsoft.com/dotnet/api/system.int32/)  

`filePath` [string](https://learn.microsoft.com/dotnet/api/system.string/)  

`argumentExpression` [string](https://learn.microsoft.com/dotnet/api/system.string/)  

#### Returns

[IServiceCollection](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection/)  
IEnumerable{Type}.