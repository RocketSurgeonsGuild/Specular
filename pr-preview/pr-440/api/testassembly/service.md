# Service

- Kind: `class`
- Package: [TestAssembly](/Specular/pr-preview/pr-440/api/testassembly.md)
- Version: `0.4.2-pr.59`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-440-merge.Sha.6c4e416f4e1851a29f25623ea42542836d5144d2/test/TestAssembly/Service.cs)
- Implements: [IService](/Specular/pr-preview/pr-440/api/testassembly/iservice.md), [IServiceB](/Specular/pr-preview/pr-440/api/testassembly/iserviceb.md)

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

- [Service](/Specular/pr-preview/pr-440/api/testassembly/service/constructors.md#constructor)
