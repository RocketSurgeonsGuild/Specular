using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>The assemblies a scan can see, built the way Specular's generator builds its assembly set.</summary>
internal static class ScanAssemblies
{
    /// <summary>
    ///     The compilation's references (a module reference stands for its containing assembly) followed by the compilation's own
    ///     assembly, keeping the first assembly for each metadata name. The order is the compilation's reference order.
    /// </summary>
    public static ImmutableArray<IAssemblySymbol> Get(Compilation compilation)
    {
        var seen = new HashSet<string>(StringComparer.Ordinal);
        var assemblies = ImmutableArray.CreateBuilder<IAssemblySymbol>();
        foreach (var reference in compilation.References)
        {
            add(compilation.GetAssemblyOrModuleSymbol(reference));
        }

        add(compilation.Assembly);
        return assemblies.ToImmutable();

        void add(ISymbol? symbol)
        {
            var assembly = symbol switch
            {
                IAssemblySymbol assemblySymbol => assemblySymbol,
                IModuleSymbol moduleSymbol => moduleSymbol.ContainingAssembly,
                _ => null,
            };
            if (assembly is { } && seen.Add(assembly.MetadataName)) assemblies.Add(assembly);
        }
    }

    /// <summary>The same set keyed by metadata name.</summary>
    public static Dictionary<string, IAssemblySymbol> ByName(ImmutableArray<IAssemblySymbol> assemblies)
    {
        var byName = new Dictionary<string, IAssemblySymbol>(assemblies.Length, StringComparer.Ordinal);
        foreach (var assembly in assemblies)
        {
            byName[assembly.MetadataName] = assembly;
        }

        return byName;
    }
}
