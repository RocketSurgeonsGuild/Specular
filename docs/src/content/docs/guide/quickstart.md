---
title: Quickstart
description: Write your first type selector and register services in under 5 minutes.
tags:
    - getting-started
    - di
---

# Quickstart

This page assumes you have already [installed Indago](../installation/) and applied `[assembly: IndagoProviderAttribute]` to your entry project.

## 1. Obtain the Provider

`IIndagoProvider.EntryAssembly` is a static property that resolves the generated provider for the currently running entry assembly. It works by reading the `IndagoProviderAttribute` the generator stamped onto your assembly at build time:

```csharp
using Indago;

IIndagoProvider provider = IIndagoProvider.EntryAssembly;
```

You can also inject the provider or store it in a variable — `IIndagoProvider` is a plain interface.

## 2. Scan for Types

Call `GetTypes()` with a fluent selector. The selector is a lambda whose text the generator captures at build time via `[CallerArgumentExpression]`:

```csharp
IEnumerable<Type> types = provider.GetTypes(s =>
    s.FromAssemblyOf<Program>()
     .AddClasses()
     .AsImplementedInterfaces());
```

The full selector fluent API is covered in the Reference section. Common starting points:

| Selector step                     | What it does                                       |
| --------------------------------- | -------------------------------------------------- |
| `FromAssemblyOf<T>()`             | Scope the scan to the assembly containing `T`      |
| `FromAssemblies()`                | Include all loaded assemblies                      |
| `FromAssemblyDependenciesOf<T>()` | Include assemblies referenced by `T`'s assembly    |
| `AddClasses()`                    | Filter to concrete (non-abstract) classes          |
| `GetTypes()`                      | Return all matched types without further filtering |

## 3. Register with Dependency Injection

### Option A — `Scan()` directly into `IServiceCollection`

`IIndagoProvider.Scan()` combines the scan and registration in one call:

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);
var provider = IIndagoProvider.EntryAssembly;

provider.Scan(builder.Services, s =>
    s.FromAssemblyOf<Program>()
     .AddClasses()
     .AsImplementedInterfaces()
     .WithSingletonLifetime());
```

### Option B — `AddIndagoServiceRegistrations()`

`AddIndagoServiceRegistrations` is an `IServiceCollection` extension method that reads `[ServiceRegistration]` attributes applied directly to classes and registers them according to their declared lifetime (default: Singleton):

```csharp
// On your service class:
[ServiceRegistration<IMyService>]
public class MyService : IMyService { }

// In Program.cs:
builder.Services.AddIndagoServiceRegistrations(provider);
```

The signature is:

```csharp
public static IServiceCollection AddIndagoServiceRegistrations(
    this IServiceCollection services,
    IIndagoProvider provider);
```

## 4. What the Generated Code Looks Like

Conceptually, the generator emits something like this (simplified):

```csharp
// IndagoProvider.g.cs  — auto-generated, do not edit
internal sealed class IndagoProvider : IIndagoProvider
{
    public IEnumerable<Type> GetTypes(
        Func<IReflectionTypeSelector, IEnumerable<Type>> selector,
        int lineNumber = 0,
        string filePath = "",
        string argumentExpression = "")
    {
        // Hash of "s => s.FromAssemblyOf<Program>().AddClasses().AsImplementedInterfaces()"
        return argumentExpression switch
        {
            "abc123==" => new[] { typeof(MyService), typeof(OtherService) },
            // ... other call sites
            _ => Enumerable.Empty<Type>()
        };
    }
    // ...
}
```

There are no calls to `Assembly.GetTypes()`, no reflection, and no dictionary lookups over runtime metadata. Every result is a compile-time constant array.

## 5. Verifying the Registration

Add a quick sanity check after `Build()`:

```csharp
var app = builder.Build();
var svc = app.Services.GetRequiredService<IMyService>(); // throws if not registered
```

## Next Steps

- [AOT Publishing](../aot-publishing/) — publish with Native AOT and zero trim warnings
- Reference section — full selector API documentation
