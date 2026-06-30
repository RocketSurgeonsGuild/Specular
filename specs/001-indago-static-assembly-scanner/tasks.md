# Tasks: Indago VitePress Documentation Site

**Input**: Design documents from `specs/001-indago-static-assembly-scanner/`

**Prerequisites**: plan.md ✅ · spec.md ✅ · research.md ✅ · data-model.md ✅ · contracts/ ✅ · quickstart.md ✅

**Scope**: US3 — VitePress documentation site, oxlint linting toolchain, GitHub Pages deployment.
US1 (static assembly scanner) and US2 (cross-assembly caching) are separate implementation tracks
and are NOT covered here.

**Tests**: Not applicable for a documentation site. The VitePress build (`mise run docs-build`)
and oxlint check are the correctness gates. Validation scenarios from `quickstart.md` are
included as polish-phase tasks.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[US3]**: Tasks belonging to User Story 3 (Public Documentation Site)

---

## Phase 1: Setup

**Purpose**: Clean up the default VitePress scaffold and wire up shared tooling dependencies.
No user story work can begin until this phase is complete.

- [ ] T001 Remove scaffold placeholder files `docs/api-examples.md` and `docs/markdown-examples.md`
- [ ] T002 Add `"oxlint": "^0.x"` to devDependencies in `package.json` (root)
- [ ] T003 [P] Add `docs-build = { run = "vitepress build docs" }` task to `[tasks]` in `.config/mise.toml`
- [ ] T004 [P] Create `docs/tsconfig.json` scoped to `docs/**` with `target: ESNext`, `module: ESNext`, `moduleResolution: Bundler`, `strict: true`, `jsx: preserve`, `lib: [ESNext, DOM]`, `include: [".vitepress/**/*.ts", ".vitepress/**/*.mts", ".vitepress/**/*.vue"]`

---

## Phase 2: Foundational — Linting Toolchain

**Purpose**: Establish the oxlint + hk integration before any docs content is written, so
every subsequent commit is automatically linted.

⚠️ **CRITICAL**: Complete before Phase 3. The hk pre-commit hook must be active before content
commits begin.

- [ ] T005 Create `oxlintrc.json` at repo root per the contract in `specs/001-indago-static-assembly-scanner/contracts/vitepress-config.md` (oxlint contract section) — set `plugins: ["vue", "typescript"]`, `ignorePatterns` for `node_modules`, `docs/.vitepress/dist`, `docs/.vitepress/cache`, `apm_modules`, `.apm`
- [ ] T006 Add `oxlint` step to `.config/hk.pkl`: new `local oxlint: Step` with `glob = List("**/*.vue", "**/*.ts", "**/*.mts")`, exclude patterns matching T005 ignorePatterns, `check = "oxlint --plugin vue --plugin typescript {{files}}"`, `fix = "oxlint --fix --plugin vue --plugin typescript {{files}}"` — then add `["oxlint"] = oxlint` to the `linters` mapping

**Checkpoint**: Run `hk check` — must exit 0 with the existing `docs/.vitepress/theme/index.ts` and `config.mts`.

---

## Phase 3: US3 — Site Configuration & Home Page

**Goal**: A working VitePress site with correct nav, sidebar, and home page — ready for content.

**Independent Test** (Scenario 1 from quickstart.md):

```bash
mise run docs
# Navigate to http://localhost:5173/Indago/
# Verify: title "Indago", nav shows Guide / Reference / Architecture / GitHub, sidebar shows all sections
```

