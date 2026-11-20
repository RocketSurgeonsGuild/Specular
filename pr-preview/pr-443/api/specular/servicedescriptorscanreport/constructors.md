# ServiceDescriptorScanReport Constructors

- Package: [Specular](/Specular/pr-preview/pr-443/api/specular.md)
- Type: [ServiceDescriptorScanReport](/Specular/pr-preview/pr-443/api/specular/servicedescriptorscanreport.md)
- Kind: `Constructors`
- Members: `1`

Contains the registrations produced by a service descriptor scan expression.

<a id="constructor"></a>
<a id="constructor-string-string-immutablelist-servicedescriptorassemblyscanreportentry"></a>

## ServiceDescriptorScanReport(string, string, ImmutableList<ServiceDescriptorAssemblyScanReportEntry>)

- Name: `Constructor(string, string, ImmutableList<ServiceDescriptorAssemblyScanReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-443-merge.Sha.0033aac02b7565a1ecf99425ec7bdc404ad43560/src/Specular/Diagnostics/ScanDiagnostics.cs#L32)

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
