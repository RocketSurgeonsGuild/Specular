# Feature Specification: Indago — Static Assembly Scanner for .NET

**Feature Branch**: `001-indago-static-assembly-scanner`

**Created**: 2026-06-25

**Status**: Draft

**Input**: User description: "This library is designed to be a static assembly scanner for .NET
that is fully AoT compatible. It uses .NET and Roslyn Source Generators. The documentation site
is a vitepress site for managing public documentation."

## User Scenarios & Testing _(mandatory)_

### User Story 1 — Scan Types at Build Time Without Runtime Reflection (Priority: P1)

A .NET library author wants to register services by scanning assemblies for types matching a set
of criteria (e.g., all classes implementing a given interface). Instead of walking assemblies at
startup with reflection, the author calls a fluent selector API; the Roslyn source generator
evaluates the selector at compile time and emits a strongly-typed, pre-resolved provider class.
The application starts up with zero reflection cost and the binary is fully compatible with .NET
Native AOT and IL trimming.

**Why this priority**: This is the entire reason Indago exists. Without it there is no product.
AOT/trim safety is a non-negotiable differentiator over Scrutor-style runtime scanners.

**Independent Test**: A consuming project can call `provider.GetTypes(s => ...)` and the
generated code contains a concrete, pre-computed list of matching types — confirmed by running
the project under `dotnet publish -r <rid>` with `PublishAot=true` without any trim warnings or
crashes.

**Acceptance Scenarios**:

1. **Given** a .NET library with classes implementing `IMyService`,
   **When** the author writes `provider.GetTypes(s => s.FromAssemblyOf<MyClass>().AddClasses().AsImplementedInterfaces())`,
   **Then** the source generator emits a strongly-typed class that returns exactly those
   implementations at runtime — with no reflection APIs in the generated output.

2. **Given** a consumer adds the `[ExcludeFromIndago]` attribute to a class,
   **When** the generator runs,
   **Then** that class is excluded from all scan results regardless of other filters.

3. **Given** a class is decorated with `[ServiceRegistration]` (default Singleton lifetime),
   **When** `AddIndagoServiceRegistrations(services)` is called,
   **Then** the class is registered in the DI container with Singleton lifetime without any
   runtime type-scanning.

---

### User Story 2 — Cross-Assembly Scan Caching (Priority: P2)

A downstream assembly wants to reuse type-scan results from a referenced library assembly
without redundant re-analysis at each build. The generator reads a cache file
(`IndagoProvider.ctpjson`) emitted by the upstream library's generator pass and uses it
directly rather than re-visiting all symbols.

**Why this priority**: Multi-project solutions would be impractical without caching; build times
and correctness both depend on it.

**Independent Test**: In a two-project solution (LibA → AppB), changing a class in LibA causes
AppB's generated code to update on the next build, while unchanged types in LibA are served from
the cache without re-visiting their symbols.

**Acceptance Scenarios**:

1. **Given** LibA emits `IndagoProvider.ctpjson` during its build,
   **When** AppB's generator runs,
   **Then** AppB resolves LibA's types from the cache file without re-inspecting LibA's source
   symbols, and the build log shows no redundant symbol visits for LibA types.

2. **Given** a type in LibA changes its implemented interfaces,
   **When** AppB rebuilds,
   **Then** AppB's generated scan results reflect the updated interface list (cache is
   invalidated by the `GeneratedHash` embedded in `IndagoProviderAttribute`).

---

### User Story 3 — Public Documentation Site (Priority: P3)

A developer new to Indago visits the VitePress documentation site to understand how to add
Indago to their project, write their first type selector, and publish an AOT-compatible binary.
The site provides a quickstart guide, API reference pages for each public type, and conceptual
guides explaining the generator pipeline and cross-assembly caching model.

**Why this priority**: Without comprehensible documentation the library is inaccessible to new
adopters, reducing its reach even if the implementation is correct.

