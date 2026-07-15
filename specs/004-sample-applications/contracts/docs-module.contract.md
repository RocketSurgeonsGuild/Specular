# Contract: Docs Build Module (`build/Build.cs`)

**Applies to**: the named docs module folded into the ModularPipelines build (FR-022–FR-029,
SC-008/009/010/011).

A named `Module<T>` (e.g. `DocsModule`) authored inline in `build/Build.cs` that generates the API
reference from the Specular Release **bin** output and builds the Astro/Starlight site into
`artifacts/docs`. The module stays deploy-agnostic; CI handles the Pages deploy gated to `main`.

## Dependencies

- `[DependsOn<BuildSolution>]` — the Release build MUST have produced
  `src/Specular/bin/Release/net8.0/Specular.dll` **and** `Specular.xml` first.
  (`SolutionSettings.Configuration` defaults to `Release`.)

## Required behavior (MUST), in order

1. **Precondition check** — verify `src/Specular/bin/Release/net8.0/Specular.dll` and `Specular.xml`
   exist. If `Specular.xml` is missing, the module MUST **fail clearly** rather than emit an empty
   or partial API reference (Edge Case "Docs build consumes bin"; FR-023).
2. **API reference generation** — run `xmldocmd` (mise dotnet tool `xmldocmd@2.9.0`) against the
   **bin** `Specular.dll` + sibling `Specular.xml`. MUST NOT perform a separate `dotnet publish` to a
   temp directory (FR-023, SC-008). Reproduce existing flags:
   `--namespace Specular --source https://github.com/RocketSurgeonsGuild/Specular/blob/main/src/Specular/ --clean`,
   output dir `docs/src/content/docs/api/`.
3. **Frontmatter injection** — run `node docs/scripts/add-api-frontmatter.mjs` (Starlight
   frontmatter injector; idempotent — skips files already having frontmatter).
4. **Site build** — `npm ci` then `npm run build` (Astro/Starlight) in `docs/` via
   `context.Node()`.
5. **Emit artifact** — place the built static site into
   `ArtifactSettings.ArtifactsDirectory / "docs"` using `FolderExtensions` (`EnsureExists`, `/`,
   `+`). (Astro default output is `docs/dist`; copy into `artifacts/docs`.) (FR-024, SC-008.)
6. **Deploy-agnostic** — the module MUST NOT deploy anywhere (FR-028).
7. **Building blocks** — reuse `ArtifactSettings`, `SolutionSettings`, `context.DotNet()`,
   `context.Node()`, `FolderExtensions` rather than reimplementing orchestration (FR-025).
8. Log via `context.Logger` (the `ConsoleUse` analyzer forbids `System.Console`).

## Replacements (MUST, FR-026/027)

| Removed / rerouted                                                                 | Action                                                                     |
| ---------------------------------------------------------------------------------- | -------------------------------------------------------------------------- |
| `.github/workflows/deploy-docs.yml`                                                | **Remove** (build+deploy folded into `build.yml` + pipeline).              |
| mise `docs:api`                                                                    | **Remove** or convert to a thin wrapper invoking the pipeline.             |
| mise `docs:build`                                                                  | **Remove** or convert to a thin wrapper invoking the pipeline.             |
| mise `docs` (dev server)                                                           | **Keep** (local authoring, FR-029).                                        |
| mise `docs:preview`                                                                | Keep (optional).                                                           |
| `.vscode/tasks.json` `mise: docs-build` (calls non-existent `mise run docs-build`) | **Fix** the stale label — point at the pipeline or the retained docs task. |

## CI integration (GitHub Actions, separate from the module)

- `build.yml` runs `mise run build` (which runs `DocsModule`) on every CI build (FR-027).
- After the build, `build.yml` runs `actions/upload-pages-artifact` (path `artifacts/docs`) and
  `actions/deploy-pages`, **guarded by `if: github.ref == 'refs/heads/main'`** with
  `pages: write` + `id-token: write` permissions and a `pages` concurrency group (FR-028, SC-010).
- Non-`main` builds produce `artifacts/docs` but do **not** deploy (Edge Case "Pages deploy
  gating", SC-010).

## Outcome / acceptance

| Condition                                                     | Result                      |
| ------------------------------------------------------------- | --------------------------- |
| `Specular.xml` missing                                          | module FAILS clearly        |
| API ref generated from bin + site built into `artifacts/docs` | PASS (SC-008)               |
| Separate `dotnet publish` for docs occurs                     | CONTRACT VIOLATION (FR-023) |
| Deploy from non-`main` / no deploy from `main`                | DEFECT (SC-010)             |

- Acceptance mapping: US7 (AC1–3), FR-022–029, SC-008/009/010/011.
