---
description: Task list for Sample Applications, AOT Validation & Documentation Build Pipeline
---

# Tasks: Sample Applications, AOT Validation & Documentation Build Pipeline

**Input**: Design documents from `/specs/004-sample-applications/`

**Prerequisites**: plan.md ✅, spec.md (7 user stories, 29 FRs, 11 SCs) ✅, research.md (D0–D6 RESOLVED, D-A–D-I) ✅, data-model.md ✅, quickstart.md ✅, contracts/ (5) ✅

**Branch (D0 RESOLVED)**: All work stays on **`feature/docs`** (PR #114). **No branch-creation tasks.**

**Tests for this feature (Constitution C1 / Principle II adaptation)**: This feature changes **no generator behavior**, so there are no new Verify snapshot tests. The "tests" are (a) each host's **in-process service-set assertion** and (b) the pipeline's **pass/fail/skip + inverted-assertion** steps. These MUST be authored **red-first** where the brief calls for it:

- Host assertions confirmed **failing first** (seed a wrong expected set / drop a library reference, observe nonzero exit / detectable failure, then make it pass).
- The **D4 spike** proving a forced/injected trim/AOT warning **fails the publish (red)** for each AOT mode **before** wiring real hosts.
- The **negative-fixture inverted assertion** (D2) — passes only when the expected Specular-attributable warning is present; a clean publish must FAIL it.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies on incomplete tasks)
- **[Story]**: US1–US7 maps the task to a spec user story; Setup/Foundational/Polish carry no story label
- Every task names exact file path(s) and a clear acceptance condition

## Path Conventions

- Samples root (NEW): `samples/` → `samples/libraries/`, `samples/hosts/`, `samples/fixtures/`
- Build modules authored **inline** in the single-file `build/Build.cs` (file-based app auto-discovers `Module<T>` subclasses in its entry assembly; sibling `.cs` files are NOT compiled — research D-A / Complexity C2)
- Central Package Management at **root** `Directory.Packages.props` (D6); `samples/Directory.Build.props` for sample-wide opt-outs
- Build modules MUST log via `context.Logger` — the `ConsoleUse` analyzer forbids `System.Console`

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Establish the `samples/` tree, sample-wide MSBuild conventions, the conditional-workload guard mechanism (D1), CPM entries (D6), and solution wiring so the default `dotnet build Specular.slnx` stays green even without MAUI/`wasm-tools`.

- [x] T001 Create the `samples/` directory tree with `samples/libraries/`, `samples/hosts/`, and `samples/fixtures/` subfolders (placeholder `.gitkeep` where empty) per plan Project Structure (FR-001).
- [x] T002 [P] Create `samples/Directory.Build.props` applying sample-wide opt-outs: `IsPackable=false`, disable public-API/RS0017 tracking, sample default analyzer relaxations, and `LangVersion=preview`. MUST NOT define package versions (CPM stays central — D6). Acceptance: a sample project under `samples/` inherits these and is excluded from the packed surface (FR-005, Principle III).
- [x] T003 [P] Create the **conditional-workload guard** props (e.g. `samples/Directory.Build.workloads.props` imported by `samples/Directory.Build.props`) that detect MAUI / `wasm-tools` workload availability and guard workload-specific TFMs/targets behind those conditions (D1). Acceptance: with MAUI/`wasm-tools` absent, evaluating the props does not introduce a workload-requiring TFM/target into the default solution build.
- [x] T004 [P] Add all new sample/host NuGet package versions to the **root** `Directory.Packages.props` (central CPM, D6): `Microsoft.Extensions.DependencyInjection`, ASP.NET Core minimal-API refs, Blazor WASM SDK refs, MAUI refs, and the D3 harness packages (e.g. `Microsoft.Playwright`). Keep the closure AOT/trim-clean (FR-012 constraint). Acceptance: no samples-local `Directory.Packages.props` exists; versions resolve from root.
- [x] T005 Add `/samples/` solution folders to `Specular.slnx` (libraries/hosts/fixtures groupings) so subsequent project-add tasks have a home (D1). Acceptance: `Specular.slnx` opens with empty samples folders; existing build unaffected.
- [x] T006 Verify the empty `samples/` scaffolding builds clean: run `dotnet build Specular.slnx` and confirm it stays green with no MAUI/`wasm-tools` workloads installed (D1 invariant baseline before any host exists).

