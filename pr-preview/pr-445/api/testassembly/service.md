# Service

- Kind: `class`
- Package: [TestAssembly](/Specular/pr-preview/pr-445/api/testassembly.md)
- Version: `0.4.2-pr.62`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-445-merge.Sha.66af3aafdf6ababbe05196f68854c137cd0b613e/test/TestAssembly/Service.cs)
- Implements: [IService](/Specular/pr-preview/pr-445/api/testassembly/iservice.md), [IServiceB](/Specular/pr-preview/pr-445/api/testassembly/iserviceb.md)

Supports all classes in the .NET class hierarchy and provides low-level services to derived classes. This is the ultimate base class of all .NET classes; it is the root of the type hierarchy.

## Definition

```csharp
namespace TestAssembly;

public class Service
    : TestAssembly.IService,
      TestAssembly.IServiceB
{
    // ...
}
```

## Constructors

- [Service](/Specular/pr-preview/pr-445/api/testassembly/service/constructors.md#constructor)
