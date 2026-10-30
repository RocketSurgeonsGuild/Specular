# ServiceDescriptorScanReport

- Kind: `record`
- Package: [Specular](/Specular/pr-preview/pr-351/api/specular.md)
- Version: `0.3.3-pr.10`
- Namespace: `Specular.Diagnostics`
- Target framework: `net10.0`
- Implements: `IEquatable<ServiceDescriptorScanReport>`

Contains the registrations produced by a service descriptor scan expression.

## Definition

```csharp
namespace Specular.Diagnostics;

public sealed record ServiceDescriptorScanReport
    : System.IEquatable<Specular.Diagnostics.ServiceDescriptorScanReport>
{
    // ...
}
```

## Constructors

- [ServiceDescriptorScanReport(string, string, ImmutableList<ServiceDescriptorAssemblyScanReportEntry>)](/Specular/pr-preview/pr-351/api/specular/servicedescriptorscanreport/constructors.md#constructor-string-string-immutablelist-servicedescriptorassemblyscanreportentry) -- Contains the registrations produced by a service descriptor scan expression.

## Properties

- [Entries](/Specular/pr-preview/pr-351/api/specular/servicedescriptorscanreport/properties.md#entries) : `ImmutableList<ServiceDescriptorAssemblyScanReportEntry>` `get; init`
- [SourceAssemblyName](/Specular/pr-preview/pr-351/api/specular/servicedescriptorscanreport/properties.md#sourceassemblyname) : `string` `get; init`
- [SourceExpression](/Specular/pr-preview/pr-351/api/specular/servicedescriptorscanreport/properties.md#sourceexpression) : `string` `get; init`

## Methods

- [<Clone>$](/Specular/pr-preview/pr-351/api/specular/servicedescriptorscanreport/methods.md#clone) : [ServiceDescriptorScanReport](/Specular/pr-preview/pr-351/api/specular/servicedescriptorscanreport.md)
- [Deconstruct(string, string, ImmutableList<ServiceDescriptorAssemblyScanReportEntry>)](/Specular/pr-preview/pr-351/api/specular/servicedescriptorscanreport/methods.md#deconstruct-string-string-immutablelist-servicedescriptorassemblyscanreportentry)
- [Equals(object?)](/Specular/pr-preview/pr-351/api/specular/servicedescriptorscanreport/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(ServiceDescriptorScanReport?)](/Specular/pr-preview/pr-351/api/specular/servicedescriptorscanreport/methods.md#equals-servicedescriptorscanreport) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/pr-preview/pr-351/api/specular/servicedescriptorscanreport/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(ServiceDescriptorScanReport?, ServiceDescriptorScanReport?)](/Specular/pr-preview/pr-351/api/specular/servicedescriptorscanreport/methods.md#op-equality-servicedescriptorscanreport-servicedescriptorscanreport) : `bool` `static`
- [op_Inequality(ServiceDescriptorScanReport?, ServiceDescriptorScanReport?)](/Specular/pr-preview/pr-351/api/specular/servicedescriptorscanreport/methods.md#op-inequality-servicedescriptorscanreport-servicedescriptorscanreport) : `bool` `static`
- [ToString](/Specular/pr-preview/pr-351/api/specular/servicedescriptorscanreport/methods.md#tostring) : `string` -- Returns a string that represents the current object.
