# Feature Specification: Migrate Documentation Site to Starlight

**Feature Branch**: `002-starlight-docs-migration`

**Created**: 2026-06-26

**Status**: Draft

## Overview

Replace the current VitePress documentation site with an Astro Starlight-based site. The migration includes selecting and integrating beneficial Starlight plugins, and establishing an automated pipeline for generating API reference documentation directly from compiled .NET assemblies and their XML documentation files.

This feature directly fulfills Principle V of the Indago Constitution: _Documentation as a First-Class Deliverable_.

---

## Clarifications

### Session 2026-06-26

- Q: Which additional Starlight plugins should be integrated beyond the initial four? → A: starlight-links-validator, starlight-sidebar-topics, starlight-llms-txt, starlight-auto-drafts, starlight-github-alerts, starlight-changelogs, starlight-page-actions, starlight-docsearch-typesense, starlight-plugin-icons, starlight-tags (total of 14 plugins including the original four).
- Q: Should developer authoring tooling be included in scope? → A: Yes — the VS Code extension `starlight-links` (HiDeoo) for link autocomplete and validation should be wired in as part of the contributor developer experience.
- Q: Which `starlight-changelogs` provider should be used? → A: `github` provider — pulls release notes directly from GitHub Releases API (owner: `RocketSurgeonsGuild`, repo: `Indago`). No local `CHANGELOG.md` required. `GH_API_TOKEN` env var used optionally for rate-limit headroom in CI.
- Q: Should Typesense search be included in this implementation? → A: No — Typesense (FR-012, T021–T024) is deferred. `starlight-docsearch-typesense` removed from package.json, astro.config.mjs, and CI workflow. Default Starlight search remains active. May be re-added in a follow-on.
- Q: How should dotnet tools (e.g., xmldocmd) be installed? → A: Via mise configuration (`"dotnet:xmldocmd" = "2.9.0"` in `.config/mise.toml`), not via `dotnet tool install` or `.config/dotnet-tools.json`. Mise manages dotnet tools the same way it manages other toolchain versions.

---

## User Scenarios & Testing _(mandatory)_

### User Story 1 — Developer Finds API Reference for a Type (Priority: P1)

A .NET developer evaluating or using Indago needs to look up the API surface for a specific type, method, or attribute. They visit the documentation site and navigate to the API Reference section, where they see complete documentation generated from the library's XML docs, including summaries, parameters, return values, and remarks.

**Why this priority**: API reference is the most-visited section of any library documentation site. Without it, developers must read source code or decompile the DLL.

**Independent Test**: Can be fully tested by generating the API reference from the Indago DLLs and verifying that all public types appear with correct summaries, parameters, and return types in the rendered Starlight site.

**Acceptance Scenarios**:

1. **Given** a developer visits the documentation site, **When** they navigate to the API Reference section, **Then** they see all public types from `Indago` and `Indago.Analyzers` listed with their XML doc summaries.
2. **Given** a public method with `<param>` and `<returns>` XML doc comments, **When** its API page renders, **Then** parameter names, types, and descriptions are displayed correctly.
3. **Given** a type marked with `[Obsolete]` or `ExcludeFromIndagoAttribute`, **When** the API reference renders, **Then** it is either excluded or visually flagged as deprecated/excluded.

---

### User Story 2 — New Contributor Reads Guides and Understands the Project (Priority: P2)

A developer new to Indago reads the Getting Started guide, the architecture overview, and the selector expression documentation. The site is fast, searchable via Typesense, and works well on both desktop and mobile. Draft pages are never visible in the production site.

**Why this priority**: Onboarding quality directly affects adoption and contribution rate. Starlight's built-in features (dark mode, i18n-ready structure, navigation) combined with Typesense search serve this need.

**Independent Test**: Can be tested by navigating the site on desktop and mobile, using the search to find a concept ("selector", "IIndagoProvider"), and confirming pages render with correct navigation, syntax highlighting, and responsive layout.

**Acceptance Scenarios**:

