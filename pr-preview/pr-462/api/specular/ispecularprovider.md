# ISpecularProvider

- Kind: `interface`
- Package: [Specular](/Specular/pr-preview/pr-462/api/specular.md)
- Version: `0.4.3-pr.15`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-462-merge.Sha.65577d9b63559d60a65f5887b78a6f971676b065/src/Specular/ISpecularProvider.cs)

A provider that gets a list of assemblies for a given context

## Definition

```csharp
namespace Specular;

public interface ISpecularProvider
{
    // ...
}
```

## Methods

- [GetAssemblies(Action<IReflectionAssemblySelector>, int, string, string)](/Specular/pr-preview/pr-462/api/specular/ispecularprovider/methods.md#getassemblies-action-ireflectionassemblyselector-int-string-string) : `IEnumerable<Assembly>` `abstract` -- Get the full list of assemblies
- [GetTypes(Func<IReflectionTypeSelector, IEnumerable<Type>>, int, string, string)](/Specular/pr-preview/pr-462/api/specular/ispecularprovider/methods.md#gettypes-func-ireflectiontypeselector-ienumerable-type-int-string-string) : `IEnumerable<Type>` `abstract` -- Get the full list of types using the given selector
- [Scan(IServiceCollection, Action<IServiceDescriptorAssemblySelector>, int, string, string)](/Specular/pr-preview/pr-462/api/specular/ispecularprovider/methods.md#scan-iservicecollection-action-iservicedescriptorassemblyselector-int-string-string) : `IServiceCollection` `abstract` -- Scan for types using the given selector
