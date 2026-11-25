using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     The scanner's per-assembly cache seam. The scanner asks it about referenced assemblies only; the implementation decides which
///     assemblies are cacheable (only references with a readable MVID) and validates entries against the MVID.
/// </summary>
internal interface IAssemblyScanCache
{
    /// <summary>The types <paramref name="request" /> matched in <paramref name="assembly" /> on an earlier build, if still valid.</summary>
    bool TryGetTypes(ScanRequest request, IAssemblySymbol assembly, out EquatableArray<ScannedType> types);

    /// <summary>Records the types <paramref name="request" /> matched in <paramref name="assembly" />.</summary>
    void SetTypes(ScanRequest request, IAssemblySymbol assembly, EquatableArray<ScannedType> types);
}
