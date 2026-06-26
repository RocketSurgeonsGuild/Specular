# Implementation Plan: Indago VitePress Documentation Site

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Branch**: `feature/docs` | **Date**: 2026-06-25 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification from `specs/001-indago-static-assembly-scanner/spec.md`

**Scope**: This plan covers the VitePress documentation site (US3 / FR-010 / FR-011 / SC-003 / SC-006)
and the linting configuration for Vue, TypeScript, and Markdown used by the docs. The .NET
generator and runtime library (US1, US2) are separate implementation tracks.

## Summary

Author and configure the Indago public documentation site using VitePress 2.0 alpha (already
pinned in mise). The site covers a quickstart guide, conceptual architecture explanation, and
API reference for all public types. An oxlint setup covers `.vue` and `.ts/.mts` files in the
docs tree. A GitHub Actions workflow deploys the built site to GitHub Pages on every merge to
`main`. The lint toolchain integrates with the existing `hk` hook runner so the same
`hk fix` / `hk check` commands used in CI already cover the new file types.

## Technical Context

**Language/Version**: TypeScript 5.x (`.mts` VitePress config), Vue 3 (VitePress theme
components), Markdown (content pages)

**Primary Dependencies**:

- `vitepress@2.0.0-alpha.17` — already installed via mise
- `oxlint@^0.x` — Rust-based linter with built-in TypeScript + Vue 3 support; no plugin packages needed
- `prettier@3.8.4` — already installed; existing config covers XML/YAML/TOML/PS1

**Storage**: Static site; no database. Generated output to `docs/.vitepress/dist/`.

**Testing**: No dedicated test suite for docs content; lint + build (`vitepress build docs`)
serves as the correctness gate. Manual quickstart validation per `quickstart.md`.

**Target Platform**: GitHub Pages (static hosting); `base` path matches repo name
(`/Indago/` unless a custom domain is configured).

**Project Type**: Static documentation site + linting configuration

**Performance Goals**: VitePress build completes in under 60 s in CI. Live site deploys
within 10 minutes of merge (SC-006). Lighthouse performance score ≥ 90 on the home page.

**Constraints**: Must use mise for all tooling (bun 1.3.14 for package management in docs,
vitepress via mise). oxlint must integrate with `hk` hooks without requiring a separate
CI job. Docs content must render without errors in `vitepress dev` and `vitepress build`.

**Scale/Scope**: ~10–20 content pages for v1. Sidebar auto-generated from directory
structure where possible; manually curated where ordering matters.

## Constitution Check

_GATE: Must pass before Phase 0 research. Re-check after Phase 1 design._

| Principle                                   | Status  | Notes                                                                       |
| ------------------------------------------- | ------- | --------------------------------------------------------------------------- |
| I. AOT & Trim Safety                        | ✅ Pass | Docs site is static HTML; no .NET runtime involved                          |
| II. Test-First + Snapshots                  | ✅ Pass | Lint + `vitepress build` act as the gate; no snapshot tests needed for docs |
| III. Minimal & Stable Public API Surface    | ✅ Pass | Docs describe the API; they do not change it                                |
| IV. Code Quality & Strict Analysis          | ✅ Pass | oxlint + prettier cover Vue/TS; hk integration required                     |
| V. Documentation as First-Class Deliverable | ✅ Pass | This plan IS the documentation deliverable                                  |

**Post-design re-check**: No violations. No Complexity Tracking entries required.

## Project Structure

### Documentation (this feature)

```text
specs/001-indago-static-assembly-scanner/
├── plan.md              # This file
├── research.md          # Phase 0 output
├── data-model.md        # Phase 1 output (content & nav model)
├── quickstart.md        # Phase 1 output (validation guide)
├── contracts/           # Phase 1 output (nav/sidebar contract)
└── tasks.md             # Phase 2 output (/speckit-tasks command)
```

### Source Code (repository root)