1. **Given** a user on the docs home page, **When** they type a keyword into the search bar, **Then** relevant pages appear as results (default Starlight search; Typesense deferred).
2. **Given** a user on a mobile device (viewport ≤ 768px), **When** they open any documentation page, **Then** the sidebar is accessible via a menu and all content is readable without horizontal scrolling.
3. **Given** a user who prefers dark mode at the OS level, **When** they visit the site, **Then** the site renders in dark mode automatically.
4. **Given** a content page marked as draft, **When** the site is built for production, **Then** the draft page does not appear in navigation or search results.

---

### User Story 3 — Maintainer Updates Docs as Part of a Feature PR (Priority: P3)

A maintainer adds a new feature and needs to update the documentation as required by the project constitution. The docs build integrates into the existing `mise run docs` task, the CI pipeline, and the formatter pre-commit hook. Any broken internal links are caught at build time, not after deployment.

**Why this priority**: The build/CI integration is a constitutional requirement. No feature is complete until docs can be verified. The toolchain must support the existing workflow.

**Independent Test**: Can be tested by running `mise run docs` and confirming the dev server starts, the site builds without errors, link validation passes, and the deploy CI workflow succeeds.

**Acceptance Scenarios**:

1. **Given** a maintainer runs the docs build command, **When** the command completes, **Then** the site builds without errors and the dev server is accessible locally.
2. **Given** a PR that changes documented public API, **When** CI runs, **Then** the docs build step validates the site renders without errors (including re-generated API reference).
3. **Given** a maintainer adds a new `.md` page under the docs content directory, **When** they run the local dev server, **Then** the new page appears in the sidebar automatically without manual nav configuration.
4. **Given** a documentation page containing a broken internal link, **When** the docs build runs, **Then** the build fails with a clear error identifying the broken link and its source file.

---

### User Story 4 — Reader Navigates Complex Content with Enhanced UX (Priority: P4)

A developer reading documentation can zoom in on diagrams, jump back to the top of the page, see visual badges indicating API status, browse content by tag, view GitHub-style alert callouts, access quick page actions, and use icons within content.

**Why this priority**: These are quality-of-life improvements enabled by Starlight plugins. They are valuable but not blocking for the core migration.

**Independent Test**: Can be tested by verifying that diagrams support zoom-on-click, a scroll-to-top button appears on long pages, heading badges render, GitHub-style alerts display correctly, tags filter content, and page action buttons are accessible.

**Acceptance Scenarios**:

1. **Given** a documentation page containing an architecture diagram, **When** a reader clicks the image, **Then** a zoomed/lightbox view opens.
2. **Given** a long documentation page (requiring significant scrolling), **When** a reader scrolls down, **Then** a scroll-to-top button appears and clicking it returns them to the top of the page.
3. **Given** a heading annotated with a version or status marker, **When** the page renders, **Then** the heading displays a visible badge (e.g., "New in v2", "Experimental").
4. **Given** a Markdown blockquote using GitHub alert syntax (e.g., `> [!NOTE]`), **When** the page renders, **Then** it displays as a styled alert callout with the appropriate icon and color.
5. **Given** a page tagged with a content tag, **When** a reader browses the tag index, **Then** they see all pages sharing that tag.

---

### User Story 5 — LLM or AI Tool Discovers the Documentation (Priority: P5)

An AI assistant or LLM-powered developer tool queries the Indago documentation to answer questions about the library. The site exposes an `llms.txt` file at a well-known URL, enabling AI tools to efficiently locate and index the documentation content.

**Why this priority**: The LLM-friendly discovery capability is a differentiator for developer experience with modern AI tooling. It does not block the core migration but adds value for an OSS library targeting .NET developers who use AI assistants.

**Independent Test**: Can be tested by verifying an `llms.txt` (or equivalent) file is served at the expected URL, listing documentation pages in a format consumable by LLM context injection tools.

**Acceptance Scenarios**:

1. **Given** an AI tool requests the `llms.txt` endpoint, **When** the site is deployed, **Then** the file lists all public documentation pages with titles and URLs.
2. **Given** a new documentation page is added, **When** the site is rebuilt, **Then** the `llms.txt` file is regenerated automatically to include the new page.

---

### User Story 6 — Contributor Authors Content in VS Code (Priority: P5)

A contributor editing documentation files in VS Code gets autocomplete suggestions for internal Starlight links and sees validation errors for broken links before committing, without needing to run the full docs build.

