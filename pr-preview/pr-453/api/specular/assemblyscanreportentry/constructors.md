# AssemblyScanReportEntry Constructors

- Package: [Specular](/Specular/pr-preview/pr-453/api/specular.md)
- Type: [AssemblyScanReportEntry](/Specular/pr-preview/pr-453/api/specular/assemblyscanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Identifies an assembly selected by an assembly scan expression.

<a id="constructor"></a>
<a id="constructor-string"></a>

## AssemblyScanReportEntry(string)

- Name: `Constructor(string)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-453-merge.Sha.d1561de846b65d15eda03f20e32d72b48aaa290d/src/Specular/Diagnostics/ScanDiagnostics.cs#L16)

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
