# Tasks: Migrate Documentation Site to Starlight

**Input**: Design documents from `specs/002-starlight-docs-migration/`

**Prerequisites**: [plan.md](plan.md) | [spec.md](spec.md) | [research.md](research.md) | [data-model.md](data-model.md) | [contracts/url-structure.md](contracts/url-structure.md) | [quickstart.md](quickstart.md)

**Tests**: No dedicated test tasks — this is a documentation infrastructure migration. Validation is performed via build-pass checks, link validator, and quickstart.md scenarios.

**Organization**: Tasks grouped by User Story to enable independent implementation and validation.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies on incomplete tasks)
- **[Story]**: Which user story this task belongs to (US1–US6)

---

## Phase 1: Setup (Astro Project Initialization)

**Purpose**: Initialize the Astro/Starlight project and remove VitePress. These tasks create the foundation that all user stories depend on.

- [x] T001 Remove `docs/.vitepress/` directory, `docs/api-examples.md`, and `docs/markdown-examples.md`
- [x] T002 Create `docs/package.json` with `astro`, `@astrojs/starlight`, and dev dependencies (Node 20+)
- [x] T003 [P] Create `docs/astro.config.mjs` with Starlight integration, site title "Specular", description, and GitHub social link
- [x] T004 [P] Create `docs/src/content/config.ts` defining the `docs` content collection schema (including `draft` and `tags` fields per data-model.md)
- [x] T005 [P] Create `docs/tsconfig.json` extending Astro's base tsconfig

---

## Phase 2: Foundational (Content Recovery + CI Wiring — Blocks All User Stories)

**Purpose**: Recover all existing docs content from git, wire the Astro build into mise and CI. No user story work can begin until this phase is complete.

**⚠️ CRITICAL**: Complete this phase before any user story work begins.

- [x] T006 Recover historical docs content from git at commit `0f4311b`: `git checkout 0f4311b -- docs/guide/ docs/reference/ docs/architecture/ docs/index.md` then move all files into `docs/src/content/docs/` (maintain directory structure)
- [x] T007 Convert `docs/src/content/docs/index.md` to `index.mdx` and rewrite frontmatter from VitePress `layout: home` format to Starlight `template: splash` format (see data-model.md for target schema)
- [x] T008 Configure base sidebar in `docs/astro.config.mjs` using autogenerate groups for `guide/`, `reference/`, and `architecture/` to verify all migrated pages appear
- [x] T009 [P] Update `mise.toml`: replace VitePress `docs` task with `cd docs && npm run dev`; add `docs:build` and `docs:api` stubs
- [x] T010 [P] Update `.github/workflows/deploy-docs.yml`: replace VitePress build steps with `cd docs && npm ci && npm run build`; add `dotnet restore && dotnet build -c Release` prerequisite step before docs build
- [x] T011 ~~Create `docs/.env.example` documenting Typesense env vars~~ — **REMOVED**: Typesense deferred; `.env.example` deleted
- [x] T012 Verify foundation: run `mise run docs` — dev server starts at localhost:4321, all recovered pages render without errors

**Checkpoint**: Foundation ready — user story implementation can now begin.

---

## Phase 3: User Story 1 — API Reference (Priority: P1) 🎯 MVP

**Goal**: Auto-generated API reference pages for all public types in `src/Specular`, regenerated automatically after `dotnet build`.

**Independent Test**: Run `mise run docs:api` (after `dotnet build -c Release`), then open `http://localhost:4321/api/` — all public types from `src/Specular` must appear with at least a name and summary. (mise installs xmldocmd automatically.)

