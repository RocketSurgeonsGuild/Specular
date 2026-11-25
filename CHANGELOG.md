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
- `Specular.Scanning`: Specular's compile-time assembly and type scanning for your own Roslyn source
  generators. Describe a scan with `ScanRequest.Build(compilation, s => s.FromAssemblies().GetTypes(...))`,
  a symbol-based mirror of Specular's runtime selectors, and run it with `context.Scan(consumerId, request)`
  in an incremental pipeline or `Scanner.Run` directly. Results for referenced assemblies are reused in
  memory and cached across builds in `obj/ctp/<ConsumerId>.ctpjson`, keyed by each assembly's MVID, and
  `ScanCache` offers a typed per-assembly store on the same rules, reachable from the same pipeline step and
  cache file through `context.Scan(consumerId, requests, transform)`. A constructed generic such as
  `IHandler<int>` matches only that closed type; a type a filter names that can't be resolved matches nothing
  and is reported in `ScanResult.UnresolvedTypes`; `ScannedType.FullyQualifiedName` gives a `global::` name to
  emit without resolving symbols. Packing a generator that references the
  package bundles `Specular.Scanning.dll` beside it and adds the MSBuild wiring that feeds the cache back
  (`SPECSCAN001`–`SPECSCAN004` flag packaging mistakes). Needs Roslyn 5.0 or later.
- `Specular.Scanning.Sources`: the same library as source, compiled into your generator as `internal`
  types so no separate assembly is loaded beside it. Needs C# 14 (`SPECSCAN005` otherwise).

### Changed

- `ServiceRegistrationAttribute` (and its generic variants) now require an explicit `ServiceLifetime`
  argument; the previous no-lifetime constructors defaulting to `Singleton` have been removed.
- **Breaking:** the `net8.0` build is gone. Projects still on .NET 8 fall back to the
  `netstandard2.0` build; move to `net10.0` or later to get the AOT-annotated build.
- The `net11.0` build is compiled with runtime async and references the 11.0 `Microsoft.Extensions.*` packages;
  the `net10.0` build stays on 10.0.x.
- **Behaviour change:** several specific assembly selections (`EntryAssembly`, `FromAssemblyOf`,
  `DependenciesFromAssemblyOf`) are now alternatives. They used to all have to match, so
  `FromAssemblyOf<A>().FromAssemblyOf<B>()` found nothing; it now finds the types of both assemblies. Likewise
  `FromAssemblyOf<A>().DependenciesFromAssemblyOf<C>()` now finds `A`'s assembly plus every assembly that
  references `C`'s, and two `DependenciesFromAssemblyOf` calls find the assemblies that reference either one
  rather than both. `FromAssemblies()` remains the starting point these narrow, so
  `FromAssemblies().DependenciesFromAssemblyOf<T>()` still means the assemblies that reference `T`'s, and
  `NotFromAssemblyOf<T>()` always excludes, whatever else selected the assembly.
  **Migrating:** only selectors that chain more than one specific selection change, and they can now return
  more types or registrations than before. Check those call sites: drop the selections you don't want, or
  exclude assemblies with `NotFromAssemblyOf<T>()`. Selectors with a single specific selection, with only
  `FromAssemblies()`, or with only exclusions behave as before.
- Specular's generator is now built on `Specular.Scanning`. `SpecularProvider.ctpjson` has a new format and its
  entries are validated by each referenced assembly's MVID instead of `SpecularHashAttribute` or the
  informational version, so a referenced project rebuilt with changes is always rescanned. Existing cache files
  are ignored once and rebuilt.

### Fixed

- A cached build no longer generates different code from an uncached one when the project and a referenced
  project contain an identical call site on the same file name and line: cached call-site results were keyed by
  file name, line and expression only, so the two collided. Cached results are now also kept apart by target
  (Native AOT or not) and by the request each call site resolved to.
- An assembly marked `[assembly: ExcludeFromSpecular]` stays excluded on cached builds.

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
