---
description: Task list for Docs Site Fixes & Plugin Verification
---

# Tasks: Docs Site Fixes & Plugin Verification

**Input**: Design documents from `/specs/003-docs-site-fixes/`

**Prerequisites**: plan.md, spec.md, research.md, data-model.md, contracts/, quickstart.md

**Tests**: This is a documentation feature. No automated unit/contract test tasks — verification is
performed live via the integrated browser MCP against the running dev server (port 4321) plus a clean
`astro build` with the link validator. Verification tasks are explicit below.

**Organization**: Tasks are grouped by user story (US1 → US2 → US3) so each can be delivered and
verified independently.

## Path Conventions

- Docs content: `docs/src/content/docs/`
- Site config: `docs/astro.config.mjs`, `docs/tags.yml`, `docs/content.config.ts`
- Tracking artifacts: `specs/003-docs-site-fixes/contracts/plugin-verification-matrix.md`

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Confirm the working environment and reconcile known unknowns before editing.

- [x] T001 Confirm the docs dev server is running and reachable at http://localhost:4321 (start with `cd docs && npm run dev` if not); confirm the integrated browser MCP can open the home page
- [x] T002 Reconcile the plugin count: list every `starlight-*` plugin actually present in `docs/astro.config.mjs` and update the row set in `specs/003-docs-site-fixes/contracts/plugin-verification-matrix.md` to match (resolve the "12 vs 14" note)
- [x] T003 Capture the current broken state as a baseline: in the browser, confirm `/reference/`, `/architecture/`, `/api/` 404 today (evidence for US1)

**Checkpoint**: Environment confirmed, plugin matrix rows match reality.

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Establish the shared template all landing pages follow.

**⚠️ CRITICAL**: Complete before US1 implementation.

- [x] T004 Review `docs/src/content/docs/guide/index.md` and record the landing-page convention (frontmatter keys `title`, `description`, optional `badge`/`tags`; body = intro + links into the section) to reuse for the three new landing pages

**Checkpoint**: Landing-page template understood — user stories can begin.

---

## Phase 3: User Story 1 - Every top-level navigation link resolves (Priority: P1) 🎯 MVP

**Goal**: Reference, Architecture, and API Reference topics resolve to real landing pages; build is
link-clean.

**Independent Test**: Navigate to all five topic URLs in the browser — each returns HTTP 200 with a
real page; `astro build` reports zero broken internal links.

### Implementation for User Story 1

- [x] T005 [P] [US1] Create `docs/src/content/docs/reference/index.md` — section landing page with intro and links to `exclude-from-indago`, `iindago-provider`, `service-registration`, `type-filters`
- [x] T006 [P] [US1] Create `docs/src/content/docs/architecture/index.md` — section landing page with intro and links to `how-it-works` and `cross-assembly-caching`
- [x] T007 [P] [US1] Create `docs/src/content/docs/api/index.md` — section landing page introducing the auto-generated API reference and linking into `Indago` / `Indago.Abstractions`
- [x] T008 [US1] Verify in the browser that `/reference/`, `/architecture/`, `/api/` now return real pages (HTTP 200) and that `/guide/` and `/changelog/` still render (no regression)
- [x] T009 [US1] Verify all in-page links on the three new landing pages navigate correctly in the browser
- [x] T010 [US1] Confirm `docs/scripts/add-api-frontmatter.mjs` (and/or the API generation/build) does not overwrite or delete `docs/src/content/docs/api/index.md`
- [x] T011 [US1] Run `cd docs && npm run build`; confirm success and that `starlight-links-validator` reports zero broken internal links
- [x] T012 [US1] Run `cd docs && GITHUB_ACTIONS=1 npm run build`; confirm the site is link-clean under the `/Indago` base path (no hard-coded base prefixes)

**Checkpoint**: All five navigation topics resolve; build is link-clean under both base paths (SC-001, SC-002, SC-006).

