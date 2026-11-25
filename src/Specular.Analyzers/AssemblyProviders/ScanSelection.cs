using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Specular.Scanning;

namespace Specular.Analyzers.AssemblyProviders;

/// <summary>
///     One selector (a <c>GetAssemblies</c>, <c>GetTypes</c> or <c>Scan</c> call) as a Specular.Scanning request, scanned one assembly at a
///     time: Specular resolves and caches each call site's result per assembly.
/// </summary>
internal sealed class ScanSelection(ScanEnvironment environment, AssemblySelection assemblies, TypeSelection? types)
{
    private readonly ScanEnvironment _environment = environment;
    private AssemblyMatcher? _assemblyMatcher;

    public AssemblySelection Assemblies { get; } = assemblies;
    public TypeSelection? Types { get; } = types;

    /// <summary>The Specular.Scanning request: it keys the scan results kept in the build cache.</summary>
    public ScanRequest Request { get; } = new(types is null ? ScanKind.Assemblies : ScanKind.Types, assemblies.Filter, types?.Filter);

    public bool IsMatch(IAssemblySymbol assembly) => ( _assemblyMatcher ??= new(Assemblies.Filter, _environment.AllAssemblies) ).IsMatch(assembly);

    /// <summary>
    ///     The types this selection finds in <paramref name="assembly" />, in declaration order. A referenced assembly's types come from
    ///     <paramref name="cache" /> while the assembly is unchanged; the compilation's own assembly is always walked.
    /// </summary>
    public ImmutableArray<INamedTypeSymbol> GetTypes(IAssemblySymbol assembly, IAssemblyScanCache? cache, CancellationToken cancellationToken)
    {
        if (Types is null || !IsMatch(assembly)) return [];

        var compilation = _environment.Compilation;
        var isReference = !SymbolEqualityComparer.Default.Equals(assembly, compilation.Assembly);
        if (isReference && cache is { } && cache.TryGetTypes(Request, assembly, out var cached) && TryResolve(assembly, cached, out var resolved))
        {
            return resolved;
        }

        var matcher = Types.GetMatcher();
        var types = ImmutableArray.CreateBuilder<INamedTypeSymbol>();
        _ = TypeWalker.Walk(
            assembly.GlobalNamespace,
            type =>
            {
                if (matcher.IsMatch(type)) types.Add(type);
                return true;
            },
            cancellationToken
        );

        if (isReference)
        {
            cache?.SetTypes(
                Request,
                assembly,
                types
                   .Select(type => new ScannedType(
                               assembly.MetadataName,
                               ScanSymbolNames.GetFullMetadataName(type),
                               type.TypeKind,
                               type.DeclaredAccessibility,
                               compilation.IsSymbolAccessibleWithin(type, compilation.Assembly),
                               type.Arity
                           )
                    )
                   .ToImmutableArray()
            );
        }

        return types.ToImmutable();
    }

    private static bool TryResolve(IAssemblySymbol assembly, EquatableArray<ScannedType> scanned, out ImmutableArray<INamedTypeSymbol> types)
    {
        var builder = ImmutableArray.CreateBuilder<INamedTypeSymbol>(scanned.Count);
        foreach (var type in scanned)
        {
            if (ScanSymbolNames.FindType(assembly, type.MetadataName) is not { } symbol)
            {
                types = default;
                return false;
            }

            builder.Add(symbol);
        }

        types = builder.MoveToImmutable();
        return true;
    }
}
