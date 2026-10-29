# AssemblyScanReport

- Kind: `record`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.3.3-beta.6`
- Namespace: `Specular.Diagnostics`
- Target framework: `net10.0`
- Implements: `IEquatable<AssemblyScanReport>`

Contains the result of an assembly scan expression.

## Definition

```csharp
namespace Specular.Diagnostics;

public sealed record AssemblyScanReport
    : System.IEquatable<Specular.Diagnostics.AssemblyScanReport>
{
    // ...
}
```

## Constructors

- [AssemblyScanReport(string, string, ImmutableList<AssemblyScanReportEntry>)](/Specular/api/specular/assemblyscanreport/constructors.md#constructor-string-string-immutablelist-assemblyscanreportentry) -- Contains the result of an assembly scan expression.

## Properties

- [Entries](/Specular/api/specular/assemblyscanreport/properties.md#entries) : `ImmutableList<AssemblyScanReportEntry>` `get; init`
- [SourceAssemblyName](/Specular/api/specular/assemblyscanreport/properties.md#sourceassemblyname) : `string` `get; init`
- [SourceExpression](/Specular/api/specular/assemblyscanreport/properties.md#sourceexpression) : `string` `get; init`

## Methods

- [<Clone>$](/Specular/api/specular/assemblyscanreport/methods.md#clone) : [AssemblyScanReport](/Specular/api/specular/assemblyscanreport.md)
- [Deconstruct(string, string, ImmutableList<AssemblyScanReportEntry>)](/Specular/api/specular/assemblyscanreport/methods.md#deconstruct-string-string-immutablelist-assemblyscanreportentry)
- [Equals(object?)](/Specular/api/specular/assemblyscanreport/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(AssemblyScanReport?)](/Specular/api/specular/assemblyscanreport/methods.md#equals-assemblyscanreport) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/api/specular/assemblyscanreport/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(AssemblyScanReport?, AssemblyScanReport?)](/Specular/api/specular/assemblyscanreport/methods.md#op-equality-assemblyscanreport-assemblyscanreport) : `bool` `static`
- [op_Inequality(AssemblyScanReport?, AssemblyScanReport?)](/Specular/api/specular/assemblyscanreport/methods.md#op-inequality-assemblyscanreport-assemblyscanreport) : `bool` `static`
- [ToString](/Specular/api/specular/assemblyscanreport/methods.md#tostring) : `string` -- Returns a string that represents the current object.
