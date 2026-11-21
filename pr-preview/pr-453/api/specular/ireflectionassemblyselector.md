# IReflectionAssemblySelector

- Kind: `interface`
- Package: [Specular](/Specular/pr-preview/pr-453/api/specular.md)
- Version: `0.4.3-pr.8`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-453-merge.Sha.ca2de83094300ad9fca8d4b18dd49afd62fc3ac1/src/Specular/Abstractions/IReflectionAssemblySelector.cs)

The compiled assembly selector

## Definition

```csharp
namespace Specular.Abstractions;

public interface IReflectionAssemblySelector
{
    // ...
}
```

## Methods

- [DependenciesFromAssemblyOf](/Specular/pr-preview/pr-453/api/specular/ireflectionassemblyselector/methods.md#dependenciesfromassemblyof) : [IReflectionTypeSelector](/Specular/pr-preview/pr-453/api/specular/ireflectiontypeselector.md) `abstract` -- Will load and scan from given types assembly
- [DependenciesFromAssemblyOf(Type)](/Specular/pr-preview/pr-453/api/specular/ireflectionassemblyselector/methods.md#dependenciesfromassemblyof-type) : [IReflectionTypeSelector](/Specular/pr-preview/pr-453/api/specular/ireflectiontypeselector.md) `abstract` -- Will load and scan from given types assembly
- [EntryAssembly](/Specular/pr-preview/pr-453/api/specular/ireflectionassemblyselector/methods.md#entryassembly) : [IReflectionTypeSelector](/Specular/pr-preview/pr-453/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from this assembly at compile time.
- [FromAssemblies](/Specular/pr-preview/pr-453/api/specular/ireflectionassemblyselector/methods.md#fromassemblies) : [IReflectionTypeSelector](/Specular/pr-preview/pr-453/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from all metadata assembly at compile time.
- [FromAssemblyOf](/Specular/pr-preview/pr-453/api/specular/ireflectionassemblyselector/methods.md#fromassemblyof) : [IReflectionTypeSelector](/Specular/pr-preview/pr-453/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from the assembly of type `T`.
- [FromAssemblyOf(Type)](/Specular/pr-preview/pr-453/api/specular/ireflectionassemblyselector/methods.md#fromassemblyof-type) : [IReflectionTypeSelector](/Specular/pr-preview/pr-453/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from the assembly of type.
- [IncludeSystemAssemblies](/Specular/pr-preview/pr-453/api/specular/ireflectionassemblyselector/methods.md#includesystemassemblies) : [IReflectionTypeSelector](/Specular/pr-preview/pr-453/api/specular/ireflectiontypeselector.md) `abstract` -- Include system assemblies
- [NotFromAssemblyOf](/Specular/pr-preview/pr-453/api/specular/ireflectionassemblyselector/methods.md#notfromassemblyof) : [IReflectionTypeSelector](/Specular/pr-preview/pr-453/api/specular/ireflectiontypeselector.md) `abstract` -- Will not scan for types from the assembly of type `T`.
- [NotFromAssemblyOf(Type)](/Specular/pr-preview/pr-453/api/specular/ireflectionassemblyselector/methods.md#notfromassemblyof-type) : [IReflectionTypeSelector](/Specular/pr-preview/pr-453/api/specular/ireflectiontypeselector.md) `abstract` -- Will not scan for types from the assembly of type.
