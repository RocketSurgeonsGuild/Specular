#if SPECULAR_SCANNING_EXPLICIT_USINGS
using System;
#endif

namespace Specular.Scanning;

/// <summary>Selects types from the chosen assemblies; the compile-time mirror of Specular's <c>IReflectionTypeSelector</c>.</summary>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
interface ITypeSymbolSelector : IAssemblySymbolSelector
{
    /// <summary>Makes this a types scan returning every type.</summary>
    void GetTypes();

    /// <summary>Makes this a types scan returning every type, or only public types.</summary>
    /// <param name="publicOnly">Only types declared public.</param>
    void GetTypes(bool publicOnly);

    /// <summary>Makes this a types scan returning the types that pass <paramref name="action" />.</summary>
    /// <param name="action">Adds the type filter clauses.</param>
    void GetTypes(Action<ITypeSymbolFilter> action);

    /// <summary>Makes this a types scan returning the (optionally public-only) types that pass <paramref name="action" />.</summary>
    /// <param name="publicOnly">Only types declared public.</param>
    /// <param name="action">Adds the type filter clauses.</param>
    void GetTypes(bool publicOnly, Action<ITypeSymbolFilter> action);
}
