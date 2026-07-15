---
title: ExcludeFromSpecularAttribute Class
slug: api/specular.abstractions.excludefromspecularattribute
sidebar:
  label: ExcludeFromSpecularAttribute
editUrl: false
description: Exclude the given assembly from compiled type provider resolution.
---
## Definition

Exclude the given assembly from compiled type provider resolution.

```csharp title="C#"
[AttributeUsage(AttributeTargets.Assembly)]
public class ExcludeFromSpecularAttribute : Attribute
```

Inheritance [object](https://learn.microsoft.com/dotnet/api/system.object/) → [Attribute](https://learn.microsoft.com/dotnet/api/system.attribute/)
## Remarks

This assembly will still have access to compiled types, but nothing will be resolved internally.

## Constructors

### ExcludeFromSpecularAttribute()

Exclude the given assembly from compiled type provider resolution.

```csharp title="C#"
public ExcludeFromSpecularAttribute()
```

#### Remarks

This assembly will still have access to compiled types, but nothing will be resolved internally.