**Why this priority**: The VS Code extension improves the authoring experience and catches errors earlier (shift-left quality). It is an optional developer experience enhancement.

**Independent Test**: Can be tested by opening a `.md` file in VS Code with the `starlight-links` extension active, typing a relative link, and confirming autocomplete suggestions appear for valid Starlight routes.

**Acceptance Scenarios**:

1. **Given** a contributor opens a Markdown file in VS Code with the starlight-links extension active, **When** they type a relative link path, **Then** VS Code suggests valid Starlight page routes.
2. **Given** a contributor has a broken link in a Markdown file, **When** VS Code analyzes the file, **Then** the broken link is highlighted as an error without requiring a build.

---

### Edge Cases

- What happens when an assembly has no XML documentation file? The API reference generation step should skip that assembly gracefully and log a warning rather than failing the build.
- What happens if a public type has no XML doc comments? The API reference page for that type should still render with the type name and signature, with a visual indicator that documentation is missing.
- What happens to existing URLs from the VitePress site? All current content paths should be preserved or redirect rules should be configured so that existing links remain valid.
- What happens when a plugin is incompatible with the current Starlight version? Plugins should be pinned to a compatible version and documented in the dependency file.
- ~~What happens if the Typesense search service is unavailable?~~ — **DEFERRED** with FR-012; default Starlight search has no external dependency.
- What happens when a draft page is linked from a non-draft page? The link validator should flag this as an error at build time.

---

## Requirements _(mandatory)_

### Functional Requirements

**Documentation Platform Migration**

- **FR-001**: The documentation site MUST be migrated from VitePress to Astro Starlight, preserving all existing content pages and their URL paths.
- **FR-002**: The `mise run docs` task MUST be updated to serve and build the Starlight site with the same interface (start dev server / build static output).
- **FR-003**: The CI deployment workflow MUST be updated to build and deploy the Starlight site.
- **FR-004**: All existing content (guides, reference, architecture pages) MUST be migrated and render without errors in Starlight.
- **FR-005**: The site MUST retain dark mode support, responsive mobile layout, and full-text search (provided by Typesense per FR-013).

**Starlight Plugin Integration — Content Quality & Validation**

- **FR-006**: The site MUST integrate **starlight-links-validator** to detect and fail the build on any broken internal links at build time.
- **FR-007**: The site MUST integrate **starlight-github-alerts** to render GitHub-style Markdown alert blockquotes (`> [!NOTE]`, `> [!WARNING]`, `> [!TIP]`, etc.) as styled callout components. This replaces the need for a separate VitePress-callout migration plugin.
- **FR-008**: The site MUST integrate **starlight-auto-drafts** so that pages marked as drafts are excluded from the production build's navigation, search index, and `llms.txt` output.

**Starlight Plugin Integration — Navigation & Structure**

- **FR-009**: The site MUST integrate **starlight-sidebar-topics** to organize the sidebar into distinct topic groups (e.g., Getting Started, Guides, Reference, API, Changelog).
- **FR-010**: The site MUST integrate **starlight-tags** to allow content pages to be tagged and browsable by tag from a tag index page.
- **FR-011**: The site MUST integrate **starlight-changelogs** using the **github** provider (owner: `RocketSurgeonsGuild`, repo: `Indago`) to render GitHub Release notes in a structured, navigable format within the docs site. No local `CHANGELOG.md` is maintained; release notes come directly from the GitHub Releases API.

**Starlight Plugin Integration — Search**

- ~~**FR-012**: The site MUST integrate **starlight-docsearch-typesense**~~ — **DEFERRED**: Typesense search removed from current scope; default Starlight search remains active. May be added in a future iteration when a Typesense Cloud account is provisioned.

**Starlight Plugin Integration — Content Enhancement**

- **FR-013**: The site MUST integrate **starlight-heading-badges** to allow headings to display version and status annotations (e.g., "New", "Experimental", "Deprecated").
- **FR-014**: The site MUST integrate **starlight-image-zoom** to enable click-to-zoom on embedded diagrams and images.
- **FR-015**: The site MUST integrate **starlight-scroll-to-top** for usability on long reference pages.
- **FR-016**: The site MUST integrate **starlight-page-actions** to display contextual action buttons on content pages (e.g., "Edit this page", "View source on GitHub").
- **FR-017**: The site MUST integrate **starlight-plugin-icons** to enable icon usage within MDX content pages.

