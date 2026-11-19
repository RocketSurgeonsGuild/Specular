# GenericServiceB

- Kind: `class`
- Package: [TestAssembly](/Specular/api/testassembly.md)
- Version: `0.4.2-beta.52`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.8bcc62d19c09284f6923e54e588877e41baf4070/test/TestAssembly/GenericServiceB.cs)
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
