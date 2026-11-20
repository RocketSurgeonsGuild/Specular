# AssemblyScanReportEntry

- Kind: `record`
- Package: [Specular](/Specular/pr-preview/pr-447/api/specular.md)
- Version: `0.4.2-pr.66`
- Namespace: `Specular.Diagnostics`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/src/Specular/Diagnostics/ScanDiagnostics.cs)
- Implements: `IEquatable<AssemblyScanReportEntry>`

Identifies an assembly selected by an assembly scan expression.

## Definition

```csharp
namespace Specular.Diagnostics;

public sealed record AssemblyScanReportEntry
    : System.IEquatable<Specular.Diagnostics.AssemblyScanReportEntry>
{
    // ...
}
```

## Constructors

- [AssemblyScanReportEntry(string)](/Specular/pr-preview/pr-447/api/specular/assemblyscanreportentry/constructors.md#constructor-string) -- Identifies an assembly selected by an assembly scan expression.

## Properties

- [AssemblyName](/Specular/pr-preview/pr-447/api/specular/assemblyscanreportentry/properties.md#assemblyname) : `string` `get; init`

## Methods

- [<Clone>$](/Specular/pr-preview/pr-447/api/specular/assemblyscanreportentry/methods.md#clone) : [AssemblyScanReportEntry](/Specular/pr-preview/pr-447/api/specular/assemblyscanreportentry.md)
- [Deconstruct(string)](/Specular/pr-preview/pr-447/api/specular/assemblyscanreportentry/methods.md#deconstruct-string)
- [Equals(object?)](/Specular/pr-preview/pr-447/api/specular/assemblyscanreportentry/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(AssemblyScanReportEntry?)](/Specular/pr-preview/pr-447/api/specular/assemblyscanreportentry/methods.md#equals-assemblyscanreportentry) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/pr-preview/pr-447/api/specular/assemblyscanreportentry/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(AssemblyScanReportEntry?, AssemblyScanReportEntry?)](/Specular/pr-preview/pr-447/api/specular/assemblyscanreportentry/methods.md#op-equality-assemblyscanreportentry-assemblyscanreportentry) : `bool` `static`
- [op_Inequality(AssemblyScanReportEntry?, AssemblyScanReportEntry?)](/Specular/pr-preview/pr-447/api/specular/assemblyscanreportentry/methods.md#op-inequality-assemblyscanreportentry-assemblyscanreportentry) : `bool` `static`
- [ToString](/Specular/pr-preview/pr-447/api/specular/assemblyscanreportentry/methods.md#tostring) : `string` -- Returns a string that represents the current object.
