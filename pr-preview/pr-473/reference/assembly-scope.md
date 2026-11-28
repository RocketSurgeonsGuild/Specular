# Assembly scope

# Assembly scope

A selector that does not name its assemblies (`FromAssemblies()`, `NotFromAssemblyOf<T>()`, `DependenciesFromAssemblyOf<T>()`) looks in
the project's **assembly scope**. By default that is every assembly the project references, plus its own, **except the framework**:

- assemblies signed with one of Microsoft's framework keys (the BCL, ASP.NET Core, `Microsoft.Extensions.*`, and so on), and
- assemblies named `netstandard`, `mscorlib`, `System`, `System.*`, `Microsoft.*` or `WindowsBase`, for builds of them without those keys.

The project's own assembly is always in scope.

## Changing it

Set two properties in the project file. Each takes patterns separated by `;` or `,`; a pattern matches the whole assembly name,
ignores case, and can use `*` (any run of characters) and `?` (one character):

```xml
<PropertyGroup>
    <!-- Leave these out of broad scans. -->
    <SpecularExcludeAssemblies>$(SpecularExcludeAssemblies);Contoso.Legacy.*;Contoso.Tests</SpecularExcludeAssemblies>

    <!-- Bring these in, whatever excludes them, the framework default included. -->
    <SpecularIncludeAssemblies>$(SpecularIncludeAssemblies);Microsoft.Extensions.Logging</SpecularIncludeAssemblies>
</PropertyGroup>
```

**An include always wins.** An assembly that matches any `SpecularIncludeAssemblies` pattern is in scope, even if an exclude pattern or
the framework default would leave it out. `<SpecularIncludeAssemblies>*</SpecularIncludeAssemblies>` scans everything.

These are properties rather than items because MSBuild expands `*` and `?` in an item's `Include` as a file glob: an item pattern would
quietly disappear, or turn into the names of files in your project folder.

| Pattern           | Matches                                                  |
| ----------------- | -------------------------------------------------------- |
| `Contoso`         | `Contoso` only                                           |
| `Contoso.*`       | `Contoso.Billing`, `Contoso.Billing.Data`, not `Contoso` |
| `Contoso*`        | `Contoso`, `Contoso.Billing`, `ContosoTools`             |
| `*.Tests`         | `Contoso.Tests`, `Fabrikam.Tests`                        |
| `Contoso.?illing` | `Contoso.Billing`, `Contoso.Filling`                     |

## What a selector can still reach

The scope decides what a selector that does not name an assembly looks in. A selector can still reach outside it for that one call:

- `FromAssemblyOf<T>()` and `FromAssemblies(typeof(T))` scan `T`'s assembly even when it is out of scope.
- `IncludeSystemAssemblies()` brings back the assemblies only the framework default leaves out. It does not bring back the ones your
  `SpecularExcludeAssemblies` patterns exclude.

`[assembly: ExcludeFromSpecular]` (see [ExcludeFromSpecularAttribute](./exclude-from-specular/)) still takes precedence over everything:
no selector scans such an assembly.

An assembly out of scope also contributes none of the scans it declares itself: its `GetTypes` and `Scan` calls are not resolved again in
your project.

## Migrating from `ExcludeAssemblyFromCTP`

The `ExcludeAssemblyFromCTP` property is no longer read. It matched names by prefix, so `System` also left out `Systems.Billing`; the
framework default now covers what its default value did. Move any entries you added to it into `SpecularExcludeAssemblies`, adding `*`
where you relied on the prefix:

```xml
<!-- Before -->
<ExcludeAssemblyFromCTP>$(ExcludeAssemblyFromCTP);Contoso.Legacy</ExcludeAssemblyFromCTP>

<!-- After -->
<SpecularExcludeAssemblies>$(SpecularExcludeAssemblies);Contoso.Legacy*</SpecularExcludeAssemblies>
```

## For generator authors

The scope lives in `Specular.Scanning`, so a generator built on it gets the same default. `context.Scan(consumerId, ...)` reads the
project's patterns from the MSBuild properties `<consumerId>ExcludeAssemblies` and `<consumerId>IncludeAssemblies`, separated by `;` or
`,`. Make them visible to the compiler and set them from whatever your package's users write. Specular's own, for the consumer id
`SpecularProvider`:

```xml
<!-- build/YourGenerator.props -->
<ItemGroup>
    <CompilerVisibleProperty Include="SpecularProviderExcludeAssemblies" />
    <CompilerVisibleProperty Include="SpecularProviderIncludeAssemblies" />
</ItemGroup>

<!-- build/YourGenerator.targets: in a target, so values set by later imports (Directory.Build.targets) are seen -->
<Target Name="_SpecularAssemblyScope" BeforeTargets="GenerateMSBuildEditorConfigFileShouldRun">
    <PropertyGroup>
        <SpecularProviderExcludeAssemblies>$(SpecularExcludeAssemblies)</SpecularProviderExcludeAssemblies>
        <SpecularProviderIncludeAssemblies>$(SpecularIncludeAssemblies)</SpecularProviderIncludeAssemblies>
    </PropertyGroup>
</Target>
```

`Scanner.Scan(compilation, request)` applies the framework default; pass a `ScanAssemblyScope` to choose another
(`ScanAssemblyScope.Everything`, `ScanAssemblyScope.FromOptions(globalOptions, consumerId)`, or one built from your own patterns).