**Checkpoint**: Samples tree, conventions, conditional guards, CPM entries, and solution wiring exist; default solution build is green.

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Prove the inline-module mechanism and the AOT-publish guardrail mechanics **before** any real host depends on them. This phase BLOCKS the AOT-enforcement tasks of US2/US3.

**⚠️ CRITICAL**: No real-host AOT-enforcement work (T019–T020, T021–T028) may begin until T009 (Native AOT D4 spike) is pinned.

- [x] T007 Verify inline `Module<T>` auto-discovery in the file-based `build/Build.cs` (research D-A): add a trivial no-op `partial class … : Module<…>` after the top-level statements, run `mise run build`, and confirm it is discovered/ordered. If auto-discovery fails, fall back to a minimal `build/Build.csproj` (tracked risk). Acceptance: the no-op module appears as a named step in build output.
- [x] T008 Add a **shared AOT-publish helper** inline in `build/Build.cs` (current-OS RID resolution, `net10.0` target, `context.DotNet().Publish(... .BinlogTo(context, binlog))`, `context.Logger` reporting, and a reusable `ModuleConfiguration.WithSkipWhen(() => SkipDecision.Of(<missing>, "<reason>"))` skip-with-reason pattern) used by every AOT step (FR-014/016/017, contract `aot-publish-step`). Acceptance: helper compiles and is referenced by a placeholder named step that reports PASS/SKIP correctly.
- [x] T009 **[D4 SPIKE — Native AOT, RED-FIRST]** Prove a forced/injected trim/AOT warning **fails** a Native AOT publish (`PublishAot=true`, `-f net10.0 -r <currentRID>`) under the warnings-as-errors property set; then **PIN the exact MSBuild property set** into `specs/004-sample-applications/contracts/aot-publish-step.contract.md` (replace the "finalized via research D4" placeholder). Acceptance: a deliberately-injected `IL2xxx`/`IL3xxx` warning makes the publish exit non-zero; the contract records the precise properties. Native-AOT-enforcement tasks (T019–T020, T021–T024) depend on this.

**Checkpoint**: Inline-module discovery confirmed; AOT step scaffolding + skip-with-reason ready; Native AOT zero-warning enforcement property set proven (red) and pinned.

---

## Phase 3: User Story 1 - Reusable sample libraries (Priority: P1) 🎯 MVP

**Goal**: Three reusable libraries whose services Specular discovers, collectively covering interface-matching + attribute-based registration + ≥2 lifetimes + ≥1 opt-out type.

**Independent Test**: Built together with the first consumer (the Console host, US2), Specular's generated provider registers exactly the expected services from each library, with correct service→implementation mappings and lifetimes, and the opt-out type is absent (contract `sample-library-inventory`).

- [x] T010 [P] [US1] Create `samples/libraries/Specular.Samples.Catalog/Specular.Samples.Catalog.csproj` (multi-target `net8.0;net10.0`, project-ref `src/Specular`) with **3–6** `IXxxService`→`XxxService` pairs registered by **interface-matching** (`FromAssemblyOf<>().AddClasses().AsMatchingInterface()`), default (Singleton) lifetime; add to `Specular.slnx` (FR-001/002/003, contract row "Interface-matching").
- [x] T011 [P] [US1] Create `samples/libraries/Specular.Samples.Notifications/Specular.Samples.Notifications.csproj` (multi-target, project-ref `src/Specular`) with **3–6** services using **attribute-based** registration (`[ServiceRegistration(ServiceLifetime.Scoped)]` and `[ServiceRegistration<TService>(ServiceLifetime.Transient)]` generic variants) demonstrating **Scoped + Transient**; add to `Specular.slnx` (FR-002/003, contract rows "Attribute-based"/"Distinct lifetimes").
- [x] T012 [P] [US1] Create `samples/libraries/Specular.Samples.Diagnostics/Specular.Samples.Diagnostics.csproj` (multi-target, project-ref `src/Specular`) with **3–6** services mixing interface-matching + attribute styles (Singleton + Transient), and **exactly one** type marked `[ExcludeFromSpecular]`; add to `Specular.slnx` (FR-002/003/004, contract rows "Opt-out type").
- [x] T013 [US1] Add an in-repo **coverage assertion doc/comment** co-located with the libraries (e.g. `samples/libraries/README.md`) enumerating each library's expected service set, lifetime, and registration style, so the inventory is inspectable without running anything (FR-019, SC-001/007). Acceptance: doc lists ≥3 styles/lifetimes + the opt-out type and matches the three projects.
- [x] T014 [US1] Verify the three libraries build under the multi-target and stay off the packed surface: `dotnet build samples/libraries/*` and confirm `IsPackable=false` is inherited and `src/Specular` RS0017 is untouched (FR-005, Principle III, SC-001).

