# TypeScanAssemblyReportEntry Constructors

- Package: [Specular](/Specular/pr-preview/pr-444/api/specular.md)
- Type: [TypeScanAssemblyReportEntry](/Specular/pr-preview/pr-444/api/specular/typescanassemblyreportentry.md)
- Kind: `Constructors`
- Members: `1`

Contains the types selected from one assembly by a type scan expression.

<a id="constructor"></a>
<a id="constructor-string-immutablelist-typescanreportentry"></a>

## TypeScanAssemblyReportEntry(string, ImmutableList<TypeScanReportEntry>)

- Name: `Constructor(string, ImmutableList<TypeScanReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-444-merge.Sha.91db23f0a6dd2aea896fb5788d96dd2d2a023c25/src/Specular/Diagnostics/ScanDiagnostics.cs#L24)

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