```text
docs/
├── .vitepress/
│   ├── config.mts           # VitePress site config (title, nav, sidebar, base)
│   └── theme/
│       └── index.ts         # Theme entry (extends default; custom CSS vars if needed)
├── guide/
│   ├── index.md             # "What is Indago?" overview
│   ├── installation.md      # NuGet install + csproj setup
│   ├── quickstart.md        # First selector → AOT publish walkthrough
│   └── aot-publishing.md    # AOT/trim compatibility guide
├── reference/
│   ├── iindago-provider.md  # IIndagoProvider API reference
│   ├── type-filters.md      # Fluent selector API (AddClasses, AssignableTo, etc.)
│   ├── service-registration.md  # ServiceRegistrationAttribute + lifetimes
│   └── exclude-from-indago.md   # ExcludeFromIndagoAttribute
├── architecture/
│   ├── how-it-works.md      # Generator pipeline overview
│   └── cross-assembly-caching.md  # ctpjson + GeneratedHash model
├── index.md                 # Home page (hero + feature cards)
└── public/
    └── logo.svg             # Site logo (if available)

# Linting configuration (repository root)
oxlintrc.json                # oxlint config (Vue 3 + TS; MD excluded)
docs/tsconfig.json           # TypeScript config scoped to docs/

# GitHub Actions
.github/workflows/
└── deploy-docs.yml          # Build + deploy VitePress to GitHub Pages
```

**Structure Decision**: Single `docs/` tree with `guide/`, `reference/`, and `architecture/`
subdirectories matches VitePress conventions and keeps sidebar configuration straightforward.
oxlint config lives at repo root so it covers all TS/Vue files consistently.

## Current Docs State

The `docs/` directory exists with the default VitePress scaffold only:

```text
docs/
├── .vitepress/config.mts    # placeholder config (title set, nav/sidebar still example-only)
├── .vitepress/theme/index.ts
├── .vitepress/theme/style.css
├── index.md                 # hero page (content placeholder)
├── api-examples.md          # scaffold file — delete
└── markdown-examples.md     # scaffold file — delete
```

The scaffold files (`api-examples.md`, `markdown-examples.md`) and the placeholder nav/sidebar
must be replaced with the content structure in the data model before the site is usable.
VitePress itself is installed via mise (`"npm:vitepress" = "v2.0.0-alpha.17"`); no `npm install`
needed for vitepress. `oxlint` must be added to the **root `package.json`** devDependencies.

## hk Integration (PKL)

hk is configured in `.config/hk.pkl` using [Pkl](https://pkl-lang.org/). The existing config
has `linters` for prettier, dotnet-format, actionlint, etc. Two new steps are needed:

```pkl
// oxlint — lint and auto-fix .vue and .ts/.mts files.
// check mode: oxlint reports errors without modifying files (exit non-zero on violations).
// fix mode: oxlint --fix rewrites fixable issues in place before commit.
// Markdown files are excluded (not in scope for linting).
local oxlint: Step = new Step {
  glob = List("**/*.vue", "**/*.ts", "**/*.mts")
  exclude = new Listing<String> {
    "node_modules/**/*"
    "docs/.vitepress/dist/**/*"
    "docs/.vitepress/cache/**/*"
    "apm_modules/**/*"
    ".apm/**/*"
  }
  check = "oxlint --plugin vue --plugin typescript {{files}}"
  fix   = "oxlint --fix --plugin vue --plugin typescript {{files}}"
}
```

Add `["oxlint"] = oxlint` to the `linters` mapping so it runs under `pre-commit`, `fix`, and
`check` hooks automatically (they all reference `steps = linters`).

**Dependency note**: `oxlint` must be in the root `package.json` devDependencies so the
`oxlint` binary resolves locally via `node_modules/.bin`. The `npm install` step in mise's
`postinstall` hook installs it automatically after `mise install`.

## New mise Task

Add to `.config/mise.toml` `[tasks]`:

```toml
docs-build = { run = "vitepress build docs" }
```

This is required by the GitHub Pages deploy workflow (`mise run docs-build`).

## Phase Progress

| Phase   | Status  | Artifact(s)                                    |
| ------- | ------- | ---------------------------------------------- |
| Phase 0 | ✅ Done | `research.md`                                  |
| Phase 1 | ✅ Done | `data-model.md`, `quickstart.md`, `contracts/` |
| Phase 2 | ✅ Done | `tasks.md`                                     |
