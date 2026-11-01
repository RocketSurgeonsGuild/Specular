# TypeScanAssemblyReportEntry Methods

- Package: [Specular](/Specular/pr-preview/pr-356/api/specular.md)
- Type: [TypeScanAssemblyReportEntry](/Specular/pr-preview/pr-356/api/specular/typescanassemblyreportentry.md)
- Kind: `Methods`
- Members: `8`

Contains the types selected from one assembly by a type scan expression.

<a id="clone"></a>

## <Clone>$

- Name: `<Clone>$`
- Returns: [TypeScanAssemblyReportEntry](/Specular/pr-preview/pr-356/api/specular/typescanassemblyreportentry.md)

```csharp
public record TypeScanAssemblyReportEntry
{
    public TypeScanAssemblyReportEntry <Clone>$()
    {
        // ...
    }
}
```

<a id="deconstruct"></a>
<a id="deconstruct-string-immutablelist-typescanreportentry"></a>

## Deconstruct(string, ImmutableList<TypeScanReportEntry>)

- Name: `Deconstruct(string, ImmutableList<TypeScanReportEntry>)`

```csharp
public record TypeScanAssemblyReportEntry
{
    public void Deconstruct(
        out string AssemblyName,
        out ImmutableList<TypeScanReportEntry> Entries)
    {
        // ...
    }
}
```

## Parameters

- `AssemblyName` (`string`)
- `Entries` (`ImmutableList<TypeScanReportEntry>`)

<a id="equals"></a>
<a id="equals-object"></a>

## Equals(object?)

- Name: `Equals(object?)`
- Modifiers: `override`
- Returns: `bool`

Determines whether the specified object is equal to the current object.

```csharp
public record TypeScanAssemblyReportEntry
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

<a id="equals-typescanassemblyreportentry"></a>

## Equals(TypeScanAssemblyReportEntry?)

- Name: `Equals(TypeScanAssemblyReportEntry?)`
- Returns: `bool`

Indicates whether the current object is equal to another object of the same type.

```csharp
public record TypeScanAssemblyReportEntry
{
    public bool Equals(
        TypeScanAssemblyReportEntry? other)
    {
        // ...
    }
}
```

## Parameters

- `other` ([TypeScanAssemblyReportEntry?](/Specular/pr-preview/pr-356/api/specular/typescanassemblyreportentry.md))
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
public record TypeScanAssemblyReportEntry
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
<a id="op-equality-typescanassemblyreportentry-typescanassemblyreportentry"></a>

## op_Equality(TypeScanAssemblyReportEntry?, TypeScanAssemblyReportEntry?)

- Name: `op_Equality(TypeScanAssemblyReportEntry?, TypeScanAssemblyReportEntry?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record TypeScanAssemblyReportEntry
{
    public static bool operator ==(
        TypeScanAssemblyReportEntry? left,
        TypeScanAssemblyReportEntry? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([TypeScanAssemblyReportEntry?](/Specular/pr-preview/pr-356/api/specular/typescanassemblyreportentry.md))
- `right` ([TypeScanAssemblyReportEntry?](/Specular/pr-preview/pr-356/api/specular/typescanassemblyreportentry.md))

<a id="op-inequality"></a>
<a id="op-inequality-typescanassemblyreportentry-typescanassemblyreportentry"></a>

## op_Inequality(TypeScanAssemblyReportEntry?, TypeScanAssemblyReportEntry?)

- Name: `op_Inequality(TypeScanAssemblyReportEntry?, TypeScanAssemblyReportEntry?)`
- Modifiers: `static`
- Returns: `bool`

```csharp
public record TypeScanAssemblyReportEntry
{
    public static bool operator !=(
        TypeScanAssemblyReportEntry? left,
        TypeScanAssemblyReportEntry? right)
    {
        // ...
    }
}
```

## Parameters

- `left` ([TypeScanAssemblyReportEntry?](/Specular/pr-preview/pr-356/api/specular/typescanassemblyreportentry.md))
- `right` ([TypeScanAssemblyReportEntry?](/Specular/pr-preview/pr-356/api/specular/typescanassemblyreportentry.md))

<a id="tostring"></a>

## ToString

- Name: `ToString`
- Modifiers: `override`
- Returns: `string`

Returns a string that represents the current object.

```csharp
public record TypeScanAssemblyReportEntry
{
    public override string ToString()
    {
        // ...
    }
}
```

## Returns

`string` -- A string that represents the current object.
