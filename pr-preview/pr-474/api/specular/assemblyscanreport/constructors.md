# AssemblyScanReport Constructors

- Package: [Specular](/Specular/pr-preview/pr-474/api/specular.md)
- Type: [AssemblyScanReport](/Specular/pr-preview/pr-474/api/specular/assemblyscanreport.md)
- Kind: `Constructors`
- Members: `1`

Contains the result of an assembly scan expression.

<a id="constructor"></a>
<a id="constructor-string-string-immutablelist-assemblyscanreportentry"></a>

## AssemblyScanReport(string, string, ImmutableList<AssemblyScanReportEntry>)

- Name: `Constructor(string, string, ImmutableList<AssemblyScanReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-474-merge.Sha.1aeada8e35256c38d58c0d80324e7d9dd7e6c8f3/src/Specular/Diagnostics/ScanDiagnostics.cs#L12)

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
