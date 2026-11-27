# TypeScanAssemblyReportEntry Constructors

- Package: [Specular](/Specular/pr-preview/pr-464/api/specular.md)
- Type: [TypeScanAssemblyReportEntry](/Specular/pr-preview/pr-464/api/specular/typescanassemblyreportentry.md)
- Kind: `Constructors`
- Members: `1`

Contains the types selected from one assembly by a type scan expression.

<a id="constructor"></a>
<a id="constructor-string-immutablelist-typescanreportentry"></a>

## TypeScanAssemblyReportEntry(string, ImmutableList<TypeScanReportEntry>)

- Name: `Constructor(string, ImmutableList<TypeScanReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-464-merge.Sha.1c51e62778e1813db280ec8727b59cd4afa8515f/src/Specular/Diagnostics/ScanDiagnostics.cs#L24)

Contains the types selected from one assembly by a type scan expression.

```csharp
public record TypeScanAssemblyReportEntry
{
    public TypeScanAssemblyReportEntry(
        string AssemblyName,
        ImmutableList<TypeScanReportEntry> Entries)
    {
        // ...
    }
}
```

## Parameters

- `AssemblyName` (`string`)
- `Entries` (`ImmutableList<TypeScanReportEntry>`)
