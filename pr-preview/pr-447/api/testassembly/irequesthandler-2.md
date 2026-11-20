# IRequestHandler<T, R>

- Kind: `interface`
- Package: [TestAssembly](/Specular/pr-preview/pr-447/api/testassembly.md)
- Version: `0.4.2-pr.66`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/test/TestAssembly/IRequestHandler.cs)

## Definition

```csharp
namespace TestAssembly;

public interface IRequestHandler<T, R>
    where T : IRequest<R>
{
    // ...
}
```
