# Feature Specification: Sample Applications, AOT Validation & Documentation Build Pipeline

**Feature Branch**: `004-sample-applications`

**Created**: 2026-06-27

**Status**: Draft

**Input**: User description: "Lets add a set of sample projects, that will validate the behavior of Indago, and so that we can provide those examples in the documentation. We should have three general purpose libraries with an example set of 3 to 6 services for each library. Those libraries will then be consumed by different kinds of applications, showing how the system works. We should have a host application for the following: Console App, Web App (just minimal apis, for demo purposes), Blazor Web Assembly, MAUI. Each host application should just be a simple demonstration that the Scrutor assembly scanning works. They should also demonstrate that AOT publishing works. The AOT publish should be a test run through the modular pipelines as well. Lets place these samples at the root in `samples/`."

## Clarifications

### Session 2026-06-27

- Q: How should the "deliberately broken host" AOT-regression test (SC-003) be structured so the build reliably fails when an Indago-attributable trim/AOT warning appears? → A: A permanent dedicated negative-fixture host that intentionally emits an Indago-attributable trim/AOT warning. The pipeline runs its AOT publish with an inverted assertion: the step passes only if that publish fails as expected, and fails the build if the warning ever stops appearing. The fixture is excluded from the "normal" AOT-publish steps.
- Q: How is the AOT publish matrix ("each supported runtime target") defined? → A: A single deterministic matrix per AOT publish: the latest TFM (`net10.0`) × the current-OS RID (the RID of whatever OS the CI job runs on, e.g. `linux-x64` on Linux CI). The sample libraries and other non-AOT concerns may still multi-target `net8.0;net10.0` as the repo does, but the AOT publish steps specifically use `net10.0` × current-OS RID only.
- Q: What is the MAUI target-platform scope in CI for the AOT/trim publish pipeline? → A: The MAUI AOT/trim publish pipeline attempts a single CI-friendly MAUI platform, `net10.0-android`, as the genuinely-exercised target (publish must complete with no Indago-attributable trim/AOT warnings). The iOS, MacCatalyst, and Windows MAUI targets are explicitly skip-with-reason. This preserves the "skipped ≠ passed" guarantee while still proving Indago survives MAUI trimming on at least one real platform.
- Q: How should each host demonstration assert the expected service set, and how does the ModularPipelines run step detect pass/fail? → A: Every host asserts in-process against an expected service set and exits nonzero (or fails its run/health check) on any mismatch; the ModularPipelines run step treats exit code as pass/fail. Console/Web hosts assert at startup; Blazor WASM and MAUI hosts assert and surface a failure state the pipeline can detect.
- Q: How should Indago-attributable AOT/trim warnings be distinguished from third-party ones so the build fails reliably? → A: Zero-warning policy. The demonstration hosts (Console/Web/Blazor-WASM/MAUI-android) must publish with ZERO trim/AOT warnings of any kind — each host's own publish closure is required-to-be-clean, so ANY trim/AOT warning (Indago or third-party) fails the build (warnings-as-errors for the publish). The dedicated negative-fixture host is the ONLY project permitted to warn, and its inverted-assertion step requires it to warn. This constrains the sample closure: demo libraries/hosts must keep their dependency closure AOT/trim-clean (limits what third-party packages they may pull in).

#### Documentation build pipeline (folded in 2026-06-27)

- Q: This docs-build change isn't covered by any existing spec — how should it be handled? → A: Fold it into this 004 feature (both touch the ModularPipelines build). Add a named docs module to `build/Build.cs`.
- Q: What should the new ModularPipelines docs module actually do? → A: Both — API-reference generation AND the full Astro/Starlight `npm` site build, with the built static site dropped into the artifacts directory (`artifacts/docs`).
- Q: The separate `dotnet publish` step is being removed — what build output should the API generator (xmldocmd) consume instead? → A: The project's normal Release build output (bin). Build the solution (or the Indago project) and point xmldocmd at `src/Indago/bin/Release/net8.0/Indago.dll` plus its sibling `Indago.xml`; no separate publish to a temp directory.
- Q: What is the blast radius for the existing tooling (mise `docs:api`/`docs:build` and the standalone `deploy-docs.yml`)? → A: Replace them. Route docs generation/build through the pipeline module; remove the standalone publish step and the separate `deploy-docs.yml`. Docs build becomes part of the main CI pipeline (`build.yml` → `mise run build`), and the GitHub Pages deploy runs only when the branch is `main`. The dev-time VS Code task (Astro dev server) is kept for local development.
- Q: Where should the actual GitHub Pages deploy (gated to `main`) live? → A: GitHub Actions handles deploy. The pipeline always produces `artifacts/docs` and stays deploy-agnostic; `build.yml` runs the official Pages actions (`upload-pages-artifact` + `deploy-pages`) gated to `main`.

