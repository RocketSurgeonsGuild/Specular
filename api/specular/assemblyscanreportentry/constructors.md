# AssemblyScanReportEntry Constructors

- Package: [Specular](/Specular/api/specular.md)
- Type: [AssemblyScanReportEntry](/Specular/api/specular/assemblyscanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Identifies an assembly selected by an assembly scan expression.

<a id="constructor"></a>
<a id="constructor-string"></a>

## AssemblyScanReportEntry(string)

- Name: `Constructor(string)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.8bcc62d19c09284f6923e54e588877e41baf4070/src/Specular/Diagnostics/ScanDiagnostics.cs#L16)

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
