# Service

- Kind: `class`
- Package: [TestAssembly](/Specular/pr-preview/pr-406/api/testassembly.md)
- Version: `0.4.2-pr.52`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-406-merge.Sha.a5bb8c31ae5b3055ce3bbac8a150aa5acd0acd0a/test/TestAssembly/Service.cs)
- Implements: [IService](/Specular/pr-preview/pr-406/api/testassembly/iservice.md), [IServiceB](/Specular/pr-preview/pr-406/api/testassembly/iserviceb.md)

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

- [Service](/Specular/pr-preview/pr-406/api/testassembly/service/constructors.md#constructor)
