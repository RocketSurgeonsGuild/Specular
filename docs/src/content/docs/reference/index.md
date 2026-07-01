---
title: Reference
description: API reference for Indago's public surface — the IIndagoProvider interface, opt-in/opt-out attributes, and the type filter selectors.
tags:
    - api
    - di
---

# Reference

This section documents Indago's **public API surface**: the interface your code calls, the attributes that control what gets scanned, and the fluent filters that shape selector expressions. Everything here is a stable contract — the runtime package (`Indago`) is intentionally tiny, and almost all of the work happens in the source generator.

> [!NOTE]
> Indago's public API surface is intentionally minimal — see each page below for the stable contracts.

## Pages :badge[Stable]{variant=success}

- [IIndagoProvider](./iindago-provider/) — the generated entry point, accessed via the static `IndagoProvider.Instance` singleton. Exposes `GetAssemblies`, `GetTypes`, and `Scan`.
- [ServiceRegistrationAttribute](./service-registration/) — declaratively mark a type for dependency-injection registration with a chosen lifetime.
- [ExcludeFromIndagoAttribute](./exclude-from-indago/) — opt a type out of scanning so the generator skips it.
- [Type Filters](./type-filters/) — the fluent selector methods (`AddClasses`, `AsImplementedInterfaces`, namespace and attribute filters, and more) used to build a scan.
