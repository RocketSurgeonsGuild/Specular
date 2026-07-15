# Implementation Discoveries — 004-sample-applications

Stateful log of contracts, APIs, and decisions found during implementation.

## Specular consumer API (confirmed from `src/Specular`)

- A consuming assembly obtains its provider via `typeof(Program).Assembly.GetSpecularProvider()`
  (reads the generator-injected `SpecularHashAttribute`) or, on `net8.0+`,
  `ISpecularProvider.EntryAssembly`.
- `ISpecularProvider` surface: `GetAssemblies(action)`, `GetTypes(selector)`,
  `Scan(services, selector)`. Selector lambdas are resolved at compile time via
  `[CallerArgumentExpression]`; the generator runs on the **calling** assembly.
- Registration styles:
    - **Interface-matching**: `provider.Scan(s, z => z.FromAssemblyOf<T>().AddClasses().AsMatchingInterface().WithSingletonLifetime())`.
    - **Attribute-based**: types decorated with `[ServiceRegistration(lifetime, ...)]` /
      `[ServiceRegistration<TService>(lifetime)]`; the helper
      `services.AddSpecularServiceRegistrations(provider)` scans for them (registers `AsSelf`,
      Singleton by default — note: lifetime honoring depends on the helper; the attribute carries
      `Lifetime`).
    - Lifetimes via `WithSingletonLifetime()/WithScopedLifetime()/WithTransientLifetime()/WithLifetime(l)`.
- `ITypeFilter` (for `AddClasses(filter)`): `AssignableTo`, `WithAttribute<T>`,
  `WithoutAttribute<T>`, `InNamespaceOf<T>`, `EndsWith`, `KindOf`, etc.

## DEVIATION D-OPTOUT — `[ExcludeFromSpecular]` is assembly-only

The contract/tasks call for "exactly one **type** marked `[ExcludeFromSpecular]`". But
`Specular.Abstractions.ExcludeFromSpecularAttribute` is declared `[AttributeUsage(AttributeTargets.Assembly)]`
— it **cannot** decorate a type (would not compile). There is no type-level opt-out attribute in
the public API; type opt-out is expressed at the **call site** via the selector filter
(`AddClasses(f => f.WithoutAttribute<TMarker>())`).

**Decision**: Demonstrate the opt-out idiomatically — the `Diagnostics` library defines a sample
marker attribute `ExcludeFromSampleScanAttribute` on exactly one type; hosts exclude it via
`.AddClasses(f => f.WithoutAttribute<ExcludeFromSampleScanAttribute>())`, proving the type is absent
from the registered set. This satisfies the contract's intent (one opt-out type, proven excluded)
within the real API.

## Baseline fix (pre-existing, unrelated)

- `DataHelpers.cs:631` `MustBeAnExpressionException()` parameterless ctor violated CS8862 under the
  roslyn4.14/5.0 support variants (primary-ctor class needs `: this(...)`). Fixed by chaining
  `: this(Location.None, string.Empty)`. Solution build is green.

## Infra already present (Phase 1 partial)

- `samples/Directory.Build.props` (IsPackable=false, RS0017 dropped), `Directory.Build.workloads.props`
  (D1 guards: `SpecularSamplesMauiEnabled`, `SpecularSamplesWasmAotEnabled` default false), and
  `samples/Directory.Build.targets` (wires the Specular analyzer via `UsesSpecularGenerator=true`).
- Root `Directory.Packages.props` already has DI, AspNetCore.Components.WebAssembly, Playwright (T004).
- `Specular.slnx` has the `/samples/` folder with the two props files.

## PRE-EXISTING RED TEST BASELINE (not caused by this feature)

`dotnet test test/Specular.Tests` is **376/376 failing** — Verify snapshot drift. Cause: the
`feature/docs` branch has many **uncommitted analyzer changes** (CompiledTypeProviderGenerator.cs,
Configuration/_, AssemblyProviders/_ — all modified at session start, none by this feature) that
changed generated output, so it no longer matches the committed `.verified.cs` snapshots
(772 `.received.*` produced vs 752 `.verified.*`). Feature 004 adds **no** generator snapshot tests,
so this is orthogonal to the sample-applications work. NOT auto-accepting (constitution forbids
silent snapshot regeneration; the drift reflects the user's in-progress generator work). Needs the
user's decision: run `dotnet verify accept` to bless the current generator output, or finish the
analyzer changes first.

### Console-host selector constraints learned (RSGD0001)

- The generator's `ExtractSyntaxFromMethod` rejects **no-argument** `.AddClasses()` and `.GetTypes()`
  (throws `MustBeAnExpressionException` → RSGD0001). Always pass a filter lambda:
  `.AddClasses(f => f.InExactNamespaceOf<T>())`, `.GetTypes(f => f.AssignableTo<T>())`.
- `ISpecularProvider.GetTypes` selector uses `IReflectionTypeSelector` (terminal `.GetTypes(filter)`),
  NOT the `.AddClasses().AsMatchingInterface()` service-descriptor chain.

### NOTE — possible bug in `samples/Directory.Build.targets`

`AdditionalFiles Include` points at `$(IntermediateOutputPath)\ctp\SpecularProvider.ctpjson` but its
`Condition` checks `Exists('...\gap\SpecularProvider.ctpjson')` (`ctp` vs `gap` mismatch). Revisit if a
host fails to consume the cross-assembly cache.
