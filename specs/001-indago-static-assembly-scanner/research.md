# Research: Indago VitePress Documentation Site

**Date**: 2026-06-25
**Feature**: [spec.md](./spec.md) | **Plan**: [plan.md](./plan.md)

## VitePress 2.0 Alpha Configuration

**Decision**: Use `docs/.vitepress/config.mts` (TypeScript) for the site config.

**Rationale**: VitePress 2.0 alpha ships with first-class TypeScript support for config files.
Using `.mts` (ESM TypeScript module) gives full type-checking on the config and aligns with the
project's `LangVersion=preview` / strict culture for C# extended to the JS/TS toolchain.

**Key config requirements**:

- `base`: Set to `/Indago/` for GitHub Pages project-site hosting (unless a custom domain maps
  to root). If a custom domain is added later, change `base` to `'/'`.
- `title` + `description`: "Indago" / "Compile-time assembly scanning for .NET"
- `themeConfig.nav`: Top-level navigation (Guide, Reference, Architecture, GitHub link)
- `themeConfig.sidebar`: Per-section sidebar defined as an object keyed by path prefix
- `themeConfig.editLink`: Points to `https://github.com/RocketSurgeonsGuild/Indago/edit/main/docs/:path`
- `themeConfig.footer`: License + GitHub link

**Alternatives considered**:

- `docs/.vitepress/config.ts` — works identically; `.mts` chosen to be explicit about ESM.
- Auto-generated sidebar (vitepress-sidebar plugin) — adds a dependency; manual sidebar is
  fine for ~20 pages and gives explicit ordering control.

---

## oxlint

**Decision**: `oxlintrc.json` at repo root using oxlint's native JSON config format.

**Rationale**: User decision (clarification 2026-06-25). oxlint is a Rust-based linter that is
significantly faster than ESLint, has built-in TypeScript support without parser packages, and
supports Vue 3 SFC rules via `--plugin vue`. For the docs site (VitePress scaffold with minimal
custom Vue), the Vue rule coverage is sufficient. Markdown fenced-block linting is out of scope —
VitePress build catches broken syntax, and content quality is the author's responsibility.

**Package selection**:

| Package  | Version | Purpose                                              |
| -------- | ------- | ---------------------------------------------------- |
| `oxlint` | `^0.x`  | All-in-one linter (TS, Vue 3, JS rules — no plugins) |

No additional parser or plugin packages are needed. oxlint bundles everything.

**Config shape** (`oxlintrc.json`):

```json
{
    "$schema": "https://raw.githubusercontent.com/oxc-project/oxc/main/npm/oxlint/configuration_schema.json",
    "plugins": ["vue", "typescript"],
    "rules": {}
}
```

**Integration with prettier**: oxlint handles logic/correctness rules only. Prettier remains
the formatter of record. No conflict — oxlint does not emit formatting rules.

**Integration with hk**: Add an `oxlint` step to `.config/hk.pkl` that runs
`oxlint --plugin vue --plugin typescript {{files}}` (check) and
`oxlint --fix --plugin vue --plugin typescript {{files}}` (fix) on staged `.vue`, `.ts`, `.mts` files.

**Alternatives considered**:

- ESLint 9 flat config — previously the plan default; superseded by oxlint per user decision.
- Biome — would replace prettier; too disruptive to existing config.
- No linter — rejected; IV. Code Quality principle requires linting.

---

## TypeScript Config for Docs

**Decision**: `docs/tsconfig.json` extending `@vue/tsconfig/tsconfig.dom.json` (or a minimal
base) scoped to `docs/**/*.ts`, `docs/**/*.mts`, `docs/**/*.vue`.

**Rationale**: VitePress components and config use browser DOM APIs. A separate tsconfig
scoped to `docs/` avoids polluting the .NET project's build with JS type-checking artifacts.

**Key settings**:

```json
{
    "compilerOptions": {
        "target": "ESNext",
        "module": "ESNext",
        "moduleResolution": "Bundler",
        "strict": true,
        "jsx": "preserve",
        "lib": ["ESNext", "DOM"]
    },
    "include": [".vitepress/**/*.ts", ".vitepress/**/*.mts", ".vitepress/**/*.vue"]
}
```

---

## GitHub Pages Deployment

**Decision**: New workflow `.github/workflows/deploy-docs.yml` using the official
`actions/upload-pages-artifact` + `actions/deploy-pages` pair.

**Rationale**: The official GitHub Pages Actions integrate cleanly with the GitHub Pages
environment protection and provide deployment status in the GitHub UI. No third-party
deployment action is needed.

**Key workflow structure**:

1. Trigger: `push` to `main` (also manual `workflow_dispatch`)
2. Jobs: `build` (run `vitepress build docs` via mise) → `deploy` (upload artifact + deploy)
3. Permissions: `pages: write`, `id-token: write` (OIDC for Pages)
4. Environment: `github-pages` (standard Pages environment name)

**`vitepress build` via mise**: `mise run docs-build` task (new) wraps `vitepress build docs`
so the workflow and local developer use the same command.

**Alternatives considered**:

- `peaceiris/actions-gh-pages` — popular third-party action; the official Pages Actions are
  now mature and preferred.
- Netlify / Vercel — no justification to leave GitHub Pages when the repo already lives on
  GitHub and the SC-006 10-minute SLA is easily met.

---

## Content Structure

**Decision**: Three top-level sections — Guide, Reference, Architecture.

| Section      | Pages | Purpose                                                                 |
| ------------ | ----- | ----------------------------------------------------------------------- |
| Guide        | 4     | Getting started, installation, quickstart, AOT publishing               |
| Reference    | 4     | API types (IIndagoProvider, type filters, ServiceRegistration, Exclude) |
| Architecture | 2     | How the generator pipeline works; cross-assembly caching                |

**Rationale**: Mirrors the structure used by successful OSS .NET library docs (e.g., Refit,
MediatR). Guide gets new users to "hello world"; Reference serves as lookup for existing users;
Architecture explains the non-obvious design decisions (AOT, caching).

**Home page**: VitePress default-theme hero + feature cards. Three feature cards:
"AOT Safe", "Build-Time Scanning", "Minimal API Surface".

---

## Scrutor Gap Analysis (FR-001 addendum)

Scrutor features **not** currently in Indago (candidates for future additions, not in this
docs scope):

| Scrutor Feature                                   | Indago Status | Notes                                                                      |
| ------------------------------------------------- | ------------- | -------------------------------------------------------------------------- |
| `UsingRegistrationStrategy` (skip/replace/append) | Missing       | Scrutor allows controlling what happens when a registration already exists |
| `Asself()` shorthand                              | Supported     | Registers the class as its own service type                                |
| `AsSingletonOf<T>()` (lifetime per-registration)  | Partial       | Indago uses attribute-based lifetime only                                  |
| Open-generic service registration                 | Missing       | e.g., `IRepository<>` → `Repository<>`                                     |

These gaps are documented here for the Reference section's "Compared to Scrutor" page
(deferred to a future docs addition after the initial site launch).
