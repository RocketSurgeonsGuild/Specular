# Feature Specification: Docs Site Fixes & Plugin Verification

**Feature Branch**: `feature/docs`

**Created**: 2026-06-27

**Status**: Implementing

**Input**: User description: "I want to fix some issues with the doc site. Some links 404 (architecture, api and reference). I'm not sure all the extensions are setup and working. I have enabled a browser mcp in vscode, that uses the integrated browser. The docs task is currently running so any changes should be reflected right away. There are also some remaining manual tasks that can be brought in here and worked on as well now that there is a browser you can use."

## Overview

The Starlight documentation site (delivered in spec 002) ships with broken top-level navigation
and a set of installed presentation plugins whose runtime behavior was never verified end-to-end.
This feature closes those gaps: every primary navigation entry must resolve to a real page, every
installed plugin must be confirmed working (or removed/fixed), and the manual verification tasks
deferred from spec 002 must be completed now that a live browser is available against the running
dev server.

This is a documentation-quality and correctness feature. It does not change the library's public
API, the source generator, or any runtime code. Per the project constitution, documentation must
render without errors before it is considered complete; this feature brings the existing docs site
up to that bar.

## User Scenarios & Testing _(mandatory)_

### User Story 1 - Every top-level navigation link resolves (Priority: P1)

A reader opens the documentation site and clicks each primary navigation topic — Getting Started,
Reference, Architecture, API Reference, and Changelog. Today, Reference, Architecture, and API
Reference return a 404 because the sidebar topic links point at section roots (`/reference/`,
`/architecture/`, `/api/`) that have no landing page. The reader expects each topic to open a real
page that orients them and links into the section.

**Why this priority**: A 404 on a primary navigation entry is the most visible, credibility-damaging
defect a docs site can have. It blocks the reader's first interaction with three of five sections.
This is the MVP — fixing it alone delivers a navigable site. The fix is to add a section landing
(index) page at each section root, mirroring how `/guide/` already works.

**Independent Test**: With the dev server running, click (or navigate to) each of the five sidebar
topic links via the integrated browser and confirm each returns HTTP 200 and renders a meaningful
landing page rather than the Starlight 404 page.

**Acceptance Scenarios**:

1. **Given** the docs site is running, **When** the reader navigates to `/reference/`, **Then** a
   section landing page renders (HTTP 200) with links to the reference pages.
2. **Given** the docs site is running, **When** the reader navigates to `/architecture/`, **Then** a
   section landing page renders (HTTP 200) with links to the architecture pages.
3. **Given** the docs site is running, **When** the reader navigates to `/api/`, **Then** a section
   landing page renders (HTTP 200) that introduces the auto-generated API reference and links into it.
4. **Given** the docs site is running, **When** the reader navigates to `/guide/` and `/changelog/`,
   **Then** both continue to render correctly (no regression).
5. **Given** the site builds for production, **When** the link validator runs, **Then** it reports
   zero broken internal links.

---

### User Story 2 - All installed plugins are confirmed working or removed (Priority: P2)

A maintainer wants confidence that every Starlight plugin listed in the site configuration actually
functions on the live site — not merely that it is installed. Each plugin must either be observably
working (its feature visible/functional in the browser) or, if it cannot be made to work or adds no
value, be removed so the configuration reflects reality.

**Why this priority**: Installed-but-inert plugins create false confidence and maintenance drag.
This depends on US1 (a navigable site) being in place to exercise the plugins across pages, but it is
independently valuable: a verified plugin set is a durable quality outcome. The goal is for all
installed plugins to be demonstrably working; removal is a last resort only when a plugin genuinely
cannot be made to work or adds no value.

**Independent Test**: For each configured plugin, exercise its feature against the running site in the
browser and record a pass/fail with the specific evidence observed (the rendered control, generated
output, or behavior). Any plugin not yet demonstrable is made to work by adding the required
config/content/frontmatter; a plugin is removed only after a working setup proves infeasible.

**Acceptance Scenarios**:

1. **Given** the configured plugin set, **When** the maintainer audits each plugin on the live site,
   **Then** every plugin has a recorded status of Working, Fixed, or Removed — none left Unknown, and
   Removed is used only where a working setup proved infeasible.
2. **Given** a plugin that requires content or frontmatter to activate (e.g. tags, heading badges,
   changelog), **When** the required content is absent, **Then** representative content is added so the
   plugin's behavior is demonstrable.
3. **Given** a plugin that depends on an external token or service (e.g. changelog via GitHub),
   **When** that dependency is unavailable in local dev, **Then** the expected behavior (and its
   dependency) is documented and the build still succeeds without it.
4. **Given** any plugin is removed, **When** the change is made, **Then** it is removed from both the
   site configuration and the package dependencies, and the site still builds.

---

### User Story 3 - Deferred manual verification from spec 002 is completed (Priority: P3)

The migration spec (002) left several verification tasks marked manual/incomplete because no browser
was available at the time. Now that the integrated browser can drive the running dev server, these
checks should be executed and their outcomes recorded so spec 002 can be considered fully closed.

**Why this priority**: These are confirmation tasks, not new functionality. They are valuable for
closing out the migration but are lower urgency than fixing live 404s and inert plugins.

