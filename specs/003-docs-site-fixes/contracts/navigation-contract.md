# Contract: Navigation Resolution

The docs site's navigation contract: every primary topic resolves to a real page, and the production
build has zero broken internal links.

## Topic resolution contract

| Topic           | URL (local)      | URL (CI)                | Expected                                                                  |
| --------------- | ---------------- | ----------------------- | ------------------------------------------------------------------------- |
| Getting Started | `/guide/`        | `/Indago/guide/`        | HTTP 200, landing page (existing — no regression)                         |
| Reference       | `/reference/`    | `/Indago/reference/`    | HTTP 200, NEW landing page with links to all 4 reference pages            |
| Architecture    | `/architecture/` | `/Indago/architecture/` | HTTP 200, NEW landing page with links to both architecture pages          |
| API Reference   | `/api/`          | `/Indago/api/`          | HTTP 200, NEW landing page introducing the generated API tree             |
| Changelog       | `/changelog/`    | `/Indago/changelog/`    | HTTP 200 (plugin-generated — no regression; token caveat per research R3) |

**Pass criteria**: all five return HTTP 200 with meaningful content (not the Starlight 404 page),
verified live in the integrated browser MCP.

## Link-validator contract

- Command: `astro build` (runs `starlight-links-validator` as configured).
- Expected: build succeeds; validator reports **zero broken internal links**.
- Config note: `errorOnRelativeLinks: false` is retained; fixes MUST NOT introduce new broken
  absolute links.

## Base-path contract

- Local (`base = ''`) and CI (`base = '/Indago'`) MUST both resolve all five topics.
- No hand-authored link may hard-code `/Indago`.
- Verification: a `GITHUB_ACTIONS=1 astro build` is link-clean if base correctness is in doubt.