**Checkpoint**: Three libraries exist and build; collective coverage matrix satisfied; opt-out type present. (Independent runtime proof arrives with the Console host in US2.)

---

## Phase 4: User Story 2 - Console host proves scanning and AOT end-to-end (Priority: P1) 🎯 MVP

**Goal**: A Console host consuming all three libraries, running Specular scanning, asserting the expected service set in-process (nonzero exit on mismatch), and publishable with Native AOT zero-warning.

**Independent Test**: `dotnet run --project samples/hosts/Specular.Samples.Console` prints the discovery result and exits `0` on match (nonzero on mismatch); a local Native AOT publish for `net10.0 × current RID` runs and asserts successfully with zero trim/AOT warnings (contract `host-assertion`).

- [x] T015 [US2] **[RED-FIRST]** Scaffold `samples/hosts/Specular.Samples.Console/Specular.Samples.Console.csproj` (target `net10.0`, `PublishAot` enabled, project-refs all three libraries) with a **deliberately wrong** `expectedServiceSet`; run it and confirm it exits **nonzero** (the assertion fails as designed). Add to `Specular.slnx` (FR-010, contract `host-assertion` §"test-first red state").
- [x] T016 [US2] Implement the Console host's Specular **compile-time** scanning of all three libraries, compute the actual discovered set via the generated `ISpecularProvider`, compare to the hard-coded `expectedServiceSet`, print the discovery result to stdout, and `Environment.Exit(nonzero)` on mismatch in `samples/hosts/Specular.Samples.Console/Program.cs` (FR-007/008/009/010).
- [x] T017 [US2] Exercise `ISpecularProvider.EntryAssembly` in the Console host to query the entry assembly's discovered types and include them in the assertion (US2 AC3, contract `host-assertion` API surface).
- [x] T018 [US2] Replace the wrong expected set with the **correct** set; run the host and confirm exit `0`, the opt-out `[ExcludeFromSpecular]` type is **absent**, and all three libraries' services are listed (FR-004/008, SC-002). Acceptance: green run with full discovery output.

**Checkpoint**: Console host runs, asserts correctly (red→green proven), and is locally Native-AOT publishable. **MVP slice (US1+US2) demonstrable.**

---

## Phase 5: User Story 3 - AOT publish exercised as a build pipeline test (Priority: P1) 🎯 MVP guardrail

**Goal**: The ModularPipelines build AOT-publishes the AOT-capable host(s) as discrete named steps under the zero-warning policy, plus the permanent negative-fixture host validated by an inverted assertion (D2).

**Independent Test**: `mise run build` shows a named AOT-publish step per AOT-capable host with PASS/FAIL/SKIP; injecting a trim/AOT warning into a demo host fails its step and the build; the negative-fixture step PASSES only because it warns and FAILS if it ever goes clean (contracts `aot-publish-step`, `negative-fixture-step`).

