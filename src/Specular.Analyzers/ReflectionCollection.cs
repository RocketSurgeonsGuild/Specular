using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Specular.Analyzers.AssemblyProviders;
using Specular.Analyzers.Configuration;
using Specular.Analyzers.Descriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

// ReSharper disable UseCollectionExpression

namespace Specular.Analyzers;

internal static class ReflectionCollection
{
    public static IncrementalValueProvider<ImmutableArray<(InvocationExpressionSyntax method, ExpressionSyntax selector, SemanticModel semanticModel)>> Create(
        SyntaxValueProvider valueProvider,
        IncrementalValueProvider<bool> hasAssemblyLoadContext
    ) => valueProvider
        .CreateSyntaxProvider((node, _) => IsValidMethod(node), (syntaxContext, _) => GetTypesMethod(syntaxContext))
        .Combine(hasAssemblyLoadContext)
        .Where(z => z is { Right: true, Left: { method: { }, selector: { } } })
        .Select((tuple, _) => tuple.Left)
        .Collect();

    public static (InvocationExpressionSyntax method, ExpressionSyntax selector, SemanticModel semanticModel) GetTypesMethod(GeneratorSyntaxContext context)
    {
        (var method, var selector) = GetTypesMethod(context.Node);
        // Structural detection only; genuine ISpecularProvider validation is deferred to GetReflectionItems, which
        // runs against the shell-augmented compilation. See SpecularProviderGenerator.CreateSemanticCompilation.
        return method is null || selector is null
            ? default
            : (method, selector, semanticModel: context.SemanticModel);
    }

