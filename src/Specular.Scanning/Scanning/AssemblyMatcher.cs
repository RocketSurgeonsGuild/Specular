using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     <see cref="AssemblyFilterData" /> applied to assemblies, with Specular's semantics: core assemblies are skipped unless included,
///     and every clause must hold, so a not-assembly removes an assembly whatever else selected it.
/// </summary>
internal sealed class AssemblyMatcher
{
    // Same list as Specular's CompiledAssemblyFilter, compared with IAssemblySymbol.Name as Specular does. An array, not a set: a
    // set with a comparer gets rewritten to a collection-expression argument, which the C# 14 floor of the Sources package lacks.
    private static readonly string[] coreAssemblies =
    [
        "mscorlib",
        "netstandard",
        "System",
        "System.Core",
        "System.Runtime",
        "System.Private.CoreLib",
    ];

    private readonly AssemblyFilterData _filter;
    private readonly bool _matchesNothing;
    private readonly List<IAssemblySymbol?> _dependenciesOf;

    public AssemblyMatcher(AssemblyFilterData filter, IReadOnlyDictionary<string, IAssemblySymbol> assemblies)
    {
        _filter = filter;
        // Specular never builds a filter without clauses from syntax; the contract documents the empty filter as matching nothing.
        _matchesNothing = !filter.AllAssemblies
         && !filter.IncludeSystemAssemblies
         && filter.Assemblies.Count == 0
         && filter.NotAssemblies.Count == 0
         && filter.DependenciesOf.Count == 0;
        _dependenciesOf = [];
        foreach (var name in filter.DependenciesOf)
        {
            // An unknown name stays as a clause that nothing satisfies, rather than being dropped and widening the scan.
            _dependenciesOf.Add(assemblies.TryGetValue(name, out var assembly) ? assembly : null);
        }
    }

    /// <summary>Whether <paramref name="assemblyName" /> (an <c>IAssemblySymbol.Name</c>) is one of the core assemblies scans skip by default.</summary>
    public static bool IsCoreAssembly(string assemblyName)
    {
        foreach (var coreAssembly in coreAssemblies)
        {
            if (string.Equals(coreAssembly, assemblyName, StringComparison.Ordinal)) return true;
        }

        return false;
    }

    public bool IsMatch(IAssemblySymbol assembly)
    {
        if (_matchesNothing) return false;
        if (!_filter.IncludeSystemAssemblies && IsCoreAssembly(assembly.Name)) return false;

        foreach (var name in _filter.Assemblies)
        {
            if (!string.Equals(assembly.MetadataName, name, StringComparison.Ordinal)) return false;
        }

        foreach (var name in _filter.NotAssemblies)
        {
            if (string.Equals(assembly.MetadataName, name, StringComparison.Ordinal)) return false;
        }

        if (_dependenciesOf.Count == 0) return true;

        var referenced = new HashSet<IAssemblySymbol>(SymbolEqualityComparer.Default);
        foreach (var module in assembly.Modules)
        {
            foreach (var referencedAssembly in module.ReferencedAssemblySymbols)
            {
                referenced.Add(referencedAssembly);
            }
        }

        foreach (var dependency in _dependenciesOf)
        {
            if (dependency is null || !referenced.Contains(dependency)) return false;
        }

        return true;
    }
}
