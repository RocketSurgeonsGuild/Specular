using System.Collections;
using System.Collections.Immutable;

namespace Specular.Scanning;

/// <summary>
///     An immutable array compared by its elements, so records holding one stay equatable for incremental generator caching.
/// </summary>
/// <typeparam name="T">The element type.</typeparam>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
readonly struct EquatableArray<T>(ImmutableArray<T> items) : IEquatable<EquatableArray<T>>, IReadOnlyList<T>
{
    /// <summary>An empty array.</summary>
    public static EquatableArray<T> Empty => new(ImmutableArray<T>.Empty);

    private readonly ImmutableArray<T> _items = items;

    /// <summary>The underlying items.</summary>
    public ImmutableArray<T> AsImmutableArray() => _items.IsDefault ? ImmutableArray<T>.Empty : _items;

    /// <inheritdoc />
    public int Count => AsImmutableArray().Length;

    /// <inheritdoc />
    public T this[int index] => AsImmutableArray()[index];

    /// <inheritdoc />
    public bool Equals(EquatableArray<T> other) => AsImmutableArray().SequenceEqual(other.AsImmutableArray());

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is EquatableArray<T> other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hash = 17;
        foreach (var item in AsImmutableArray())
        {
            hash = unchecked(( hash * 31 ) + ( item?.GetHashCode() ?? 0 ));
        }

        return hash;
    }

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator() => ( (IEnumerable<T>)AsImmutableArray() ).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Compares two arrays by their elements.</summary>
    public static bool operator ==(EquatableArray<T> left, EquatableArray<T> right) => left.Equals(right);

    /// <summary>Compares two arrays by their elements.</summary>
    public static bool operator !=(EquatableArray<T> left, EquatableArray<T> right) => !left.Equals(right);

    /// <summary>Wraps an immutable array.</summary>
    public static implicit operator EquatableArray<T>(ImmutableArray<T> items) => new(items);
}
