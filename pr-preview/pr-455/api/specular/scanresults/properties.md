# ScanResults Properties

- Package: [Specular](/Specular/pr-preview/pr-455/api/specular.md)
- Type: [ScanResults](/Specular/pr-preview/pr-455/api/specular/scanresults.md)
- Kind: `Properties`
- Members: `3`

Contains the compile-time results for all Specular scans in an assembly.

<a id="assemblyscanreports"></a>

## AssemblyScanReports

- Name: `AssemblyScanReports`
- Modifiers: `get; init`
- Returns: `ImmutableList<AssemblyScanReport>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-455-merge.Sha.ad40fe1677bfb4ad95a1053b46feddf65f718b76/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<AssemblyScanReport> AssemblyScanReports { get; init; }
```

<a id="servicedescriptorscanreports"></a>

## ServiceDescriptorScanReports

- Name: `ServiceDescriptorScanReports`
- Modifiers: `get; init`
- Returns: `ImmutableList<ServiceDescriptorScanReport>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-455-merge.Sha.ad40fe1677bfb4ad95a1053b46feddf65f718b76/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<ServiceDescriptorScanReport> ServiceDescriptorScanReports { get; init; }
```

<a id="typescanreports"></a>

## TypeScanReports

- Name: `TypeScanReports`
- Modifiers: `get; init`
- Returns: `ImmutableList<TypeScanReport>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-455-merge.Sha.ad40fe1677bfb4ad95a1053b46feddf65f718b76/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<TypeScanReport> TypeScanReports { get; init; }
```
