# KeyedServiceRegistrationAttribute

# KeyedServiceRegistrationAttribute

`KeyedServiceRegistrationAttribute` is the keyed-service counterpart to [`ServiceRegistrationAttribute`](./service-registration) — it behaves the same way, except every registration it produces is a
[keyed service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services) (`ServiceDescriptor.KeyedSingleton`, `KeyedScoped`, `KeyedTransient`) resolved with `IServiceProvider.GetKeyedService`/`GetRequiredKeyedService`. Like `ServiceRegistrationAttribute`, the Specular generator reads the attribute at build time; no runtime reflection occurs.

## `[KeyedServiceRegistration]`

```csharp
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class KeyedServiceRegistrationAttribute : KeyedServiceRegistrationBaseAttribute
```

Applied to a class, it instructs the generator to register the class under the given key. If no explicit service types are given, the type is registered — under that same key — against itself and every interface it implements.

**Constructor:**

```csharp
KeyedServiceRegistrationAttribute(object? key, ServiceLifetime lifetime, params Type[] serviceTypes)
```

**Properties:**

| Property       | Type                   | Description                                                                                  |
| -------------- | ---------------------- | ---------------------------------------------------------------------------------------------- |
| `Key`          | `object?`              | The service key. Must be a compile-time constant (string, number, bool, char, enum, `Type`, or `null`). |
| `ServiceTypes` | `ImmutableArray<Type>` | The service types to register against. Empty means register against all implemented interfaces (plus self). |
| `Lifetime`     | `ServiceLifetime`      | The DI lifetime. Always required.                                                             |

`AllowMultiple = true` means a single class can carry multiple `[KeyedServiceRegistration]` attributes — for different keys, different service types, or different lifetimes.

Two registrations are only considered duplicates (and raise a diagnostic) when both the **service type and the key** match. The same service type may be registered any number of times under distinct keys:

```csharp
[KeyedServiceRegistration("primary", ServiceLifetime.Singleton, typeof(ICache))]
[KeyedServiceRegistration("fallback", ServiceLifetime.Singleton, typeof(ICache))]
public class RedisCache : ICache { }
```

## Generic overloads

The generic variants provide a concise way to specify one to eight explicit service types without passing `typeof(...)` arguments.

### `KeyedServiceRegistrationAttribute<TService>`

```csharp
[KeyedServiceRegistration<IMyService>("my-key", ServiceLifetime.Scoped)]
public class MyService : IMyService { }
```

Registers `MyService` as `IMyService` under `"my-key"`.

### `KeyedServiceRegistrationAttribute<TService1, TService2>` through `<..., TService8>`

Registers against up to eight explicit service types, all under the same key. Every generic variant requires both a `key` and a `ServiceLifetime` constructor argument — neither has a default.

## Interaction with `[RegistrationLifetime]`

[`RegistrationLifetimeAttribute`](./service-registration#registrationlifetime) can still override the resolved lifetime for an individual service type, keyed or not. Priority order (highest first):

1. `[RegistrationLifetime]` on the specific service type being registered.
2. The lifetime declared on `[KeyedServiceRegistration]` itself.
3. `[RegistrationLifetime]` on the decorated class.
4. `ServiceLifetime.Singleton`.

```csharp
[RegistrationLifetime(ServiceLifetime.Scoped)]
public interface IService { }

// Registered as itself (Singleton, from the attribute) but as IService (Scoped, from the override)
[KeyedServiceRegistration("service-key", ServiceLifetime.Singleton, typeof(IService))]
public class Service : IService { }
```

## `AddSpecularServiceRegistrations`

`AddSpecularServiceRegistrations` (see [ServiceRegistrationAttribute](./service-registration#addspecularserviceregistrations)) scans for every `[ServiceRegistration]` **and** `[KeyedServiceRegistration]` variant in one pass — no separate call is needed for keyed types.

```csharp
builder.Services.AddSpecularServiceRegistrations(SpecularProvider.Instance);
```

## Resolving keyed services

Registrations produced by `[KeyedServiceRegistration]` are resolved the normal keyed-DI way:

```csharp
public class Consumer([FromKeyedServices("primary")] ICache cache) { }

// or
var cache = serviceProvider.GetRequiredKeyedService<ICache>("primary");
```

## Notes

- Registration is a **compile-time** operation. The generator discovers `[KeyedServiceRegistration]`-decorated types during the build, not at application startup.
- Abstract classes and static classes are implicitly excluded.
- A keyed registration and a non-keyed `[ServiceRegistration]` for the same service type on the same class coexist without conflict — keyed and non-keyed services occupy independent slots in the container.

## See also

- [ServiceRegistrationAttribute](./service-registration) — the non-keyed counterpart to this attribute
- [ISpecularProvider](./ispecular-provider) — the provider that backs `AddSpecularServiceRegistrations`
- [Type Filters](./type-filters) — the fluent alternative to attribute-based registration