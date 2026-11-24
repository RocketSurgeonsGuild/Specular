namespace Specular.Scanning;

/// <summary>Type characteristics a type filter can require or exclude; values match Specular's runtime <c>TypeInfoFilter</c>.</summary>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
enum TypeInfoFilter
{
    /// <summary>Not a valid filter.</summary>
    Unknown = 0,

    /// <summary>The type is abstract.</summary>
    Abstract = 1,

    /// <summary>The type is declared public.</summary>
    Visible = 2,

    /// <summary>The type is a value type.</summary>
    ValueType = 3,

    /// <summary>The type is sealed.</summary>
    Sealed = 5,

    /// <summary>The type is generic.</summary>
    GenericType = 6,

    /// <summary>The type is static.</summary>
    Static = -2,
}
