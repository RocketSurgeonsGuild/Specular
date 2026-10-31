# IReflectionTypeSelector

- Kind: `interface`
- Package: [Specular](/Specular/v0.4.0/api/specular.md)
- Version: `0.4.0`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Implements: [IReflectionAssemblySelector](/Specular/v0.4.0/api/specular/ireflectionassemblyselector.md)

The Compiled Implementation Type Selector

## Definition

```csharp
namespace Specular.Abstractions;

public interface IReflectionTypeSelector
    : Specular.Abstractions.IReflectionAssemblySelector
{
    // ...
}
```

## Methods

- [GetTypes](/Specular/v0.4.0/api/specular/ireflectiontypeselector/methods.md#gettypes) : `IEnumerable<Type>` `abstract` -- Lists all of the classes in a given assembly
- [GetTypes(bool)](/Specular/v0.4.0/api/specular/ireflectiontypeselector/methods.md#gettypes-bool) : `IEnumerable<Type>` `abstract` -- Lists all of the public classes in a given assembly
- [GetTypes(Action<ITypeFilter>)](/Specular/v0.4.0/api/specular/ireflectiontypeselector/methods.md#gettypes-action-itypefilter) : `IEnumerable<Type>` `abstract` -- Adds all public, non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.
- [GetTypes(bool, Action<ITypeFilter>)](/Specular/v0.4.0/api/specular/ireflectiontypeselector/methods.md#gettypes-bool-action-itypefilter) : `IEnumerable<Type>` `abstract` -- Adds all non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.