## User Scenarios & Testing _(mandatory)_

### User Story 1 - Reusable sample libraries demonstrating Indago scanning patterns (Priority: P1)

As an Indago maintainer and documentation author, I want a small set of shared, general-purpose sample libraries whose services are discovered by Indago's compile-time scanning, so I have realistic, reusable building blocks that prove scanning works and can be referenced from documentation.

**Why this priority**: The shared libraries are the foundation every host application consumes. Without them, no host can demonstrate scanning. They are the smallest slice that, on its own, proves the core promise of Indago (compile-time discovery of services) in a realistic multi-library arrangement.

**Independent Test**: Build the sample libraries together with a minimal consumer, then assert that Indago's generated provider registers exactly the expected set of services from each library, with the expected service/implementation mappings and lifetimes — without any runtime reflection.

**Acceptance Scenarios**:

1. **Given** three sample libraries each exposing between 3 and 6 services, **When** a consumer scans them via Indago fluent selectors (e.g. `FromAssemblyOf<>().AddClasses().AsMatchingInterface()`), **Then** every intended service from every library is registered and resolvable, and no unintended types are registered.
2. **Given** the sample libraries collectively illustrate more than one registration style (interface matching, attribute-based registration, and varied lifetimes), **When** scanning runs, **Then** each style resolves to the expected lifetime and implementation.
3. **Given** a type explicitly marked to be excluded from scanning, **When** scanning runs, **Then** that type is not registered.

---

### User Story 2 - Console host proves scanning and AOT end-to-end (Priority: P1)

As an Indago maintainer, I want a Console host application that consumes the sample libraries, runs Indago scanning, and can be published with Native AOT, so I have the simplest possible end-to-end demonstration that scanning survives AOT/trimming.

**Why this priority**: A console app is the lowest-friction host with the fewest platform dependencies, making it the most reliable proof that scanning + AOT publishing work together. Combined with User Story 1 it forms a complete, demonstrable MVP.

**Independent Test**: Run the console host and confirm it asserts in-process that the expected services from all three libraries were discovered and resolved—exiting nonzero on any mismatch—and prints the discovery result; then publish it with Native AOT for the AOT publish target (latest TFM `net10.0` × the current-OS RID) and confirm the published binary runs, asserts successfully (exit code 0), produces the same discovery result, and publishes with zero trim/AOT warnings. The ModularPipelines run step treats the exit code as the pass/fail signal.

**Acceptance Scenarios**:

1. **Given** the console host references the three sample libraries, **When** it runs, **Then** it demonstrates that the expected services are discovered and resolved through the Indago-generated provider and exits successfully.
2. **Given** the console host, **When** it is published with Native AOT, **Then** the publish completes with zero trim/AOT warnings of any kind (the publish closure is clean) and the published executable runs successfully.
3. **Given** `IIndagoProvider.EntryAssembly`-based access, **When** the host queries the entry assembly's discovered types, **Then** the result matches the expected set.

---

### User Story 3 - AOT publish of samples is exercised as a build pipeline test (Priority: P1)

As an Indago maintainer, I want the AOT publish of the sample hosts to be executed and verified as a step in the ModularPipelines build (`mise run build`), so regressions in AOT/trim compatibility are caught automatically in CI rather than discovered manually.

**Why this priority**: AOT/trim safety is a non-negotiable project principle. Manual sample apps that nobody publishes provide no protection against regressions. Making the AOT publish a pipeline-verified test is what turns the samples into a guardrail.

