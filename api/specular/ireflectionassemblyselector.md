# IReflectionAssemblySelector

- Kind: `interface`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.2-beta.65`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.cfc4e1e69ff7a6dcb9388579cde1a5879d0e3e7e/src/Specular/Abstractions/IReflectionAssemblySelector.cs)

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

- [DependenciesFromAssemblyOf](/Specular/api/specular/ireflectionassemblyselector/methods.md#dependenciesfromassemblyof) : [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md) `abstract` -- Will load and scan from given types assembly
- [DependenciesFromAssemblyOf(Type)](/Specular/api/specular/ireflectionassemblyselector/methods.md#dependenciesfromassemblyof-type) : [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md) `abstract` -- Will load and scan from given types assembly
- [EntryAssembly](/Specular/api/specular/ireflectionassemblyselector/methods.md#entryassembly) : [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from this assembly at compile time.
- [FromAssemblies](/Specular/api/specular/ireflectionassemblyselector/methods.md#fromassemblies) : [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from all metadata assembly at compile time.
- [FromAssemblyOf](/Specular/api/specular/ireflectionassemblyselector/methods.md#fromassemblyof) : [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from the assembly of type `T`.
- [FromAssemblyOf(Type)](/Specular/api/specular/ireflectionassemblyselector/methods.md#fromassemblyof-type) : [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from the assembly of type.
- [IncludeSystemAssemblies](/Specular/api/specular/ireflectionassemblyselector/methods.md#includesystemassemblies) : [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md) `abstract` -- Include system assemblies
- [NotFromAssemblyOf](/Specular/api/specular/ireflectionassemblyselector/methods.md#notfromassemblyof) : [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md) `abstract` -- Will not scan for types from the assembly of type `T`.
- [NotFromAssemblyOf(Type)](/Specular/api/specular/ireflectionassemblyselector/methods.md#notfromassemblyof-type) : [IReflectionTypeSelector](/Specular/api/specular/ireflectiontypeselector.md) `abstract` -- Will not scan for types from the assembly of type.
