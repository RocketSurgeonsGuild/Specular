# TypeScanAssemblyReportEntry Properties

- Package: [Specular](/Specular/pr-preview/pr-349/api/specular.md)
- Type: [TypeScanAssemblyReportEntry](/Specular/pr-preview/pr-349/api/specular/typescanassemblyreportentry.md)
- Kind: `Properties`
- Members: `2`

Contains the types selected from one assembly by a type scan expression.

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
- Returns: `ImmutableList<TypeScanReportEntry>`

```csharp
public ImmutableList<TypeScanReportEntry> Entries { get; init; }
```