**Independent Test**: Invoke the build pipeline and confirm it includes a stage that AOT-publishes the AOT-capable sample hosts, fails the build if any publish fails or emits any trim/AOT warning (zero-warning policy), and reports the result as a discrete, named pipeline step.

**Acceptance Scenarios**:

1. **Given** the ModularPipelines build, **When** it runs, **Then** it AOT-publishes each AOT-capable sample host as an explicit pipeline step for the AOT publish target (latest TFM `net10.0` × the current-OS RID).
2. **Given** a sample host that fails to AOT-publish or emits any trim/AOT warning (Indago-attributable or third-party), **When** the pipeline runs, **Then** the pipeline step fails and the overall build fails.
3. **Given** a successful run, **When** the pipeline completes, **Then** the AOT publish result is surfaced as a clearly identifiable pass/fail outcome in the build output.

---

### User Story 4 - Web (minimal API) host demonstrates scanning and AOT (Priority: P2)

As a documentation author, I want a minimal-API web host that uses Indago scanning and supports AOT publishing, so the docs can show how Indago integrates with the most common web hosting style.

**Why this priority**: Minimal APIs are the most common ASP.NET Core demo surface and are AOT-friendly, making this the highest-value host after the console MVP.

**Independent Test**: Start the web host and confirm it asserts the expected service set at startup (failing its run/health check on any mismatch), call a demonstration endpoint that reports which services Indago discovered, and confirm the expected services are present; then AOT-publish the host and confirm it starts, passes its startup assertion, serves the same endpoint, and publishes with zero trim/AOT warnings. The ModularPipelines run step treats the host's startup/health outcome as the pass/fail signal.

**Acceptance Scenarios**:

1. **Given** the minimal-API host references the sample libraries, **When** it starts, **Then** the Indago-discovered services are registered in the dependency injection container and a demonstration endpoint reports them.
2. **Given** the minimal-API host, **When** it is published with Native AOT and started, **Then** it serves the demonstration endpoint successfully with zero trim/AOT warnings of any kind in its publish closure.

---

### User Story 5 - Blazor WebAssembly host demonstrates scanning (and AOT where supported) (Priority: P3)

As a documentation author, I want a Blazor WebAssembly host that uses Indago scanning and is published with WebAssembly AOT, so the docs cover client-side browser scenarios.

**Why this priority**: Blazor WASM showcases Indago in a trimming-heavy, browser-hosted environment, but it is lower priority than server and console hosts because of its heavier toolchain and longer publish times.

**Independent Test**: Load the Blazor WASM host (in a browser or via its published output) and confirm it asserts the expected service set and surfaces a detectable failure state on any mismatch, with a page/component listing the Indago-discovered services from the sample libraries; confirm the WebAssembly AOT publish completes successfully with zero trim/AOT warnings of any kind. The ModularPipelines run step treats the host's surfaced pass/fail state as the pass/fail signal.

**Acceptance Scenarios**:

1. **Given** the Blazor WASM host references the sample libraries, **When** it loads, **Then** a component displays the expected set of Indago-discovered services.
2. **Given** the Blazor WASM host, **When** it is published with WebAssembly AOT, **Then** the publish completes with zero trim/AOT warnings of any kind in its publish closure.

---

### User Story 6 - MAUI host demonstrates scanning (and AOT where the platform supports it) (Priority: P3)

As a documentation author, I want a MAUI host that uses Indago scanning and exercises the platform's AOT/trimming publish path, so the docs cover cross-platform native application scenarios.

**Why this priority**: MAUI broadens platform coverage but has the heaviest toolchain (per-platform workloads) and the most constrained Native AOT support, so it is the lowest-priority host and may have platform-dependent AOT scope.

**Independent Test**: Launch the MAUI host on a supported platform and confirm it asserts the expected service set and surfaces a detectable failure state on any mismatch, with a screen listing the Indago-discovered services; confirm the `net10.0-android` AOT/trimmed publish completes with zero trim/AOT warnings of any kind, while iOS/MacCatalyst/Windows targets are reported as skip-with-reason. The ModularPipelines run step treats the host's surfaced pass/fail state as the pass/fail signal.

