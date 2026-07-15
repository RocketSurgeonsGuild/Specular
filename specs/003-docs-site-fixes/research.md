# Phase 0 Research: Docs Site Fixes & Plugin Verification

This feature touches an already-built Starlight site, so "research" here means resolving the concrete
mechanics of each fix against the running site rather than choosing a tech stack.

## R1 — Why the section roots 404, and the fix

**Decision**: Add an `index.md` landing page at `reference/`, `architecture/`, and `api/`.

**Rationale**: `starlight-sidebar-topics` defines each topic with a `link` to the section root
(`/reference/`, `/architecture/`, `/api/`). Starlight only serves a page at that URL if a content
file resolves there. `guide/` has `index.md` and resolves; the other three have no index file, so the
topic link lands on Starlight's 404 page. The existing `guide/index.md` (frontmatter: `title`,
`description`, optional `badge`, `tags`; body with intro + links into the section) is the proven
template to mirror.

**Alternatives considered**: Retarget each topic `link` to an existing first page
(e.g. `/reference/type-filters/`). Rejected during brainstorming — the section root URL would still
404 if visited directly, and landing pages give readers a better orientation. (User decision: index
landing pages.)

## R2 — Base path correctness (root locally vs `/Specular` in CI)

**Decision**: Landing pages and any new links use Starlight's link handling; do not hard-code a
`/Specular` prefix. Verify the built site under the CI base by building with `GITHUB_ACTIONS=1` if link
resolution is in doubt.

**Rationale**: `astro.config.mjs` sets `base = process.env.GITHUB_ACTIONS ? '/Specular' : ''`. Astro
prepends `base` to internal links it controls (nav, sidebar, generated routes). Root-relative links
written by hand in markdown are the known risk area; the link validator runs at build time and is the
gate. The existing pages already use root-relative links (e.g. `/guide/quickstart`) and the validator
currently tolerates relative links (`errorOnRelativeLinks: false`).

**Alternatives considered**: Switch to fully relative links everywhere — rejected as a larger change
than this feature warrants; the validator + a CI-base build check is sufficient assurance.

## R3 — Per-plugin activation requirements

Audit of current content shows several plugins have **nothing to demonstrate** until activating
content is added. Decision per plugin (goal: all Working/Fixed; removal last resort):

| Plugin                      | What it does                       | Current state                                                                                 | Action to make demonstrable                                                                               |
| --------------------------- | ---------------------------------- | --------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------- |
| `starlight-sidebar-topics`  | Topic-based sidebar                | Working, but topics 404                                                                       | Fixed by R1 landing pages                                                                                 |
| `starlight-links-validator` | Build-time broken-link check       | Active                                                                                        | Confirm zero errors on build (gate)                                                                       |
| `starlight-github-alerts`   | GitHub `> [!NOTE]` alert rendering | **No alert syntax used anywhere**                                                             | Add representative alert(s) in a landing/content page                                                     |
| `starlight-heading-badges`  | Badges on headings                 | **No heading-badge syntax used**                                                              | Add a heading badge in a landing page                                                                     |
| `starlight-image-zoom`      | Click-to-zoom images               | **No images in content**                                                                      | Add one representative image, OR document that no content image exists yet                                |
| `starlight-tags`            | Tag pages / filtering              | `tags.yml` present, `tags:` frontmatter used, but `starlightTags()` called with **no config** | Confirm whether plugin auto-discovers `tags.yml`; wire it explicitly if required; verify `/tags/` renders |
| `starlight-changelogs`      | Changelog from GitHub releases     | Loader needs `GH_API_TOKEN`                                                                   | Verify `/changelog/` renders when token present; document local-dev limitation when absent                |
| `starlight-llms-txt`        | Generates `llms.txt`               | Active                                                                                        | Confirm `/llms.txt` (and variants) generated on build                                                     |
| `starlight-auto-drafts`     | Hides draft pages in prod          | Active                                                                                        | Confirm a `draft: true` page is hidden in build, shown in dev                                             |
| `starlight-scroll-to-top`   | Scroll-to-top button               | Active                                                                                        | Confirm button appears on a long page                                                                     |
| `starlight-image-zoom`      | (see above)                        |                                                                                               |                                                                                                           |
| `starlight-page-actions`    | Edit/page action links             | `editLink` configured                                                                         | Confirm action renders and points to GitHub edit URL                                                      |
| `starlight-plugin-icons`    | Extra icon set                     | Active                                                                                        | Confirm an icon from the set renders (sidebar topic icons already use `seti:`)                            |

**Decision**: For plugins needing content (alerts, heading badges, image), add minimal representative
content inside the new section landing pages so activation doubles as useful documentation. For
`starlight-tags`, treat "does it auto-load `tags.yml`?" as a verification step and wire explicitly if
the `/tags/` page is empty/missing. For `starlight-changelogs`, document the token dependency and
verify with a token if available; absence must not break the build.

**Rationale**: Activation content placed in real pages avoids throwaway fixtures and serves readers.
The matrix in `contracts/plugin-verification-matrix.md` records evidence + final status per plugin.

## R4 — API landing page coexistence with generation

**Decision**: Author `api/index.md` as a hand-written file; confirm `docs/scripts/add-api-frontmatter.mjs`
(and any API generation) does not overwrite or delete `index.md`.

**Rationale**: The API tree under `api/` is auto-generated; a stray generation step could clobber a
hand-authored index. Verification: run the generation script (or build) and confirm `api/index.md`
survives and `/api/` renders.

**Alternatives considered**: Generate the API index as part of the API pipeline — rejected as
out-of-scope; a static hand-authored landing page is simplest and sufficient.

## R5 — Verification method (browser MCP + build)

**Decision**: Two-layer verification. (1) Live: integrated browser MCP drives the running `astro dev`
server (port 4321, confirmed running) to confirm each URL returns a real page and each plugin feature
renders in light + dark mode. (2) Build: `astro build` must succeed and the link validator must report
zero broken internal links.

**Rationale**: The user enabled the browser MCP specifically to verify rendering; the spec's success
criteria are about live resolution and a clean build. This also lets us close the deferred spec-002
manual tasks (which were blocked precisely because no browser was available).

## Resolved unknowns

All Technical Context items are resolved; no `NEEDS CLARIFICATION` markers remain. The only
externally-gated item is the changelog GitHub token, explicitly handled as a documented limitation
(R3) rather than a blocker.
