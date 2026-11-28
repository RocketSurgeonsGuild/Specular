# TypeScanReportEntry Constructors

- Package: [Specular](/Specular/pr-preview/pr-474/api/specular.md)
- Type: [TypeScanReportEntry](/Specular/pr-preview/pr-474/api/specular/typescanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Identifies a type selected by a type scan expression.

<a id="constructor"></a>
<a id="constructor-string"></a>

## TypeScanReportEntry(string)

- Name: `Constructor(string)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-474-merge.Sha.1aeada8e35256c38d58c0d80324e7d9dd7e6c8f3/src/Specular/Diagnostics/ScanDiagnostics.cs#L28)

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
