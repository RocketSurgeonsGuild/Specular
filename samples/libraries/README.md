# Sample Libraries — Discovery Coverage Matrix

Three reusable libraries demonstrate the registration styles and lifetimes Indago discovers at
compile time. Hosts consume them **unmodified**; the expected discovered set below is what every
host asserts in-process (US2/US4/US5/US6). This file is the inspectable inventory required by
FR-019 / SC-001 / SC-007 — you can verify coverage without running anything.

## Collective coverage (contract `sample-library-inventory`)

| Capability                         | Satisfied by                                                         |
| ---------------------------------- | -------------------------------------------------------------------- |
| Interface-matching registration    | Catalog (4 pairs), Diagnostics (2 pairs)                             |
| Attribute-based registration       | Notifications (4 services, `[ServiceRegistration]`)                  |
| Distinct lifetimes (≥2)            | Singleton (Catalog), Transient (Notifications), Scoped (Diagnostics) |
| Opt-out type (≥1, proven excluded) | Diagnostics `ExperimentalProbe`                                      |

## `Indago.Samples.Catalog` — interface-matching, Singleton

Host selector:
`FromAssemblyOf<IProductService>().AddClasses().AsMatchingInterface().WithSingletonLifetime()`

| Service             | Implementation     | Lifetime  |
| ------------------- | ------------------ | --------- |
| `IProductService`   | `ProductService`   | Singleton |
| `ICategoryService`  | `CategoryService`  | Singleton |
| `IInventoryService` | `InventoryService` | Singleton |
| `IPricingService`   | `PricingService`   | Singleton |

## `Indago.Samples.Notifications` — attribute-based, Transient (AsSelf)

Each service carries `[ServiceRegistration(ServiceLifetime.Transient)]`. Host selector filters on the
attribute and registers the concrete type as itself:
`FromAssemblyOf<NotificationDispatcher>().AddClasses(f => f.WithAttribute<ServiceRegistrationAttribute>()).AsSelf().WithTransientLifetime()`

| Service (AsSelf)         | Lifetime  | Attribute                          |
| ------------------------ | --------- | ---------------------------------- |
| `EmailNotifier`          | Transient | `[ServiceRegistration(Transient)]` |
| `SmsNotifier`            | Transient | `[ServiceRegistration(Transient)]` |
| `PushNotifier`           | Transient | `[ServiceRegistration(Transient)]` |
| `NotificationDispatcher` | Transient | `[ServiceRegistration(Transient)]` |

## `Indago.Samples.Diagnostics` — interface-matching + opt-out, Scoped

Interface-matching selector filters out the opt-out marker:
`FromAssemblyOf<IHealthCheck>().AddClasses(f => f.WithoutAttribute<ExcludeFromSampleScanAttribute>()).AsMatchingInterface().WithScopedLifetime()`

| Service              | Implementation      | Lifetime       | Style                               |
| -------------------- | ------------------- | -------------- | ----------------------------------- |
| `IHealthCheck`       | `HealthCheck`       | Scoped         | interface-matching                  |
| `IMetricsCollector`  | `MetricsCollector`  | Scoped         | interface-matching                  |
| `IExperimentalProbe` | `ExperimentalProbe` | — **EXCLUDED** | opt-out (`[ExcludeFromSampleScan]`) |

> **Opt-out note**: Indago's built-in `[ExcludeFromIndago]` targets **assemblies**, not types.
> Per-type opt-out is expressed at the host call site by filtering a marker attribute. The host
> MUST NOT register `IExperimentalProbe` / `ExperimentalProbe`.

## Expected total registered set (per host, opt-out absent)

Catalog 4 (Singleton) + Notifications 4 (Transient) + Diagnostics 2 (Scoped) = **10 services**;
`ExperimentalProbe` absent. All three lifetimes (Singleton, Scoped, Transient) are demonstrated.
