# ScanResults Properties

- Package: [Specular](/Specular/pr-preview/pr-439/api/specular.md)
- Type: [ScanResults](/Specular/pr-preview/pr-439/api/specular/scanresults.md)
- Kind: `Properties`
- Members: `3`

Contains the compile-time results for all Specular scans in an assembly.

<a id="assemblyscanreports"></a>

## AssemblyScanReports

- Name: `AssemblyScanReports`
- Modifiers: `get; init`
- Returns: `ImmutableList<AssemblyScanReport>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-439-merge.Sha.a00a386a02665437a1eaba7e40471f7bf42b5822/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<AssemblyScanReport> AssemblyScanReports { get; init; }
```

<a id="servicedescriptorscanreports"></a>

## ServiceDescriptorScanReports

- Name: `ServiceDescriptorScanReports`
- Modifiers: `get; init`
- Returns: `ImmutableList<ServiceDescriptorScanReport>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-439-merge.Sha.a00a386a02665437a1eaba7e40471f7bf42b5822/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<ServiceDescriptorScanReport> ServiceDescriptorScanReports { get; init; }
```

<a id="typescanreports"></a>

## TypeScanReports

- Name: `TypeScanReports`
- Modifiers: `get; init`
- Returns: `ImmutableList<TypeScanReport>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-439-merge.Sha.a00a386a02665437a1eaba7e40471f7bf42b5822/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<TypeScanReport> TypeScanReports { get; init; }
```
