# ISpecularProvider

- Kind: `interface`
- Package: [Specular](/Specular/pr-preview/pr-453/api/specular.md)
- Version: `0.4.3-pr.13`
- Namespace: `Specular`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-453-merge.Sha.e15ed429f3bf41f14f92fb73273b178497785fb7/src/Specular/ISpecularProvider.cs)

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

- [GetAssemblies(Action<IReflectionAssemblySelector>, int, string, string)](/Specular/pr-preview/pr-453/api/specular/ispecularprovider/methods.md#getassemblies-action-ireflectionassemblyselector-int-string-string) : `IEnumerable<Assembly>` `abstract` -- Get the full list of assemblies
- [GetTypes(Func<IReflectionTypeSelector, IEnumerable<Type>>, int, string, string)](/Specular/pr-preview/pr-453/api/specular/ispecularprovider/methods.md#gettypes-func-ireflectiontypeselector-ienumerable-type-int-string-string) : `IEnumerable<Type>` `abstract` -- Get the full list of types using the given selector
- [Scan(IServiceCollection, Action<IServiceDescriptorAssemblySelector>, int, string, string)](/Specular/pr-preview/pr-453/api/specular/ispecularprovider/methods.md#scan-iservicecollection-action-iservicedescriptorassemblyselector-int-string-string) : `IServiceCollection` `abstract` -- Scan for types using the given selector
