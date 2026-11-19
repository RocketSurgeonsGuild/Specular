# GenericServiceA

- Kind: `class`
- Package: [TestAssembly](/Specular/pr-preview/pr-441/api/testassembly.md)
- Version: `0.4.2-pr.59`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-441-merge.Sha.483f529455000ea72a283b9f607726cf95ec03ef/test/TestAssembly/Nested.cs)
- Implements: `IGenericService<string>`, [IOther](/Specular/pr-preview/pr-441/api/testassembly/iother.md)

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

- [GenericServiceA](/Specular/pr-preview/pr-441/api/testassembly/genericservicea/constructors.md#constructor)
