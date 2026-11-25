namespace Specular.Scanning;

/// <summary>A type-name filter clause.</summary>
/// <param name="Include">True to require a match with any of the names; false to require a match with none.</param>
/// <param name="Filter">How names are compared.</param>
/// <param name="Names">The values to compare the type name with.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record NameFilterData(bool Include, TextDirectionFilter Filter, EquatableArray<string> Names);
