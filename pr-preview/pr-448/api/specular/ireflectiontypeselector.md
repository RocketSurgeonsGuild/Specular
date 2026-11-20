# IReflectionTypeSelector

- Kind: `interface`
- Package: [Specular](/Specular/pr-preview/pr-448/api/specular.md)
- Version: `0.4.3-pr.2`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-448-merge.Sha.69052bf45a1b3de253f1409be1700c8f80800784/src/Specular/Abstractions/IReflectionTypeSelector.cs)
- Implements: [IReflectionAssemblySelector](/Specular/pr-preview/pr-448/api/specular/ireflectionassemblyselector.md)

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

- [GetTypes](/Specular/pr-preview/pr-448/api/specular/ireflectiontypeselector/methods.md#gettypes) : `IEnumerable<Type>` `abstract` -- Lists all of the classes in a given assembly
- [GetTypes(bool)](/Specular/pr-preview/pr-448/api/specular/ireflectiontypeselector/methods.md#gettypes-bool) : `IEnumerable<Type>` `abstract` -- Lists all of the public classes in a given assembly
- [GetTypes(Action<ITypeFilter>)](/Specular/pr-preview/pr-448/api/specular/ireflectiontypeselector/methods.md#gettypes-action-itypefilter) : `IEnumerable<Type>` `abstract` -- Adds all public, non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.
- [GetTypes(bool, Action<ITypeFilter>)](/Specular/pr-preview/pr-448/api/specular/ireflectiontypeselector/methods.md#gettypes-bool-action-itypefilter) : `IEnumerable<Type>` `abstract` -- Adds all non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.
