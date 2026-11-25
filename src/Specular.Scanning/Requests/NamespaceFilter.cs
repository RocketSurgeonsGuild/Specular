namespace Specular.Scanning;

/// <summary>How a namespace filter compares a type's containing namespace.</summary>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
enum NamespaceFilter
{
    /// <summary>The namespace equals one of the given namespaces.</summary>
    Exact = 1,

    /// <summary>The namespace starts with one of the given namespaces.</summary>
    In = 2,

    /// <summary>The namespace starts with none of the given namespaces.</summary>
    NotIn = 3,
}
