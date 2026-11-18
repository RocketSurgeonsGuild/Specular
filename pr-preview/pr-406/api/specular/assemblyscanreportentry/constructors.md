# AssemblyScanReportEntry Constructors

- Package: [Specular](/Specular/pr-preview/pr-406/api/specular.md)
- Type: [AssemblyScanReportEntry](/Specular/pr-preview/pr-406/api/specular/assemblyscanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Identifies an assembly selected by an assembly scan expression.

<a id="constructor"></a>
<a id="constructor-string"></a>

## AssemblyScanReportEntry(string)

- Name: `Constructor(string)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-406-merge.Sha.a5bb8c31ae5b3055ce3bbac8a150aa5acd0acd0a/src/Specular/Diagnostics/ScanDiagnostics.cs#L16)

Identifies an assembly selected by an assembly scan expression.

```csharp
public record AssemblyScanReportEntry
{
    public AssemblyScanReportEntry(
        string AssemblyName)
    {
        // ...
    }
}
```

## Parameters

- `AssemblyName` (`string`)
