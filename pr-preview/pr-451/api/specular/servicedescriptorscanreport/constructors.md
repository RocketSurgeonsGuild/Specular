# ServiceDescriptorScanReport Constructors

- Package: [Specular](/Specular/pr-preview/pr-451/api/specular.md)
- Type: [ServiceDescriptorScanReport](/Specular/pr-preview/pr-451/api/specular/servicedescriptorscanreport.md)
- Kind: `Constructors`
- Members: `1`

Contains the registrations produced by a service descriptor scan expression.

<a id="constructor"></a>
<a id="constructor-string-string-immutablelist-servicedescriptorassemblyscanreportentry"></a>

## ServiceDescriptorScanReport(string, string, ImmutableList<ServiceDescriptorAssemblyScanReportEntry>)

- Name: `Constructor(string, string, ImmutableList<ServiceDescriptorAssemblyScanReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-451-merge.Sha.3976afc911b40cb4a244b415afe8ec88dc2bcb05/src/Specular/Diagnostics/ScanDiagnostics.cs#L32)

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
