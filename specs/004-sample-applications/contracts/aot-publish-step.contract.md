# Contract: AOT Publish Step (demonstration hosts)

**Applies to**: `Specular.Samples.Console`, `Specular.Samples.Web`, `Specular.Samples.Blazor`,
`Specular.Samples.Maui` (FR-011–FR-017, SC-004/005/006).
**Excludes**: the negative-fixture host (see `negative-fixture-step.contract.md`).

This is a behavioral contract for the named ModularPipelines step(s) that AOT-publish each
demonstration host. It defines inputs, the required outcome semantics, and the observable result —
not the exact MSBuild property strings (finalized via research D4).

## Inputs

| Input          | Value                                                                                                    |
| -------------- | -------------------------------------------------------------------------------------------------------- |
| Host project   | one demonstration host `.csproj`                                                                         |
| TFM            | `net10.0` (MAUI: `net10.0-android`) — single deterministic matrix, **no fan-out over `net8.0`** (FR-013) |
| RID            | current-OS RID of the CI job (e.g. `linux-x64` on `ubuntu-latest`)                                       |
| AOT mode       | Native AOT (console/web) · WASM AOT (blazor) · platform-MAUI (maui)                                      |
| Warning policy | warnings-as-errors for the publish closure (zero-warning, FR-012)                                        |

## Required behavior (MUST)

1. The step MUST be a **discrete, named** pipeline step whose result is individually visible in
   the build output (FR-014/017, SC-006).
2. The step MUST publish using the host-appropriate AOT/trim mode (FR-011).
3. The publish MUST fail the step (and the build) if it **fails** OR emits **any** trim/AOT
   warning of any kind — Specular-attributable or third-party (FR-012/015, SC-004).
4. When the required toolchain/workload is **absent**, the step MUST report `skip` with a clear,
   actionable reason and MUST NOT report `pass` (FR-016, SC-005). Use
   `ModuleConfiguration.WithSkipWhen(() => SkipDecision.Of(<missing>, "<reason>"))`.
5. For MAUI: `net10.0-android` is the genuinely-published target subject to rules 2–3; iOS,
   MacCatalyst, and Windows MUST always be reported as `skip` with an out-of-CI-scope reason
   (FR-006/016, SC-005).
6. The published demonstration host, when run, MUST assert its expected service set and exit
   `0` only on a match (see `host-assertion.contract.md`).

## Outcome semantics

| Condition                                | Outcome                         |
| ---------------------------------------- | ------------------------------- |
| Publish succeeds, zero trim/AOT warnings | `PASS`                          |
| Publish fails OR ≥1 trim/AOT warning     | `FAIL` (fails the build)        |
| Required workload/toolchain missing      | `SKIP` (+ reason)               |
| MAUI non-android platform                | `SKIP` (out-of-CI-scope reason) |

## Implementation guidance (non-normative)

- Invoke via `context.DotNet().Publish(new DotNetPublishOptions { … }.BinlogTo(context, binlog))`.
- Native AOT: `PublishAot=true`, `SelfContained=true`, `-r <rid> -f net10.0`.
- WASM AOT: `RunAOTCompilation=true`, requires the `wasm-tools` workload (skip-with-reason if
  absent).
- MAUI-android: `-f net10.0-android` with platform-appropriate trimming/AOT; requires MAUI +
  Android workloads (skip-with-reason if absent).
- Build modules MUST log via `context.Logger` (the `ConsoleUse` analyzer forbids `System.Console`).

## Pinned warning-enforcement property set (D4 — RESOLVED)

> Pinned by T009 (Native AOT spike), 2026-06-29, SDK `10.0.301`, `Microsoft.DotNet.ILCompiler`
> `10.0.9`. Future SDK drift that changes these is a deliberate, reviewed contract edit.

**Native AOT (Console / Web).** The enforcement properties live **in the host `.csproj`** (scoped to
the host), NOT passed as global `-p:` on the command line. Passing `PublishAot`/`TreatWarningsAsErrors`
globally propagates into the `netstandard2.0` dependency closure (`src/Specular`, analyzers) and fails
with `NETSDK1207` and unrelated warning-as-error promotions — so the pipeline module publishes the
host project with **no propagating `-p:` switches**:

```xml
<PublishAot>true</PublishAot>            <!-- self-contained AOT compile on publish -->
<InvariantGlobalization>true</InvariantGlobalization>
<TrimmerSingleWarn>false</TrimmerSingleWarn>   <!-- surface each IL2xxx/IL3xxx individually -->
<TreatWarningsAsErrors>true</TreatWarningsAsErrors> <!-- any trim/AOT warning ⇒ publish FAILS -->
```

Publish invocation: `-c Release -f net10.0 -r <currentRID>` (single deterministic matrix, FR-013).

**RED proof (T009).** Injecting `Assembly.GetExecutingAssembly().GetTypes()` into the Console host
produced `error IL2026` and the publish exited non-zero — confirming the property set fails the
publish on a trim/AOT warning. Removing the injection restores a clean trim/AOT analysis (zero IL
warnings). Each host repeats this red proof (Console T020, Web T035, …).

**Specular core prerequisite (discovered by T009).** The `ISpecularProvider.EntryAssembly` path resolved
the generated provider via `Activator.CreateInstance(Type)` in `SpecularHashAttribute`, which emitted
`IL2077` under Native AOT. Fixed by annotating the `Type` with
`[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)]`
(Constitution Principle I). Without that fix every zero-warning AOT publish that touches
`EntryAssembly` fails.

**Local toolchain note (D4 / skip-with-reason).** On `osx-arm64` the Native AOT **link** step needs a
chain of Homebrew native libs (`openssl@3`, `brotli`, …) on the linker search path; absent by default
the link fails (`ld: library 'ssl'/'brotlienc' not found`) **after** a clean trim/AOT analysis. Per the
resolved decision, AOT-publish modules **publish for real in CI (Linux)** and report **skip-with-reason**
locally when the native link prerequisites are unsatisfied — never `pass` (FR-016, rule 4).

## Acceptance mapping

- US2 (AC2), US3 (AC1–3), US4 (AC2), US5 (AC2), US6 (AC2/AC3), SC-004/005/006.
