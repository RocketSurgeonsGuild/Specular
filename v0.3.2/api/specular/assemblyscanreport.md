# AssemblyScanReport

- Kind: `record`
- Package: [Specular](/Specular/v0.3.2/api/specular.md)
- Version: `0.3.2`
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

- [AssemblyScanReport(string, string, ImmutableList<AssemblyScanReportEntry>)](/Specular/v0.3.2/api/specular/assemblyscanreport/constructors.md#constructor-string-string-immutablelist-assemblyscanreportentry) -- Contains the result of an assembly scan expression.

## Properties

- [Entries](/Specular/v0.3.2/api/specular/assemblyscanreport/properties.md#entries) : `ImmutableList<AssemblyScanReportEntry>` `get; init`
- [SourceAssemblyName](/Specular/v0.3.2/api/specular/assemblyscanreport/properties.md#sourceassemblyname) : `string` `get; init`
- [SourceExpression](/Specular/v0.3.2/api/specular/assemblyscanreport/properties.md#sourceexpression) : `string` `get; init`

## Methods

- [<Clone>$](/Specular/v0.3.2/api/specular/assemblyscanreport/methods.md#clone) : [AssemblyScanReport](/Specular/v0.3.2/api/specular/assemblyscanreport.md)
- [Deconstruct(string, string, ImmutableList<AssemblyScanReportEntry>)](/Specular/v0.3.2/api/specular/assemblyscanreport/methods.md#deconstruct-string-string-immutablelist-assemblyscanreportentry)
- [Equals(object?)](/Specular/v0.3.2/api/specular/assemblyscanreport/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(AssemblyScanReport?)](/Specular/v0.3.2/api/specular/assemblyscanreport/methods.md#equals-assemblyscanreport) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/v0.3.2/api/specular/assemblyscanreport/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(AssemblyScanReport?, AssemblyScanReport?)](/Specular/v0.3.2/api/specular/assemblyscanreport/methods.md#op-equality-assemblyscanreport-assemblyscanreport) : `bool` `static`
- [op_Inequality(AssemblyScanReport?, AssemblyScanReport?)](/Specular/v0.3.2/api/specular/assemblyscanreport/methods.md#op-inequality-assemblyscanreport-assemblyscanreport) : `bool` `static`
- [ToString](/Specular/v0.3.2/api/specular/assemblyscanreport/methods.md#tostring) : `string` -- Returns a string that represents the current object.
