# Quickstart: Verifying the Docs Site Fixes

Validation guide proving this feature works end-to-end. Run against the live dev server with the
integrated browser MCP, then confirm with a production build.

## Prerequisites

- Dev server running: `astro dev` (already running on **http://localhost:4321**). Start with
  `cd docs && npm run dev` if not.
- Integrated browser MCP available in VS Code.
- Node ≥ 20.

## Scenario 1 — Navigation resolves (US1 / FR-001..FR-004)

For each topic, navigate in the browser and confirm a real page renders (not the 404 page):

- http://localhost:4321/guide/ → existing landing page (no regression)
- http://localhost:4321/reference/ → NEW landing page, links to all reference pages work
- http://localhost:4321/architecture/ → NEW landing page, links to architecture pages work
- http://localhost:4321/api/ → NEW landing page, links into generated API tree work
- http://localhost:4321/changelog/ → renders (token caveat per research R3)

**Expected**: all five HTTP 200 with meaningful content.

## Scenario 2 — Clean production build + link validator (US1 / FR-004, FR-010)

```bash
cd docs && npm run build
```

**Expected**: build succeeds; `starlight-links-validator` reports zero broken internal links. If
base-path correctness is in doubt, also run `GITHUB_ACTIONS=1 npm run build` and confirm it is
link-clean.

## Scenario 3 — Plugin verification (US2 / FR-005..FR-007)

Walk each row of [contracts/plugin-verification-matrix.md](contracts/plugin-verification-matrix.md):
exercise the feature on the live site (light + dark mode where visual), record evidence, and set
status. Add activating content (alerts, heading badge, image, tag wiring) where the matrix calls for
it, then re-verify.

**Expected**: every plugin row has status Working / Fixed / Removed; no blanks; every Removed has a
reason.

## Scenario 4 — Deferred spec-002 checks (US3 / FR-008)

Execute each and record pass/follow-up:

- **T029** Mobile layout ≤ 768px: sidebar reachable via menu, no horizontal scroll.
- **T030** Dark mode auto-detection from OS setting.
- **T035** `deploy-docs.yml` end-to-end (workflow_dispatch / push) → Pages deploy succeeds.
- **T043** `/tags/` index lists tagged pages; links work.
- **T045** `/changelog/` renders in light + dark mode.
- **T051** VS Code recommended-extensions prompt appears; `hideoo.starlight-links` gives link
  completion when editing a `.md` under `docs/src/content/docs/`.
- **T053** Run all quickstart scenarios (this file).
- **T055** Each plugin's key feature renders in light + dark mode.

**Expected**: every task marked pass or has a recorded follow-up item.

## Scenario 5 — Formatting gate (FR-011)

```bash
prettier --check "docs/**/*.{md,mdx,mjs,ts,yml}"
```

**Expected**: passes (run `prettier --write` first if needed).

## Done

Feature is complete when: all 5 topics resolve (SC-001), build is link-clean (SC-002), every plugin
has a recorded status (SC-003), every deferred task passes or has a follow-up (SC-004), the build
succeeds with zero errors (SC-005), and a reader can reach every section from the home page (SC-006).
