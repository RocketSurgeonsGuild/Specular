# Implementation Plan: Docs Site Fixes & Plugin Verification

**Branch**: `feature/docs` | **Date**: 2026-06-27 | **Spec**: [spec.md](spec.md)

**Input**: Feature specification from `/specs/003-docs-site-fixes/spec.md`

## Summary

The Starlight (Astro) docs site ships with three top-level sections (Reference, Architecture, API
Reference) that 404 because their sidebar topic links target section roots that have no landing page,
and with 14 Starlight plugins whose runtime behavior was never verified end-to-end. This plan adds an
index landing page to each broken section (mirroring the existing `/guide/` page), audits and
activates every installed plugin until each is demonstrably Working or Fixed (removal only as a
last resort), and completes the manual verification tasks deferred from spec 002 — all verified live
in the integrated browser MCP against the already-running dev server (port 4321) and confirmed by a
clean `astro build` with zero broken internal links.

This is a documentation-only feature: no changes to library source, the source generator, or runtime
code.

## Technical Context

**Language/Version**: Markdown / MDX content; JavaScript ESM site config (`astro.config.mjs`); Node ≥ 20

**Primary Dependencies**: Astro 7, Starlight 0.41, and the 14 installed `starlight-*` plugins
(auto-drafts, github-alerts, sidebar-topics, links-validator, heading-badges, image-zoom,
scroll-to-top, page-actions, icons, tags, changelogs, llms-txt)

**Storage**: N/A — static content files under `docs/src/content/docs/`

**Testing**: `astro build` (build-time correctness) + `starlight-links-validator` (zero broken
internal links) + live browser verification via the integrated browser MCP against `astro dev`

**Target Platform**: Static site → GitHub Pages (served at root locally, under `/Specular` in CI)

**Project Type**: Documentation site (single Astro project under `docs/`)

**Performance Goals**: N/A for this feature (no perf regression; build remains green). Existing site
performance criteria from spec 002 are unchanged.

**Constraints**: All fixes MUST work under both base paths (root + `/Specular`); MUST NOT hard-code
absolute paths that break under a base prefix; API section work MUST coexist with the auto-generated
API tree; the GitHub-token-dependent changelog plugin may not be fully exercisable in local dev and
its production behavior is documented rather than blocking.

**Scale/Scope**: 3 new section landing pages, 14 plugins to verify/activate, 8 deferred verification
tasks (T029, T030, T035, T043, T045, T051, T053, T055 from spec 002).

## Constitution Check

_GATE: Must pass before Phase 0 research. Re-check after Phase 1 design._

| Principle                                         | Applies?          | Status                                                                                                                                                                                                         |
| ------------------------------------------------- | ----------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **I. AOT & Trim Safety**                          | No                | No generator/runtime/public-API code changes. Docs-only. ✅ N/A                                                                                                                                                |
| **II. Test-First with Snapshot Verification**     | No                | No generator behavior change; no `.verified.cs` snapshots affected. Verification here is browser + `astro build` + link validator. ✅ N/A                                                                      |
| **III. Minimal & Stable Public API Surface**      | No                | No `src/Specular` API change; no RS0017 tracking update required. ✅ N/A                                                                                                                                         |
| **IV. Code Quality & Strict Analysis**            | Partial           | Docs config/content MUST pass prettier. No .NET analysis impact. ✅ Met by prettier gate                                                                                                                       |
| **V. Documentation as a First-Class Deliverable** | **Yes (primary)** | This feature directly serves Principle V: the docs site MUST render without errors before merge. Adding landing pages, verifying plugins, and a clean build is exactly the bar this principle sets. ✅ Aligned |

**Gate result**: PASS. No violations; Complexity Tracking not required.

## Project Structure

### Documentation (this feature)

```text
specs/003-docs-site-fixes/
├── plan.md              # This file (/speckit-plan command output)
├── research.md          # Phase 0 output — plugin activation + base-path + landing-page decisions
├── data-model.md        # Phase 1 output — Navigation Topic, Landing Page, Plugin Record, Deferred Task
├── quickstart.md        # Phase 1 output — live browser + build verification scenarios
├── contracts/           # Phase 1 output
│   ├── navigation-contract.md       # Every topic resolves (HTTP 200) + link-validator clean
│   └── plugin-verification-matrix.md # Per-plugin evidence + status (Working/Fixed/Removed)
└── tasks.md             # Phase 2 output (/speckit-tasks — NOT created here)
```

### Source Code (repository root)

```text
docs/
├── astro.config.mjs                 # Sidebar topics, plugin list, base path (edit: plugin activation)
├── tags.yml                         # Tag definitions (wire into starlight-tags if needed)
├── content.config.ts                # Collections incl. changelogs loader
├── scripts/
│   └── add-api-frontmatter.mjs      # API generation step (must coexist with new api landing page)
└── src/content/docs/
    ├── index.mdx                    # Home (splash) — unchanged
    ├── guide/
    │   └── index.md                 # EXISTING landing page (template/reference)
    ├── reference/
    │   ├── index.md                 # NEW — section landing page
    │   ├── exclude-from-specular.md
    │   ├── ispecular-provider.md
    │   ├── service-registration.md
    │   └── type-filters.md
    ├── architecture/
    │   ├── index.md                 # NEW — section landing page
    │   ├── how-it-works.md
    │   └── cross-assembly-caching.md
    └── api/
        ├── index.md                 # NEW — section landing page (coexists with generated tree)
        └── Specular*/...              # Auto-generated API reference (untouched by hand)

.github/workflows/deploy-docs.yml    # Deployment (verify end-to-end — T035)
.vscode/extensions.json              # Recommended editor extensions (verify — T051)
```

**Structure Decision**: Single Astro documentation project under `docs/`. All content changes land in
`docs/src/content/docs/`; configuration changes land in `docs/astro.config.mjs` (and `tags.yml` /
`content.config.ts` only if a plugin requires wiring). The auto-generated API tree under `api/` is
not hand-edited except for the new `api/index.md` landing page, which the generation step must leave
intact.

## Complexity Tracking

> No Constitution Check violations. This section is intentionally empty.
