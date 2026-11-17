# AssemblyScanReport Properties

- Package: [Specular](/Specular/pr-preview/pr-431/api/specular.md)
- Type: [AssemblyScanReport](/Specular/pr-preview/pr-431/api/specular/assemblyscanreport.md)
- Kind: `Properties`
- Members: `3`

Contains the result of an assembly scan expression.

<a id="entries"></a>

## Entries

- Name: `Entries`
- Modifiers: `get; init`
- Returns: `ImmutableList<AssemblyScanReportEntry>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-431-merge.Sha.e8ba1688856cc88c795c2a6c26416db05d34b225/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<AssemblyScanReportEntry> Entries { get; init; }
```

<a id="sourceassemblyname"></a>

## SourceAssemblyName

- Name: `SourceAssemblyName`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-431-merge.Sha.e8ba1688856cc88c795c2a6c26416db05d34b225/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string SourceAssemblyName { get; init; }
```

<a id="sourceexpression"></a>

## SourceExpression

- Name: `SourceExpression`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-431-merge.Sha.e8ba1688856cc88c795c2a6c26416db05d34b225/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string SourceExpression { get; init; }
```
