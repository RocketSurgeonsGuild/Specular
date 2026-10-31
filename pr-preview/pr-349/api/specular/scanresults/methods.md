# ScanResults Methods

- Package: [Specular](/Specular/pr-preview/pr-349/api/specular.md)
- Type: [ScanResults](/Specular/pr-preview/pr-349/api/specular/scanresults.md)
- Kind: `Methods`
- Members: `8`

Contains the compile-time results for all Specular scans in an assembly.

<a id="clone"></a>

## <Clone>$

- Name: `<Clone>$`
- Returns: [ScanResults](/Specular/pr-preview/pr-349/api/specular/scanresults.md)

```csharp
public record ScanResults
{
    public ScanResults <Clone>$()
    {
        // ...
    }
}
```

<a id="deconstruct"></a>
<a id="deconstruct-immutablelist-assemblyscanreport-immutablelist-typescanreport-immutablelist-servicedescriptorscanreport"></a>

## Deconstruct(ImmutableList<AssemblyScanReport>, ImmutableList<TypeScanReport>, ImmutableList<ServiceDescriptorScanReport>)

- Name: `Deconstruct(ImmutableList<AssemblyScanReport>, ImmutableList<TypeScanReport>, ImmutableList<ServiceDescriptorScanReport>)`

```csharp
public record ScanResults
{
    public void Deconstruct(
        out ImmutableList<AssemblyScanReport> AssemblyScanReports,
        out ImmutableList<TypeScanReport> TypeScanReports,
        out ImmutableList<ServiceDescriptorScanReport> ServiceDescriptorScanReports)
    {
        // ...
    }
}
```

## Parameters

- `AssemblyScanReports` (`ImmutableList<AssemblyScanReport>`)
- `TypeScanReports` (`ImmutableList<TypeScanReport>`)
- `ServiceDescriptorScanReports` (`ImmutableList<ServiceDescriptorScanReport>`)

<a id="equals"></a>
<a id="equals-object"></a>

## Equals(object?)

- Name: `Equals(object?)`
- Modifiers: `override`
- Returns: `bool`

Determines whether the specified object is equal to the current object.

```csharp
public record ScanResults
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

<a id="equals-scanresults"></a>

## Equals(ScanResults?)

- Name: `Equals(ScanResults?)`
- Returns: `bool`

Indicates whether the current object is equal to another object of the same type.

```csharp
public record ScanResults
{
    public bool Equals(
        ScanResults? other)
    {
        // ...
    }
}
```

## Parameters

- `other` ([ScanResults?](/Specular/pr-preview/pr-349/api/specular/scanresults.md))
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
public record ScanResults
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
<a id="op-equality-scanresults-scanresults"></a>

## op_Equality(ScanResults?, ScanResults?)

- Name: `op_Equality(ScanResults?, ScanResults?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record ScanResults
{
    public static bool operator ==(
        ScanResults? left,
        ScanResults? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([ScanResults?](/Specular/pr-preview/pr-349/api/specular/scanresults.md))
- `right` ([ScanResults?](/Specular/pr-preview/pr-349/api/specular/scanresults.md))

<a id="op-inequality"></a>
<a id="op-inequality-scanresults-scanresults"></a>

## op_Inequality(ScanResults?, ScanResults?)

- Name: `op_Inequality(ScanResults?, ScanResults?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record ScanResults
{
    public static bool operator !=(
        ScanResults? left,
        ScanResults? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([ScanResults?](/Specular/pr-preview/pr-349/api/specular/scanresults.md))
- `right` ([ScanResults?](/Specular/pr-preview/pr-349/api/specular/scanresults.md))

<a id="tostring"></a>

## ToString

- Name: `ToString`
- Modifiers: `override`
- Returns: `string`

Returns a string that represents the current object.

```csharp
public record ScanResults
{
    public override string ToString()
    {
        // ...
    }
}
```

## Returns

`string` -- A string that represents the current object.
