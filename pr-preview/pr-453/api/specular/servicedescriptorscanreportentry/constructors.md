# ServiceDescriptorScanReportEntry Constructors

- Package: [Specular](/Specular/pr-preview/pr-453/api/specular.md)
- Type: [ServiceDescriptorScanReportEntry](/Specular/pr-preview/pr-453/api/specular/servicedescriptorscanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Describes one service descriptor produced by a service descriptor scan.

<a id="constructor"></a>
<a id="constructor-servicelifetime-string-string"></a>

## ServiceDescriptorScanReportEntry(ServiceLifetime, string, string)

- Name: `Constructor(ServiceLifetime, string, string)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-453-merge.Sha.d1561de846b65d15eda03f20e32d72b48aaa290d/src/Specular/Diagnostics/ScanDiagnostics.cs#L40)

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
