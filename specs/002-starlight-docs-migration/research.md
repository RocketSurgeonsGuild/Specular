# Research: Migrate Documentation Site to Starlight

**Phase 0 output for plan.md**
**Date**: 2026-06-26
**Feature**: specs/002-starlight-docs-migration

---

## Decision 1: API Documentation Generator

**Decision**: Use **xmldocmd** (XmlDocMarkdown by Faithlife)

**Rationale**:

- Outputs pure Markdown files with no HTML dependencies — direct drop-in for Starlight's content collection
- Reads from compiled DLL + `.xml` doc file pairs (standard `<GenerateDocumentationFile>true</GenerateDocumentationFile>` output)
- Active maintenance (v2.9.0, 2024), MIT licensed
- Works as a `dotnet tool` — no extra runtime required; installable via `dotnet tool restore`
- Supports Cake and Nuke build integrations (aligns with project's existing Nuke pipeline in `.build/`)
- Output is flat Markdown files compatible with Starlight sidebar auto-discovery

**Alternatives considered**:

- **xmldoc2md**: Similar approach but less active community; xmldocmd preferred
- **DocFX**: Outputs HTML static site, not Markdown — incompatible with Starlight as content source
- **Sandcastle/SHFB**: Legacy proprietary formats (CHM, HTML Help); not suitable for modern web docs

**Integration point**: Run `xmldocmd src/Indago/bin/Release/net8.0/Indago.dll docs/src/content/docs/api/` after `dotnet build -c Release`. Wire into `mise run docs:api` task.

---

## Decision 2: Typesense Deployment Mode

**Decision**: **Typesense Cloud** (managed service) for production; local Typesense server via Docker for development validation

**Rationale**:

- Typesense Cloud has a free tier ("Hobbyist") sufficient for a library documentation site
- Eliminates server maintenance burden vs. self-hosted
- API keys stored as CI secrets (`TYPESENSE_HOST`, `TYPESENSE_API_KEY`, `TYPESENSE_COLLECTION_NAME`)
- `starlight-docsearch-typesense` integrates via environment variables, keeping credentials out of committed code
- Local Docker validation: `docker run -p 8108:8108 typesense/typesense:latest` for dev environment smoke testing

**Alternatives considered**:

- **Self-hosted Typesense on VPS**: More control but requires infra management; overkill for OSS library docs
- **Algolia DocSearch**: Free for OSS but requires application/approval process; Typesense is immediately usable

**Configuration shape**:

```
TYPESENSE_HOST=xxx.a1.typesense.net
TYPESENSE_PORT=443
TYPESENSE_PROTOCOL=https
TYPESENSE_API_KEY=<search-only key stored in CI>
TYPESENSE_COLLECTION_NAME=indago-docs
```

---

## Decision 3: Astro/Starlight Version

**Decision**: Astro **5.x** with Starlight **0.35+** (latest stable at planning time)

**Rationale**:

- Astro 5 provides stable content layer and collections API used by all plugins
- Starlight 0.35+ has stable plugin API that all listed plugins target
- All 14 plugins in FR-006 through FR-018 are verified compatible with Starlight 0.35+
- Node.js 20+ required (already available in CI via mise)

**Node.js requirement**: ≥ 20.0.0 (Astro 5 minimum)

---

## Decision 4: Content Recovery Strategy

**Decision**: Recover historical docs content from **git commit `0f4311b`** (the architecture section merge commit, which is the most recent commit that has the full content set)

**Rationale**: The current HEAD on `feature/docs` has only the VitePress default scaffold (6 files). The full docs content (guide/, reference/, architecture/ sections) was committed in the worktree-based development flow and exists at commit `0f4311b`. The content must be recovered via `git show` or `git checkout 0f4311b -- docs/` before migration.

**Content inventory at `0f4311b`**:

```
docs/guide/aot-publishing.md
docs/guide/index.md
docs/guide/installation.md
docs/guide/quickstart.md
docs/reference/exclude-from-indago.md
docs/reference/iindago-provider.md
docs/reference/service-registration.md
docs/reference/type-filters.md
docs/architecture/cross-assembly-caching.md
docs/architecture/how-it-works.md
docs/index.md
docs/api-examples.md     (VitePress example — remove)
docs/markdown-examples.md (VitePress example — remove)
docs/tsconfig.json
```

**VitePress → Starlight content changes required**:

- Frontmatter: `layout: home` → `template: splash`; hero format differs slightly
- Custom callouts: If any `:::tip` / `:::warning` VitePress syntax found → convert to `> [!TIP]` / `> [!WARNING]` GitHub alert format (handled by `starlight-github-alerts`)
- Internal links: VitePress uses `/guide/installation`; Starlight uses the same pattern — likely compatible
- Remove VitePress-example files (`api-examples.md`, `markdown-examples.md`)

---

## Decision 5: Package Structure

**Decision**: Create a **separate `docs/package.json`** for the Astro/Starlight project; keep root `package.json` for formatter-only tooling

**Rationale**:

- Isolates Astro/npm dependencies from the root formatter tooling
- Follows Astro's convention (Astro projects have their own `package.json`)
- Root `package.json` retains prettier and oxlint; docs `package.json` manages Astro, Starlight, plugins
- `mise run docs` task runs from `docs/` directory

**Workspace consideration**: No npm workspaces needed; the docs project is self-contained and not importing from the root.

---

## Decision 6: Sidebar Organization (starlight-sidebar-topics)

**Decision**: Organize the sidebar into **5 topic groups**

| Topic           | Content                                                                                                           |
| --------------- | ----------------------------------------------------------------------------------------------------------------- |
| Getting Started | guide/index, guide/installation, guide/quickstart                                                                 |
| Guides          | guide/aot-publishing (and future guides)                                                                          |
| Reference       | reference/iindago-provider, reference/type-filters, reference/service-registration, reference/exclude-from-indago |
| Architecture    | architecture/how-it-works, architecture/cross-assembly-caching                                                    |
| API Reference   | api/\* (auto-generated by xmldocmd)                                                                               |
| Changelog       | changelog/\* (rendered by starlight-changelogs)                                                                   |

---

## Decision 7: URL Preservation

**Decision**: Preserve existing URL paths (no redirects needed for current minimal content)

**Rationale**: The existing content paths (`/guide/`, `/reference/`, `/architecture/`) are preserved directly in Starlight's `src/content/docs/` directory structure. Starlight maps `src/content/docs/guide/installation.md` → `/guide/installation/` automatically.

**Exception**: VitePress example pages (`/api-examples`, `/markdown-examples`) will be removed as they are not real documentation.

---

## Decision 8: CI/CD Integration

**Decision**: Update `.github/workflows/deploy-docs.yml` to use Astro build; add Typesense indexing step

**Build sequence in CI**:

1. `dotnet restore && dotnet build -c Release` (produces DLLs + XML docs)
2. `dotnet tool restore` (installs xmldocmd)
3. `xmldocmd` → generates `docs/src/content/docs/api/*.md`
4. `cd docs && npm ci && npm run build` (Astro build with all plugins)
5. Typesense index rebuild (using Typesense API with `TYPESENSE_API_KEY` CI secret)
6. Deploy static output to GitHub Pages

**Mise tasks**:

- `docs` → starts Astro dev server (`cd docs && npm run dev`)
- `docs:build` → full build including API doc generation
- `docs:api` → API doc generation only

---

## Constitution Compliance Notes

This feature is **documentation infrastructure only** — it does not touch the Roslyn generator, public API, or test suite.

| Principle                | Impact                                                          | Status    |
| ------------------------ | --------------------------------------------------------------- | --------- |
| I. AOT Safety            | Not applicable (docs tooling only)                              | N/A       |
| II. Test-First           | Not applicable (no generator changes)                           | N/A       |
| III. Minimal API Surface | No public API changes                                           | Clear     |
| IV. Code Quality         | Prettier config unchanged; docs MDX exempt from `dotnet format` | Clear     |
| V. Documentation         | This IS the documentation feature — fully satisfies Principle V | Fulfilled |
