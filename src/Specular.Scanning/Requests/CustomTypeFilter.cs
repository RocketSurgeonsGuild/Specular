using System;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     A consumer-defined type filter. Its <see cref="CacheKey" /> stands in for its logic in request equality and cache keys, so it
///     must change whenever what the filter matches changes.
/// </summary>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
abstract class CustomTypeFilter : IEquatable<CustomTypeFilter>
{
    /// <summary>A stable key describing exactly what this filter matches.</summary>
    public abstract string CacheKey { get; }

    /// <summary>Whether <paramref name="type" /> passes this filter.</summary>
    /// <param name="compilation">The compilation being scanned.</param>
    /// <param name="type">The candidate type.</param>
    public abstract bool IsMatch(Compilation compilation, INamedTypeSymbol type);

    /// <inheritdoc />
    public bool Equals(CustomTypeFilter? other) =>
        other is { } && other.GetType() == GetType() && string.Equals(other.CacheKey, CacheKey, StringComparison.Ordinal);

    /// <inheritdoc />
    public sealed override bool Equals(object? obj) => Equals(obj as CustomTypeFilter);

    /// <inheritdoc />
    public sealed override int GetHashCode() => StringComparer.Ordinal.GetHashCode(CacheKey);
}
