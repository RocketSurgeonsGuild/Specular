using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed partial record ScanResult
{
    private partial ScanSymbols ResolveCore(Compilation compilation)
    {
        var byName = ScanAssemblies.ByName(ScanAssemblies.Get(compilation));

        var assemblies = ImmutableArray.CreateBuilder<IAssemblySymbol>(Assemblies.Count);
        foreach (var scanned in Assemblies.AsImmutableArray())
        {
            if (byName.TryGetValue(scanned.Name, out var assembly)) assemblies.Add(assembly);
        }

        var types = ImmutableArray.CreateBuilder<INamedTypeSymbol>(Types.Count);
        foreach (var scanned in Types.AsImmutableArray())
        {
            if (byName.TryGetValue(scanned.AssemblyName, out var assembly) && ScanSymbolNames.FindType(assembly, scanned.MetadataName) is { } type)
            {
                types.Add(type);
            }
        }

        return new(assemblies.ToImmutable(), types.ToImmutable());
    }
}
