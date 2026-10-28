# TypeScanReport

- Kind: `record`
- Package: [Specular](/Specular/pr-preview/pr-339/api/specular.md)
- Version: `0.3.2-pr.2`
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

- [TypeScanReport(string, string, ImmutableList<TypeScanAssemblyReportEntry>)](/Specular/pr-preview/pr-339/api/specular/typescanreport/constructors.md#constructor-string-string-immutablelist-typescanassemblyreportentry) -- Contains the result of a type scan expression.

## Properties

- [Entries](/Specular/pr-preview/pr-339/api/specular/typescanreport/properties.md#entries) : `ImmutableList<TypeScanAssemblyReportEntry>` `get; init`
- [SourceAssemblyName](/Specular/pr-preview/pr-339/api/specular/typescanreport/properties.md#sourceassemblyname) : `string` `get; init`
- [SourceExpression](/Specular/pr-preview/pr-339/api/specular/typescanreport/properties.md#sourceexpression) : `string` `get; init`

## Methods

- [<Clone>$](/Specular/pr-preview/pr-339/api/specular/typescanreport/methods.md#clone) : [TypeScanReport](/Specular/pr-preview/pr-339/api/specular/typescanreport.md)
- [Deconstruct(string, string, ImmutableList<TypeScanAssemblyReportEntry>)](/Specular/pr-preview/pr-339/api/specular/typescanreport/methods.md#deconstruct-string-string-immutablelist-typescanassemblyreportentry)
- [Equals(object?)](/Specular/pr-preview/pr-339/api/specular/typescanreport/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(TypeScanReport?)](/Specular/pr-preview/pr-339/api/specular/typescanreport/methods.md#equals-typescanreport) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/pr-preview/pr-339/api/specular/typescanreport/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(TypeScanReport?, TypeScanReport?)](/Specular/pr-preview/pr-339/api/specular/typescanreport/methods.md#op-equality-typescanreport-typescanreport) : `bool` `static`
- [op_Inequality(TypeScanReport?, TypeScanReport?)](/Specular/pr-preview/pr-339/api/specular/typescanreport/methods.md#op-inequality-typescanreport-typescanreport) : `bool` `static`
- [ToString](/Specular/pr-preview/pr-339/api/specular/typescanreport/methods.md#tostring) : `string` -- Returns a string that represents the current object.
