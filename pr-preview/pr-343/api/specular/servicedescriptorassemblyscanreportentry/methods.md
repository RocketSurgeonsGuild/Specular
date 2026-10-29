# ServiceDescriptorAssemblyScanReportEntry Methods

- Package: [Specular](/Specular/pr-preview/pr-343/api/specular.md)
- Type: [ServiceDescriptorAssemblyScanReportEntry](/Specular/pr-preview/pr-343/api/specular/servicedescriptorassemblyscanreportentry.md)
- Kind: `Methods`
- Members: `8`

Contains the registrations produced from one scanned assembly.

<a id="clone"></a>

## <Clone>$

- Name: `<Clone>$`
- Returns: [ServiceDescriptorAssemblyScanReportEntry](/Specular/pr-preview/pr-343/api/specular/servicedescriptorassemblyscanreportentry.md)

```csharp
public record ServiceDescriptorAssemblyScanReportEntry
{
    public ServiceDescriptorAssemblyScanReportEntry <Clone>$()
    {
        // ...
    }
}
```

<a id="deconstruct"></a>
<a id="deconstruct-string-immutablelist-servicedescriptorscanreportentry"></a>

## Deconstruct(string, ImmutableList<ServiceDescriptorScanReportEntry>)

- Name: `Deconstruct(string, ImmutableList<ServiceDescriptorScanReportEntry>)`

```csharp
public record ServiceDescriptorAssemblyScanReportEntry
{
    public void Deconstruct(
        out string AssemblyName,
        out ImmutableList<ServiceDescriptorScanReportEntry> Entries)
    {
        // ...
    }
}
```

## Parameters

- `AssemblyName` (`string`)
- `Entries` (`ImmutableList<ServiceDescriptorScanReportEntry>`)

<a id="equals"></a>
<a id="equals-object"></a>

## Equals(object?)

- Name: `Equals(object?)`
- Modifiers: `override`
- Returns: `bool`

Determines whether the specified object is equal to the current object.

```csharp
public record ServiceDescriptorAssemblyScanReportEntry
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

<a id="equals-servicedescriptorassemblyscanreportentry"></a>

## Equals(ServiceDescriptorAssemblyScanReportEntry?)

- Name: `Equals(ServiceDescriptorAssemblyScanReportEntry?)`
- Returns: `bool`

Indicates whether the current object is equal to another object of the same type.

```csharp
public record ServiceDescriptorAssemblyScanReportEntry
{
    public bool Equals(
        ServiceDescriptorAssemblyScanReportEntry? other)
    {
        // ...
    }
}
```

## Parameters

- `other` ([ServiceDescriptorAssemblyScanReportEntry?](/Specular/pr-preview/pr-343/api/specular/servicedescriptorassemblyscanreportentry.md))
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
public record ServiceDescriptorAssemblyScanReportEntry
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
<a id="op-equality-servicedescriptorassemblyscanreportentry-servicedescriptorassemblyscanreportentry"></a>

## op_Equality(ServiceDescriptorAssemblyScanReportEntry?, ServiceDescriptorAssemblyScanReportEntry?)

- Name: `op_Equality(ServiceDescriptorAssemblyScanReportEntry?, ServiceDescriptorAssemblyScanReportEntry?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record ServiceDescriptorAssemblyScanReportEntry
{
    public static bool operator ==(
        ServiceDescriptorAssemblyScanReportEntry? left,
        ServiceDescriptorAssemblyScanReportEntry? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([ServiceDescriptorAssemblyScanReportEntry?](/Specular/pr-preview/pr-343/api/specular/servicedescriptorassemblyscanreportentry.md))
- `right` ([ServiceDescriptorAssemblyScanReportEntry?](/Specular/pr-preview/pr-343/api/specular/servicedescriptorassemblyscanreportentry.md))

<a id="op-inequality"></a>
<a id="op-inequality-servicedescriptorassemblyscanreportentry-servicedescriptorassemblyscanreportentry"></a>

## op_Inequality(ServiceDescriptorAssemblyScanReportEntry?, ServiceDescriptorAssemblyScanReportEntry?)

- Name: `op_Inequality(ServiceDescriptorAssemblyScanReportEntry?, ServiceDescriptorAssemblyScanReportEntry?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record ServiceDescriptorAssemblyScanReportEntry
{
    public static bool operator !=(
        ServiceDescriptorAssemblyScanReportEntry? left,
        ServiceDescriptorAssemblyScanReportEntry? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([ServiceDescriptorAssemblyScanReportEntry?](/Specular/pr-preview/pr-343/api/specular/servicedescriptorassemblyscanreportentry.md))
- `right` ([ServiceDescriptorAssemblyScanReportEntry?](/Specular/pr-preview/pr-343/api/specular/servicedescriptorassemblyscanreportentry.md))

<a id="tostring"></a>

## ToString

- Name: `ToString`
- Modifiers: `override`
- Returns: `string`

Returns a string that represents the current object.

```csharp
public record ServiceDescriptorAssemblyScanReportEntry
{
    public override string ToString()
    {
        // ...
    }
}
```

## Returns

`string` -- A string that represents the current object.
