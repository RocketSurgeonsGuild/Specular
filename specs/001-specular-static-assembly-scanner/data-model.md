# Data Model: Specular VitePress Documentation Site

**Date**: 2026-06-25

This document describes the content model, navigation structure, and configuration entities
for the VitePress documentation site.

---

## Site Configuration Entity

**File**: `docs/.vitepress/config.mts`

| Field         | Value                                                                   | Notes                                    |
| ------------- | ----------------------------------------------------------------------- | ---------------------------------------- |
| `title`       | `"Specular"`                                                              | Browser tab + OpenGraph title            |
| `description` | `"Compile-time assembly scanning for .NET — AOT safe, zero reflection"` | Meta description                         |
| `base`        | `"/Specular/"`                                                            | GitHub Pages project-site base path      |
| `cleanUrls`   | `true`                                                                  | Drop `.html` from URLs                   |
| `lastUpdated` | `true`                                                                  | Show git last-modified date on each page |
| `lang`        | `"en-US"`                                                               | Primary language                         |

---

## Navigation Model

### Top-Level Nav

| Label        | Link                                            | Type     |
| ------------ | ----------------------------------------------- | -------- |
| Guide        | `/guide/`                                       | Internal |
| Reference    | `/reference/`                                   | Internal |
| Architecture | `/architecture/`                                | Internal |
| GitHub       | `https://github.com/RocketSurgeonsGuild/Specular` | External |
| NuGet        | `https://www.nuget.org/packages/Specular`         | External |

### Sidebar

#### `/guide/` section

| Order | Label           | File                      |
| ----- | --------------- | ------------------------- |
| 1     | What is Specular? | `guide/index.md`          |
| 2     | Installation    | `guide/installation.md`   |
| 3     | Quickstart      | `guide/quickstart.md`     |
| 4     | AOT Publishing  | `guide/aot-publishing.md` |

#### `/reference/` section

| Order | Label                          | File                                |
| ----- | ------------------------------ | ----------------------------------- |
| 1     | `ISpecularProvider`              | `reference/ispecular-provider.md`     |
| 2     | Type Filters                   | `reference/type-filters.md`         |
| 3     | `ServiceRegistrationAttribute` | `reference/service-registration.md` |
| 4     | `ExcludeFromSpecularAttribute`   | `reference/exclude-from-specular.md`  |

#### `/architecture/` section

| Order | Label                  | File                                     |
| ----- | ---------------------- | ---------------------------------------- |
| 1     | How It Works           | `architecture/how-it-works.md`           |
| 2     | Cross-Assembly Caching | `architecture/cross-assembly-caching.md` |

---

## Content Page Model

Each content page has:

| Field                       | Description                                                 | Required         |
| --------------------------- | ----------------------------------------------------------- | ---------------- |
| `title` (frontmatter)       | Page title used in `<title>` and sidebar                    | Yes              |
| `description` (frontmatter) | Meta description for SEO                                    | Recommended      |
| H1 heading                  | Matches frontmatter title                                   | Yes              |
| Code examples               | Fenced blocks with language tags (`csharp`, `bash`, `json`) | Where applicable |
| "Next steps" footer links   | Inline markdown links to the next logical page              | Recommended      |

---

## Linting Configuration Entity

**File**: `oxlintrc.json` (repo root)

### Config shape

| Field            | Value                                                           | Notes                                     |
| ---------------- | --------------------------------------------------------------- | ----------------------------------------- |
| `$schema`        | oxc configuration schema URL                                    | Enables IDE autocomplete                  |
| `plugins`        | `["vue", "typescript"]`                                         | Built-in plugins; no separate npm install |
| `rules`          | `{}`                                                            | Start with recommended defaults           |
| `ignorePatterns` | `node_modules`, `docs/.vitepress/dist`, `docs/.vitepress/cache` | Exclude generated output                  |

### Covered file types

| Files           | Plugin       | Notes                                          |
| --------------- | ------------ | ---------------------------------------------- |
| `**/*.{ts,mts}` | `typescript` | Built-in TS rules; no separate parser required |
| `**/*.vue`      | `vue`        | Vue 3 SFC rules                                |

Markdown fenced-block linting is **not** in scope (out of scope per clarification 2026-06-25).

### TypeScript config for VitePress type-checking

**File**: `docs/tsconfig.json`

Scoped to `docs/**` only. Used by the VitePress build and IDE; not required by oxlint
(oxlint resolves types independently).

---

## GitHub Pages Deployment Entity

**File**: `.github/workflows/deploy-docs.yml`

| Field                | Value                                                                                 |
| -------------------- | ------------------------------------------------------------------------------------- |
| Trigger              | `push` to `main`; `workflow_dispatch`                                                 |
| Build command        | `mise run docs-build`                                                                 |
| Artifact source      | `docs/.vitepress/dist`                                                                |
| Environment          | `github-pages`                                                                        |
| Required permissions | `pages: write`, `id-token: write`, `contents: read`                                   |
| Concurrency          | One deploy at a time (`cancel-in-progress: false` — never cancel a deploy mid-flight) |

### New mise task

| Task         | Command                | Purpose                                  |
| ------------ | ---------------------- | ---------------------------------------- |
| `docs-build` | `vitepress build docs` | Reproducible docs build for CI and local |

---

## hk Hook Integration

The existing `hk` configuration (`.config/hk.pkl`) gains one new hook entry:

| Hook         | Trigger                                  | Command                                                 |
| ------------ | ---------------------------------------- | ------------------------------------------------------- |
| oxlint fix   | pre-commit, staged `.vue/.ts/.mts` files | `oxlint --fix --plugin vue --plugin typescript <files>` |
| oxlint check | CI check (`hk check`)                    | `oxlint --plugin vue --plugin typescript <files>`       |

No changes to the existing prettier hooks. Markdown files are not linted by oxlint.
