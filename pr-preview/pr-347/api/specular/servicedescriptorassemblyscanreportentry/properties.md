# ServiceDescriptorAssemblyScanReportEntry Properties

- Package: [Specular](/Specular/pr-preview/pr-347/api/specular.md)
- Type: [ServiceDescriptorAssemblyScanReportEntry](/Specular/pr-preview/pr-347/api/specular/servicedescriptorassemblyscanreportentry.md)
- Kind: `Properties`
- Members: `2`

Contains the registrations produced from one scanned assembly.

<a id="assemblyname"></a>

## AssemblyName

- Name: `AssemblyName`
- Modifiers: `get; init`
- Returns: `string`

```csharp
public string AssemblyName { get; init; }
```

<a id="entries"></a>

## Entries

- Name: `Entries`
- Modifiers: `get; init`
- Returns: `ImmutableList<ServiceDescriptorScanReportEntry>`

```csharp
public ImmutableList<ServiceDescriptorScanReportEntry> Entries { get; init; }
```
