# ServiceDescriptorScanReportEntry Methods

- Package: [Specular](/Specular/pr-preview/pr-346/api/specular.md)
- Type: [ServiceDescriptorScanReportEntry](/Specular/pr-preview/pr-346/api/specular/servicedescriptorscanreportentry.md)
- Kind: `Methods`
- Members: `8`

Describes one service descriptor produced by a service descriptor scan.

<a id="clone"></a>

## <Clone>$

- Name: `<Clone>$`
- Returns: [ServiceDescriptorScanReportEntry](/Specular/pr-preview/pr-346/api/specular/servicedescriptorscanreportentry.md)

```csharp
public record ServiceDescriptorScanReportEntry
{
    public ServiceDescriptorScanReportEntry <Clone>$()
    {
        // ...
    }
}
```

<a id="deconstruct"></a>
<a id="deconstruct-servicelifetime-string-string"></a>

## Deconstruct(ServiceLifetime, string, string)

- Name: `Deconstruct(ServiceLifetime, string, string)`

```csharp
public record ServiceDescriptorScanReportEntry
{
    public void Deconstruct(
        out ServiceLifetime ServiceLifetime,
        out string ServiceTypeName,
        out string ImplementationTypeName)
    {
        // ...
    }
}
```

## Parameters

- `ServiceLifetime` (`ServiceLifetime`)
- `ServiceTypeName` (`string`)
- `ImplementationTypeName` (`string`)

<a id="equals"></a>
<a id="equals-object"></a>

## Equals(object?)

- Name: `Equals(object?)`
- Modifiers: `override`
- Returns: `bool`

Determines whether the specified object is equal to the current object.

```csharp
public record ServiceDescriptorScanReportEntry
{
    public override bool Equals(
        object? obj)
    {
        // ...
    }
}
```

## Parameters

- `obj` (`object?`)
  The object to compare with the current object.

## Returns

`bool` -- `true` if the specified object is equal to the current object; otherwise, `false`.

<a id="equals-servicedescriptorscanreportentry"></a>

## Equals(ServiceDescriptorScanReportEntry?)

- Name: `Equals(ServiceDescriptorScanReportEntry?)`
- Returns: `bool`

Indicates whether the current object is equal to another object of the same type.

```csharp
public record ServiceDescriptorScanReportEntry
{
    public bool Equals(
        ServiceDescriptorScanReportEntry? other)
    {
        // ...
    }
}
```

## Parameters

- `other` ([ServiceDescriptorScanReportEntry?](/Specular/pr-preview/pr-346/api/specular/servicedescriptorscanreportentry.md))
  An object to compare with this object.

## Returns

`bool` -- `true` if the current object is equal to the `other` parameter; otherwise, `false`.

<a id="gethashcode"></a>

## GetHashCode

- Name: `GetHashCode`
- Modifiers: `override`
- Returns: `int`

Serves as the default hash function.

```csharp
public record ServiceDescriptorScanReportEntry
{
    public override int GetHashCode()
    {
        // ...
    }
}
```

## Returns

`int` -- A hash code for the current object.

<a id="op-equality"></a>
<a id="op-equality-servicedescriptorscanreportentry-servicedescriptorscanreportentry"></a>

## op_Equality(ServiceDescriptorScanReportEntry?, ServiceDescriptorScanReportEntry?)

- Name: `op_Equality(ServiceDescriptorScanReportEntry?, ServiceDescriptorScanReportEntry?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record ServiceDescriptorScanReportEntry
{
    public static bool operator ==(
        ServiceDescriptorScanReportEntry? left,
        ServiceDescriptorScanReportEntry? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([ServiceDescriptorScanReportEntry?](/Specular/pr-preview/pr-346/api/specular/servicedescriptorscanreportentry.md))
- `right` ([ServiceDescriptorScanReportEntry?](/Specular/pr-preview/pr-346/api/specular/servicedescriptorscanreportentry.md))

<a id="op-inequality"></a>
<a id="op-inequality-servicedescriptorscanreportentry-servicedescriptorscanreportentry"></a>

## op_Inequality(ServiceDescriptorScanReportEntry?, ServiceDescriptorScanReportEntry?)

- Name: `op_Inequality(ServiceDescriptorScanReportEntry?, ServiceDescriptorScanReportEntry?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record ServiceDescriptorScanReportEntry
{
    public static bool operator !=(
        ServiceDescriptorScanReportEntry? left,
        ServiceDescriptorScanReportEntry? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([ServiceDescriptorScanReportEntry?](/Specular/pr-preview/pr-346/api/specular/servicedescriptorscanreportentry.md))
- `right` ([ServiceDescriptorScanReportEntry?](/Specular/pr-preview/pr-346/api/specular/servicedescriptorscanreportentry.md))

<a id="tostring"></a>

## ToString

- Name: `ToString`
- Modifiers: `override`
- Returns: `string`

Returns a string that represents the current object.

```csharp
public record ServiceDescriptorScanReportEntry
{
    public override string ToString()
    {
        // ...
    }
}
```

## Returns

`string` -- A string that represents the current object.
