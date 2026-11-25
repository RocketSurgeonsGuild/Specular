namespace Specular.Scanning;

/// <summary>Which assemblies a scan looks in. Assemblies are named by <c>IAssemblySymbol.MetadataName</c>.</summary>
/// <param name="AllAssemblies">Every referenced assembly and the compilation's own (<c>FromAssemblies()</c>).</param>
/// <param name="IncludeSystemAssemblies">Whether core assemblies (mscorlib, netstandard, System.Runtime, ...) are included.</param>
/// <param name="Assemblies">Specific assemblies to include.</param>
/// <param name="NotAssemblies">Assemblies to exclude.</param>
/// <param name="DependenciesOf">Include every assembly that references one of these.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record AssemblyFilterData(
    bool AllAssemblies,
    bool IncludeSystemAssemblies,
    EquatableArray<string> Assemblies,
    EquatableArray<string> NotAssemblies,
    EquatableArray<string> DependenciesOf
)
{
    /// <summary>A filter that matches nothing until something is added.</summary>
    public static AssemblyFilterData Empty { get; } = new(
        false,
        false,
        EquatableArray<string>.Empty,
        EquatableArray<string>.Empty,
        EquatableArray<string>.Empty
    );
}
