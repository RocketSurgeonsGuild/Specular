namespace Specular.Scanning;

/// <summary>An assignability clause: <c>AssignableTo</c>, <c>AssignableToAny</c>, <c>NotAssignableTo</c>, <c>NotAssignableToAny</c>.</summary>
/// <param name="Include">True: the type must be assignable to any of <paramref name="Types" />. False: to none of them.</param>
/// <param name="Types">The types to test assignability to; an unbound generic matches any closed form.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record AssignableToFilterData(bool Include, EquatableArray<TypeReference> Types);
