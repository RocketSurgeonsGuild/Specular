using System.Collections.Immutable;
using System.Text;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Specular.Analyzers.Configuration;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Specular.Analyzers;

public static class Constants
{
    public const string SpecularProviderCacheFileName = "SpecularProvider.ctpjson";
}

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
        var generatedJsonProvider = context
                                   .AdditionalTextsProvider.Where(z => Path.GetFileName(z.Path).Equals(Constants.SpecularProviderCacheFileName, StringComparison.OrdinalIgnoreCase))
                                   .Select(
                                        (text, _) =>
                                        {
                                            var source = text.GetText(_)?.ToString();
                                            return source is not { Length: > 100 }
                                                ? new(
                                                    ImmutableDictionary<string, CompiledAssemblyProviderData>.Empty,
                                                    ImmutableHashSet<string>.Empty,
                                                    ImmutableDictionary<string, GeneratedLocationAssemblyResolvedSourceCollection>.Empty
                                                )
                                                : JsonSerializer.Deserialize(
                                                    source,
                                                    JsonSourceGenerationContext.Default.GeneratedAssemblyProviderData
                                                );
                                        }
                                    )
                                   .Collect()
                                   .Select(
                                        (z, _) => z.SingleOrDefault()
                                         ?? new(
                                                ImmutableDictionary<string, CompiledAssemblyProviderData>.Empty,
                                                ImmutableHashSet<string>.Empty,
                                                ImmutableDictionary<string, GeneratedLocationAssemblyResolvedSourceCollection>.Empty
                                            )
                                    );
        context.RegisterImplementationSourceOutput(
            context
               .CompilationProvider
               .Combine(context.AnalyzerConfigOptionsProvider)
               .Combine(collectionProvider)
               .Combine(generatedJsonProvider)
               .Select(
                    (tuple, _) => (
                        compilation: tuple.Left.Left.Left,
                        options: tuple.Left.Left.Right,
                        tuple.Left.Right.assemblies,
                        tuple.Left.Right.reflection,
                        tuple.Left.Right.serviceDescriptors,
                        additionalFiles: tuple.Right
                    )
                ),
            static (context, request) =>
            {
                HashSet<string> excludedAssemblies = request.options.GlobalOptions.TryGetValue("build_property.ExcludeAssemblyFromCTP", out var assemblies)
                    ? [.. assemblies.Split([';', ','], StringSplitOptions.RemoveEmptyEntries)]
                    : [];
                // Whether this assembly should emit its own ISpecularProvider implementation.
                // Libraries that are only meant to be scanned can opt out by setting
                // <SpecularEmitProvider>false</SpecularEmitProvider>; the consuming application
                // then emits the provider. Defaults to true to preserve existing behaviour.
                var emitProvider = !request.options.GlobalOptions.TryGetValue("build_property.SpecularEmitProvider", out var emitProviderValue)
                 || !bool.TryParse(emitProviderValue, out var parsedEmitProvider)
                 || parsedEmitProvider;
                // Emits SpecularScanReport.g.cs (discovered scanner expressions, assemblies, and types, plus a
                // Mermaid rendering) by default; opt out with <SpecularGenerateDiagnostics>false</SpecularGenerateDiagnostics>.
                var generateScanReport =
                    !request.options.GlobalOptions.TryGetValue("build_property.SpecularGenerateDiagnostics", out var generateScanReportValue)
                 || !bool.TryParse(generateScanReportValue, out var parsedGenerateScanReport)
                 || parsedGenerateScanReport;
                var privateAssemblies = new HashSet<IAssemblySymbol>(SymbolEqualityComparer.Default);
                var diagnostics = new HashSet<Diagnostic>();

                // The generated SpecularProvider type does not exist while this generator runs, so consumer scan
                // calls written against `SpecularProvider.Instance` (or a local typed from it) fail to bind, which
                // would silently drop every scan expression. Build a semantic-only compilation that includes a
                // minimal SpecularProvider shell purely so those selector lambdas bind and can be parsed. This
                // augmented compilation is used ONLY for interpreting selector expressions - never for enumerating
                // the types being scanned - so the shell can never appear in scan results.
                var semanticCompilation = CreateSemanticCompilation(request.compilation);

                var assemblyRequests = AssemblyCollection.GetAssemblyItems(semanticCompilation, diagnostics, request.assemblies, context.CancellationToken);
                var reflectionRequests = ReflectionCollection.GetReflectionItems(
                    semanticCompilation,
                    diagnostics,
                    request.reflection,
                    context.CancellationToken
                );
                var serviceDescriptorRequests = ServiceDescriptorCollection.GetServiceDescriptorItems(
                    semanticCompilation,
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
                                     .Select(
                                          symbol =>
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

                var resultingData = new ResultingAssemblyProviderData();

                var config = new AssemblyProviderConfiguration(
                    context,
                    request.compilation,
                    request.options,
                    request.additionalFiles,
                    resultingData
                );

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
                    diagnostics
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
                    request.compilation.Assembly
                );
                var serviceDescriptorSources = ServiceDescriptorCollection.ResolveSources(
                    config,
                    request.compilation,
                    diagnostics,
                    serviceDescriptorRequests,
                    request.compilation.Assembly
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
                cu = cu.AddSharedTrivia()
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
                    TryAddScanReport(context, diagnostics, assemblySources, reflectionSources, serviceDescriptorSources, request.compilation, assemblySymbols);
                }

                foreach (var diagnostic in diagnostics)
                {
                    context.ReportDiagnostic(diagnostic);
                }

                context.AddSource(
                    Path.ChangeExtension(Constants.SpecularProviderCacheFileName, ".g.cs"),
                    cu.NormalizeWhitespace().SyntaxTree.GetRoot().GetText(Encoding.UTF8)
                );

                if (GetCacheDirectory(request.options) is not { } cacheDirectory) return;

#pragma warning disable RS1035

                var fileInfo = new FileInfo(Path.Combine(cacheDirectory, Constants.SpecularProviderCacheFileName));
                var writer = fileInfo.Open(fileInfo.Exists ? FileMode.Truncate : FileMode.CreateNew);
                try
                {
                    using var streamWriter = new StreamWriter(writer);
                    var generatedData = resultingData.ToGeneratedAssemblyProviderData();
                    streamWriter.Write(JsonSerializer.Serialize(generatedData, JsonSourceGenerationContext.Default.GeneratedAssemblyProviderData));
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                finally
                {
                    writer.Dispose();
                }

#pragma warning restore RS1035

                return;

                static IEnumerable<IAssemblySymbol> joinAssemblies(IEnumerable<KeyValuePair<string, IAssemblySymbol>> assemblies, IEnumerable<ResolvedSourceLocation> sources) => sources.SelectMany(z => z.PrivateAssemblies).Join(assemblies, z => z, z => z.Key, (_, a) => a.Value);
            }
        );
    }
#pragma warning disable RS1035
    private static string? GetCacheDirectory(AnalyzerConfigOptionsProvider options)
    {
        var directory = options.GlobalOptions.TryGetValue("build_property.IntermediateOutputPath", out var intermediateOutputPath)
            ? intermediateOutputPath
            : null;
        if (directory is null) return null;

        if (!Path.IsPathRooted(directory) && options.GlobalOptions.TryGetValue("build_property.ProjectDir", out var projectDirectory)) directory = Path.Combine(projectDirectory, directory);

        var cacheDirectory = Path.Combine(directory, "ctp");
        if (!Directory.Exists(cacheDirectory)) _ = Directory.CreateDirectory(cacheDirectory);

        return cacheDirectory;
    }
#pragma warning restore RS1035

    // Produces a compilation augmented with a minimal SpecularProvider shell so that consumer scan calls written
    // against the generated `SpecularProvider.Instance` bind during generation. Used only for parsing selector
    // expressions - never for enumerating the types being scanned.
    private static Compilation CreateSemanticCompilation(Compilation compilation)
    {
        // If the consuming compilation already declares an SpecularProvider, do not inject the shell.
        if (compilation.GetTypeByMetadataName("SpecularProvider") is { }) return compilation;

        const string shellSource =
            "internal sealed class SpecularProvider\n"
          + "{\n"
          + "    public static global::Specular.ISpecularProvider Instance { get; }\n"
          + "}\n";

        var parseOptions = compilation.SyntaxTrees.Select(z => z.Options).FirstOrDefault() as CSharpParseOptions;
        var shellTree = CSharpSyntaxTree.ParseText(shellSource, parseOptions);
        return compilation.AddSyntaxTrees(shellTree);
    }

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "A source generator must never crash the build; unexpected exceptions are surfaced as diagnostics.")]
    private static void TryAddScanReport(
        SourceProductionContext context,
        HashSet<Diagnostic> diagnostics,
        ImmutableList<ResolvedSourceLocation> assemblySources,
        ImmutableList<ResolvedSourceLocation> reflectionSources,
        ImmutableList<ResolvedSourceLocation> serviceDescriptorSources,
        Compilation compilation,
        ImmutableDictionary<string, IAssemblySymbol> assemblySymbols
    )
    {
        try
        {
            var scanReport = ScanReport.ScanReportBuilder.GetScanReport(assemblySources, reflectionSources, serviceDescriptorSources, compilation, assemblySymbols);
            context.AddSource("SpecularScanReport.g.cs", scanReport);
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
