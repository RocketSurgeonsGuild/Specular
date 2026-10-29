# TypeScanReport

- Kind: `record`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.3.3-beta.5`
- Namespace: `Specular.Diagnostics`
- Target framework: `net10.0`
- Implements: `IEquatable<TypeScanReport>`

Contains the result of a type scan expression.

## Definition

```csharp
namespace Specular.Diagnostics;

public sealed record TypeScanReport
    : System.IEquatable<Specular.Diagnostics.TypeScanReport>
{
    // ...
}
```

## Constructors

- [TypeScanReport(string, string, ImmutableList<TypeScanAssemblyReportEntry>)](/Specular/api/specular/typescanreport/constructors.md#constructor-string-string-immutablelist-typescanassemblyreportentry) -- Contains the result of a type scan expression.

## Properties

- [Entries](/Specular/api/specular/typescanreport/properties.md#entries) : `ImmutableList<TypeScanAssemblyReportEntry>` `get; init`
- [SourceAssemblyName](/Specular/api/specular/typescanreport/properties.md#sourceassemblyname) : `string` `get; init`
- [SourceExpression](/Specular/api/specular/typescanreport/properties.md#sourceexpression) : `string` `get; init`

## Methods

- [<Clone>$](/Specular/api/specular/typescanreport/methods.md#clone) : [TypeScanReport](/Specular/api/specular/typescanreport.md)
- [Deconstruct(string, string, ImmutableList<TypeScanAssemblyReportEntry>)](/Specular/api/specular/typescanreport/methods.md#deconstruct-string-string-immutablelist-typescanassemblyreportentry)
- [Equals(object?)](/Specular/api/specular/typescanreport/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(TypeScanReport?)](/Specular/api/specular/typescanreport/methods.md#equals-typescanreport) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/api/specular/typescanreport/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(TypeScanReport?, TypeScanReport?)](/Specular/api/specular/typescanreport/methods.md#op-equality-typescanreport-typescanreport) : `bool` `static`
- [op_Inequality(TypeScanReport?, TypeScanReport?)](/Specular/api/specular/typescanreport/methods.md#op-inequality-typescanreport-typescanreport) : `bool` `static`
- [ToString](/Specular/api/specular/typescanreport/methods.md#tostring) : `string` -- Returns a string that represents the current object.
