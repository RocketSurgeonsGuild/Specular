# ServiceDescriptorAssemblyScanReportEntry Constructors

- Package: [Specular](/Specular/pr-preview/pr-406/api/specular.md)
- Type: [ServiceDescriptorAssemblyScanReportEntry](/Specular/pr-preview/pr-406/api/specular/servicedescriptorassemblyscanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Contains the registrations produced from one scanned assembly.

<a id="constructor"></a>
<a id="constructor-string-immutablelist-servicedescriptorscanreportentry"></a>

## ServiceDescriptorAssemblyScanReportEntry(string, ImmutableList<ServiceDescriptorScanReportEntry>)

- Name: `Constructor(string, ImmutableList<ServiceDescriptorScanReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-406-merge.Sha.a5bb8c31ae5b3055ce3bbac8a150aa5acd0acd0a/src/Specular/Diagnostics/ScanDiagnostics.cs#L36)

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
