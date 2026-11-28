using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Specular.Scanning;

namespace Specular.Analyzers.AssemblyProviders;

/// <summary>What one generator run scans against: the compilation and every assembly it can see, found the way Specular.Scanning finds them.</summary>
internal sealed class ScanEnvironment
{
    public ScanEnvironment(Compilation compilation, ScanAssemblyScope scope)
    {
        Compilation = compilation;
        Scope = scope;
        AllAssemblies = ScanAssemblies.ByName(ScanAssemblies.Get(compilation));
        Resolver = new(compilation, AllAssemblies);
    }

    public Compilation Compilation { get; }

    /// <summary>The assemblies a selector that does not name them looks in: the project's SpecularExcludeAssemblies / SpecularIncludeAssemblies over the framework default.</summary>
    public ScanAssemblyScope Scope { get; }

    /// <summary>
    ///     Every assembly by metadata name, including those <see cref="Scope" /> leaves out: a selector written in source names its
    ///     assemblies and types by symbol, so it can still refer to them, and a selector that names one scans it.
    /// </summary>
    public IReadOnlyDictionary<string, IAssemblySymbol> AllAssemblies { get; }

    /// <summary>Resolves type references taken from symbols in this compilation's source.</summary>
    public TypeReferenceResolver Resolver { get; }

    /// <summary>
    ///     One semantic model per syntax tree for the whole run. A new model per call site would bind the enclosing method again for
    ///     every scan call in it, which grows with the square of the calls in one method.
    /// </summary>
    public SemanticModel GetSemanticModel(SyntaxTree syntaxTree)
    {
        if (_semanticModels.TryGetValue(syntaxTree, out var semanticModel)) return semanticModel;

        semanticModel = Compilation.GetSemanticModel(syntaxTree);
        _semanticModels.Add(syntaxTree, semanticModel);
        return semanticModel;
    }

    /// <summary>
    ///     Every type the compilation's own assembly declares, in the order <see cref="TypeWalker" /> offers them, listed once for the run.
    ///     Each scan of the own assembly (one per local and inherited request) filters this list instead of walking the source again.
    /// </summary>
    public ImmutableArray<INamedTypeSymbol> GetOwnTypes(CancellationToken cancellationToken)
    {
        if (!_ownTypes.IsDefault) return _ownTypes;

        var types = ImmutableArray.CreateBuilder<INamedTypeSymbol>();
        _ = TypeWalker.Walk(
            Compilation.Assembly.GlobalNamespace,
            type =>
            {
                types.Add(type);
                return true;
            },
            cancellationToken
        );
        _ownTypes = types.ToImmutable();
        return _ownTypes;
    }

    private readonly Dictionary<SyntaxTree, SemanticModel> _semanticModels = [];
    private ImmutableArray<INamedTypeSymbol> _ownTypes;
}
