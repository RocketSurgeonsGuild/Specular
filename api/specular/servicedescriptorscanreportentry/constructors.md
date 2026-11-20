# ServiceDescriptorScanReportEntry Constructors

- Package: [Specular](/Specular/api/specular.md)
- Type: [ServiceDescriptorScanReportEntry](/Specular/api/specular/servicedescriptorscanreportentry.md)
- Kind: `Constructors`
- Members: `1`

Describes one service descriptor produced by a service descriptor scan.

<a id="constructor"></a>
<a id="constructor-servicelifetime-string-string"></a>

## ServiceDescriptorScanReportEntry(ServiceLifetime, string, string)

- Name: `Constructor(ServiceLifetime, string, string)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.cfc4e1e69ff7a6dcb9388579cde1a5879d0e3e7e/src/Specular/Diagnostics/ScanDiagnostics.cs#L40)

Describes one service descriptor produced by a service descriptor scan.

```csharp
public record ServiceDescriptorScanReportEntry
{
    public ServiceDescriptorScanReportEntry(
        ServiceLifetime ServiceLifetime,
        string ServiceTypeName,
        string ImplementationTypeName)
    {
        // ...
    }
}
```

## Parameters

- `ServiceLifetime` (`ServiceLifetime`)
- `ServiceTypeName` (`string`)
- `ImplementationTypeName` (`string`)
