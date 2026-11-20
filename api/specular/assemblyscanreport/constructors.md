# AssemblyScanReport Constructors

- Package: [Specular](/Specular/api/specular.md)
- Type: [AssemblyScanReport](/Specular/api/specular/assemblyscanreport.md)
- Kind: `Constructors`
- Members: `1`

Contains the result of an assembly scan expression.

<a id="constructor"></a>
<a id="constructor-string-string-immutablelist-assemblyscanreportentry"></a>

## AssemblyScanReport(string, string, ImmutableList<AssemblyScanReportEntry>)

- Name: `Constructor(string, string, ImmutableList<AssemblyScanReportEntry>)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.cfc4e1e69ff7a6dcb9388579cde1a5879d0e3e7e/src/Specular/Diagnostics/ScanDiagnostics.cs#L12)

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
