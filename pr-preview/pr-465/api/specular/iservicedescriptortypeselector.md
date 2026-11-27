# IServiceDescriptorTypeSelector

- Kind: `interface`
- Package: [Specular](/Specular/pr-preview/pr-465/api/specular.md)
- Version: `0.4.3-pr.20`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-465-merge.Sha.8017a748818d9c4a88a3d98a3d7bf52355eb148b/src/Specular/Abstractions/IServiceDescriptorTypeSelector.cs)
- Implements: [IServiceDescriptorAssemblySelector](/Specular/pr-preview/pr-465/api/specular/iservicedescriptorassemblyselector.md)

The Compiled Implementation Type Selector

## Definition

```csharp
namespace Specular.Abstractions;

public interface IServiceDescriptorTypeSelector
    : Specular.Abstractions.IServiceDescriptorAssemblySelector
{
    // ...
}
```

## Methods

- [AddClasses](/Specular/pr-preview/pr-465/api/specular/iservicedescriptortypeselector/methods.md#addclasses) : [IServiceTypeSelector](/Specular/pr-preview/pr-465/api/specular/iservicetypeselector.md) `abstract` -- Adds all public, non-abstract classes from the selected assemblies to the `DependencyInjection.IServiceCollection`.
- [AddClasses(bool)](/Specular/pr-preview/pr-465/api/specular/iservicedescriptortypeselector/methods.md#addclasses-bool) : [IServiceTypeSelector](/Specular/pr-preview/pr-465/api/specular/iservicetypeselector.md) `abstract` -- Adds all non-abstract classes from the selected assemblies to the `DependencyInjection.IServiceCollection`.
- [AddClasses(Action<ITypeFilter>)](/Specular/pr-preview/pr-465/api/specular/iservicedescriptortypeselector/methods.md#addclasses-action-itypefilter) : [IServiceTypeSelector](/Specular/pr-preview/pr-465/api/specular/iservicetypeselector.md) `abstract` -- Adds all public, non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.
- [AddClasses(Action<ITypeFilter>, bool)](/Specular/pr-preview/pr-465/api/specular/iservicedescriptortypeselector/methods.md#addclasses-action-itypefilter-bool) : [IServiceTypeSelector](/Specular/pr-preview/pr-465/api/specular/iservicetypeselector.md) `abstract` -- Adds all non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.
