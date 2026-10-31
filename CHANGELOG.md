# Changelog

All notable changes to Specular will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- Initial release of Specular — compile-time assembly/type-scanning for .NET.
- AOT-safe Roslyn incremental source generator that emits `ISpecularProvider` implementations.
- Cross-assembly scan result caching via `SpecularProvider.ctpjson`.
- Fluent selector API compatible with Scrutor conventions.
- Support for .NET 10 and .NET 11 (`netstandard2.0`, `net10.0`, `net11.0`).
- `KeyedServiceRegistrationAttribute` (and its generic `<TService1..8>` variants) — the keyed-service
  counterpart to `ServiceRegistrationAttribute`, producing `ServiceDescriptor.Keyed*` registrations
  resolved via `GetRequiredKeyedService`. `AddSpecularServiceRegistrations` now discovers both
  attribute families in a single scan.

### Changed

- `ServiceRegistrationAttribute` (and its generic variants) now require an explicit `ServiceLifetime`
  argument; the previous no-lifetime constructors defaulting to `Singleton` have been removed.
- **Breaking:** the `net8.0` build is gone. Projects still on .NET 8 fall back to the
  `netstandard2.0` build; move to `net10.0` or later to get the AOT-annotated build.
- The `net11.0` build is compiled with runtime async and references the 11.0 `Microsoft.Extensions.*` packages;
  the `net10.0` build stays on 10.0.x.

### Fixed

- `[ServiceRegistration<TService>(lifetime)]` and the other generic `ServiceRegistrationAttribute<...>` variants
  are now collected by the generator; previously only the non-generic attribute was matched, so the generic
  forms were silently ignored by `AddSpecularServiceRegistrations`.
- A class carrying more than one `[ServiceRegistration]` attribute (generic, non-generic or a mix) is no longer
  reported as a duplicate of itself, which previously raised `RSGD0005` and dropped every registration in the
  scan. The class is registered once, with the lifetime of its first attribute; only a service type claimed by
  two attributes is still a duplicate.
- Open generic classes with a `[ServiceRegistration]` attribute (for example
  `[ServiceRegistration(ServiceLifetime.Scoped, typeof(IRepository<>))]`) are registered as open generic type
  definitions instead of generating a factory that does not compile.
