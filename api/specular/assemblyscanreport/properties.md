# AssemblyScanReport Properties

- Package: [Specular](/Specular/api/specular.md)
- Type: [AssemblyScanReport](/Specular/api/specular/assemblyscanreport.md)
- Kind: `Properties`
- Members: `3`

Contains the result of an assembly scan expression.

<a id="entries"></a>

## Entries

- Name: `Entries`
- Modifiers: `get; init`
- Returns: `ImmutableList<AssemblyScanReportEntry>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.71a81f46ec8e0f8bc11d811fc85b38e1320f9d2a/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<AssemblyScanReportEntry> Entries { get; init; }
```

<a id="sourceassemblyname"></a>

## SourceAssemblyName

- Name: `SourceAssemblyName`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.71a81f46ec8e0f8bc11d811fc85b38e1320f9d2a/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string SourceAssemblyName { get; init; }
```

<a id="sourceexpression"></a>

## SourceExpression

- Name: `SourceExpression`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.71a81f46ec8e0f8bc11d811fc85b38e1320f9d2a/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string SourceExpression { get; init; }
```
