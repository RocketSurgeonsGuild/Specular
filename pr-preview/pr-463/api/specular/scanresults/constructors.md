# ScanResults Constructors

- Package: [Specular](/Specular/pr-preview/pr-463/api/specular.md)
- Type: [ScanResults](/Specular/pr-preview/pr-463/api/specular/scanresults.md)
- Kind: `Constructors`
- Members: `1`

Contains the compile-time results for all Specular scans in an assembly.

<a id="constructor"></a>
<a id="constructor-immutablelist-assemblyscanreport-immutablelist-typescanreport-immutablelist-servicedescriptorscanreport"></a>

## ScanResults(ImmutableList<AssemblyScanReport>, ImmutableList<TypeScanReport>, ImmutableList<ServiceDescriptorScanReport>)

- Name: `Constructor(ImmutableList<AssemblyScanReport>, ImmutableList<TypeScanReport>, ImmutableList<ServiceDescriptorScanReport>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-463-merge.Sha.e15c1ea5e8128370add189f972645c6989282090/src/Specular/Diagnostics/ScanDiagnostics.cs#L8)

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
