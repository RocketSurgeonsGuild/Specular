---
title: IServiceDescriptorTypeSelector Interface
slug: api/specular.abstractions.iservicedescriptortypeselector
sidebar:
  label: IServiceDescriptorTypeSelector
editUrl: false
description: The Compiled Implementation Type Selector
---
## Definition

The Compiled Implementation Type Selector

```csharp title="C#"
public interface IServiceDescriptorTypeSelector : IServiceDescriptorAssemblySelector
```


## Methods

### AddClasses()

Adds all public, non-abstract classes from the selected assemblies to the [IServiceCollection](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection/).

```csharp title="C#"
IServiceTypeSelector AddClasses()
```

#### Returns

[IServiceTypeSelector](../specular.abstractions.iservicetypeselector/)

### AddClasses(bool)

Adds all non-abstract classes from the selected assemblies to the [IServiceCollection](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection/).

```csharp title="C#"
IServiceTypeSelector AddClasses(bool publicOnly)
```

#### Parameters

`publicOnly` [bool](https://learn.microsoft.com/dotnet/api/system.boolean/)  
Specifies whether too add public types only.

#### Returns

[IServiceTypeSelector](../specular.abstractions.iservicetypeselector/)

### AddClasses(Action\<ITypeFilter>)

Adds all public, non-abstract classes from the selected assemblies that matches the requirements specified in the action to the [IServiceCollection](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection/).

```csharp title="C#"
IServiceTypeSelector AddClasses(Action<ITypeFilter> action)
```

#### Parameters

`action` [Action\<ITypeFilter\>](https://learn.microsoft.com/dotnet/api/system.action-1/)  
The filtering action.

#### Returns

[IServiceTypeSelector](../specular.abstractions.iservicetypeselector/)
#### Exceptions

[ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception/)  
If the action argument is null.


### AddClasses(Action\<ITypeFilter>, bool)

Adds all non-abstract classes from the selected assemblies that matches the requirements specified in the action to the [IServiceCollection](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection/).

```csharp title="C#"
IServiceTypeSelector AddClasses(Action<ITypeFilter> action, bool publicOnly)
```

#### Parameters

`action` [Action\<ITypeFilter\>](https://learn.microsoft.com/dotnet/api/system.action-1/)  
The filtering action.

`publicOnly` [bool](https://learn.microsoft.com/dotnet/api/system.boolean/)  
Specifies whether too add public types only.

#### Returns

[IServiceTypeSelector](../specular.abstractions.iservicetypeselector/)
#### Exceptions

[ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception/)  
If the action argument is null.