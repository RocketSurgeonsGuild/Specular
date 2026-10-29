# ServiceDescriptorScanReportEntry

- Kind: `record`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.3.3-beta.7`
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

- [ServiceDescriptorScanReportEntry(ServiceLifetime, string, string)](/Specular/api/specular/servicedescriptorscanreportentry/constructors.md#constructor-servicelifetime-string-string) -- Describes one service descriptor produced by a service descriptor scan.

## Properties

- [ImplementationTypeName](/Specular/api/specular/servicedescriptorscanreportentry/properties.md#implementationtypename) : `string` `get; init`
- [ServiceLifetime](/Specular/api/specular/servicedescriptorscanreportentry/properties.md#servicelifetime) : `ServiceLifetime` `get; init`
- [ServiceTypeName](/Specular/api/specular/servicedescriptorscanreportentry/properties.md#servicetypename) : `string` `get; init`

## Methods

- [<Clone>$](/Specular/api/specular/servicedescriptorscanreportentry/methods.md#clone) : [ServiceDescriptorScanReportEntry](/Specular/api/specular/servicedescriptorscanreportentry.md)
- [Deconstruct(ServiceLifetime, string, string)](/Specular/api/specular/servicedescriptorscanreportentry/methods.md#deconstruct-servicelifetime-string-string)
- [Equals(object?)](/Specular/api/specular/servicedescriptorscanreportentry/methods.md#equals-object) : `bool` -- Determines whether the specified object is equal to the current object.
- [Equals(ServiceDescriptorScanReportEntry?)](/Specular/api/specular/servicedescriptorscanreportentry/methods.md#equals-servicedescriptorscanreportentry) : `bool` -- Indicates whether the current object is equal to another object of the same type.
- [GetHashCode](/Specular/api/specular/servicedescriptorscanreportentry/methods.md#gethashcode) : `int` -- Serves as the default hash function.
- [op_Equality(ServiceDescriptorScanReportEntry?, ServiceDescriptorScanReportEntry?)](/Specular/api/specular/servicedescriptorscanreportentry/methods.md#op-equality-servicedescriptorscanreportentry-servicedescriptorscanreportentry) : `bool` `static`
- [op_Inequality(ServiceDescriptorScanReportEntry?, ServiceDescriptorScanReportEntry?)](/Specular/api/specular/servicedescriptorscanreportentry/methods.md#op-inequality-servicedescriptorscanreportentry-servicedescriptorscanreportentry) : `bool` `static`
- [ToString](/Specular/api/specular/servicedescriptorscanreportentry/methods.md#tostring) : `string` -- Returns a string that represents the current object.
