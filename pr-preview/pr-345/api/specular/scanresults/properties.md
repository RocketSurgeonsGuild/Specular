# ScanResults Properties

- Package: [Specular](/Specular/pr-preview/pr-345/api/specular.md)
- Type: [ScanResults](/Specular/pr-preview/pr-345/api/specular/scanresults.md)
- Kind: `Properties`
- Members: `3`

Contains the compile-time results for all Specular scans in an assembly.

<a id="assemblyscanreports"></a>

## AssemblyScanReports

- Name: `AssemblyScanReports`
- Modifiers: `get; init`
- Returns: `ImmutableList<AssemblyScanReport>`

```csharp
public ImmutableList<AssemblyScanReport> AssemblyScanReports { get; init; }
```

<a id="servicedescriptorscanreports"></a>

## ServiceDescriptorScanReports

- Name: `ServiceDescriptorScanReports`
- Modifiers: `get; init`
- Returns: `ImmutableList<ServiceDescriptorScanReport>`

```csharp
public ImmutableList<ServiceDescriptorScanReport> ServiceDescriptorScanReports { get; init; }
```

<a id="typescanreports"></a>

## TypeScanReports

- Name: `TypeScanReports`
- Modifiers: `get; init`
- Returns: `ImmutableList<TypeScanReport>`

```csharp
public ImmutableList<TypeScanReport> TypeScanReports { get; init; }
```
