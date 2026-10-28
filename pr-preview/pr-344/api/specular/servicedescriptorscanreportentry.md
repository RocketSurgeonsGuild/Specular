# ServiceDescriptorScanReportEntry

- Kind: `record`
- Package: [Specular](/Specular/pr-preview/pr-344/api/specular.md)
- Version: `0.3.3-pr.4`
- Namespace: `Specular.Diagnostics`
- Target framework: `net10.0`
- Implements: `IEquatable<ServiceDescriptorScanReportEntry>`

Describes one service descriptor produced by a service descriptor scan.

## Definition

```csharp
namespace Specular.Diagnostics;

public sealed record ServiceDescriptorScanReportEntry
    : System.IEquatable<Specular.Diagnostics.ServiceDescriptorScanReportEntry>
{
    // ...
}
```

## Constructors

- [ServiceDescriptorScanReportEntry(ServiceLifetime, string, string)](/Specular/pr-preview/pr-344/api/specular/servicedescriptorscanreportentry/constructors.md#constructor-servicelifetime-string-string) -- Describes one service descriptor produced by a service descriptor scan.

## Properties

- [ImplementationTypeName](/Specular/pr-preview/pr-344/api/specular/servicedescriptorscanreportentry/properties.md#implementationtypename) : `string` `get; init`
- [ServiceLifetime](/Specular/pr-preview/pr-344/api/specular/servicedescriptorscanreportentry/properties.md#servicelifetime) : `ServiceLifetime` `get; init`
- [ServiceTypeName](/Specular/pr-preview/pr-344/api/specular/servicedescriptorscanreportentry/properties.md#servicetypename) : `string` `get; init`

## Methods

- [<Clone>$](/Specular/pr-preview/pr-344/api/specular/servicedescriptorscanreportentry/methods.md#clone) : [ServiceDescriptorScanReportEntry](/Specular/pr-preview/pr-344/api/specular/servicedescriptorscanreportentry.md)
- [Deconstruct(ServiceLifetime, string, string)](/Specular/pr-preview/pr-344/api/specular/servicedescriptorscanreportentry/methods.md#deconstruct-servicelifetime-string-string)
- [Equals(object?)](/Specular/pr-preview/pr-344/api/specular/servicedescriptorscanreportentry/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(ServiceDescriptorScanReportEntry?)](/Specular/pr-preview/pr-344/api/specular/servicedescriptorscanreportentry/methods.md#equals-servicedescriptorscanreportentry) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/pr-preview/pr-344/api/specular/servicedescriptorscanreportentry/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(ServiceDescriptorScanReportEntry?, ServiceDescriptorScanReportEntry?)](/Specular/pr-preview/pr-344/api/specular/servicedescriptorscanreportentry/methods.md#op-equality-servicedescriptorscanreportentry-servicedescriptorscanreportentry) : `bool` `static`
- [op_Inequality(ServiceDescriptorScanReportEntry?, ServiceDescriptorScanReportEntry?)](/Specular/pr-preview/pr-344/api/specular/servicedescriptorscanreportentry/methods.md#op-inequality-servicedescriptorscanreportentry-servicedescriptorscanreportentry) : `bool` `static`
- [ToString](/Specular/pr-preview/pr-344/api/specular/servicedescriptorscanreportentry/methods.md#tostring) : `string` -- Returns a string that represents the current object.
