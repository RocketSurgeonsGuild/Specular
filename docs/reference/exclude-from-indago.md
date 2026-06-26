---
title: ExcludeFromIndagoAttribute
description: Opt an assembly out of all Indago scan results at compile time.
---

# ExcludeFromIndagoAttribute

```csharp
[AttributeUsage(AttributeTargets.Assembly)]
public class ExcludeFromIndagoAttribute : Attribute
```

`ExcludeFromIndagoAttribute` is an **assembly-level** attribute that tells the Indago generator to exclude the entire assembly from compiled type provider resolution. An excluded assembly will not contribute types to any `GetAssemblies`, `GetTypes`, or `Scan` call in other assemblies.

> **Note:** The excluded assembly itself still has access to the compiled type provider — it can call `IIndagoProvider` methods normally. Only its own types are hidden from resolution in _other_ assemblies that scan it.

## Usage

Place the attribute in any file within the assembly you want to exclude, using the `[assembly:]` target:

```csharp
[assembly: ExcludeFromIndago]
```

A common convention is to put assembly-level attributes in an `AssemblyInfo.cs` file or directly in the project's top-level `Program.cs`.

## When to use it

- **Test assemblies** — prevent test fixture types, fakes, and stubs from appearing in production DI scans when the test project is referenced.
- **Internal implementation assemblies** — a helper assembly that should never be scanned by consuming projects.
- **Third-party or generated assemblies** — assemblies you reference but do not control, where you want to prevent their types from appearing in a broad `FromAssemblies()` scan.

## Compile-time exclusion

This is a build-time decision. The Roslyn generator reads `[ExcludeFromIndago]` from the assembly metadata during the build. The exclusion is baked into the generated provider — there is no runtime check and no performance cost.

## Interaction with other attributes

`[ExcludeFromIndago]` applies at the assembly level and takes precedence over everything else. Types in the excluded assembly will not appear in scan results even if those types carry `[ServiceRegistration]` or are otherwise matched by a fluent selector.

## See also

- [IIndagoProvider](./iindago-provider) — the entry point for scanning operations
- [Type Filters](./type-filters) — per-selector filtering, including `NotFromAssemblyOf<T>()`
- [ServiceRegistrationAttribute](./service-registration) — attribute-driven type registration
