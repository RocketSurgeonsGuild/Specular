---
title: Installation
description: Add Indago to your .NET project in under 5 minutes.
tags:
    - getting-started
---

# Installation

## Prerequisites

- .NET 8.0 or .NET 10.0 SDK
- A project that uses `Microsoft.Extensions.DependencyInjection`

## 1. Add the NuGet Package

```bash
dotnet add package Indago
```

Or add it manually to your `.csproj`:

```xml
<ItemGroup>
    <PackageReference Include="Indago" />
</ItemGroup>
```

> **Central Package Management:** If your solution uses `Directory.Packages.props`, add the version there and reference it without a version attribute in the project file:
>
> ```xml
> <!-- Directory.Packages.props -->
> <PackageVersion Include="Indago" Version="x.y.z" />
> ```

## 2. Apply the Assembly Attribute

The generator needs to know which assembly is the entry point for scanning. Add `[assembly: IndagoHashAttribute]` to your project. The conventional location is the top of `Program.cs` or a dedicated `AssemblyInfo.cs` file.

```csharp
using Indago.Abstractions;

[assembly: IndagoHashAttribute]
```

The attribute lives in the `Indago.Abstractions` namespace.

> **One attribute per entry assembly.** If you have multiple projects in a solution, apply the attribute only to the projects that call `IIndagoProvider` methods directly. Library projects that are _scanned_ do not need the attribute.

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
        Indago.Analyzers/
          Indago.Analyzers.IndagoProviderGenerator/
            IndagoProvider.g.cs
```

You can inspect `IndagoProvider.g.cs` to see exactly what the generator produced — a concrete class implementing `IIndagoProvider` whose methods return pre-computed arrays of types.

## 4. Verify the Setup

Add a quick check to confirm the provider resolves:

```csharp
using Indago;

var provider = IIndagoProvider.EntryAssembly;
Console.WriteLine(provider.GetType().FullName); // should print the generated type name
```

## No Additional Configuration

That's it. The generator runs automatically on every build. There are no config files, no MSBuild properties to set, and no additional packages to reference. The `IndagoProvider.ctpjson` cross-assembly cache file is written to `obj/` alongside the generated source and is picked up automatically by downstream assemblies.

## Next Steps

- [Quickstart](../quickstart/) — write your first selector and register services
