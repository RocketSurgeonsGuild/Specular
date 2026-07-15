---
title: SpecularProviderServiceCollectionExtensions Class
slug: api/specular.specularproviderservicecollectionextensions
sidebar:
  label: SpecularProviderServiceCollectionExtensions
editUrl: false
description: Extension methods for the service collection using the compiled type provider
---
## Definition

Extension methods for the service collection using the compiled type provider

```csharp title="C#"
public static class SpecularProviderServiceCollectionExtensions
```

Inheritance [object](https://learn.microsoft.com/dotnet/api/system.object/)

## Methods

### AddSpecularServiceRegistrations(IServiceCollection, ISpecularProvider)

Adds all the services with the [ServiceRegistrationAttribute](../specular.serviceregistrationattribute/) to the service collection

```csharp title="C#"
public static IServiceCollection AddSpecularServiceRegistrations(this IServiceCollection services, ISpecularProvider provider)
```

#### Parameters

`services` [IServiceCollection](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection/)  

`provider` [ISpecularProvider](../specular.ispecularprovider/)  

#### Returns

[IServiceCollection](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection/)