# TypeScanReport Properties

- Package: [Specular](/Specular/pr-preview/pr-450/api/specular.md)
- Type: [TypeScanReport](/Specular/pr-preview/pr-450/api/specular/typescanreport.md)
- Kind: `Properties`
- Members: `3`

Contains the result of a type scan expression.

<a id="entries"></a>

## Entries

- Name: `Entries`
- Modifiers: `get; init`
- Returns: `ImmutableList<TypeScanAssemblyReportEntry>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-450-merge.Sha.2c76fe2a335f0c75ef4036ce494843e5ff65f585/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<TypeScanAssemblyReportEntry> Entries { get; init; }
```

<a id="sourceassemblyname"></a>

## SourceAssemblyName

- Name: `SourceAssemblyName`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-450-merge.Sha.2c76fe2a335f0c75ef4036ce494843e5ff65f585/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string SourceAssemblyName { get; init; }
```

<a id="sourceexpression"></a>

## SourceExpression

- Name: `SourceExpression`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-450-merge.Sha.2c76fe2a335f0c75ef4036ce494843e5ff65f585/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string SourceExpression { get; init; }
```
