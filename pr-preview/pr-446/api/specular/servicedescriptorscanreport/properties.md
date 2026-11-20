# ServiceDescriptorScanReport Properties

- Package: [Specular](/Specular/pr-preview/pr-446/api/specular.md)
- Type: [ServiceDescriptorScanReport](/Specular/pr-preview/pr-446/api/specular/servicedescriptorscanreport.md)
- Kind: `Properties`
- Members: `3`

Contains the registrations produced by a service descriptor scan expression.

<a id="entries"></a>

## Entries

- Name: `Entries`
- Modifiers: `get; init`
- Returns: `ImmutableList<ServiceDescriptorAssemblyScanReportEntry>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-446-merge.Sha.bdccb36ec194143897302d3d96fd4fa575d90fa8/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<ServiceDescriptorAssemblyScanReportEntry> Entries { get; init; }
```

<a id="sourceassemblyname"></a>

## SourceAssemblyName

- Name: `SourceAssemblyName`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-446-merge.Sha.bdccb36ec194143897302d3d96fd4fa575d90fa8/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string SourceAssemblyName { get; init; }
```

<a id="sourceexpression"></a>

## SourceExpression

- Name: `SourceExpression`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-446-merge.Sha.bdccb36ec194143897302d3d96fd4fa575d90fa8/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string SourceExpression { get; init; }
```
