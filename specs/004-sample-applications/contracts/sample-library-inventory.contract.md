# Contract: Sample Library Inventory

**Applies to**: the three sample libraries (FR-001–FR-005, SC-001).

Defines the **collective coverage guarantees** the three libraries must satisfy. Exact service
names are an implementation detail; the coverage matrix below is the contract.

## Structural requirements (MUST)

1. Exactly **three** general-purpose libraries under `samples/libraries/` (FR-001).
2. Each library exposes **3–6** discoverable service/implementation pairs (FR-002).
3. Each library multi-targets `net8.0;net10.0` (consistent with the repo). AOT publish of hosts
   uses `net10.0` only; libraries are not AOT-published directly (FR-013).
4. Libraries are independently reusable by every host with **no per-host modification** (FR-005).
5. Libraries are excluded from the shipped/packaged surface (`IsPackable=false`) and MUST NOT
   change Specular's public API (Principle III; RS0017 stays green).

## Collective coverage matrix (MUST, across the three libraries)

| Capability                      | Requirement                                                             | Where (planned)                               |
| ------------------------------- | ----------------------------------------------------------------------- | --------------------------------------------- |
| Interface-matching registration | ≥1 library uses `FromAssemblyOf<>().AddClasses().AsMatchingInterface()` | Catalog                                       |
| Attribute-based registration    | ≥1 library uses `ServiceRegistrationAttribute` (incl. generic variants) | Notifications                                 |
| Distinct lifetimes              | ≥2 of {Singleton, Scoped, Transient} demonstrated                       | Notifications (Scoped+Transient), Diagnostics |
| Opt-out type                    | ≥1 type marked `[ExcludeFromSpecular]` and proven excluded                | Diagnostics                                   |
| ≥3 styles/lifetimes total       | satisfied by the sum of the above                                       | — (SC-001)                                    |

## Per-library inventory (planned — finalize during implementation)

### `Specular.Samples.Catalog` (interface-matching)

- 3–6 `IXxxService` → `XxxService` pairs, default lifetime, registered by interface matching.

### `Specular.Samples.Notifications` (attribute-based, varied lifetimes)

- 3–6 services annotated with `[ServiceRegistration(ServiceLifetime.Scoped)]` /
  `[ServiceRegistration<TService>(ServiceLifetime.Transient)]`, demonstrating Scoped + Transient.

### `Specular.Samples.Diagnostics` (mixed + opt-out)

- 3–6 services mixing interface-matching and attribute styles; exactly one type carries
  `[ExcludeFromSpecular]` and MUST NOT be discovered by any host.

## Domain note

Per the spec Assumptions, the three domains are intentionally distinct and lightweight, chosen
only to make discovered services easy to distinguish in demonstrations; the specific domains carry
no functional requirement beyond illustrating scanning patterns.

## Acceptance mapping

- US1 (AC1–3), FR-001–005, SC-001.
