---
title: ServiceRegistrationAttribute<TService1, TService2, TService3, TService4> Class
slug: api/specular.serviceregistrationattribute-4
sidebar:
  label: ServiceRegistrationAttribute<TService1, TService2, TService3, TService4>
editUrl: false
description: Attribute used to define the service registration of a given type
---
## Definition

Attribute used to define the service registration of a given type

```csharp title="C#"
public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4> : ServiceRegistrationAttribute
```

### Type Parameters

`TService1`  


`TService2`  


`TService3`  


`TService4`  


Inheritance [object](https://learn.microsoft.com/dotnet/api/system.object/) → [Attribute](https://learn.microsoft.com/dotnet/api/system.attribute/) → [ServiceRegistrationAttribute](../specular.serviceregistrationattribute/)
## Remarks

Constructor to specify the lifetime

## Constructors

### ServiceRegistrationAttribute(ServiceLifetime)

Attribute used to define the service registration of a given type

```csharp title="C#"
public ServiceRegistrationAttribute(ServiceLifetime lifetime = ServiceLifetime.Singleton)
```

#### Parameters

`lifetime` [ServiceLifetime](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.servicelifetime/)  

#### Remarks

Constructor to specify the lifetime