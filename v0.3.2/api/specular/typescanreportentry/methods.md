# TypeScanReportEntry Methods

- Package: [Specular](/Specular/v0.3.2/api/specular.md)
- Type: [TypeScanReportEntry](/Specular/v0.3.2/api/specular/typescanreportentry.md)
- Kind: `Methods`
- Members: `8`

Identifies a type selected by a type scan expression.

<a id="clone"></a>

## <Clone>$

- Name: `<Clone>$`
- Returns: [TypeScanReportEntry](/Specular/v0.3.2/api/specular/typescanreportentry.md)

```csharp
public record TypeScanReportEntry
{
    public TypeScanReportEntry <Clone>$()
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
public record TypeScanReportEntry
{
    public void Deconstruct(
        out string TypeName)
    {
        // ...
    }
}
```

## Parameters

- `TypeName` (`string`)

<a id="equals"></a>
<a id="equals-object"></a>

## Equals(object?)

- Name: `Equals(object?)`
- Modifiers: `override`
- Returns: `bool`

Determines whether the specified object is equal to the current object.

```csharp
public record TypeScanReportEntry
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

<a id="equals-typescanreportentry"></a>

## Equals(TypeScanReportEntry?)

- Name: `Equals(TypeScanReportEntry?)`
- Returns: `bool`

Indicates whether the current object is equal to another object of the same type.

```csharp
public record TypeScanReportEntry
{
    public bool Equals(
        TypeScanReportEntry? other)
    {
        // ...
    }
}
```

## Parameters

- `other` ([TypeScanReportEntry?](/Specular/v0.3.2/api/specular/typescanreportentry.md))
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
public record TypeScanReportEntry
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
<a id="op-equality-typescanreportentry-typescanreportentry"></a>

## op_Equality(TypeScanReportEntry?, TypeScanReportEntry?)

- Name: `op_Equality(TypeScanReportEntry?, TypeScanReportEntry?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record TypeScanReportEntry
{
    public static bool operator ==(
        TypeScanReportEntry? left,
        TypeScanReportEntry? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([TypeScanReportEntry?](/Specular/v0.3.2/api/specular/typescanreportentry.md))
- `right` ([TypeScanReportEntry?](/Specular/v0.3.2/api/specular/typescanreportentry.md))

<a id="op-inequality"></a>
<a id="op-inequality-typescanreportentry-typescanreportentry"></a>

## op_Inequality(TypeScanReportEntry?, TypeScanReportEntry?)

- Name: `op_Inequality(TypeScanReportEntry?, TypeScanReportEntry?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record TypeScanReportEntry
{
    public static bool operator !=(
        TypeScanReportEntry? left,
        TypeScanReportEntry? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([TypeScanReportEntry?](/Specular/v0.3.2/api/specular/typescanreportentry.md))
- `right` ([TypeScanReportEntry?](/Specular/v0.3.2/api/specular/typescanreportentry.md))

<a id="tostring"></a>

## ToString

- Name: `ToString`
- Modifiers: `override`
- Returns: `string`

Returns a string that represents the current object.

```csharp
public record TypeScanReportEntry
{
    public override string ToString()
    {
        // ...
    }
}
```

## Returns

`string` -- A string that represents the current object.
