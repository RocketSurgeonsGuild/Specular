namespace Specular.Scanning;

/// <summary>A namespace filter clause.</summary>
/// <param name="Filter">How namespaces are compared.</param>
/// <param name="Namespaces">The namespaces to compare with.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record NamespaceFilterData(NamespaceFilter Filter, EquatableArray<string> Namespaces);
