# TypeScanAssemblyReportEntry Properties

- Package: [Specular](/Specular/pr-preview/pr-447/api/specular.md)
- Type: [TypeScanAssemblyReportEntry](/Specular/pr-preview/pr-447/api/specular/typescanassemblyreportentry.md)
- Kind: `Properties`
- Members: `2`

Contains the types selected from one assembly by a type scan expression.

<a id="assemblyname"></a>

## AssemblyName

- Name: `AssemblyName`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string AssemblyName { get; init; }
```

<a id="entries"></a>

## Entries

- Name: `Entries`
- Modifiers: `get; init`
- Returns: `ImmutableList<TypeScanReportEntry>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<TypeScanReportEntry> Entries { get; init; }
```
