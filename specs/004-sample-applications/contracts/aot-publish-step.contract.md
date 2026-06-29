# Contract: AOT Publish Step (demonstration hosts)

**Applies to**: `Indago.Samples.Console`, `Indago.Samples.Web`, `Indago.Samples.Blazor`,
`Indago.Samples.Maui` (FR-011–FR-017, SC-004/005/006).
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
   warning of any kind — Indago-attributable or third-party (FR-012/015, SC-004).
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
- Warning enforcement property set is pinned in research D4; each host MUST be proven to FAIL when
  a trim/AOT warning is injected (the test-first red state for this guardrail).
- Build modules MUST log via `context.Logger` (the `ConsoleUse` analyzer forbids `System.Console`).

## Acceptance mapping

- US2 (AC2), US3 (AC1–3), US4 (AC2), US5 (AC2), US6 (AC2/AC3), SC-004/005/006.
