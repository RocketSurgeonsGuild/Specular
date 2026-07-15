---
title: RegistrationLifetimeAttribute Class
slug: api/specular.registrationlifetimeattribute
sidebar:
  label: RegistrationLifetimeAttribute
editUrl: false
description: Defines the lifetime that should be used service registrations created using the Specular.ISpecularProvider
---
## Definition

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](../specular.ispecularprovider/)

```csharp title="C#"
[AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface)]
public class RegistrationLifetimeAttribute : Attribute
```

Inheritance [object](https://learn.microsoft.com/dotnet/api/system.object/) → [Attribute](https://learn.microsoft.com/dotnet/api/system.attribute/)
## Remarks

Constructor to specify the lifetime

## Constructors

### RegistrationLifetimeAttribute(ServiceLifetime)

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](../specular.ispecularprovider/)

```csharp title="C#"
public RegistrationLifetimeAttribute(ServiceLifetime lifetime)
```

#### Parameters

`lifetime` [ServiceLifetime](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.servicelifetime/)  

#### Remarks

Constructor to specify the lifetime

## Properties

### Lifetime

The lifetime

```csharp title="C#"
public ServiceLifetime Lifetime { get; }
```