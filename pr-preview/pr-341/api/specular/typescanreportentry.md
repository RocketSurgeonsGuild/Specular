# TypeScanReportEntry

- Kind: `record`
- Package: [Specular](/Specular/pr-preview/pr-341/api/specular.md)
- Version: `0.3.2-pr.3`
- Namespace: `Specular.Diagnostics`
- Target framework: `net10.0`
- Implements: `IEquatable<TypeScanReportEntry>`

Identifies a type selected by a type scan expression.

## Definition

```csharp
namespace Specular.Diagnostics;

public sealed record TypeScanReportEntry
    : System.IEquatable<Specular.Diagnostics.TypeScanReportEntry>
{
    // ...
}
```

## Constructors

- [TypeScanReportEntry(string)](/Specular/pr-preview/pr-341/api/specular/typescanreportentry/constructors.md#constructor-string) -- Identifies a type selected by a type scan expression.

## Properties

- [TypeName](/Specular/pr-preview/pr-341/api/specular/typescanreportentry/properties.md#typename) : `string` `get; init`

## Methods

- [<Clone>$](/Specular/pr-preview/pr-341/api/specular/typescanreportentry/methods.md#clone) : [TypeScanReportEntry](/Specular/pr-preview/pr-341/api/specular/typescanreportentry.md)
- [Deconstruct(string)](/Specular/pr-preview/pr-341/api/specular/typescanreportentry/methods.md#deconstruct-string)
- [Equals(object?)](/Specular/pr-preview/pr-341/api/specular/typescanreportentry/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(TypeScanReportEntry?)](/Specular/pr-preview/pr-341/api/specular/typescanreportentry/methods.md#equals-typescanreportentry) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/pr-preview/pr-341/api/specular/typescanreportentry/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(TypeScanReportEntry?, TypeScanReportEntry?)](/Specular/pr-preview/pr-341/api/specular/typescanreportentry/methods.md#op-equality-typescanreportentry-typescanreportentry) : `bool` `static`
- [op_Inequality(TypeScanReportEntry?, TypeScanReportEntry?)](/Specular/pr-preview/pr-341/api/specular/typescanreportentry/methods.md#op-inequality-typescanreportentry-typescanreportentry) : `bool` `static`
- [ToString](/Specular/pr-preview/pr-341/api/specular/typescanreportentry/methods.md#tostring) : `string` -- Returns a string that represents the current object.
