# ITypeFilter Methods

- Package: [Specular](/Specular/v0.4.0/api/specular.md)
- Type: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)
- Kind: `Methods`
- Members: `33`

The Compiled Implementation Type Filter

<a id="assignableto"></a>

## AssignableTo

- Name: `AssignableTo`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that are assignable to `T`.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter AssignableTo<T>()
    {
        // ...
    }
}
```

<a id="assignableto-type"></a>

## AssignableTo(Type)

- Name: `AssignableTo(Type)`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that are assignable to the specified `type`.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter AssignableTo(
        Type type)
    {
        // ...
    }
}
```

## Parameters

- `type` (`Type`)
  The type that should be assignable from the matching types.

<a id="assignabletoany"></a>
<a id="assignabletoany-type-type"></a>

## AssignableToAny(Type, Type[])

- Name: `AssignableToAny(Type, Type[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that are assignable to any of the specified `types`.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter AssignableToAny(
        Type type,
        params Type[] types)
    {
        // ...
    }
}
```

## Parameters

- `type` (`Type`)
  The first type that should be assignable from the matching types.
- `types` (`Type[]`)
  The types that should be assignable from the matching types.

<a id="contains"></a>
<a id="contains-string-string"></a>

## Contains(string, string[])

- Name: `Contains(string, string[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that contain the given values

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter Contains(
        string value,
        params string[] values)
    {
        // ...
    }
}
```

## Parameters

- `value` (`string`)
- `values` (`string[]`)

<a id="endswith"></a>
<a id="endswith-string-string"></a>

## EndsWith(string, string[])

- Name: `EndsWith(string, string[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that end with

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter EndsWith(
        string value,
        params string[] values)
    {
        // ...
    }
}
```

## Parameters

- `value` (`string`)
- `values` (`string[]`)

<a id="inexactnamespaceof"></a>

## InExactNamespaceOf

