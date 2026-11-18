# GenericServiceB

- Kind: `class`
- Package: [TestAssembly](/Specular/api/testassembly.md)
- Version: `0.4.2-beta.50`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.d4a702693c72a74c7d2f15946a641a7a58285f09/test/TestAssembly/GenericServiceB.cs)
- Implements: `IGenericService<decimal>`, [IOther](/Specular/api/testassembly/iother.md)

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

- [GenericServiceB](/Specular/api/testassembly/genericserviceb/constructors.md#constructor)
