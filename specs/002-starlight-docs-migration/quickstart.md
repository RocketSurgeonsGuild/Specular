# Quickstart Validation Guide: Starlight Documentation Site

**Phase 1 output for plan.md**
**Date**: 2026-06-26
**Feature**: specs/002-starlight-docs-migration

This guide documents the runnable validation scenarios that prove the feature works end-to-end. Run these after implementation to validate each success criterion from the spec.

---

## Prerequisites

- Node.js 20+ installed (via mise: `mise install node`)
- .NET 8+ SDK installed
- `dotnet tool restore` completed (installs xmldocmd)
- Typesense API credentials available (for search validation; skip for local-only checks)

---

## Validation 1: Dev Server Starts (SC-005)

**What this validates**: The `mise run docs` task starts the Astro dev server successfully on a clean checkout within 2 minutes.

```bash
# From repo root
mise run docs
```

**Expected outcome**:

- Server starts at `http://localhost:4321` (Astro default)
- No build errors in console output
- Home page renders with Starlight theme, dark mode toggle, and sidebar

**Pass condition**: HTTP 200 at `http://localhost:4321/` within 2 minutes of running the command.

---

## Validation 2: All Content Pages Load (SC-001)

**What this validates**: All existing documentation URLs resolve with HTTP 200.

With the dev server running, verify each URL from the [URL Structure contract](contracts/url-structure.md):

```bash
# Quick check using curl (dev server running)
for path in "/" "/guide/" "/guide/installation/" "/guide/quickstart/" \
  "/guide/aot-publishing/" "/reference/ispecular-provider/" \
  "/reference/type-filters/" "/reference/service-registration/" \
  "/reference/exclude-from-specular/" "/architecture/how-it-works/" \
  "/architecture/cross-assembly-caching/"; do
  status=$(curl -s -o /dev/null -w "%{http_code}" "http://localhost:4321${path}")
  echo "$status $path"
done
```

**Pass condition**: All paths return 200.

---

## Validation 3: API Reference Generation (SC-004, SC-006)

**What this validates**: xmldocmd generates API reference pages for 100% of public types, and no manual steps are required after `dotnet build`.

```bash
# From repo root
dotnet build -c Release
mise run docs:api
```

**Expected outcome**:

- `docs/src/content/docs/api/` directory is populated with `.md` files
- At minimum, these types are present: `ISpecularProvider`, `ServiceRegistrationAttribute`, `ExcludeFromSpecularAttribute`
- No errors in xmldocmd output (warnings for missing XML docs are acceptable)

```bash
# Verify key types exist
ls docs/src/content/docs/api/
grep -r "ISpecularProvider" docs/src/content/docs/api/
```

**Pass condition**: All public types from `src/Specular` appear as `.md` files in the `api/` directory.

---

## Validation 4: Link Validator Passes (SC-009)

**What this validates**: Zero broken internal links exist (`starlight-links-validator` passes in CI).

```bash
# Trigger a production build (includes link validation)
cd docs && npm run build
```

**Expected outcome**:

- Build completes without "broken link" errors from `starlight-links-validator`
- If broken links are found, the output shows the source file and target path

**Pass condition**: `npm run build` exits with code 0.

---

## Validation 5: All Plugins Render (SC-007)

Open the dev server and manually verify each plugin:

| Plugin                   | Validation Action                        | Expected                                               |
| ------------------------ | ---------------------------------------- | ------------------------------------------------------ |
| starlight-github-alerts  | Open any page with `> [!NOTE]` syntax    | Styled blue callout with info icon                     |
| starlight-heading-badges | Find a heading with `badge:` frontmatter | Badge label visible next to heading                    |
| starlight-image-zoom     | Click any image/diagram on the page      | Lightbox zoom opens                                    |
| starlight-scroll-to-top  | Scroll down on a long page               | Floating scroll-to-top button appears                  |
| starlight-page-actions   | Open any content page                    | Action buttons visible (e.g., "Edit this page")        |
| starlight-plugin-icons   | Open a page using icon syntax            | Icon renders inline                                    |
| starlight-sidebar-topics | Check sidebar                            | Topics (Getting Started, Guides, etc.) shown as groups |
| starlight-tags           | Open `/tags/` page                       | Tag index lists all tags with linked pages             |
| starlight-changelogs     | Open `/changelog/`                       | CHANGELOG.md rendered in structured format             |
| starlight-auto-drafts    | Add `draft: true` to a page, rebuild     | Page not visible in sidebar or search                  |
| starlight-llms-txt       | Request `http://localhost:4321/llms.txt` | File lists all non-draft pages                         |

---

## Validation 6: Search Works (SC-003)

**What this validates**: Typesense search returns results within 1 second.

_(Requires Typesense credentials configured in environment)_

```bash
export TYPESENSE_HOST=xxx.a1.typesense.net
export TYPESENSE_API_KEY=<search-only-key>
export TYPESENSE_COLLECTION_NAME=specular-docs
cd docs && npm run build && npm run typesense:index
```

Then open the built site and search for "ISpecularProvider" — results should appear within 1 second.

**Fallback validation** (without Typesense credentials): Verify that the search UI renders without console errors, even if results are empty.

---

## Validation 7: Build Performance (SC-002)

**What this validates**: Docs build completes in the same time or faster than the previous VitePress build.

```bash
time (cd docs && npm run build)
```

**Baseline**: VitePress build time from CI logs (reference: previous CI runs on `feature/docs` branch).

**Pass condition**: Astro build time ≤ VitePress build time (measured in CI, not local dev).

---

## Validation 8: VS Code Extension Prompt (SC-010)

**What this validates**: Contributors are prompted to install `starlight-links` automatically.

Open the project root in VS Code. Expected: a notification appears recommending extensions from `.vscode/extensions.json`.

```json
// .vscode/extensions.json — verify these entries exist
{
    "recommendations": ["astro-build.astro-vscode", "hideoo.starlight-links"]
}
```

**Pass condition**: `.vscode/extensions.json` exists with both entries; VS Code shows "Install All" prompt on project open.

---

## Validation 9: Prettier Passes (FR-026)

```bash
# From repo root
prettier --check docs/src/
```

**Pass condition**: Exit code 0 (no formatting violations in docs Markdown/MDX files).

---

## CI Validation Checklist

When the CI deploy-docs.yml workflow runs, all of the above must pass automatically. The CI workflow should:

1. `dotnet build -c Release` ✓
2. `dotnet tool restore && mise run docs:api` ✓
3. `cd docs && npm ci && npm run build` ✓ (includes link validator)
4. Typesense index rebuild (using CI secrets) ✓
5. Deploy static output ✓
6. Smoke-test deployed URLs (validate HTTP 200 for all contract URLs) ✓