**Starlight Plugin Integration — AI/LLM Discoverability**

- **FR-018**: The site MUST integrate **starlight-llms-txt** to auto-generate an `llms.txt` file at deployment that lists all non-draft documentation pages with their titles and URLs, enabling AI assistant tools to discover and index the docs.

**Developer Authoring Tooling**

- **FR-019**: The project MUST document and recommend the **starlight-links** VS Code extension (by HiDeoo) as part of the contributor development environment setup. Configuration or workspace settings for the extension SHOULD be included in the repository's `.vscode/extensions.json` or equivalent so contributors are prompted to install it automatically.

**API Reference Generation**

- **FR-020**: The documentation site MUST include an auto-generated API Reference section populated from the compiled Indago assemblies and their XML documentation files.
- **FR-021**: The API reference generator MUST read from DLL + XML doc file pairs (not source code) so it can run after a standard `dotnet build`.
- **FR-022**: The API reference generator MUST output standard Markdown files compatible with Starlight's content collection (no HTML-only output, no proprietary formats).
- **FR-023**: The API reference generation MUST be integrated into the docs build pipeline so that the reference is always in sync with the compiled output.
- **FR-024**: The API reference pages MUST include: type summaries, method/property signatures, parameter descriptions, return type descriptions, and inheritance/implementation information where available.
- **FR-025**: The API reference generation step MUST fail gracefully when an XML doc file is missing, logging a warning rather than a build error.

**Content Quality Gates**

- **FR-026**: The `prettier` formatter MUST continue to pass on all documentation source files after migration.
- **FR-027**: The site MUST build without errors in the CI environment before any feature PR can merge (same gate as the current VitePress requirement from the Constitution).

### Key Entities

- **Documentation Site**: The Starlight-based static site under `docs/`, containing all authored content and auto-generated API reference.
- **Content Page**: An individual Markdown or MDX file representing a guide, reference page, or concept explanation.
- **API Reference Page**: A Markdown file auto-generated from a .NET assembly + XML doc file, representing one type or namespace.
- **Starlight Plugin**: An npm package that extends Starlight's functionality, installed and configured in `astro.config.mjs`.
- **API Doc Generator**: A dotnet tool or CLI tool that reads compiled DLL + XML doc pairs and emits Markdown files.
- **Draft Page**: A content page marked as unpublished; excluded from production navigation, search, and `llms.txt` by `starlight-auto-drafts`.
- **Tag**: A metadata label applied to content pages; browsable via a generated tag index page (via `starlight-tags`).
- **llms.txt**: A machine-readable index file served at a well-known URL listing all public documentation pages for AI tool consumption.

---

## Success Criteria _(mandatory)_

### Measurable Outcomes

- **SC-001**: All existing documentation pages are accessible at the same URL paths after migration, with zero 404 errors for any URL that existed in the VitePress site.
- **SC-002**: The docs build completes in the same time or faster than the current VitePress build (measured in the CI pipeline).
- ~~**SC-003**: The full-text search (Typesense) returns results within 1 second~~ — **DEFERRED**: Typesense removed from scope; default Starlight search remains.
- **SC-004**: The API Reference section covers 100% of public types in `src/Indago` and `src/Indago.Analyzers`, each with at least a name and signature.
- **SC-005**: The `mise run docs` command starts the dev server successfully on a clean checkout within 2 minutes (including dependency install).
- **SC-006**: Zero manual steps are required to regenerate API reference after a `dotnet build` — the API docs update automatically as part of the docs build pipeline.
- **SC-007**: All 13 active Starlight plugins (FR-006 through FR-018 excluding the deferred FR-012) render correctly in both light and dark mode with no console errors.
- **SC-008**: The `llms.txt` file is present and lists all non-draft documentation pages within 5 seconds of the production site loading.
- **SC-009**: Zero broken internal links exist on the deployed site, verified by `starlight-links-validator` passing in CI.
- **SC-010**: The `starlight-links` VS Code extension is listed in `.vscode/extensions.json`, prompting new contributors to install it automatically.

