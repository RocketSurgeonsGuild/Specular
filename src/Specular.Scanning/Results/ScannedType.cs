using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>A type a scan matched, described by value so it can be cached and compared.</summary>
/// <param name="AssemblyName">The containing assembly's metadata name.</param>
/// <param name="MetadataName">The full metadata name (nested types use <c>+</c>, generics carry their arity).</param>
/// <param name="Kind">The type kind.</param>
/// <param name="DeclaredAccessibility">The declared accessibility.</param>
/// <param name="IsAccessible">Whether the compilation can reference the type (declared accessibility, containing types and InternalsVisibleTo).</param>
/// <param name="Arity">The number of generic type parameters.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record ScannedType(
    string AssemblyName,
    string MetadataName,
    TypeKind Kind,
    Accessibility DeclaredAccessibility,
    bool IsAccessible,
    int Arity
);
