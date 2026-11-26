# ServiceDescriptorAssemblyScanReportEntry Constructors

- Package: [Specular](/Specular/api/specular.md)
- Type: [ServiceDescriptorAssemblyScanReportEntry](/Specular/api/specular/servicedescriptorassemblyscanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Contains the registrations produced from one scanned assembly.

<a id="constructor"></a>
<a id="constructor-string-immutablelist-servicedescriptorscanreportentry"></a>

## ServiceDescriptorAssemblyScanReportEntry(string, ImmutableList<ServiceDescriptorScanReportEntry>)

- Name: `Constructor(string, ImmutableList<ServiceDescriptorScanReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.e5c021749f0331c39313b2934dda1475f69a2acd/src/Specular/Diagnostics/ScanDiagnostics.cs#L36)

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