- [x] T019 [US3] Add the **Console AOT-publish named module** inline in `build/Build.cs` using the T008 helper and the T009-pinned property set: publish `Specular.Samples.Console` for `net10.0 × current RID`, `PublishAot=true`, warnings-as-errors; report PASS/FAIL/SKIP via `context.Logger`; skip-with-reason if the native toolchain is absent (FR-011/012/013/014/016/017, contract `aot-publish-step`).
- [x] T020 [US3] **[RED proof on real host]** Temporarily inject a trim/AOT warning into the Console host and confirm the T019 step (and the overall build) **FAILS**, then remove it and confirm PASS (FR-015, SC-004; contract `aot-publish-step` rule "proven to FAIL when a warning is injected").
- [x] T021 [US3] Create `samples/fixtures/Specular.Samples.NegativeFixture/Specular.Samples.NegativeFixture.csproj` (target `net10.0`, project-ref `src/Specular`) constructed to emit **some Specular-attributable `IL2xxx`/`IL3xxx`** trim/AOT warning under AOT publish; exclude it from the normal AOT steps and from the four demonstration hosts; add to `Specular.slnx` (FR-020, D2, contract `negative-fixture-step`).
- [x] T022 [US3] **[D2 PIN]** Publish the fixture, capture the emitted warning, and **pin the exact warning code/text + triggering construct** into `specs/004-sample-applications/contracts/negative-fixture-step.contract.md` (replace the "pinned during implementation — research D2" placeholder). Acceptance: contract records a stable, greppable signature (FR-021, D2).
- [x] T023 [US3] Add the **`NegativeFixtureAotPublishModule`** inline in `build/Build.cs`: run the same `net10.0 × current RID` publish **without** warnings-as-errors, inspect output for the T022-pinned signature, and apply the **inverted** assertion — warning present ⇒ PASS, clean/absent ⇒ FAIL the build; skip-with-reason (never PASS) if toolchain absent (FR-020/021, SC-003, contract `negative-fixture-step`).
- [x] T024 [US3] **[RED-FIRST inverted assertion]** Force the fixture publish **clean** (temporarily remove its trigger) and confirm the T023 step **FAILS** the build (regression-detector guard), then restore the trigger and confirm PASS (FR-021, SC-003, Edge Case "Negative-fixture regression guard").

**Checkpoint**: First **pipeline-verified** end-to-end slice complete — Console AOT guardrail + negative-fixture inverted assertion are green in `mise run build`. **Full MVP guardrail reached.**

---

## Phase 6: User Story 7 - Documentation build through the ModularPipelines build (Priority: P2)

**Goal**: A named docs module folds API-reference generation (from Release **bin**, not publish) + Astro/Starlight site build into `build/Build.cs`, emitting `artifacts/docs`; the standalone docs toolchain is removed; Pages deploy is a CI concern gated to `main`. (Sequenced early per D5.)

**Independent Test**: `mise run build` on a non-`main` branch generates the API reference from `src/Specular/bin/Release/net8.0/Specular.dll` + `Specular.xml` and produces `artifacts/docs`, with no separate `dotnet publish` for docs and no Pages deploy; the dev server still starts (contract `docs-module`, SC-008/009/010/011).

