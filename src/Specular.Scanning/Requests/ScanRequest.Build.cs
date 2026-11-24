using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

// Stub: replaced by the selector implementation (Phase 1).
public partial record ScanRequest
{
    private static partial ScanRequest BuildCore(Compilation compilation, Action<IAssemblySymbolSelector> select) => throw new NotImplementedException();

    private partial string GetCacheKeyCore() => throw new NotImplementedException();
}
