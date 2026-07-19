---
title: Installation
description: Add Specular to your .NET project in under 5 minutes.
tags:
    - getting-started
---

# Installation

## Prerequisites

- .NET 8.0 or .NET 10.0 SDK
- A project that uses `Microsoft.Extensions.DependencyInjection`

## 1. Add the NuGet Package

```bash
dotnet add package Specular
```

Or add it manually to your `.csproj`:

```xml
<ItemGroup>
    <PackageReference Include="Specular" />
</ItemGroup>
```

> **Central Package Management:** If your solution uses `Directory.Packages.props`, add the version there and reference it without a version attribute in the project file:
>
> ```xml
> <!-- Directory.Packages.props -->
> <PackageVersion Include="Specular" Version="x.y.z" />
> ```

## 2. No Attribute to Apply

There is nothing to wire up by hand. When the generator runs it stamps your assembly with `[assembly: SpecularHashAttribute("<hash>")]` automatically (the hash drives cross-assembly cache invalidation) and emits the `SpecularProvider` class you scan through. You never write this attribute yourself.

> **Libraries that are only scanned** — and never call `ISpecularProvider` themselves — can opt out of emitting their own provider by setting `<SpecularEmitProvider>false</SpecularEmitProvider>` in the project file. Application and entry projects emit a provider by default.

## 3. Build the Project

```bash
dotnet build
```

The Roslyn incremental source generator runs automatically during the build. After a successful build, the generated provider appears under your project's `obj/` folder:

```
obj/
  Debug/
    net8.0/
      generated/
        Specular.Analyzers/
          Specular.Analyzers.SpecularProviderGenerator/
            SpecularProvider.g.cs
```

You can inspect `SpecularProvider.g.cs` to see exactly what the generator produced — a concrete class implementing `ISpecularProvider` whose methods return pre-computed arrays of types.

## 4. Verify the Setup

Add a quick check to confirm the provider resolves:

```csharp
var provider = SpecularProvider.Instance;
Console.WriteLine(provider.GetType().FullName); // should print "SpecularProvider"
```

## No Additional Configuration

That's it. The generator runs automatically on every build. There are no config files and no additional packages to reference. The only optional MSBuild knobs are `<SpecularEmitProvider>` (see step 2) for libraries that should not emit their own provider, and `<SpecularGenerateDiagnostics>false</SpecularGenerateDiagnostics>` to opt out of the [scan report](../architecture/scan-report.md) (on by default). The `SpecularProvider.ctpjson` cross-assembly cache file is written to `obj/` alongside the generated source and is picked up automatically by downstream assemblies.

## Next Steps

- [Quickstart](../quickstart/) — write your first selector and register services