**Acceptance Scenarios**:

1. **Given** the MAUI host references the sample libraries, **When** it launches, **Then** the UI displays the expected set of Indago-discovered services.
2. **Given** the MAUI host, **When** it is published for `net10.0-android` using the platform-appropriate AOT/trimming configuration, **Then** the publish completes with zero trim/AOT warnings of any kind in its publish closure.
3. **Given** the iOS, MacCatalyst, and Windows MAUI targets, **When** the AOT publish pipeline runs in CI, **Then** each is reported as explicitly "skipped" with an out-of-CI-scope reason and never as "passed".

---

### User Story 7 - Documentation build runs through the ModularPipelines build (Priority: P2)

As a maintainer, I want API-reference generation and the documentation site build to run as a named module in the ModularPipelines build (`mise run build`) that drops the built site into the artifacts directory, so docs are built the same way locally and in CI, the separate publish step is gone, and deployment is a thin CI concern gated to `main`.

**Why this priority**: It removes a parallel, drift-prone docs toolchain (a separate `dotnet publish` + standalone workflow) and unifies docs into the canonical build, but the demonstration samples (P1) remain the core of the feature.

**Independent Test**: Run `mise run build` on a non-`main` branch; confirm a named docs module generates the API reference from the project bin output and produces the built static site under `artifacts/docs`, with no separate `dotnet publish` invoked, and that no GitHub Pages deploy occurs. Confirm the dev-time VS Code task still starts the Astro dev server.

**Acceptance Scenarios**:

1. **Given** a clean checkout, **When** `mise run build` runs, **Then** a named docs module generates the API reference from `src/Indago/bin/Release/net8.0/Indago.dll` + `Indago.xml` and builds the Starlight site into `artifacts/docs`.
2. **Given** the docs module ran, **When** the build completes, **Then** no separate `dotnet publish` of Indago for docs occurred — the API reference was generated from the normal Release build output.
3. **Given** a push to a non-`main` branch, **When** CI runs, **Then** the docs are built (and available as a build artifact) but NOT deployed to GitHub Pages; **And given** a push to `main`, **When** CI runs, **Then** the built `artifacts/docs` is deployed to GitHub Pages via the official Pages actions.

### Edge Cases

- **MAUI Native AOT constraints**: Native AOT publishing is not uniformly available for every MAUI target platform, and per-platform workloads may be unavailable in some CI environments. The pipeline genuinely exercises exactly one CI-friendly MAUI platform (`net10.0-android`) and explicitly skips iOS, MacCatalyst, and Windows with a logged out-of-CI-scope reason rather than silently passing or hard-failing the whole build.
- **Workload availability in CI**: When a required SDK workload (e.g. `wasm-tools`, MAUI workloads) is not installed, the corresponding AOT publish step must report a clear, actionable message and be treated as a skipped (not silently passed) step.
- **Empty/over-broad scans**: A host that accidentally scans the wrong assembly (or scans nothing) must produce a visible failure in its demonstration assertion, not a false "success".
- **AOT publish target vs. multi-targeting**: The sample libraries (and other non-AOT concerns) may multi-target `net8.0;net10.0` as the repo does, but each AOT publish step runs a single deterministic matrix—latest TFM `net10.0` × the current-OS RID—so a regression on the AOT publish target is never masked, and the AOT matrix does not fan out per legacy TFM.
- **Excluded/opt-out types**: Types marked to be excluded from scanning must remain excluded across every host and every publish mode.
- **New host, no extra wiring**: Adding a new sample host that consumes the shared libraries must not require changes to the libraries themselves.
- **Negative-fixture regression guard**: The dedicated negative-fixture host must continue to emit its Indago-attributable trim/AOT warning. If a future change causes that warning to disappear (e.g. the fixture stops triggering the regression path), the inverted-assertion step must fail the build, signalling that the regression detector can no longer prove failures are caught—rather than silently passing.
- **Clean dependency closure constraint (zero-warning policy)**: Because each demonstration host's AOT publish must be entirely warning-free (any trim/AOT warning, Indago or third-party, fails the build), the sample libraries and hosts must keep their full dependency closure AOT/trim-clean. This constrains which third-party packages the demo libraries/hosts may reference—any package that emits trim/AOT warnings under the target publish would break the build and must be avoided, replaced, or have its warnings legitimately resolved. The negative-fixture host is the sole exception (it is required to warn).
- **Docs build consumes bin, not publish**: The API reference is generated from the project's Release build output (`src/Indago/bin/Release/net8.0/`), so the docs module depends on a prior Release build producing both `Indago.dll` and `Indago.xml` (XML documentation generation enabled). If the XML doc file is missing, the docs module MUST fail clearly rather than emit an empty or partial API reference.
- **Pages deploy gating**: Documentation is built on every CI run, but the GitHub Pages deploy MUST execute only from `main`. A misconfiguration that deploys from other branches—or fails to deploy from `main`—is a defect.

