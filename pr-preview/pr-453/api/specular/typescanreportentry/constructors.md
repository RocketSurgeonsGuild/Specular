# TypeScanReportEntry Constructors

- Package: [Specular](/Specular/pr-preview/pr-453/api/specular.md)
- Type: [TypeScanReportEntry](/Specular/pr-preview/pr-453/api/specular/typescanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Identifies a type selected by a type scan expression.

<a id="constructor"></a>
<a id="constructor-string"></a>

## TypeScanReportEntry(string)

- Name: `Constructor(string)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-453-merge.Sha.e15ed429f3bf41f14f92fb73273b178497785fb7/src/Specular/Diagnostics/ScanDiagnostics.cs#L28)

Identifies a type selected by a type scan expression.

```csharp
public record TypeScanReportEntry
{
    public TypeScanReportEntry(
        string TypeName)
    {
        // ...
    }
}
```

## Parameters

- `TypeName` (`string`)
