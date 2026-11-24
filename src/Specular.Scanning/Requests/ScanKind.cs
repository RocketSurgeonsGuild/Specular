namespace Specular.Scanning;

/// <summary>What a <see cref="ScanRequest" /> returns.</summary>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
enum ScanKind
{
    /// <summary>The assemblies that pass the assembly filter.</summary>
    Assemblies = 1,

    /// <summary>The types, from the assemblies that pass the assembly filter, that pass the type filter.</summary>
    Types = 2,
}
