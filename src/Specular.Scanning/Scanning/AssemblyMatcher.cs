using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     <see cref="AssemblyFilterData" /> applied to assemblies. Core assemblies are skipped unless included, and a not-assembly removes an
///     assembly whatever selected it. Specific assemblies and dependencies-of are alternatives to each other (from A or B, or from A or
///     the assemblies that reference C). All-assemblies is the starting point those narrow, as in <c>FromAssemblies().DependenciesFromAssemblyOf&lt;T&gt;()</c>,
///     so it selects everything only when there is no specific selection; with only exclusions, everything not excluded matches.
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
    private readonly bool _hasSpecificSelection;
    private readonly List<IAssemblySymbol> _dependenciesOf;

    public AssemblyMatcher(AssemblyFilterData filter, IReadOnlyDictionary<string, IAssemblySymbol> assemblies)
    {
        _filter = filter;
        // Specular never builds a filter without clauses from syntax; the contract documents the empty filter as matching nothing.
        _matchesNothing = !filter.AllAssemblies
         && !filter.IncludeSystemAssemblies
         && filter.Assemblies.Count == 0
         && filter.NotAssemblies.Count == 0
         && filter.DependenciesOf.Count == 0;
        _hasSpecificSelection = filter.Assemblies.Count > 0 || filter.DependenciesOf.Count > 0;
        _dependenciesOf = [];
        foreach (var name in filter.DependenciesOf)
        {
            // An unknown name selects nothing; it still counts as a specific selection, so it never widens the scan to everything.
            if (assemblies.TryGetValue(name, out var assembly)) _dependenciesOf.Add(assembly);
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

        foreach (var name in _filter.NotAssemblies)
        {
            if (string.Equals(assembly.MetadataName, name, StringComparison.Ordinal)) return false;
        }

        // No specific selection: everything not excluded, whether from FromAssemblies() or from exclusions alone.
        if (!_hasSpecificSelection) return true;

        foreach (var name in _filter.Assemblies)
        {
            if (string.Equals(assembly.MetadataName, name, StringComparison.Ordinal)) return true;
        }

        return _dependenciesOf.Count > 0 && ReferencesAnyDependency(assembly);
    }

    private bool ReferencesAnyDependency(IAssemblySymbol assembly)
    {
        foreach (var module in assembly.Modules)
        {
            foreach (var referencedAssembly in module.ReferencedAssemblySymbols)
            {
                foreach (var dependency in _dependenciesOf)
                {
                    if (SymbolEqualityComparer.Default.Equals(dependency, referencedAssembly)) return true;
                }
            }
        }

        return false;
    }
}
