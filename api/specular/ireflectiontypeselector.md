# IReflectionTypeSelector

- Kind: `interface`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.3-beta.8`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.4fa7b6a1dfdd2b88d9fb2eee7f65e035b74e1c7e/src/Specular/Abstractions/IReflectionTypeSelector.cs)
- Implements: [IReflectionAssemblySelector](/Specular/api/specular/ireflectionassemblyselector.md)

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

- [GetTypes](/Specular/api/specular/ireflectiontypeselector/methods.md#gettypes) : `IEnumerable<Type>` `abstract` -- Lists all of the classes in a given assembly
- [GetTypes(bool)](/Specular/api/specular/ireflectiontypeselector/methods.md#gettypes-bool) : `IEnumerable<Type>` `abstract` -- Lists all of the public classes in a given assembly
- [GetTypes(Action<ITypeFilter>)](/Specular/api/specular/ireflectiontypeselector/methods.md#gettypes-action-itypefilter) : `IEnumerable<Type>` `abstract` -- Adds all public, non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.
- [GetTypes(bool, Action<ITypeFilter>)](/Specular/api/specular/ireflectiontypeselector/methods.md#gettypes-bool-action-itypefilter) : `IEnumerable<Type>` `abstract` -- Adds all non-abstract classes from the selected assemblies that matches the requirements specified in the `action` to the `DependencyInjection.IServiceCollection`.
