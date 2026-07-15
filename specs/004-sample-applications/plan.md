# Implementation Plan: Sample Applications, AOT Validation & Documentation Build Pipeline

**Branch**: `feature/docs` (PR #114) | **Date**: 2026-06-27 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification from `/specs/004-sample-applications/spec.md`

> **Branch note (D0 RESOLVED)**: Work continues on **`feature/docs`** (single PR #114). No
> separate `004-sample-applications` VCS branch is created.

## Summary

Deliver two intertwined bodies of work that both live on top of the existing ModularPipelines
build (`mise run build` → `dotnet run build/Build.cs`):

1. **Samples & AOT validation suite** — three reusable sample libraries (3–6 discoverable
   services each, covering interface-matching + attribute-based registration + ≥2 lifetimes +
   ≥1 opt-out type), consumed by four demonstration hosts (Console P1, Web minimal-API P2,
   Blazor WASM P3, MAUI P3 with `net10.0-android` as the sole CI-exercised target) plus one
   permanent negative-fixture host. Each demonstration host asserts its expected service set
   in-process and signals mismatch via nonzero exit / detectable failure state. The
   ModularPipelines build gains explicit, named steps that AOT-publish each AOT-capable host
   for a single deterministic target (`net10.0` × current-OS RID) under a zero-warning policy
   (warnings-as-errors for the publish), skip-with-reason when a toolchain/workload is absent,
   and an **inverted** assertion for the negative-fixture host (its step passes only when the
   expected Specular-attributable trim/AOT warning is present).

2. **Documentation build pipeline** — a named docs module folded into `build/Build.cs` that
   (a) generates the API reference with `xmldocmd` from the Specular project's **Release bin
   output** (`src/Specular/bin/Release/net8.0/Specular.dll` + `Specular.xml`, no separate
   `dotnet publish`) and (b) builds the Astro/Starlight site, dropping the built site into
   `artifacts/docs` via `ArtifactSettings`. This replaces the standalone
   `dotnet publish`+xmldocmd step, the mise `docs:api`/`docs:build` tasks, and the
   `deploy-docs.yml` workflow. The pipeline stays deploy-agnostic; `build.yml` runs the
   official GitHub Pages actions gated to `main`. The Astro dev server stays as a VS Code task.

**Technical approach**: Author new local `Module<T>` types **inline in `build/Build.cs`** (the
file-based program's entry assembly auto-discovers `Module` subclasses), reusing
modular-pipelines-extensions building blocks (`ArtifactSettings`, `SolutionSettings`,
`context.DotNet()`, `context.Node()`, `FolderExtensions`, `[DependsOn<>]`, `SkipDecision`).
Samples live under a new top-level `samples/` directory with their own `Directory.Build.props`
that keeps them off the shipped surface (no pack, no public-API tracking) so Specular's public
API (RS0017) is untouched.

## Technical Context

**Language/Version**: C# `LangVersion=preview`; runtime sample TFMs `net8.0;net10.0` (libraries),
AOT publish target `net10.0` only; build program is a .NET 10 file-based app.

**Primary Dependencies**: Specular (project references from `src/Specular`);
`Microsoft.Extensions.DependencyInjection`; ASP.NET Core minimal APIs (Web host); Blazor
WebAssembly SDK + `wasm-tools` workload; .NET MAUI SDK + Android workload; ModularPipelines +
`Rocket.Surgery.ModularPipelines.Extensions` v0.1.6 + `ModularPipelines.Node`; `xmldocmd` 2.9.0
(mise dotnet tool); Astro/Starlight (npm) for the docs site.

**Storage**: N/A (no datastore). "Data" here is the static sample service inventory, host
inventory, and pipeline-step contracts captured in `data-model.md` / `contracts/`.

**Testing**: The hosts' in-process service-set assertions and the pipeline's pass/fail/skip and
inverted-assertion steps **are** the tests for this feature. No new generator snapshot tests are
required (no generator behavior changes). Existing TUnit/Verify suites remain unchanged.
Per **Decision D3**, the two UI hosts are additionally **executed at runtime in CI**: Blazor WASM
via a headless browser harness and MAUI (`net10.0-android`) via an Android emulator, with their
assertion result observed as the CI pass/fail signal (in addition to the AOT-publish guardrail).

**Target Platform**: Console (current-OS RID), ASP.NET Core minimal API (current-OS RID),
Blazor WebAssembly (browser/`browser-wasm`; **executed headless in CI**), MAUI (`net10.0-android`
exercised + **run on an Android emulator in CI**; iOS/MacCatalyst/Windows skip-with-reason).
CI runner: `ubuntu-latest` (current-OS RID resolves to `linux-x64`).

**Project Type**: Multi-host sample/infrastructure suite + build-pipeline tooling (not a
data-modeled service).

**Performance Goals**: N/A (functional/guardrail feature). The only implicit budget is keeping
CI publish times tolerable — limit AOT publishes to the single deterministic matrix.

**Constraints**: Zero-warning policy for every demonstration host's AOT/trim publish closure
(warnings-as-errors); samples MUST NOT modify Specular's public API (RS0017 stays green); samples
are excluded from the packaged/shipped surface; build modules MUST NOT use `System.Console`
directly (the `ConsoleUse` analyzer forbids it — use `context.Logger`); all new NuGet versions
go in `Directory.Packages.props` (central package management); demo dependency closures must
stay AOT/trim-clean (limits third-party packages).

**Scale/Scope**: 3 libraries + 4 demo hosts + 1 negative-fixture host + ~1–3 new build modules +
docs module + CI/workflow edits + UI-host runtime harnesses (headless browser + Android emulator,
per D3). ~12–17 new projects; no changes to `src/Specular*` runtime or generator code.

## Constitution Check

_GATE: Must pass before Phase 0 research. Re-check after Phase 1 design._

| #   | Principle                                                  | Assessment                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
| --- | ---------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| I   | **AOT & Trim Safety (NON-NEGOTIABLE)**                     | ✅ **Directly advances.** The whole AOT-publish suite plus zero-warning policy and the negative-fixture regression guard exist to enforce this principle continuously in CI. No generated-code changes; samples only consume the existing AOT-safe API.                                                                                                                                                                                                                                                                  |
| II  | **Test-First with Snapshot Verification (NON-NEGOTIABLE)** | ⚠️ **Adapted — justified in Complexity Tracking (C1).** This feature changes **no generator behavior**, so the snapshot-test mandate (aimed at generator output) does not literally apply. The feature's "tests" are the hosts' in-process assertions and the pipeline pass/fail/skip + inverted-assertion steps, which must be authored/wired so they can be confirmed failing first (e.g. a deliberately-wrong expected set fails the host; the negative fixture must warn). No existing snapshot coverage is reduced. |
| III | **Minimal & Stable Public API Surface**                    | ✅ **Preserved.** Samples reference Specular but MUST NOT add/remove public API. `samples/Directory.Build.props` disables public-API tracking for samples; RS0017 on `src/Specular` stays green. Verified the needed surface (`ISpecularProvider`, `EntryAssembly`, `FromAssemblyOf<>()...AsMatchingInterface()`, `ServiceRegistrationAttribute`, `ExcludeFromSpecularAttribute`) already exists.                                                                                                                                |
| IV  | **Code Quality & Strict Analysis**                         | ✅ **Honored.** Samples inherit repo analyzers; zero-warning AOT aligns with strict analysis. Build modules must avoid `System.Console` (`ConsoleUse` analyzer) and use `context.Logger`. New package versions go in `Directory.Packages.props`.                                                                                                                                                                                                                                                                         |
| V   | **Documentation as a First-Class Deliverable**             | ✅ **Directly advances.** The docs module unifies API-reference + site build into the canonical pipeline; samples become referenceable worked examples (FR-018/019, SC-007).                                                                                                                                                                                                                                                                                                                                             |

**Roslyn compatibility matrix**: N/A — samples and build tooling do not touch
`Specular.Analyzers`; the 4.8/4.14/5.0 variant builds are unaffected.

**Initial Constitution Check result**: **PASS with one justified adaptation** (Principle II test
interpretation, tracked as C1). No unjustified violations; no AOT-safety deviations requested.

**Post-Design re-check (after Phase 1)**: **PASS** — design keeps samples off the public/shipped
surface, routes all AOT/trim enforcement through the pipeline, and the docs module reuses
extensions building blocks without new abstractions. C1 remains the only entry; no new violations
introduced. See Complexity Tracking.

## Project Structure

### Documentation (this feature)

```text
specs/004-sample-applications/
├── plan.md              # This file
├── research.md          # Phase 0 output — decisions & NEEDS-decision items
├── data-model.md        # Phase 1 — sample/host/step inventories (entity model)
├── quickstart.md        # Phase 1 — how to build/run/validate locally and in CI
├── contracts/           # Phase 1 — step & module contracts
│   ├── aot-publish-step.contract.md
│   ├── negative-fixture-step.contract.md
│   ├── host-assertion.contract.md
│   ├── docs-module.contract.md
│   └── sample-library-inventory.contract.md
└── checklists/
    └── requirements.md  # (pre-existing)
```

### Source Code (repository root)

```text
samples/                                  # NEW — top-level samples root (FR-001)
├── Directory.Build.props                 # IsPackable=false; disable public-API tracking; samples defaults
│                                         #   (CPM stays central per D6 — no samples-local Directory.Packages.props)
├── libraries/
│   ├── Specular.Samples.Catalog/           # library A — interface-matching registration
│   ├── Specular.Samples.Notifications/     # library B — attribute-based registration (varied lifetimes)
│   └── Specular.Samples.Diagnostics/       # library C — mixed styles + 1 opt-out type
├── hosts/
│   ├── Specular.Samples.Console/           # P1 Console host (Native AOT)
│   ├── Specular.Samples.Web/               # P2 minimal-API host (Native AOT)
│   ├── Specular.Samples.Blazor/            # P3 Blazor WASM host (WASM AOT) — run headless in CI (D3)
│   └── Specular.Samples.Maui/              # P3 MAUI host (net10.0-android) — run on emulator in CI (D3)
└── fixtures/
    └── Specular.Samples.NegativeFixture/   # permanent negative-fixture host (must warn under AOT)

build/
└── Build.cs                              # MODIFIED — add inline Module types:
                                          #   DocsModule, AotPublishSamplesModule(s),
                                          #   NegativeFixtureAotPublishModule (see contracts/)

src/Specular/Specular.csproj                  # UNCHANGED public API; XML docs already emitted to Release bin

docs/                                     # UNCHANGED site source; built by DocsModule into artifacts/docs
.github/workflows/
├── build.yml                             # MODIFIED — add Pages upload+deploy gated to main
└── deploy-docs.yml                       # REMOVED (FR-026)
.config/mise.toml                         # MODIFIED — remove/reroute docs:api & docs:build (FR-026)
.vscode/tasks.json                        # MODIFIED — keep docs dev server; fix stale `docs-build` label
Specular.slnx                              # MODIFIED — add ALL sample projects (incl. MAUI + Blazor);
                                          #   missing-workload hard-fails prevented via conditional
                                          #   MSBuild props + pipeline skip-with-reason (D1)
Directory.Packages.props                  # MODIFIED — add any new sample/host package versions (central CPM, D6)
```

**Structure Decision**: A new top-level `samples/` tree (per FR-001 and the user description),
subdivided into `libraries/`, `hosts/`, and `fixtures/` for clarity. Build orchestration changes
are confined to `build/Build.cs` (inline modules) and CI/workflow/mise/VS Code config. No runtime
or generator source under `src/` changes. Samples are isolated from the shipped surface via
`samples/Directory.Build.props`. Per **Decision D1**, every sample project (including the
workload-heavy MAUI and Blazor WASM hosts) is added to `Specular.slnx`; the default solution build
must stay green where MAUI/`wasm-tools` workloads are absent by guarding workload-specific TFMs/
targets behind conditional MSBuild props, with the pipeline reporting skip-with-reason rather than
hard-failing. Per **Decision D5**, implementation proceeds in priority order (libraries + Console +
AOT step + Web + docs pipeline → Blazor WASM → MAUI).

## Complexity Tracking

> Fill ONLY if Constitution Check has violations that must be justified.

| ID  | Violation / Deviation                                                                                                                                    | Why Needed                                                                                                                                                                                                                                                                                                                                                                                | Simpler Alternative Rejected Because                                                                                                                                                                                                                                                  |
| --- | -------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| C1  | **Principle II (Test-First / Snapshot Verification) is satisfied by host assertions + pipeline steps instead of new Verify snapshot tests.**             | This feature introduces **no generator behavior changes**; there is no generated output to snapshot. The behavioral guardrails here are runtime/publish-time: each host's in-process service-set assertion (red-first by temporarily using a wrong expected set) and the pipeline's pass/fail/skip + inverted-assertion steps. These are the appropriate "tests" for sample + infra work. | Forcing new generator snapshot tests would test code this feature does not change, adding noise without protecting the actual deliverables. Existing snapshot coverage is preserved (not reduced), honoring the non-regression clause.                                                |
| C2  | **New local `Module` types authored inline in the single-file `build/Build.cs`** rather than as separate compiled project files.                         | The user explicitly requested "create a module in the Build.cs for now," and the file-based program (`dotnet run build/Build.cs`) auto-discovers `Module` subclasses declared in its own entry assembly. Sibling `.cs` files are not compiled by a file-based app without directives.                                                                                                     | A dedicated build project (`.csproj`) is heavier than warranted now and diverges from how the extensions repo's own `build/Build.cs` is structured. Revisit if the inline file grows unwieldy.                                                                                        |
| C3  | **CI runtime execution of the Blazor WASM and MAUI hosts** (headless browser + Android emulator) in addition to the AOT-publish guardrail (Decision D3). | The maintainer chose genuine in-CI runtime verification over "publish-clean is the signal." This proves the discovered service set is correct on the real UI targets, not merely that they publish trim/AOT-clean.                                                                                                                                                                        | Relying only on the AOT-publish guardrail (the planner's original recommendation) was rejected by the maintainer as insufficient runtime coverage for the UI hosts. Accepted cost: emulator/browser boot time and flakiness, mitigated by stabilization waits and infra-only retries. |

_No AOT-safety deviations are requested. C1/C2 are scope-shape adaptations, not principle
violations, and are recorded here for reviewer transparency per the constitution._
