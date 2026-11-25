using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     Filters types; the compile-time mirror of Specular's <c>ITypeFilter</c>. Every call adds a clause and a type must match all of
///     them. String overloads take full metadata names (e.g. <c>MyLib.IHandler`1</c>).
/// </summary>
/// <remarks>
///     <para>
///         Generic types: a generic definition or unbound generic (<c>IHandler&lt;&gt;</c>) matches any closed form, while a closed
///         generic symbol (<c>IHandler&lt;int&gt;</c>) matches exactly that closed type, with its type arguments recorded in
///         <see cref="TypeReference.TypeArguments" />. Metadata-name strings cannot express type arguments: a generic name such as
///         <c>MyLib.IHandler`1</c> means any closed form.
///     </para>
///     <para>
///         Types are resolved when the scan runs. One that does not resolve (a misspelled name, a missing reference, or a name defined in
///         more than one referenced assembly, which only a symbol or an assembly-qualified <see cref="TypeReference" /> disambiguates)
///         is treated as a type nothing is assignable to and no type carries, and is reported in <see cref="ScanResult.UnresolvedTypes" />.
///         So an include clause whose types all fail matches nothing, and an exclude clause excludes nothing for that type.
///     </para>
/// </remarks>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
interface ITypeSymbolFilter
{
    /// <summary>
    ///     Assignable to <paramref name="type" />: a generic definition or unbound generic matches any closed form, a closed generic
    ///     (<c>IHandler&lt;int&gt;</c>, <c>IHandler&lt;int[]&gt;</c>) only types assignable to that closed type (variance applies).
    /// </summary>
    /// <exception cref="ArgumentException">
    ///     <paramref name="type" /> is not a named type, or is a closed generic with a type argument that is neither a named type nor an
    ///     array of one (a pointer, type parameter, <c>dynamic</c> or error type); pass the generic definition to match any closed form
    ///     instead.
    /// </exception>
    ITypeSymbolFilter AssignableTo(ITypeSymbol type);

    /// <summary>Assignable to the type with the given full metadata name; a generic name (<c>MyLib.IHandler`1</c>) matches any closed form.</summary>
    ITypeSymbolFilter AssignableTo(string metadataName);

    /// <summary>Assignable to any of the types, each matched as <see cref="AssignableTo(ITypeSymbol)" /> does.</summary>
    /// <exception cref="ArgumentException">A type is not supported, as for <see cref="AssignableTo(ITypeSymbol)" />.</exception>
    ITypeSymbolFilter AssignableToAny(ITypeSymbol type, params ITypeSymbol[] types);

    /// <summary>Not assignable to <paramref name="type" />, matched as <see cref="AssignableTo(ITypeSymbol)" /> does.</summary>
    /// <exception cref="ArgumentException">The type is not supported, as for <see cref="AssignableTo(ITypeSymbol)" />.</exception>
    ITypeSymbolFilter NotAssignableTo(ITypeSymbol type);

    /// <summary>Not assignable to the type with the given full metadata name.</summary>
    ITypeSymbolFilter NotAssignableTo(string metadataName);

    /// <summary>Assignable to none of the types, each matched as <see cref="AssignableTo(ITypeSymbol)" /> does.</summary>
    /// <exception cref="ArgumentException">A type is not supported, as for <see cref="AssignableTo(ITypeSymbol)" />.</exception>
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

    /// <summary>Has the attribute; a closed generic attribute type (<c>TagAttribute&lt;int&gt;</c>) matches only that closed use.</summary>
    /// <exception cref="ArgumentException">The type is not supported, as for <see cref="AssignableTo(ITypeSymbol)" />.</exception>
    ITypeSymbolFilter WithAttribute(ITypeSymbol attributeType);

    /// <summary>Has an attribute with the given full metadata name.</summary>
    ITypeSymbolFilter WithAttribute(string attributeFullName);

    /// <summary>
    ///     Has any of the attributes, compared by name and namespace as Specular does (so a generic definition matches every closed use);
    ///     a closed generic attribute type matches only that closed use.
    /// </summary>
    /// <exception cref="ArgumentException">A type is not supported, as for <see cref="AssignableTo(ITypeSymbol)" />.</exception>
    ITypeSymbolFilter WithAnyAttribute(ITypeSymbol attributeType, params ITypeSymbol[] attributeTypes);

    /// <summary>Has an attribute with any of the given full metadata names.</summary>
    ITypeSymbolFilter WithAnyAttribute(string attributeFullName, params string[] attributeFullNames);

    /// <summary>Does not have the attribute, matched as <see cref="WithAttribute(ITypeSymbol)" /> does.</summary>
    /// <exception cref="ArgumentException">The type is not supported, as for <see cref="AssignableTo(ITypeSymbol)" />.</exception>
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
