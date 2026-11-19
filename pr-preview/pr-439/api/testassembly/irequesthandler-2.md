# IRequestHandler<T, R>

- Kind: `interface`
- Package: [TestAssembly](/Specular/pr-preview/pr-439/api/testassembly.md)
- Version: `0.4.2-pr.54`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-439-merge.Sha.a00a386a02665437a1eaba7e40471f7bf42b5822/test/TestAssembly/IRequestHandler.cs)

## Definition

```csharp
namespace TestAssembly;

public interface IRequestHandler<T, R>
    where T : IRequest<R>
{
    // ...
}
```
