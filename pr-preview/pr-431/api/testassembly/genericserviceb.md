# GenericServiceB

- Kind: `class`
- Package: [TestAssembly](/Specular/pr-preview/pr-431/api/testassembly.md)
- Version: `0.4.2-pr.55`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-431-merge.Sha.1fc516ea1b9500fed12a71dad9d2c95bf1783a47/test/TestAssembly/GenericServiceB.cs)
- Implements: `IGenericService<decimal>`, [IOther](/Specular/pr-preview/pr-431/api/testassembly/iother.md)

Supports all classes in the .NET class hierarchy and provides low-level services to derived classes. This is the ultimate base class of all .NET classes; it is the root of the type hierarchy.

## Definition

```csharp
namespace TestAssembly;

public class GenericServiceB
    : TestAssembly.IGenericService<decimal>,
      TestAssembly.IOther
{
    // ...
}
```

## Constructors

- [GenericServiceB](/Specular/pr-preview/pr-431/api/testassembly/genericserviceb/constructors.md#constructor)
