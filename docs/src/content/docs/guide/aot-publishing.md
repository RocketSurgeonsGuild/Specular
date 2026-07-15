---
title: AOT Publishing
description: Publish your Specular-powered app with .NET Native AOT — zero trim warnings.
---

# AOT Publishing

## Why AOT Compatibility Matters

.NET Native AOT compiles your application to a standalone native binary at publish time. The IL trimmer removes all code that is not statically reachable from the entry point. Both transformations fail — or produce incorrect binaries — when application code calls trim-unsafe APIs.

The most common source of trim warnings in DI-heavy applications is assembly scanning:

```
ILLink: Trim analysis warning IL2026: 'Assembly.GetTypes()' requires unreferenced code.
```

Standard runtime scanners (including Scrutor) call `Assembly.GetTypes()` at startup. The trimmer cannot know at publish time which types will be returned, so it cannot safely remove any of them — or it warns that it might.

## How Specular Avoids This

Specular's generator resolves every scan at **build time**, before the trimmer or AOT compiler ever runs. The generated `SpecularProvider.g.cs` contains only direct `typeof()` expressions and array literals:

```csharp
// Generated — no reflection APIs
return new Type[] { typeof(MyService), typeof(OtherService), typeof(ThirdService) };
```

`typeof()` is a compile-time constant. The trimmer sees it as a static root, keeps the referenced types, and produces no warnings. There are no calls to `Assembly.GetTypes()`, `Type.GetMethod()`, `Activator.CreateInstance()`, or any other dynamically-dispatched reflection API in the Specular runtime path.

## Supported Target Frameworks

Specular targets `net8.0` and `net10.0` — both of which support Native AOT publishing.

## Publishing Command

```bash
dotnet publish -r linux-x64 --self-contained -p:PublishAot=true
```

Replace `linux-x64` with your target runtime identifier (`win-x64`, `osx-arm64`, etc.).

For a Release build:

```bash
dotnet publish -c Release -r linux-x64 --self-contained -p:PublishAot=true
```

## Verifying Zero Trim Warnings

After publishing, check the output for any trim warnings that mention Specular:

```bash
dotnet publish -r linux-x64 --self-contained -p:PublishAot=true 2>&1 | grep -i specular
```

A clean Specular integration produces no output from that command. If you see trim warnings, they originate from your own code or from other dependencies — not from Specular itself.

You can also enable verbose trim analysis to get a full report:

```xml
<!-- in your .csproj -->
<PropertyGroup>
    <TrimmerRootDescriptor>TrimmerRoots.xml</TrimmerRootDescriptor>
    <SuppressTrimAnalysisWarnings>false</SuppressTrimAnalysisWarnings>
</PropertyGroup>
```

## Trim-Safe Patterns with Specular

### Use `[ServiceRegistration]` instead of convention-based scanning where possible

`[ServiceRegistration]` attributes are read at **compile time** by the generator. The generated code contains explicit `typeof()` references — the trimmer keeps the marked types automatically.

```csharp
// Trim-safe: generator emits typeof(OrderService) directly
[ServiceRegistration<IOrderService>]
public class OrderService : IOrderService { }
```

### Keep selectors stable across builds

Because selectors are hashed by their text, avoid dynamically constructing selector strings or conditionally branching on environment variables inside selectors. The generator evaluates selectors once at build time — the result is fixed until the next build.

### Avoid `FromAssemblies()` in AOT scenarios

`FromAssemblies()` includes all assemblies loaded at the time the generator runs. In an AOT binary, the set of assemblies is fixed at publish time, so this works correctly. However, it can slow incremental builds when the dependency graph is large. Prefer `FromAssemblyOf<T>()` for tighter scoping.

## Next Steps

Explore the Reference section for the full selector API, lifetime configuration, and cross-assembly cache details.
