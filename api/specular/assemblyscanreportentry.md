# AssemblyScanReportEntry

- Kind: `record`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.3.3-beta.10`
- Namespace: `Specular.Diagnostics`
- Target framework: `net10.0`
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

- [AssemblyScanReportEntry(string)](/Specular/api/specular/assemblyscanreportentry/constructors.md#constructor-string) -- Identifies an assembly selected by an assembly scan expression.

## Properties

- [AssemblyName](/Specular/api/specular/assemblyscanreportentry/properties.md#assemblyname) : `string` `get; init`

## Methods

- [<Clone>$](/Specular/api/specular/assemblyscanreportentry/methods.md#clone) : [AssemblyScanReportEntry](/Specular/api/specular/assemblyscanreportentry.md)
- [Deconstruct(string)](/Specular/api/specular/assemblyscanreportentry/methods.md#deconstruct-string)
- [Equals(object?)](/Specular/api/specular/assemblyscanreportentry/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(AssemblyScanReportEntry?)](/Specular/api/specular/assemblyscanreportentry/methods.md#equals-assemblyscanreportentry) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/api/specular/assemblyscanreportentry/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(AssemblyScanReportEntry?, AssemblyScanReportEntry?)](/Specular/api/specular/assemblyscanreportentry/methods.md#op-equality-assemblyscanreportentry-assemblyscanreportentry) : `bool` `static`
- [op_Inequality(AssemblyScanReportEntry?, AssemblyScanReportEntry?)](/Specular/api/specular/assemblyscanreportentry/methods.md#op-inequality-assemblyscanreportentry-assemblyscanreportentry) : `bool` `static`
- [ToString](/Specular/api/specular/assemblyscanreportentry/methods.md#tostring) : `string` -- Returns a string that represents the current object.
