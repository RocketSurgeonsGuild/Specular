# ScanResults

- Kind: `record`
- Package: [Specular](/Specular/pr-preview/pr-446/api/specular.md)
- Version: `0.4.2-pr.62`
- Namespace: `Specular.Diagnostics`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-446-merge.Sha.bdccb36ec194143897302d3d96fd4fa575d90fa8/src/Specular/Diagnostics/ScanDiagnostics.cs)
- Implements: `IEquatable<ScanResults>`

Contains the compile-time results for all Specular scans in an assembly.

## Definition

```csharp
namespace Specular.Diagnostics;

public sealed record ScanResults
    : System.IEquatable<Specular.Diagnostics.ScanResults>
{
    // ...
}
```

## Constructors

- [ScanResults(ImmutableList<AssemblyScanReport>, ImmutableList<TypeScanReport>, ImmutableList<ServiceDescriptorScanReport>)](/Specular/pr-preview/pr-446/api/specular/scanresults/constructors.md#constructor-immutablelist-assemblyscanreport-immutablelist-typescanreport-immutablelist-servicedescriptorscanreport) -- Contains the compile-time results for all Specular scans in an assembly.

## Properties

- [AssemblyScanReports](/Specular/pr-preview/pr-446/api/specular/scanresults/properties.md#assemblyscanreports) : `ImmutableList<AssemblyScanReport>` `get; init`
- [ServiceDescriptorScanReports](/Specular/pr-preview/pr-446/api/specular/scanresults/properties.md#servicedescriptorscanreports) : `ImmutableList<ServiceDescriptorScanReport>` `get; init`
- [TypeScanReports](/Specular/pr-preview/pr-446/api/specular/scanresults/properties.md#typescanreports) : `ImmutableList<TypeScanReport>` `get; init`

## Methods

- [<Clone>$](/Specular/pr-preview/pr-446/api/specular/scanresults/methods.md#clone) : [ScanResults](/Specular/pr-preview/pr-446/api/specular/scanresults.md)
- [Deconstruct(ImmutableList<AssemblyScanReport>, ImmutableList<TypeScanReport>, ImmutableList<ServiceDescriptorScanReport>)](/Specular/pr-preview/pr-446/api/specular/scanresults/methods.md#deconstruct-immutablelist-assemblyscanreport-immutablelist-typescanreport-immutablelist-servicedescriptorscanreport)
- [Equals(object?)](/Specular/pr-preview/pr-446/api/specular/scanresults/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(ScanResults?)](/Specular/pr-preview/pr-446/api/specular/scanresults/methods.md#equals-scanresults) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/pr-preview/pr-446/api/specular/scanresults/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(ScanResults?, ScanResults?)](/Specular/pr-preview/pr-446/api/specular/scanresults/methods.md#op-equality-scanresults-scanresults) : `bool` `static`
- [op_Inequality(ScanResults?, ScanResults?)](/Specular/pr-preview/pr-446/api/specular/scanresults/methods.md#op-inequality-scanresults-scanresults) : `bool` `static`
- [ToString](/Specular/pr-preview/pr-446/api/specular/scanresults/methods.md#tostring) : `string` -- Returns a string that represents the current object.
