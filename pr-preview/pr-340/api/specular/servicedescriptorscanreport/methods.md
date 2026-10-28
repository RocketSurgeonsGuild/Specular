# ServiceDescriptorScanReport Methods

- Package: [Specular](/Specular/pr-preview/pr-340/api/specular.md)
- Type: [ServiceDescriptorScanReport](/Specular/pr-preview/pr-340/api/specular/servicedescriptorscanreport.md)
- Kind: `Methods`
- Members: `8`

Contains the registrations produced by a service descriptor scan expression.

<a id="clone"></a>

## <Clone>$

- Name: `<Clone>$`
- Returns: [ServiceDescriptorScanReport](/Specular/pr-preview/pr-340/api/specular/servicedescriptorscanreport.md)

```csharp
public record ServiceDescriptorScanReport
{
    public ServiceDescriptorScanReport <Clone>$()
    {
        // ...
    }
}
```

<a id="deconstruct"></a>
<a id="deconstruct-string-string-immutablelist-servicedescriptorassemblyscanreportentry"></a>

## Deconstruct(string, string, ImmutableList<ServiceDescriptorAssemblyScanReportEntry>)

- Name: `Deconstruct(string, string, ImmutableList<ServiceDescriptorAssemblyScanReportEntry>)`

```csharp
public record ServiceDescriptorScanReport
{
    public void Deconstruct(
        out string SourceAssemblyName,
        out string SourceExpression,
        out ImmutableList<ServiceDescriptorAssemblyScanReportEntry> Entries)
    {
        // ...
    }
}
```

## Parameters

- `SourceAssemblyName` (`string`)
- `SourceExpression` (`string`)
- `Entries` (`ImmutableList<ServiceDescriptorAssemblyScanReportEntry>`)

<a id="equals"></a>
<a id="equals-object"></a>

## Equals(object?)

- Name: `Equals(object?)`
- Modifiers: `override`
- Returns: `bool`

Determines whether the specified object is equal to the current object.

```csharp
public record ServiceDescriptorScanReport
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

<a id="equals-servicedescriptorscanreport"></a>

## Equals(ServiceDescriptorScanReport?)

- Name: `Equals(ServiceDescriptorScanReport?)`
- Returns: `bool`

Indicates whether the current object is equal to another object of the same type.

```csharp
public record ServiceDescriptorScanReport
{
    public bool Equals(
        ServiceDescriptorScanReport? other)
    {
        // ...
    }
}
```

## Parameters

- `other` ([ServiceDescriptorScanReport?](/Specular/pr-preview/pr-340/api/specular/servicedescriptorscanreport.md))
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
public record ServiceDescriptorScanReport
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
<a id="op-equality-servicedescriptorscanreport-servicedescriptorscanreport"></a>

## op_Equality(ServiceDescriptorScanReport?, ServiceDescriptorScanReport?)

- Name: `op_Equality(ServiceDescriptorScanReport?, ServiceDescriptorScanReport?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record ServiceDescriptorScanReport
{
    public static bool operator ==(
        ServiceDescriptorScanReport? left,
        ServiceDescriptorScanReport? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([ServiceDescriptorScanReport?](/Specular/pr-preview/pr-340/api/specular/servicedescriptorscanreport.md))
- `right` ([ServiceDescriptorScanReport?](/Specular/pr-preview/pr-340/api/specular/servicedescriptorscanreport.md))

<a id="op-inequality"></a>
<a id="op-inequality-servicedescriptorscanreport-servicedescriptorscanreport"></a>

## op_Inequality(ServiceDescriptorScanReport?, ServiceDescriptorScanReport?)

- Name: `op_Inequality(ServiceDescriptorScanReport?, ServiceDescriptorScanReport?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record ServiceDescriptorScanReport
{
    public static bool operator !=(
        ServiceDescriptorScanReport? left,
        ServiceDescriptorScanReport? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([ServiceDescriptorScanReport?](/Specular/pr-preview/pr-340/api/specular/servicedescriptorscanreport.md))
- `right` ([ServiceDescriptorScanReport?](/Specular/pr-preview/pr-340/api/specular/servicedescriptorscanreport.md))

<a id="tostring"></a>

## ToString

- Name: `ToString`
- Modifiers: `override`
- Returns: `string`

Returns a string that represents the current object.

```csharp
public record ServiceDescriptorScanReport
{
    public override string ToString()
    {
        // ...
    }
}
```

## Returns

`string` -- A string that represents the current object.
