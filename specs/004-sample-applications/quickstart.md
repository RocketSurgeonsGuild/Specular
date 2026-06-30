# Quickstart: Sample Applications, AOT Validation & Documentation Build Pipeline

**Feature**: 004-sample-applications

This is a **validation & run guide** — how to build, run, and verify the feature locally and how
CI exercises it. It references `data-model.md` and `contracts/` rather than duplicating them.
Implementation details (project bodies, module code, service definitions) belong in `tasks.md`
and the implementation phase.

## Prerequisites

- `mise` installed; run `mise install` at the repo root (provisions .NET 8 + 10, Node 22,
  `xmldocmd@2.9.0`, etc.).
- For AOT publishes locally:
    - Native AOT (Console/Web): platform C toolchain (clang/MSVC) for the current OS.
    - Blazor WASM AOT: `dotnet workload install wasm-tools`.
    - MAUI `net10.0-android`: `dotnet workload install maui-android` (+ Android SDK).
    - When a workload is absent, the corresponding pipeline step **skips with reason** (never
      "passes") — see `contracts/aot-publish-step.contract.md`.

## 1. Build everything (libraries + hosts + docs)

```bash
mise run build        # dotnet run build/Build.cs → ModularPipelines
```

Expected: solution builds (Release); the **docs module** generates the API reference from
`src/Indago/bin/Release/net8.0/Indago.dll` + `Indago.xml` and produces the built site under
`artifacts/docs`; AOT-publish steps run for the demonstration hosts (or skip-with-reason where a
workload is unavailable); the negative-fixture step runs its inverted assertion. Each AOT outcome
is a discrete, named pass/fail/skip line in the build output (SC-006).

## 2. Validate sample libraries (US1)

The three libraries collectively cover interface-matching + attribute-based registration + ≥2
lifetimes + ≥1 opt-out type (see `contracts/sample-library-inventory.contract.md`). A host (below)
is the in-context test; the opt-out type must never appear in any discovered set.

## 3. Run the Console host (US2 — P1 MVP)

```bash
dotnet run --project samples/hosts/Indago.Samples.Console
echo "exit: $?"     # 0 = expected service set matched; nonzero = mismatch
```

Expected: prints the discovered services from all three libraries; exits `0` on match. Then AOT:

```bash
dotnet publish samples/hosts/Indago.Samples.Console -f net10.0 -r <current-rid> \
  -p:PublishAot=true
# Must complete with ZERO trim/AOT warnings (warnings-as-errors). Run the published binary; exit 0.
```

(See `contracts/host-assertion.contract.md` and `contracts/aot-publish-step.contract.md`.)

## 4. Run the Web minimal-API host (US4 — P2)

```bash
dotnet run --project samples/hosts/Indago.Samples.Web
# Startup assertion fails fast on mismatch (process exits nonzero / health check fails).
# Hit the demonstration endpoint to see discovered services. Then Native AOT publish, zero warnings.
```

## 5. Run the Blazor WASM host (US5 — P3)

```bash
dotnet run --project samples/hosts/Indago.Samples.Blazor
# A component lists discovered services; mismatch surfaces a detectable failure state.
dotnet publish samples/hosts/Indago.Samples.Blazor -f net10.0 -p:RunAOTCompilation=true
# Requires wasm-tools; must publish with zero trim/AOT warnings.
```

## 6. Run the MAUI host (US6 — P3)

```bash
# net10.0-android is the only CI-exercised target; iOS/MacCatalyst/Windows are skip-with-reason.
dotnet publish samples/hosts/Indago.Samples.Maui -f net10.0-android
# Requires MAUI + Android workloads; must publish with zero trim/AOT warnings.
```

## 7. Verify the AOT pipeline guardrail (US3 — P1)

- Run `mise run build` and confirm a **named** AOT-publish step per AOT-capable host, with a
  clear `PASS`/`FAIL`/`SKIP` result (SC-006).
- **Red-state proof**: inject a trim/AOT warning into a demo host and confirm its step (and the
  build) FAILS (zero-warning policy, SC-004).
- **Negative fixture**: confirm `Indago.Samples.NegativeFixture`'s inverted-assertion step PASSES
  _because_ it warns, and that making it clean FAILS the build (SC-003,
  `contracts/negative-fixture-step.contract.md`).

## 8. Verify the docs pipeline (US7 — P2)

```bash
mise run build        # on a non-main branch
ls artifacts/docs     # built Starlight site present
```

Expected: API reference generated from **bin** (no separate `dotnet publish` for docs, SC-008);
site under `artifacts/docs`; **no** GitHub Pages deploy on non-`main` (SC-010). On `main`, CI runs
`actions/upload-pages-artifact` + `actions/deploy-pages` (gated). Confirm the standalone
`deploy-docs.yml` and the independent mise `docs:api`/`docs:build` publish path are gone (SC-009).

Dev server still works for local authoring:

```bash
mise run docs         # Astro/Starlight dev server (also a VS Code task) — SC-011
```

## 9. CI expectations

- `build.yml` runs `mise run build` on every push/PR (main/next, tags, merge_group), uploads
  existing artifacts, and additionally deploys `artifacts/docs` to GitHub Pages **only from
  `main`**.
- AOT steps skip-with-reason where workloads are unavailable on the runner; `ubuntu-latest`
  resolves current-OS RID to `linux-x64`.

## Success-criteria checklist (maps to spec SC-001…011)

- [ ] SC-001 three libraries, 3–6 services each, ≥3 styles/lifetimes
- [ ] SC-002 four hosts assert in-process; mismatch → nonzero exit / detectable failure
- [ ] SC-003 negative-fixture inverted assertion guards the detector
- [ ] SC-004 every demo host AOT-publishes for `net10.0` × current RID with zero warnings
- [ ] SC-005 missing-toolchain → skip-with-reason (never passed); MAUI non-android always skip
- [ ] SC-006 per-host AOT pass/fail/skip visible in build output
- [ ] SC-007 each host's Indago usage is documented/referenceable without running it
- [ ] SC-008 `mise run build` builds docs from bin into `artifacts/docs`
- [ ] SC-009 standalone docs publish path removed
- [ ] SC-010 docs built every CI run; Pages deploy only from `main`
- [ ] SC-011 docs dev server retained as a VS Code task
