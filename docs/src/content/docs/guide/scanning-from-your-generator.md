---
title: Scanning from your own generator
description: Use Specular.Scanning to give your own Roslyn source generator Specular's compile-time assembly and type scanning, with results cached across builds.
badge:
    text: New
    variant: tip
tags:
    - source-generator
---

# Scanning from your own generator

Specular's generator scans assemblies and types at build time. `Specular.Scanning` makes that scanner available to your own
Roslyn incremental source generator. You describe a scan with a symbol-based copy of Specular's runtime selectors
(`FromAssemblies().GetTypes(f => f.AssignableTo(...))`), run it inside your pipeline, and get back an equatable result. Results
for referenced assemblies are cached: in memory while the IDE edits the project, and on disk across builds.

This page is for **source generator authors**. If you only want to register services or find types in an application, use
`Specular` itself; see the [Quickstart](../quickstart/).

## Choosing a package

The library ships in two forms with the same code and API, in the `Specular.Scanning` namespace.

| Package                     | What your generator gets                                                    | Choose it when                                                                                                                        |
| --------------------------- | --------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------- |
| `Specular.Scanning`         | A compile reference to `Specular.Scanning.dll`, which is packed beside it.  | You want the simplest setup. This is the default.                                                                                     |
| `Specular.Scanning.Sources` | The library's `.cs` files compiled into your generator as `internal` types. | You don't want a second assembly loaded beside your generator, or you need to be sure no other generator's copy can clash with yours. |

With the DLL package, every generator that uses the library ships its own `Specular.Scanning.dll`. Two generators built
against different versions then rely on the compiler loading each package's analyzers in a separate load context, which
compilers running on .NET do. The Sources package avoids the question: there is no separate assembly to load. Specular's own
generator uses the library this way.

Requirements for both packages:

- The generator targets `netstandard2.0` and builds against `Microsoft.CodeAnalysis.CSharp` **5.0 or later**. The DLL
  package holds a build of the library for Roslyn 5.0, 5.3, 5.6, 5.9 and 5.12.
- **Reference the package with `PrivateAssets="all"`.** It's a build-time dependency of your generator, not something the
  projects that use your generator should reference. Without it, the DLL package warns with `SPECSCAN001`.

### `Specular.Scanning` (DLL)

```xml
<ItemGroup>
    <PackageReference Include="Microsoft.CodeAnalysis.CSharp" Version="5.0.0" PrivateAssets="all" />
    <PackageReference Include="Specular.Scanning" Version="x.y.z" PrivateAssets="all" />
</ItemGroup>
```