---

## Phase 4: User Story 2 - All installed plugins are confirmed working or removed (Priority: P2)

**Goal**: Every installed Starlight plugin is demonstrably Working or Fixed (removal only as a last
resort with a recorded reason), with evidence recorded in the matrix.

**Independent Test**: Each row of `plugin-verification-matrix.md` has evidence + a non-blank status;
the features are observable on the live site.

### Activation content (add what plugins need to be demonstrable)

- [x] T013 [P] [US2] Add a representative GitHub-style alert (`> [!NOTE]` / `> [!TIP]`) to one of the new landing pages to activate `starlight-github-alerts`
- [x] T014 [P] [US2] Add a heading-badge example to one of the new landing pages to activate `starlight-heading-badges`
- [x] T015 [P] [US2] Add one representative image to content to activate `starlight-image-zoom`, OR record in the matrix that no content image exists yet (documented limitation) — _Outcome_: no content images exist yet; recorded as a documented limitation (plugin wired, activates automatically once an image is added)
- [x] T016 [US2] Verify `starlight-tags` wiring: check whether `starlightTags()` auto-discovers `docs/tags.yml`; if `/tags/` is empty or missing, wire the config explicitly in `docs/astro.config.mjs` — _Outcome_: **bug fixed** — `tags.yml` auto-discovery worked but tag pages showed "0 pages" because the `docs` collection schema did not expose the `tags` frontmatter field; extended `docs/src/content.config.ts` with `docsSchema({ extend: starlightTagsExtension })`

### Live verification (record evidence + status in the matrix)

- [x] T017 [P] [US2] Verify `starlight-sidebar-topics` (topics render + resolve — covered by US1) and `starlight-links-validator` (clean build — T011); record evidence/status
- [x] T018 [P] [US2] Verify `starlight-github-alerts`, `starlight-heading-badges`, `starlight-image-zoom` render on the live site in light + dark mode; record evidence/status
- [x] T019 [P] [US2] Verify `starlight-tags` (`/tags/` lists tagged pages) and `starlight-page-actions` (edit link points to the GitHub edit URL); record evidence/status
- [x] T020 [P] [US2] Verify `starlight-scroll-to-top` (button on a long page), `starlight-plugin-icons` (an icon renders), and `starlight-auto-drafts` (a `draft: true` page hidden in build, shown in dev); record evidence/status
- [x] T021 [P] [US2] Verify `starlight-llms-txt` generates `llms.txt` on build; record evidence/status
- [x] T022 [US2] Verify `starlight-changelogs`: if `GH_API_TOKEN` is available, confirm `/changelog/` renders; otherwise record the local-dev token limitation in the matrix and confirm the build still succeeds without it
- [x] T023 [US2] For any plugin that cannot be made to work, remove it from `docs/astro.config.mjs` and `docs/package.json`, record the reason in the matrix, and re-run the build; otherwise confirm all rows are Working/Fixed

**Checkpoint**: Every plugin has evidence + a status; no blanks; removals (if any) carry a reason (SC-003).

---

## Phase 5: User Story 3 - Deferred manual verification from spec 002 is completed (Priority: P3)

**Goal**: The browser-dependent verification tasks deferred in spec 002 are executed and recorded.

**Independent Test**: Each deferred task is marked pass or has a recorded follow-up.

