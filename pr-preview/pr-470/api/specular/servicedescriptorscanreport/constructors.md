# ServiceDescriptorScanReport Constructors

- Package: [Specular](/Specular/pr-preview/pr-470/api/specular.md)
- Type: [ServiceDescriptorScanReport](/Specular/pr-preview/pr-470/api/specular/servicedescriptorscanreport.md)
- Kind: `Constructors`
- Members: `1`

Contains the registrations produced by a service descriptor scan expression.

<a id="constructor"></a>
<a id="constructor-string-string-immutablelist-servicedescriptorassemblyscanreportentry"></a>

## ServiceDescriptorScanReport(string, string, ImmutableList<ServiceDescriptorAssemblyScanReportEntry>)

- Name: `Constructor(string, string, ImmutableList<ServiceDescriptorAssemblyScanReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-470-merge.Sha.9e8231dd8943f7f4db4653a342095fa749fa9265/src/Specular/Diagnostics/ScanDiagnostics.cs#L32)

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