**Independent Test**: A developer who has never used Indago can follow the quickstart guide
from a blank .NET project to a working, AOT-published binary entirely using the documentation
site — without reading source code.

**Acceptance Scenarios**:

1. **Given** a developer visits the docs home page on GitHub Pages,
   **When** they follow the "Getting Started" quickstart guide,
   **Then** they can add the Indago NuGet package, write a selector, and verify the generated
   code exists in their project in under 15 minutes.

2. **Given** a developer wants to understand the cross-assembly caching model,
   **When** they navigate to the "Architecture" or "How it works" section,
   **Then** they find a clear explanation of `IndagoProvider.ctpjson`, `GeneratedHash`, and the
   order in which assemblies are processed.

3. **Given** a developer wants to look up `ServiceRegistrationAttribute`,
   **When** they search or navigate to the API reference,
   **Then** they find the attribute's purpose, parameters, and a usage example.

---

### Edge Cases

- What happens when a selector expression references a type from a non-referenced assembly?
  The generator MUST emit a diagnostic error at build time, not a silent empty result.
- What happens when `IndagoProvider.ctpjson` is missing or corrupt for a referenced assembly?
  The generator MUST fall back to full symbol resolution and emit a build warning.
- What happens when two assemblies in the dependency graph have conflicting `GeneratedHash`
  values? The downstream generator must detect the mismatch and re-resolve rather than use a
  stale cache entry.
- What happens when a selector lambda contains a syntax form the generator cannot statically
  evaluate? The generator MUST emit a build error with a diagnostic pointing to the call site.

## Requirements _(mandatory)_

### Functional Requirements

- **FR-001**: The library MUST provide a Roslyn incremental source generator that evaluates
  type-selector expressions at compile time and emits a concrete `IIndagoProvider` implementation
  with pre-resolved type lists. The fluent selector API MUST support class and interface filters,
  namespace scoping, attribute-based opt-in/out, assignability checks (`AssignableTo<T>`), and
  type-kind filters (concrete, open-generic). Scrutor API gaps not yet covered by Indago are
  candidates for addition following a gap analysis during planning.
- **FR-002**: The generator MUST support `GetAssemblies()`, `GetTypes()`, and `Scan()` on
  `IIndagoProvider`, each keyed by a stable hash of the selector argument expression at the
  call site.
- **FR-003**: All generated code MUST be free of trim-unsafe reflection APIs and MUST produce
  zero trim warnings under `PublishAot=true`.
- **FR-004**: The library MUST provide `[ServiceRegistrationAttribute]` and
  `[RegistrationLifetimeAttribute]` for marking types for DI registration. `[RegistrationLifetimeAttribute]`
  MUST support Singleton, Scoped, and Transient lifetimes (mapping 1:1 to `ServiceLifetime`),
  with Singleton as the default when the attribute is omitted.
- **FR-005**: The library MUST provide `[ExcludeFromIndagoAttribute]` to opt individual types
  out of all scan results.
- **FR-006**: The generator MUST serialize scan results to `IndagoProvider.ctpjson` using
  source-generated JSON serialization so downstream assemblies can reuse them without
  re-resolving.
- **FR-007**: The generator MUST read `IndagoProvider.ctpjson` from referenced assemblies
  (via `AdditionalText`) and use it as a cache, validated against the `GeneratedHash` embedded
  in `[IndagoProviderAttribute]`.
- **FR-008**: The generator MUST compile and produce correct output on Roslyn 4.8, 4.14, and 5.0.
- **FR-009**: The library MUST expose `AddIndagoServiceRegistrations(IServiceCollection)` to
  register all types annotated with `[ServiceRegistration]` without runtime reflection.
- **FR-010**: The documentation site MUST include a quickstart guide, conceptual architecture
  explanation, and API reference for all public types in `src/Indago`.
- **FR-011**: A GitHub Actions workflow MUST build and deploy the VitePress documentation site
  to GitHub Pages automatically on every merge to the main branch.

