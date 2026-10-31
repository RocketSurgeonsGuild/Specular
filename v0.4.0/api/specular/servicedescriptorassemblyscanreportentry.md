# ServiceDescriptorAssemblyScanReportEntry

- Kind: `record`
- Package: [Specular](/Specular/v0.4.0/api/specular.md)
- Version: `0.4.0`
- Namespace: `Specular.Diagnostics`
- Target framework: `net10.0`
- Implements: `IEquatable<ServiceDescriptorAssemblyScanReportEntry>`

Contains the registrations produced from one scanned assembly.

## Definition

```csharp
namespace Specular.Diagnostics;

public sealed record ServiceDescriptorAssemblyScanReportEntry
    : System.IEquatable<Specular.Diagnostics.ServiceDescriptorAssemblyScanReportEntry>
{
    // ...
}
```

## Constructors

- [ServiceDescriptorAssemblyScanReportEntry(string, ImmutableList<ServiceDescriptorScanReportEntry>)](/Specular/v0.4.0/api/specular/servicedescriptorassemblyscanreportentry/constructors.md#constructor-string-immutablelist-servicedescriptorscanreportentry) -- Contains the registrations produced from one scanned assembly.

## Properties

- [AssemblyName](/Specular/v0.4.0/api/specular/servicedescriptorassemblyscanreportentry/properties.md#assemblyname) : `string` `get; init`
- [Entries](/Specular/v0.4.0/api/specular/servicedescriptorassemblyscanreportentry/properties.md#entries) : `ImmutableList<ServiceDescriptorScanReportEntry>` `get; init`

## Methods

- [<Clone>$](/Specular/v0.4.0/api/specular/servicedescriptorassemblyscanreportentry/methods.md#clone) : [ServiceDescriptorAssemblyScanReportEntry](/Specular/v0.4.0/api/specular/servicedescriptorassemblyscanreportentry.md)
- [Deconstruct(string, ImmutableList<ServiceDescriptorScanReportEntry>)](/Specular/v0.4.0/api/specular/servicedescriptorassemblyscanreportentry/methods.md#deconstruct-string-immutablelist-servicedescriptorscanreportentry)
- [Equals(object?)](/Specular/v0.4.0/api/specular/servicedescriptorassemblyscanreportentry/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(ServiceDescriptorAssemblyScanReportEntry?)](/Specular/v0.4.0/api/specular/servicedescriptorassemblyscanreportentry/methods.md#equals-servicedescriptorassemblyscanreportentry) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/v0.4.0/api/specular/servicedescriptorassemblyscanreportentry/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(ServiceDescriptorAssemblyScanReportEntry?, ServiceDescriptorAssemblyScanReportEntry?)](/Specular/v0.4.0/api/specular/servicedescriptorassemblyscanreportentry/methods.md#op-equality-servicedescriptorassemblyscanreportentry-servicedescriptorassemblyscanreportentry) : `bool` `static`
- [op_Inequality(ServiceDescriptorAssemblyScanReportEntry?, ServiceDescriptorAssemblyScanReportEntry?)](/Specular/v0.4.0/api/specular/servicedescriptorassemblyscanreportentry/methods.md#op-inequality-servicedescriptorassemblyscanreportentry-servicedescriptorassemblyscanreportentry) : `bool` `static`
- [ToString](/Specular/v0.4.0/api/specular/servicedescriptorassemblyscanreportentry/methods.md#tostring) : `string` -- Returns a string that represents the current object.
