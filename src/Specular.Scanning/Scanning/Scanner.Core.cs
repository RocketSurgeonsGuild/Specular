using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

// Results are ordered so equal inputs give equal outputs regardless of reference order: assemblies by name, types by assembly name
// then full metadata name, all ordinal.
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
static partial class Scanner
{
    private static partial ScanResult ScanCore(Compilation compilation, ScanRequest request, IAssemblyScanCache? cache, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var assemblies = ScanAssemblies.Get(compilation);
        var byName = ScanAssemblies.ByName(assemblies);
        var assemblyMatcher = new AssemblyMatcher(request.Assemblies, byName);
        var passing = new List<IAssemblySymbol>();
        foreach (var assembly in assemblies)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (assemblyMatcher.IsMatch(assembly)) passing.Add(assembly);
        }

        passing.Sort(static (left, right) => StringComparer.Ordinal.Compare(left.MetadataName, right.MetadataName));

        if (request.Kind != ScanKind.Types)
        {
            return new(request, passing.Select(static z => new ScannedAssembly(z.MetadataName, false)).ToImmutableArray(), EquatableArray<ScannedType>.Empty);
        }

        // Built on first use: when every assembly comes from the cache, the filter's type references never need resolving.
        TypeMatcher? typeMatcher = null;
        var scannedAssemblies = ImmutableArray.CreateBuilder<ScannedAssembly>(passing.Count);
        var types = new List<ScannedType>();
        foreach (var assembly in passing)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var hasInaccessibleMatches = false;
            foreach (var type in GetAssemblyTypes(assembly).AsImmutableArray())
            {
                types.Add(type);
                hasInaccessibleMatches |= !type.IsAccessible;
            }

            scannedAssemblies.Add(new(assembly.MetadataName, hasInaccessibleMatches));
        }

        return new(request, scannedAssemblies.MoveToImmutable(), types.OrderBy(static z => z.AssemblyName, StringComparer.Ordinal).ThenBy(static z => z.MetadataName, StringComparer.Ordinal).ToImmutableArray());

        EquatableArray<ScannedType> GetAssemblyTypes(IAssemblySymbol assembly)
        {
            // The compilation's own assembly changes with every edit, so only references go through the cache.
            var isReference = !SymbolEqualityComparer.Default.Equals(assembly, compilation.Assembly);
            if (isReference && cache is { } && cache.TryGetTypes(request, assembly, out var cached)) return cached;

            typeMatcher ??= new(compilation, request.Types ?? TypeFilterData.All, new(compilation, byName));
            var walked = WalkAssembly(compilation, assembly, typeMatcher, cancellationToken);
            if (isReference) cache?.SetTypes(request, assembly, walked);
            return walked;
        }
    }

    private static EquatableArray<ScannedType> WalkAssembly(Compilation compilation, IAssemblySymbol assembly, TypeMatcher typeMatcher, CancellationToken cancellationToken)
    {
        var matches = new List<ScannedType>();
        _ = TypeWalker.Walk(
            assembly.GlobalNamespace,
            type =>
            {
                if (typeMatcher.IsMatch(type))
                {
                    matches.Add(
                        new(
                            assembly.MetadataName,
                            ScanSymbolNames.GetFullMetadataName(type),
                            type.TypeKind,
                            type.DeclaredAccessibility,
                            compilation.IsSymbolAccessibleWithin(type, compilation.Assembly),
                            type.Arity
                        )
                    );
                }

                return true;
            },
            cancellationToken
        );

        return matches.OrderBy(static z => z.MetadataName, StringComparer.Ordinal).ToImmutableArray();
    }
}
