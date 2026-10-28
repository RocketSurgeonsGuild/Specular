# AssemblyScanReportEntry Methods

- Package: [Specular](/Specular/pr-preview/pr-340/api/specular.md)
- Type: [AssemblyScanReportEntry](/Specular/pr-preview/pr-340/api/specular/assemblyscanreportentry.md)
- Kind: `Methods`
- Members: `8`

Identifies an assembly selected by an assembly scan expression.

<a id="clone"></a>

## <Clone>$

- Name: `<Clone>$`
- Returns: [AssemblyScanReportEntry](/Specular/pr-preview/pr-340/api/specular/assemblyscanreportentry.md)

```csharp
public record AssemblyScanReportEntry
{
    public AssemblyScanReportEntry <Clone>$()
    {
        // ...
    }
}
```

<a id="deconstruct"></a>
<a id="deconstruct-string"></a>

## Deconstruct(string)

- Name: `Deconstruct(string)`

```csharp
public record AssemblyScanReportEntry
{
    public void Deconstruct(
        out string AssemblyName)
    {
        // ...
    }
}
```

## Parameters

- `AssemblyName` (`string`)

<a id="equals"></a>
<a id="equals-object"></a>

## Equals(object?)

- Name: `Equals(object?)`
- Modifiers: `override`
- Returns: `bool`

Determines whether the specified object is equal to the current object.

```csharp
public record AssemblyScanReportEntry
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

<a id="equals-assemblyscanreportentry"></a>

## Equals(AssemblyScanReportEntry?)

- Name: `Equals(AssemblyScanReportEntry?)`
- Returns: `bool`

Indicates whether the current object is equal to another object of the same type.

```csharp
public record AssemblyScanReportEntry
{
    public bool Equals(
        AssemblyScanReportEntry? other)
    {
        // ...
    }
}
```

## Parameters

- `other` ([AssemblyScanReportEntry?](/Specular/pr-preview/pr-340/api/specular/assemblyscanreportentry.md))
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
public record AssemblyScanReportEntry
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
<a id="op-equality-assemblyscanreportentry-assemblyscanreportentry"></a>

## op_Equality(AssemblyScanReportEntry?, AssemblyScanReportEntry?)

- Name: `op_Equality(AssemblyScanReportEntry?, AssemblyScanReportEntry?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record AssemblyScanReportEntry
{
    public static bool operator ==(
        AssemblyScanReportEntry? left,
        AssemblyScanReportEntry? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([AssemblyScanReportEntry?](/Specular/pr-preview/pr-340/api/specular/assemblyscanreportentry.md))
- `right` ([AssemblyScanReportEntry?](/Specular/pr-preview/pr-340/api/specular/assemblyscanreportentry.md))

<a id="op-inequality"></a>
<a id="op-inequality-assemblyscanreportentry-assemblyscanreportentry"></a>

## op_Inequality(AssemblyScanReportEntry?, AssemblyScanReportEntry?)

- Name: `op_Inequality(AssemblyScanReportEntry?, AssemblyScanReportEntry?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record AssemblyScanReportEntry
{
    public static bool operator !=(
        AssemblyScanReportEntry? left,
        AssemblyScanReportEntry? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([AssemblyScanReportEntry?](/Specular/pr-preview/pr-340/api/specular/assemblyscanreportentry.md))
- `right` ([AssemblyScanReportEntry?](/Specular/pr-preview/pr-340/api/specular/assemblyscanreportentry.md))

<a id="tostring"></a>

## ToString

- Name: `ToString`
- Modifiers: `override`
- Returns: `string`

Returns a string that represents the current object.

```csharp
public record AssemblyScanReportEntry
{
    public override string ToString()
    {
        // ...
    }
}
```

## Returns

`string` -- A string that represents the current object.
