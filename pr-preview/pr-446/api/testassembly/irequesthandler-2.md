# IRequestHandler<T, R>

- Kind: `interface`
- Package: [TestAssembly](/Specular/pr-preview/pr-446/api/testassembly.md)
- Version: `0.4.2-pr.62`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-446-merge.Sha.bdccb36ec194143897302d3d96fd4fa575d90fa8/test/TestAssembly/IRequestHandler.cs)

## Definition

```csharp
namespace TestAssembly;

public interface IRequestHandler<T, R>
    where T : IRequest<R>
{
    // ...
}
```
