# AssemblyScanReportEntry Constructors

- Package: [Specular](/Specular/pr-preview/pr-444/api/specular.md)
- Type: [AssemblyScanReportEntry](/Specular/pr-preview/pr-444/api/specular/assemblyscanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Identifies an assembly selected by an assembly scan expression.

<a id="constructor"></a>
<a id="constructor-string"></a>

## AssemblyScanReportEntry(string)

- Name: `Constructor(string)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-444-merge.Sha.91db23f0a6dd2aea896fb5788d96dd2d2a023c25/src/Specular/Diagnostics/ScanDiagnostics.cs#L16)

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