- [x] T013 [US1] Add `xmldocmd` to `.config/mise.toml` under `[tools]` as `"dotnet:xmldocmd" = "2.9.0"`; mise manages the tool version (no `.config/dotnet-tools.json` manifest)
- [x] T014 [US1] Create `mise run docs:api` task in `mise.toml`: runs `xmldocmd src/Specular/bin/Release/net8.0/Specular.dll docs/src/content/docs/api/ --namespace Specular --source https://github.com/RocketSurgeonsGuild/Specular/blob/main/src/Specular/`
- [x] T015 [US1] Run `dotnet build -c Release && mise run docs:api` to produce initial API reference output in `docs/src/content/docs/api/`
- [x] T016 [US1] Add `api/` topic group to sidebar config in `docs/astro.config.mjs` (label: "API Reference", autogenerate from `api/`)
- [x] T017 [P] [US1] Verify `ISpecularProvider` page renders in dev server with correct summary, methods, and parameter descriptions
- [x] T018 [P] [US1] Verify `ServiceRegistrationAttribute` and `ExcludeFromSpecularAttribute` pages render with summaries and constructor parameters
- [x] T019 [US1] Update `mise run docs:build` task in `mise.toml` to run `docs:api` before the Astro build: `mise run docs:api && cd docs && npm run build`
- [x] T020 [US1] Update `.github/workflows/deploy-docs.yml` to include `mise run docs:api` step before the Astro build step (mise installs xmldocmd automatically; no `dotnet tool restore` needed)

**Checkpoint**: US1 complete — API reference auto-generates from compiled DLLs with zero manual steps.

---

## Phase 4: User Story 2 — Guides, Search & Draft Management (Priority: P2)

**Goal**: All guide, reference, and architecture content is readable, draft pages excluded from production, and GitHub alert callouts render correctly. (Typesense search deferred — removed from scope.)

**Independent Test**: Mark any page `draft: true`, run build — page absent from nav. Open a page with `> [!NOTE]` — styled callout renders.

- [x] T021 [US2] ~~Install `starlight-docsearch-typesense`~~ — **REMOVED**: Typesense deferred; package uninstalled from `docs/package.json`, import and env-var block removed from `docs/astro.config.mjs`
- [x] T022 [US2] ~~Create Typesense Cloud account~~ — **REMOVED**: Typesense deferred; `TYPESENSE_*` env vars removed from `.github/workflows/deploy-docs.yml` and `.env.example` deleted
- [x] T023 [US2] ~~Configure `starlight-docsearch-typesense`~~ — **REMOVED**: see T021
- [x] T024 [US2] ~~Add Typesense index rebuild step~~ — **REMOVED**: see T022
- [x] T025 [US2] Install `starlight-auto-drafts`; add to plugins array in `docs/astro.config.mjs`
- [x] T026 [P] [US2] Install `starlight-github-alerts`; add to plugins array in `docs/astro.config.mjs`
- [x] T027 [P] [US2] Audit all recovered content files in `docs/src/content/docs/` for VitePress callout syntax (`:::tip`, `:::warning`, etc.); convert any found to GitHub alert format (`> [!TIP]`, `> [!WARNING]`)
- [x] T028 [US2] Install `starlight-sidebar-topics`; replace base sidebar in `docs/astro.config.mjs` with 6 topic groups: Getting Started, Guides, Reference, Architecture, API Reference, Changelog (see research.md Decision 6)
- [x] T029 [US2] Verify mobile layout at viewport ≤ 768px: sidebar accessible via menu, all content readable without horizontal scrolling (manual browser dev-tools check) — verified in spec 003 (T024): at 390×844 no horizontal scroll (scrollWidth 375 ≤ 390) and the menu button is present
- [x] T030 [US2] Verify dark mode auto-detection: set OS to dark mode, open site — renders in dark mode automatically — verified in spec 003 (T025): theme switches to dark (body bg `rgb(23,24,28)`); the theme control defaults to "Auto" (respects OS `prefers-color-scheme`)

**Checkpoint**: US2 complete — guides readable, search functional, drafts excluded, alerts render.

---

## Phase 5: User Story 3 — Maintainer CI/Workflow Integration (Priority: P3)

**Goal**: Broken links fail the build. `mise run docs` and `mise run docs:build` work from a clean checkout. New pages auto-appear in sidebar. CI pipeline is green.

**Independent Test**: Run `cd docs && npm run build` — build passes with link validator active. Add a new `.md` file under any content subdirectory — it appears in sidebar without editing `astro.config.mjs`.

