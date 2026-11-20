# ServiceDescriptorAssemblyScanReportEntry Constructors

- Package: [Specular](/Specular/pr-preview/pr-447/api/specular.md)
- Type: [ServiceDescriptorAssemblyScanReportEntry](/Specular/pr-preview/pr-447/api/specular/servicedescriptorassemblyscanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Contains the registrations produced from one scanned assembly.

<a id="constructor"></a>
<a id="constructor-string-immutablelist-servicedescriptorscanreportentry"></a>

## ServiceDescriptorAssemblyScanReportEntry(string, ImmutableList<ServiceDescriptorScanReportEntry>)

- Name: `Constructor(string, ImmutableList<ServiceDescriptorScanReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/src/Specular/Diagnostics/ScanDiagnostics.cs#L36)

Contains the registrations produced from one scanned assembly.

```csharp
public record ServiceDescriptorAssemblyScanReportEntry
{
    public ServiceDescriptorAssemblyScanReportEntry(
        string AssemblyName,
        ImmutableList<ServiceDescriptorScanReportEntry> Entries)
    {
        // ...
    }
}
```

## Parameters

- `AssemblyName` (`string`)
- `Entries` (`ImmutableList<ServiceDescriptorScanReportEntry>`)
