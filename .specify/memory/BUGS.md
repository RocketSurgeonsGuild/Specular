# Bugs & Prevention

Durable lessons from fixed bugs. Newest entries appended; see `INDEX.md` for routing.

---

### 2026-06-29 - SpecularHashAttribute emitted IL2077 under Native AOT

**Status**
Active

**Why this is durable**
Constitution Principle I (AOT & Trim Safety) is non-negotiable, yet the provider-resolution path in
`src/Specular` was trim-unsafe and shipped latent — every zero-warning AOT publish that touched
`ISpecularProvider.EntryAssembly` would have failed.

**Decision / Finding**
`SpecularHashAttribute` resolved the source-generated provider via
`Activator.CreateInstance(type)` where `type` had no `[DynamicallyAccessedMembers]` annotation, so the
trim analyzer emitted **IL2077**. Fixed by annotating the constructor parameter and the `Type` getter
with `[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)]`.
This is a param/return-attribute change only — it does **not** alter the RS0017 public-API surface.

**Tradeoffs / Prevention**

- Gained: AOT/trim-clean provider resolution; `samples/fixtures/Specular.Samples.NegativeFixture`
  (asserts `IL2072`) is the standing regression detector.
- Reconsider: any `Type` that flows to `Activator`/reflection in `src/Specular` MUST carry the matching
  `[DynamicallyAccessedMembers]`; verify with a Native AOT publish (analysis runs before the link).

---

### 2026-06-29 - Pipeline DocsModule recursion via a delegating mise task

**Status**
Active

**Why this is durable**
A one-line "thin wrapper" mistake produced an exponential process explosion (load average > 280,
hung builds, repeated CPM stripping) that was hard to diagnose because each individual module looked
correct.

**Decision / Finding**
The ModularPipelines `DocsModule` (from `Rocket.Surgery.ModularPipelines.Extensions`) invokes the
mise `docs:build` task. Rewriting `docs:build` to `run = "mise run build"` created infinite recursion:
`build → DocsModule → docs:build → build → …`. Reverted `docs:build` to its real work
(`npm run build --workspace docs`, depends on `docs:api`).

**Tradeoffs / Prevention**

- Gained: a single `mise run build` completes in ~55s again.
- Reconsider: any mise task invoked **by** the pipeline must do real work and never delegate back to
  the umbrella `build` task. When a build "hangs", check for multiple concurrent `dotnet run
build/Build.cs` processes before assuming a module defect.
