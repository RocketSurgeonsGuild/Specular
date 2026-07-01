using System.Collections.Immutable;
using Indago.Analyzers.AssemblyProviders;
using Indago.Analyzers.Configuration;
using Indago.Analyzers.Descriptors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Indago.Analyzers;

internal static class AssemblyCollection
{
    public static IncrementalValueProvider<ImmutableArray<(InvocationExpressionSyntax method, ExpressionSyntax selector, SemanticModel semanticModel)>> Create(
        SyntaxValueProvider valueProvider,
        IncrementalValueProvider<bool> hasAssemblyLoadContext
    ) => valueProvider
        .CreateSyntaxProvider((node, _) => IsValidMethod(node), (syntaxContext, _) => GetMethod(syntaxContext))
        .Combine(hasAssemblyLoadContext)
        .Where(z => z is { Right: true, Left: { method: { }, selector: { } } })
        .Select((tuple, _) => tuple.Left)
        .Collect();

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "A source generator must never crash the build; unexpected exceptions are surfaced as diagnostics.")]
    public static ImmutableList<Item> GetAssemblyItems(
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
                // The compilation passed here is augmented with an IndagoProvider shell so the receiver binds.
                // Only treat this as a scan when the call actually resolves to IIndagoProvider; otherwise a
                // syntactically-similar call would produce an empty (match-everything) filter.
                if (!Helpers.IsIndagoProviderCall(semanticModel, methodCallSyntax)) continue;

                var assemblies = new List<IAssemblyDescriptor>();
                var typeFilters = new List<ITypeFilterDescriptor>();
                var serviceDescriptors = new List<IServiceTypeDescriptor>();
                var lifetime = 2;
                var classFilter = ClassFilter.All;

                DataHelpers.HandleInvocationExpressionSyntax(
                    diagnostics,
                    semanticModel,
                    selector,
                    assemblies,
                    typeFilters,
                    serviceDescriptors,
                    ref lifetime,
                    ref classFilter,
                    cancellationToken
                );

                var assemblyFilter = new CompiledAssemblyFilter(assemblies.ToImmutableList());

                var source = Helpers.CreateSourceLocation(SourceLocationKind.Assembly, methodCallSyntax, cancellationToken);
                // disallow list?
                if (source.FileName == "ConventionContextHelpers.cs") continue;

                var i = new Item(source, assemblyFilter);
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
                        e.StackTrace,
                        e.GetType().Name,
                        e.ToString()
                    )
                );
            }
        }

        return items.ToImmutable();
    }

    public static (InvocationExpressionSyntax method, ExpressionSyntax selector, SemanticModel semanticModel) GetMethod(
        GeneratorSyntaxContext context
    )
    {
        (var method, var selector) = GetMethod(context.Node);
        // Detection is intentionally structural only. The selector's converted type cannot be resolved here when
        // the call is written against the generated IndagoProvider (which does not exist yet during generation),
        // so validation that this is a genuine IIndagoProvider call is deferred to GetAssemblyItems, which runs
        // against a shell-augmented compilation. See IndagoProviderGenerator.CreateSemanticCompilation.
        return method is null || selector is null
            ? default
            : (method, selector, semanticModel: context.SemanticModel);
    }

    public static (InvocationExpressionSyntax? method, ExpressionSyntax? selector) GetMethod(SyntaxNode node) =>
        node is InvocationExpressionSyntax
        {
            Expression: MemberAccessExpressionSyntax { Name.Identifier.Text: "GetAssemblies" },
            ArgumentList.Arguments: [{ Expression: { } expression }],
        } invocationExpressionSyntax
            ? (invocationExpressionSyntax, expression)
            : default;

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "A source generator must never crash the build; unexpected exceptions are surfaced as diagnostics.")]
    public static ImmutableList<ResolvedSourceLocation> ResolveSources(
        AssemblyProviderConfiguration configuration,
        Compilation compilation,
        HashSet<Diagnostic> diagnostics,
        ImmutableList<Item> items,
        ImmutableDictionary<string, IAssemblySymbol> assemblySymbols
    )
    {
        var results = new List<ResolvedSourceLocation>();
        foreach (var item in items)
        {
            var pa = new HashSet<IAssemblySymbol>(SymbolEqualityComparer.Default);
            try
            {
                var filterAssemblies = assemblySymbols
                                      .Values
                                      .Where(z => item.AssemblyFilter.IsMatch(compilation, z))
                                      .ToArray();

                if (filterAssemblies.Length == 0) continue;

                var descriptors = GenerateDescriptors(configuration, diagnostics, filterAssemblies, pa).NormalizeWhitespace().ToFullString().Replace("\r", "");
                results.Add(new(item.Location, descriptors, pa.Select(z => z.MetadataName).ToImmutableHashSet(), ""));
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

        return results.ToImmutableList();
        //        .WithBody(Block(SwitchGenerator.GenerateSwitchStatement(results)));
    }

    public sealed record Item(SourceLocation Location, CompiledAssemblyFilter AssemblyFilter);

    private static BlockSyntax GenerateDescriptors(AssemblyProviderConfiguration configuration, HashSet<Diagnostic> diagnostics, IEnumerable<IAssemblySymbol> assemblies, HashSet<IAssemblySymbol> privateAssemblies)
    {
        var compilation = configuration.Compilation;
        var block = Block();
        foreach (var assembly in assemblies.OrderBy(z => z.ToDisplayString()))
        {
            // TODO: Make this always use the load context?
            if (StatementGeneration.GetAssemblyExpression(compilation, assembly) is not { } assemblyExpression)
            {
                if (configuration.IsAot)
                {
                    _ = diagnostics.Add(
                        Diagnostic.Create(Diagnostics.PrivateTypeUnreachableUnderAot, Location.None, assembly.Identity.Name, assembly.Identity.Name)
                    );
                    continue;
                }

                privateAssemblies.Add(assembly);
                block = block.AddStatements(
                    ExpressionStatement(
                        InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("items"), IdentifierName("Add")))
                           .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(StatementGeneration.GetPrivateAssembly(assembly)))))
                    )
                );
                continue;
            }

            block = block.AddStatements(
                ExpressionStatement(
                    InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("items"), IdentifierName("Add")))
                       .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(assemblyExpression))))
                )
            );
        }

        return block;
    }

    private static bool IsValidMethod(SyntaxNode node) => GetMethod(node) is { method: { }, selector: { } };

    private const string IReflectionAssemblySelector = nameof(IReflectionAssemblySelector);
}
