---
title: SpecularHashAttribute Class
slug: api/specular.abstractions.specularhashattribute
sidebar:
  label: SpecularHashAttribute
editUrl: false
description: Attribute used to define the compiled type provider for a given assembly
---
## Definition

Attribute used to define the compiled type provider for a given assembly

```csharp title="C#"
[AttributeUsage(AttributeTargets.Assembly)]
public sealed class SpecularHashAttribute : Attribute
```

Inheritance [object](https://learn.microsoft.com/dotnet/api/system.object/) → [Attribute](https://learn.microsoft.com/dotnet/api/system.attribute/)

## Constructors

### SpecularHashAttribute(string)

Attribute used to define the compiled type provider for a given assembly

```csharp title="C#"
public SpecularHashAttribute(string generatedHash)
```

#### Parameters

`generatedHash` [string](https://learn.microsoft.com/dotnet/api/system.string/)  


## Properties

### GeneratedHash

The generated hash to be used for cache busting

```csharp title="C#"
public string GeneratedHash { get; }
```