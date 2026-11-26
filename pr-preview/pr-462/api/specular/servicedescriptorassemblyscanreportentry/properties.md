# ServiceDescriptorAssemblyScanReportEntry Properties

- Package: [Specular](/Specular/pr-preview/pr-462/api/specular.md)
- Type: [ServiceDescriptorAssemblyScanReportEntry](/Specular/pr-preview/pr-462/api/specular/servicedescriptorassemblyscanreportentry.md)
- Kind: `Properties`
- Members: `2`

Contains the registrations produced from one scanned assembly.

<a id="assemblyname"></a>

## AssemblyName

- Name: `AssemblyName`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-462-merge.Sha.65577d9b63559d60a65f5887b78a6f971676b065/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string AssemblyName { get; init; }
```

<a id="entries"></a>

## Entries

- Name: `Entries`
- Modifiers: `get; init`
- Returns: `ImmutableList<ServiceDescriptorScanReportEntry>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-462-merge.Sha.65577d9b63559d60a65f5887b78a6f971676b065/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<ServiceDescriptorScanReportEntry> Entries { get; init; }
```
