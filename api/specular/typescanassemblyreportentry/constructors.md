# TypeScanAssemblyReportEntry Constructors

- Package: [Specular](/Specular/api/specular.md)
- Type: [TypeScanAssemblyReportEntry](/Specular/api/specular/typescanassemblyreportentry.md)
- Kind: `Constructors`
- Members: `1`

Contains the types selected from one assembly by a type scan expression.

<a id="constructor"></a>
<a id="constructor-string-immutablelist-typescanreportentry"></a>

## TypeScanAssemblyReportEntry(string, ImmutableList<TypeScanReportEntry>)

- Name: `Constructor(string, ImmutableList<TypeScanReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.2f45910c65845a287dd3d4cb803a28e2c40926d4/src/Specular/Diagnostics/ScanDiagnostics.cs#L24)

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
