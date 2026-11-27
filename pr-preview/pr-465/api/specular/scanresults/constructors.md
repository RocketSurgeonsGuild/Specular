# ScanResults Constructors

- Package: [Specular](/Specular/pr-preview/pr-465/api/specular.md)
- Type: [ScanResults](/Specular/pr-preview/pr-465/api/specular/scanresults.md)
- Kind: `Constructors`
- Members: `1`

Contains the compile-time results for all Specular scans in an assembly.

<a id="constructor"></a>
<a id="constructor-immutablelist-assemblyscanreport-immutablelist-typescanreport-immutablelist-servicedescriptorscanreport"></a>

## ScanResults(ImmutableList<AssemblyScanReport>, ImmutableList<TypeScanReport>, ImmutableList<ServiceDescriptorScanReport>)

- Name: `Constructor(ImmutableList<AssemblyScanReport>, ImmutableList<TypeScanReport>, ImmutableList<ServiceDescriptorScanReport>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-465-merge.Sha.8017a748818d9c4a88a3d98a3d7bf52355eb148b/src/Specular/Diagnostics/ScanDiagnostics.cs#L8)

Contains the compile-time results for all Specular scans in an assembly.

```csharp
public record ScanResults
{
    public ScanResults(
        ImmutableList<AssemblyScanReport> AssemblyScanReports,
        ImmutableList<TypeScanReport> TypeScanReports,
        ImmutableList<ServiceDescriptorScanReport> ServiceDescriptorScanReports)
    {
        // ...
    }
}
```

## Parameters

- `AssemblyScanReports` (`ImmutableList<AssemblyScanReport>`)
- `TypeScanReports` (`ImmutableList<TypeScanReport>`)
- `ServiceDescriptorScanReports` (`ImmutableList<ServiceDescriptorScanReport>`)
