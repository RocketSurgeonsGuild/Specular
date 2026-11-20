# AssemblyScanReportEntry Constructors

- Package: [Specular](/Specular/pr-preview/pr-447/api/specular.md)
- Type: [AssemblyScanReportEntry](/Specular/pr-preview/pr-447/api/specular/assemblyscanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Identifies an assembly selected by an assembly scan expression.

<a id="constructor"></a>
<a id="constructor-string"></a>

## AssemblyScanReportEntry(string)

- Name: `Constructor(string)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-447-merge.Sha.49f6fe108d2be1a11ffa9923c90174a3b77915f9/src/Specular/Diagnostics/ScanDiagnostics.cs#L16)

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
