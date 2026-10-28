# IServiceDescriptorTypeSelector

- Kind: `interface`
- Package: [Specular](/Specular/v0.3.1/api/specular.md)
- Version: `0.3.1`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Implements: [IServiceDescriptorAssemblySelector](/Specular/v0.3.1/api/specular/iservicedescriptorassemblyselector.md)

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

- [AddClasses](/Specular/v0.3.1/api/specular/iservicedescriptortypeselector/methods.md#addclasses) : [IServiceTypeSelector](/Specular/v0.3.1/api/specular/iservicetypeselector.md) `abstract` -- Adds all public, non-abstract classes from the selected assemblies to the `DependencyInjection.IServiceCollection`.
- [AddClasses(bool)](/Specular/v0.3.1/api/specular/iservicedescriptortypeselector/methods.md#addclasses-bool) : [IServiceTypeSelector](/Specular/v0.3.1/api/specular/iservicetypeselector.md) `abstract` -- Adds all non-abstract classes from the selected assemblies to the `DependencyInjection.IServiceCollection`.
- [AddClasses(Action<ITypeFilter>)](/Specular/v0.3.1/api/specular/iservicedescriptortypeselector/methods.md#addclasses-action-itypefilter) : [IServiceTypeSelector](/Specular/v0.3.1/api/specular/iservicetypeselector.md) `abstract` -- Adds all public, non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.
- [AddClasses(Action<ITypeFilter>, bool)](/Specular/v0.3.1/api/specular/iservicedescriptortypeselector/methods.md#addclasses-action-itypefilter-bool) : [IServiceTypeSelector](/Specular/v0.3.1/api/specular/iservicetypeselector.md) `abstract` -- Adds all non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.
