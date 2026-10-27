# ServiceRegistrationAttribute

# ServiceRegistrationAttribute

`ServiceRegistrationAttribute` is a declarative way to mark a class for DI registration without writing an explicit fluent selector. The Specular generator reads the attribute at build time and includes the type in the generated provider; no runtime reflection occurs.

## `[ServiceRegistration]`

```csharp
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ServiceRegistrationAttribute : Attribute
```

Applied to a class, it instructs the generator to register the class in the DI container. The lifetime is always required; if no explicit service types are given, the type is registered against every interface it implements (plus itself).

**Constructors:**

```csharp
// Register against explicit service types with a required lifetime
ServiceRegistrationAttribute(ServiceLifetime lifetime, params Type[] serviceTypes)
```

**Properties:**

| Property       | Type                   | Description                                                          |
| -------------- | ---------------------- | -------------------------------------------------------------------- |
| `ServiceTypes` | `ImmutableArray<Type>` | The service types to register against. Empty means register against all implemented interfaces (plus self). |
| `Lifetime`     | `ServiceLifetime`      | The DI lifetime. Always required.                                   |

`AllowMultiple = true` means a single class can carry multiple `[ServiceRegistration]` attributes to be registered against different service types or with different lifetimes simultaneously.

## Generic overloads

The generic variants provide a concise way to specify one to eight explicit service types without passing `typeof(...)` arguments.

### `ServiceRegistrationAttribute<TService>`

```csharp
[ServiceRegistration<IMyService>(ServiceLifetime.Scoped)]
public class MyService : IMyService { }
```

Registers `MyService` as `IMyService` with the given lifetime.

### `ServiceRegistrationAttribute<TService1, TService2>`

```csharp
[ServiceRegistration<IMyService, IAlternate>(ServiceLifetime.Scoped)]
public class MyService : IMyService, IAlternate { }
```

Registers against two service types.

### `ServiceRegistrationAttribute<TService1, TService2, TService3>` through `<..., TService8>`

Registers against up to eight explicit service types. Every generic variant requires a `ServiceLifetime` constructor argument — there is no default.

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
[ServiceRegistration(ServiceLifetime.Singleton)]
[RegistrationLifetime(ServiceLifetime.Scoped)]
public class AnotherService : IAnotherService { }

// Multiple registrations on one class
[ServiceRegistration<IMyService>(ServiceLifetime.Singleton)]
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
- If `ServiceTypes` is empty (i.e. the attribute is applied with no explicit type arguments), the class is registered against itself and every interface it implements.

## See also

- [ISpecularProvider](./ispecular-provider) — the provider that backs `AddSpecularServiceRegistrations`
- [KeyedServiceRegistrationAttribute](./keyed-service-registration) — the keyed-service counterpart to this attribute
- [Type Filters](./type-filters) — the fluent alternative to attribute-based registration
- [ExcludeFromSpecularAttribute](./exclude-from-specular) — opt an assembly out of all scanning