- [x] T025 [US7] Add the **`DocsModule`** inline in `build/Build.cs` with `[DependsOn<BuildSolution>]`: (1) precondition-check `src/Specular/bin/Release/net8.0/Specular.dll` **and** `Specular.xml` exist and **fail clearly** if `Specular.xml` is missing; (2) run `xmldocmd` (mise tool `xmldocmd@2.9.0`) against the **bin** dll with flags `--namespace Specular --source https://github.com/RocketSurgeonsGuild/Specular/blob/main/src/Specular/ --clean` → `docs/src/content/docs/api/`; (3) `node docs/scripts/add-api-frontmatter.mjs`; (4) `npm ci` + `npm run build` in `docs/` via `context.Node()`; (5) copy `docs/dist` into `ArtifactSettings.ArtifactsDirectory / "docs"` via `FolderExtensions`; log via `context.Logger` (FR-022/023/024/025, contract `docs-module`).
- [x] T026 [US7] **[RED-FIRST precondition]** Temporarily remove `Specular.xml` (or disable its emission) and confirm `DocsModule` **fails clearly** (not an empty/partial API ref), then restore and confirm PASS (FR-023, Edge Case "Docs build consumes bin").
- [x] T027 [P] [US7] Remove `.github/workflows/deploy-docs.yml` (FR-026, SC-009).
- [x] T028 [P] [US7] In `.config/mise.toml`, remove or convert to thin pipeline-wrappers the `docs:api` and `docs:build` tasks (which currently do a standalone `dotnet publish`+xmldocmd), and **keep** `docs` (dev server) and `docs:preview` (FR-026/029, SC-009/011).
- [x] T029 [P] [US7] Fix the stale `mise: docs-build` label in `.vscode/tasks.json` (it calls the non-existent `mise run docs-build`) — point it at `mise run build` (the pipeline) or the retained `docs` task; keep the Astro dev-server task (FR-029, SC-011, research D-H).
- [x] T030 [US7] Update `.github/workflows/build.yml`: after `mise run build`, add `actions/upload-pages-artifact` (path `artifacts/docs`) + `actions/deploy-pages`, **guarded by `if: github.ref == 'refs/heads/main'`**, with `pages: write` + `id-token: write` permissions and a `pages` concurrency group (FR-027/028, SC-010, research D-G).
- [x] T031 [US7] Validate the docs pipeline: `mise run build` produces `artifacts/docs` with the API ref generated from **bin** (no separate `dotnet publish` for docs), and confirm no Pages deploy occurs off `main` (SC-008/009/010).

**Checkpoint**: Docs are built through the canonical pipeline into `artifacts/docs`; standalone toolchain removed; Pages deploy gated to `main`; dev server retained.

---

## Phase 7: User Story 4 - Web (minimal API) host demonstrates scanning and AOT (Priority: P2)

**Goal**: A minimal-API host consuming the libraries, asserting the expected service set at startup (fail-fast nonzero/health), exposing a demonstration endpoint, and Native-AOT publishable zero-warning.

**Independent Test**: Start the host; it fails fast on mismatch; a demonstration endpoint reports discovered services; a Native AOT publish runs zero-warning (contracts `host-assertion`, `aot-publish-step`).

- [x] T032 [US4] **[RED-FIRST]** Scaffold `samples/hosts/Specular.Samples.Web/Specular.Samples.Web.csproj` (target `net10.0`, minimal API, `PublishAot` enabled, project-refs all three libraries) with a **wrong** expected set and confirm startup assertion **fails fast** (process exits nonzero / health check fails). Add to `Specular.slnx` (FR-010, contract `host-assertion`).
- [x] T033 [US4] Implement the Web host in `samples/hosts/Specular.Samples.Web/Program.cs`: Specular compile-time scanning of all three libraries, startup assertion against the hard-coded expected set (fail-fast on mismatch), and a demonstration **minimal-API endpoint** reporting discovered services; correct the expected set and confirm startup green + endpoint lists services with the opt-out type absent (FR-007/008/009/010, SC-002).
- [x] T034 [US4] Add the **Web AOT-publish named module** inline in `build/Build.cs` (reuse T008 helper + T009 Native-AOT property set): publish `Specular.Samples.Web` for `net10.0 × current RID`, warnings-as-errors, PASS/FAIL/SKIP via `context.Logger`, skip-with-reason if toolchain absent (FR-011/012/014/016/017, US4 AC2, contract `aot-publish-step`).
- [x] T035 [US4] Verify end-to-end: AOT-published Web host starts, passes its startup assertion, serves the endpoint, and the publish is zero-warning (SC-004, US4 AC2).

**Checkpoint**: Web host runs, asserts at startup, serves its endpoint, and is pipeline-AOT-published clean.

---

## Phase 8: User Story 5 - Blazor WebAssembly host + CI runtime harness (Priority: P3)

**Goal**: A Blazor WASM host listing discovered services, surfacing a detectable failure state on mismatch, WASM-AOT publishable zero-warning, **and executed headlessly in CI** (D3) with its assertion result observed.

**Independent Test**: Load the host (browser/published output); a component lists discovered services; mismatch surfaces a detectable failure; WASM AOT publish completes zero-warning; the headless-browser harness reads pass/fail (contracts `host-assertion` §D3, `aot-publish-step`).

