# ServiceDescriptorScanReport Constructors

- Package: [Specular](/Specular/api/specular.md)
- Type: [ServiceDescriptorScanReport](/Specular/api/specular/servicedescriptorscanreport.md)
- Kind: `Constructors`
- Members: `1`

Contains the registrations produced by a service descriptor scan expression.

<a id="constructor"></a>
<a id="constructor-string-string-immutablelist-servicedescriptorassemblyscanreportentry"></a>

## ServiceDescriptorScanReport(string, string, ImmutableList<ServiceDescriptorAssemblyScanReportEntry>)

- Name: `Constructor(string, string, ImmutableList<ServiceDescriptorAssemblyScanReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.a96f6200b68eab2ddc508f55e3106003db424406/src/Specular/Diagnostics/ScanDiagnostics.cs#L32)

Contains the registrations produced by a service descriptor scan expression.

```csharp
public record ServiceDescriptorScanReport
{
    public ServiceDescriptorScanReport(
        string SourceAssemblyName,
        string SourceExpression,
        ImmutableList<ServiceDescriptorAssemblyScanReportEntry> Entries)
    {
        // ...
    }
}
```

## Parameters

- `SourceAssemblyName` (`string`)
- `SourceExpression` (`string`)
- `Entries` (`ImmutableList<ServiceDescriptorAssemblyScanReportEntry>`)
