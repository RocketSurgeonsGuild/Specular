# ServiceDescriptorScanReportEntry Constructors

- Package: [Specular](/Specular/pr-preview/pr-456/api/specular.md)
- Type: [ServiceDescriptorScanReportEntry](/Specular/pr-preview/pr-456/api/specular/servicedescriptorscanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Describes one service descriptor produced by a service descriptor scan.

<a id="constructor"></a>
<a id="constructor-servicelifetime-string-string"></a>

## ServiceDescriptorScanReportEntry(ServiceLifetime, string, string)

- Name: `Constructor(ServiceLifetime, string, string)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-456-merge.Sha.47963bfdc23834edc92125d7edf7e0ecbfd1105a/src/Specular/Diagnostics/ScanDiagnostics.cs#L40)

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
