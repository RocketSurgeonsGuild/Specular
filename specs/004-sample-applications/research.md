# Phase 0 Research: Sample Applications, AOT Validation & Documentation Build Pipeline

**Feature**: 004-sample-applications | **Date**: 2026-06-27

The spec is fully clarified (0 NEEDS CLARIFICATION markers). This document records the
**technical decisions** needed to implement it against the real repo, plus a small set of
**NEEDS-decision** items that require a maintainer call before or during implementation. No
spec ambiguity blocks planning.

---

## Decisions

### D-A. Where new build modules live (module placement)

- **Decision**: Author the new modules (`DocsModule`, the AOT-publish module(s), and the
  negative-fixture module) **inline as `partial class … : Module<T>` declarations inside
  `build/Build.cs`**, after the top-level statements.
- **Rationale**: `mise run build` runs `dotnet run build/Build.cs` — a single-file program.
  ModularPipelines auto-discovers `Module` subclasses in the **entry assembly**, so types
  declared in `Build.cs` are picked up without explicit registration (the same mechanism that
  surfaces the package's modules once referenced). The user explicitly said "create a module in
  the Build.cs for now." The extensions repo's own `build/Build.cs` is likewise a single file.
- **Verification required at implementation**: Confirm a locally-declared `Module<T>` in the
  entry assembly is auto-discovered and ordered via `[DependsOn<>]`. If the file-based app does
  **not** auto-discover entry-assembly modules, fall back to a tiny `build/Build.csproj`-based
  project (tracked as risk, not expected).
- **Alternatives considered**: (a) sibling `.cs` files — rejected: a file-based app compiles
  only the named file unless `#:project`/`#:file` directives are used; (b) a separate build
  project — rejected for now (heavier than requested, see Complexity Tracking C2).

### D-B. How AOT publish is expressed as named pipeline steps

- **Decision**: Add a discrete, named module per AOT-capable demonstration host (or one
  parameterized module that emits one named step per host) that invokes
  `context.DotNet().Publish(...)` with the host-appropriate AOT mode and a
  **warnings-as-errors-for-publish** configuration, targeting `net10.0` × current-OS RID only.
  Publish properties per host type:
    - Console / Web (Native AOT): `PublishAot=true`, `SelfContained=true`,
      `-r <currentRID>`, `-f net10.0`.
    - Blazor WASM: `RunAOTCompilation=true`, `-f net10.0` (browser-wasm), requires `wasm-tools`.
    - MAUI: `-f net10.0-android` with the platform-appropriate trimming/AOT publish; iOS/
      MacCatalyst/Windows are skip-with-reason (never published).
    - Enforce zero-warning via MSBuild trim/AOT warning promotion (e.g.
      `TreatWarningsAsErrors=true` plus ensuring `IlcTreatWarningsAsErrors`/trim analyzer warnings
      surface) — exact property set finalized in `contracts/aot-publish-step.contract.md`.
- **Rationale**: `context.DotNet()` is the extensions' first-class command surface; named modules
  give the discrete, surfaced pass/fail/skip outcomes required by FR-014/017 and SC-006.
- **Skip-with-reason**: Use `ModuleConfiguration.WithSkipWhen(() => SkipDecision.Of(<workload
missing>, "<reason>"))` (pattern used by `PublishNuGetPackagesModule`, `TestSolution`, etc.).
  `SkipDecision` makes "skipped ≠ passed" explicit (FR-016, SC-005).
- **Alternatives considered**: shelling out to raw `dotnet publish` strings — rejected; loses the
  extensions' command result handling, logging interceptors, and binlog conventions.

### D-C. Negative-fixture inverted assertion

- **Decision**: A dedicated `NegativeFixtureAotPublishModule` runs the same `net10.0` ×
  current-OS RID AOT publish for `Specular.Samples.NegativeFixture`, **excluded** from the normal
  AOT steps, and **passes only when the publish warns/fails** as expected (the Specular-attributable
  trim/AOT warning is present). If the publish becomes clean, the module fails the build.
- **Mechanism**: Run the publish **without** treating warnings as errors, capture the command
  result + output, and assert the expected warning signature is present (and/or the publish
  exit indicates the warning). Invert: present ⇒ step passes; absent ⇒ step fails.
- **Rationale**: Implements FR-020/FR-021 and SC-003; guards the regression detector itself.
- **Open detail (see NEEDS-decision D2)**: the precise, stable warning signature the fixture
  emits and how the fixture reliably triggers an _Specular-attributable_ trim/AOT warning.

### D-D. Host in-process assertion + pass/fail signalling

- **Decision**: Each host computes the actual Specular-discovered service set (via the generated
  `ISpecularProvider` / DI container) and compares it to a hard-coded **expected set**. Mismatch ⇒
  visible failure:
    - **Console**: assert at startup; `Environment.Exit(nonzero)` (or non-zero return) on mismatch;
      print the discovery result.
    - **Web (minimal API)**: assert at startup (fail fast so the process exits non-zero / health
      check fails); expose a demonstration endpoint reporting discovered services.
    - **Blazor WASM**: assert on load; surface a detectable failure state (e.g. a failed health
      marker / non-200 served asset / JS-visible failure flag) the pipeline run step can detect.
    - **MAUI**: assert on launch; surface a detectable failure state.
- **Rationale**: FR-008/FR-010, SC-002. The ModularPipelines run step keys off exit code (Console/
  Web) or detectable failure state (Blazor/MAUI).
- **Open detail (see NEEDS-decision D3)**: exactly how the pipeline _runs/observes_ the Blazor &
  MAUI hosts in CI to read their pass/fail (headless run vs. published-output smoke check). The
  AOT-publish guardrail is the primary CI signal for those two; full interactive run may be
  local-only.

### D-E. Docs module behavior

- **Decision**: `DocsModule` (`[DependsOn<BuildSolution>]`) does, in order: 1. Assert `src/Specular/bin/Release/net8.0/Specular.dll` **and** `Specular.xml` exist (fail clearly
  if `Specular.xml` is missing — Edge Case "Docs build consumes bin"). `SolutionSettings.
Configuration` defaults to `Release`, so `BuildSolution` already produces these. 2. Run `xmldocmd <Specular.dll> docs/src/content/docs/api/ --namespace Specular --source
https://github.com/RocketSurgeonsGuild/Specular/blob/main/src/Specular/ --clean`
  (xmldocmd is the mise dotnet tool `xmldocmd@2.9.0`). 3. Run `node docs/scripts/add-api-frontmatter.mjs` (existing Starlight frontmatter injector). 4. `npm ci` + `npm run build` in `docs/` via `context.Node()`. 5. Copy/emit the built site (`docs/dist`) into `ArtifactSettings.ArtifactsDirectory / "docs"`
  using `FolderExtensions` (`EnsureExists`, `/`, `+`).
- **Rationale**: FR-022–FR-025, SC-008. Generates from **bin, not publish** (FR-023). Reuses
  extensions building blocks (FR-025). Stays deploy-agnostic (FR-028).
- **Alternatives considered**: building Astro to a temp dir then copying — equivalent; we keep
  Astro's default `docs/dist` then copy to `artifacts/docs` to avoid Astro config churn.

### D-F. Replace standalone docs toolchain

- **Decision**: Remove `.github/workflows/deploy-docs.yml`; remove (or convert to thin
  pipeline wrappers) mise tasks `docs:api` and `docs:build`; keep `docs` (dev server) and
  `docs:preview`. Add GitHub Pages upload+deploy to `build.yml`, gated to `main`.
- **Rationale**: FR-026/FR-027/FR-028, SC-009/SC-010.

### D-G. CI Pages deploy gating

- **Decision**: In `build.yml`, after `mise run build`, add `actions/upload-pages-artifact`
  (path `artifacts/docs`) and `actions/deploy-pages`, both guarded by
  `if: github.ref == 'refs/heads/main'` (plus the required `pages: write` / `id-token: write`
  permissions and a `pages` concurrency group). Non-`main` runs still produce `artifacts/docs`.
- **Rationale**: FR-027/FR-028, SC-010, Edge Case "Pages deploy gating".

### D-H. VS Code dev task retained + stale label fixed

- **Decision**: Keep the Astro dev-server task; fix the stale `mise: docs-build` task that calls
  the non-existent `mise run docs-build` (actual task is `docs:build`, which is being
  removed/rerouted) — point it at the pipeline (`mise run build`) or the retained docs task.
- **Rationale**: FR-029, SC-011; the user flagged the stale label.

### D-I. Sample registration-style coverage (FR-002/003/004)

- **Decision**: Distribute the required coverage across the three libraries:
    - **Catalog** — interface-matching (`FromAssemblyOf<>().AddClasses().AsMatchingInterface()`),
      default lifetime.
    - **Notifications** — attribute-based (`[ServiceRegistration(ServiceLifetime.Scoped)]` /
      `<TService>` generic variants), demonstrating Scoped + Transient lifetimes.
    - **Diagnostics** — mixed, and includes the `[ExcludeFromSpecular]` opt-out type (FR-004).
    - Collectively: interface-matching + attribute-based + ≥2 lifetimes + ≥1 opt-out ⇒ ≥3 styles
      (SC-001). Verified the needed API exists in `src/Specular` (`ServiceRegistrationAttribute`,
      `ExcludeFromSpecularAttribute`, fluent selectors, `ISpecularProvider.EntryAssembly`).

---

## NEEDS-decision items — RESOLVED (maintainer call, 2026-06-27)

All seven items below were decided with the maintainer during the post-plan review. Status is **RESOLVED**; `/speckit.tasks` should treat these as binding.

| ID     | Item                                                                       | Decision (RESOLVED)                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| ------ | -------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **D0** | Working branch.                                                            | **Continue on `feature/docs`** (single PR #114). No new branch is created; ignore the Spec Kit `004-sample-applications` branch name for VCS purposes.                                                                                                                                                                                                                                                                                                                |
| **D1** | Samples in main `Specular.slnx` vs. a solution filter.                       | **All sample projects (libraries, all four hosts incl. MAUI + Blazor WASM, and the negative-fixture) go in `Specular.slnx`.** Missing-workload hard-fails are prevented via **conditional MSBuild props** (e.g. guard MAUI/WASM-AOT targets/TFMs behind workload-availability conditions) and the pipeline's skip-with-reason, NOT by excluding them from the solution. The default solution build must remain green where workloads are absent.                        |
| **D2** | Negative-fixture warning signature.                                        | **Deferred to implementation.** Requirement: the fixture must emit _some_ Specular-attributable `IL2xxx`/`IL3xxx` trim/AOT warning; the exact warning code/text is pinned in `contracts/negative-fixture-step.contract.md` when the fixture is built. The inverted assertion matches the pinned signature.                                                                                                                                                              |
| **D3** | How CI observes Blazor WASM & MAUI host pass/fail.                         | **Added scope: automated runtime execution in CI for both UI hosts.** Blazor WASM is exercised via a **headless browser** harness; MAUI (`net10.0-android`) via an **Android emulator** in CI. These runtime runs assert the expected service set and feed pass/fail to the pipeline, IN ADDITION to the AOT-publish guardrail. (Accepted cost: emulator/browser boot time + potential flakiness; tasks must include harness setup + reasonable stabilization/retry.) |
| **D4** | Exact MSBuild property set that fails the publish on ANY trim/AOT warning. | **Spike during implementation.** Before wiring real hosts, prove a forced/injected warning fails the publish (red) for each mode (Native AOT, WASM AOT, MAUI-android), then pin the exact property set in `contracts/aot-publish-step.contract.md`.                                                                                                                                                                                                                   |
| **D5** | Implementation sequencing.                                                 | **Confirmed priority order**: (1) sample libraries + Console host + AOT publish step + Web host + docs pipeline, then (2) Blazor WASM, then (3) MAUI. All in scope; sequencing only.                                                                                                                                                                                                                                                                                  |
| **D6** | Sample CPM location.                                                       | **Reuse the root `Directory.Packages.props`** (central package management, single source of truth). Add any sample-only package versions there. A `samples/Directory.Build.props` still applies sample-wide opt-outs (no packing, no public-API tracking), but versions stay central.                                                                                                                                                                                 |

---

## Constraints carried into design

- **Zero-warning policy** (FR-012/015): demo libraries/hosts must keep their dependency closure
  AOT/trim-clean → restrict third-party packages to AOT/trim-safe ones.
- **No public API change** (FR + Principle III): `samples/Directory.Build.props` disables
  public-API tracking & packing; RS0017 on `src/Specular` stays green.
- **`ConsoleUse` analyzer**: build modules must use `context.Logger`, never `System.Console`.
- **Central Package Management**: all new versions in `Directory.Packages.props`.
- **Docs depends on Release bin + XML**: `DocsModule` fails clearly if `Specular.xml` is missing.