### Key Entities

- **IIndagoProvider**: The generated provider interface; entry point for all scanning operations.
  Resolved at startup via `[IndagoProviderAttribute]` on the entry assembly.
- **TypeSelector / AssemblySelector**: Fluent builder objects passed to `GetTypes()` /
  `GetAssemblies()` / `Scan()` respectively; their expressions are hashed at the call site.
- **IndagoProvider.ctpjson**: The cross-assembly cache artifact; contains serialized scan
  results keyed by selector-expression hash.
- **ServiceRegistrationAttribute**: Marks a class for DI auto-registration; carries optional
  lifetime override.
- **ExcludeFromIndagoAttribute**: Opts a type out of all Indago scan results.

## Success Criteria _(mandatory)_

### Measurable Outcomes

- **SC-001**: A .NET application using Indago for DI registration publishes successfully with
  `PublishAot=true` on both `net8.0` and `net10.0` targets with zero trim warnings attributable
  to Indago.
- **SC-002**: The source generator completes incremental rebuilds (no source changes) in under
  100 ms on a standard developer machine for a solution with up to 5 projects.
- **SC-003**: A developer new to Indago can reach a working, AOT-published "hello world"
  integration in under 15 minutes following only the documentation quickstart served from the
  live GitHub Pages site.
- **SC-006**: The GitHub Pages deployment workflow completes and the live site reflects merged
  content within 10 minutes of a merge to the main branch.
- **SC-004**: 100% of public API surface changes are caught at build time by the public API
  tracking analyzer (`RS0017`), preventing accidental breaking changes from reaching a release.
- **SC-005**: All snapshot tests pass on Roslyn 4.8, 4.14, and 5.0 variants in CI, ensuring
  cross-version generator correctness.

## Clarifications

### Session 2026-06-25

- Q: Is automated deployment of the VitePress docs site in scope for this feature? → A: Yes.
  Content authoring and an automated deployment pipeline to GitHub Pages are both in scope.
- Q: What DI lifetimes does `[RegistrationLifetimeAttribute]` support? → A: Singleton, Scoped, and
  Transient — mapping 1:1 to the `ServiceLifetime` enum. Singleton is the default when the
  attribute is omitted.
- Q: What is the complete set of type filter/selector methods in scope for v1? → A: Class/interface
  filters, namespace scoping, attribute-based opt-in/out, assignability checks (`AssignableTo<T>`),
  and type-kind filters (concrete, open-generic) are all currently supported. Scrutor features not
  yet supported by Indago are candidates for inclusion and should be identified during planning.
- Q: Which linting tool should be used for `.vue`, `.ts/.mts`, and `.md` files in the docs tree? → A: oxlint.
  Use oxlint instead of ESLint. oxlint handles TypeScript and Vue rules natively via `--plugin vue`.
  Markdown fenced-block linting is out of scope for the linter (content quality is the author's
  responsibility; the VitePress build catches broken syntax).

## Assumptions

- The primary consumer is a .NET library or application author who wants AOT-safe DI registration
  without writing manual registration code.
- "Fully AOT compatible" means the library targets both `PublishAot=true` and IL trimming
  scenarios on .NET 8 and .NET 10; older framework targets (e.g., .NET Framework) are out of
  scope.
- The VitePress documentation site is the sole public documentation channel; no separate PDF,
  wiki, or third-party hosting is required for v1. The site is deployed to GitHub Pages via
  an automated GitHub Actions workflow triggered on merge to the main branch.
- Cross-assembly caching covers direct project references only; transitive cache traversal beyond
  one hop is a future enhancement.
- The library does not need to support dynamic assembly loading at runtime; all scanning is
  fully resolved at build time.
- The DI registration helper (`AddIndagoServiceRegistrations`) targets `Microsoft.Extensions.DependencyInjection.Abstractions`;
  no third-party DI container support is required for v1.
