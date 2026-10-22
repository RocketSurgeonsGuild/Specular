---
title: Sample Applications
description: Worked examples of Specular compile-time scanning — three reusable libraries and the hosts that consume them, with the exact selector expressions.
---

# Sample Applications

The [`samples/`](https://github.com/RocketSurgeonsGuild/Specular/tree/main/samples) tree is a set of
worked examples that exercise Specular end-to-end. Three reusable **libraries** declare services in
different registration styles; **hosts** consume all three and assert — in process — that Specular
discovered exactly the expected service set, then publish under a zero-warning Native AOT guardrail.

You can read the selector expressions below to understand how scanning resolves **without running
anything** — that is the whole point of a compile-time provider.

## The three libraries

Each library is a plain class library that references `Specular`. Collectively they cover
interface-matching **and** attribute-based registration, three lifetimes, and one opt-out type.

| Library                        | Style                               | Lifetime(s)          | Notable                               |
| ------------------------------ | ----------------------------------- | -------------------- | ------------------------------------- |
| `Specular.Samples.Catalog`       | interface-matching                  | Singleton            | `IXxxService` → `XxxService`          |
| `Specular.Samples.Notifications` | attribute (`[ServiceRegistration]`) | Transient (`AsSelf`) | generic + non-generic attribute       |
| `Specular.Samples.Diagnostics`   | interface-matching                  | Scoped               | one `[ExcludeFromSampleScan]` opt-out |

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

Every host runs the same three `Scan` calls from
[`samples/hosts/Shared/SampleScan.cs`](https://github.com/RocketSurgeonsGuild/Specular/tree/main/samples/hosts/Shared/SampleScan.cs),
linked into each project so that each host's own generator resolves them, and compares the result with a hard-coded
**expected service set**. Each prints a `SPECULAR-SAMPLE: MATCH …` (or `MISMATCH …`) line.

| Host                          | Kind                          | Heads                                                    | Proves                                                                       |
| ----------------------------- | ----------------------------- | -------------------------------------------------------- | ---------------------------------------------------------------------------- |
| `Specular.Samples.Console`    | console                       | net10.0, net11.0                                         | exits non-zero on mismatch; zero-warning Native AOT publish                  |
| `Specular.Samples.Web`        | ASP.NET minimal API           | net10.0, net11.0                                         | fail-fast at startup on mismatch; `/services` lists discovery; Native AOT    |
| `Specular.Samples.BlazorWasm` | Blazor WebAssembly            | net10.0, net11.0                                         | scanning in the browser, incl. the net11.0 browser-wasm (no runtime async) build |
| `Specular.Samples.Uno`        | Uno Platform (Skia renderer)  | net10.0 desktop, WebAssembly, Android, iOS               | scanning in an Uno app on every Uno head                                     |
| `Specular.Samples.Maui`       | .NET MAUI                     | net10.0/net11.0 Android, iOS, Mac Catalyst, Windows      | scanning in a MAUI app, including the per-RID mobile heads                   |

The Uno and MAUI hosts need .NET workloads for most heads, so `Specular.slnx` doesn't build them; pick heads with
`SpecularSampleTargetFrameworks` (`;` escaped as `%3B` on the command line):

```bash
dotnet build samples/hosts/Specular.Samples.Maui -p:SpecularSampleTargetFrameworks=net10.0-android%3Bnet11.0-android
```

## How CI checks them

The **Samples** workflow (`.github/workflows/samples.yml`) runs on Linux, macOS and Windows. On each OS it builds the hosts,
runs them, and checks for the `MATCH` line with [`samples/smoke.mjs`](https://github.com/RocketSurgeonsGuild/Specular/tree/main/samples/smoke.mjs):

| Host                | Checked at runtime                                                                      | Build only                                     |
| ------------------- | --------------------------------------------------------------------------------------- | ---------------------------------------------- |
| Console, Web        | Native AOT binaries on every OS (Web: `GET /` must report the match)                    |                                                |
| Blazor WebAssembly  | published app in headless Chrome on every OS (the browser console must log the match)   |                                                |
| Uno                 | desktop head on every OS (`SPECULAR_SAMPLE_SMOKE=1` exits before a window opens); WebAssembly head in headless Chrome | Android, iOS                                   |
| MAUI                | Mac Catalyst head (`SPECULAR_SAMPLE_SMOKE=1` exits before UIKit starts)                  | Android, iOS, Windows                          |

To run the same check locally:

```bash
node samples/smoke.mjs exe -- samples/hosts/Specular.Samples.Console/bin/Debug/net10.0/Specular.Samples.Console
node samples/smoke.mjs browser samples/hosts/Specular.Samples.BlazorWasm/bin/Release/net10.0/publish/wwwroot
```
