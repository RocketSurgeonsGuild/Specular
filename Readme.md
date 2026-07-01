# Indago

Indago is the next-generation library from Rocket Surgeons Guild, focused on modern .NET development. This repository contains only the Indago library and related documentation.

I like reflection, I have a recurring joke with a good friend about `.Any()` and how we first met when we were working together. Anyway I reflection really but I hate how much time it takes! I know time is relative and reflection today is really fast.

But wait, there's AOT, you can't rely on reflection in AOT, and heck you can't rely on reflection to have all the assemblies loaded it needs. Sometimes you have to kick it by inspecting the assemblies yourself.

But I also like tools like [Scrutor](https://www.nuget.org/packages/Scrutor) being able to scan assemblies and register services. Makes life so easy! What I wanted to do that but in an AOT friend way. So I created Indago, a library that allows you to scan assemblies and register services in a way that is AOT friendly.

Using the 🪄 MAGIC 🪄 of roslyn and Source Generators we get something that is really really close to magic.

## How

This library works in two parts.

### The .NET Library ( `Indago` )

The first part is the interfaces and attributes. `IIndagoProvider` grants you access the scanner. There are helper methods to get access to the scanner for a given assembly ( any thoughts on how to improve this experience are welcome?!? )

Each assembly that emits a provider exposes it as a compile-time singleton, `IndagoProvider.Instance`, so applications can grab the scanner for their own compilation directly without any runtime reflection. However your consumers should almost **ALWAYS** accept the `IIndagoProvider` interface as a dependency, and not rely on the static `IndagoProvider.Instance` singleton. This ensures that the correct scanner is used. THIS IS IMPORTANT but we will get to that in step 2.

The `IIndagoProvider` interface has 3 methods for scanning.

> It's important to note that these methods are never actually executed at runtime. There are limiitations about what you can and can't pass into these methods, because they are compile time only hints for the source generator. You will find you cannot for example pass in a variable, but you can use static values, values that the compiler knows at compile. (strings, open generics, etc.)

#### Assemblies

You can scan for assemblies!

- `GetAssemblies(selector)` which allows you to get a list of assemblies based on the `IReflectionAssemblySelector`.
  These method calls can be chained together to select multiples, and do many reflection like operations.
    - `FromAssemblies`: grants a list of all assemblies from the `EntryAssembly` compilation. More specifically the compilation that produced the `IIndagoProvider`. Again this comes around in part 2.
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

#### Service Descriptors

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

`ExcludeFromIndagoAttribute`: Lets you exclude a given assembly from being annotated by the source generator. This is useful for assemblies that you don't want to scan, or that you don't want to be scanned by the source generator.
`RegistrationLifetimeAttribute`: Specifies the lifetime of a service registration. If not set, will use `ServiceRegistrationAttribute`.
`ServiceRegistrationAttribute`: Specifies the service registration for a given type. If lifetime is not set, will use the lifetime specified at compile time.

### The Source Generator ( `Indago.Analyzers` )

Okay so the source generator is where the magic happens.

> Let me preface this by saying I know you're not supposed to cache source generators. However the method I used is pretty clever in that we're not reading from disk only saving. We let `AdditionalItems` do the reading for us. This is a bit of a hack, but it works. If you have a better way to do this, please let me know.

Reflection is awesome partly because Linq is a pretty awesome as well. So this library can be thought as the poor-compiler version of linq. I'm not looking to reflection reflection in every single corner cas, but I want to be able to capture the important cases. Things like...

- I have a `IRequestHandler` and I want to get a list of all them in all the referenced assemblies.
- I have an attribute I use to mark services for registration.
- etc...

If there are corner cases that the compiler can handle then lets add them, I want this to be as comprehensive as possible, but I am not striving for full compatibility with reflection, it just is not feasible.

Okay the explaining part out of the way.

#### The Metadata

Every assembly that references the `Indago` library will have all of the scanned selectors serialized into a string, and stored as AssemblyMetadata attributes at compile time. In essence we're not scanning any assemblies, we're only telling the compiler "this is what I want you to scan for me from this point in the future.".

When the compiler scans this assembly in the future, it will see the metadata and hydrate the queries. Each assembly brings it's own query data (if any) and the compiler will merge the results together later.

#### The Scanner

The scanner uses the compiler API to scan all assemblies and types of the given compilation. It uses all the different pieces of metadata to build up a full list method calls that it needs to build out. Each assembly then gets an `IIndagoProvider` generated for it, and the method calls are generated to return the correct results, using generators (yield return) to return the results.

> I skipped something in the explination of the types earlier. Every method on the `IndagoProvider` takes in it's arguments but it also uses `CallerLineNumber`, `CallerFilePath` and `CallerArgumentExpression`. It then uses these values in a huge switch statement to determine which list of items to return. If the queries are all on different lines 1 jump, at most you'll have 2 jumps (unless you make some silly single long code!)

The generated `IIndagoProvider` is exposed as a compile-time singleton (`IndagoProvider.Instance`) and is also described by an assembly attribute used for cross-assembly cache busting. Whether an assembly emits a provider at all is controlled by the `IndagoEmitProvider` MSBuild property (defaults to `true`); library assemblies that are only meant to be scanned can set it to `false` so the consuming application is the one that emits the provider. Inside the generated provider are the scanner results of all of the queries that were found across all assemblies in the compilation.

# Status

<!-- badges -->

[![github-license-badge]][github-license]

<!-- badges -->

<!-- history badges -->

| GitHub Actions            |
| ------------------------- |
| [![github-badge]][github] |

<!-- history badges -->

<!-- nuget packages -->

| Package | NuGet                                         |
| ------- | --------------------------------------------- |
| Indago  | [![nuget-version-indago-badge]][nuget-indago] |

<!-- nuget packages -->

# Whats next?

<!-- generated references -->

[github-license]: https://github.com/RocketSurgeonsGuild/Indago/blob/main/LICENSE
[github-license-badge]: https://img.shields.io/github/license/RocketSurgeonsGuild/Indago.svg?style=flat 'License'
[github]: https://github.com/RocketSurgeonsGuild/Indago/actions?query=workflow%3Aci
[github-badge]: https://img.shields.io/github/actions/workflow/status/RocketSurgeonsGuild/Indago/ci.yml?branch=main&label=github&logo=github&color=b845fc&logoColor=b845fc&style=flat 'GitHub Actions Status'
[nuget-indago]: https://www.nuget.org/packages/Indago/
[nuget-version-indago-badge]: https://img.shields.io/nuget/v/Indago.svg?color=004880&logo=nuget&style=flat-square 'NuGet Version'

<!-- nuke-data
github:
  owner: RocketSurgeonsGuild
  repository: Extensions
-->
