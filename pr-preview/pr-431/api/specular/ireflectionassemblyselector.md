# IReflectionAssemblySelector

- Kind: `interface`
- Package: [Specular](/Specular/pr-preview/pr-431/api/specular.md)
- Version: `0.4.2-pr.57`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-431-merge.Sha.216015f7483e4a22dec13765e68d4ac57c482acf/src/Specular/Abstractions/IReflectionAssemblySelector.cs)

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

- [DependenciesFromAssemblyOf](/Specular/pr-preview/pr-431/api/specular/ireflectionassemblyselector/methods.md#dependenciesfromassemblyof) : [IReflectionTypeSelector](/Specular/pr-preview/pr-431/api/specular/ireflectiontypeselector.md) `abstract` -- Will load and scan from given types assembly
- [DependenciesFromAssemblyOf(Type)](/Specular/pr-preview/pr-431/api/specular/ireflectionassemblyselector/methods.md#dependenciesfromassemblyof-type) : [IReflectionTypeSelector](/Specular/pr-preview/pr-431/api/specular/ireflectiontypeselector.md) `abstract` -- Will load and scan from given types assembly
- [EntryAssembly](/Specular/pr-preview/pr-431/api/specular/ireflectionassemblyselector/methods.md#entryassembly) : [IReflectionTypeSelector](/Specular/pr-preview/pr-431/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from this assembly at compile time.
- [FromAssemblies](/Specular/pr-preview/pr-431/api/specular/ireflectionassemblyselector/methods.md#fromassemblies) : [IReflectionTypeSelector](/Specular/pr-preview/pr-431/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from all metadata assembly at compile time.
- [FromAssemblyOf](/Specular/pr-preview/pr-431/api/specular/ireflectionassemblyselector/methods.md#fromassemblyof) : [IReflectionTypeSelector](/Specular/pr-preview/pr-431/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from the assembly of type `T`.
- [FromAssemblyOf(Type)](/Specular/pr-preview/pr-431/api/specular/ireflectionassemblyselector/methods.md#fromassemblyof-type) : [IReflectionTypeSelector](/Specular/pr-preview/pr-431/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from the assembly of type.
- [IncludeSystemAssemblies](/Specular/pr-preview/pr-431/api/specular/ireflectionassemblyselector/methods.md#includesystemassemblies) : [IReflectionTypeSelector](/Specular/pr-preview/pr-431/api/specular/ireflectiontypeselector.md) `abstract` -- Include system assemblies
- [NotFromAssemblyOf](/Specular/pr-preview/pr-431/api/specular/ireflectionassemblyselector/methods.md#notfromassemblyof) : [IReflectionTypeSelector](/Specular/pr-preview/pr-431/api/specular/ireflectiontypeselector.md) `abstract` -- Will not scan for types from the assembly of type `T`.
- [NotFromAssemblyOf(Type)](/Specular/pr-preview/pr-431/api/specular/ireflectionassemblyselector/methods.md#notfromassemblyof-type) : [IReflectionTypeSelector](/Specular/pr-preview/pr-431/api/specular/ireflectiontypeselector.md) `abstract` -- Will not scan for types from the assembly of type.
