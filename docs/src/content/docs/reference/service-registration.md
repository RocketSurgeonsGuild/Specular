---
title: ServiceRegistrationAttribute
description: Mark classes for automatic DI registration using compile-time scanning.
---

# ServiceRegistrationAttribute

`ServiceRegistrationAttribute` is a declarative way to mark a class for DI registration without writing an explicit fluent selector. The Specular generator reads the attribute at build time and includes the type in the generated provider; no runtime reflection occurs.

## `[ServiceRegistration]`

```csharp
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ServiceRegistrationAttribute : Attribute
```

Applied to a class, it instructs the generator to register the class in the DI container. When applied without arguments, the type is registered as itself (`AsSelf`) with `ServiceLifetime.Singleton`.

**Constructors:**

```csharp
// Register as self, Singleton (default)
ServiceRegistrationAttribute()

// Register against explicit service types, Singleton
ServiceRegistrationAttribute(params Type[] serviceTypes)

// Register against explicit service types with a specified lifetime
ServiceRegistrationAttribute(ServiceLifetime lifetime, params Type[] serviceTypes)
```

**Properties:**

| Property       | Type                   | Description                                                          |
| -------------- | ---------------------- | -------------------------------------------------------------------- |
| `ServiceTypes` | `ImmutableArray<Type>` | The service types to register against. Empty means register as self. |
| `Lifetime`     | `ServiceLifetime`      | The DI lifetime. Defaults to `Singleton`.                            |

`AllowMultiple = true` means a single class can carry multiple `[ServiceRegistration]` attributes to be registered against different service types or with different lifetimes simultaneously.

## Generic overloads

The generic variants provide a concise way to specify one to four explicit service types without passing `typeof(...)` arguments.

### `ServiceRegistrationAttribute<TService>`

```csharp
[ServiceRegistration<IMyService>]
public class MyService : IMyService { }
```

Registers `MyService` as `IMyService`. Accepts an optional `ServiceLifetime` constructor argument (defaults to `Singleton`).

### `ServiceRegistrationAttribute<TService1, TService2>`

```csharp
[ServiceRegistration<IMyService, IAlternate>]
public class MyService : IMyService, IAlternate { }
```

Registers against two service types.

### `ServiceRegistrationAttribute<TService1, TService2, TService3>`

Registers against three service types.

### `ServiceRegistrationAttribute<TService1, TService2, TService3, TService4>`

Registers against four service types.

All generic variants accept an optional `ServiceLifetime` constructor argument:

```csharp
[ServiceRegistration<IMyService>(ServiceLifetime.Scoped)]
public class MyService : IMyService { }
```

## `[RegistrationLifetime]`

```csharp
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class RegistrationLifetimeAttribute : Attribute
```

Overrides the DI lifetime for a type independently of `[ServiceRegistration]`. Useful when you want to set a single lifetime on a base class or interface and have it apply across all registrations of that type.

```csharp
public RegistrationLifetimeAttribute(ServiceLifetime lifetime)
```

| Property   | Type              | Description              |
| ---------- | ----------------- | ------------------------ |
| `Lifetime` | `ServiceLifetime` | The overriding lifetime. |

## `AddSpecularServiceRegistrations`

```csharp
public static IServiceCollection AddSpecularServiceRegistrations(
    this IServiceCollection services,
    ISpecularProvider provider
)
```

Extension method on `IServiceCollection` that uses `provider.Scan()` to find all classes decorated with any `[ServiceRegistration]` variant and adds them to the container. The scan itself is resolved at build time; this call at startup simply replays the pre-built registrations.

**Usage:**

```csharp
// In Program.cs / Startup.cs
services.AddSpecularServiceRegistrations(SpecularProvider.Instance);
```

## Combined example

```csharp
// Scoped service registered as IMyService — lifetime from [ServiceRegistration] itself
[ServiceRegistration<IMyService>(ServiceLifetime.Scoped)]
public class MyService : IMyService { }

// Class carrying a separate lifetime override via [RegistrationLifetime]
[ServiceRegistration]
[RegistrationLifetime(ServiceLifetime.Scoped)]
public class AnotherService : IAnotherService { }

// Multiple registrations on one class
[ServiceRegistration<IMyService>]
[ServiceRegistration<IAlternate>(ServiceLifetime.Transient)]
public class DualService : IMyService, IAlternate { }
```

```csharp
// Wire up everything in one call
builder.Services.AddSpecularServiceRegistrations(SpecularProvider.Instance);
```

## Notes

- Registration is a **compile-time** operation. The generator discovers `[ServiceRegistration]`-decorated types during the build, not at application startup.
- Abstract classes and static classes are implicitly excluded — `AddClasses()` skips them by default.
- If `ServiceTypes` is empty (i.e. the attribute is applied with no type arguments), the class is registered as its own concrete type (`AsSelf`).

## See also

- [ISpecularProvider](./ispecular-provider) — the provider that backs `AddSpecularServiceRegistrations`
- [Type Filters](./type-filters) — the fluent alternative to attribute-based registration
- [ExcludeFromSpecularAttribute](./exclude-from-specular) — opt an assembly out of all scanning
