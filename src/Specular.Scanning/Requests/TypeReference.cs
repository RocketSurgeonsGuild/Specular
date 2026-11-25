namespace Specular.Scanning;

/// <summary>A type named by metadata, so requests stay equatable and serializable without holding symbols.</summary>
/// <remarks>
///     A reference that cannot be resolved in the compilation being scanned (a misspelled name, a type whose assembly is not
///     referenced, a type argument that does not resolve) is reported in <see cref="ScanResult.UnresolvedTypes" /> and treated as a
///     type nothing is assignable to and no type carries as an attribute. A name defined in more than one referenced assembly is
///     unresolved too when <paramref name="AssemblyName" /> is empty, because <c>Compilation.GetTypeByMetadataName</c> returns null for
///     ambiguous names: select the symbol itself (the selector records its containing assembly) or set
///     <paramref name="AssemblyName" /> to disambiguate.
/// </remarks>
/// <param name="AssemblyName">
///     The containing assembly's metadata name, or empty to resolve with <c>Compilation.GetTypeByMetadataName</c>. Only a public type
///     is found by assembly name; anything else falls back to the compilation-wide lookup.
/// </param>
/// <param name="MetadataName">The full metadata name, e.g. <c>MyLib.IHandler`1</c>.</param>
/// <param name="UnboundGeneric">Whether the type is an unbound (open) generic such as <c>IHandler&lt;&gt;</c>.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record TypeReference(string AssemblyName, string MetadataName, bool UnboundGeneric)
{
    /// <summary>
    ///     The type arguments of a closed generic such as <c>IHandler&lt;int&gt;</c>, each itself a closed reference; empty for a
    ///     non-generic type, a generic definition or an unbound generic.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Arguments are listed as metadata does, outermost containing type first: <c>Outer&lt;int&gt;.Inner&lt;string&gt;</c>
    ///         (<c>Outer`1+Inner`1</c>) has <c>[int, string]</c>. A non-empty list names exactly that closed type: assignability is the
    ///         compiler's implicit conversion (so variance applies), and attribute clauses compare the closed attribute type exactly.
    ///     </para>
    ///     <para>
    ///         The reference is unresolved unless the definition and every argument resolve, the count equals the definition's total
    ///         arity, <see cref="UnboundGeneric" /> is false, and no argument is itself a generic definition or unbound generic.
    ///     </para>
    /// </remarks>
    public EquatableArray<TypeReference> TypeArguments { get; init; } = EquatableArray<TypeReference>.Empty;
}
