# Implementation Plan: Specular.Scanning — shared compile-time scanning for source generators

**Branch**: `claude/specular-scanning-library` | **Date**: 2026-11-24 | **Status**: Phase 0 (scaffolding) done; Phase 1 next
**Baseline**: all file and API references verified against `origin/main` @ `6aa7d2eef`

## Summary

Extract the compile-time assembly/type scanning and its build cache out of `Specular.Analyzers` into a reusable
library, `Specular.Scanning`, so other source generators can scan referenced assemblies and the current compilation with
the same capabilities, and cache those results across builds. Generator authors describe scans with a symbol-based
selector API that mirrors Specular's runtime selectors (`IReflectionAssemblySelector` → `IReflectionTypeSelector` →
`ITypeFilter`), so they never construct filter types by hand. `Specular.Analyzers` becomes the first consumer.

## Agreed design decisions

| Area               | Decision                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |
| ------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Packages           | `Specular.Scanning` (public DLL) and `Specular.Scanning.Sources` (same code as `internal` source). Namespace `Specular.Scanning`.                                                                                                                                                                                                                                                                                                                                                                                                                                                           |
| Roslyn targeting   | Same matrix as the analyzers (5.0, 5.3, 5.6, 5.9, 5.12) via the `.supports` pattern. Package layout: `lib/netstandard2.0` (from the 5.0 build, the compile reference) + `roslyn/roslynX.Y/` variants; the pack helper copies the matching variant into the consumer's `analyzers/dotnet/roslynX.Y/cs`. Nothing under `analyzers/` in our own package.                                                                                                                                                                                                                                       |
| Specular's own use | `Specular.Analyzers` (and its `.supports` builds) compile the library **as sources**, so Specular never loads a separate `Specular.Scanning.dll` that could collide with another generator's copy.                                                                                                                                                                                                                                                                                                                                                                                          |
| Authoring API      | `IAssemblySymbolSelector` / `ITypeSymbolSelector` / `ITypeSymbolFilter`: 1:1 mirror of the runtime selectors. `Type` → `ITypeSymbol`, no generic `<T>` overloads, plus `IAssemblySymbol` overloads (`FromAssembly`, `NotFromAssembly`) and metadata-name string overloads.                                                                                                                                                                                                                                                                                                                  |
| Filter scope       | Assembly filters (all, system, specific, not, dependencies-of) and type filters (assignable/any, not-assignable, name, namespace, with/without attribute, kind, info, public-only). Service descriptors and lifetimes stay in Specular.                                                                                                                                                                                                                                                                                                                                                     |
| Custom filters     | Allowed; each must supply a stable `CacheKey` that is folded into the request hash.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |
| Request model      | The selector builds a public, equatable, read-only `ScanRequest` (metadata names + filter data). It is the incremental-pipeline value and the cache key.                                                                                                                                                                                                                                                                                                                                                                                                                                    |
| Execution          | Two-step. `ScanRequest.Build(compilation, s => …)` then either `Scanner.Run(compilation, request)` (sync → symbols) or `requestProvider.Scan(consumerId)` (incremental → `IncrementalValueProvider<ScanResult>`).                                                                                                                                                                                                                                                                                                                                                                           |
| Results            | One `ScanResult` shape for both scan kinds: equatable `ScannedAssembly` / `ScannedType` records, `Resolve(Compilation)` back to symbols, `ScannedType.IsAccessible` (relative to the compilation), `ScannedAssembly.HasInaccessibleMatches`, plus per-request data needed by Specular's ScanReport.                                                                                                                                                                                                                                                                                         |
| Assembly selection | Fixed in this work (was: every clause ANDed, so `FromAssemblyOf<A>().FromAssemblyOf<B>()` matched nothing). Specific assemblies and dependencies-of are alternatives (A or B; A or dependents of C); `FromAssemblies()` is the starting point they narrow, so it selects everything only without a specific selection (`FromAssemblies().DependenciesFromAssemblyOf<T>()` still means "dependents of T"); `NotFrom…` always excludes. Phase 6 changes `Specular.Analyzers`' `CompiledAssemblyFilter` to match; any snapshot that changes is updated deliberately and gets a CHANGELOG note. |
| Scan report        | Data lives in results; generating the `ScanReport` class stays in `Specular.Analyzers`.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
| Cache shape        | Generalized file cache (no store interface). One file per consumer: `obj/ctp/<ConsumerId>.ctpjson`. Specular uses consumer ID `SpecularProvider`, preserving today's file name.                                                                                                                                                                                                                                                                                                                                                                                                             |
| Cache keys         | Per (request hash, referenced assembly). Valid while the assembly MVID matches. Only `PortableExecutableReference`s with a readable MVID are disk-cached; anything else (e.g. IDE `CompilationReference`s) is always rescanned (R1, accepted). The compilation's own assembly is never cached.                                                                                                                                                                                                                                                                                              |
| Cache versioning   | Schema version only. Mismatch or any read failure ⇒ file ignored and rebuilt; never throws.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| Typed store        | Public `Get/Set<T>(key, IAssemblySymbol)` on the same file, MVID-validated, serialized via a consumer-supplied `JsonTypeInfo<T>` and a consumer-supplied data version. Specular's `[AssemblyMetadata]` request propagation (stays Specular-only) is rebuilt on it.                                                                                                                                                                                                                                                                                                                          |
| Cache writes       | Still written from the generator; the single `RS1035` suppression lives in the library's cache writer.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      |
| MSBuild wiring     | One reusable targets file: adds `$(IntermediateOutputPath)ctp/*.ctpjson` to `AdditionalFiles` in a target before `CoreCompile` (matching Specular's current timing) and deletes them on Clean; import-guarded. A pack helper places it in each consumer generator's `build`/`buildTransitive`.                                                                                                                                                                                                                                                                                              |
| Compatibility bar  | `test/Specular.Tests` snapshots (`#SpecularProvider.g.verified.cs` and `#SpecularScanReport.g.verified.cs`) stay byte-identical. The `.ctpjson` format may change.                                                                                                                                                                                                                                                                                                                                                                                                                          |
| Out of scope       | Generic cross-assembly scan propagation; an `IScanCache` store abstraction; a syntax/lambda parser in the library.                                                                                                                                                                                                                                                                                                                                                                                                                                                                          |

## Technical context

- **Language/targets**: C# preview, `netstandard2.0`, `EnforceExtendedAnalyzerRules`, Polyfill for BCL gaps.
- **Dependencies**: `Microsoft.CodeAnalysis.CSharp` (per matrix, `PrivateAssets=all`), `System.Text.Json` (source-gen
  context only), `System.Collections.Immutable`. `.Sources` consumers reference System.Text.Json themselves.
- **Testing**: TUnit + `TUnit.Assertions.Should` + Verify.TUnit (as `test/Directory.Build.targets` wires up); in-memory `CSharpCompilation`s (via
  `Rocket.Surgery.Extensions.Testing.SourceGenerators`, as `Specular.Tests` does) plus `test/TestAssembly` as a
  metadata reference.
- **Existing code being extracted** (all in `src/Specular.Analyzers`): `AssemblyProviders/*` (visitors,
  `CompiledAssemblyFilter`, `CompiledTypeFilter`, `FindType*`), `Descriptors/*` (assembly + type-filter descriptors
  only), the type/assembly halves of `Configuration/*Data.cs`, `Configuration/AssemblySymbolExtensions.cs`, and the cache
  plumbing in `CompiledTypeProviderGenerator.cs` / `GeneratedAssemblyProviderData` / `ResultingAssemblyProviderData`.

## Constitution check

| Principle                 | Status | Notes                                                                                                                                                                                                                                                        |
| ------------------------- | ------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| I. AOT & trim safety      | ✅     | Library is analyzer-side only; no runtime reflection. All cache JSON goes through a source-generated `JsonSerializerContext`; the typed store requires `JsonTypeInfo<T>`. Specular keeps `SpecularProvider.ctpjson`.                                         |
| II. Test-first, snapshots | ✅     | Every phase starts with failing tests. Existing `Specular.Tests` snapshots are the regression net and must not change.                                                                                                                                       |
| III. Minimal public API   | ⚠️     | `src/Specular`'s surface is untouched. `Specular.Scanning` adds a new public surface. The repo disables PublicAPI tracking (`RocketSurgeryPublicApiAnalyzers=false`), so no `PublicAPI.*.txt` files. Custom filters and the typed store are justified below. |
| IV. Strict analysis       | ✅     | New projects inherit repo analyzers; `netstandard2.0` explicit usings.                                                                                                                                                                                       |
| V. Docs                   | ✅     | XML docs on all public API; new docs page + CHANGELOG entry (Phase 7).                                                                                                                                                                                       |
| Roslyn matrix             | ✅     | Library builds per Roslyn variant; no APIs newer than 5.0 without guards.                                                                                                                                                                                    |

### Complexity tracking

| Addition                       | Why it is needed                                                                                  | Simpler alternative rejected because                                                  |
| ------------------------------ | ------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------- |
| Custom filters with `CacheKey` | Third-party generators need predicates beyond Specular's built-ins (explicit requirement).        | Post-filtering results works but forces every consumer to re-resolve symbols.         |
| Public typed cache store       | Specular's `[AssemblyMetadata]` propagation needs per-assembly cached data; concrete in-repo use. | A second Specular-only cache file duplicates the MVID/versioning/IO logic.            |
| Dual DLL + Sources packaging   | DLL for third parties; Sources so Specular itself can't hit cross-generator version collisions.   | DLL-only risks load collisions; Sources-only forces every consumer to carry the code. |

## Project structure

```text
src/
  Specular.Scanning/                     # main build (Roslyn 5.12), packs DLL per roslyn folder + targets + pack helper
    Selectors/                           # IAssemblySymbolSelector, ITypeSymbolSelector, ITypeSymbolFilter + impls
    Requests/                            # ScanRequest, AssemblyFilterData, TypeFilterData, IScanTypeFilter (custom)
    Scanning/                            # Scanner, visitors, compiled filters (moved from Specular.Analyzers)
    Results/                             # ScanResult, ScannedAssembly, ScannedType, Resolve
    Caching/                             # ScanCacheFile, typed store, MVID keys, JSON context, cache writer
    Incremental/                         # Scan(consumerId) IncrementalValueProvider extensions
    build/Specular.Scanning.targets      # reusable ctp/*.ctpjson AdditionalFiles + Clean wiring (import-guarded)
    build/Specular.Scanning.Pack.targets # helper consumer generators import to bundle DLL + targets into their nupkg
  Specular.Scanning.supports/            # roslyn5.0/5.3/5.6/5.9 variants (mirrors Specular.Analyzers.supports)
  Specular.Scanning.Sources/             # packs the .cs files as contentFiles, internal via SPECULAR_SCANNING_SOURCES
  Specular.Analyzers/                    # consumes Specular.Scanning sources; keeps parser, service descriptors, report
test/
  Specular.Scanning.Tests/               # TUnit unit + incremental + cache tests
samples/
  Specular.Scanning.SampleGenerator/     # third-party-style consumer proving the DLL + pack path end to end
specs/001-specular-scanning/plan.md      # this file
```

**Visibility switch**: library sources declare `public` types guarded by a single convention
(`#if SPECULAR_SCANNING_SOURCES internal #else public #endif` on each top-level type). The `.Sources` package and
`Specular.Analyzers` define `SPECULAR_SCANNING_SOURCES`.

## Phases

**Phase 0 status — done.** `src/Specular.Scanning`, `src/Specular.Scanning.supports` (roslyn5.0–5.9),
`src/Specular.Scanning.Sources`, `test/Specular.Scanning.Tests` exist and are in `Specular.slnx`; all build with 0
warnings. `Specular.Scanning` packs to the layout above. `Specular.Scanning.Sources` packs only once the library has
`.cs` files (NU5017 on an empty library) — its `contentFiles` layout is verified in Phase 1. The test project has no
tests yet, so `dotnet test` on it fails until Phase 1 adds the first ones.

Each phase is test-first: write the tests, confirm they fail, then implement. Phases 1–5 add the library without
touching `Specular.Analyzers` behaviour; Phase 6 is the migration.

### Phase 0 — Scaffolding

1. Create `src/Specular.Scanning` (`netstandard2.0`, `IsRoslynComponent`, `EnforceExtendedAnalyzerRules`), add to `Specular.slnx`.
2. Create `src/Specular.Scanning.supports/` with roslyn5.0/5.3/5.6/5.9 variants, copying the `Specular.Analyzers.supports` `Directory.Build.props/targets` pattern (compile `../Specular.Scanning/**/*.cs`, `RunAnalyzers=false`).
3. Pack layout: DLL + `System.Text.Json.dll` per `analyzers/dotnet/roslynX.Y/cs`; `build/` targets.
4. Create `src/Specular.Scanning.Sources` (contentFiles `cs/any`, defines `SPECULAR_SCANNING_SOURCES` via its `build/` props).
5. Create `test/Specular.Scanning.Tests` (TUnit, references `TestAssembly`).
6. ~~Add PublicAPI tracking files~~ — dropped: the repo disables PublicAPI tracking.
7. Gate: `dotnet build` of the solution and `dotnet build src/Specular.Scanning.supports/` succeed.

### Phase 1 — Request model and selector API

1. Tests: building the same selection twice yields equal `ScanRequest`s with identical hashes; symbol overloads and metadata-name overloads produce equal requests; each `ITypeSymbolFilter` method maps to the expected filter data; custom filter `CacheKey` changes the hash.
2. Implement `ScanRequest` (+ `AssemblyFilterData`, `TypeFilterData`, scan kind) as equatable records with a stable hash.
3. Implement `IAssemblySymbolSelector` / `ITypeSymbolSelector` / `ITypeSymbolFilter` mirroring `src/Specular/Abstractions/I*Selector.cs` and `ITypeFilter.cs`, including `TypeKindFilter`/`TypeInfoFilter` equivalents.
4. Implement `ScanRequest.Build(compilation, …)` for assembly scans and type scans.

### Phase 2 — Scanner core (sync, symbols)

1. Tests against in-memory compilations referencing `TestAssembly`: each assembly filter, each type filter, public-only, system-assembly exclusion, dependencies-of, combinations; parity cases copied from `GetTypesTestsData`.
2. Move `TypeSymbolVisitor*`, `CompiledAssemblyFilter`, `CompiledTypeFilter`, `FindType*` and the assembly/type descriptors into the library, driven by `ScanRequest` instead of parsed syntax.
3. Implement `Scanner.Run(compilation, request)` returning assemblies and `INamedTypeSymbol`s.

### Phase 3 — Results and incremental API

1. Tests: `ScanResult` equality across unchanged recompiles; `Resolve` round-trips; `IsAccessible` / `HasInaccessibleMatches` for internal types in referenced assemblies; incremental step-tracking tests showing outputs are `Cached`/`Unchanged` when only unrelated syntax changes.
2. Implement `ScannedAssembly`, `ScannedType`, `ScanResult` (including the report data Specular needs), `Resolve(Compilation)`.
3. Implement `IncrementalValueProvider<ScanRequest>.Scan(consumerId)` (and the `IncrementalValuesProvider` variant).

### Phase 4 — Cache

1. Tests: per-consumer file name; hit when MVID unchanged; miss when MVID changes; compilation assembly never cached; schema mismatch and corrupt JSON ignored without exceptions; typed store round-trip with data version invalidation; two consumers do not read each other's files.
2. Implement `ScanCacheFile` (schema version, per-request/per-assembly entries, typed-store section) with a source-generated JSON context.
3. Implement MVID key resolution (`PortableExecutableReference` → module MVID) with the fallback rules from Risk R1.
4. Wire reading (from `AdditionalTextsProvider` filtered by consumer ID) and writing (single `RS1035`-suppressed writer using `build_property.IntermediateOutputPath`/`ProjectDir`) into `Scan(consumerId)`.

### Phase 5 — MSBuild wiring and third-party path

1. Write `build/Specular.Scanning.targets` (glob, `BeforeTargets="CoreCompile"`, Clean target, import guard) and `Specular.Scanning.Pack.targets`.
2. Add `samples/Specular.Scanning.SampleGenerator` (+ a tiny consuming project) using the DLL package path; verify the cache file is produced, fed back on the second build, and cleaned.
3. Verify a project referencing both the sample generator and Specular gets two independent cache files.

### Phase 6 — Migrate Specular.Analyzers

1. Include `Specular.Scanning` sources in `Specular.Analyzers` and its `.supports` projects with `SPECULAR_SCANNING_SOURCES`; delete the moved files from `Specular.Analyzers`.
2. Map `DataHelpers`' parsed selector output onto `ScanRequest` (service-descriptor parts stay local).
3. Change `CompiledAssemblyFilter` to the agreed assembly-selection rule (see decisions table); update affected snapshots deliberately and note it in the CHANGELOG.
4. Rebuild the cross-assembly `[AssemblyMetadata]` propagation and per-call-site cache on the typed store with consumer ID `SpecularProvider`.
5. Replace Specular's own `AddSpecularCache` / `CleanSpecularCache` targets with the shared targets (keeping the `SpecularProvider.ctpjson` name).
6. Gate: all `test/Specular.Tests` snapshots unchanged except the deliberate assembly-selection updates (`git diff --exit-code test/Specular.Tests/snapshots`), `NativeAotTests` pass, `.supports` builds pass.

### Phase 7 — Docs and release notes

1. XML docs on every public type/member.
2. New docs page (`docs/src/content/docs/guide/` — "Scanning from your own source generator") covering the selector API, two-step execution, caching, consumer IDs and packaging (DLL vs Sources).
3. Architecture page update for the cache shape change; CHANGELOG entry.

## Verification gates (from the constitution)

- `dotnet test` (all projects) green; `Specular.Tests` snapshots byte-identical.
- `dotnet build src/Specular.Analyzers.supports/` and `dotnet build src/Specular.Scanning.supports/` green.
- AOT tests pass on `net10.0` and `net11.0`.
- PublicAPI files updated; `prettier` + `dotnet format` clean; NuGet audit clean.

## Risks and refinements

- **R1 — MVID fallback for project references in the IDE (accepted).** In the IDE, project references arrive as
  `CompilationReference`s with no MVID, and their identity version rarely changes between edits, so an
  identity-version fallback would serve stale results. Decision: only `PortableExecutableReference`s with a readable
  MVID are disk-cached; anything else is always rescanned (incremental caching still applies).
- **R2 — Byte-identical snapshots.** Type and assembly ordering in Specular's output depends on today's visitor order;
  the library must preserve deterministic ordering (by metadata name) or Phase 6 snapshots will drift.
- **R3 — Load collisions for third parties.** Two third-party generators bundling different `Specular.Scanning.dll`
  versions rely on per-analyzer load contexts in the compiler host. Documented in Phase 7; the Sources package is the
  escape hatch.
- **R4 — Expression-hash contract.** Specular's per-call-site keys keep using `GetArgumentExpressionHash`; the library's
  request hash is a separate, internal-to-cache value and is not part of the public stable contract.
