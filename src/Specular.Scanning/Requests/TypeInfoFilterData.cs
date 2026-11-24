namespace Specular.Scanning;

/// <summary>A type-info filter clause.</summary>
/// <param name="Include">True to require any of the characteristics; false to require none.</param>
/// <param name="TypeInfos">The characteristics.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record TypeInfoFilterData(bool Include, EquatableArray<TypeInfoFilter> TypeInfos);
