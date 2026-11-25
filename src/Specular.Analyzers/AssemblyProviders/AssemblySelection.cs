using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using ScanAssemblyFilter = Specular.Scanning.AssemblyFilterData;
using WireAssemblyFilter = Specular.Analyzers.Configuration.AssemblyFilterData;

namespace Specular.Analyzers.AssemblyProviders;

/// <summary>
///     The assembly half of a selector: <see cref="Data" /> is what Specular writes into <c>[AssemblyMetadata]</c> for other assemblies,
///     <see cref="Filter" /> is what Specular.Scanning matches assemblies with.
/// </summary>
internal sealed class AssemblySelection
{
    private AssemblySelection(WireAssemblyFilter data, ScanAssemblyFilter filter)
    {
        Data = data;
        Filter = filter;
    }

    public WireAssemblyFilter Data { get; }
    public ScanAssemblyFilter Filter { get; }

    /// <summary>
    ///     A selection read from a referenced assembly's <c>[AssemblyMetadata]</c>. Assemblies this run cannot see are dropped from it, as
    ///     Specular has always done when loading those requests.
    /// </summary>
    public static AssemblySelection Load(WireAssemblyFilter data, IReadOnlyDictionary<string, IAssemblySymbol> visibleAssemblies)
    {
        var visible = new WireAssemblyFilter(
            data.AllAssembly,
            data.IncludeSystem,
            known(data.Assembly),
            known(data.NotAssembly),
            known(data.AssemblyDependencies)
        );
        return new(data, ToFilter(visible));

        ImmutableArray<string> known(ImmutableArray<string> names) =>
            names.IsDefaultOrEmpty ? [] : names.Where(visibleAssemblies.ContainsKey).ToImmutableArray();
    }

    internal static AssemblySelection Create(
        bool allAssemblies,
        bool includeSystemAssemblies,
        IEnumerable<IAssemblySymbol> assemblies,
        IEnumerable<IAssemblySymbol> notAssemblies,
        IEnumerable<IAssemblySymbol> dependenciesOf
    )
    {
        var data = new WireAssemblyFilter(allAssemblies, includeSystemAssemblies, names(assemblies), names(notAssemblies), names(dependenciesOf));
        return new(data, ToFilter(data));

        // The order (and the repeats) Specular has always written into [AssemblyMetadata].
        static ImmutableArray<string> names(IEnumerable<IAssemblySymbol> symbols) => symbols.Select(z => z.MetadataName).OrderBy(z => z).ToImmutableArray();
    }

    // A selector without any assembly clause has always meant every assembly; Specular.Scanning's empty filter matches nothing.
    private static ScanAssemblyFilter ToFilter(WireAssemblyFilter data)
    {
        var assemblies = orEmpty(data.Assembly);
        var notAssemblies = orEmpty(data.NotAssembly);
        var dependenciesOf = orEmpty(data.AssemblyDependencies);
        var hasClauses = data.AllAssembly || data.IncludeSystem || assemblies.Length > 0 || notAssemblies.Length > 0 || dependenciesOf.Length > 0;
        return new(data.AllAssembly || !hasClauses, data.IncludeSystem, assemblies, notAssemblies, dependenciesOf);

        static ImmutableArray<string> orEmpty(ImmutableArray<string> names) => names.IsDefault ? [] : names;
    }
}

/// <summary>Collects the assembly clauses of a selector written in source.</summary>
internal sealed class AssemblySelectionBuilder
{
    private readonly List<IAssemblySymbol> _assemblies = [];
    private readonly List<IAssemblySymbol> _notAssemblies = [];
    private readonly List<IAssemblySymbol> _dependenciesOf = [];
    private bool _allAssemblies;
    private bool _includeSystemAssemblies;

    public void FromAssemblies() => _allAssemblies = true;
    public void IncludeSystemAssemblies() => _includeSystemAssemblies = true;
    public void FromAssembly(IAssemblySymbol assembly) => _assemblies.Add(assembly);
    public void NotFromAssembly(IAssemblySymbol assembly) => _notAssemblies.Add(assembly);
    public void DependenciesOf(IAssemblySymbol assembly) => _dependenciesOf.Add(assembly);

    public AssemblySelection Build() =>
        AssemblySelection.Create(_allAssemblies, _includeSystemAssemblies, _assemblies, _notAssemblies, _dependenciesOf);
}
