# AssemblyScanReport Methods

- Package: [Specular](/Specular/pr-preview/pr-348/api/specular.md)
- Type: [AssemblyScanReport](/Specular/pr-preview/pr-348/api/specular/assemblyscanreport.md)
- Kind: `Methods`
- Members: `8`

Contains the result of an assembly scan expression.

<a id="clone"></a>

## <Clone>$

- Name: `<Clone>$`
- Returns: [AssemblyScanReport](/Specular/pr-preview/pr-348/api/specular/assemblyscanreport.md)

```csharp
public record AssemblyScanReport
{
    public AssemblyScanReport <Clone>$()
    {
        // ...
    }
}
```

<a id="deconstruct"></a>
<a id="deconstruct-string-string-immutablelist-assemblyscanreportentry"></a>

## Deconstruct(string, string, ImmutableList<AssemblyScanReportEntry>)

- Name: `Deconstruct(string, string, ImmutableList<AssemblyScanReportEntry>)`

```csharp
public record AssemblyScanReport
{
    public void Deconstruct(
        out string SourceAssemblyName,
        out string SourceExpression,
        out ImmutableList<AssemblyScanReportEntry> Entries)
    {
        // ...
    }
}
```

## Parameters

- `SourceAssemblyName` (`string`)
- `SourceExpression` (`string`)
- `Entries` (`ImmutableList<AssemblyScanReportEntry>`)

<a id="equals"></a>
<a id="equals-object"></a>

## Equals(object?)

- Name: `Equals(object?)`
- Modifiers: `override`
- Returns: `bool`

Determines whether the specified object is equal to the current object.

```csharp
public record AssemblyScanReport
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

<a id="equals-assemblyscanreport"></a>

## Equals(AssemblyScanReport?)

- Name: `Equals(AssemblyScanReport?)`
- Returns: `bool`

Indicates whether the current object is equal to another object of the same type.

```csharp
public record AssemblyScanReport
{
    public bool Equals(
        AssemblyScanReport? other)
    {
        // ...
    }
}
```

## Parameters

- `other` ([AssemblyScanReport?](/Specular/pr-preview/pr-348/api/specular/assemblyscanreport.md))
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
public record AssemblyScanReport
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
<a id="op-equality-assemblyscanreport-assemblyscanreport"></a>

## op_Equality(AssemblyScanReport?, AssemblyScanReport?)

- Name: `op_Equality(AssemblyScanReport?, AssemblyScanReport?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record AssemblyScanReport
{
    public static bool operator ==(
        AssemblyScanReport? left,
        AssemblyScanReport? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([AssemblyScanReport?](/Specular/pr-preview/pr-348/api/specular/assemblyscanreport.md))
- `right` ([AssemblyScanReport?](/Specular/pr-preview/pr-348/api/specular/assemblyscanreport.md))

<a id="op-inequality"></a>
<a id="op-inequality-assemblyscanreport-assemblyscanreport"></a>

## op_Inequality(AssemblyScanReport?, AssemblyScanReport?)

- Name: `op_Inequality(AssemblyScanReport?, AssemblyScanReport?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record AssemblyScanReport
{
    public static bool operator !=(
        AssemblyScanReport? left,
        AssemblyScanReport? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([AssemblyScanReport?](/Specular/pr-preview/pr-348/api/specular/assemblyscanreport.md))
- `right` ([AssemblyScanReport?](/Specular/pr-preview/pr-348/api/specular/assemblyscanreport.md))

<a id="tostring"></a>

## ToString

- Name: `ToString`
- Modifiers: `override`
- Returns: `string`

Returns a string that represents the current object.

```csharp
public record AssemblyScanReport
{
    public override string ToString()
    {
        // ...
    }
}
```

## Returns

`string` -- A string that represents the current object.
