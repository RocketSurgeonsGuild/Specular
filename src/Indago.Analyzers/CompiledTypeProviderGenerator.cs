using System.Collections.Immutable;
using System.Text;
using System.Text.Json;
using Indago.Analyzers.Configuration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Indago.Analyzers;

public static class Constants
{
    public const string IndagoProviderCacheFileName = "IndagoProvider.ctpjson";
}

/// <summary>
///     Source generate used for scanning assemblies for registrations
/// </summary>
[Generator]
public class IndagoProviderGenerator : IIncrementalGenerator
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
                                   .AdditionalTextsProvider.Where(z => Path.GetFileName(z.Path).Equals(Constants.IndagoProviderCacheFileName, StringComparison.OrdinalIgnoreCase))
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
                // Whether this assembly should emit its own IIndagoProvider implementation.
                // Libraries that are only meant to be scanned can opt out by setting
                // <IndagoEmitProvider>false</IndagoEmitProvider>; the consuming application
                // then emits the provider. Defaults to true to preserve existing behaviour.
                var emitProvider = !request.options.GlobalOptions.TryGetValue("build_property.IndagoEmitProvider", out var emitProviderValue)
                 || !bool.TryParse(emitProviderValue, out var parsedEmitProvider)
                 || parsedEmitProvider;
                var privateAssemblies = new HashSet<IAssemblySymbol>(SymbolEqualityComparer.Default);
                var diagnostics = new HashSet<Diagnostic>();
                var assemblyRequests = AssemblyCollection.GetAssemblyItems(request.compilation, diagnostics, request.assemblies, context.CancellationToken);
                var reflectionRequests = ReflectionCollection.GetReflectionItems(
                    request.compilation,
                    diagnostics,
                    request.reflection,
                    context.CancellationToken
                );
                var serviceDescriptorRequests = ServiceDescriptorCollection.GetServiceDescriptorItems(
                    request.compilation,
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
                                UsingDirective(ParseName("Indago")),
                                UsingDirective(ParseName("Indago.Abstractions")),
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
                if (privateAssemblies.Any() && !config.IsAot) cu = cu.AddUsings(UsingDirective(ParseName("System.Runtime.Loader")));

                cu = cu.AddSharedTrivia().AddAttributeLists(attributes);

                // Only emit the provider implementation (and the attribute that points at it)
                // when this assembly is configured to do so. The scan-metadata attributes above
                // are always emitted so that downstream assemblies can reuse the resolved data.
                if (emitProvider)
                {
                    if (privateAssemblies.Any()) cu = cu.AddUsings(UsingDirective(ParseName("System.Runtime.Loader")));

                    cu = cu
                        .AddAttributeLists(
                             AttributeList(
                                     SingletonSeparatedList(
                                         Attribute(
                                             ParseName("Indago.Abstractions.IndagoProviderAttribute"),
                                             AttributeArgumentList(
                                                 SeparatedList(
                                                     [
                                                         AttributeArgument(TypeOfExpression(ParseName(assemblyProvider.Identifier.Text))),
                                                         AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(cacheHash))),
                                                     ]
                                                 )
                                             )
                                         )
                                     )
                                 )
                                .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword)))
                         )
                        .AddMembers(assemblyProvider);
                }

                foreach (var diagnostic in diagnostics)
                {
                    context.ReportDiagnostic(diagnostic);
                }

                context.AddSource(
                    Path.ChangeExtension(Constants.IndagoProviderCacheFileName, ".g.cs"),
                    cu.NormalizeWhitespace().SyntaxTree.GetRoot().GetText(Encoding.UTF8)
                );

                if (GetCacheDirectory(request.options) is not { } cacheDirectory) return;

#pragma warning disable RS1035

                var fileInfo = new FileInfo(Path.Combine(cacheDirectory, Constants.IndagoProviderCacheFileName));
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
}