---

## Assumptions

- The existing VitePress content under `docs/` is authored in standard Markdown/MDX and can be migrated with minimal or no syntax changes (no heavy use of Vue-specific VitePress components).
- VitePress-style custom callout syntax (if any) will be converted to GitHub alert syntax (`> [!NOTE]` etc.) as part of the migration, handled by `starlight-github-alerts`.
- The primary API doc generation tool will be **xmldocmd** (XmlDocMarkdown) based on its direct Markdown output and Starlight compatibility; if evaluation during planning reveals a better fit, **xmldoc2md** is the secondary candidate.
- Dotnet tools (including `xmldocmd`) are installed and versioned via mise (e.g., `"dotnet:xmldocmd" = "2.9.0"` in `.config/mise.toml`). No `.config/dotnet-tools.json` local tool manifest is used; `dotnet tool restore` is not part of the build pipeline.
- The documentation site is deployed as a static site (GitHub Pages or similar); no server-side rendering is required.
- The `docs/` directory is the sole location for documentation source; no documentation lives outside this directory.
- Prettier is already configured to format Markdown files; the Starlight migration does not require changes to the Prettier configuration.
- The `.github/workflows/deploy-docs.yml` CI workflow will need to be updated as part of this migration.
- The Indago Constitution's references to "VitePress docs site under `docs/`" and "`mise run docs`" will be updated (PATCH amendment) as a follow-on to this feature once the migration is confirmed working.
- **Typesense search** (FR-012) is **deferred from this implementation**. The `starlight-docsearch-typesense` package has been removed; default Starlight search is active. Typesense may be re-added in a follow-on when a Cloud account is provisioned.
- The `starlight-links` VS Code extension requires the Starlight project to be open as a workspace root in VS Code; contributors using other editors will rely on the `starlight-links-validator` build-time check instead.

---

## API Documentation Tool Evaluation

The following four tools were evaluated for generating API reference from .NET DLLs + XML docs. This section informs the choice made in FR-021 and FR-022.

| Tool                          | Output        | Starlight Compatible | Maintenance                 |
| ----------------------------- | ------------- | -------------------- | --------------------------- |
| **xmldocmd** (XmlDocMarkdown) | Pure Markdown | Yes — direct         | Active (v2.9.0, 2024)       |
| **xmldoc2md**                 | Markdown      | Yes — plain MD       | Active (dotnet global tool) |
| **DocFX**                     | HTML website  | No — HTML only       | Active (community, v2.78)   |
| **Sandcastle / SHFB**         | HTML / CHM    | No — proprietary     | Active (legacy format)      |

**Recommendation**: xmldocmd or xmldoc2md for Starlight integration; DocFX and Sandcastle/SHFB output HTML and are not suitable as Markdown sources for Starlight.

---

## Plugin Inventory

| Plugin                            | Purpose                                  | Category     |
| --------------------------------- | ---------------------------------------- | ------------ |
| starlight-links-validator         | Fails build on broken internal links     | Quality      |
| starlight-github-alerts           | GitHub-style alert callouts              | Content      |
| starlight-auto-drafts             | Excludes draft pages from production     | Quality      |
| starlight-sidebar-topics          | Topic-grouped sidebar navigation         | Navigation   |
| starlight-tags                    | Content tagging + tag index page         | Navigation   |
| starlight-changelogs              | Structured changelog rendering           | Content      |
| ~~starlight-docsearch-typesense~~ | ~~Typesense-powered full-text search~~   | **DEFERRED** |
| starlight-heading-badges          | Version/status badges on headings        | Content      |
| starlight-image-zoom              | Click-to-zoom on images/diagrams         | UX           |
| starlight-scroll-to-top           | Scroll-to-top button on long pages       | UX           |
| starlight-page-actions            | Contextual action buttons per page       | UX           |
| starlight-plugin-icons            | Icon support in MDX content              | Content      |
| starlight-llms-txt                | Auto-generate llms.txt for AI tools      | AI/LLM       |
| **starlight-links** (VS Code ext) | Link autocomplete + validation in editor | Dev Tooling  |
