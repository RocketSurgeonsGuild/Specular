# TypeScanReportEntry Constructors

- Package: [Specular](/Specular/pr-preview/pr-445/api/specular.md)
- Type: [TypeScanReportEntry](/Specular/pr-preview/pr-445/api/specular/typescanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Identifies a type selected by a type scan expression.

<a id="constructor"></a>
<a id="constructor-string"></a>

## TypeScanReportEntry(string)

- Name: `Constructor(string)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-445-merge.Sha.148444cdd6691571fa2f9e5421c54e07d5203dfc/src/Specular/Diagnostics/ScanDiagnostics.cs#L28)

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