- [ ] T036 [US5] **[D4 SPIKE — WASM AOT, RED-FIRST]** Prove a forced/injected trim/AOT warning **fails** a WASM AOT publish (`RunAOTCompilation=true`, `-f net10.0`, requires `wasm-tools`); **pin the WASM property set** into `contracts/aot-publish-step.contract.md`. Acceptance: injected warning fails the publish; contract updated. (Gates T038.)
- [ ] T037 [US5] **[RED-FIRST]** Scaffold `samples/hosts/Specular.Samples.Blazor/Specular.Samples.Blazor.csproj` (Blazor WASM, `net10.0`, conditional `wasm-tools`/WASM-AOT guards from T003, project-refs all three libraries) with a **wrong** expected set; confirm the host surfaces a **detectable failure state** on load. Add to `Specular.slnx`; confirm default solution build stays green without `wasm-tools` (D1, FR-010).
- [ ] T038 [US5] Implement the Blazor host: Specular compile-time scanning, a **component/page listing** discovered services, on-load assertion against the expected set, and a **detectable failure state surfaced to DOM/console** the harness can read; correct the expected set and confirm green + opt-out type absent (FR-007/008/009/010, US5 AC1).
- [ ] T039 [US5] Add the **Blazor WASM AOT-publish named module** inline in `build/Build.cs` (reuse helper + T036 property set): publish for `net10.0`, warnings-as-errors, **skip-with-reason if `wasm-tools` absent** (never PASS) (FR-011/012/014/016/017, US5 AC2, contract `aot-publish-step`).
- [ ] T040 [US5] **[D3 harness]** Add a **headless-browser harness** (e.g. Playwright/Chromium) that loads the published Blazor WASM app, reads the surfaced assertion result (DOM/console), and exits pass/fail; wire it as a discrete named pipeline step in `build/Build.cs` distinct from the publish step; include reasonable startup/stabilization waits and **infra-only retry** that MUST NOT retry a genuine assertion mismatch (D3, contract `host-assertion` §D3). Acceptance: harness reports the host's pass/fail to the pipeline.
- [ ] T041 [US5] **[RED-FIRST harness]** Seed a wrong expected set, run the T040 harness, and confirm it reports **FAIL** (no infra-retry masks it); restore and confirm PASS (D3, SC-002).

**Checkpoint**: Blazor WASM host runs, asserts, WASM-AOT-publishes clean (or skips with reason), and is executed + verified headlessly in CI.

---

## Phase 9: User Story 6 - MAUI host + Android emulator harness (Priority: P3)

**Goal**: A MAUI host (`net10.0-android` CI target) listing discovered services, surfacing a detectable failure state, AOT/trim-publishable zero-warning on android (other platforms skip-with-reason), **and executed on an Android emulator in CI** (D3).

**Independent Test**: Launch on android; UI lists discovered services; mismatch surfaces a detectable failure; `net10.0-android` AOT/trim publish completes zero-warning while iOS/MacCatalyst/Windows are skip-with-reason; the emulator harness reads pass/fail (contracts `host-assertion` §D3, `aot-publish-step`).

