# IRequestHandler<T, R>

- Kind: `interface`
- Package: [TestAssembly](/Specular/api/testassembly.md)
- Version: `0.4.2-beta.50`
- Namespace: `TestAssembly`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.d4a702693c72a74c7d2f15946a641a7a58285f09/test/TestAssembly/IRequestHandler.cs)

## Definition

```csharp
namespace TestAssembly;

public interface IRequestHandler<T, R>
    where T : IRequest<R>
{
    // ...
}
```
