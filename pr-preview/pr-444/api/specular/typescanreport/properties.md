# TypeScanReport Properties

- Package: [Specular](/Specular/pr-preview/pr-444/api/specular.md)
- Type: [TypeScanReport](/Specular/pr-preview/pr-444/api/specular/typescanreport.md)
- Kind: `Properties`
- Members: `3`

Contains the result of a type scan expression.

<a id="entries"></a>

## Entries

- Name: `Entries`
- Modifiers: `get; init`
- Returns: `ImmutableList<TypeScanAssemblyReportEntry>`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-444-merge.Sha.91db23f0a6dd2aea896fb5788d96dd2d2a023c25/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public ImmutableList<TypeScanAssemblyReportEntry> Entries { get; init; }
```

<a id="sourceassemblyname"></a>

## SourceAssemblyName

- Name: `SourceAssemblyName`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-444-merge.Sha.91db23f0a6dd2aea896fb5788d96dd2d2a023c25/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string SourceAssemblyName { get; init; }
```

<a id="sourceexpression"></a>

## SourceExpression

- Name: `SourceExpression`
- Modifiers: `get; init`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-444-merge.Sha.91db23f0a6dd2aea896fb5788d96dd2d2a023c25/src/Specular/Diagnostics/ScanDiagnostics.cs)

```csharp
public string SourceExpression { get; init; }
```
