# TypeScanAssemblyReportEntry

- Kind: `record`
- Package: [Specular](/Specular/pr-preview/pr-353/api/specular.md)
- Version: `0.3.3-pr.11`
- Namespace: `Specular.Diagnostics`
- Target framework: `net10.0`
- Implements: `IEquatable<TypeScanAssemblyReportEntry>`

Contains the types selected from one assembly by a type scan expression.

## Definition

```csharp
namespace Specular.Diagnostics;

public sealed record TypeScanAssemblyReportEntry
    : System.IEquatable<Specular.Diagnostics.TypeScanAssemblyReportEntry>
{
    // ...
}
```

## Constructors

- [TypeScanAssemblyReportEntry(string, ImmutableList<TypeScanReportEntry>)](/Specular/pr-preview/pr-353/api/specular/typescanassemblyreportentry/constructors.md#constructor-string-immutablelist-typescanreportentry) -- Contains the types selected from one assembly by a type scan expression.

## Properties

- [AssemblyName](/Specular/pr-preview/pr-353/api/specular/typescanassemblyreportentry/properties.md#assemblyname) : `string` `get; init`
- [Entries](/Specular/pr-preview/pr-353/api/specular/typescanassemblyreportentry/properties.md#entries) : `ImmutableList<TypeScanReportEntry>` `get; init`

## Methods

- [<Clone>$](/Specular/pr-preview/pr-353/api/specular/typescanassemblyreportentry/methods.md#clone) : [TypeScanAssemblyReportEntry](/Specular/pr-preview/pr-353/api/specular/typescanassemblyreportentry.md)
- [Deconstruct(string, ImmutableList<TypeScanReportEntry>)](/Specular/pr-preview/pr-353/api/specular/typescanassemblyreportentry/methods.md#deconstruct-string-immutablelist-typescanreportentry)
- [Equals(object?)](/Specular/pr-preview/pr-353/api/specular/typescanassemblyreportentry/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(TypeScanAssemblyReportEntry?)](/Specular/pr-preview/pr-353/api/specular/typescanassemblyreportentry/methods.md#equals-typescanassemblyreportentry) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/pr-preview/pr-353/api/specular/typescanassemblyreportentry/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(TypeScanAssemblyReportEntry?, TypeScanAssemblyReportEntry?)](/Specular/pr-preview/pr-353/api/specular/typescanassemblyreportentry/methods.md#op-equality-typescanassemblyreportentry-typescanassemblyreportentry) : `bool` `static`
- [op_Inequality(TypeScanAssemblyReportEntry?, TypeScanAssemblyReportEntry?)](/Specular/pr-preview/pr-353/api/specular/typescanassemblyreportentry/methods.md#op-inequality-typescanassemblyreportentry-typescanassemblyreportentry) : `bool` `static`
- [ToString](/Specular/pr-preview/pr-353/api/specular/typescanassemblyreportentry/methods.md#tostring) : `string` -- Returns a string that represents the current object.
