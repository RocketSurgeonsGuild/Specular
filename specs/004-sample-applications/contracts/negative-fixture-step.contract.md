# Contract: Negative-Fixture AOT Publish Step (inverted assertion)

**Applies to**: `Indago.Samples.NegativeFixture` only (FR-020/FR-021, SC-003).

A permanent fixture whose sole purpose is to intentionally emit an **Indago-attributable**
trim/AOT warning during AOT publish, validated by an **inverted** assertion so the regression
detector is itself guarded against silently breaking.

## Inputs

| Input            | Value                                                                                                |
| ---------------- | ---------------------------------------------------------------------------------------------------- |
| Fixture project  | `Indago.Samples.NegativeFixture.csproj`                                                              |
| TFM × RID        | `net10.0` × current-OS RID (same target as the demo hosts, FR-021)                                   |
| Warning policy   | **NOT** warnings-as-errors — the publish must be allowed to warn so the warning can be detected      |
| Expected warning | A stable Indago-attributable trim/AOT warning signature (pinned during implementation — research D2) |

## Required behavior (MUST)

1. The fixture MUST be **excluded** from the normal AOT-publish steps and MUST NOT be counted as
   one of the four demonstration hosts (FR-020).
2. The step MUST be a discrete, named pipeline step (parallel to the demo-host steps).
3. The step MUST run the AOT publish and inspect the result for the expected
   Indago-attributable trim/AOT warning.
4. **Inverted assertion**:
    - Expected warning **present** (publish warns/fails as expected) ⇒ step `PASS`.
    - Publish **clean** OR expected warning **absent** ⇒ step `FAIL` and the overall build fails
      (the detector can no longer prove failures are caught — Edge Case "Negative-fixture
      regression guard").
5. The fixture is the **only** project exempt from the zero-warning policy (FR-012/015) and is in
   fact **required to warn** (FR-021, SC-003).

## Outcome semantics

| Condition                                             | Outcome                          |
| ----------------------------------------------------- | -------------------------------- |
| Expected Indago-attributable trim/AOT warning present | `PASS`                           |
| Publish clean / expected warning missing              | `FAIL` (fails the build)         |
| Required toolchain absent                             | `SKIP` (+ reason) — never `PASS` |

## Implementation guidance (non-normative)

- Capture the publish command result + output; match the expected warning code/text. Prefer a
  specific, stable warning identifier over fragile substring matching; tolerate unrelated SDK
  noise.
- Document the exact triggering construct and matched signature in this contract once pinned
  (research D2), so a future SDK change that alters the warning is a deliberate, reviewed update.
- Keep the fixture minimal and self-contained; it must reliably re-trigger across SDK updates or
  fail loudly when it stops (that is the point).

## Acceptance mapping

- US3 (negative-fixture clause), FR-020/FR-021, SC-003.
