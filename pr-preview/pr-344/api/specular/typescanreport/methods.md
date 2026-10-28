# TypeScanReport Methods

- Package: [Specular](/Specular/pr-preview/pr-344/api/specular.md)
- Type: [TypeScanReport](/Specular/pr-preview/pr-344/api/specular/typescanreport.md)
- Kind: `Methods`
- Members: `8`

Contains the result of a type scan expression.

<a id="clone"></a>

## <Clone>$

- Name: `<Clone>$`
- Returns: [TypeScanReport](/Specular/pr-preview/pr-344/api/specular/typescanreport.md)

```csharp
public record TypeScanReport
{
    public TypeScanReport <Clone>$()
    {
        // ...
    }
}
```

<a id="deconstruct"></a>
<a id="deconstruct-string-string-immutablelist-typescanassemblyreportentry"></a>

## Deconstruct(string, string, ImmutableList<TypeScanAssemblyReportEntry>)

- Name: `Deconstruct(string, string, ImmutableList<TypeScanAssemblyReportEntry>)`

```csharp
public record TypeScanReport
{
    public void Deconstruct(
        out string SourceAssemblyName,
        out string SourceExpression,
        out ImmutableList<TypeScanAssemblyReportEntry> Entries)
    {
        // ...
    }
}
```

## Parameters

- `SourceAssemblyName` (`string`)
- `SourceExpression` (`string`)
- `Entries` (`ImmutableList<TypeScanAssemblyReportEntry>`)

<a id="equals"></a>
<a id="equals-object"></a>

## Equals(object?)

- Name: `Equals(object?)`
- Modifiers: `override`
- Returns: `bool`

Determines whether the specified object is equal to the current object.

```csharp
public record TypeScanReport
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

<a id="equals-typescanreport"></a>

## Equals(TypeScanReport?)

- Name: `Equals(TypeScanReport?)`
- Returns: `bool`

Indicates whether the current object is equal to another object of the same type.

```csharp
public record TypeScanReport
{
    public bool Equals(
        TypeScanReport? other)
    {
        // ...
    }
}
```

## Parameters

- `other` ([TypeScanReport?](/Specular/pr-preview/pr-344/api/specular/typescanreport.md))
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
public record TypeScanReport
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
<a id="op-equality-typescanreport-typescanreport"></a>

## op_Equality(TypeScanReport?, TypeScanReport?)

- Name: `op_Equality(TypeScanReport?, TypeScanReport?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record TypeScanReport
{
    public static bool operator ==(
        TypeScanReport? left,
        TypeScanReport? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([TypeScanReport?](/Specular/pr-preview/pr-344/api/specular/typescanreport.md))
- `right` ([TypeScanReport?](/Specular/pr-preview/pr-344/api/specular/typescanreport.md))

<a id="op-inequality"></a>
<a id="op-inequality-typescanreport-typescanreport"></a>

## op_Inequality(TypeScanReport?, TypeScanReport?)

- Name: `op_Inequality(TypeScanReport?, TypeScanReport?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record TypeScanReport
{
    public static bool operator !=(
        TypeScanReport? left,
        TypeScanReport? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([TypeScanReport?](/Specular/pr-preview/pr-344/api/specular/typescanreport.md))
- `right` ([TypeScanReport?](/Specular/pr-preview/pr-344/api/specular/typescanreport.md))

<a id="tostring"></a>

## ToString

- Name: `ToString`
- Modifiers: `override`
- Returns: `string`

Returns a string that represents the current object.

```csharp
public record TypeScanReport
{
    public override string ToString()
    {
        // ...
    }
}
```

## Returns

`string` -- A string that represents the current object.
