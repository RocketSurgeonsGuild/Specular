# Contract: Negative-Fixture AOT Publish Step (inverted assertion)

**Applies to**: `Specular.Samples.NegativeFixture` only (FR-020/FR-021, SC-003).

A permanent fixture whose sole purpose is to intentionally emit an **Specular-attributable**
trim/AOT warning during AOT publish, validated by an **inverted** assertion so the regression
detector is itself guarded against silently breaking.

## Inputs

| Input            | Value                                                                                           |
| ---------------- | ----------------------------------------------------------------------------------------------- |
| Fixture project  | `Specular.Samples.NegativeFixture.csproj`                                                         |
| TFM × RID        | `net10.0` × current-OS RID (same target as the demo hosts, FR-021)                              |
| Warning policy   | **NOT** warnings-as-errors — the publish must be allowed to warn so the warning can be detected |
| Expected warning | `IL2072` — pinned below (D2 RESOLVED)                                                           |

## Required behavior (MUST)

1. The fixture MUST be **excluded** from the normal AOT-publish steps and MUST NOT be counted as
   one of the four demonstration hosts (FR-020).
2. The step MUST be a discrete, named pipeline step (parallel to the demo-host steps).
3. The step MUST run the AOT publish and inspect the result for the expected
   Specular-attributable trim/AOT warning.
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
| Expected Specular-attributable trim/AOT warning present | `PASS`                           |
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

## Pinned warning signature (D2 — RESOLVED)

> Pinned by T021/T022, 2026-06-29, SDK `10.0.301`, `Microsoft.DotNet.ILCompiler` `10.0.9`.

**Triggering construct.** `samples/fixtures/Specular.Samples.NegativeFixture/Program.cs` feeds the
results of Specular's `ISpecularProvider.GetTypes(...)` — whose element `Type` carries **no**
`[DynamicallyAccessedMembers]` annotation by design — into `Activator.CreateInstance(Type)`:

```csharp
foreach (var type in provider.GetTypes(s => s.EntryAssembly().GetTypes(f => f.AssignableTo<Marker>())))
{
    _ = Activator.CreateInstance(type);
}
```

**Emitted warning (greppable signature).**

```
warning IL2072: 'type' argument does not satisfy 'DynamicallyAccessedMemberTypes.PublicParameterlessConstructor' in call to 'System.Activator.CreateInstance(Type)'
```

Project-scoped match: the warning line ends with
`[…/Specular.Samples.NegativeFixture.csproj]`.

**Attribution note.** Because the value flows through `foreach`, the trim analyzer names the
immediate data-flow source as `IEnumerator<Type>.Current` rather than `ISpecularProvider.GetTypes`
directly; the warning nonetheless arises solely from consuming Specular's un-annotated `GetTypes`
return. If Specular ever annotates `GetTypes` with `[DynamicallyAccessedMembers]`, this warning
disappears and the **inverted assertion FAILS** — which is precisely the regression signal D2 exists
to provide. A future SDK that renames/renumbers the warning is a deliberate, reviewed edit here.

**Inverted-assertion module behavior.** `NegativeFixtureAotPublishModule` publishes this project
(`net10.0 × current RID`, NOT warnings-as-errors, `ThrowOnNonZeroExitCode=false`), scans the captured
publish output for `IL2072` scoped to `Specular.Samples.NegativeFixture.csproj`:

- present ⇒ `PASS`; clean/absent ⇒ `FAIL` (fails the build).
- The trim/AOT analysis (where `IL2072` is emitted) runs **before** the native link, so the inverted
  assertion is observable locally even when the local Native AOT link is unavailable; it reports
  `SKIP` only when the trim toolchain itself cannot run.

## Acceptance mapping

- US3 (negative-fixture clause), FR-020/FR-021, SC-003.
