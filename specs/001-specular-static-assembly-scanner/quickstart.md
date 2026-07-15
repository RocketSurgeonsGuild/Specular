# Quickstart Validation Guide: Specular VitePress Documentation Site

**Date**: 2026-06-25
**Validates**: US3 / FR-010 / FR-011 / SC-003 / SC-006

This guide describes how to verify the documentation site and linting configuration work
end-to-end. It is a validation guide, not an implementation guide.

---

## Prerequisites

- `mise install` completed (installs vitepress, bun, hk, actionlint, prettier)
- `npm install` completed at repo root (installs ESLint + plugins)
- Repository cloned with full git history (`--depth 0`) for `lastUpdated` to work

---

## Scenario 1: Local Dev Server (SC-003 precondition)

**Purpose**: Confirm the site builds and serves without errors.

```bash
mise run docs
```

**Expected outcome**:

- Terminal shows `vitepress v2.x.x dev server running at http://localhost:5173/Specular/`
- Navigating to `http://localhost:5173/Specular/` shows the Specular home page with hero text
  "Compile-time assembly scanning for .NET"
- All nav links (Guide, Reference, Architecture) resolve without 404
- Sidebar renders all entries from the data model (4 Guide pages, 4 Reference pages,
  2 Architecture pages)

---

## Scenario 2: Production Build (FR-010 gate)

**Purpose**: Confirm the build produces a valid static site with no dead links.

```bash
mise run docs-build
```

**Expected outcome**:

- Exit code 0
- `docs/.vitepress/dist/` contains `index.html` and at least 10 HTML files corresponding
  to the content pages
- No "dead link" warnings in build output
- No TypeScript errors from `config.mts`

---

## Scenario 3: ESLint Clean Pass (IV. Code Quality gate)

**Purpose**: Confirm all Vue, TypeScript, and Markdown files in docs pass linting.

```bash
npx eslint docs/.vitepress
```

**Expected outcome**: Exit code 0. No errors or warnings.

---

## Scenario 4: hk Pre-Commit Hook (IV. Code Quality gate)

**Purpose**: Confirm the hk pre-commit hook runs ESLint fix and prettier on staged docs files.

```bash
# Stage a .vue or .ts file with a deliberate lint issue, then commit
git add docs/.vitepress/theme/index.ts
git commit -m "test hook"
```

**Expected outcome**: `hk fix` runs automatically before the commit. If the lint issue is
auto-fixable, the file is fixed and the commit proceeds. If not auto-fixable, the commit is
rejected with a clear ESLint error message.

---

## Scenario 5: actionlint on Deploy Workflow (FR-011 gate)

**Purpose**: Confirm the deploy workflow passes the actionlint checker.

```bash
actionlint .github/workflows/deploy-docs.yml
```

**Expected outcome**: Exit code 0. No errors.

---

## Scenario 6: GitHub Pages Deployment (SC-006 gate)

**Purpose**: Confirm the docs deploy to GitHub Pages within 10 minutes of a merge.

1. Merge a PR to `main` containing a docs change.
2. Navigate to the repository's **Actions** tab on GitHub.
3. Observe the `Deploy Docs` workflow run triggered by the merge.
4. Note the workflow start time.

**Expected outcome**:

- The `Deploy Docs` workflow completes successfully (green checkmark).
- The deployed site at `https://rocketsurgeonsguild.github.io/Specular/` reflects the
  content change.
- Elapsed time from workflow trigger to live site update is ≤ 10 minutes.

---

## Scenario 7: Developer Quickstart (SC-003 primary)

**Purpose**: Validate that a developer new to Specular can reach a working integration
in under 15 minutes using only the live documentation site.

**Setup**: Use a blank .NET 8 or .NET 10 console/web project with no prior Specular knowledge.

**Steps** (follow the live docs at `https://rocketsurgeonsguild.github.io/Specular/guide/`):

1. Follow the Installation page to add the Specular NuGet package.
2. Follow the Quickstart page to write a type selector and call `GetTypes()`.
3. Build the project — confirm no build errors and that generated code appears under
   `obj/` in the project directory.
4. Follow the AOT Publishing page to publish with `PublishAot=true`.
5. Confirm the published binary runs without trim warnings.

**Expected outcome**: All 5 steps complete in under 15 minutes. Zero trim warnings.
The developer did not need to read source code or open GitHub.
