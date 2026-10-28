# IReflectionAssemblySelector

- Kind: `interface`
- Package: [Specular](/Specular/v0.3.2/api/specular.md)
- Version: `0.3.2`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`

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

- [DependenciesFromAssemblyOf](/Specular/v0.3.2/api/specular/ireflectionassemblyselector/methods.md#dependenciesfromassemblyof) : [IReflectionTypeSelector](/Specular/v0.3.2/api/specular/ireflectiontypeselector.md) `abstract` -- Will load and scan from given types assembly
- [DependenciesFromAssemblyOf(Type)](/Specular/v0.3.2/api/specular/ireflectionassemblyselector/methods.md#dependenciesfromassemblyof-type) : [IReflectionTypeSelector](/Specular/v0.3.2/api/specular/ireflectiontypeselector.md) `abstract` -- Will load and scan from given types assembly
- [EntryAssembly](/Specular/v0.3.2/api/specular/ireflectionassemblyselector/methods.md#entryassembly) : [IReflectionTypeSelector](/Specular/v0.3.2/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from this assembly at compile time.
- [FromAssemblies](/Specular/v0.3.2/api/specular/ireflectionassemblyselector/methods.md#fromassemblies) : [IReflectionTypeSelector](/Specular/v0.3.2/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from all metadata assembly at compile time.
- [FromAssemblyOf](/Specular/v0.3.2/api/specular/ireflectionassemblyselector/methods.md#fromassemblyof) : [IReflectionTypeSelector](/Specular/v0.3.2/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from the assembly of type `T`.
- [FromAssemblyOf(Type)](/Specular/v0.3.2/api/specular/ireflectionassemblyselector/methods.md#fromassemblyof-type) : [IReflectionTypeSelector](/Specular/v0.3.2/api/specular/ireflectiontypeselector.md) `abstract` -- Will scan for types from the assembly of type.
- [IncludeSystemAssemblies](/Specular/v0.3.2/api/specular/ireflectionassemblyselector/methods.md#includesystemassemblies) : [IReflectionTypeSelector](/Specular/v0.3.2/api/specular/ireflectiontypeselector.md) `abstract` -- Include system assemblies
- [NotFromAssemblyOf](/Specular/v0.3.2/api/specular/ireflectionassemblyselector/methods.md#notfromassemblyof) : [IReflectionTypeSelector](/Specular/v0.3.2/api/specular/ireflectiontypeselector.md) `abstract` -- Will not scan for types from the assembly of type `T`.
- [NotFromAssemblyOf(Type)](/Specular/v0.3.2/api/specular/ireflectionassemblyselector/methods.md#notfromassemblyof-type) : [IReflectionTypeSelector](/Specular/v0.3.2/api/specular/ireflectiontypeselector.md) `abstract` -- Will not scan for types from the assembly of type.