- [x] T024 [P] [US3] T029 — Verify mobile layout at viewport ≤ 768px in the browser: sidebar reachable via menu, no horizontal scrolling
- [x] T025 [P] [US3] T030 — Verify dark-mode auto-detection: with OS in dark mode, the site renders dark automatically
- [x] T026 [P] [US3] T043 — Verify `/tags/` index renders with tagged pages listed and links working
- [x] T027 [P] [US3] T045 — Verify `/changelog/` renders in both light and dark mode
- [x] T028 [P] [US3] T051 — Open the repo in VS Code: confirm the recommended-extensions prompt appears and `hideoo.starlight-links` provides link completion in a `.md` under `docs/src/content/docs/`
- [~] T029 [US3] T035 — Verify `.github/workflows/deploy-docs.yml` end-to-end (trigger via workflow*dispatch or push) and confirm GitHub Pages deployment succeeds; record follow-up if not runnable now — \_Outcome*: **follow-up** — workflow config verified and the API-generation step was reproduced locally, confirming `xmldocmd --clean` preserves the hand-written `api/index.md`; `GH_API_TOKEN` is passed to the build. Actual GitHub Pages deployment runs only on push to `main`/`workflow_dispatch`, so end-to-end confirmation is deferred to merge.
- [x] T030 [US3] Update the deferred task checkboxes in `specs/002-starlight-docs-migration/tasks.md` (T029, T030, T035, T043, T045, T051, T053, T055) to reflect outcomes, with follow-ups noted for any failures

**Checkpoint**: All deferred spec-002 verification tasks closed or assigned a follow-up (SC-004).

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Final consistency, formatting, and full validation pass.

- [x] T031 Run all scenarios in `specs/003-docs-site-fixes/quickstart.md` end-to-end (T053 from spec 002)
- [x] T032 Run `prettier --write "docs/**/*.{md,mdx,mjs,ts,yml}"` then `prettier --check` to confirm the formatting gate passes (FR-011)
- [x] T033 Final `cd docs && npm run build` — confirm zero errors and zero broken internal links (SC-005)
- [x] T034 Confirm `specs/003-docs-site-fixes/contracts/plugin-verification-matrix.md` is fully filled (no blank status) and `specs/003-docs-site-fixes/checklists/requirements.md` still passes

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies — start immediately.
- **Foundational (Phase 2)**: Depends on Setup — establishes the landing-page template; blocks US1.
- **US1 (Phase 3)**: Depends on Foundational. Delivers the MVP (navigable site).
- **US2 (Phase 4)**: Depends on US1 (needs landing pages to host activation content and a navigable
  site to exercise plugins).
- **US3 (Phase 5)**: Depends on US1 (navigable site); independent of US2 and can run in parallel with it.
- **Polish (Phase 6)**: Depends on all desired user stories.

### Within Each User Story

- US1: the three `index.md` pages (T005–T007) are parallel; verification (T008–T012) follows.
- US2: activation content (T013–T016) before live verification (T017–T023).
- US3: verification tasks are mostly parallel; the spec-002 checkbox update (T030) comes last.

### Parallel Opportunities

- T005, T006, T007 (three landing pages — different files) can run in parallel.
- T013, T014, T015 (activation content) and T017–T021 (plugin verifications) are parallelizable.
- T024–T028 (deferred checks) are parallelizable.
- US2 and US3 can proceed in parallel once US1 is complete.

---

## Parallel Example: User Story 1

```bash
# Create all three section landing pages together (different files):
Task: "Create docs/src/content/docs/reference/index.md"
Task: "Create docs/src/content/docs/architecture/index.md"
Task: "Create docs/src/content/docs/api/index.md"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Phase 1: Setup → 2. Phase 2: Foundational → 3. Phase 3: US1.
2. **STOP and VALIDATE**: all five topics resolve, build is link-clean. This alone fixes the reported
   404s and is shippable.

### Incremental Delivery

1. Setup + Foundational → ready.
2. US1 → navigable site (MVP) → verify in browser + build.
3. US2 → all plugins verified → matrix complete.
4. US3 → deferred spec-002 checks closed.
5. Polish → formatting + final build.

---

## Notes

- [P] = different files, no dependencies.
- No automated tests: verification is live (browser MCP) + `astro build` + link validator, per spec.
- Docs-only: no library/generator/runtime changes; no RS0017 or `.verified.cs` impact.
- Commit after each logical group; keep the site building green throughout.
