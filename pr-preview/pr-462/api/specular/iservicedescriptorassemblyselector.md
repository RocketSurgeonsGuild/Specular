# IServiceDescriptorAssemblySelector

- Kind: `interface`
- Package: [Specular](/Specular/pr-preview/pr-462/api/specular.md)
- Version: `0.4.3-pr.15`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-462-merge.Sha.65577d9b63559d60a65f5887b78a6f971676b065/src/Specular/Abstractions/IServiceDescriptorAssemblySelector.cs)

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

- [DependenciesFromAssemblyOf](/Specular/pr-preview/pr-462/api/specular/iservicedescriptorassemblyselector/methods.md#dependenciesfromassemblyof) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-462/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will load and scan from given types assembly
- [DependenciesFromAssemblyOf(Type)](/Specular/pr-preview/pr-462/api/specular/iservicedescriptorassemblyselector/methods.md#dependenciesfromassemblyof-type) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-462/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will load and scan from given types assembly
- [EntryAssembly](/Specular/pr-preview/pr-462/api/specular/iservicedescriptorassemblyselector/methods.md#entryassembly) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-462/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will scan for types from this assembly at compile time.
- [FromAssemblies](/Specular/pr-preview/pr-462/api/specular/iservicedescriptorassemblyselector/methods.md#fromassemblies) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-462/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will scan for types from all metadata assembly at compile time.
- [FromAssemblyOf](/Specular/pr-preview/pr-462/api/specular/iservicedescriptorassemblyselector/methods.md#fromassemblyof) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-462/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will scan for types from the assembly of type `T`.
- [FromAssemblyOf(Type)](/Specular/pr-preview/pr-462/api/specular/iservicedescriptorassemblyselector/methods.md#fromassemblyof-type) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-462/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will scan for types from the assembly of type.
- [IncludeSystemAssemblies](/Specular/pr-preview/pr-462/api/specular/iservicedescriptorassemblyselector/methods.md#includesystemassemblies) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-462/api/specular/iservicedescriptortypeselector.md) `abstract` -- Include system assemblies
- [NotFromAssemblyOf](/Specular/pr-preview/pr-462/api/specular/iservicedescriptorassemblyselector/methods.md#notfromassemblyof) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-462/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will not scan for types from the assembly of type `T`.
- [NotFromAssemblyOf(Type)](/Specular/pr-preview/pr-462/api/specular/iservicedescriptorassemblyselector/methods.md#notfromassemblyof-type) : [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-462/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will not scan for types from the assembly of type.
