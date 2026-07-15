---
title: TypeKindFilter Enum
slug: api/specular.abstractions.typekindfilter
sidebar:
  label: TypeKindFilter
editUrl: false
description: Enumeration for possible kinds of type symbols.
---
## Definition

Enumeration for possible kinds of type symbols.

```csharp title="C#"
public enum TypeKindFilter : byte
```


## Fields

### Array

Type is an array type.

```csharp title="C#"
Array = 1
```

### Class

Type is a class.

```csharp title="C#"
Class = 2
```

### Delegate

Type is a delegate.

```csharp title="C#"
Delegate = 3
```

### Enum

Type is an enumeration.

```csharp title="C#"
Enum = 5
```

### Interface

Type is an interface.

```csharp title="C#"
Interface = 7
```

### Struct

Type is a C# struct or VB Structure

```csharp title="C#"
Struct = 10
```