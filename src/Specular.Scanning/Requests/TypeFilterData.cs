namespace Specular.Scanning;

/// <summary>Which types a scan returns. A type matches when every clause matches.</summary>
/// <param name="PublicOnly">Only types declared public.</param>
/// <param name="AssignableTo">Assignability clauses.</param>
/// <param name="Attributes">Attribute clauses naming attribute types.</param>
/// <param name="AttributeNames">Attribute clauses naming attributes by metadata name.</param>
/// <param name="Namespaces">Namespace clauses.</param>
/// <param name="Names">Type-name clauses.</param>
/// <param name="TypeKinds">Type-kind clauses.</param>
/// <param name="TypeInfos">Type-info clauses.</param>
/// <param name="CustomFilters">Consumer-defined clauses, compared by <see cref="CustomTypeFilter.CacheKey" />.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record TypeFilterData(
    bool PublicOnly,
    EquatableArray<AssignableToFilterData> AssignableTo,
    EquatableArray<AttributeFilterData> Attributes,
    EquatableArray<AttributeNameFilterData> AttributeNames,
    EquatableArray<NamespaceFilterData> Namespaces,
    EquatableArray<NameFilterData> Names,
    EquatableArray<TypeKindFilterData> TypeKinds,
    EquatableArray<TypeInfoFilterData> TypeInfos,
    EquatableArray<CustomTypeFilter> CustomFilters
)
{
    /// <summary>A filter that matches every type.</summary>
    public static TypeFilterData All { get; } = new(
        false,
        EquatableArray<AssignableToFilterData>.Empty,
        EquatableArray<AttributeFilterData>.Empty,
        EquatableArray<AttributeNameFilterData>.Empty,
        EquatableArray<NamespaceFilterData>.Empty,
        EquatableArray<NameFilterData>.Empty,
        EquatableArray<TypeKindFilterData>.Empty,
        EquatableArray<TypeInfoFilterData>.Empty,
        EquatableArray<CustomTypeFilter>.Empty
    );
}
