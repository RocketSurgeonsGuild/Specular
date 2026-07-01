---
title: IIndagoProvider
description: The generated provider interface — entry point for all compile-time scanning operations.
tags:
    - api
    - source-generator
---

# IIndagoProvider

`IIndagoProvider` is the primary entry point for all compile-time scanning operations in Indago. At build time, the Roslyn source generator emits a concrete `IndagoProvider` implementation of this interface into your assembly. You access it through the generated static `IndagoProvider.Instance` property — no manual instantiation, no reflection.

## Accessing the Provider

### `IndagoProvider.Instance`

```csharp
public static IIndagoProvider Instance { get; }
```

The source generator emits a concrete `IndagoProvider` class into your assembly and exposes it through the static `Instance` property. This is the entry point for all scanning — access it directly, with no reflection or `Activator.CreateInstance` involved:

```csharp
IIndagoProvider provider = IndagoProvider.Instance;
```

`IndagoProvider` is `internal` to the assembly the generator runs in, so `Instance` is available anywhere in that same assembly. Hold the returned `IIndagoProvider` in a field or local, or call `GetAssemblies`, `GetTypes`, and `Scan` on it inline.

### `GetArgumentExpressionHash(string)`

```csharp
[EditorBrowsable(EditorBrowsableState.Never)]
static string GetArgumentExpressionHash(string argumentExpression);
```

Converts a selector expression string into a stable, compact key. Whitespace (spaces, tabs, newlines) is stripped before hashing so that formatting changes do not invalidate the cache. The resulting hash is an MD5-based Base64 string.

This method is marked `[EditorBrowsable(Never)]` — it is called by generated code, not by application code.

**Why it matters:** The generator keys every generated scan result to the _text_ of the selector lambda at its call site. When the lambda changes (e.g. you add `.AssignableTo<INewService>()`), the hash changes, and the generator produces new output. Call sites that share the same expression text share the same generated result.

## Instance Methods

All three scanning methods share the same hidden caller-info parameters:

| Parameter            | Attribute                    | Purpose                       |
| -------------------- | ---------------------------- | ----------------------------- |
| `lineNumber`         | `[CallerLineNumber]`         | Line number of the call site  |
| `filePath`           | `[CallerFilePath]`           | File path of the call site    |
| `argumentExpression` | `[CallerArgumentExpression]` | Text of the selector argument |

The compiler fills these in automatically. **Do not pass them explicitly.** The generator uses all three together to locate the exact call site and associate it with the generated output.

### `GetAssemblies`

```csharp
IEnumerable<Assembly> GetAssemblies(
    Action<IReflectionAssemblySelector> action,
    [CallerLineNumber] int lineNumber = 0,
    [CallerFilePath] string filePath = "",
    [CallerArgumentExpression(nameof(action))] string argumentExpression = ""
);
```

Returns the set of `Assembly` objects that match the selector. The selector is expressed as an `Action<IReflectionAssemblySelector>`; the generator resolves the full list at build time. At runtime the generated implementation returns a pre-built collection — no reflection occurs.

**Example:**

```csharp
IEnumerable<Assembly> assemblies = provider.GetAssemblies(
    s => s.FromAssemblyOf<MyApp>()
);
```

### `GetTypes`

```csharp
IEnumerable<Type> GetTypes(
    Func<IReflectionTypeSelector, IEnumerable<Type>> selector,
    [CallerLineNumber] int lineNumber = 0,
    [CallerFilePath] string filePath = "",
    [CallerArgumentExpression(nameof(selector))] string argumentExpression = ""
);
```

Returns the set of `Type` objects that match the selector. The selector is a `Func` that receives an `IReflectionTypeSelector` and returns a filtered `IEnumerable<Type>`. The generator evaluates the selector expression at build time.

**Example:**

```csharp
IEnumerable<Type> types = provider.GetTypes(
    s => s.FromAssemblyOf<MyApp>()
          .GetTypes(f => f.AssignableTo<IMyService>())
);
```

### `Scan`

```csharp
IServiceCollection Scan(
    IServiceCollection services,
    Action<IServiceDescriptorAssemblySelector> selector,
    [CallerLineNumber] int lineNumber = 0,
    [CallerFilePath] string filePath = "",
    [CallerArgumentExpression(nameof(selector))] string argumentExpression = ""
);
```

Adds `ServiceDescriptor` entries to the supplied `IServiceCollection` based on the selector. Returns the same `IServiceCollection` so calls can be chained. The full list of registrations is resolved at build time; the generated implementation performs no reflection at runtime.

**Example:**

```csharp
services = provider.Scan(
    services,
    s => s.FromAssemblyOf<MyApp>()
          .AddClasses(c => c.AssignableTo<IMyService>())
          .AsImplementedInterfaces()
          .WithScopedLifetime()
);
```

## Why the selector expression text matters

Because the generator captures the _text_ of each selector argument via `[CallerArgumentExpression]`, the text is the identity of a call site. Two calls with identical expression text (after whitespace normalisation) share a single generated code path. A call site whose selector text changes between builds produces new generated output on the next build. This is why you should avoid dynamic string construction or code-generated selectors — the generator cannot evaluate them.

## See also

- [Type Filters](./type-filters) — the fluent API used inside selectors
- [ServiceRegistrationAttribute](./service-registration) — attribute-driven registration alternative
- [ExcludeFromIndagoAttribute](./exclude-from-indago) — opt an assembly out of scanning
