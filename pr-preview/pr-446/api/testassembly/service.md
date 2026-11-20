# Service

- Kind: `class`
- Package: [TestAssembly](/Specular/pr-preview/pr-446/api/testassembly.md)
- Version: `0.4.2-pr.62`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-446-merge.Sha.bdccb36ec194143897302d3d96fd4fa575d90fa8/test/TestAssembly/Service.cs)
- Implements: [IService](/Specular/pr-preview/pr-446/api/testassembly/iservice.md), [IServiceB](/Specular/pr-preview/pr-446/api/testassembly/iserviceb.md)

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

- [Service](/Specular/pr-preview/pr-446/api/testassembly/service/constructors.md#constructor)
