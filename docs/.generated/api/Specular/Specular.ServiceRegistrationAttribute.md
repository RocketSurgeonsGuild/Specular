---
title: ServiceRegistrationAttribute Class
slug: api/specular.serviceregistrationattribute
sidebar:
  label: ServiceRegistrationAttribute
editUrl: false
description: Attribute used to define the service registration of a given type
---
## Definition

Attribute used to define the service registration of a given type

```csharp title="C#"
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ServiceRegistrationAttribute : Attribute
```

Inheritance [object](https://learn.microsoft.com/dotnet/api/system.object/) → [Attribute](https://learn.microsoft.com/dotnet/api/system.attribute/)
## Remarks

Constructor to specify the service type including optional runtime

## Constructors

### ServiceRegistrationAttribute(ServiceLifetime, params Type[])

Attribute used to define the service registration of a given type

```csharp title="C#"
public ServiceRegistrationAttribute(ServiceLifetime lifetime, params Type[] serviceTypes)
```

#### Parameters

`lifetime` [ServiceLifetime](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.servicelifetime/)  

`serviceTypes` [Type[]](https://learn.microsoft.com/dotnet/api/system.type/)  

#### Remarks

Constructor to specify the service type including optional runtime

### ServiceRegistrationAttribute(params Type[])

Constructor to specify the service type including optional runtime

```csharp title="C#"
public ServiceRegistrationAttribute(params Type[] serviceTypes)
```

#### Parameters

`serviceTypes` [Type[]](https://learn.microsoft.com/dotnet/api/system.type/)  

#### Remarks

The default lifetime is [Singleton](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.servicelifetime#microsoft-extensions-dependencyinjection-servicelifetime-singleton/)

## Properties

### ServiceTypes

The service type

```csharp title="C#"
public ImmutableArray<Type> ServiceTypes { get; }
```

### Lifetime

The lifetime

```csharp title="C#"
public ServiceLifetime Lifetime { get; }
```