- [ ] T007 [US3] Rewrite `docs/.vitepress/config.mts` per the VitePress contract in `specs/001-indago-static-assembly-scanner/contracts/vitepress-config.md`: set `title: "Indago"`, `description: "Compile-time assembly scanning for .NET — AOT safe, zero reflection"`, `lang: "en-US"`, `base: "/Indago/"`, `cleanUrls: true`, `lastUpdated: true`, nav with Guide/Reference/Architecture/GitHub/NuGet links, sidebar object with `/guide/`, `/reference/`, `/architecture/` sections per `data-model.md`, `editLink`, `socialLinks`, `search: { provider: "local" }`
- [ ] T008 [P] [US3] Simplify `docs/.vitepress/theme/index.ts` to extend DefaultTheme with no custom layout slot boilerplate (remove the placeholder `Layout` wrapper; keep only `extends: DefaultTheme` and the empty `enhanceApp`)
- [ ] T009 [P] [US3] Rewrite `docs/index.md` as the home page: hero with `name: "Indago"`, `text: "Compile-time assembly scanning for .NET"`, `tagline: "AOT safe. Zero reflection. Build-time performance."`, two action buttons (brand: "Get Started" → `/guide/`, alt: "API Reference" → `/reference/iindago-provider`), three feature cards: "AOT Safe" / "Build-Time Scanning" / "Minimal API Surface"

**Checkpoint**: `mise run docs` renders without errors; nav and sidebar resolve without 404.

---

## Phase 4: US3 — Guide Section

**Goal**: Four guide pages covering the new-user journey from installation to AOT publishing.

**Independent Test** (Scenario 7 from quickstart.md): A developer can follow installation → quickstart → AOT guide without leaving the docs.

- [ ] T010 [P] [US3] Create `docs/guide/index.md` — "What is Indago?" page: explain the problem (runtime reflection cost, AOT incompatibility), the solution (Roslyn source generator emitting a strongly-typed provider), and the key concepts (`IIndagoProvider`, selector expressions, `IndagoProvider.ctpjson`). Read `src/Indago/IIndagoProvider.cs` for API shape. Include a one-paragraph architecture summary linking to `/architecture/how-it-works`.
- [ ] T011 [P] [US3] Create `docs/guide/installation.md` — NuGet install + project setup: `dotnet add package Indago`, wire `[assembly: IndagoProvider]` attribute, confirm generated file appears under `obj/`. Read `src/Indago/IndagoProviderAttribute.cs` for attribute usage. Include a minimal working csproj snippet.
- [ ] T012 [P] [US3] Create `docs/guide/quickstart.md` — First selector walkthrough: call `provider.GetTypes(s => s.FromAssemblyOf<Program>().AddClasses().AsImplementedInterfaces())`, register results with `AddIndagoServiceRegistrations`, show the generated code structure. Read `src/Indago/IIndagoProvider.cs` and `src/Indago/IndagoProviderServiceCollectionExtensions.cs`.
- [ ] T013 [P] [US3] Create `docs/guide/aot-publishing.md` — AOT/trim compatibility guide: `dotnet publish -r <rid> --self-contained -p:PublishAot=true`, explain why zero reflection matters, note both `net8.0` and `net10.0` targets. Read `src/Indago/IIndagoProvider.cs` doc comments. End with "Next steps" linking to Reference section.

**Checkpoint**: All four pages render in `mise run docs` dev server without broken links.

---

## Phase 5: US3 — Reference Section

**Goal**: Four API reference pages covering all public types.

**Independent Test**: Developer can look up `ServiceRegistrationAttribute` and find its purpose, parameters, and a usage example without opening source code.