- [x] T031 [US3] Install `starlight-links-validator`; add to plugins array in `docs/astro.config.mjs` with `errorOnRelativeLinks: false` (relative links within content are valid)
- [x] T032 [US3] Run `cd docs && npm run build` to trigger link validation; fix all broken internal links identified in output
- [x] T033 [US3] Verify `mise run docs` starts Astro dev server from a clean checkout (including `npm install` dependency); document any prerequisite in contributor README
- [x] T034 [US3] Verify `mise run docs:build` runs the full pipeline (dotnet build → docs:api → Astro build) without errors
- [~] T035 [US3] Test `.github/workflows/deploy-docs.yml` end-to-end (trigger via workflow_dispatch or push to branch; verify GitHub Pages deployment succeeds) — **follow-up**: workflow config verified in spec 003 (T029) — API generation reproduced locally and confirmed `xmldocmd --clean` preserves the hand-written `api/index.md`; `GH_API_TOKEN` is passed to the build. Actual GitHub Pages deployment is confirmable only after merge to `main` (the workflow triggers on push to `main`/`workflow_dispatch`); deferred to merge.
- [x] T036 [US3] Create a throwaway test page in `docs/src/content/docs/guide/test-page.md`, start dev server — verify it appears in sidebar automatically; then delete the test page

**Checkpoint**: US3 complete — CI green, link validation enforced, developer workflow functional.

---

## Phase 6: User Story 4 — Enhanced UX Plugins (Priority: P4)

**Goal**: All five UX/content enhancement plugins (heading-badges, image-zoom, scroll-to-top, page-actions, plugin-icons) plus tags and changelogs are installed and render correctly in both themes.

**Independent Test**: Open any page — scroll-to-top button visible on long pages, page-action buttons present, any heading with badge frontmatter shows the badge. `/tags/` lists all tagged pages. `/changelog/` renders the changelog.

- [x] T037 [P] [US4] Install `starlight-heading-badges`; add to plugins in `docs/astro.config.mjs`; add `badge: { text: "New", variant: "tip" }` to frontmatter of `docs/src/content/docs/guide/index.md` as demonstration
- [x] T038 [P] [US4] Install `starlight-image-zoom`; add to plugins in `docs/astro.config.mjs`
- [x] T039 [P] [US4] Install `starlight-scroll-to-top`; add to plugins in `docs/astro.config.mjs`
- [x] T040 [P] [US4] Install `starlight-page-actions`; configure in `docs/astro.config.mjs` with "Edit on GitHub" action pointing to `https://github.com/RocketSurgeonsGuild/Specular/edit/main/docs/src/content/docs/`
- [x] T041 [P] [US4] Install `starlight-plugin-icons`; add to plugins in `docs/astro.config.mjs`
- [x] T042 [US4] Install `starlight-tags`; add to plugins in `docs/astro.config.mjs`; add representative tags to at least 3 content pages (e.g., `tags: ["di", "aot"]` on quickstart page)
- [x] T043 [US4] Verify `/tags/` index page renders with all tagged pages listed and links work — verified in spec 003 (T016/T019): a wiring bug was **fixed** (extended the `docs` schema with `starlightTagsExtension`); `/tags/` now lists tags and `/tags/api/` shows 3 pages with working links + related tags
- [x] T044 [US4] Install `starlight-changelogs`; add to plugins in `docs/astro.config.mjs`; configure `changelogsLoader` in `docs/src/content.config.ts` using `github` provider (`owner: 'RocketSurgeonsGuild'`, `repo: 'Specular'`); no local `CHANGELOG.md` needed
- [x] T045 [US4] Verify `/changelog/` page renders the changelog in structured format in both light and dark mode — verified in spec 003 (T022/T027): `/changelog/` renders v0.0.2 (Latest) + v0.0.1 with PRs and contributors; build generates the changelog + version pages even without `GH_API_TOKEN` locally

**Checkpoint**: US4 complete — all 14 plugins installed; all render in light and dark mode (SC-007).

---

## Phase 7: User Story 5 — LLM/AI Discoverability (Priority: P5)

**Goal**: An `llms.txt` file is served at `/llms.txt` listing all non-draft documentation pages, regenerated automatically on every build.

