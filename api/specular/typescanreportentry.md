# TypeScanReportEntry

- Kind: `record`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.3.1-beta.9`
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

- [TypeScanReportEntry(string)](/Specular/api/specular/typescanreportentry/constructors.md#constructor-string) -- Identifies a type selected by a type scan expression.

## Properties

- [TypeName](/Specular/api/specular/typescanreportentry/properties.md#typename) : `string` `get; init`

## Methods

- [<Clone>$](/Specular/api/specular/typescanreportentry/methods.md#clone) : [TypeScanReportEntry](/Specular/api/specular/typescanreportentry.md)
- [Deconstruct(string)](/Specular/api/specular/typescanreportentry/methods.md#deconstruct-string)
- [Equals(object?)](/Specular/api/specular/typescanreportentry/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(TypeScanReportEntry?)](/Specular/api/specular/typescanreportentry/methods.md#equals-typescanreportentry) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/api/specular/typescanreportentry/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(TypeScanReportEntry?, TypeScanReportEntry?)](/Specular/api/specular/typescanreportentry/methods.md#op-equality-typescanreportentry-typescanreportentry) : `bool` `static`
- [op_Inequality(TypeScanReportEntry?, TypeScanReportEntry?)](/Specular/api/specular/typescanreportentry/methods.md#op-inequality-typescanreportentry-typescanreportentry) : `bool` `static`
- [ToString](/Specular/api/specular/typescanreportentry/methods.md#tostring) : `string` -- Returns a string that represents the current object.
