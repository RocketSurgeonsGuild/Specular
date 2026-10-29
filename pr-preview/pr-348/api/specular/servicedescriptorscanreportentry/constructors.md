# ServiceDescriptorScanReportEntry Constructors

- Package: [Specular](/Specular/pr-preview/pr-348/api/specular.md)
- Type: [ServiceDescriptorScanReportEntry](/Specular/pr-preview/pr-348/api/specular/servicedescriptorscanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Describes one service descriptor produced by a service descriptor scan.

<a id="constructor"></a>
<a id="constructor-servicelifetime-string-string"></a>

## ServiceDescriptorScanReportEntry(ServiceLifetime, string, string)

- Name: `Constructor(ServiceLifetime, string, string)`

Describes one service descriptor produced by a service descriptor scan.

```csharp
public record ServiceDescriptorScanReportEntry
{
    public ServiceDescriptorScanReportEntry(
        ServiceLifetime ServiceLifetime,
        string ServiceTypeName,
        string ImplementationTypeName)
    {
        // ...
    }
}
```

## Parameters

- `ServiceLifetime` (`ServiceLifetime`)
- `ServiceTypeName` (`string`)
- `ImplementationTypeName` (`string`)
