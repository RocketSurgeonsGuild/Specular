# IServiceDescriptorAssemblySelector

- Kind: `interface`
- Package: [Specular](/Specular/pr-preview/pr-464/api/specular.md)
- Version: `0.4.3-pr.22`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-464-merge.Sha.1c51e62778e1813db280ec8727b59cd4afa8515f/src/Specular/Abstractions/IServiceDescriptorAssemblySelector.cs)

The compiled assembly selector

## Definition

```csharp
namespace Specular.Abstractions;

public interface IServiceDescriptorAssemblySelector
{
    // ...
}
```

## Methods

- [DependenciesFromAssemblyOf](/Specular/pr-preview/pr-464/api/specular/iservicedescriptorassemblyselector/methods.md#dependenciesfromassemblyof) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-464/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will load and scan from given types assembly
- [DependenciesFromAssemblyOf(Type)](/Specular/pr-preview/pr-464/api/specular/iservicedescriptorassemblyselector/methods.md#dependenciesfromassemblyof-type) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-464/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will load and scan from given types assembly
- [EntryAssembly](/Specular/pr-preview/pr-464/api/specular/iservicedescriptorassemblyselector/methods.md#entryassembly) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-464/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will scan for types from this assembly at compile time.
- [FromAssemblies](/Specular/pr-preview/pr-464/api/specular/iservicedescriptorassemblyselector/methods.md#fromassemblies) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-464/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will scan for types from all metadata assembly at compile time.
- [FromAssemblyOf](/Specular/pr-preview/pr-464/api/specular/iservicedescriptorassemblyselector/methods.md#fromassemblyof) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-464/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will scan for types from the assembly of type `T`.
- [FromAssemblyOf(Type)](/Specular/pr-preview/pr-464/api/specular/iservicedescriptorassemblyselector/methods.md#fromassemblyof-type) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-464/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will scan for types from the assembly of type.
- [IncludeSystemAssemblies](/Specular/pr-preview/pr-464/api/specular/iservicedescriptorassemblyselector/methods.md#includesystemassemblies) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-464/api/specular/iservicedescriptortypeselector.md) `abstract` -- Include system assemblies
- [NotFromAssemblyOf](/Specular/pr-preview/pr-464/api/specular/iservicedescriptorassemblyselector/methods.md#notfromassemblyof) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-464/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will not scan for types from the assembly of type `T`.
- [NotFromAssemblyOf(Type)](/Specular/pr-preview/pr-464/api/specular/iservicedescriptorassemblyselector/methods.md#notfromassemblyof-type) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-464/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will not scan for types from the assembly of type.
