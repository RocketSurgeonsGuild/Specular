# AssemblyScanReport Constructors

- Package: [Specular](/Specular/pr-preview/pr-453/api/specular.md)
- Type: [AssemblyScanReport](/Specular/pr-preview/pr-453/api/specular/assemblyscanreport.md)
- Kind: `Constructors`
- Members: `1`

Contains the result of an assembly scan expression.

<a id="constructor"></a>
<a id="constructor-string-string-immutablelist-assemblyscanreportentry"></a>

## AssemblyScanReport(string, string, ImmutableList<AssemblyScanReportEntry>)

- Name: `Constructor(string, string, ImmutableList<AssemblyScanReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-453-merge.Sha.e15ed429f3bf41f14f92fb73273b178497785fb7/src/Specular/Diagnostics/ScanDiagnostics.cs#L12)

Contains the result of an assembly scan expression.

```csharp
public record AssemblyScanReport
{
    public AssemblyScanReport(
        string SourceAssemblyName,
        string SourceExpression,
        ImmutableList<AssemblyScanReportEntry> Entries)
    {
        // ...
    }
}
```

## Parameters

- `SourceAssemblyName` (`string`)
- `SourceExpression` (`string`)
- `Entries` (`ImmutableList<AssemblyScanReportEntry>`)
