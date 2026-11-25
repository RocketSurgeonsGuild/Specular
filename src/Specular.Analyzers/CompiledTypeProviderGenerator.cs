using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Specular.Analyzers.AssemblyProviders;
using Specular.Analyzers.Configuration;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Specular.Analyzers;

/// <summary>
///     Source generate used for scanning assemblies for registrations
/// </summary>
[Generator]
public class SpecularProviderGenerator : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(z => z.AddEmbeddedAttributeDefinition());
        var hasAssemblyLoadContext =
            context.CompilationProvider.Select((compilation, _) => compilation.GetTypeByMetadataName("System.Runtime.Loader.AssemblyLoadContext") is { });
        var assembliesSyntaxProvider = AssemblyCollection.Create(context.SyntaxProvider, hasAssemblyLoadContext);
        var reflectionSyntaxProvider = ReflectionCollection.Create(context.SyntaxProvider, hasAssemblyLoadContext);
        var serviceDescriptorSyntaxProvider = ServiceDescriptorCollection.Create(context.SyntaxProvider, hasAssemblyLoadContext);
        var collectionProvider = assembliesSyntaxProvider
                                .Combine(reflectionSyntaxProvider)
                                .Combine(serviceDescriptorSyntaxProvider)
                                .Select((z, _) => (assemblies: z.Left.Left, reflection: z.Left.Right, serviceDescriptors: z.Right));
        // The build cache from the previous build (obj/ctp/SpecularProvider.ctpjson), parsed only when its text changes.
        var cacheFileProvider = context
                               .AdditionalTextsProvider.Where(SpecularCache.IsCacheFile)
                               .Select((text, cancellationToken) => text.GetText(cancellationToken)?.ToString())
                               .Collect()
                               .Select((texts, _) => texts.FirstOrDefault())
                               .Select((text, _) => SpecularCache.Parse(text));

        // Specular calls Specular.Scanning directly from this one implementation output rather than through context.Scan: its requests
        // come from syntax across call sites and from referenced assemblies' [AssemblyMetadata], both only known with the compilation in
        // hand; what it generates needs the matched symbols, not just their names; and its own cache entries (the requests above and
        // each call site's result) must go into the same file as the scan results, which only one ScanCache per run can write.
        context.RegisterImplementationSourceOutput(
            context
               .CompilationProvider
               .Combine(context.AnalyzerConfigOptionsProvider)
               .Combine(collectionProvider)
               .Combine(cacheFileProvider)
               .Select((tuple, _) => (
                           compilation: tuple.Left.Left.Left,
                           options: tuple.Left.Left.Right,
                           tuple.Left.Right.assemblies,
                           tuple.Left.Right.reflection,
                           tuple.Left.Right.serviceDescriptors,
                           cacheFile: tuple.Right
                       )
                ),
            static (context, request) =>
            {
                static bool getMsBuildBoolean(AnalyzerConfigOptions options, string propertyName, bool @default) =>
                    options.TryGetValue($"build_property.{propertyName}", out var value) && bool.TryParse(value, out var parsedValue) ? parsedValue : @default;

                HashSet<string> excludedAssemblies = request.options.GlobalOptions.TryGetValue("build_property.ExcludeAssemblyFromCTP", out var assemblies)
                    ? [.. assemblies.Split([';', ','], StringSplitOptions.RemoveEmptyEntries)]
                    : [];

                var emitProvider = getMsBuildBoolean(request.options.GlobalOptions, "SpecularEmitProvider", true);
                var generateScanReport = getMsBuildBoolean(request.options.GlobalOptions, "SpecularGenerateDiagnostics", true);
                var privateAssemblies = new HashSet<IAssemblySymbol>(SymbolEqualityComparer.Default);
                var diagnostics = new HashSet<Diagnostic>();

                var environment = new ScanEnvironment(request.compilation);
                var isAot = StatementGeneration.IsAotCompilation(request.options);
                var cache = new SpecularCache(request.compilation, request.cacheFile, isAot);

                var assemblyRequests = AssemblyCollection.GetAssemblyItems(environment, diagnostics, request.assemblies, context.CancellationToken);
                var reflectionRequests = ReflectionCollection.GetReflectionItems(
                    environment,
                    diagnostics,
                    request.reflection,
                    context.CancellationToken
                );
                var serviceDescriptorRequests = ServiceDescriptorCollection.GetServiceDescriptorItems(
                    environment,
                    diagnostics,
                    request.serviceDescriptors,
                    context.CancellationToken
                );
                var attributes = AssemblyProviderConfiguration.ToAssemblyAttributes(context, assemblyRequests, reflectionRequests, serviceDescriptorRequests).ToArray();

                var assemblySymbols = request
                                     .compilation
                                     .References
                                     .Select(request.compilation.GetAssemblyOrModuleSymbol)
                                     .Concat([request.compilation.Assembly])
                                     .Select(symbol =>
                                             {
                                                 if (symbol is IAssemblySymbol assemblySymbol) return assemblySymbol;
                                                 if (symbol is IModuleSymbol moduleSymbol) return moduleSymbol.ContainingAssembly;
                                                 // ReSharper disable once NullableWarningSuppressionIsUsed
                                                 return null!;
                                             }
                                      )
                                     .Where(z => z is { })
                                     .Where(z => excludedAssemblies.All(a => !z.MetadataName.StartsWith(a, StringComparison.OrdinalIgnoreCase)))
                                     .GroupBy(z => z.MetadataName, z => z, (s, symbols) => (Key: s, Symbol: symbols.First()))
                                     .ToImmutableDictionary(z => z.Key, z => z.Symbol);

                var config = new AssemblyProviderConfiguration(context, environment, isAot, cache);

                (
                    var internalAssemblyRequests,
                    var internalReflectionRequests,
                    var internalReflectionSources,
                    var internalServiceDescriptorRequests,
                    var internalServiceDescriptorSources
                ) = config.FromAssemblyAttributes(
                    ref assemblySymbols,
                    reflectionRequests,
                    serviceDescriptorRequests,
                    diagnostics,
                    context.CancellationToken
                );

                assemblyRequests = assemblyRequests.AddRange(internalAssemblyRequests);
                reflectionRequests = reflectionRequests.AddRange(internalReflectionRequests);
                serviceDescriptorRequests = serviceDescriptorRequests.AddRange(internalServiceDescriptorRequests);

                var assemblySources = AssemblyCollection.ResolveSources(
                    config,
                    request.compilation,
                    diagnostics,
                    assemblyRequests,
                    assemblySymbols
                );
                var reflectionSources = ReflectionCollection.ResolveSources(
                    config,
                    request.compilation,
                    diagnostics,
                    reflectionRequests,
                    request.compilation.Assembly,
                    context.CancellationToken
                );
                var serviceDescriptorSources = ServiceDescriptorCollection.ResolveSources(
                    config,
                    request.compilation,
                    diagnostics,
                    serviceDescriptorRequests,
                    request.compilation.Assembly,
                    context.CancellationToken
                );

                reflectionSources = reflectionSources.AddRange(internalReflectionSources);
                serviceDescriptorSources = serviceDescriptorSources.AddRange(internalServiceDescriptorSources);

                privateAssemblies.UnionWith(joinAssemblies(assemblySymbols, assemblySources));
                privateAssemblies.UnionWith(joinAssemblies(assemblySymbols, reflectionSources));
                privateAssemblies.UnionWith(joinAssemblies(assemblySymbols, serviceDescriptorSources));

                var cu = CompilationUnit()
                   .WithUsings(
                        List(
                            [
                                UsingDirective(ParseName("System")),
                                UsingDirective(ParseName("System.Collections.Generic")),
                                UsingDirective(ParseName("System.Reflection")),
                                UsingDirective(ParseName("Microsoft.Extensions.DependencyInjection")),
                                UsingDirective(ParseName("Specular")),
                                UsingDirective(ParseName("Specular.Abstractions")),
                            ]
                        )
                    );

                var assemblyProvider = AssemblyProviderBuilder.GetAssemblyProvider(
                    assemblySources,
                    reflectionSources,
                    serviceDescriptorSources,
                    privateAssemblies,
                    config,
                    out var cacheHash
                );
                cu = cu
                    .AddSharedTrivia()
                    .AddAttributeLists(attributes)
                    .AddAttributeLists(
                         AttributeList(
                                 SingletonSeparatedList(
                                     Attribute(
                                         ParseName("Specular.Abstractions.SpecularHashAttribute"),
                                         AttributeArgumentList(
                                             SeparatedList(
                                                 [
                                                     AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(cacheHash))),
                                                 ]
                                             )
                                         )
                                     )
                                 )
                             )
                            .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword)))
                     );

                // Only emit the provider implementation (and the attribute that points at it)
                // when this assembly is configured to do so. The scan-metadata attributes above
                // are always emitted so that downstream assemblies can reuse the resolved data.
                if (emitProvider)
                {
                    if (privateAssemblies.Count != 0 && !config.IsAot) cu = cu.AddUsings(UsingDirective(ParseName("System.Runtime.Loader")));
                    cu = cu.AddMembers(assemblyProvider);
                }

                if (generateScanReport)
                {
                    TryAddScanReport(context, diagnostics, assemblySources, reflectionSources, serviceDescriptorSources);
                }

                foreach (var diagnostic in diagnostics)
                {
                    context.ReportDiagnostic(diagnostic);
                }

                context.AddSource(
                    "SpecularProvider.g.cs",
                    new CollectionReturnLayout().Visit(cu.NormalizeWhitespace()).GetText(Encoding.UTF8)
                );

                cache.Save(request.options.GlobalOptions);

                return;

                static IEnumerable<IAssemblySymbol> joinAssemblies(IEnumerable<KeyValuePair<string, IAssemblySymbol>> assemblies, IEnumerable<ResolvedSourceLocation> sources) =>
                    sources.SelectMany(z => z.PrivateAssemblies).Join(assemblies, z => z, z => z.Key, (_, a) => a.Value);
            }
        );
    }
    private sealed class TypeInferenceRewriter : CSharpSyntaxRewriter
    {
        private int indentLevel = 1;

        //        public override SyntaxNode? VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
        //        {
        //            try
        //            {
        //                indentLevel++;
        //
        //                return base.VisitImplicitObjectCreationExpression(((node.ReplaceNode(node.ArgumentList, Indent(node.ArgumentList)))));
        //            }
        //            finally
        //            {
        //                indentLevel--;
        //            }
        //        }

        public override SyntaxNode? VisitExpressionElement(ExpressionElementSyntax node)
        {
            try
            {
                indentLevel++;

                return base.VisitExpressionElement(Indent(node));
            }
            finally
            {
                indentLevel--;
            }
        }

        public override SyntaxNode? VisitCollectionExpression(CollectionExpressionSyntax node)
        {
            try
            {
                indentLevel++;

                return base.VisitCollectionExpression(Outdent(Indent(node)));
            }
            finally
            {
                indentLevel--;
            }
        }

        private T Indent<T>(T node) where T : SyntaxNode
            => node.WithLeadingTrivia(TriviaList([.. node.GetLeadingTrivia(), LineFeed, .. Enumerable.Range(0, indentLevel).SelectMany(_ => Enumerable.Range(0, 4).Select(z => Space))]));
        private T Outdent<T>(T node) where T : SyntaxNode
            => node.WithTrailingTrivia(TriviaList([.. node.GetTrailingTrivia(), LineFeed, .. Enumerable.Range(0, indentLevel - 1).SelectMany(_ => Enumerable.Range(0, 4).Select(z => Space))]));
    }

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "A source generator must never crash the build; unexpected exceptions are surfaced as diagnostics.")]
    private static void TryAddScanReport(
        SourceProductionContext context,
        HashSet<Diagnostic> diagnostics,
        ImmutableList<ResolvedSourceLocation> assemblySources,
        ImmutableList<ResolvedSourceLocation> reflectionSources,
        ImmutableList<ResolvedSourceLocation> serviceDescriptorSources
    )
    {
        try
        {
            var scanReport = ScanReport.ScanReportBuilder.GetScanReport(assemblySources, reflectionSources, serviceDescriptorSources);
            var cu = CompilationUnit()
                    .WithUsings(
                         List(
                             [
                                 UsingDirective(ParseName("System")),
                                 UsingDirective(ParseName("System.Collections.Generic")),
                                 UsingDirective(ParseName("System.Reflection")),
                                 UsingDirective(ParseName("Microsoft.Extensions.DependencyInjection")),
                                 UsingDirective(ParseName("Specular")),
                                 UsingDirective(ParseName("Specular.Abstractions")),
                                 UsingDirective(ParseName("Specular.Diagnostics")),
                             ]
                         )
                     )
                    .AddMembers(scanReport)
                    .NormalizeWhitespace();

            TypeInferenceRewriter rewriter = new();

            var rewritten = rewriter.Visit(cu.SyntaxTree.GetRoot());


            context.AddSource("SpecularScanReport.g.cs", rewritten.GetText(Encoding.UTF8));
        }
        catch (Exception e)
        {
            diagnostics.Add(
                Diagnostic.Create(
                    Diagnostics.UnhandledException,
                    null,
                    e.Message,
                    e.StackTrace?.Replace("\r", "").Replace("\n", ""),
                    e.GetType().Name,
                    e.ToString()
                )
            );
        }
    }
}
