using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     Filters types; the compile-time mirror of Specular's <c>ITypeFilter</c>. Every call adds a clause and a type must match all of
///     them. String overloads take full metadata names (e.g. <c>MyLib.IHandler`1</c>).
/// </summary>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
interface ITypeSymbolFilter
{
    /// <summary>Assignable to <paramref name="type" />; an unbound generic matches any closed form.</summary>
    ITypeSymbolFilter AssignableTo(ITypeSymbol type);

    /// <summary>Assignable to the type with the given full metadata name.</summary>
    ITypeSymbolFilter AssignableTo(string metadataName);

    /// <summary>Assignable to any of the types.</summary>
    ITypeSymbolFilter AssignableToAny(ITypeSymbol type, params ITypeSymbol[] types);

    /// <summary>Not assignable to <paramref name="type" />.</summary>
    ITypeSymbolFilter NotAssignableTo(ITypeSymbol type);

    /// <summary>Not assignable to the type with the given full metadata name.</summary>
    ITypeSymbolFilter NotAssignableTo(string metadataName);

    /// <summary>Assignable to none of the types.</summary>
    ITypeSymbolFilter NotAssignableToAny(ITypeSymbol type, params ITypeSymbol[] types);

    /// <summary>The type name ends with any of the values.</summary>
    ITypeSymbolFilter EndsWith(string value, params string[] values);

    /// <summary>The type name ends with none of the values.</summary>
    ITypeSymbolFilter NotEndsWith(string value, params string[] values);

    /// <summary>The type name starts with any of the values.</summary>
    ITypeSymbolFilter StartsWith(string value, params string[] values);

    /// <summary>The type name starts with none of the values.</summary>
    ITypeSymbolFilter NotStartsWith(string value, params string[] values);

    /// <summary>The type name contains any of the values.</summary>
    ITypeSymbolFilter Contains(string value, params string[] values);

    /// <summary>The type name contains none of the values.</summary>
    ITypeSymbolFilter NotContains(string value, params string[] values);

    /// <summary>In exactly the namespace of any of the types.</summary>
    ITypeSymbolFilter InExactNamespaceOf(ITypeSymbol type, params ITypeSymbol[] types);

    /// <summary>In exactly any of the namespaces.</summary>
    ITypeSymbolFilter InExactNamespaces(string first, params string[] namespaces);

    /// <summary>In, or nested under, the namespace of any of the types.</summary>
    ITypeSymbolFilter InNamespaceOf(ITypeSymbol type, params ITypeSymbol[] types);

    /// <summary>In, or nested under, any of the namespaces.</summary>
    ITypeSymbolFilter InNamespaces(string first, params string[] namespaces);

    /// <summary>Not in, or nested under, the namespace of any of the types.</summary>
    ITypeSymbolFilter NotInNamespaceOf(ITypeSymbol type, params ITypeSymbol[] types);

    /// <summary>Not in, or nested under, any of the namespaces.</summary>
    ITypeSymbolFilter NotInNamespaces(string first, params string[] namespaces);

    /// <summary>Has the attribute.</summary>
    ITypeSymbolFilter WithAttribute(ITypeSymbol attributeType);

    /// <summary>Has an attribute with the given full metadata name.</summary>
    ITypeSymbolFilter WithAttribute(string attributeFullName);

    /// <summary>Has any of the attributes.</summary>
    ITypeSymbolFilter WithAnyAttribute(ITypeSymbol attributeType, params ITypeSymbol[] attributeTypes);

    /// <summary>Has an attribute with any of the given full metadata names.</summary>
    ITypeSymbolFilter WithAnyAttribute(string attributeFullName, params string[] attributeFullNames);

    /// <summary>Does not have the attribute.</summary>
    ITypeSymbolFilter WithoutAttribute(ITypeSymbol attributeType);

    /// <summary>Does not have an attribute with the given full metadata name.</summary>
    ITypeSymbolFilter WithoutAttribute(string attributeFullName);

    /// <summary>Is any of the kinds.</summary>
    ITypeSymbolFilter KindOf(TypeKind typeKind, params TypeKind[] typeKinds);

    /// <summary>Is none of the kinds.</summary>
    ITypeSymbolFilter NotKindOf(TypeKind typeKind, params TypeKind[] typeKinds);

    /// <summary>Has any of the characteristics.</summary>
    ITypeSymbolFilter InfoOf(TypeInfoFilter typeInfoFilter, params TypeInfoFilter[] typeInfoFilters);

    /// <summary>Has none of the characteristics.</summary>
    ITypeSymbolFilter NotInfoOf(TypeInfoFilter typeInfoFilter, params TypeInfoFilter[] typeInfoFilters);

    /// <summary>Passes a consumer-defined filter.</summary>
    ITypeSymbolFilter Where(CustomTypeFilter filter);
}
