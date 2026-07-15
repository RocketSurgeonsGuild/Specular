# Decisions

Durable technical/architectural choices. Newest entries appended; see `INDEX.md` for routing.

---

### 2026-06-29 - Native AOT zero-warning enforcement is csproj-scoped; modules skip-with-reason locally

**Status**
Active

**Why this is durable**
The sample-host AOT guardrail is a core deliverable, and the scoping rule is non-obvious — getting it
wrong breaks the whole solution build or silently weakens the guardrail.

**Decision / Finding**

- The zero-warning AOT property set (`PublishAot=true`, `TrimmerSingleWarn=false`,
  `TreatWarningsAsErrors=true`, `InvariantGlobalization=true`) lives **in each host `.csproj`**, never
  passed as global `-p:` on the command line. Global `-p:` propagates into the `netstandard2.0`
  dependency closure (`src/Specular`, analyzers) and fails with `NETSDK1207` plus unrelated
  warning-as-error promotions. Pinned in `contracts/aot-publish-step.contract.md`.
- AOT-publish pipeline modules **publish for real in CI (Linux)** and report **skip-with-reason**
  locally (never `pass`): osx-arm64 Native AOT linking needs Homebrew `openssl@3`/`brotli` on the
  linker path. Set `SPECULAR_AOT_LOCAL=1` to opt in locally.
- The negative fixture's inverted assertion runs everywhere because the trim/AOT analysis (where the
  warning is emitted) happens **before** the native link.

**Tradeoffs / Prevention**

- Gained: solution builds green with no MAUI/wasm-tools workloads (D1 invariant); guardrail is real
  in CI.
- Reconsider: `xmldocmd` reflection-loads the assembly, so `docs/scripts/generate-api.sh` builds with
  `CopyLocalLockFileAssemblies=true` to populate NuGet dep DLLs next to the bin assembly (libraries
  don't copy them by default); it targets the `net8.0` Release bin and treats `xmldocmd` as mandatory
  (no XML-only fallback).
