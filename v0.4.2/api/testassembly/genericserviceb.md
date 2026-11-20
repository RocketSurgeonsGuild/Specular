# GenericServiceB

- Kind: `class`
- Package: [TestAssembly](/Specular/v0.4.2/api/testassembly.md)
- Version: `0.4.2`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.cfc4e1e69ff7a6dcb9388579cde1a5879d0e3e7e/test/TestAssembly/GenericServiceB.cs)
- Implements: `IGenericService<decimal>`, [IOther](/Specular/v0.4.2/api/testassembly/iother.md)

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

- [GenericServiceB](/Specular/v0.4.2/api/testassembly/genericserviceb/constructors.md#constructor)
