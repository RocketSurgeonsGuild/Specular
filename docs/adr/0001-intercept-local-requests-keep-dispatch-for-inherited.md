---
status: proposed
---

# Intercept local requests, keep the file/line dispatch for inherited requests

Today every scan request is answered at runtime by the generated provider: `GetAssemblies`, `GetTypes` and `Scan` take the caller's line
number, file path and argument expression (`[CallerLineNumber]`, `[CallerFilePath]`, `[CallerArgumentExpression]`), and the provider
switches on them to find the results resolved at build time. We propose to answer **local requests** with C# interceptors instead (stable
since SDK 9.0.2xx: the generator gets each call site's `InterceptableLocation` from `GetInterceptableLocation` and emits an
`[InterceptsLocation]` method that returns that call site's results directly), and to keep the switch for **inherited requests**. An
interceptor can replace a call to a method declared anywhere, including `ISpecularProvider` in another assembly, but only at call sites in
the compilation that generates it. An inherited request's call site is in a library compiled before the app exists, and the library's
generator cannot know the assemblies the app will see, so those call sites have to keep finding their results at runtime, by location, in
the app's provider.

## Considered options

- **Keep the switch for everything** (today). One dispatch path, no new MSBuild opt-in, but every call pays a runtime lookup on a string
  path, a line and an expression hash, and moving a call site changes the key the build cache and the provider are keyed by.
- **Intercept everything.** Not possible: the library's compilation, the only place its call sites can be intercepted, does not have the
  app's view of the assemblies, which is what an inherited request is resolved against.
- **Hybrid (proposed).** Local call sites get their results with no lookup and no location strings in the app's binary; inherited requests
  behave exactly as today.

## Consequences

- Two dispatch paths to keep in step: the generated provider still needs the switch, but only for inherited requests, so an app with
  none can drop it.
- Interception binds a call site to this app's results whatever provider instance it is called on. A local call made on another
  `ISpecularProvider` (a test double, another assembly's provider) would no longer reach that instance; the generator should only intercept
  calls whose receiver is statically this assembly's `SpecularProvider`, or document the change.
- Interceptors need the consuming project to list the generated namespace in `InterceptorsNamespaces`; the package's build props can add it.
- The `[Caller*]` parameters stay on `ISpecularProvider` for inherited requests, so the public API does not change.
- Worth measuring before deciding: the runtime cost of today's lookup per call, and how much of the generated provider the switch is for
  apps with no inherited requests.
