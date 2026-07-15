---
title: TypeInfoFilter Enum
slug: api/specular.abstractions.typeinfofilter
sidebar:
  label: TypeInfoFilter
editUrl: false
description: Enumeration for possible type information filters.
---
## Definition

Enumeration for possible type information filters.

```csharp title="C#"
public enum TypeInfoFilter
```


## Fields

### Unknown

The type is unknown.

```csharp title="C#"
Unknown = 0
```

### Abstract

The type is abstract.

```csharp title="C#"
Abstract = 1
```

### Visible

The type is visible.

```csharp title="C#"
Visible = 2
```

### ValueType

The type is a value type.

```csharp title="C#"
ValueType = 3
```

### Sealed

The type is sealed.

```csharp title="C#"
Sealed = 5
```

### GenericType

The type is a generic type.

```csharp title="C#"
GenericType = 6
```

### Static

The type is static.

```csharp title="C#"
Static = -2
```