- Name: `InExactNamespaceOf`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types in the exact same namespace as the type `T`

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter InExactNamespaceOf<T>()
    {
        // ...
    }
}
```

<a id="inexactnamespaceof-type-type"></a>

## InExactNamespaceOf(Type, Type[])

- Name: `InExactNamespaceOf(Type, Type[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types in the exact same namespace as the type `types`

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter InExactNamespaceOf(
        Type type,
        params Type[] types)
    {
        // ...
    }
}
```

## Parameters

- `type` (`Type`)
  The first type in the namespaces to include.
- `types` (`Type[]`)
  The types in the namespaces to include.

<a id="inexactnamespaces"></a>
<a id="inexactnamespaces-string-string"></a>

## InExactNamespaces(string, string[])

- Name: `InExactNamespaces(string, string[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types in the exact same namespace as the type `namespaces`

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter InExactNamespaces(
        string first,
        params string[] namespaces)
    {
        // ...
    }
}
```

## Parameters

- `first` (`string`)
  The first namespace to include.
- `namespaces` (`string[]`)
  The namespace to include.

<a id="infoof"></a>
<a id="infoof-typeinfofilter-typeinfofilter"></a>

## InfoOf(TypeInfoFilter, TypeInfoFilter[])

- Name: `InfoOf(TypeInfoFilter, TypeInfoFilter[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that are of the specified `typeInfoFilter`.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter InfoOf(
        TypeInfoFilter typeInfoFilter,
        params TypeInfoFilter[] typeInfoFilters)
    {
        // ...
    }
}
```

## Parameters

- `typeInfoFilter` ([TypeInfoFilter](/Specular/v0.4.0/api/specular/typeinfofilter.md))
- `typeInfoFilters` (`TypeInfoFilter[]`)

<a id="innamespaceof"></a>

## InNamespaceOf

- Name: `InNamespaceOf`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types in the same namespace as the type `T`.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter InNamespaceOf<T>()
    {
        // ...
    }
}
```

<a id="innamespaceof-type-type"></a>

## InNamespaceOf(Type, Type[])

- Name: `InNamespaceOf(Type, Type[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types in any of the namespaces of the `types` specified.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter InNamespaceOf(
        Type type,
        params Type[] types)
    {
        // ...
    }
}
```

## Parameters

- `type` (`Type`)
  The first type in the namespaces to include.
- `types` (`Type[]`)
  The types in the namespaces to include.

<a id="innamespaces"></a>
<a id="innamespaces-string-string"></a>

## InNamespaces(string, string[])

- Name: `InNamespaces(string, string[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types in any of the `namespaces` specified.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter InNamespaces(
        string first,
        params string[] namespaces)
    {
        // ...
    }
}
```

## Parameters

- `first` (`string`)
  The first namespace to include.
- `namespaces` (`string[]`)
  The namespaces to include.

<a id="kindof"></a>
<a id="kindof-typekindfilter-typekindfilter"></a>

## KindOf(TypeKindFilter, TypeKindFilter[])

- Name: `KindOf(TypeKindFilter, TypeKindFilter[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that are of the specified `typeKindFilter`.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter KindOf(
        TypeKindFilter typeKindFilter,
        params TypeKindFilter[] typeKindFilters)
    {
        // ...
    }
}
```

## Parameters

- `typeKindFilter` ([TypeKindFilter](/Specular/v0.4.0/api/specular/typekindfilter.md))
- `typeKindFilters` (`TypeKindFilter[]`)

<a id="notassignableto"></a>

## NotAssignableTo

- Name: `NotAssignableTo`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will not match all types that are assignable to `T`.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter NotAssignableTo<T>()
    {
        // ...
    }
}
```

<a id="notassignableto-type"></a>

## NotAssignableTo(Type)

- Name: `NotAssignableTo(Type)`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will not match all types that are assignable to the specified `type`.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter NotAssignableTo(
        Type type)
    {
        // ...
    }
}
```

## Parameters

- `type` (`Type`)
  The type that should be assignable from the matching types.

<a id="notassignabletoany"></a>
<a id="notassignabletoany-type-type"></a>

## NotAssignableToAny(Type, Type[])

- Name: `NotAssignableToAny(Type, Type[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will not match all types that are assignable to any of the specified `types`.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter NotAssignableToAny(
        Type type,
        params Type[] types)
    {
        // ...
    }
}
```

## Parameters

- `type` (`Type`)
  The first type that should be assignable from the matching types.
- `types` (`Type[]`)
  The types that should be assignable from the matching types.

<a id="notcontains"></a>
<a id="notcontains-string-string"></a>

## NotContains(string, string[])

- Name: `NotContains(string, string[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that contain the given values

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter NotContains(
        string value,
        params string[] values)
    {
        // ...
    }
}
```

## Parameters

- `value` (`string`)
- `values` (`string[]`)

<a id="notendswith"></a>
<a id="notendswith-string-string"></a>

## NotEndsWith(string, string[])

- Name: `NotEndsWith(string, string[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that end with

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter NotEndsWith(
        string value,
        params string[] values)
    {
        // ...
    }
}
```

## Parameters

- `value` (`string`)
- `values` (`string[]`)

<a id="notinfoof"></a>
<a id="notinfoof-typeinfofilter-typeinfofilter"></a>

## NotInfoOf(TypeInfoFilter, TypeInfoFilter[])

- Name: `NotInfoOf(TypeInfoFilter, TypeInfoFilter[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that are not of the specified `typeInfoFilter`.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter NotInfoOf(
        TypeInfoFilter typeInfoFilter,
        params TypeInfoFilter[] typeInfoFilters)
    {
        // ...
    }
}
```

## Parameters

- `typeInfoFilter` ([TypeInfoFilter](/Specular/v0.4.0/api/specular/typeinfofilter.md))
- `typeInfoFilters` (`TypeInfoFilter[]`)

<a id="notinnamespaceof"></a>

## NotInNamespaceOf

- Name: `NotInNamespaceOf`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types outside of the same namespace as the type `T`.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter NotInNamespaceOf<T>()
    {
        // ...
    }
}
```

<a id="notinnamespaceof-type-type"></a>

## NotInNamespaceOf(Type, Type[])

- Name: `NotInNamespaceOf(Type, Type[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types outside of all of the namespaces of the `types` specified.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter NotInNamespaceOf(
        Type type,
        params Type[] types)
    {
        // ...
    }
}
```

## Parameters

- `type` (`Type`)
  The first type in the namespaces to include.
- `types` (`Type[]`)
  The types in the namespaces to include.

<a id="notinnamespaces"></a>
<a id="notinnamespaces-string-string"></a>

## NotInNamespaces(string, string[])

- Name: `NotInNamespaces(string, string[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types outside of all of the `namespaces` specified.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter NotInNamespaces(
        string first,
        params string[] namespaces)
    {
        // ...
    }
}
```

## Parameters

- `first` (`string`)
  The first namespace to include.
- `namespaces` (`string[]`)
  The namespaces to include.

<a id="notkindof"></a>
<a id="notkindof-typekindfilter-typekindfilter"></a>

## NotKindOf(TypeKindFilter, TypeKindFilter[])

- Name: `NotKindOf(TypeKindFilter, TypeKindFilter[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that are not of the specified `typeKindFilter`.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter NotKindOf(
        TypeKindFilter typeKindFilter,
        params TypeKindFilter[] typeKindFilters)
    {
        // ...
    }
}
```

## Parameters

- `typeKindFilter` ([TypeKindFilter](/Specular/v0.4.0/api/specular/typekindfilter.md))
- `typeKindFilters` (`TypeKindFilter[]`)

<a id="notstartswith"></a>
<a id="notstartswith-string-string"></a>

## NotStartsWith(string, string[])

- Name: `NotStartsWith(string, string[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that start with

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter NotStartsWith(
        string value,
        params string[] values)
    {
        // ...
    }
}
```

## Parameters

- `value` (`string`)
- `values` (`string[]`)

<a id="startswith"></a>
<a id="startswith-string-string"></a>

## StartsWith(string, string[])

- Name: `StartsWith(string, string[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that start with

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter StartsWith(
        string value,
        params string[] values)
    {
        // ...
    }
}
```

## Parameters

- `value` (`string`)
- `values` (`string[]`)

<a id="withanyattribute"></a>
<a id="withanyattribute-type-type"></a>

## WithAnyAttribute(Type, Type[])

- Name: `WithAnyAttribute(Type, Type[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that has an attribute of `attributeType` defined.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter WithAnyAttribute(
        Type attributeType,
        params Type[] attributeTypes)
    {
        // ...
    }
}
```

## Parameters

- `attributeType` (`Type`)
  Type of the attribute.
- `attributeTypes` (`Type[]`)
  Types of the attribute.

<a id="withanyattribute-string-string"></a>

## WithAnyAttribute(string?, string[])

- Name: `WithAnyAttribute(string?, string[])`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that has an attribute of `attributeFullName` defined.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter WithAnyAttribute(
        string? attributeFullName,
        params string[] attributeFullNames)
    {
        // ...
    }
}
```

## Parameters

- `attributeFullName` (`string?`)
  The full name of the attribute.
- `attributeFullNames` (`string[]`)
  The full name of the attributes.

<a id="withattribute"></a>

## WithAttribute

- Name: `WithAttribute`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that has an attribute of type `T` defined.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter WithAttribute<T>()
    {
        // ...
    }
}
```

<a id="withattribute-type"></a>

## WithAttribute(Type)

- Name: `WithAttribute(Type)`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that has an attribute of `attributeType` defined.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter WithAttribute(
        Type attributeType)
    {
        // ...
    }
}
```

## Parameters

- `attributeType` (`Type`)
  Type of the attribute.

<a id="withattribute-string"></a>

## WithAttribute(string?)

- Name: `WithAttribute(string?)`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that has an attribute of `attributeFullName` defined.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter WithAttribute(
        string? attributeFullName)
    {
        // ...
    }
}
```

## Parameters

- `attributeFullName` (`string?`)
  The full name of the attribute.

<a id="withoutattribute"></a>

## WithoutAttribute

- Name: `WithoutAttribute`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that doesn't have an attribute of type `T` defined.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter WithoutAttribute<T>()
    {
        // ...
    }
}
```

<a id="withoutattribute-type"></a>

## WithoutAttribute(Type)

- Name: `WithoutAttribute(Type)`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that doesn't have an attribute of `attributeType` defined.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter WithoutAttribute(
        Type attributeType)
    {
        // ...
    }
}
```

## Parameters

- `attributeType` (`Type`)
  Type of the attribute.

<a id="withoutattribute-string"></a>

## WithoutAttribute(string?)

- Name: `WithoutAttribute(string?)`
- Modifiers: `abstract`
- Returns: [ITypeFilter](/Specular/v0.4.0/api/specular/itypefilter.md)

Will match all types that doesn't have an attribute of `attributeFullName` defined.

```csharp
public interface ITypeFilter
{
    public abstract ITypeFilter WithoutAttribute(
        string? attributeFullName)
    {
        // ...
    }
}
```

## Parameters

- `attributeFullName` (`string?`)
  The full name of the attribute.
