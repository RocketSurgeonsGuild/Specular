namespace Specular.Scanning;

/// <summary>How a name filter compares a type's name.</summary>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
enum TextDirectionFilter
{
    /// <summary>The name starts with the value.</summary>
    StartsWith = 0,

    /// <summary>The name ends with the value.</summary>
    EndsWith = 1,

    /// <summary>The name contains the value.</summary>
    Contains = 2,
}
