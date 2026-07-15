# Data Model: Starlight Documentation Site

**Phase 1 output for plan.md**
**Date**: 2026-06-26
**Feature**: specs/002-starlight-docs-migration

This feature is documentation infrastructure, not a data-model-heavy feature. The "entities" here are content pages, site configuration, and plugin configuration shapes.

---

## Content Page (Frontmatter Schema)

Every Markdown/MDX file in `docs/src/content/docs/` uses Starlight frontmatter:

```yaml
---
title: "Page Title"                 # Required. Display title in sidebar + <title> tag
description: "Brief description"    # Optional. Used in <meta> and llms.txt
template: doc                       # Optional. Default is 'doc'. Home page uses 'splash'
sidebar:
  label: "Sidebar Label"            # Optional. Overrides title in sidebar
  order: 1                          # Optional. Controls sort order within group
  badge:                            # Optional (starlight-heading-badges)
    text: "New"
    variant: tip                    # tip | note | danger | caution | success | default
draft: true                         # Optional (starlight-auto-drafts). Excludes from prod build
tags:                               # Optional (starlight-tags). Array of string tags
  - "di"
  - "aot"
---
```

**Lifecycle states**: `draft: true` → excluded from production; omit `draft` or `draft: false` → published.

---

## Home Page (Splash Template)

`docs/src/content/docs/index.mdx` uses the Starlight splash template. Key differences from VitePress:

**VitePress (current)**:

```yaml
layout: home
hero:
    name: Specular
    text: Compile-time assembly scanning
    tagline: AOT safe. Zero reflection.
    actions:
        - theme: brand
          text: Get Started
          link: /guide/
features:
    - title: AOT Safe
      details: ...
```

**Starlight (target)**:

```yaml
template: splash
hero:
    title: Specular
    tagline: AOT safe. Zero reflection. Build-time performance.
    actions:
        - text: Get Started
          link: /guide/
          icon: right-arrow
          variant: primary
        - text: API Reference
          link: /api/
          icon: external
```

---

## Astro Configuration Shape (`astro.config.mjs`)

```js
// Logical shape — exact values filled during implementation
{
  site: "https://rocketSurgeonsGuild.github.io/Specular",
  base: "/Specular",                    // If deployed to GitHub Pages subpath
  integrations: [
    starlight({
      title: "Specular",
      description: "...",
      social: { github: "..." },
      logo: { ... },
      plugins: [
        starlightLinksValidator(),    // FR-006
        starlightGithubAlerts(),      // FR-007
        starlightAutoDrafts(),        // FR-008
        starlightSidebarTopics([...]),// FR-009
        starlightTags(),              // FR-010
        starlightChangelogs(),        // FR-011
        starlightDocSearch({ ... }),  // FR-012 (Typesense)
        starlightHeadingBadges(),     // FR-013
        starlightImageZoom(),         // FR-014
        starlightScrollToTop(),       // FR-015
        starlightPageActions([...]),  // FR-016
        starlightPluginIcons(),       // FR-017
        starlightLlmsTxt(),           // FR-018
      ],
      sidebar: [/* configured by starlight-sidebar-topics */],
    })
  ]
}
```

---

## Sidebar Topics Structure

Configured via `starlight-sidebar-topics` plugin. Maps content directories to named topics:

| Topic Label     | Directory / Pattern                                     | Order |
| --------------- | ------------------------------------------------------- | ----- |
| Getting Started | `guide/index`, `guide/installation`, `guide/quickstart` | 1     |
| Guides          | `guide/aot-publishing` + future guides                  | 2     |
| Reference       | `reference/*`                                           | 3     |
| Architecture    | `architecture/*`                                        | 4     |
| API Reference   | `api/*` (auto-generated)                                | 5     |
| Changelog       | `changelog/*`                                           | 6     |

---

## Typesense Index Schema

The Typesense collection created for search. `starlight-docsearch-typesense` manages index creation.

| Field            | Type     | Indexed | Purpose                              |
| ---------------- | -------- | ------- | ------------------------------------ |
| `id`             | string   | Yes     | Unique page identifier (URL path)    |
| `title`          | string   | Yes     | Page title (from frontmatter)        |
| `content`        | string   | Yes     | Full page text content               |
| `url`            | string   | No      | Absolute URL for search result links |
| `tags`           | string[] | Yes     | Content tags (from frontmatter)      |
| `hierarchy.lvl0` | string   | Yes     | Top-level section (topic group)      |
| `hierarchy.lvl1` | string   | Yes     | Page title                           |
| `hierarchy.lvl2` | string   | Yes     | H2 heading (section within page)     |
| `hierarchy.lvl3` | string   | Yes     | H3 heading                           |

---

## API Reference Page Structure (xmldocmd output)

Each public type produces one Markdown file. Representative structure:

```
docs/src/content/docs/api/
├── specular/                          # Namespace directory
│   ├── ispecular-provider.md          # ISpecularProvider interface
│   ├── specular-provider-service-collection-extensions.md
│   └── ...
└── specular.analyzers/                # Namespace directory (if exposed)
    └── ...
```

Each generated page has frontmatter:

```yaml
---
title: "ISpecularProvider"
sidebar:
  label: "ISpecularProvider"
---
```

And content sections:

- Type summary (from `<summary>` XML doc)
- Properties table
- Methods table with parameters + return types
- Inheritance / interface implementation
- Individual method detail sections

---

## URL Structure (Preserved from VitePress)

All existing public URLs are preserved. Starlight maps file paths to URLs automatically:

| File path                                                 | URL                                     |
| --------------------------------------------------------- | --------------------------------------- |
| `src/content/docs/index.mdx`                              | `/`                                     |
| `src/content/docs/guide/installation.md`                  | `/guide/installation/`                  |
| `src/content/docs/guide/quickstart.md`                    | `/guide/quickstart/`                    |
| `src/content/docs/guide/aot-publishing.md`                | `/guide/aot-publishing/`                |
| `src/content/docs/reference/ispecular-provider.md`          | `/reference/ispecular-provider/`          |
| `src/content/docs/reference/type-filters.md`              | `/reference/type-filters/`              |
| `src/content/docs/reference/service-registration.md`      | `/reference/service-registration/`      |
| `src/content/docs/reference/exclude-from-specular.md`       | `/reference/exclude-from-specular/`       |
| `src/content/docs/architecture/how-it-works.md`           | `/architecture/how-it-works/`           |
| `src/content/docs/architecture/cross-assembly-caching.md` | `/architecture/cross-assembly-caching/` |
| `src/content/docs/api/**`                                 | `/api/**` (auto-generated)              |
| `public/llms.txt`                                         | `/llms.txt`                             |

**Note**: Starlight appends trailing slashes by default. The deploy-docs.yml workflow must configure the base URL correctly for GitHub Pages.

---

## `.vscode/extensions.json` Schema

```json
{
    "recommendations": ["astro-build.astro-vscode", "hideoo.starlight-links"]
}
```

This prompts contributors to install the Astro VS Code extension (syntax highlighting, autocomplete for `.astro` files) and the `starlight-links` extension (link validation and autocomplete).
