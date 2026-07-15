---
title: IServiceLifetimeSelector Interface
slug: api/specular.abstractions.iservicelifetimeselector
sidebar:
  label: IServiceLifetimeSelector
editUrl: false
description: The Compiled Lifetime Selector
---
## Definition

The Compiled Lifetime Selector

```csharp title="C#"
public interface IServiceLifetimeSelector : IServiceTypeSelector
```


## Methods

### WithSingletonLifetime()

Registers each matching concrete type with [Singleton](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.servicelifetime#microsoft-extensions-dependencyinjection-servicelifetime-singleton/) lifetime.

```csharp title="C#"
void WithSingletonLifetime()
```


### WithScopedLifetime()

Registers each matching concrete type with [Scoped](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.servicelifetime#microsoft-extensions-dependencyinjection-servicelifetime-scoped/) lifetime.

```csharp title="C#"
void WithScopedLifetime()
```


### WithTransientLifetime()

Registers each matching concrete type with [Transient](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.servicelifetime#microsoft-extensions-dependencyinjection-servicelifetime-transient/) lifetime.

```csharp title="C#"
void WithTransientLifetime()
```


### WithLifetime(ServiceLifetime)

Registers each matching concrete type with the specified lifetime.

```csharp title="C#"
void WithLifetime(ServiceLifetime lifetime)
```

#### Parameters

`lifetime` [ServiceLifetime](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.servicelifetime/)