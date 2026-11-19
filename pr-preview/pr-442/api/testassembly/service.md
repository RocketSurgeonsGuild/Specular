# Service

- Kind: `class`
- Package: [TestAssembly](/Specular/pr-preview/pr-442/api/testassembly.md)
- Version: `0.4.2-pr.59`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-442-merge.Sha.dca6b2e19ff475e66bd255b81efe7b8c43df4286/test/TestAssembly/Service.cs)
- Implements: [IService](/Specular/pr-preview/pr-442/api/testassembly/iservice.md), [IServiceB](/Specular/pr-preview/pr-442/api/testassembly/iserviceb.md)

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

- [Service](/Specular/pr-preview/pr-442/api/testassembly/service/constructors.md#constructor)
