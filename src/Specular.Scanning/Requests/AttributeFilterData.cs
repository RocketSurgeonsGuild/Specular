namespace Specular.Scanning;

/// <summary>An attribute clause naming attribute types: <c>WithAttribute</c>, <c>WithAnyAttribute</c>, <c>WithoutAttribute</c>.</summary>
/// <param name="Include">True: the type must have any of the attributes. False: none of them.</param>
/// <param name="Any">
///     Whether this came from <c>WithAnyAttribute</c>, which compares attribute classes by name and namespace rather than by symbol,
///     matching Specular's existing behaviour; a closed generic attribute type (one with <see cref="TypeReference.TypeArguments" />)
///     is still compared exactly.
/// </param>
/// <param name="Attributes">
///     The attribute types. Types that do not resolve are left out, so an include clause with none left matches nothing and an
///     exclude clause excludes nothing for them.
/// </param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record AttributeFilterData(bool Include, bool Any, EquatableArray<TypeReference> Attributes);
