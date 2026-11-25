using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>A type-kind filter clause.</summary>
/// <param name="Include">True to require any of the kinds; false to require none.</param>
/// <param name="TypeKinds">The kinds.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record TypeKindFilterData(bool Include, EquatableArray<TypeKind> TypeKinds);
