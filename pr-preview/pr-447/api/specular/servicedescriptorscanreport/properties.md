# ServiceDescriptorScanReport Properties

- Package: [Specular](/Specular/pr-preview/pr-447/api/specular.md)
- Type: [ServiceDescriptorScanReport](/Specular/pr-preview/pr-447/api/specular/servicedescriptorscanreport.md)
- Kind: `Properties`
- Members: `3`

Contains the registrations produced by a service descriptor scan expression.

<a id="entries"></a>

## Entries

- Name: `Entries`
- Modifiers: `get; init`
- Returns: `ImmutableList<ServiceDescriptorAssemblyScanReportEntry>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<ServiceDescriptorAssemblyScanReportEntry> Entries { get; init; }
```

<a id="sourceassemblyname"></a>

## SourceAssemblyName

- Name: `SourceAssemblyName`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string SourceAssemblyName { get; init; }
```

<a id="sourceexpression"></a>

## SourceExpression

- Name: `SourceExpression`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string SourceExpression { get; init; }
```