- [ ] T014 [P] [US3] Create `docs/reference/iindago-provider.md` — `IIndagoProvider` API reference: document `GetAssemblies()`, `GetTypes()`, `Scan()`, `EntryAssembly`, `GetArgumentExpressionHash()`. Explain the `[CallerLineNumber]`/`[CallerFilePath]`/`[CallerArgumentExpression]` hidden parameters and why they matter. Read `src/Indago/IIndagoProvider.cs`.
- [ ] T015 [P] [US3] Create `docs/reference/type-filters.md` — Fluent selector API reference: document `IReflectionAssemblySelector`, `ITypeFilter`, `AddClasses()`, `AsImplementedInterfaces()`, `AsMatchingInterface()`, `AssignableTo<T>()`, `WithAttribute<T>()`, namespace scoping, type-kind filters. Read files in `src/Indago/Abstractions/`.
- [ ] T016 [P] [US3] Create `docs/reference/service-registration.md` — `ServiceRegistrationAttribute` and `RegistrationLifetimeAttribute` reference: purpose, constructors, `Singleton`/`Scoped`/`Transient` lifetime values, `AddIndagoServiceRegistrations` usage. Read `src/Indago/ServiceRegistrationAttribute.cs`, `src/Indago/RegistrationLifetimeAttribute.cs`, `src/Indago/IndagoProviderServiceCollectionExtensions.cs`.
- [ ] T017 [P] [US3] Create `docs/reference/exclude-from-indago.md` — `ExcludeFromIndagoAttribute` reference: when to use it, how it interacts with all three `IIndagoProvider` methods, an example. Read `src/Indago/ExcludeFromIndagoAttribute.cs`.

**Checkpoint**: All four pages render correctly; no dead links to guide or architecture pages.

---

## Phase 6: US3 — Architecture Section

**Goal**: Two conceptual pages explaining the generator pipeline and the cross-assembly cache.

**Independent Test** (US3 Scenario 2): A developer navigating to Architecture finds a clear explanation of `IndagoProvider.ctpjson` and `GeneratedHash`.

- [ ] T018 [P] [US3] Create `docs/architecture/how-it-works.md` — Generator pipeline overview: syntax providers (`AssemblyCollection`, `ReflectionCollection`, `ServiceDescriptorCollection`), selector expression hashing (`GetArgumentExpressionHash`), symbol visitor flow in `AssemblyProviders/`, code emission. Include a simple flow diagram in Mermaid. Read `src/Indago.Analyzers/CompiledTypeProviderGenerator.cs` for the pipeline structure.
- [ ] T019 [P] [US3] Create `docs/architecture/cross-assembly-caching.md` — Cross-assembly caching model: what `IndagoProvider.ctpjson` contains, how `GeneratedHash` in `IndagoProviderAttribute` is used for cache invalidation, the two-project (LibA → AppB) scenario, what happens when cache is missing or corrupt. Read `src/Indago.Analyzers/Configuration/` and `src/Indago/IndagoProviderAttribute.cs`.

**Checkpoint**: Both pages render in `mise run docs`; Mermaid diagram in T018 renders in the browser.

---

## Phase 7: US3 — GitHub Pages Deployment

**Goal**: Automated docs deployment on every merge to `main`.

**Independent Test** (Scenario 5 + 6 from quickstart.md): Workflow passes `actionlint`; after merge to `main`, the live site updates within 10 minutes.

- [ ] T020 [US3] Create `.github/workflows/deploy-docs.yml` per the deploy workflow contract in `specs/001-indago-static-assembly-scanner/contracts/vitepress-config.md`: trigger on `push` to `main` + `workflow_dispatch`, permissions `pages: write` + `id-token: write` + `contents: read`, concurrency `group: pages` with `cancel-in-progress: false`, `build` job using `actions/checkout@v7` with `fetch-depth: 0`, `jdx/mise-action@v4`, `mise run docs-build`, `actions/upload-pages-artifact@v3` pointing at `docs/.vitepress/dist`, `deploy` job with `actions/deploy-pages@v4` in `github-pages` environment

**Checkpoint**: `actionlint .github/workflows/deploy-docs.yml` exits 0.

---

## Phase 8: Polish & Validation

**Purpose**: Run through the quickstart.md validation scenarios to confirm all gates pass.

- [ ] T021 [P] Run Scenario 1 (dev server): `mise run docs` — verify home page renders at `http://localhost:5173/Indago/`, all nav links resolve, no console errors
- [ ] T022 [P] Run Scenario 2 (production build): `mise run docs-build` — verify exit 0, `docs/.vitepress/dist/` contains ≥10 HTML files, no dead-link warnings, no TypeScript errors from `config.mts`
- [ ] T023 [P] Run Scenario 3 (oxlint): `oxlint --plugin vue --plugin typescript docs/.vitepress` — verify exit 0, no errors or warnings
- [ ] T024 Run Scenario 4 (hk pre-commit): stage `docs/.vitepress/theme/index.ts` and attempt a commit — verify `hk fix` runs and the commit proceeds cleanly
- [ ] T025 [P] Run Scenario 5 (actionlint): `actionlint .github/workflows/deploy-docs.yml` — verify exit 0

