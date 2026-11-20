# KeyedService

- Kind: `class`
- Package: [TestAssembly](/Specular/pr-preview/pr-443/api/testassembly.md)
- Version: `0.4.2-pr.62`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-443-merge.Sha.0ce6dafae22555d843d161383c929a8d23274722/test/TestAssembly/KeyedService.cs)
- Implements: [IKeyedService](/Specular/pr-preview/pr-443/api/testassembly/ikeyedservice.md)

Supports all classes in the .NET class hierarchy and provides low-level services to derived classes. This is the ultimate base class of all .NET classes; it is the root of the type hierarchy.

## Definition

```csharp
namespace TestAssembly;

public class KeyedService
    : TestAssembly.IKeyedService
{
    // ...
}
```

## Constructors

- [KeyedService](/Specular/pr-preview/pr-443/api/testassembly/keyedservice/constructors.md#constructor)
