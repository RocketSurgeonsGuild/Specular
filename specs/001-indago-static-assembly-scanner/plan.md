# Implementation Plan: Indago VitePress Documentation Site

**Branch**: `feature/docs` | **Date**: 2026-06-25 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification from `specs/001-indago-static-assembly-scanner/spec.md`

**Scope**: This plan covers the VitePress documentation site (US3 / FR-010 / FR-011 / SC-003 / SC-006)
and the linting configuration for Vue, TypeScript, and Markdown used by the docs. The .NET
generator and runtime library (US1, US2) are separate implementation tracks.

## Summary

Author and configure the Indago public documentation site using VitePress 2.0 alpha (already
pinned in mise). The site covers a quickstart guide, conceptual architecture explanation, and
API reference for all public types. An ESLint 9 flat-config setup covers `.vue`, `.ts/.mts`,
and `.md` files in the docs tree. A GitHub Actions workflow deploys the built site to GitHub
Pages on every merge to `main`. The lint toolchain integrates with the existing `hk` hook runner
so the same `hk fix` / `hk check` commands used in CI already cover the new file types.

## Technical Context

**Language/Version**: TypeScript 5.x (`.mts` VitePress config), Vue 3 (VitePress theme
components), Markdown (content pages)

**Primary Dependencies**:

- `vitepress@2.0.0-alpha.17` — already installed via mise
- `eslint@9.x` — flat config (`eslint.config.mjs`), modern ESLint
- `eslint-plugin-vue@9.x` — Vue 3 SFC linting
- `@typescript-eslint/eslint-plugin` + `@typescript-eslint/parser` — TypeScript linting
- `eslint-plugin-markdown@3.x` — lint fenced code blocks in `.md` files
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
vitepress via mise). ESLint must integrate with `hk` hooks without requiring a separate
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
| IV. Code Quality & Strict Analysis          | ✅ Pass | ESLint flat config + prettier cover Vue/TS/MD; hk integration required      |
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
eslint.config.mjs            # ESLint 9 flat config (Vue 3 + TS + MD)
docs/tsconfig.json           # TypeScript config scoped to docs/

# GitHub Actions
.github/workflows/
└── deploy-docs.yml          # Build + deploy VitePress to GitHub Pages
```

**Structure Decision**: Single `docs/` tree with `guide/`, `reference/`, and `architecture/`
subdirectories matches VitePress conventions and keeps sidebar configuration straightforward.
ESLint flat config lives at repo root so it covers all JS/TS/Vue/MD files consistently.