**Independent Test**: Run `cd docs && npm run build`, then check `docs/dist/llms.txt` — file exists and lists all published pages with titles and URLs. Draft pages are absent.

- [x] T046 [US5] Install `starlight-llms-txt`; add to plugins in `docs/astro.config.mjs`
- [x] T047 [US5] Run production build; verify `docs/dist/llms.txt` exists and contains entries for all non-draft docs pages (minimum: index, guide/_, reference/_, architecture/_, api/_)
- [x] T048 [US5] Create a test draft page (`draft: true`), rebuild — verify it is absent from `llms.txt`; then delete the test page

**Checkpoint**: US5 complete — `llms.txt` auto-generated, AI tools can discover documentation.

---

## Phase 8: User Story 6 — VS Code Authoring Tooling (Priority: P5)

**Goal**: Contributors opening the project in VS Code are prompted to install `astro-build.astro-vscode` and `hideoo.starlight-links`. The starlight-links extension provides link autocomplete in Markdown files.

**Independent Test**: Open repo root in VS Code — "Install Recommended Extensions" notification appears referencing both extensions.

- [x] T049 [US6] Create `docs/.vscode/extensions.json` with recommendations: `["astro-build.astro-vscode", "hideoo.starlight-links"]`
- [x] T050 [US6] Add contributor authoring setup note to `docs/src/content/docs/guide/index.md` (or a dedicated contributing guide page): mention installing VS Code recommended extensions for link autocomplete and Astro syntax support
- [x] T051 [US6] Verify: open repo in VS Code — confirm "Recommended Extensions" notification appears; confirm `hideoo.starlight-links` provides link completion when editing a `.md` file inside `docs/src/content/docs/` — verified in spec 003 (T028): `.vscode/extensions.json` recommends both `astro-build.astro-vscode` and `hideoo.starlight-links`, so VS Code surfaces the prompt and provides link completion (config-verified; live IDE behavior is environment-dependent)

**Checkpoint**: US6 complete — contributors get link autocomplete and validation in VS Code.

---

## Phase 9: Polish & Cross-Cutting Concerns

**Purpose**: Final quality gates, formatting, URL contract verification, and constitution follow-up.

- [x] T052 [P] Run `prettier --check "docs/src/**/*.{md,mdx,ts,mjs}"` from repo root; fix any formatting violations found
- [x] T053 [P] Run all validation scenarios from [quickstart.md](quickstart.md): dev server, content pages, API generation, link validator, plugin rendering, search, build performance, VS Code extension, prettier — verified in spec 003 (T031): dev server reachable, all section pages resolve (HTTP 200), API generation reproduced (`xmldocmd --clean` preserves `api/index.md`), link validator clean (root + `/Specular` base), all 12 plugins rendered, production build green, prettier `--check` passes
- [x] T054 Verify all URL paths from [contracts/url-structure.md](contracts/url-structure.md) return HTTP 200 on the deployed site (or locally via `npm run preview`)
- [x] T055 Manual verification: open each of the 14 plugins' key features in browser — confirm all render correctly in both light and dark mode (SC-007) — completed in spec 003 (US2): all **12** installed `starlight-*` plugins verified Working/Fixed with evidence in `specs/003-docs-site-fixes/contracts/plugin-verification-matrix.md` (the "14" figure was a stale count; 12 are actually installed)
- [x] T056 Update `mise.toml` task descriptions to reference Astro/Starlight instead of VitePress in any comments or task names
- [x] T057 PATCH amendment: update `.specify/memory/constitution.md` Principle V — replace "VitePress docs site under `docs/`" and "`mise run docs` (VitePress)" with "Starlight docs site under `docs/`" and correct `mise run docs` description

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies — start immediately
- **Foundational (Phase 2)**: Depends on Phase 1 — **BLOCKS all user stories**
- **US1 (Phase 3)**: Depends on Phase 2 — no other user story dependencies
- **US2 (Phase 4)**: Depends on Phase 2 — can run in parallel with US1 if desired
- **US3 (Phase 5)**: Depends on Phase 2 — best run after US1 (link validator needs content populated)
- **US4 (Phase 6)**: Depends on Phase 2 — can run in parallel with US1–US3 (plugins are additive)
- **US5 (Phase 7)**: Depends on Phase 2 — can start any time after foundation
- **US6 (Phase 8)**: Independent of all user stories — can run any time after Phase 1
- **Polish (Phase 9)**: Depends on all desired user stories being complete

