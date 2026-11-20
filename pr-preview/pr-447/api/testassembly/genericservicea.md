# GenericServiceA

- Kind: `class`
- Package: [TestAssembly](/Specular/pr-preview/pr-447/api/testassembly.md)
- Version: `0.4.2-pr.66`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/test/TestAssembly/Nested.cs)
- Implements: `IGenericService<string>`, [IOther](/Specular/pr-preview/pr-447/api/testassembly/iother.md)

Supports all classes in the .NET class hierarchy and provides low-level services to derived classes. This is the ultimate base class of all .NET classes; it is the root of the type hierarchy.

## Definition

```csharp
namespace TestAssembly;

public class GenericServiceA
    : TestAssembly.IGenericService<string>,
      TestAssembly.IOther
{
    // ...
}
```

## Constructors

- [GenericServiceA](/Specular/pr-preview/pr-447/api/testassembly/genericservicea/constructors.md#constructor)