- [ ] T042 [US6] **[D4 SPIKE — MAUI-android, RED-FIRST]** Prove a forced/injected trim/AOT warning **fails** a `net10.0-android` platform-appropriate AOT/trim publish (requires MAUI + Android workloads); **pin the MAUI-android property set** into `contracts/aot-publish-step.contract.md`. Acceptance: injected warning fails the publish; contract updated. (Gates T044.)
- [ ] T043 [US6] **[RED-FIRST]** Scaffold `samples/hosts/Specular.Samples.Maui/Specular.Samples.Maui.csproj` (`net10.0-android` exercised; iOS/MacCatalyst/Windows guarded behind conditional workload props from T003; project-refs all three libraries) with a **wrong** expected set; confirm the host surfaces a **detectable failure state** on launch. Add to `Specular.slnx`; confirm default solution build stays green without MAUI workloads (D1, FR-006/010).
- [ ] T044 [US6] Implement the MAUI host: Specular compile-time scanning, a **screen listing** discovered services, on-launch assertion against the expected set, and a **detectable failure state** (instrumentation/log/exit signal) the harness can read; correct the expected set and confirm green + opt-out type absent (FR-007/008/009/010, US6 AC1).
- [ ] T045 [US6] Add the **MAUI AOT-publish named module** inline in `build/Build.cs` (reuse helper + T042 property set): genuinely publish `net10.0-android` warnings-as-errors; **always report iOS/MacCatalyst/Windows as skip-with-reason** (out-of-CI-scope, never PASS); skip-with-reason if MAUI/Android workloads absent (FR-006/011/012/016/017, US6 AC2/AC3, SC-005, contract `aot-publish-step`).
- [ ] T046 [US6] **[D3 harness]** Add an **Android-emulator harness** in CI that boots an emulator, installs/runs the published `net10.0-android` app, reads the surfaced assertion result (instrumentation/log/exit), and exits pass/fail; wire it as a discrete named pipeline step distinct from the publish step; include emulator boot/stabilization waits and **infra-only retry** that MUST NOT retry a genuine assertion mismatch (D3, contract `host-assertion` §D3).
- [ ] T047 [US6] **[RED-FIRST harness]** Seed a wrong expected set, run the T046 emulator harness, and confirm it reports **FAIL** (no infra-retry masks it); restore and confirm PASS (D3, SC-002).

**Checkpoint**: MAUI host runs on android, asserts, AOT/trim-publishes clean (non-android skip-with-reason), and is executed + verified on an emulator in CI.

---

## Phase 10: Polish & Cross-Cutting Concerns

**Purpose**: Documentation integration, the D1 green-build invariant, and full quickstart validation across all stories.

- [x] T048 [P] Document the samples as **worked examples** in the docs site (`docs/src/content/docs/…`): for each host, extract the Specular selector expressions / registration approach so a reader understands the scanning without running the sample (FR-018/019, SC-007).
- [x] T049 [P] Verify the **D1 green-build invariant**: on a machine/runner **without** MAUI and `wasm-tools`, `dotnet build Specular.slnx` succeeds (workload-specific TFMs/targets guarded by the T003 conditional props), and the corresponding AOT/harness steps report **skip-with-reason** (not pass) (D1, FR-016, SC-005).
- [x] T050 [P] Confirm `src/Specular` public API is unchanged: RS0017 stays green and no samples-induced public-API diff (Principle III, contract `sample-library-inventory` §5).
- [x] T051 Run the full `quickstart.md` validation checklist (SC-001…SC-011) end-to-end via `mise run build` plus the per-host run/publish steps; confirm every named AOT step and harness step surfaces a clear PASS/FAIL/SKIP line (SC-006) and tick each SC box.

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies — start immediately.
- **Foundational (Phase 2)**: Depends on Setup. **BLOCKS** all AOT-enforcement work. T009 (Native AOT D4 spike) specifically gates T019/T020 and T034.
- **US1 (Phase 3, P1)**: Depends on Setup. Independent of other stories.
- **US2 (Phase 4, P1)**: Depends on US1 (consumes the libraries). MVP with US1.
- **US3 (Phase 5, P1)**: Depends on US2 (Console host) + T008/T009. Delivers the pipeline guardrail.
- **US7 (Phase 6, P2)**: Depends on Foundational (inline-module mechanism T007) + an existing `BuildSolution`. Independent of US2–US6; sequenced early per D5.
- **US4 (Phase 7, P2)**: Depends on US1 + T008/T009 (reuses Native-AOT property set).
- **US5 (Phase 8, P3)**: Depends on US1 + T008. Its T039 depends on the WASM D4 spike T036; T040/T041 depend on T038.
- **US6 (Phase 9, P3)**: Depends on US1 + T008. Its T045 depends on the MAUI D4 spike T042; T046/T047 depend on T044.
- **Polish (Phase 10)**: Depends on the desired set of stories being complete.

### Story Sequencing (D5 — RESOLVED)

1. **Batch 1 (MVP + early value)**: US1 → US2 → US3 (Console + AOT guardrail + negative fixture) → US4 (Web) → US7 (docs pipeline).
2. **Batch 2**: US5 (Blazor WASM + headless harness).
3. **Batch 3**: US6 (MAUI + emulator harness).

