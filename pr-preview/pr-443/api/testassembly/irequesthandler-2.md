# IRequestHandler<T, R>

- Kind: `interface`
- Package: [TestAssembly](/Specular/pr-preview/pr-443/api/testassembly.md)
- Version: `0.4.2-pr.62`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-443-merge.Sha.0ce6dafae22555d843d161383c929a8d23274722/test/TestAssembly/IRequestHandler.cs)

## Definition

```csharp
namespace TestAssembly;

public interface IRequestHandler<T, R>
    where T : IRequest<R>
{
    // ...
}
```
