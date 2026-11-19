# ScanResults Constructors

- Package: [Specular](/Specular/pr-preview/pr-440/api/specular.md)
- Type: [ScanResults](/Specular/pr-preview/pr-440/api/specular/scanresults.md)
- Kind: `Constructors`
- Members: `1`

Contains the compile-time results for all Specular scans in an assembly.

<a id="constructor"></a>
<a id="constructor-immutablelist-assemblyscanreport-immutablelist-typescanreport-immutablelist-servicedescriptorscanreport"></a>

## ScanResults(ImmutableList<AssemblyScanReport>, ImmutableList<TypeScanReport>, ImmutableList<ServiceDescriptorScanReport>)

- Name: `Constructor(ImmutableList<AssemblyScanReport>, ImmutableList<TypeScanReport>, ImmutableList<ServiceDescriptorScanReport>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-440-merge.Sha.6c4e416f4e1851a29f25623ea42542836d5144d2/src/Specular/Diagnostics/ScanDiagnostics.cs#L8)

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