The package depends on `System.Text.Json` and `System.Collections.Immutable` 9.0.21 or later, so you don't reference them
yourself; an older version pinned in your project fails restore with `NU1605`. When you pack the generator, the package adds
`Specular.Scanning.dll` and its `System.Text.Json.dll` to every `analyzers/` folder your generator is packed into. If you
reference a newer `System.Text.Json` than the bundled one, pack that `System.Text.Json.dll` beside your generator yourself; a
file your project packs takes precedence. See [Packaging](#packaging-and-msbuild).

### `Specular.Scanning.Sources` (source-only)

```xml
<ItemGroup>
    <PackageReference Include="Microsoft.CodeAnalysis.CSharp" Version="5.0.0" PrivateAssets="all" />
    <PackageReference Include="Specular.Scanning.Sources" Version="x.y.z" PrivateAssets="all" />
    <!-- The sources use System.Text.Json and immutable collections; reference them yourself. -->
    <PackageReference Include="System.Text.Json" Version="9.0.0" PrivateAssets="all" />
    <PackageReference Include="System.Collections.Immutable" Version="9.0.0" PrivateAssets="all" />
</ItemGroup>
```

- The sources need **C# 14**. A `netstandard2.0` project defaults to C# 7.3, so the package sets `LangVersion` to 14 when your
  project doesn't set it. If your project sets a version older than 14, the build fails with `SPECSCAN005` and tells you to
  raise it. The package never overrides a `LangVersion` you set.
- The package depends on [Polyfill](https://github.com/SimonCropp/Polyfill), which is also source-only, and turns on its
  argument-exception back-ports (`PolyArgumentExceptions`) unless you set that property yourself.
- The types are compiled as `internal` (the package defines `SPECULAR_SCANNING_SOURCES`), so they never become part of your
  generator's API.
- Your generator now depends on `System.Text.Json` at run time. Ship `System.Text.Json.dll` beside your generator, as you
  would any generator dependency.

## A complete generator

The generator below finds every concrete class that implements `Plugins.IPlugin`, in the project and in everything it
references, and generates a `PluginRegistry` that lists them.

```csharp
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Specular.Scanning;

[Generator(LanguageNames.CSharp)]
public sealed class PluginRegistryGenerator : IIncrementalGenerator
{
    // Unique to this generator. It names the cache file: obj/<configuration>/<tfm>/ctp/PluginRegistry.ctpjson.
    private const string ConsumerId = "PluginRegistry";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // 1. Describe the scan. This runs on every compilation change, but a ScanRequest is equatable,
        //    so nothing downstream reruns while the request stays the same.
        var request = context.CompilationProvider.Select(static (compilation, _) =>
            compilation.GetTypeByMetadataName("Plugins.IPlugin") is { } plugin
                ? ScanRequest.Build(compilation, s => s
                    .FromAssemblies()
                    .GetTypes(f => f
                        .AssignableTo(plugin)
                        .KindOf(TypeKind.Class)
                        .NotInfoOf(TypeInfoFilter.Abstract, TypeInfoFilter.GenericType)))
                // An empty selection matches nothing.
                : ScanRequest.Build(compilation, static _ => { }));

        // 2. Run it. A referenced assembly is walked once, then served from memory or from the cache file
        //    until it changes.
        IncrementalValueProvider<ScanResult> result = context.Scan(ConsumerId, request);

        // 3. Keep only what the output needs, as an equatable value.
        var plugins = result.Select(static (scan, _) => new EquatableArray<string>(
            scan.Types
                .Where(static type => type.IsAccessible)
                .Select(static type => type.FullyQualifiedName)
                .ToImmutableArray()));

        context.RegisterSourceOutput(plugins, static (output, names) =>
        {
            var source = new StringBuilder()
                .AppendLine("namespace Plugins;")
                .AppendLine()
                .AppendLine("public static class PluginRegistry")
                .AppendLine("{")
                .AppendLine("    public static global::System.Type[] All { get; } = new global::System.Type[]")
                .AppendLine("    {");
            foreach (var name in names)
            {
                source.Append("        typeof(").Append(name).AppendLine("),");
            }

            source.AppendLine("    };").AppendLine("}");
            output.AddSource("PluginRegistry.g.cs", source.ToString());
        });
    }
}
```

Points to notice:

- `ScanRequest.Build(compilation, select)` runs your selection against a compilation and returns a `ScanRequest`: a
  read-only record that names assemblies and types by metadata name, not by symbol. It's the value that flows through your
  pipeline and keys the cache.
- `context.Scan(consumerId, request)` is an extension method on `IncrementalGeneratorInitializationContext`. It returns
  an `IncrementalValueProvider<ScanResult>`. It also adds the steps that read and write the cache file.
- `ScanResult` is equatable, so you can `Select` from it directly. The step above reduces it to a list of type names, so the
  output step reruns only when that list changes.
- The type-name trick only works for non-generic types, which the filter guarantees. For anything more involved, resolve
  the result back to symbols in the output step: see [Results](#results).

### Several requests

If your generator needs more than one scan, pass all of them to **one** `Scan` call, as an
`IncrementalValuesProvider<ScanRequest>`. You get one `ScanResult` per request, in request order, and a request listed twice
is scanned once.

```csharp
var requests = context.CompilationProvider.SelectMany(static (compilation, _) => ImmutableArray.Create(
    ScanRequest.Build(compilation, s => s.FromAssemblies().GetTypes(f => f.EndsWith("Handler"))),
    ScanRequest.Build(compilation, s => s.DependenciesFromAssembly("MyFramework"))));

IncrementalValuesProvider<ScanResult> results = context.Scan(ConsumerId, requests);
```

When you know the scans up front, pass them as a tuple instead, and read each result by name. There are overloads for 2 to 8
requests; the results come back in the same positions, and the tuple compares by value, so the pipeline caches it:

```csharp
var requests = context.CompilationProvider.Select(static (compilation, _) => (
    ScanRequest.Build(compilation, s => s.FromAssemblies().GetTypes(f => f.EndsWith("Handler"))),
    ScanRequest.Build(compilation, s => s.DependenciesFromAssembly("MyFramework"))));

IncrementalValueProvider<(ScanResult, ScanResult)> results = context.Scan(ConsumerId, requests);

context.RegisterSourceOutput(results, static (output, scans) =>
{
    var (handlers, dependents) = scans;
    // ...
});
```

Tuple element names are compile-time only, so they don't carry from the requests to the results: name the results where you
read them, as above.

If the requests come from different parts of your pipeline, pass the providers separately instead (2 to 8 of them). They're
combined into one scan, and the results come back in parameter order:

```csharp
var handlers = context.CompilationProvider.Select(static (compilation, _) => ScanRequest.Build(compilation, BuildHandlerScan));
var plugins = context.CompilationProvider.Select(static (compilation, _) => ScanRequest.Build(compilation, BuildPluginScan));

IncrementalValueProvider<(ScanResult, ScanResult)> results = context.Scan(ConsumerId, handlers, plugins);
```

Don't call `Scan` twice with the same consumer id. Each call saves only the entries its own requests used, so two calls that
share a file keep overwriting each other's entries.

## The selector API

The selectors mirror Specular's runtime selectors (`IReflectionAssemblySelector`, `IReflectionTypeSelector` and
`ITypeFilter`) as `IAssemblySymbolSelector`, `ITypeSymbolSelector` and `ITypeSymbolFilter`. If you know one, you know the
other. The differences:

- **`Type` becomes `ITypeSymbol`.** Get symbols from the compilation you were given, for example with
  `compilation.GetTypeByMetadataName("MyLib.IHandler")`.
- **There are no generic `<T>` overloads.** `FromAssemblyOf<T>()` becomes `FromAssemblyOf(symbol)`. A generator can't name the
  user's types as type arguments.
- **String overloads take metadata names.** Assemblies are named by `IAssemblySymbol.MetadataName` (`"MyLib"`). Types are
  named by their full metadata name: namespaces joined with `.`, nested types with `+`, and generic types with their arity
  (``"MyLib.IHandler`1"``, `"MyLib.Outer+Inner"`).
- **Assemblies can be named directly.** `FromAssembly`, `NotFromAssembly` and `DependenciesFromAssembly` take an
  `IAssemblySymbol` or an assembly name.
- **`GetTypes` returns nothing.** It makes the request a types scan. Without it the request is an assemblies scan.
- **`KindOf` takes Roslyn's `TypeKind`.** Its `Array`, `Class`, `Delegate`, `Enum`, `Interface` and `Struct` values are the
  same as Specular's `TypeKindFilter`. `InfoOf` takes `Specular.Scanning.TypeInfoFilter`, which has the same members and
  values as Specular's.
- **`Where(CustomTypeFilter)`** adds a filter of your own. See [Custom filters](#custom-filters).

### Assembly selection

| Specular (runtime)                                   | Specular.Scanning                                                                                  |
| ---------------------------------------------------- | -------------------------------------------------------------------------------------------------- |
| `EntryAssembly()`                                    | `EntryAssembly()`                                                                                  |
| `FromAssemblies()`                                   | `FromAssemblies()`                                                                                 |
| `FromAssemblyOf<T>()`, `FromAssemblyOf(Type)`        | `FromAssemblyOf(ITypeSymbol)`, `FromAssembly(IAssemblySymbol)`, `FromAssembly(string)`             |
| `NotFromAssemblyOf<T>()`, `NotFromAssemblyOf(Type)`  | `NotFromAssemblyOf(ITypeSymbol)`, `NotFromAssembly(IAssemblySymbol)`, `NotFromAssembly(string)`    |
| `DependenciesFromAssemblyOf<T>()`, `(Type)`          | `DependenciesFromAssemblyOf(ITypeSymbol)`, `DependenciesFromAssembly(IAssemblySymbol)`, `(string)` |
| `IncludeSystemAssemblies()`                          | `IncludeSystemAssemblies()`                                                                        |
| `GetTypes()`, `GetTypes(bool publicOnly)`            | `GetTypes()`, `GetTypes(bool publicOnly)`                                                          |
| `GetTypes(Action<ITypeFilter>)`, `(bool, Action<…>)` | `GetTypes(Action<ITypeSymbolFilter>)`, `GetTypes(bool, Action<ITypeSymbolFilter>)`                 |

### Type filters

Every filter call adds a clause, and a type must match all of them.

| Specular (runtime)                                             | Specular.Scanning                                                      |
| -------------------------------------------------------------- | ---------------------------------------------------------------------- |
| `AssignableTo<T>()`, `AssignableTo(Type)`                      | `AssignableTo(ITypeSymbol)`, `AssignableTo(string metadataName)`       |
| `AssignableToAny(Type, params Type[])`                         | `AssignableToAny(ITypeSymbol, params ITypeSymbol[])`                   |
| `NotAssignableTo<T>()`, `NotAssignableTo(Type)`                | `NotAssignableTo(ITypeSymbol)`, `NotAssignableTo(string metadataName)` |
| `NotAssignableToAny(Type, params Type[])`                      | `NotAssignableToAny(ITypeSymbol, params ITypeSymbol[])`                |
| `StartsWith`, `EndsWith`, `Contains` and their `Not…` forms    | The same, with the same `(string, params string[])` signatures         |
| `InExactNamespaceOf<T>()`, `(Type, params Type[])`             | `InExactNamespaceOf(ITypeSymbol, params ITypeSymbol[])`                |
| `InNamespaceOf<T>()`, `NotInNamespaceOf<T>()` and `Type` forms | `InNamespaceOf(…)`, `NotInNamespaceOf(…)` with `ITypeSymbol` arguments |
| `InExactNamespaces`, `InNamespaces`, `NotInNamespaces`         | The same, with the same `(string, params string[])` signatures         |
| `WithAttribute<T>()`, `(Type)`, `(string)`                     | `WithAttribute(ITypeSymbol)`, `WithAttribute(string)`                  |
| `WithAnyAttribute(Type, …)`, `(string, …)`                     | `WithAnyAttribute(ITypeSymbol, …)`, `WithAnyAttribute(string, …)`      |
| `WithoutAttribute<T>()`, `(Type)`, `(string)`                  | `WithoutAttribute(ITypeSymbol)`, `WithoutAttribute(string)`            |
| `KindOf(TypeKindFilter, …)`, `NotKindOf(…)`                    | `KindOf(TypeKind, …)`, `NotKindOf(TypeKind, …)`                        |
| `InfoOf(TypeInfoFilter, …)`, `NotInfoOf(…)`                    | `InfoOf(TypeInfoFilter, …)`, `NotInfoOf(…)`                            |
| none                                                           | `Where(CustomTypeFilter)`                                              |

The rules match Specular's own generator:

- **Name filters** compare the type's simple name (`type.Name`, without namespace or generic arity), case-sensitively.
- **`InNamespaces` and `NotInNamespaces` compare by prefix**, so `InNamespaces("MyApp.Foo")` also matches `MyApp.FooBar`.
  Use `InExactNamespaces` for an exact match.
- **Generics.** Pass the generic definition (``GetTypeByMetadataName("MyLib.IHandler`1")``, or the string ``"MyLib.IHandler`1"``)
  to match every closed form, through the type itself, its base types or its interfaces. Pass a constructed type, such as
  `IHandler<int>` built with `handler.Construct(compilation.GetSpecialType(SpecialType.System_Int32))`, to match only that
  closed type; variance applies as in C#, so `IVariant<object>` matches a type implementing `IVariant<string>`. Type
  arguments must be named types (nested, constructed, tuples and `int?` are fine) or arrays of them, so `IHandler<int[]>` and
  `IHandler<string[,]>` work too; pointers, type parameters and `dynamic` as arguments throw `ArgumentException`. The string
  overloads can't express type arguments.
- **`object` is never a match** for `AssignableTo`, as in Specular.
- **Attributes** are read from the type itself, not inherited. `WithAttribute(symbol)` needs the exact attribute class.
  `WithAnyAttribute(symbol, …)` compares name and namespace, so a generic attribute definition matches every closed use of it.
- **A type that can't be found matches nothing.** An include clause whose types don't resolve matches no type, so
  `AssignableTo("MyLib.IHandlr")` with a typo returns nothing rather than everything; an exclude clause ignores a type it
  can't find. Every reference that didn't resolve is listed in `ScanResult.UnresolvedTypes`, so your generator can report a
  diagnostic. A type named by string is looked up with `Compilation.GetTypeByMetadataName`, which also finds nothing when two
  referenced assemblies define the same name; a symbol overload records the type's assembly, so it doesn't have that problem.
  Prefer symbol overloads, and check for `null` when you look a symbol up, as the example does. A request with an unresolved
  type is never cached, so it recovers as soon as the type appears.
- `WithAttribute(string)` and its relatives compare the full metadata name of each applied attribute's class, without looking
  the name up.
- Only named types can be selected as the type a filter names. Arrays, pointers, type parameters and error types throw
  `ArgumentException` there; an array is fine as a type argument of a closed generic.

### How assembly selections combine

Several specific selections are **alternatives**. `FromAssemblies()` is the starting point they narrow, and `NotFrom…`
always excludes:

1. Core assemblies (`mscorlib`, `netstandard`, `System`, `System.Core`, `System.Runtime`, `System.Private.CoreLib`) are
   skipped unless you call `IncludeSystemAssemblies()`, even when you name them.
2. An assembly named by any `NotFrom…` call is excluded, whatever else selects it.
3. If there's at least one specific selection (`EntryAssembly`, `FromAssembly…` or `DependenciesFromAssembly…`), an
   assembly matches when **any** of them selects it. `FromAssemblies()` then adds nothing.
4. With no specific selection, every assembly that's left matches. That includes the compilation's own assembly.
5. An empty selection matches nothing.

| Selection                                         | Scans                                                                              |
| ------------------------------------------------- | ---------------------------------------------------------------------------------- |
| `FromAssemblies()`                                | Every assembly: the references and the compilation's own                           |
| `FromAssemblyOf(a)`                               | `a`'s assembly                                                                     |
| `FromAssemblyOf(a).FromAssemblyOf(b)`             | `a`'s assembly **and** `b`'s assembly                                              |
| `FromAssemblies().FromAssemblyOf(a)`              | Only `a`'s assembly                                                                |
| `DependenciesFromAssemblyOf(t)`                   | Every assembly that directly references `t`'s assembly (not `t`'s assembly itself) |
| `FromAssemblies().DependenciesFromAssemblyOf(t)`  | The same: every assembly that references `t`'s assembly                            |
| `FromAssemblyOf(a).DependenciesFromAssemblyOf(c)` | `a`'s assembly, plus every assembly that references `c`'s assembly                 |
| `FromAssemblies().NotFromAssemblyOf(x)`           | Every assembly except `x`'s                                                        |
| `NotFromAssemblyOf(x)`                            | Every assembly except `x`'s                                                        |
| `FromAssemblyOf(a).NotFromAssemblyOf(a)`          | Nothing                                                                            |
| `IncludeSystemAssemblies()`                       | Every assembly, core assemblies included                                           |

`DependenciesFromAssembly` with an assembly the compilation doesn't reference selects nothing. It still counts as a specific
selection, so it never widens the scan to everything.

> [!NOTE]
> Specular's own generator applies the same rules. Before this change it required every clause to match, so
> `FromAssemblyOf<A>().FromAssemblyOf<B>()` found nothing. See the [changelog](https://github.com/RocketSurgeonsGuild/Specular/blob/main/CHANGELOG.md).

### Selector rules

- The selector is only valid inside the `ScanRequest.Build` callback. Calling it afterwards throws.
- `GetTypes` can be called once per selection.
- The same selection always builds an equal request with the same `CacheKey`. An assembly named twice is recorded once, and
  the `IAssemblySymbol`, type and name overloads of an assembly selection build the same request. For type filters the symbol
  and string overloads build different requests (see above), though they match the same types when both resolve.

### Custom filters

Derive from `CustomTypeFilter` for a rule the built-in filters can't express:

```csharp
public sealed class HasParameterlessConstructor : CustomTypeFilter
{
    // Bump the version whenever IsMatch changes what it matches.
    public override string CacheKey => "has-parameterless-ctor/v1";

    public override bool IsMatch(Compilation compilation, INamedTypeSymbol type) =>
        type.InstanceConstructors.Any(static ctor => ctor.Parameters.IsEmpty && ctor.DeclaredAccessibility == Accessibility.Public);
}

var request = ScanRequest.Build(compilation, s => s
    .FromAssemblies()
    .GetTypes(f => f.AssignableTo(plugin).Where(new HasParameterlessConstructor())));
```

The filter's logic can't be compared or saved, so its `CacheKey` stands in for it:

- Two filters are equal when they're the same class with the same `CacheKey`. The class name and the key are part of the
  request's `CacheKey`, which names the cache entries.
- **`CacheKey` must change whenever what the filter matches changes.** That includes any parameters the filter holds, and the
  filter's own logic when you ship a new version of your generator. If it doesn't change, a build keeps using the results it
  saved before, for as long as the referenced assembly stays the same.
- `IsMatch` should depend only on `type` and the assemblies it comes from. Results for a referenced assembly are reused while
  that assembly is unchanged, even when the project's own code changes.
- Custom filters run after every built-in clause, so they only see types that already passed the rest.

## Results

A scan returns a `ScanResult`:

| Member                 | What it holds                                                                                                                           |
| ---------------------- | --------------------------------------------------------------------------------------------------------------------------------------- |
| `Request`              | The `ScanRequest` that produced it.                                                                                                     |
| `Assemblies`           | `EquatableArray<ScannedAssembly>`: the assemblies that passed the assembly filter, ordered by name.                                     |
| `Types`                | `EquatableArray<ScannedType>`: for a types scan, the matched types ordered by assembly and metadata name. Empty for an assemblies scan. |
| `UnresolvedTypes`      | `EquatableArray<TypeReference>`: each type the request's filters named that couldn't be resolved, in order of first appearance.         |
| `Resolve(Compilation)` | The same result as symbols, in a `ScanSymbols` (`Assemblies` and `Types` as immutable arrays).                                          |

`ScannedAssembly` has the assembly's `Name` and `HasInaccessibleMatches`: whether any type matched in it can't be referenced
from the compilation.

`ScannedType` describes a type by value, so results can be cached and compared:

| Property                | Meaning                                                                                                                                                                                                  |
| ----------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `AssemblyName`          | The containing assembly's metadata name.                                                                                                                                                                 |
| `MetadataName`          | The full metadata name, as `GetTypeByMetadataName` takes it (`MyLib.Outer+Inner`, ``MyLib.IHandler`1``).                                                                                                 |
| `FullyQualifiedName`    | A `global::`-qualified C# name to emit (`global::MyLib.Outer.Inner`, `global::MyLib.IHandler<T>`). For `typeof` of an open generic, keep only the commas inside each `<…>` (`global::MyLib.IHandler<>`). |
| `Kind`                  | The Roslyn `TypeKind`.                                                                                                                                                                                   |
| `DeclaredAccessibility` | The declared accessibility.                                                                                                                                                                              |
| `IsAccessible`          | Whether the compilation can reference the type. This takes into account the declared accessibility, the containing types and `InternalsVisibleTo`.                                                       |
| `Arity`                 | The number of generic type parameters.                                                                                                                                                                   |

Scans also return types your generated code can't name, such as `internal` types in another assembly. Check `IsAccessible`
before you emit `typeof(...)` or a `new` expression.

`ScanSymbols` holds symbols, so it isn't equatable. Use `Resolve` inside an output step, not as a pipeline value. Entries that
no longer resolve are skipped:

```csharp
context.RegisterSourceOutput(result.Combine(context.CompilationProvider), static (output, input) =>
{
    var (result, compilation) = input;
    foreach (INamedTypeSymbol type in result.Resolve(compilation).Types)
    {
        // type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat), its members, its attributes, ...
    }
});
```

Combining with `CompilationProvider` makes the output step run on every compilation change. Where you can, reduce the
result to an equatable model first, as the complete example does.

## Running a scan directly

Outside an incremental pipeline (an analyzer, a test, a one-off transform), run a request yourself:

```csharp
var request = ScanRequest.Build(compilation, s => s.FromAssemblyOf(marker).GetTypes(publicOnly: true));

ScanSymbols symbols = Scanner.Run(compilation, request, cancellationToken);   // symbols
ScanResult result = Scanner.Scan(compilation, request, cancellationToken);    // the equatable result
```

`Scanner.Run` and `Scanner.Scan` don't use any cache. Every call walks every selected assembly.

## Caching

`context.Scan` caches the types each request matched in each **referenced** assembly. Nothing else needs to be set up in the
generator.

- **One file per consumer id**, at `$(IntermediateOutputPath)ctp/<ConsumerId>.ctpjson`, for example
  `obj/Debug/net10.0/ctp/PluginRegistry.ctpjson`. A build writes it and feeds it back to the next build as an
  additional file.
- **Consumer ids** are ASCII letters, digits, `.`, `_` and `-`, and can't be empty. Anything else throws
  `ArgumentException`. Pick an id unique to your generator. File names are matched ignoring case, and `SpecularProvider`
  is Specular's own. Generators never read each other's files.
- **One `Scan` call per consumer id.** See [Several requests](#several-requests).
- **Entries stay valid while the assembly's MVID is unchanged.** The module version id changes whenever the assembly is
  rebuilt with different content. Entries are kept per request (its `CacheKey`) and per assembly.
- **Only references with a readable MVID are cached on disk.** That means `PortableExecutableReference`s: package and
  framework references, and project references in a command-line build. In the IDE, project references arrive as
  compilation references with no MVID, so they're always rescanned rather than read from the file.
- **The compilation's own assembly is never cached.** It changes with every edit, so it's walked on every run.
- **While you type, results are reused in memory.** A referenced assembly's results are remembered for as long as the
  compiler keeps the same reference object, so an edit in the project doesn't rescan its references.
- **Only builds save the file.** It's written from an implementation-only output, which the IDE doesn't run. It's written
  only when its content changed, so an unchanged cache doesn't touch the file's timestamp or make the project out of date.
- **Each save keeps only what the run used.** Entries for removed references or requests drop out.
- **A missing, corrupt or outdated file is ignored and rebuilt.** The cache never fails a build, and nothing is written
  outside MSBuild.

The file format is described in [Scan cache file](../../architecture/scan-cache-file/).

### Storing your own data

A generator often derives something else from each referenced assembly, such as a manifest read from its attributes.
`ScanCache` stores such values per assembly, under the same MVID rules, in a consumer's cache file. Values are serialized
with a `JsonTypeInfo<T>` you supply from a source-generated `JsonSerializerContext` (the DLL package brings
`System.Text.Json` and its source generator; with the Sources package you reference it yourself):

```csharp
public sealed class PluginManifest
{
    public string Name { get; set; } = "";
    public string[] Capabilities { get; set; } = [];
}

[JsonSerializable(typeof(PluginManifest))]
internal sealed partial class ManifestJsonContext : JsonSerializerContext { }
```

Use the store through the `Scan` overload that takes a transform. The transform runs in the same pipeline step as the scans,
gets the run's cache, and whatever it reads or writes is saved in the same file as the scan results, under the same consumer
id:

```csharp
private const int ManifestVersion = 1;

IncrementalValueProvider<PluginModel> model = context.Scan(ConsumerId, requests, static scan =>
{
    var manifests = ImmutableArray.CreateBuilder<PluginManifest>();
    foreach (var assembly in scan.Compilation.SourceModule.ReferencedAssemblySymbols)
    {
        if (!scan.Cache.TryGet("manifest", assembly, ManifestJsonContext.Default.PluginManifest, ManifestVersion, out var manifest))
        {
            manifest = ReadManifest(assembly);
            scan.Cache.Set("manifest", assembly, manifest, ManifestJsonContext.Default.PluginManifest, ManifestVersion);
        }

        manifests.Add(manifest);
    }

    // The output is a pipeline value: make it equatable by value, and never keep symbols, the compilation or `scan` in it.
    return new PluginModel(scan.Results, manifests.ToImmutable());
});
```

`scan` is a `ScanContext` with the `Compilation`, the `Results` in request order, the run's `Cache` and the
`CancellationToken`. It's only valid while the transform runs, and the cache is saved for you: don't call `Save`. Store values
are also remembered in memory while a referenced assembly is unchanged, as scan results are, so typing in the IDE doesn't
recompute them.

A generator that doesn't scan can still use the store by loading and saving a `ScanCache` itself in an implementation output:
`ScanCache.Load(consumerId, compilation, additionalFiles)`, then `TryGet`/`Set`, then `Save(options.GlobalOptions)`. Don't
combine that with `context.Scan` under the same consumer id: each writes the whole file, so they would drop each other's
entries.

- `TryGet` hits only when the assembly's MVID matches **and** the value was stored with the same data version. Bump your
  data version when the shape or meaning of the value changes.
- `Set` does nothing for an assembly that can't be cached: the compilation's own, or a reference without an MVID.
- `Save` writes the entries read or written in this run to `$(IntermediateOutputPath)ctp/<ConsumerId>.ctpjson`, and only when
  the content changed. It does nothing outside MSBuild and never throws on IO errors.
- `ScanCache` is safe to use from several threads within one run. It isn't equatable, so don't pass it through the pipeline.

## Packaging and MSBuild

The cache only works if the projects that use your generator feed the cache file back to the compiler. Both packages take care
of this when you pack your generator: there's nothing to add to your project beyond the `PackageReference`.

### What happens when you pack

The package imports a pack helper into your generator's project. When the project is packed:

1. **DLL package only:** `Specular.Scanning.dll` (and `System.Text.Json.dll`) is added to every `analyzers/` folder that your
   generator packs a `.dll` into. A folder for a Roslyn version, such as `analyzers/dotnet/roslyn5.9/cs`, gets the newest build
   of the library for that version or older. A folder without one, such as `analyzers/dotnet/cs`, gets the Roslyn 5.0 build.
   Files you already pack into a folder are left alone.
2. **The cache wiring:** `build/` and `buildTransitive/` get `Specular.Scanning.Cache.targets` and a `<PackageId>.targets`
   that imports it. If you already pack your own `<PackageId>.targets`, a `<PackageId>.props` is used instead. The import is
   guarded, so a project that uses several such generators imports it once.

The wiring, in each project that uses your generator:

- adds every existing `$(IntermediateOutputPath)ctp/*.ctpjson` to `AdditionalFiles` just before `CoreCompile` (each generator
  picks out its own file), once per file even when Specular adds its own file too;
- deletes the files on `Clean`;
- makes `IntermediateOutputPath` and `ProjectDir` visible to generators, which write the cache there.

A project that uses the generator can set `SpecularScanningCache` to `false` to stop feeding the files back. Generators then
rescan on every build, and `Clean` still deletes the files.

### Properties

Set these in your generator's project file.

| Property                             | Default                                    | Effect                                                                                                                                                                                                                                                                              |
| ------------------------------------ | ------------------------------------------ | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `SpecularScanningPack`               | `true`                                     | `false` adds nothing to the package.                                                                                                                                                                                                                                                |
| `SpecularScanningBundleLibrary`      | `true`                                     | `false` leaves `Specular.Scanning.dll` out of the `analyzers/` folders; the generator then has to ship it. DLL package only.                                                                                                                                                        |
| `SpecularScanningPackCacheTargets`   | `true`                                     | `false` leaves the cache wiring out of the package. Projects using the generator then rescan on every build unless they import `Specular.Scanning.Cache.targets` themselves.                                                                                                        |
| `SpecularScanningAnalyzerPackPaths`  | empty                                      | The `analyzers/` folders the generator is packed into, separated by `;`. Replaces the folders read from the pack items; set it when you pack the generator some other way.                                                                                                          |
| `SpecularScanningLibraryAssembly`    | the package's Roslyn 5.0 build             | The `Specular.Scanning.dll` returned from `GetTargetPath`, with the `System.Text.Json.dll` beside it. Empty with the Sources package. Set it when the generator references the library's project rather than a package; see [In this repository](#referencing-the-library-project). |
| `SpecularScanningAddToTargetPath`    | `true` (`false` with `ImportCacheTargets`) | `false` stops returning `Specular.Scanning.dll` and its `System.Text.Json.dll` from `GetTargetPath`. See [ProjectReference](#using-the-generator-through-a-projectreference).                                                                                                       |
| `SpecularScanningImportCacheTargets` | `false`                                    | `true` imports the cache wiring into this project. For a project that uses the generator through a `ProjectReference`.                                                                                                                                                              |

The helper finds your `analyzers/` folders from what the project packs: `None` and `Content` items with `Pack="true"` and a
`PackagePath`, and `TfmSpecificPackageFile` items. That covers the usual generator layouts.

### Diagnostics

| Code          | Severity | Meaning                                                                                                                                                                                                                                                                            |
| ------------- | -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `SPECSCAN001` | Warning  | `Specular.Scanning` is referenced without `PrivateAssets="all"`. Your package would depend on it, which makes the library a compile reference of every project that uses your generator.                                                                                           |
| `SPECSCAN002` | Warning  | `IsRoslynComponent` is `true`, but nothing is packed under `analyzers/`, so nothing is added.                                                                                                                                                                                      |
| `SPECSCAN003` | Warning  | The project packs both its own `<PackageId>.targets` and `.props`, and neither imports the cache wiring. Add this line to one of them: `<Import Project="$(MSBuildThisFileDirectory)Specular.Scanning.Cache.targets" Condition="'$(_SpecularScanningCacheImported)' != 'true'" />` |
| `SPECSCAN004` | Warning  | An `analyzers/` folder targets a Roslyn version older than 5.0. Nothing is added to it.                                                                                                                                                                                            |
| `SPECSCAN005` | Error    | `Specular.Scanning.Sources` only: the project's `LangVersion` is older than C# 14.                                                                                                                                                                                                 |

### Using the generator through a `ProjectReference`

Inside your own solution (tests, samples) you may use the generator through a `ProjectReference` rather than a package:

```xml
<ItemGroup>
    <ProjectReference Include="../MyGenerator/MyGenerator.csproj" OutputItemType="Analyzer" ReferenceOutputAssembly="false" />
    <!-- Only for the cache wiring. -->
    <PackageReference Include="Specular.Scanning" Version="x.y.z" PrivateAssets="all" IncludeAssets="build" />
</ItemGroup>

<PropertyGroup>
    <SpecularScanningImportCacheTargets>true</SpecularScanningImportCacheTargets>
</PropertyGroup>
```

- With the DLL package, the generator's project returns `Specular.Scanning.dll` and its `System.Text.Json.dll` from
  `GetTargetPath`, so the referencing project passes both to the compiler beside the generator.
- A test project that runs the generator in-process (with `CSharpGeneratorDriver`) gets `Specular.Scanning.dll` copied to its
  output but not listed in its `.deps.json`, so loading the generator fails with `FileNotFoundException`. Reference
  `Specular.Scanning` from the test project too.
- The referencing project imports the cache wiring itself, through the package reference and
  `SpecularScanningImportCacheTargets` above. Use the `Specular.Scanning` package for this even when your generator uses
  `Specular.Scanning.Sources`: the Sources package's build files also set the project's default `LangVersion` and define
  `SPECULAR_SCANNING_SOURCES`, which a project that only uses the generator doesn't want.

### Referencing the library project

A generator in the same repository as `Specular.Scanning` (as the samples in Specular's repository are) can't use the package's
build files, which expect the package layout. Reference the project and import the pack helper directly, pointing
`SpecularScanningLibraryAssembly` at the library's Roslyn 5.0 build:

```xml
<ItemGroup>
    <ProjectReference Include="../Specular.Scanning/Specular.Scanning.csproj" PrivateAssets="all" />
</ItemGroup>

<PropertyGroup>
    <SpecularScanningLibraryAssembly>$(MSBuildThisFileDirectory)../Specular.Scanning.supports/bin/roslyn5.0/$(Configuration)/netstandard2.0/Specular.Scanning.dll</SpecularScanningLibraryAssembly>
</PropertyGroup>

<Import Project="../Specular.Scanning/build/Specular.Scanning.Pack.targets" />
```

Projects that use such a generator import `Specular.Scanning/build/Specular.Scanning.Cache.targets` for the cache wiring.
`samples/generators` in Specular's repository is a working example. Packing a generator wired this way bundles nothing; use
the package for generators you publish.
