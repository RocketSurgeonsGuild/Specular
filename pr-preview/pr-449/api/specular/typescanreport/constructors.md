# TypeScanReport Constructors

- Package: [Specular](/Specular/pr-preview/pr-449/api/specular.md)
- Type: [TypeScanReport](/Specular/pr-preview/pr-449/api/specular/typescanreport.md)
- Kind: `Constructors`
- Members: `1`

Contains the result of a type scan expression.

<a id="constructor"></a>
<a id="constructor-string-string-immutablelist-typescanassemblyreportentry"></a>

## TypeScanReport(string, string, ImmutableList<TypeScanAssemblyReportEntry>)

- Name: `Constructor(string, string, ImmutableList<TypeScanAssemblyReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-449-merge.Sha.a4e7451225f525b22271723fffa01211c276f62e/src/Specular/Diagnostics/ScanDiagnostics.cs#L20)

Contains the result of a type scan expression.

```csharp
public record TypeScanReport
{
    public TypeScanReport(
        string SourceAssemblyName,
        string SourceExpression,
        ImmutableList<TypeScanAssemblyReportEntry> Entries)
    {
        // ...
    }
}
```

## Parameters

- `SourceAssemblyName` (`string`)
- `SourceExpression` (`string`)
- `Entries` (`ImmutableList<TypeScanAssemblyReportEntry>`)
