---
title: What is Indago?
description: Indago is a compile-time assembly scanning library for .NET that replaces runtime reflection with Roslyn source generation.
badge:
    text: New
    variant: tip
tags:
    - getting-started
    - source-generator
    - aot
---

# What is Indago?

## The Problem: Runtime Reflection at Startup

Libraries like [Scrutor](https://github.com/khellang/Scrutor) make it easy to wire up dependency injection by scanning assemblies at startup:

```csharp
services.Scan(scan => scan
    .FromAssemblyOf<Program>()
    .AddClasses()
    .AsMatchingInterface());
```

This is convenient, but it comes with real costs:

- **Startup latency** — every call to `Assembly.GetTypes()` allocates and reflects over metadata at runtime.
- **AOT / IL trimming incompatibility** — the .NET Native AOT compiler and the IL trimmer cannot statically determine which types will be loaded via reflection, so they must either keep everything (bloating the binary) or trim too aggressively (causing runtime failures). `Assembly.GetTypes()` is flagged as trim-unsafe.
- **No compile-time errors** — a misconfigured selector silently produces wrong registrations; you only find out at runtime.

## The Solution: Compile-Time Scanning

Indago moves the scan to **build time**. A [Roslyn incremental source generator](https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview) reads the same fluent selector expressions you already write, evaluates them against the compilation's type graph, and emits a strongly-typed `IIndagoProvider` implementation whose methods return **pre-computed, hard-coded arrays** — no reflection required at runtime.

```
[Build time]
selector expression  ──►  Roslyn generator  ──►  IndagoProvider.g.cs
                                                   (pre-resolved type lists)

[Runtime]
IIndagoProvider.GetTypes(...)  ──►  returns the pre-built array  ──►  zero reflection
```

The generated file is checked into `obj/` and compiled into your assembly just like any other C# source file. The runtime cost of a scan drops to an array return.

## Key Concepts

### `IIndagoProvider` — the generated entry point

`IIndagoProvider` is the interface your code calls. It exposes three methods:

| Method                     | What it returns                                            |
| -------------------------- | ---------------------------------------------------------- |
| `GetAssemblies(selector)`  | Assemblies matched by the selector                         |
| `GetTypes(selector)`       | Types matched by the selector                              |
| `Scan(services, selector)` | Registers matched types directly into `IServiceCollection` |

The static property `IIndagoProvider.EntryAssembly` resolves the generated provider for the running entry assembly automatically. See [Quickstart](./quickstart/) for usage.

### Selector Expressions

Selectors are the fluent lambdas you pass to each method:

```csharp
provider.GetTypes(s =>
    s.FromAssemblyOf<Program>()
     .AddClasses()
     .AsImplementedInterfaces())
```

The generator captures the **text** of the lambda via `[CallerArgumentExpression]` and hashes it to produce a stable key. Each unique selector expression at each call site generates a distinct result set. Changing the selector — even whitespace-significantly — causes the generator to re-evaluate and re-emit.

### `IndagoProvider.ctpjson` — Cross-Assembly Cache

When the generator processes an assembly it writes a cache file (`IndagoProvider.ctpjson`) alongside the output. Downstream assemblies that reference yours can consume this cache instead of re-resolving all types from scratch, keeping incremental builds fast.

## Comparison to Scrutor

Scrutor walks your assemblies at runtime using `Assembly.GetTypes()`, `Type.IsAssignableTo()`, and similar reflection APIs. This is flexible and works in all .NET environments, but it cannot be used with Native AOT and incurs startup cost proportional to the number of types in scope.

Indago performs the same logical scan at **compile time** using Roslyn's type symbol graph. The runtime implementation is a generated class that returns a pre-computed array — there is no reflection, no assembly enumeration, and no trim warnings. The trade-off is that Indago requires a build step and that selector changes require a recompile (which is already true for all source-generated code).

## Next Steps

- [Installation](./installation/) — add Indago to your project in under 5 minutes

## Contributing to the Docs

If you are editing documentation, install the VS Code recommended extensions for the best authoring experience:

- **Astro** (`astro-build.astro-vscode`) — syntax highlighting and IntelliSense for `.astro` and `.mdx` files
- **Starlight Links** (`hideoo.starlight-links`) — link autocomplete and validation for `docs/src/content/docs/` paths

VS Code will prompt you to install these automatically when you open the `docs/` folder. Accept the prompt or install them via the Extensions panel.
