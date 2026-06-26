# Work Routing

How to decide who handles what.

## Routing Table

| Work Type                     | Route To                          | Examples                                                                                                            |
| ----------------------------- | --------------------------------- | ------------------------------------------------------------------------------------------------------------------- |
| Generator / source generation | roslyn-engineer                   | `IndagoProviderGenerator`, syntax providers, symbol visitors, `ctpjson` cache, `AssemblyProviders/`, `Descriptors/` |
| Roslyn version compat         | roslyn-engineer                   | Roslyn 4.8/4.14/5.0 variants, `EnforceExtendedAnalyzerRules`, `Polyfill`                                            |
| Runtime API / DI              | dotnet-engineer                   | `IIndagoProvider`, attributes, `AddIndagoServiceRegistrations`, `src/Indago/`                                       |
| AOT / trim safety             | dotnet-engineer                   | `PublishAot=true`, trim warnings, IL linking                                                                        |
| NuGet / packaging             | dotnet-engineer                   | `Directory.Packages.props`, csproj, `buildTransitive` targets                                                       |
| Public API surface            | dotnet-engineer                   | RS0017 PublicAPI files, breaking-change review                                                                      |
| Docs content / site           | docs-engineer                     | Pages under `docs/`, VitePress config, sidebar, nav, `index.md`                                                     |
| ESLint / linting config       | docs-engineer                     | `eslint.config.mjs`, `hk` hook integration, `.vue`/`.ts`/`.md` lint rules                                           |
| GitHub Actions / Pages        | docs-engineer                     | `deploy-docs.yml`, GitHub Pages deployment, CI lint job                                                             |
| Snapshot tests                | qa-engineer                       | `.received.cs` / `.verified.cs` diffs, `dotnet verify accept`                                                       |
| Generator tests               | qa-engineer                       | `test/Indago.Tests/`, `GeneratorTest` builder, `TestAssembly/` fixtures                                             |
| Cross-version CI              | qa-engineer                       | Roslyn 4.8/4.14/5.0 snapshot parity, MTP treenode-filter runs                                                       |
| Code review                   | roslyn-engineer / dotnet-engineer | PRs touching generator or runtime library respectively                                                              |
| Session logging               | Scribe                            | Automatic — always background, never blocked                                                                        |
| RAI review                    | Rai                               | Content safety, credential detection, bias checks                                                                   |
| Memory / context              | ralph                             | Cross-session context, project state recall                                                                         |

## Regex Routing Signals

| Pattern                                                                                    | Agent           |
| ------------------------------------------------------------------------------------------ | --------------- |
| `/\bIIncrementalGenerator\|syntax provider\|symbol visitor\|AssemblyProvider\|ctpjson\b/i` | roslyn-engineer |
| `/\bRoslyn (4\.8\|4\.14\|5\.0)\|EnforceExtendedAnalyzerRules\|Polyfill\b/i`                | roslyn-engineer |
| `/\bIIndagoProvider\|ServiceRegistration\|ExcludeFromIndago\|AddIndago\b/i`                | dotnet-engineer |
| `/\bPublishAot\|trim warn\|AOT compat\|IL trim\b/i`                                        | dotnet-engineer |
| `/\bVitePress\|docs\/(guide\|reference\|architecture)\|sidebar\|navbar\b/i`                | docs-engineer   |
| `/\beslint\|hk fix\|hk check\|deploy-docs\|GitHub Pages\b/i`                               | docs-engineer   |
| `/\bsnapshot\|\.received\.\|\.verified\.\|dotnet verify\|TUnit\|Shouldly\b/i`              | qa-engineer     |
| `/\btest(ing)?\|coverage\|GeneratorTest\|TestAssembly\|treenode-filter\b/i`                | qa-engineer     |

## Issue Routing

| Label                   | Action                                               | Who             |
| ----------------------- | ---------------------------------------------------- | --------------- |
| `squad`                 | Triage: analyze issue, assign `squad:{member}` label | Lead            |
| `squad:roslyn-engineer` | Pick up generator / Roslyn compatibility work        | roslyn-engineer |
| `squad:dotnet-engineer` | Pick up runtime API / AOT / packaging work           | dotnet-engineer |
| `squad:docs-engineer`   | Pick up docs content / site / deployment work        | docs-engineer   |
| `squad:qa-engineer`     | Pick up test / snapshot / coverage work              | qa-engineer     |
| `squad:ralph`           | Context persistence or cross-session recall needed   | ralph           |
| `squad:scribe`          | Session log or decision record needed                | Scribe          |
| `squad:rai`             | RAI / content safety review needed                   | Rai             |

### How Issue Assignment Works

1. When a GitHub issue gets the `squad` label, the **Lead** triages it — analyzing content, assigning the right `squad:{member}` label, and commenting with triage notes.
2. When a `squad:{member}` label is applied, that member picks up the issue in their next session.
3. Members can reassign by removing their label and adding another member's label.
4. The `squad` label is the "inbox" — untriaged issues waiting for Lead review.

## Rules

1. **Eager by default** — spawn all agents who could usefully start work, including anticipatory downstream work.
2. **Scribe always runs** after substantial work, always as `mode: "background"`. Never blocks.
3. **Quick facts → coordinator answers directly.** Don't spawn an agent for "what port does the server run on?"
4. **When two agents could handle it**, pick the one whose domain is the primary concern.
5. **"Team, ..." → fan-out.** Spawn all relevant agents in parallel as `mode: "background"`.
6. **Anticipate downstream work.** When a generator feature is built, spawn qa-engineer to write test cases from acceptance scenarios simultaneously.
7. **Issue-labeled work** — when a `squad:{member}` label is applied to an issue, route to that member. The Lead handles all `squad` (base label) triage.
