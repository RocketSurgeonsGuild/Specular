namespace Specular.Scanning;

/// <summary>An attribute clause naming attribute types: <c>WithAttribute</c>, <c>WithAnyAttribute</c>, <c>WithoutAttribute</c>.</summary>
/// <param name="Include">True: the type must have any of the attributes. False: none of them.</param>
/// <param name="Any">
///     Whether this came from <c>WithAnyAttribute</c>, which compares attribute classes by name and namespace rather than by symbol,
///     matching Specular's existing behaviour.
/// </param>
/// <param name="Attributes">The attribute types.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record AttributeFilterData(bool Include, bool Any, EquatableArray<TypeReference> Attributes);
