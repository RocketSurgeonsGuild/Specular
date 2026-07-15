---
title: ITypeFilter Interface
slug: api/specular.abstractions.itypefilter
sidebar:
  label: ITypeFilter
editUrl: false
description: The Compiled Implementation Type Filter
---
## Definition

The Compiled Implementation Type Filter

```csharp title="C#"
public interface ITypeFilter
```


## Methods

### AssignableTo\<T>()

Will match all types that are assignable to T.

```csharp title="C#"
ITypeFilter AssignableTo<T>()
```

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### AssignableTo(Type)

Will match all types that are assignable to the specified type.

```csharp title="C#"
ITypeFilter AssignableTo(Type type)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  
The type that should be assignable from the matching types.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### AssignableToAny(Type, params Type[])

Will match all types that are assignable to any of the specified types.

```csharp title="C#"
ITypeFilter AssignableToAny(Type type, params Type[] types)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  
The first type that should be assignable from the matching types.

`types` [Type[]](https://learn.microsoft.com/dotnet/api/system.type/)  
The types that should be assignable from the matching types.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### NotAssignableTo\<T>()

Will not match all types that are assignable to T.

```csharp title="C#"
ITypeFilter NotAssignableTo<T>()
```

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### NotAssignableTo(Type)

Will not match all types that are assignable to the specified type.

```csharp title="C#"
ITypeFilter NotAssignableTo(Type type)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  
The type that should be assignable from the matching types.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### NotAssignableToAny(Type, params Type[])

Will not match all types that are assignable to any of the specified types.

```csharp title="C#"
ITypeFilter NotAssignableToAny(Type type, params Type[] types)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  
The first type that should be assignable from the matching types.

`types` [Type[]](https://learn.microsoft.com/dotnet/api/system.type/)  
The types that should be assignable from the matching types.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### EndsWith(string, params string[])

Will match all types that end with

```csharp title="C#"
ITypeFilter EndsWith(string value, params string[] values)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string/)  

`values` [string[]](https://learn.microsoft.com/dotnet/api/system.string/)  

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### NotEndsWith(string, params string[])

Will match all types that end with

```csharp title="C#"
ITypeFilter NotEndsWith(string value, params string[] values)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string/)  

`values` [string[]](https://learn.microsoft.com/dotnet/api/system.string/)  

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### StartsWith(string, params string[])

Will match all types that start with

```csharp title="C#"
ITypeFilter StartsWith(string value, params string[] values)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string/)  

`values` [string[]](https://learn.microsoft.com/dotnet/api/system.string/)  

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### NotStartsWith(string, params string[])

Will match all types that start with

```csharp title="C#"
ITypeFilter NotStartsWith(string value, params string[] values)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string/)  

`values` [string[]](https://learn.microsoft.com/dotnet/api/system.string/)  

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### Contains(string, params string[])

Will match all types that contain the given values

```csharp title="C#"
ITypeFilter Contains(string value, params string[] values)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string/)  

`values` [string[]](https://learn.microsoft.com/dotnet/api/system.string/)  

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### NotContains(string, params string[])

Will match all types that contain the given values

```csharp title="C#"
ITypeFilter NotContains(string value, params string[] values)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string/)  

`values` [string[]](https://learn.microsoft.com/dotnet/api/system.string/)  

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### InExactNamespaceOf\<T>()

Will match all types in the exact same namespace as the type T

```csharp title="C#"
ITypeFilter InExactNamespaceOf<T>()
```

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### InExactNamespaceOf(Type, params Type[])

Will match all types in the exact same namespace as the type types

```csharp title="C#"
ITypeFilter InExactNamespaceOf(Type type, params Type[] types)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  
The first type in the namespaces to include.

`types` [Type[]](https://learn.microsoft.com/dotnet/api/system.type/)  
The types in the namespaces to include.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### InExactNamespaces(string, params string[])

Will match all types in the exact same namespace as the type namespaces

```csharp title="C#"
ITypeFilter InExactNamespaces(string first, params string[] namespaces)
```

#### Parameters

`first` [string](https://learn.microsoft.com/dotnet/api/system.string/)  
The first namespace to include.

`namespaces` [string[]](https://learn.microsoft.com/dotnet/api/system.string/)  
The namespace to include.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### InNamespaceOf\<T>()

Will match all types in the same namespace as the type T.

```csharp title="C#"
ITypeFilter InNamespaceOf<T>()
```

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### InNamespaceOf(Type, params Type[])

Will match all types in any of the namespaces of the types specified.

```csharp title="C#"
ITypeFilter InNamespaceOf(Type type, params Type[] types)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  
The first type in the namespaces to include.

`types` [Type[]](https://learn.microsoft.com/dotnet/api/system.type/)  
The types in the namespaces to include.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### InNamespaces(string, params string[])

Will match all types in any of the namespaces specified.

```csharp title="C#"
ITypeFilter InNamespaces(string first, params string[] namespaces)
```

#### Parameters

`first` [string](https://learn.microsoft.com/dotnet/api/system.string/)  
The first namespace to include.

`namespaces` [string[]](https://learn.microsoft.com/dotnet/api/system.string/)  
The namespaces to include.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### NotInNamespaceOf\<T>()

Will match all types outside of the same namespace as the type T.

```csharp title="C#"
ITypeFilter NotInNamespaceOf<T>()
```

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### NotInNamespaceOf(Type, params Type[])

Will match all types outside of all of the namespaces of the types specified.

```csharp title="C#"
ITypeFilter NotInNamespaceOf(Type type, params Type[] types)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  
The first type in the namespaces to include.

`types` [Type[]](https://learn.microsoft.com/dotnet/api/system.type/)  
The types in the namespaces to include.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### NotInNamespaces(string, params string[])

Will match all types outside of all of the namespaces specified.

```csharp title="C#"
ITypeFilter NotInNamespaces(string first, params string[] namespaces)
```

#### Parameters

`first` [string](https://learn.microsoft.com/dotnet/api/system.string/)  
The first namespace to include.

`namespaces` [string[]](https://learn.microsoft.com/dotnet/api/system.string/)  
The namespaces to include.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### WithAttribute\<T>()

Will match all types that has an attribute of type T defined.

```csharp title="C#"
ITypeFilter WithAttribute<T>() where T : Attribute
```

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### WithAttribute(Type)

Will match all types that has an attribute of attributeType defined.

```csharp title="C#"
ITypeFilter WithAttribute(Type attributeType)
```

#### Parameters

`attributeType` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  
Type of the attribute.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### WithAttribute(string?)

Will match all types that has an attribute of attributeFullName defined.

```csharp title="C#"
ITypeFilter WithAttribute(string? attributeFullName)
```

#### Parameters

`attributeFullName` [string](https://learn.microsoft.com/dotnet/api/system.string/)  
The full name of the attribute.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### WithAnyAttribute(Type, params Type[])

Will match all types that has an attribute of attributeType defined.

```csharp title="C#"
ITypeFilter WithAnyAttribute(Type attributeType, params Type[] attributeTypes)
```

#### Parameters

`attributeType` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  
Type of the attribute.

`attributeTypes` [Type[]](https://learn.microsoft.com/dotnet/api/system.type/)  
Types of the attribute.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### WithAnyAttribute(string?, params string[])

Will match all types that has an attribute of attributeFullName defined.

```csharp title="C#"
ITypeFilter WithAnyAttribute(string? attributeFullName, params string[] attributeFullNames)
```

#### Parameters

`attributeFullName` [string](https://learn.microsoft.com/dotnet/api/system.string/)  
The full name of the attribute.

`attributeFullNames` [string[]](https://learn.microsoft.com/dotnet/api/system.string/)  
The full name of the attributes.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### WithoutAttribute\<T>()

Will match all types that doesn't have an attribute of type T defined.

```csharp title="C#"
ITypeFilter WithoutAttribute<T>() where T : Attribute
```

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### WithoutAttribute(Type)

Will match all types that doesn't have an attribute of attributeType defined.

```csharp title="C#"
ITypeFilter WithoutAttribute(Type attributeType)
```

#### Parameters

`attributeType` [Type](https://learn.microsoft.com/dotnet/api/system.type/)  
Type of the attribute.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### WithoutAttribute(string?)

Will match all types that doesn't have an attribute of attributeFullName defined.

```csharp title="C#"
ITypeFilter WithoutAttribute(string? attributeFullName)
```

#### Parameters

`attributeFullName` [string](https://learn.microsoft.com/dotnet/api/system.string/)  
The full name of the attribute.

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### KindOf(TypeKindFilter, params TypeKindFilter[])

Will match all types that are of the specified typeKindFilter.

```csharp title="C#"
ITypeFilter KindOf(TypeKindFilter typeKindFilter, params TypeKindFilter[] typeKindFilters)
```

#### Parameters

`typeKindFilter` [TypeKindFilter](../specular.abstractions.typekindfilter/)  

`typeKindFilters` [TypeKindFilter[]](../specular.abstractions.typekindfilter/)  

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### NotKindOf(TypeKindFilter, params TypeKindFilter[])

Will match all types that are not of the specified typeKindFilter.

```csharp title="C#"
ITypeFilter NotKindOf(TypeKindFilter typeKindFilter, params TypeKindFilter[] typeKindFilters)
```

#### Parameters

`typeKindFilter` [TypeKindFilter](../specular.abstractions.typekindfilter/)  

`typeKindFilters` [TypeKindFilter[]](../specular.abstractions.typekindfilter/)  

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### InfoOf(TypeInfoFilter, params TypeInfoFilter[])

Will match all types that are of the specified typeInfoFilter.

```csharp title="C#"
ITypeFilter InfoOf(TypeInfoFilter typeInfoFilter, params TypeInfoFilter[] typeInfoFilters)
```

#### Parameters

`typeInfoFilter` [TypeInfoFilter](../specular.abstractions.typeinfofilter/)  

`typeInfoFilters` [TypeInfoFilter[]](../specular.abstractions.typeinfofilter/)  

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)

### NotInfoOf(TypeInfoFilter, params TypeInfoFilter[])

Will match all types that are not of the specified typeInfoFilter.

```csharp title="C#"
ITypeFilter NotInfoOf(TypeInfoFilter typeInfoFilter, params TypeInfoFilter[] typeInfoFilters)
```

#### Parameters

`typeInfoFilter` [TypeInfoFilter](../specular.abstractions.typeinfofilter/)  

`typeInfoFilters` [TypeInfoFilter[]](../specular.abstractions.typeinfofilter/)  

#### Returns

[ITypeFilter](../specular.abstractions.itypefilter/)