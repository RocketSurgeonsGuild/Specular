# IServiceDescriptorTypeSelector

- Kind: `interface`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.3.3-beta.9`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Implements: [IServiceDescriptorAssemblySelector](/Specular/api/specular/iservicedescriptorassemblyselector.md)

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

- [AddClasses](/Specular/api/specular/iservicedescriptortypeselector/methods.md#addclasses) : [IServiceTypeSelector](/Specular/api/specular/iservicetypeselector.md) `abstract` -- Adds all public, non-abstract classes from the selected assemblies to the `DependencyInjection.IServiceCollection`.
- [AddClasses(bool)](/Specular/api/specular/iservicedescriptortypeselector/methods.md#addclasses-bool) : [IServiceTypeSelector](/Specular/api/specular/iservicetypeselector.md) `abstract` -- Adds all non-abstract classes from the selected assemblies to the `DependencyInjection.IServiceCollection`.
- [AddClasses(Action<ITypeFilter>)](/Specular/api/specular/iservicedescriptortypeselector/methods.md#addclasses-action-itypefilter) : [IServiceTypeSelector](/Specular/api/specular/iservicetypeselector.md) `abstract` -- Adds all public, non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.
- [AddClasses(Action<ITypeFilter>, bool)](/Specular/api/specular/iservicedescriptortypeselector/methods.md#addclasses-action-itypefilter-bool) : [IServiceTypeSelector](/Specular/api/specular/iservicetypeselector.md) `abstract` -- Adds all non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.