### User Story Dependencies

- **US1 (P1)**: Independent — start after Foundation
- **US2 (P2)**: Independent — start after Foundation (Typesense setup needed before US3 CI validation)
- **US3 (P3)**: Best after US1 + US2 — link validator needs full content set to validate
- **US4 (P4)**: Independent — plugins are additive, no content dependencies
- **US5 (P5)**: Independent — llms.txt generation is self-contained
- **US6 (P5)**: Independent — VS Code tooling is independent of all other stories

### Parallel Opportunities Within Phases

- **Phase 1**: T003, T004, T005 can run in parallel (different files)
- **Phase 2**: T009, T010 can run in parallel (mise.toml vs CI workflow)
- **Phase 3**: T017, T018 can run in parallel (different type verifications)
- **Phase 4**: T026, T027 can run in parallel (install vs content audit)
- **Phase 6**: T037, T038, T039, T040, T041 can all run in parallel (each is a separate plugin, different config additions)
- **Phase 9**: T052, T053 can run in parallel

---

## Parallel Execution Example: Phase 6 (US4 Plugins)

```text
# All five content-enhancement plugins are independent — install in parallel:
Task T037: Install starlight-heading-badges + add demo badge to guide/index.md
Task T038: Install starlight-image-zoom
Task T039: Install starlight-scroll-to-top
Task T040: Install starlight-page-actions + configure GitHub edit link
Task T041: Install starlight-plugin-icons

# Then sequentially:
Task T042: Install starlight-tags + tag representative pages
Task T043: Verify /tags/ index
Task T044: Install starlight-changelogs + github provider (pulls from GitHub Releases)
Task T045: Verify /changelog/ in light and dark mode
```

---

## Implementation Strategy

### MVP First (User Story 1 — API Reference)

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundation (CRITICAL)
3. Complete Phase 3: US1 — API reference from DLLs
4. **STOP and VALIDATE**: `mise run docs:api` → open `/api/` → all public types present
5. This alone delivers the highest-value user story (P1)

### Incremental Delivery

1. Phase 1 + 2 → Foundation + content recovered
2. Phase 3 → US1 (API reference) → **MVP deliverable**
3. Phase 4 → US2 (Search + guides + draft management)
4. Phase 5 → US3 (CI integration + link validation)
5. Phase 6 → US4 (14 plugins complete)
6. Phase 7 → US5 (llms.txt)
7. Phase 8 → US6 (VS Code tooling)
8. Phase 9 → Polish + constitution amendment

### Summary

| Phase        | Tasks     | User Story | Deliverable                   |
| ------------ | --------- | ---------- | ----------------------------- |
| 1 Setup      | T001–T005 | —          | Astro project initialized     |
| 2 Foundation | T006–T012 | —          | Content recovered, CI wired   |
| 3 US1        | T013–T020 | P1 🎯      | API reference from DLLs       |
| 4 US2        | T021–T030 | P2         | Search, guides, drafts        |
| 5 US3        | T031–T036 | P3         | CI, link validation, workflow |
| 6 US4        | T037–T045 | P4         | All 14 plugins complete       |
| 7 US5        | T046–T048 | P5         | llms.txt discoverability      |
| 8 US6        | T049–T051 | P5         | VS Code authoring tooling     |
| 9 Polish     | T052–T057 | —          | Quality gates + amendment     |

**Total tasks**: 57

---

## Notes

- `[P]` tasks = different files, no blocking dependencies — safe to run in parallel
- `[USN]` label maps each task to its user story for traceability
- No snapshot or unit tests required — this is docs infrastructure; validation is via build pass + quickstart.md scenarios
- Commit after each phase checkpoint to create safe rollback points
- `starlight-links-validator` (T031) will catch broken links introduced during migration — run it before finalizing content
- Typesense search (T021–T024) has been removed from scope and marked complete with REMOVED annotations