**Independent Test**: Walk each deferred verification scenario in the browser against the running
site and record pass/fail with evidence; file follow-up items for any failures discovered.

**Acceptance Scenarios**:

1. **Given** the running site, **When** the responsive layout is checked at a viewport ≤ 768px,
   **Then** the sidebar is reachable via the menu and content reads without horizontal scrolling.
2. **Given** the running site, **When** the OS is set to dark mode, **Then** the site renders in dark
   mode automatically and all content is legible.
3. **Given** the running site, **When** the `/tags/` and `/changelog/` pages are opened, **Then** each
   renders correctly in both light and dark mode with working links.
4. **Given** the deferred task list from spec 002, **When** each item is exercised, **Then** every
   item is marked passed or has a recorded follow-up.

---

### Edge Cases

- **Base path differences**: Locally the site is served at the root; in GitHub Actions it is served
  under `/Specular`. Section landing links and fixes MUST work under both base paths (no hard-coded
  absolute paths that break when a base prefix is applied).
- **Auto-generated API pages**: The API section is generated from compiled DLLs. A landing page or
  fix MUST NOT be clobbered by the generation step, and MUST coexist with the generated tree.
- **Plugin with no demonstrable surface locally**: If a plugin's behavior only manifests in a
  production build or requires an external token, its verification approach and any limitation MUST be
  recorded rather than silently passed.
- **Link validator vs. relative links**: The validator currently tolerates relative links
  (`errorOnRelativeLinks: false`); fixes MUST still pass the validator as configured and MUST NOT
  introduce new broken absolute links.

## Requirements _(mandatory)_

### Functional Requirements

- **FR-001**: Every primary sidebar topic (Getting Started, Reference, Architecture, API Reference,
  Changelog) MUST resolve to a rendering page (HTTP 200) on the running site.
- **FR-002**: The Reference, Architecture, and API Reference sections MUST each have an index landing
  page at their section root that orients the reader and links into the section's content (mirroring
  the existing `/guide/` landing page).
- **FR-003**: Section landing pages MUST function correctly under both the local (root) and CI
  (`/Specular`) base paths.
- **FR-004**: The production build's link validator MUST report zero broken internal links.
- **FR-005**: Each installed Starlight plugin MUST have a verified status of Working, Fixed, or
  Removed, with recorded evidence of the determination. The intended outcome is that all installed
  plugins are Working or Fixed.
- **FR-006**: A plugin MUST be removed (from both the site configuration and the package dependencies)
  only as a last resort, when a working setup is proven infeasible or the plugin adds no value; any
  removal MUST record the reason.
- **FR-007**: Plugins that require activating content or frontmatter MUST have representative content
  added so their behavior is demonstrable.
- **FR-008**: The deferred manual verification tasks carried over from spec 002 MUST be executed
  against the running site and each marked passed or assigned a recorded follow-up.
- **FR-009**: Changes MUST be limited to documentation content, docs site configuration, and docs
  dependencies; no changes to library source, the generator, or runtime code.
- **FR-010**: The docs site MUST continue to build successfully (`astro build`) after all changes.
- **FR-011**: Documentation source and configuration changes MUST pass the project's formatting gate
  (prettier).

### Key Entities _(include if feature involves data)_

- **Navigation Topic**: A primary sidebar entry with a label, icon, target link, and an associated
  content directory. Each topic's target link must resolve to a real page.
- **Section Landing Page**: The index page at a section root that introduces the section and links to
  its child pages.
- **Plugin Verification Record**: For each configured plugin — its name, the feature it provides, the
  evidence observed on the live site, and a status (Working / Fixed / Removed).
- **Deferred Verification Task**: A manual check carried over from spec 002, with a scenario, an
  outcome (pass/fail), and any follow-up.

## Success Criteria _(mandatory)_

### Measurable Outcomes

- **SC-001**: 100% of primary navigation topics (5 of 5) resolve to a rendering page; zero return a 404.
- **SC-002**: The link validator reports zero broken internal links on a production build.
- **SC-003**: 100% of installed plugins have a recorded status of Working, Fixed, or Removed; zero
  remain Unknown, and every Removed status carries a recorded reason.
- **SC-004**: 100% of the deferred spec-002 verification tasks are marked passed or have a recorded
  follow-up.
- **SC-005**: The docs site builds successfully with zero errors after all changes.
- **SC-006**: A first-time reader can reach every documentation section from the home page without
  encountering a 404.

## Assumptions

- The Astro/Starlight dev server is running locally (confirmed on port 4321) and reflects content
  changes on save; verification will be performed against it via the integrated browser MCP.
- The fix for the section 404s is the absence of section landing/index pages; adding an index page at
  each section root (the chosen approach) resolves the reported issue.
- "Extensions" in the user's request refers to the installed Starlight plugins in the site
  configuration (and, secondarily, the recommended VS Code editor extensions referenced in spec 002).
- The API reference tree is auto-generated; landing-page work must coexist with that generation and
  not require regenerating the API content.
- No changes to the library's public API or generator are in scope; this is a docs-only feature and
  therefore does not require RS0017 public-API tracking updates or snapshot test changes.
- The GitHub-token-dependent changelog plugin may not be fully exercisable in local dev; its expected
  production behavior will be documented rather than blocking this feature.
