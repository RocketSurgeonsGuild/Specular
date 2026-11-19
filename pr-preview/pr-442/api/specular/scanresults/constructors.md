# ScanResults Constructors

- Package: [Specular](/Specular/pr-preview/pr-442/api/specular.md)
- Type: [ScanResults](/Specular/pr-preview/pr-442/api/specular/scanresults.md)
- Kind: `Constructors`
- Members: `1`

Contains the compile-time results for all Specular scans in an assembly.

<a id="constructor"></a>
<a id="constructor-immutablelist-assemblyscanreport-immutablelist-typescanreport-immutablelist-servicedescriptorscanreport"></a>

## ScanResults(ImmutableList<AssemblyScanReport>, ImmutableList<TypeScanReport>, ImmutableList<ServiceDescriptorScanReport>)

- Name: `Constructor(ImmutableList<AssemblyScanReport>, ImmutableList<TypeScanReport>, ImmutableList<ServiceDescriptorScanReport>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-442-merge.Sha.dca6b2e19ff475e66bd255b81efe7b8c43df4286/src/Specular/Diagnostics/ScanDiagnostics.cs#L8)

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
