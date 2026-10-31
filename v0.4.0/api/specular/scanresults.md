# ScanResults

- Kind: `record`
- Package: [Specular](/Specular/v0.4.0/api/specular.md)
- Version: `0.4.0`
- Namespace: `Specular.Diagnostics`
- Target framework: `net10.0`
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

- [ScanResults(ImmutableList<AssemblyScanReport>, ImmutableList<TypeScanReport>, ImmutableList<ServiceDescriptorScanReport>)](/Specular/v0.4.0/api/specular/scanresults/constructors.md#constructor-immutablelist-assemblyscanreport-immutablelist-typescanreport-immutablelist-servicedescriptorscanreport) -- Contains the compile-time results for all Specular scans in an assembly.

## Properties

- [AssemblyScanReports](/Specular/v0.4.0/api/specular/scanresults/properties.md#assemblyscanreports) : `ImmutableList<AssemblyScanReport>` `get; init`
- [ServiceDescriptorScanReports](/Specular/v0.4.0/api/specular/scanresults/properties.md#servicedescriptorscanreports) : `ImmutableList<ServiceDescriptorScanReport>` `get; init`
- [TypeScanReports](/Specular/v0.4.0/api/specular/scanresults/properties.md#typescanreports) : `ImmutableList<TypeScanReport>` `get; init`

## Methods

- [<Clone>$](/Specular/v0.4.0/api/specular/scanresults/methods.md#clone) : [ScanResults](/Specular/v0.4.0/api/specular/scanresults.md)
- [Deconstruct(ImmutableList<AssemblyScanReport>, ImmutableList<TypeScanReport>, ImmutableList<ServiceDescriptorScanReport>)](/Specular/v0.4.0/api/specular/scanresults/methods.md#deconstruct-immutablelist-assemblyscanreport-immutablelist-typescanreport-immutablelist-servicedescriptorscanreport)
- [Equals(object?)](/Specular/v0.4.0/api/specular/scanresults/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(ScanResults?)](/Specular/v0.4.0/api/specular/scanresults/methods.md#equals-scanresults) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/v0.4.0/api/specular/scanresults/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(ScanResults?, ScanResults?)](/Specular/v0.4.0/api/specular/scanresults/methods.md#op-equality-scanresults-scanresults) : `bool` `static`
- [op_Inequality(ScanResults?, ScanResults?)](/Specular/v0.4.0/api/specular/scanresults/methods.md#op-inequality-scanresults-scanresults) : `bool` `static`
- [ToString](/Specular/v0.4.0/api/specular/scanresults/methods.md#tostring) : `string` -- Returns a string that represents the current object.
