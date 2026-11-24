using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>Selects the assemblies to scan; the compile-time mirror of Specular's <c>IReflectionAssemblySelector</c>.</summary>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
interface IAssemblySymbolSelector
{
    /// <summary>The compilation's own assembly.</summary>
    ITypeSymbolSelector EntryAssembly();

    /// <summary>Every referenced assembly and the compilation's own.</summary>
    ITypeSymbolSelector FromAssemblies();

    /// <summary>The given assembly.</summary>
    ITypeSymbolSelector FromAssembly(IAssemblySymbol assembly);

    /// <summary>The assembly with the given metadata name.</summary>
    ITypeSymbolSelector FromAssembly(string assemblyName);

    /// <summary>The assembly that contains <paramref name="type" />.</summary>
    ITypeSymbolSelector FromAssemblyOf(ITypeSymbol type);

    /// <summary>Excludes the given assembly.</summary>
    ITypeSymbolSelector NotFromAssembly(IAssemblySymbol assembly);

    /// <summary>Excludes the assembly with the given metadata name.</summary>
    ITypeSymbolSelector NotFromAssembly(string assemblyName);

    /// <summary>Excludes the assembly that contains <paramref name="type" />.</summary>
    ITypeSymbolSelector NotFromAssemblyOf(ITypeSymbol type);

    /// <summary>Every assembly that references the given assembly.</summary>
    ITypeSymbolSelector DependenciesFromAssembly(IAssemblySymbol assembly);

    /// <summary>Every assembly that references the assembly with the given metadata name.</summary>
    ITypeSymbolSelector DependenciesFromAssembly(string assemblyName);

    /// <summary>Every assembly that references the assembly containing <paramref name="type" />.</summary>
    ITypeSymbolSelector DependenciesFromAssemblyOf(ITypeSymbol type);

    /// <summary>Includes core assemblies (mscorlib, netstandard, System.Runtime, ...), which are skipped by default.</summary>
    ITypeSymbolSelector IncludeSystemAssemblies();
}
