# Specular

Compile-time assembly and type scanning for .NET. Specular gives you a Scrutor-style fluent API for finding types and
registering them with `Microsoft.Extensions.DependencyInjection`. A Roslyn source generator evaluates each query at
build time and writes the results into your assembly. Nothing is scanned at runtime, so the same code works with Native
AOT and trimming.

[![github-release-badge]][github-release]
[![github-license-badge]][github-license]
[![github-build-badge]][github-build]
[![nuget-Specular-badge]][nuget-Specular]

Documentation: <https://rocketsurgeonsguild.github.io/Specular/>

## Getting started

Add the package. It brings the source generator with it:

```bash
dotnet add package Specular
```

The generator emits an internal `SpecularProvider` class into your assembly. Pass its `Instance` to `Scan` to register
services; the selector lambda is read at build time, so it can only use values the compiler knows, such as types and
string literals:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Specular;

var services = new ServiceCollection();
ISpecularProvider provider = SpecularProvider.Instance;

provider.Scan(
    services,
    z => z.FromAssemblyOf<IProductService>()
          .AddClasses(f => f.InExactNamespaceOf<IProductService>())
          .AsMatchingInterface()
          .WithSingletonLifetime()
);
```

`GetTypes` returns the matching types without registering them:

```csharp
IEnumerable<Type> productServices = provider.GetTypes(
    z => z.FromAssemblies().GetTypes(f => f.AssignableTo<IProductService>())
);
```

Libraries that should be scanned by the application, without a provider of their own, set
`<SpecularEmitProvider>false</SpecularEmitProvider>`. See the
[installation guide](https://rocketsurgeonsguild.github.io/Specular/guide/installation/), the
[quickstart](https://rocketsurgeonsguild.github.io/Specular/guide/quickstart/) and
[AOT publishing](https://rocketsurgeonsguild.github.io/Specular/guide/aot-publishing/).

## Packages

| Package                                                      | NuGet                                                                       | What it does                                                                                          |
| ------------------------------------------------------------ | --------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------- |
| [Specular][nuget-Specular]                                   | [![nuget-Specular-badge]][nuget-Specular]                                   | `ISpecularProvider`, the selector and type filter interfaces, and the service registration attributes |
| [Specular.Analyzers][nuget-Specular.Analyzers]               | [![nuget-Specular.Analyzers-badge]][nuget-Specular.Analyzers]               | The source generator that resolves each query and writes `SpecularProvider`                           |
| [Specular.Scanning][nuget-Specular.Scanning]                 | [![nuget-Specular.Scanning-badge]][nuget-Specular.Scanning]                 | Specular's compile-time scanner and build cache, for your own source generators                       |
| [Specular.Scanning.Sources][nuget-Specular.Scanning.Sources] | [![nuget-Specular.Scanning.Sources-badge]][nuget-Specular.Scanning.Sources] | The same library as source, compiled into your generator                                              |

`Specular` brings in `Specular.Analyzers`, so you don't reference it yourself.

Writing your own source generator? `Specular.Scanning` lets it scan the compilation and its references with the same
selectors, for example `FromAssemblies().GetTypes(f => f.AssignableTo(handler))`, and caches the results across builds. See
[Scanning from your own generator](https://rocketsurgeonsguild.github.io/Specular/guide/scanning-from-your-generator/).

## How it works

This library works in two parts.

### The .NET library (`Specular`)

The first part is the interfaces and attributes. `ISpecularProvider` grants you access to the scanner. There are helper methods to get access to the scanner for a given assembly ( any thoughts on how to improve this experience are welcome?!? )

Each assembly that emits a provider exposes it as a compile-time singleton, `SpecularProvider.Instance`, so applications can grab the scanner for their own compilation directly without any runtime reflection. However your consumers should almost **ALWAYS** accept the `ISpecularProvider` interface as a dependency, and not rely on the static `SpecularProvider.Instance` singleton. This ensures that the correct scanner is used. THIS IS IMPORTANT but we will get to that in step 2.

The `ISpecularProvider` interface has 3 methods for scanning.

> It's important to note that these methods are never actually executed at runtime. There are limitations about what you can and can't pass into these methods, because they are compile time only hints for the source generator. You will find you cannot for example pass in a variable, but you can use static values, values that the compiler knows at compile. (strings, open generics, etc.)

#### Assemblies

You can scan for assemblies!

- `GetAssemblies(selector)` which allows you to get a list of assemblies based on the `IReflectionAssemblySelector`.
  These method calls can be chained together to select multiples, and do many reflection like operations.
    - `FromAssemblies`: grants a list of all assemblies from the `EntryAssembly` compilation. More specifically the compilation that produced the `ISpecularProvider`. Again this comes around in part 2.
    - `EntryAssembly`: Gets the entry assembly.
    - `DependenciesFromAssemblyOf<T>()` / `DependenciesFromAssemblyOf(Type type)`: Gets the dependencies of the assembly of the given type.
    - `FromAssemblyOf<T>()` / `FromAssemblyOf(Type type)`: Includes the assembly of the given type.
    - `NotFromAssemblyOf<T>()` / `NotFromAssemblyOf(Type type)`: Excludes the assembly of the given type.
    - `IncludeSystemAssemblies()`: By default we exclude the system assemblies to speed up the scanning process. ("mscorlib", "netstandard", "System", "System.Core", "System.Runtime", "System.Private.CoreLib")

> This method offers a glimpse into how this library works. When you see the next two methods you'll see certain parts that look duplicated, that because it is.

#### Types

You can scan for types!

- `GetTypes(selector)` which allows you get a list of types based on the `IReflectionTypeSelector`
    - `GetTypes([bool publicOnly])`: Just the standard get types, not really interesting... not really helpful for AOT scenarios.
    - `GetTypes([bool publicOnly], Action<ITypeFilter> action)`: where you provider a `ITypeFilter`
        - `AssignableTo`/`AssignableToAny`/`NotAssignableTo`/`NotAssignableToAny`: Filters the types based on assignability to the given type(s).
        - `StartsWith`/`EndsWith`/`NotStartsWith`/`NotEndsWith`/`Contains`/`NotContains`: Filters the types based on the name of the type.
        - `InExactNamespaceOf`/`InExactNamespaces`/`InNamespaceOf`/`InNamespaces`/`NotInExactNamespaceOf`/`NotInExactNamespaces`/`NotInNamespaceOf`/`NotInNamespaces`: Filters the types based on the namespace of the type.
        - `WithAttribute`/`WithAnyAttribute`/`WithoutAttribute`/`WithoutAnyAttribute`: Filters the types based on the attributes of the type.
        - `KindOf`/`NotKindOf`: Filters the types based on the kind of type (class, interface, struct, record, etc).
        - `InfoOf`/`NotInfoOf`: Filters the types based on the kind of type (sealed, abstract, generic, static, visible, valuetype, etc).

> Service descriptors below support the exact same filtering as the type filtering, so we'll skip that below.

#### Service descriptors

For service descriptors they're a little different, but directly extend the type scanning.

We introduce `IServiceTypeSelector` and `IServiceLifetimeSelector`

After we scan the types, we get to choose how the service is supposed to behave when generated.

- `AsSelf`: Registers the type as itself.
- `As<T>`/`As(Type type)`: Registers the type as the given type.
- `AsImplementedInterfaces([Action<ITypeFilter> action])`/`AsSelfWithInterfaces([Action<ITypeFilter> action])`: Registers the type as all of its implemented interfaces, or as itself and all of its implemented interfaces.
- `AsMatchingInterface`: Matching interface and class names.
- `IServiceLifetimeSelector`
    - `WithSingletonLifetime`: Registers the type as a singleton.
    - `WithScopedLifetime`: Registers the type as scoped.
    - `WithTransientLifetime`: Registers the type as transient.
    - `WithLifetime(ServiceLifetime lifetime)`: Registers the type with the given lifetime.

We also have baked in support for a few attributes

- `ExcludeFromSpecularAttribute`: Lets you exclude a given assembly from being annotated by the source generator. This is useful for assemblies that you don't want to scan, or that you don't want to be scanned by the source generator.
- `RegistrationLifetimeAttribute`: Specifies the lifetime of a service registration. If not set, will use `ServiceRegistrationAttribute`.
- `ServiceRegistrationAttribute`: Specifies the service registration for a given type. If lifetime is not set, will use the lifetime specified at compile time.

### The source generator (`Specular.Analyzers`)

Okay so the source generator is where the magic happens.

> Let me preface this by saying I know you're not supposed to cache source generators. However the method I used is pretty clever in that we're not reading from disk only saving. We let `AdditionalItems` do the reading for us. This is a bit of a hack, but it works. If you have a better way to do this, please let me know.

Reflection is awesome partly because Linq is a pretty awesome as well. So this library can be thought as the poor-compiler version of linq. I'm not looking to reflection reflection in every single corner cas, but I want to be able to capture the important cases. Things like...

- I have a `IRequestHandler` and I want to get a list of all them in all the referenced assemblies.
- I have an attribute I use to mark services for registration.
- etc...

If there are corner cases that the compiler can handle then lets add them, I want this to be as comprehensive as possible, but I am not striving for full compatibility with reflection, it just is not feasible.

Okay the explaining part out of the way.

#### The metadata

Every assembly that references the `Specular` library will have all of the scanned selectors serialized into a string, and stored as AssemblyMetadata attributes at compile time. In essence we're not scanning any assemblies, we're only telling the compiler "this is what I want you to scan for me from this point in the future.".

When the compiler scans this assembly in the future, it will see the metadata and hydrate the queries. Each assembly brings its own query data (if any) and the compiler will merge the results together later.

#### The scanner

The scanner uses the compiler API to scan all assemblies and types of the given compilation. It uses all the different pieces of metadata to build up a full list method calls that it needs to build out. Each assembly then gets an `ISpecularProvider` generated for it, and the method calls are generated to return the correct results, using generators (yield return) to return the results.

> I skipped something in the explanation of the types earlier. Every method on the `SpecularProvider` takes in its arguments but it also uses `CallerLineNumber`, `CallerFilePath` and `CallerArgumentExpression`. It then uses these values in a huge switch statement to determine which list of items to return. If the queries are all on different lines 1 jump, at most you'll have 2 jumps (unless you make some silly single long code!)

The generated `ISpecularProvider` is exposed as a compile-time singleton (`SpecularProvider.Instance`) and is also described by an assembly attribute used for cross-assembly cache busting. Whether an assembly emits a provider at all is controlled by the `SpecularEmitProvider` MSBuild property (defaults to `true`); library assemblies that are only meant to be scanned can set it to `false` so the consuming application is the one that emits the provider. Inside the generated provider are the scanner results of all of the queries that were found across all assemblies in the compilation.

## Contributing

Run `mise install` before building: it installs the .NET SDK and the extra .NET 10 runtime that the net10.0 tests run on (`.config/mise.toml`). `global.json` pins the same SDK, so a `dotnet` outside mise fails fast unless that SDK is installed.

> [!NOTE]
> On macOS, the first `mise install` of a new .NET SDK can overwrite the shared `~/.local/share/mise/dotnet-root/dotnet` host in place, and macOS then kills every `dotnet` command (SIGKILL). Replace the host with a fresh copy to fix it:
>
> ```bash
> cd ~/.local/share/mise/dotnet-root && cp -p dotnet dotnet.fresh && mv -f dotnet.fresh dotnet
> ```

<!-- references -->

[github-release]: https://github.com/RocketSurgeonsGuild/Specular/releases/latest
[github-release-badge]: https://img.shields.io/github/release/RocketSurgeonsGuild/Specular.svg?logo=github&style=flat "Latest Release"
[github-license]: https://github.com/RocketSurgeonsGuild/Specular/blob/main/LICENSE
[github-license-badge]: https://img.shields.io/github/license/RocketSurgeonsGuild/Specular.svg?style=flat "License"
[github-build]: https://github.com/RocketSurgeonsGuild/Specular/actions/workflows/build.yml
[github-build-badge]: https://github.com/RocketSurgeonsGuild/Specular/actions/workflows/build.yml/badge.svg?branch=main "Build"
[nuget-Specular]: https://www.nuget.org/packages/Specular/
[nuget-Specular-badge]: https://img.shields.io/nuget/v/Specular.svg?logo=nuget&style=flat
[nuget-Specular.Analyzers]: https://www.nuget.org/packages/Specular.Analyzers/
[nuget-Specular.Analyzers-badge]: https://img.shields.io/nuget/v/Specular.Analyzers.svg?logo=nuget&style=flat
[nuget-Specular.Scanning]: https://www.nuget.org/packages/Specular.Scanning/
[nuget-Specular.Scanning-badge]: https://img.shields.io/nuget/v/Specular.Scanning.svg?logo=nuget&style=flat
[nuget-Specular.Scanning.Sources]: https://www.nuget.org/packages/Specular.Scanning.Sources/
[nuget-Specular.Scanning.Sources-badge]: https://img.shields.io/nuget/v/Specular.Scanning.Sources.svg?logo=nuget&style=flat
