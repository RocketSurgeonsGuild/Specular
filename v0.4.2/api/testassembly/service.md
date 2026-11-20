# Service

- Kind: `class`
- Package: [TestAssembly](/Specular/v0.4.2/api/testassembly.md)
- Version: `0.4.2`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.cfc4e1e69ff7a6dcb9388579cde1a5879d0e3e7e/test/TestAssembly/Service.cs)
- Implements: [IService](/Specular/v0.4.2/api/testassembly/iservice.md), [IServiceB](/Specular/v0.4.2/api/testassembly/iserviceb.md)

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

- [Service](/Specular/v0.4.2/api/testassembly/service/constructors.md#constructor)
