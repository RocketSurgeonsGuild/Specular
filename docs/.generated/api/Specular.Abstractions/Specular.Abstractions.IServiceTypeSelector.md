---
title: IServiceTypeSelector Interface
slug: api/specular.abstractions.iservicetypeselector
sidebar:
  label: IServiceTypeSelector
editUrl: false
description: The Compiled Service Type Selector
---
## Definition

The Compiled Service Type Selector

```csharp title="C#"
public interface IServiceTypeSelector
```


## Methods

### AsSelf()

Registers each matching concrete type as itself.

```csharp title="C#"
IServiceLifetimeSelector AsSelf()
```

#### Returns

[IServiceLifetimeSelector](../specular.abstractions.iservicelifetimeselector/)

### As\<T>()

Registers each matching concrete type as T.

```csharp title="C#"
IServiceLifetimeSelector As<T>()
```

#### Returns

[IServiceLifetimeSelector](../specular.abstractions.iservicelifetimeselector/)

### As(Type)

Registers each matching concrete type as <param name="type">type</param>

```csharp title="C#"
IServiceLifetimeSelector As(Type type)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  

#### Returns

[IServiceLifetimeSelector](../specular.abstractions.iservicelifetimeselector/)

### AsImplementedInterfaces()

Registers each matching concrete type as all of its implemented interfaces.

```csharp title="C#"
IServiceLifetimeSelector AsImplementedInterfaces()
```

#### Returns

[IServiceLifetimeSelector](../specular.abstractions.iservicelifetimeselector/)

### AsImplementedInterfaces(Action\<ITypeFilter>)

Registers each matching concrete type as all of its implemented interfaces.

```csharp title="C#"
IServiceLifetimeSelector AsImplementedInterfaces(Action<ITypeFilter> action)
```

#### Parameters

`action` [Action\<ITypeFilter\>](https://learn.microsoft.com/dotnet/api/system.action-1/)  

#### Returns

[IServiceLifetimeSelector](../specular.abstractions.iservicelifetimeselector/)

### AsSelfWithInterfaces()

Registers each matching concrete type as all of its implemented interfaces, by returning an instance of the main type

```csharp title="C#"
IServiceLifetimeSelector AsSelfWithInterfaces()
```

#### Returns

[IServiceLifetimeSelector](../specular.abstractions.iservicelifetimeselector/)

### AsSelfWithInterfaces(Action\<ITypeFilter>)

Registers each matching concrete type as all of its implemented interfaces, by returning an instance of the main type

```csharp title="C#"
IServiceLifetimeSelector AsSelfWithInterfaces(Action<ITypeFilter> action)
```

#### Parameters

`action` [Action\<ITypeFilter\>](https://learn.microsoft.com/dotnet/api/system.action-1/)  

#### Returns

[IServiceLifetimeSelector](../specular.abstractions.iservicelifetimeselector/)

### AsMatchingInterface()

Registers the type with the first found matching interface name.  (e.g. ClassName is matched to IClassName)

```csharp title="C#"
IServiceLifetimeSelector AsMatchingInterface()
```

#### Returns

[IServiceLifetimeSelector](../specular.abstractions.iservicelifetimeselector/)