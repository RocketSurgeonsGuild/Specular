# IReflectionTypeSelector

- Kind: `interface`
- Package: [Specular](/Specular/pr-preview/pr-462/api/specular.md)
- Version: `0.4.3-pr.15`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-462-merge.Sha.65577d9b63559d60a65f5887b78a6f971676b065/src/Specular/Abstractions/IReflectionTypeSelector.cs)
- Implements: [IReflectionAssemblySelector](/Specular/pr-preview/pr-462/api/specular/ireflectionassemblyselector.md)

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

- [GetTypes](/Specular/pr-preview/pr-462/api/specular/ireflectiontypeselector/methods.md#gettypes) : `IEnumerable<Type>` `abstract` -- Lists all of the classes in a given assembly
- [GetTypes(bool)](/Specular/pr-preview/pr-462/api/specular/ireflectiontypeselector/methods.md#gettypes-bool) : `IEnumerable<Type>` `abstract` -- Lists all of the public classes in a given assembly
- [GetTypes(Action<ITypeFilter>)](/Specular/pr-preview/pr-462/api/specular/ireflectiontypeselector/methods.md#gettypes-action-itypefilter) : `IEnumerable<Type>` `abstract` -- Adds all public, non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.
- [GetTypes(bool, Action<ITypeFilter>)](/Specular/pr-preview/pr-462/api/specular/ireflectiontypeselector/methods.md#gettypes-bool-action-itypefilter) : `IEnumerable<Type>` `abstract` -- Adds all non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.