---

## Dependencies & Execution Order

### Phase Dependencies

- **Phase 1 (Setup)**: No dependencies — start immediately
- **Phase 2 (Foundational)**: Depends on Phase 1 — **blocks all content work**
- **Phase 3 (Site Config)**: Depends on Phase 2
- **Phase 4 (Guide)**: Depends on Phase 3 (sidebar must be wired before pages are added)
- **Phase 5 (Reference)**: Can start alongside Phase 4 — all [P] within phase
- **Phase 6 (Architecture)**: Can start alongside Phases 4 & 5 — all [P] within phase
- **Phase 7 (Deployment)**: Can start after Phase 3 (independent of content pages)
- **Phase 8 (Polish)**: Depends on all prior phases complete

### Parallel Opportunities

- T003 and T004 (Phase 1) run in parallel — different files
- T005 and T006 (Phase 2) are sequential — T006 depends on T005 being complete
- T008, T009 (Phase 3) run in parallel after T007
- T010–T013 (Phase 4) all run in parallel — independent files
- T014–T017 (Phase 5) all run in parallel — independent files
- T018–T019 (Phase 6) run in parallel — independent files
- T020 (Phase 7) can run in parallel with Phases 4–6 after Phase 3 completes
- T021–T023, T025 (Phase 8) run in parallel; T024 is sequential

---

## Parallel Example: US3 Content Phases

```bash
# After Phase 3 is complete, launch all content pages in parallel:

# Guide section (Phase 4):
Task: "Create docs/guide/index.md"
Task: "Create docs/guide/installation.md"
Task: "Create docs/guide/quickstart.md"
Task: "Create docs/guide/aot-publishing.md"

# Reference section (Phase 5) — simultaneously:
Task: "Create docs/reference/iindago-provider.md"
Task: "Create docs/reference/type-filters.md"
Task: "Create docs/reference/service-registration.md"
Task: "Create docs/reference/exclude-from-indago.md"

# Architecture section (Phase 6) — simultaneously:
Task: "Create docs/architecture/how-it-works.md"
Task: "Create docs/architecture/cross-assembly-caching.md"

# Deploy workflow (Phase 7) — simultaneously:
Task: "Create .github/workflows/deploy-docs.yml"
```

---

## Implementation Strategy

### MVP First (US3 Core)

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational linting
3. Complete Phase 3: Site config + home page — **stop here to validate `mise run docs`**
4. Complete Phase 4: Guide section — **most important for new-user SC-003**
5. Deploy to GitHub Pages (Phase 7) — live site

### Incremental Delivery

1. Phases 1–3 → working site skeleton
2. Phase 4 (Guide) → SC-003 satisfied (quickstart)
3. Phases 5–6 (Reference + Architecture) → FR-010 satisfied (full docs)
4. Phase 7 → FR-011 + SC-006 satisfied (automated deployment)
5. Phase 8 → validation sign-off

---

## Notes

- Content pages (T010–T019) require reading the corresponding source files in `src/Indago/` and `src/Indago.Analyzers/` — file paths are listed in each task
- `mise run docs-build` is the primary correctness gate; a build with dead links exits non-zero
- `oxlint` is installed via `npm install` (triggered by `mise install` postinstall hook) — run `npm install` once after adding the package in T002
- The `fetch-depth: 0` in the deploy workflow (T020) is required for VitePress `lastUpdated` to work
- VitePress is installed via mise (`"npm:vitepress" = "v2.0.0-alpha.17"`) — not via npm; no `npm install vitepress` needed
