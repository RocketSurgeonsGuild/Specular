---
title: Sample Applications
description: Worked examples of Indago compile-time scanning — three reusable libraries and the hosts that consume them, with the exact selector expressions.
---

# Sample Applications

The [`samples/`](https://github.com/RocketSurgeonsGuild/Indago/tree/main/samples) tree is a set of
worked examples that exercise Indago end-to-end. Three reusable **libraries** declare services in
different registration styles; **hosts** consume all three and assert — in process — that Indago
discovered exactly the expected service set, then publish under a zero-warning Native AOT guardrail.

You can read the selector expressions below to understand how scanning resolves **without running
anything** — that is the whole point of a compile-time provider.

## The three libraries

Each library is a plain class library that references `Indago`. Collectively they cover
interface-matching **and** attribute-based registration, three lifetimes, and one opt-out type.

| Library                        | Style                               | Lifetime(s)          | Notable                               |
| ------------------------------ | ----------------------------------- | -------------------- | ------------------------------------- |
| `Indago.Samples.Catalog`       | interface-matching                  | Singleton            | `IXxxService` → `XxxService`          |
| `Indago.Samples.Notifications` | attribute (`[ServiceRegistration]`) | Transient (`AsSelf`) | generic + non-generic attribute       |
| `Indago.Samples.Diagnostics`   | interface-matching                  | Scoped               | one `[ExcludeFromSampleScan]` opt-out |

## How a host scans them

A host calls `provider.Scan(services, selector)` once per library. Each selector is **hashed at the
call site** and resolved by the source generator at build time into concrete registrations — there is
no runtime reflection.

### 1. Interface-matching, Singleton (Catalog)

```csharp
provider.Scan(
    services,
    z => z.FromAssemblyOf<IProductService>()
          .AddClasses(f => f.InExactNamespaceOf<IProductService>())
          .AsMatchingInterface()
          .WithSingletonLifetime()
);
```

`AsMatchingInterface()` registers `ProductService` as `IProductService`, `CategoryService` as
`ICategoryService`, and so on.

### 2. Attribute-based, Transient as-self (Notifications)

```csharp
provider.Scan(
    services,
    z => z.FromAssemblyOf<NotificationDispatcher>()
          .AddClasses(f => f.WithAttribute<ServiceRegistrationAttribute>())
          .AsSelf()
          .WithTransientLifetime()
);
```

Only types carrying `[ServiceRegistration]` are registered, each as its own concrete type.

### 3. Interface-matching with an opt-out, Scoped (Diagnostics)

```csharp
provider.Scan(
    services,
    z => z.FromAssemblyOf<IHealthCheck>()
          .AddClasses(f => f.WithoutAttribute<ExcludeFromSampleScanAttribute>())
          .AsMatchingInterface()
          .WithScopedLifetime()
);
```

`WithoutAttribute<ExcludeFromSampleScanAttribute>()` is why the opt-out type **never** appears in the
discovered set — the host asserts its absence.

## The hosts

| Host                     | Kind                | Proves                                                                  |
| ------------------------ | ------------------- | ----------------------------------------------------------------------- |
| `Indago.Samples.Console` | console             | discovery + `IIndagoProvider.EntryAssembly`; exits non-zero on mismatch |
| `Indago.Samples.Web`     | ASP.NET minimal API | fail-fast at startup on mismatch; `/services` endpoint lists discovery  |

Both hosts hold a hard-coded **expected service set** and compare it to what Indago discovered,
failing the process (and therefore the CI smoke test) on any mismatch. Both are published under the
zero-warning Native AOT policy as discrete, named pipeline steps.
