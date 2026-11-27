using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Specular.Analyzers.AssemblyProviders;
using Specular.Analyzers.Configuration;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

// ReSharper disable UseCollectionExpression

namespace Specular.Analyzers;

internal static class ReflectionCollection
{
    public static IncrementalValueProvider<ImmutableArray<(InvocationExpressionSyntax method, ExpressionSyntax selector)>> Create(
        SyntaxValueProvider valueProvider,
        IncrementalValueProvider<bool> hasAssemblyLoadContext
    ) => valueProvider
        .CreateSyntaxProvider((node, _) => IsValidMethod(node), (syntaxContext, _) => GetTypesMethod(syntaxContext))
        .Combine(hasAssemblyLoadContext)
        .Where(z => z is { Right: true, Left: { method: { }, selector: { } } })
        .Select((tuple, _) => tuple.Left)
        .Collect();

    public static (InvocationExpressionSyntax method, ExpressionSyntax selector) GetTypesMethod(GeneratorSyntaxContext context)
    {
        (var method, var selector) = GetTypesMethod(context.Node);
        // Structural detection only; genuine ISpecularProvider validation is deferred to GetReflectionItems, which
        // runs against the shell-augmented compilation. See SpecularProviderGenerator.CreateSemanticCompilation.
        return method is null || selector is null
            ? default
            : (method, selector);
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
        IAssemblySymbol targetAssembly,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return SymbolEqualityComparer.Default.Equals(targetAssembly, compilation.Assembly)
                ? resolvedSourceLocation()
                : configuration.Cache.GetOrAddSource(
                    item.Location,
                    item.Selection.Request,
                    targetAssembly,
                    () =>
                    {
                        var reported = diagnostics.Count;
                        return (resolvedSourceLocation(), diagnostics.Count == reported);
                    }
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
            var reducedTypes = item.Selection.GetTypes(targetAssembly, configuration.Cache.Scans, cancellationToken);
            if (reducedTypes.Length == 0) return null;

            var (localBlock, results) = GenerateDescriptors(configuration, diagnostics, reducedTypes, pa);
            return new(
                item.Location,
                GeneratedCodeLayout.ToText(localBlock),
                pa.Select(z => z.MetadataName).ToImmutableHashSet(),
                results,
                [],
                [],
                ScannedAssemblyName: targetAssembly.MetadataName
            );
        }
    }

    public static ImmutableList<ResolvedSourceLocation> ResolveSources(
        AssemblyProviderConfiguration configuration,
        Compilation compilation,
        HashSet<Diagnostic> diagnostics,
        IReadOnlyList<Item> items,
        IAssemblySymbol targetAssembly,
        CancellationToken cancellationToken
    )
    {
        if (items.Count == 0) return ImmutableList<ResolvedSourceLocation>.Empty;

        var results = new List<ResolvedSourceLocation>();
        foreach (var item in items)
        {
            if (ResolveSource(configuration, compilation, diagnostics, item, targetAssembly, cancellationToken) is not { } location) continue;

            results.Add(location);
        }

        return results.ToImmutableList();
    }

    public sealed record Item(SourceLocation Location, ScanSelection Selection);

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "A source generator must never crash the build; unexpected exceptions are surfaced as diagnostics.")]
    internal static ImmutableList<Item> GetReflectionItems(
        ScanEnvironment environment,
        HashSet<Diagnostic> diagnostics,
        IReadOnlyList<(InvocationExpressionSyntax expression, ExpressionSyntax selector)> results,
        CancellationToken cancellationToken
    )
    {
        var compilation = environment.Compilation;
        var items = ImmutableList.CreateBuilder<Item>();
        foreach (var tuple in results)
        {
            try
            {
                (var methodCallSyntax, var selector) = tuple;

                var semanticModel = environment.GetSemanticModel(methodCallSyntax.SyntaxTree);
                if (!Helpers.IsSpecularProviderCall(semanticModel, methodCallSyntax)) continue;

                var assemblies = new AssemblySelectionBuilder();
                var typeFilters = new TypeSelectionBuilder(environment);
                var lifetime = 2;

                DataHelpers.HandleInvocationExpressionSyntax(
                    diagnostics,
                    semanticModel,
                    selector,
                    environment,
                    assemblies,
                    typeFilters,
                    [],
                    ref lifetime,
                    cancellationToken
                );

                var source = Helpers.CreateSourceLocation(SourceLocationKind.Reflection, methodCallSyntax, compilation.Assembly.MetadataName, cancellationToken);
                var i = new Item(source, new(environment, assemblies.Build(), typeFilters.Build()));
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


    private sealed record TypeScanGeneration(BlockSyntax Block, ImmutableList<TypeScanReportEntryData> ReportEntries);

    private static TypeScanGeneration GenerateDescriptors(AssemblyProviderConfiguration configuration, HashSet<Diagnostic> diagnostics, IEnumerable<INamedTypeSymbol> types, HashSet<IAssemblySymbol> privateAssemblies)
    {
        var compilation = configuration.Compilation;
        var block = Block();
        var reportEntries = ImmutableList.CreateBuilder<TypeScanReportEntryData>();
        foreach (var type in types.OrderBy(Helpers.GetGenericDisplayName))
        {
            if (configuration.IsAot && StatementGeneration.GetUnreachableType(compilation, type) is { } unreachable)
            {
                _ = diagnostics.Add(
                    Diagnostic.Create(Diagnostics.PrivateTypeUnreachableUnderAot, Location.None, Helpers.GetGenericDisplayName(type), type.ContainingAssembly.Identity.Name)
                );
                continue;
            }

            var privateType = StatementGeneration.GetTypeOfExpression(compilation, type, true);
            if (privateType is { })
            {
                block = block.AddStatements(
                    ExpressionStatement(
                        InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("items"), IdentifierName("Add")))
                           .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(privateType))))
                    )
                );
                reportEntries.Add(new(Helpers.GetGenericDisplayName(type)));
            }
            else
            {
                // add a comment stating we could reach the private type
                block = block.AddStatements(
                    EmptyStatement().WithTrailingTrivia(TriviaList(Comment($"// Could not generate typeof expression for {Helpers.GetGenericDisplayName(type)}")))
                );
            }

            if (compilation.IsSymbolAccessibleWithin(type, compilation.Assembly)) continue;

            _ = privateAssemblies.Add(type.ContainingAssembly);
        }

        return new(block, reportEntries.ToImmutable());
    }

    private static bool IsValidMethod(SyntaxNode node) => GetTypesMethod(node) is { method: { }, selector: { } };

    private const string IReflectionTypeSelector = nameof(IReflectionTypeSelector);
}