    public static (InvocationExpressionSyntax method, ExpressionSyntax selector) GetTypesMethod(SyntaxNode node) =>
        node is InvocationExpressionSyntax
        {
            Expression: MemberAccessExpressionSyntax { Name.Identifier.Text: "GetTypes" },
            ArgumentList.Arguments: [.., { Expression: { } expression }],
        } invocationExpressionSyntax
            ? (invocationExpressionSyntax, expression)
            : default;

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "A source generator must never crash the build; unexpected exceptions are surfaced as diagnostics.")]
    public static ResolvedSourceLocation? ResolveSource(
        AssemblyProviderConfiguration configuration,
        Compilation compilation,
        HashSet<Diagnostic> diagnostics,
        Item item,
        IAssemblySymbol targetAssembly
    )
    {
        try
        {
            return SymbolEqualityComparer.Default.Equals(targetAssembly, compilation.Assembly)
                ? resolvedSourceLocation()
                : configuration.CacheSourceLocation(
                    item.Location,
                    targetAssembly,
                    resolvedSourceLocation
                );
        }
        catch (Exception e)
        {
            _ = diagnostics.Add(
                Diagnostic.Create(
                    Diagnostics.UnhandledException,
                    null,
                    e.Message,
                    e.StackTrace.Replace("\r", "").Replace("\n", ""),
                    e.GetType().Name,
                    e.ToString()
                )
            );
            return null;
        }

        ResolvedSourceLocation? resolvedSourceLocation()
        {
            var pa = new HashSet<IAssemblySymbol>(SymbolEqualityComparer.Default);
            var reducedTypes = new TypeSymbolVisitor(compilation, item.AssemblyFilter, item.TypeFilter)
                              .GetReferencedTypes(targetAssembly)
                              .GetTypes();
            if (reducedTypes.Count == 0) return null;

            var localBlock = GenerateDescriptors(configuration, diagnostics, reducedTypes, pa).NormalizeWhitespace().ToFullString().Replace("\r", "");
            var discoveredTypes = reducedTypes.Select(z => new ScanReportTypeData(z.ContainingAssembly.MetadataName, Helpers.GetFullMetadataName(z))).ToImmutableArray();
            return new(item.Location, localBlock, pa.Select(z => z.MetadataName).ToImmutableHashSet(), targetAssembly.GetCachedVersion(), discoveredTypes);
        }
    }

    public static ImmutableList<ResolvedSourceLocation> ResolveSources(
        AssemblyProviderConfiguration configuration,
        Compilation compilation,
        HashSet<Diagnostic> diagnostics,
        IReadOnlyList<Item> items,
        IAssemblySymbol targetAssembly
    )
    {
        if (!items.Any()) return ImmutableList<ResolvedSourceLocation>.Empty;

        var results = new List<ResolvedSourceLocation>();
        foreach (var item in items)
        {
            if (ResolveSource(configuration, compilation, diagnostics, item, targetAssembly) is not { } location) continue;

            results.Add(location);
        }

        return results.ToImmutableList();
    }

    public sealed record Item(SourceLocation Location, CompiledAssemblyFilter AssemblyFilter, CompiledTypeFilter TypeFilter);

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "A source generator must never crash the build; unexpected exceptions are surfaced as diagnostics.")]
    internal static ImmutableList<Item> GetReflectionItems(
        Compilation compilation,
        HashSet<Diagnostic> diagnostics,
        IReadOnlyList<(InvocationExpressionSyntax expression, ExpressionSyntax selector, SemanticModel semanticModel)> results,
        CancellationToken cancellationToken
    )
    {
        var items = ImmutableList.CreateBuilder<Item>();
        foreach (var tuple in results)
        {
            try
            {
                (var methodCallSyntax, var selector, _) = tuple;

                var semanticModel = compilation.GetSemanticModel(tuple.expression.SyntaxTree);
                if (!Helpers.IsSpecularProviderCall(semanticModel, methodCallSyntax)) continue;

                var assemblies = new List<IAssemblyDescriptor>();
                var typeFilters = new List<ITypeFilterDescriptor>();
                var classFilter = ClassFilter.All;
                var lifetime = 2;

                DataHelpers.HandleInvocationExpressionSyntax(
                    diagnostics,
                    semanticModel,
                    selector,
                    assemblies,
                    typeFilters,
                    [],
                    ref lifetime,
                    ref classFilter,
                    cancellationToken
                );

                var source = Helpers.CreateSourceLocation(SourceLocationKind.Reflection, methodCallSyntax, cancellationToken);
                var assemblyFilter = new CompiledAssemblyFilter(assemblies.ToImmutableList(), source);
                var typeFilter = new CompiledTypeFilter(classFilter, typeFilters.ToImmutableList(), source);

                var i = new Item(source, assemblyFilter, typeFilter);
                items.Add(i);
            }
            catch (MustBeAnExpressionException e)
            {
                _ = diagnostics.Add(Diagnostic.Create(Diagnostics.MustBeAnExpression, e.Location));
            }
            catch (Exception e)
            {
                _ = diagnostics.Add(
                    Diagnostic.Create(
                        Diagnostics.UnhandledException,
                        null,
                        e.Message,
                        e.StackTrace.Replace("\r", "").Replace("\n", ""),
                        e.GetType().Name,
                        e.ToString()
                    )
                );
            }
        }

        return items.ToImmutable();
    }

    private static BlockSyntax GenerateDescriptors(AssemblyProviderConfiguration configuration, HashSet<Diagnostic> diagnostics, IEnumerable<INamedTypeSymbol> types, HashSet<IAssemblySymbol> privateAssemblies)
    {
        var compilation = configuration.Compilation;
        var block = Block();
        foreach (var type in types.OrderBy(z => z.ToDisplayString()))
        {
            if (configuration.IsAot && StatementGeneration.GetUnreachableType(compilation, type) is { } unreachable)
            {
                _ = diagnostics.Add(
                    Diagnostic.Create(Diagnostics.PrivateTypeUnreachableUnderAot, Location.None, unreachable.ToDisplayString(), unreachable.ContainingAssembly.Identity.Name)
                );
                continue;
            }

            block = block.AddStatements(
                ExpressionStatement(
                    InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("items"), IdentifierName("Add")))
                       .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(StatementGeneration.GetTypeOfExpression(compilation, type)))))
                )
            );
            if (compilation.IsSymbolAccessibleWithin(type, compilation.Assembly)) continue;

            _ = privateAssemblies.Add(type.ContainingAssembly);
        }

        return block;
    }

    private static bool IsValidMethod(SyntaxNode node) => GetTypesMethod(node) is { method: { }, selector: { } };

    private const string IReflectionTypeSelector = nameof(IReflectionTypeSelector);
}
