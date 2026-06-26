# Data Model: Indago VitePress Documentation Site

**Date**: 2026-06-25

This document describes the content model, navigation structure, and configuration entities
for the VitePress documentation site.

---

## Site Configuration Entity

**File**: `docs/.vitepress/config.mts`

| Field         | Value                                                                   | Notes                                    |
| ------------- | ----------------------------------------------------------------------- | ---------------------------------------- |
| `title`       | `"Indago"`                                                              | Browser tab + OpenGraph title            |
| `description` | `"Compile-time assembly scanning for .NET — AOT safe, zero reflection"` | Meta description                         |
| `base`        | `"/Indago/"`                                                            | GitHub Pages project-site base path      |
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
| GitHub       | `https://github.com/RocketSurgeonsGuild/Indago` | External |
| NuGet        | `https://www.nuget.org/packages/Indago`         | External |

### Sidebar

#### `/guide/` section

| Order | Label           | File                      |
| ----- | --------------- | ------------------------- |
| 1     | What is Indago? | `guide/index.md`          |
| 2     | Installation    | `guide/installation.md`   |
| 3     | Quickstart      | `guide/quickstart.md`     |
| 4     | AOT Publishing  | `guide/aot-publishing.md` |

#### `/reference/` section

| Order | Label                          | File                                |
| ----- | ------------------------------ | ----------------------------------- |
| 1     | `IIndagoProvider`              | `reference/iindago-provider.md`     |
| 2     | Type Filters                   | `reference/type-filters.md`         |
| 3     | `ServiceRegistrationAttribute` | `reference/service-registration.md` |
| 4     | `ExcludeFromIndagoAttribute`   | `reference/exclude-from-indago.md`  |

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

**File**: `eslint.config.mjs` (repo root)

### Config blocks

| Block name | `files` pattern     | Parser                      | Key rules                                                       |
| ---------- | ------------------- | --------------------------- | --------------------------------------------------------------- |
| TypeScript | `**/*.{ts,mts,cts}` | `@typescript-eslint/parser` | `@typescript-eslint/recommended-type-checked`                   |
| Vue SFC    | `**/*.vue`          | `vue-eslint-parser`         | `plugin:vue/vue3-recommended`                                   |
| Markdown   | `**/*.md`           | (markdown plugin)           | `plugin:markdown/recommended` (lint fenced blocks)              |
| Ignores    | —                   | —                           | `node_modules`, `docs/.vitepress/dist`, `docs/.vitepress/cache` |

### TypeScript project config for ESLint type-aware rules

**File**: `docs/tsconfig.json`

Scoped to `docs/**` only. Required for `@typescript-eslint/recommended-type-checked`.

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

The existing `hk` configuration (`hk.pkl` or equivalent) gains two new hook entries:

| Hook         | Trigger                                      | Command                |
| ------------ | -------------------------------------------- | ---------------------- |
| ESLint fix   | pre-commit, staged `.vue/.ts/.mts/.md` files | `eslint --fix <files>` |
| ESLint check | CI check (`hk check`)                        | `eslint .`             |

No changes to the existing prettier hooks.
