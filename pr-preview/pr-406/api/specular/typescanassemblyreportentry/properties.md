# TypeScanAssemblyReportEntry Properties

- Package: [Specular](/Specular/pr-preview/pr-406/api/specular.md)
- Type: [TypeScanAssemblyReportEntry](/Specular/pr-preview/pr-406/api/specular/typescanassemblyreportentry.md)
- Kind: `Properties`
- Members: `2`

Contains the types selected from one assembly by a type scan expression.

<a id="assemblyname"></a>

## AssemblyName

- Name: `AssemblyName`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-406-merge.Sha.a5bb8c31ae5b3055ce3bbac8a150aa5acd0acd0a/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string AssemblyName { get; init; }
```

<a id="entries"></a>

## Entries

- Name: `Entries`
- Modifiers: `get; init`
- Returns: `ImmutableList<TypeScanReportEntry>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-406-merge.Sha.a5bb8c31ae5b3055ce3bbac8a150aa5acd0acd0a/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<TypeScanReportEntry> Entries { get; init; }
```
