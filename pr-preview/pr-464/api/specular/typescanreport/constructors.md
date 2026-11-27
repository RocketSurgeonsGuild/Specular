# TypeScanReport Constructors

- Package: [Specular](/Specular/pr-preview/pr-464/api/specular.md)
- Type: [TypeScanReport](/Specular/pr-preview/pr-464/api/specular/typescanreport.md)
- Kind: `Constructors`
- Members: `1`

Contains the result of a type scan expression.

<a id="constructor"></a>
<a id="constructor-string-string-immutablelist-typescanassemblyreportentry"></a>

## TypeScanReport(string, string, ImmutableList<TypeScanAssemblyReportEntry>)

- Name: `Constructor(string, string, ImmutableList<TypeScanAssemblyReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-464-merge.Sha.508f50752fa3740945d130055b251429b230fc35/src/Specular/Diagnostics/ScanDiagnostics.cs#L20)

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
