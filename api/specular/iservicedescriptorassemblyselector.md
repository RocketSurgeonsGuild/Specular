# IServiceDescriptorAssemblySelector

- Kind: `interface`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.1-beta.1`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`

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

- [DependenciesFromAssemblyOf](/Specular/api/specular/iservicedescriptorassemblyselector/methods.md#dependenciesfromassemblyof) : [IServiceDescriptorTypeSelector](/Specular/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will load and scan from given types assembly
- [DependenciesFromAssemblyOf(Type)](/Specular/api/specular/iservicedescriptorassemblyselector/methods.md#dependenciesfromassemblyof-type) : [IServiceDescriptorTypeSelector](/Specular/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will load and scan from given types assembly
- [EntryAssembly](/Specular/api/specular/iservicedescriptorassemblyselector/methods.md#entryassembly) : [IServiceDescriptorTypeSelector](/Specular/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will scan for types from this assembly at compile time.
- [FromAssemblies](/Specular/api/specular/iservicedescriptorassemblyselector/methods.md#fromassemblies) : [IServiceDescriptorTypeSelector](/Specular/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will scan for types from all metadata assembly at compile time.
- [FromAssemblyOf](/Specular/api/specular/iservicedescriptorassemblyselector/methods.md#fromassemblyof) : [IServiceDescriptorTypeSelector](/Specular/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will scan for types from the assembly of type `T`.
- [FromAssemblyOf(Type)](/Specular/api/specular/iservicedescriptorassemblyselector/methods.md#fromassemblyof-type) : [IServiceDescriptorTypeSelector](/Specular/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will scan for types from the assembly of type.
- [IncludeSystemAssemblies](/Specular/api/specular/iservicedescriptorassemblyselector/methods.md#includesystemassemblies) : [IServiceDescriptorTypeSelector](/Specular/api/specular/iservicedescriptortypeselector.md) `abstract` -- Include system assemblies
- [NotFromAssemblyOf](/Specular/api/specular/iservicedescriptorassemblyselector/methods.md#notfromassemblyof) : [IServiceDescriptorTypeSelector](/Specular/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will not scan for types from the assembly of type `T`.
- [NotFromAssemblyOf(Type)](/Specular/api/specular/iservicedescriptorassemblyselector/methods.md#notfromassemblyof-type) : [IServiceDescriptorTypeSelector](/Specular/api/specular/iservicedescriptortypeselector.md) `abstract` -- Will not scan for types from the assembly of type.
