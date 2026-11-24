using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

// Stub: replaced by the scanner core (Phase 2/3).
public partial class Scanner
{
    private static partial ScanResult ScanCore(Compilation compilation, ScanRequest request, IAssemblyScanCache? cache, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