### Within Each Story

- Red-first assertion tasks precede the green implementation (host assertions confirmed failing first).
- D4 spikes (T009/T036/T042) precede the real-host AOT-enforcement module for that mode.
- D2 fixture build (T021) precedes the pin (T022), which precedes the inverted-assertion module (T023), which precedes the inverted red-first proof (T024).
- Library projects before host projects; host project before its AOT module; AOT module before its runtime harness (UI hosts).

### Parallel Opportunities

- **Setup**: T002, T003, T004 are `[P]` (distinct files). T005/T006 follow.
- **US1**: T010, T011, T012 are `[P]` — the three libraries are independent projects.
- **US7**: T027, T028, T029 are `[P]` (delete workflow / edit mise / edit vscode — distinct files).
- **Cross-story (after Foundational + US1)**: US7 (docs) can proceed fully in parallel with the US2/US3 AOT track since they touch different concerns (docs module vs. AOT modules) — only the shared `build/Build.cs` file is a coordination point.
- **Polish**: T048, T049, T050 are `[P]`.
- The two D3 harnesses (Blazor T040 / MAUI T046) are independent once their hosts exist and can be built in parallel by different contributors.

---

## Parallel Example: User Story 1

```bash
# The three sample libraries are independent — create them together:
Task T010: "Create Specular.Samples.Catalog (interface-matching) in samples/libraries/Specular.Samples.Catalog/"
Task T011: "Create Specular.Samples.Notifications (attribute-based, Scoped+Transient) in samples/libraries/Specular.Samples.Notifications/"
Task T012: "Create Specular.Samples.Diagnostics (mixed + [ExcludeFromSpecular]) in samples/libraries/Specular.Samples.Diagnostics/"
```

## Parallel Example: Documentation Toolchain Removal (US7)

```bash
Task T027: "Remove .github/workflows/deploy-docs.yml"
Task T028: "Reroute/remove mise docs:api & docs:build in .config/mise.toml"
Task T029: "Fix stale docs-build label in .vscode/tasks.json"
```

---

## Implementation Strategy

### MVP First (Phases 1–5)

1. Phase 1 Setup → Phase 2 Foundational (incl. Native AOT D4 spike T009).
2. Phase 3 US1 (libraries) → Phase 4 US2 (Console host runs & asserts, red→green).
3. **STOP & VALIDATE**: Console host demonstrates discovery and exits 0 (US1+US2 = demonstrable MVP).
4. Phase 5 US3 → the AOT-publish guardrail + negative-fixture inverted assertion turn the MVP into a **pipeline-verified** slice. **This is the first end-to-end demonstrable + CI-guarded boundary.**

### Incremental Delivery (D5 order)

1. MVP guardrail (US1+US2+US3) → demo + CI guardrail.
2. Add US4 (Web) and US7 (docs pipeline) → broader P2 value.
3. Add US5 (Blazor WASM + headless harness) → client-side coverage.
4. Add US6 (MAUI + emulator harness) → cross-platform coverage.

### Parallel Team Strategy

- After Foundational + US1: one track drives US2→US3 (AOT guardrail), another drives US7 (docs), coordinating edits to `build/Build.cs`.
- Once T008 exists, US4/US5/US6 AOT modules follow the same pattern and can be split across contributors (each gated by its mode's D4 spike).

---

## Notes

- `[P]` = different files, no incomplete-task dependencies.
- All AOT/harness pipeline steps are **named** and surface a discrete PASS/FAIL/SKIP (FR-014/017, SC-006); skip-with-reason is never reported as pass (FR-016, SC-005).
- Build modules MUST use `context.Logger` (the `ConsoleUse` analyzer forbids `System.Console`).
- All NuGet versions live in root `Directory.Packages.props` (D6); samples carry no local CPM file.
- The negative-fixture host is the **only** project exempt from the zero-warning policy and is **required to warn** (FR-021).
- D2 (T022) and D4 (T009/T036/T042) **pin** their results back into the contracts so future SDK drift is a deliberate, reviewed change.
