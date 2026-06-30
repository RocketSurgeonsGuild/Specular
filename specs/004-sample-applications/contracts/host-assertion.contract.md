# Contract: Host In-Process Assertion & Pass/Fail Signalling

**Applies to**: all four demonstration hosts (FR-007–FR-010, SC-002).

Each host performs Indago compile-time scanning of the three sample libraries, computes the
actual discovered service set, and asserts it against a hard-coded expected set. Mismatch MUST be
visible and machine-detectable by the ModularPipelines run step.

## Common requirements (MUST)

1. The host MUST use Indago's **compile-time** scanning (generated `IIndagoProvider` / DI
   registration) — **no runtime reflection-based scanning** (FR-007).
2. The host MUST consume all three shared libraries **without modifying** them (FR-005).
3. The host MUST hold an explicit `expectedServiceSet` and compare it to the actual discovered set
   (including correct service→implementation mappings and lifetimes where applicable).
4. The opt-out type (FR-004) MUST NOT appear in the discovered set.
5. The host MUST provide a **visible** demonstration of the discovery result suitable for docs
   (console output, HTTP endpoint, rendered component, or UI screen) (FR-008, FR-018/019).
6. On mismatch the host MUST fail visibly (FR-010, SC-002).

## Per-kind signalling

| Host kind         | Assertion point     | Failure signal (MUST)                                   | Demonstration surface                                |
| ----------------- | ------------------- | ------------------------------------------------------- | ---------------------------------------------------- |
| Console           | startup             | nonzero exit code                                       | stdout listing of discovered services                |
| Web (minimal API) | startup (fail-fast) | process exits nonzero / health check fails              | demonstration endpoint reporting discovered services |
| Blazor WASM       | on load             | detectable failure state the pipeline run step can read | component/page listing discovered services           |
| MAUI              | on launch           | detectable failure state the pipeline run step can read | screen listing discovered services                   |

7. The ModularPipelines run step MUST treat the host's **exit code** (console/web) or
   **detectable failure state** (blazor/maui) as the pass/fail signal (FR-010).
8. An accidental empty or over-broad scan MUST surface as a failure, never a false "success"
   (Edge Case "Empty/over-broad scans").

## CI runtime execution of UI hosts (Decision D3 — RESOLVED)

Beyond the AOT-publish guardrail, the two UI hosts MUST be **executed at runtime in CI** and have
their pass/fail observed (not just their publish):

| Host kind                | CI runtime harness (MUST)                                                      | How pass/fail is observed                                                                                                     |
| ------------------------ | ------------------------------------------------------------------------------ | ----------------------------------------------------------------------------------------------------------------------------- |
| Blazor WASM              | **headless browser** (e.g. Playwright/Chromium) loading the published WASM app | the assertion result is surfaced to the DOM/console and read by the harness; harness exit code feeds the pipeline             |
| MAUI (`net10.0-android`) | **Android emulator** in CI running the published app                           | the assertion result is surfaced (instrumentation/log/exit signal) and read by the harness; harness result feeds the pipeline |

- The runtime harness step is **distinct** from the AOT-publish step: publish proves it builds
  trim/AOT-clean; the runtime run proves the discovered service set is correct on the real target.
- Accepted cost: emulator/browser boot time and potential flakiness; the harness MUST include
  reasonable startup waits / stabilization and may retry transient infra failures (but MUST NOT
  retry a genuine assertion mismatch).
- Console/Web hosts already execute headlessly; their startup assertion + exit code remain the
  CI signal (no extra harness needed).

## Expected-set source of truth

- The `expectedServiceSet` is co-located with each host (e.g. a static list/const) and is the
  contract a reader can inspect without running the sample (FR-019, SC-007).
- For the test-first red state: temporarily seed a wrong expected set (or remove a library
  reference) and confirm the host fails before the correct set is asserted (Complexity Tracking
  C1).

## API surface used (verified present in `src/Indago`)

- `IIndagoProvider` and `IIndagoProvider.EntryAssembly` (US2 AC3).
- Fluent selectors, e.g. `FromAssemblyOf<>().AddClasses().AsMatchingInterface()` (US1 AC1).
- `ServiceRegistrationAttribute` / generic variants (attribute-based registration).
- `ExcludeFromIndagoAttribute` (opt-out).

## Acceptance mapping

- US1 (AC1–3), US2 (AC1/AC3), US4 (AC1), US5 (AC1), US6 (AC1), FR-007–010, SC-002/007.