## Requirements _(mandatory)_

### Functional Requirements

#### Sample libraries

- **FR-001**: The feature MUST provide exactly three general-purpose sample libraries located under a `samples/` directory at the repository root.
- **FR-002**: Each sample library MUST expose between 3 and 6 services (service/implementation pairs) intended to be discovered by Indago.
- **FR-003**: The three libraries together MUST illustrate more than one Indago registration style (at minimum: interface-matching registration, attribute-based registration, and at least two distinct service lifetimes).
- **FR-004**: At least one sample library MUST include a type explicitly opted out of scanning, to demonstrate exclusion behavior.
- **FR-005**: The sample libraries MUST be independently reusable by every host application without per-host modification to the libraries.

#### Host applications

- **FR-006**: The feature MUST provide four host applications under `samples/`: a Console host, a Web host using minimal APIs only, a Blazor WebAssembly host, and a MAUI host. The MAUI host's CI-exercised target platform MUST be `net10.0-android`; its other MAUI target platforms (iOS, MacCatalyst, Windows) are out of scope for the AOT/trim publish pipeline and are explicitly skip-with-reason.
- **FR-007**: Each host application MUST consume the shared sample libraries and use Indago's compile-time scanning to discover and register their services (no runtime reflection-based scanning).
- **FR-008**: Each host application MUST assert in-process against an expected service set and provide a visible demonstration that the expected services were discovered and resolved (e.g. console output, an HTTP endpoint, a rendered component, or a UI screen), suitable for inclusion in documentation.
- **FR-009**: Each host application MUST be a minimal demonstration focused solely on showcasing Indago scanning and AOT compatibility, avoiding unrelated application complexity.
- **FR-010**: Host demonstrations MUST fail visibly when the discovered service set does not match expectations, by exiting with a nonzero exit code (or failing the host's run/health check), so the samples function as behavior validation and not just examples. Console and Web hosts MUST perform this assertion at startup; Blazor WebAssembly and MAUI hosts MUST perform this assertion and surface a failure state the ModularPipelines run step can detect. The pipeline run step MUST treat the host's exit code (or detectable failure state) as the pass/fail signal.

#### AOT publishing

- **FR-011**: Each AOT-capable host application MUST be publishable using the AOT/trimming publish mode appropriate to its application type (Native AOT for console and minimal-API hosts, WebAssembly AOT for Blazor WASM, platform-appropriate AOT/trimming for MAUI). For the MAUI host, the AOT/trimming publish MUST be genuinely exercised on exactly one CI-friendly platform, `net10.0-android`, and that publish MUST be clean (zero trim/AOT warnings of any kind); the remaining MAUI platforms (iOS, MacCatalyst, Windows) are skip-with-reason rather than published.
- **FR-012**: An AOT/trimming publish of any demonstration host (Console, Web, Blazor WASM, MAUI-android) MUST complete with ZERO trim or AOT warnings of any kind — its own publish closure is required-to-be-clean. ANY trim/AOT warning (whether attributable to Indago, its generated output, or a third-party dependency) MUST be treated as a build failure (warnings-as-errors for the publish). The dedicated negative-fixture host (FR-020/FR-021) is the sole exception and is excluded from this rule.
- **FR-013**: AOT publishing MUST be validated against a single deterministic target per host—the latest TFM (`net10.0`) × the current-OS RID (the RID of whatever OS the CI job runs on, e.g. `linux-x64` on Linux CI)—where the host's toolchain supports it. The AOT publish steps MUST NOT fan out across the libraries' legacy multi-target (`net8.0`); legacy TFMs remain only for non-AOT build/test concerns.

#### Build pipeline integration

- **FR-014**: The ModularPipelines build (invoked via `mise run build`) MUST include explicit, named step(s) that AOT-publish the AOT-capable sample hosts.
- **FR-015**: The AOT publish pipeline step(s) MUST fail the build when any demonstration host's AOT publish fails or emits any trim/AOT warning (warnings-as-errors for the publish; zero-warning policy). This rule does not apply to the negative-fixture host, which is governed by the inverted assertion in FR-021.
- **FR-016**: The pipeline MUST skip an AOT publish for a given host/target with a clear, logged reason when the required toolchain/workload is unavailable in the current environment, and MUST distinguish "skipped" from "passed". For the MAUI host specifically, the non-CI MAUI platforms (iOS, MacCatalyst, Windows) MUST always be reported as skip-with-reason (out-of-CI-scope), while `net10.0-android` is the genuinely-published target subject to the normal pass/fail rules.
- **FR-017**: The AOT publish outcome MUST be surfaced in the build output as a clearly identifiable pass/fail (and skip) result per host/target.
- **FR-020**: The feature MUST provide a permanent dedicated negative-fixture host under `samples/` whose sole purpose is to intentionally emit an Indago-attributable trim/AOT warning during AOT publish. This fixture MUST be excluded from the "normal" AOT-publish steps and MUST NOT be treated as one of the four demonstration host applications.
- **FR-021**: The pipeline MUST AOT-publish the negative-fixture host as a discrete, named step using the same AOT publish target as the demonstration hosts (latest TFM `net10.0` × the current-OS RID) and an inverted assertion: the step MUST pass only when that publish warns/fails (i.e. the expected Indago-attributable trim/AOT warning is present), and MUST fail the build if the publish ever becomes clean or the expected warning stops appearing—so the regression detector itself is guarded against silently breaking. The negative-fixture host is the ONLY project exempt from the zero-warning policy (FR-012/FR-015) and is in fact required to warn.

#### Documentation

- **FR-018**: The samples MUST be referenceable from the project documentation as worked examples of Indago usage across host types, consistent with the project's documentation-as-deliverable principle.
- **FR-019**: Each host sample MUST make its Indago usage (the selector expressions and registration approach) easy to extract for documentation, without requiring the reader to run the sample.

#### Documentation build pipeline

- **FR-022**: The ModularPipelines build (`build/Build.cs`, invoked via `mise run build`) MUST include a dedicated, named docs module that performs both (a) API-reference generation and (b) the documentation site (Astro/Starlight) build.
- **FR-023**: The docs module MUST generate the API reference from the project's normal Release build output (bin)—pointing the API generator (xmldocmd) at `src/Indago/bin/Release/net8.0/Indago.dll` and its sibling `Indago.xml`—and MUST NOT perform a separate `dotnet publish` to a temporary directory for this purpose.
- **FR-024**: The docs module MUST place the built static documentation site into the artifacts directory (`artifacts/docs`), using the extensions' artifact settings/conventions (e.g. `ArtifactSettings.ArtifactsDirectory`).
- **FR-025**: The docs module MUST leverage the existing modular-pipelines-extensions building blocks (e.g. `ArtifactSettings`, `SolutionSettings`, the DotNet/Node command modules, `FolderExtensions`) rather than reimplementing build orchestration.
- **FR-026**: The standalone docs generation/publish tooling MUST be replaced by the pipeline module: the separate `dotnet publish`+xmldocmd step, the mise `docs:api`/`docs:build` tasks, and the standalone `deploy-docs.yml` workflow MUST be removed or rerouted through the pipeline (a thin mise/CI wrapper that invokes the pipeline is acceptable).
- **FR-027**: Documentation generation and the site build MUST become part of the main CI pipeline (`build.yml` → `mise run build`) and MUST run on every CI build, while the GitHub Pages deploy MUST be gated to run only from the `main` branch.
- **FR-028**: The GitHub Pages deploy MUST remain a CI (GitHub Actions) concern using the official Pages actions (`upload-pages-artifact` + `deploy-pages`) consuming `artifacts/docs`; the ModularPipelines build itself MUST stay deploy-agnostic (it produces the artifact but does not deploy).
- **FR-029**: A dev-time entry point for iterating on docs (the Astro/Starlight dev server, surfaced as a VS Code task) MUST be retained for local development.

### Key Entities _(include if feature involves data)_

- **Sample Library**: A reusable, general-purpose library under `samples/` containing 3–6 discoverable services and demonstrating one or more registration styles. Consumed by all host applications.
- **Sample Service**: A service/implementation pair within a sample library that Indago is expected to discover, with a defined lifetime and registration style; some are explicitly excluded to demonstrate opt-out.
- **Host Application**: A runnable demonstration (Console, Web minimal API, Blazor WASM, or MAUI) that consumes the sample libraries, performs Indago scanning, and exposes a visible discovery result.
- **AOT Publish Validation**: A pipeline-executed verification that a demonstration host can be AOT/trim published for its supported targets with zero trim/AOT warnings of any kind (clean publish closure), reported as pass/fail/skip.
- **Negative-Fixture Host**: A permanent sample host under `samples/` that intentionally emits an Indago-attributable trim/AOT warning during AOT publish. It is excluded from the normal AOT-publish steps and is validated by an inverted-assertion pipeline step that passes only when its publish fails as expected, ensuring the regression detector remains effective.
- **Docs Build Module**: A named ModularPipelines module in `build/Build.cs` that generates the API reference from the Indago project's Release bin output (`Indago.dll` + `Indago.xml`) and builds the Astro/Starlight site, emitting the built static site to `artifacts/docs`. It replaces the standalone publish+xmldocmd step and the separate deploy workflow, and stays deploy-agnostic (CI handles the Pages deploy, gated to `main`).

## Success Criteria _(mandatory)_

### Measurable Outcomes

- **SC-001**: All three sample libraries exist under `samples/`, each exposing 3–6 discoverable services, and together cover at least three registration styles/lifetimes.
- **SC-002**: All four host applications (Console, Web minimal API, Blazor WASM, MAUI) build, run, and visibly demonstrate the correct, complete set of Indago-discovered services from every sample library; each host asserts the expected service set in-process and signals mismatch via a nonzero exit code (Console/Web at startup) or a detectable failure state (Blazor WASM/MAUI), which the ModularPipelines run step interprets as the host's pass/fail outcome.
- **SC-003**: Running the ModularPipelines build executes the AOT publish validation as discrete, named step(s), and a permanent dedicated negative-fixture host—one that intentionally emits an Indago-attributable trim/AOT warning—is validated with an inverted assertion (its AOT publish step passes only when that publish warns/fails as expected). If the expected warning ever stops appearing, the negative-fixture step and the overall build fail. The negative-fixture host is excluded from the "normal" AOT-publish steps and is the only project exempt from the zero-warning policy.
- **SC-004**: For every demonstration host, an AOT/trim publish completes for the AOT publish target (latest TFM `net10.0` × the current-OS RID) with ZERO trim/AOT warnings of any kind (Indago-attributable or third-party); any warning fails the build.
- **SC-005**: When a required AOT toolchain/workload is unavailable, the pipeline reports the affected host/target as explicitly "skipped" with a reason, and never as "passed". For the MAUI host, exactly one platform (`net10.0-android`) is reported as a genuine pass/fail AOT/trim publish result, while iOS, MacCatalyst, and Windows are always reported as "skipped" with an out-of-CI-scope reason.
- **SC-006**: A maintainer can determine the AOT publish pass/fail/skip status of every sample host from the build output without re-running anything manually.
- **SC-007**: Each host sample's Indago usage is documented (or directly referenceable) so a reader can understand the scanning approach without executing the sample.
- **SC-008**: Running `mise run build` produces the built documentation site under `artifacts/docs` via a named docs module, with the API reference generated from the project bin output and no separate `dotnet publish` step for docs.
- **SC-009**: The standalone docs publish path is gone: there is no separate `deploy-docs.yml` build-and-publish of the docs, and the mise `docs:api`/`docs:build` tasks no longer perform an independent `dotnet publish`+xmldocmd outside the pipeline (a thin wrapper that calls the pipeline is acceptable).
- **SC-010**: Documentation is built on every CI run, but a GitHub Pages deployment occurs only for the `main` branch; non-`main` builds produce the docs artifact without deploying.
- **SC-011**: A developer can still start the docs dev server locally via the retained VS Code task for iterative authoring.

## Assumptions

- "Scrutor assembly scanning" in the user description refers to Indago's Scrutor-style compile-time assembly scanning; Indago is the AOT-friendly replacement for runtime reflection-based scanning, and the samples demonstrate Indago's scanning.
- The AOT publish steps use a single deterministic matrix: the latest TFM (`net10.0`) × the current-OS RID (the RID of whatever OS the CI job runs on, e.g. `linux-x64` on Linux CI). The sample libraries and other non-AOT concerns may still multi-target `net8.0;net10.0` as the repo does, but the AOT publish steps do not fan out per legacy TFM or across multiple RIDs. MAUI and Blazor WASM target frameworks follow their respective platform requirements rather than being forced to match exactly.
- The three libraries are "general purpose" with intentionally distinct, lightweight domains chosen only to make the discovered services easy to distinguish in demonstrations; the specific domains carry no functional requirement beyond illustrating different scanning patterns.
- Native AOT is the intended AOT mode for the console and minimal-API hosts; Blazor WASM uses WebAssembly AOT; MAUI uses the platform-appropriate AOT/trimming publish path on its single CI-exercised platform `net10.0-android`, and the other MAUI platforms (iOS, MacCatalyst, Windows) are out of CI scope and skip-with-reason.
- The AOT publish validation runs through the existing ModularPipelines build (`mise run build`, `build/Build.cs`) rather than introducing a separate orchestrator; the Nuke release pipeline is out of scope for this feature.
- Samples are intended primarily as validation and documentation artifacts; they are not packaged or published as NuGet packages and are excluded from the shipped product surface.
- CI environments may lack some workloads (e.g. MAUI per-platform workloads, `wasm-tools`); the pipeline is expected to skip-with-reason in those cases rather than fail, while still failing on genuine Indago AOT regressions where the toolchain is present.
- Host demonstrations may assert discovery results in-process (failing on mismatch) so the samples double as behavior validation, consistent with the project's test-first and AOT-safety principles.
- A strict zero-warning policy applies to every demonstration host's AOT/trim publish: the publish closure must be entirely free of trim/AOT warnings (treated as warnings-as-errors), so the sample libraries and hosts must only depend on third-party packages whose closure is AOT/trim-clean under the `net10.0` × current-OS RID publish target. The dedicated negative-fixture host is the sole project exempt from this policy and is required to warn.
- The documentation build is folded into this feature: a named docs module in `build/Build.cs` runs API-reference generation and the Astro/Starlight site build, emitting the built site to `artifacts/docs`. This replaces the standalone `dotnet publish`+xmldocmd step, the mise `docs:api`/`docs:build` tasks, and the separate `deploy-docs.yml` workflow.
- The API reference is generated from the project's normal Release build output (`src/Indago/bin/Release/net8.0/Indago.dll` + `Indago.xml`), which assumes XML documentation generation is enabled for the Indago project; no separate publish is performed for docs. (Verified: the Release bin already produces `Indago.xml`.)
- GitHub Pages deployment remains a GitHub Actions concern (official `upload-pages-artifact` + `deploy-pages`) gated to `main`; the ModularPipelines build stays deploy-agnostic and only produces the `artifacts/docs` output. The Astro dev server remains available as a VS Code task for local authoring.
