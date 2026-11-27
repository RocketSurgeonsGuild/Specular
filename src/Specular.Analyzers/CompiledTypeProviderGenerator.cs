using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Specular.Analyzers.AssemblyProviders;
using Specular.Analyzers.Configuration;
using Specular.Scanning;

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

        // Specular calls Specular.Scanning directly rather than through context.Scan: its requests come from syntax across call sites
        // and from referenced assemblies' [AssemblyMetadata], both only known with the compilation in hand; and its own cache entries
        // (the requests above and each call site's result) must go into the same file as the scan results, which only one ScanCache per
        // run can write.
        //
        // The work is split so an edit that leaves the scan results as they were stops there: analyzing (binding the call sites and
        // resolving them) runs on every compilation, but the model it produces holds only text and compares by value, so writing the
        // provider and the scan report, the expensive half, reruns only when the results change. Writing still needs symbols for the
        // private assemblies it names, all of them references, so it takes a compilation that only counts as new when its references do.
        var analysis = context
                      .CompilationProvider
                      .Combine(context.AnalyzerConfigOptionsProvider)
                      .Combine(collectionProvider)
                      .Combine(cacheFileProvider)
                      .Select(
                           static (tuple, cancellationToken) => Analyze(
                               tuple.Left.Left.Left,
                               tuple.Left.Left.Right,
                               tuple.Left.Right.assemblies,
                               tuple.Left.Right.reflection,
                               tuple.Left.Right.serviceDescriptors,
                               tuple.Right,
                               cancellationToken
                           )
                       );

        context.RegisterImplementationSourceOutput(
            analysis,
            static (context, result) =>
            {
                foreach (var diagnostic in result.Diagnostics)
                {
                    context.ReportDiagnostic(diagnostic);
                }

                result.Cache.Save(result.GlobalOptions);
            }
        );

        context.RegisterImplementationSourceOutput(
            analysis
               .Select(static (result, _) => result.Model)
               .WithTrackingName(ModelTrackingName)
               .Combine(context.CompilationProvider.WithComparer(SameReferencesComparer.Instance)),
            static (context, input) => Render(context, input.Left, input.Right)
        );
    }

    /// <summary>The tracking name of the step whose value decides whether the provider and the scan report are written again.</summary>
    internal const string ModelTrackingName = "SpecularModel";

    private static AnalysisResult Analyze(
        Compilation compilation,
        AnalyzerConfigOptionsProvider options,
        ImmutableArray<(InvocationExpressionSyntax method, ExpressionSyntax selector)> assemblies,
        ImmutableArray<(InvocationExpressionSyntax method, ExpressionSyntax selector)> reflection,
        ImmutableArray<(InvocationExpressionSyntax method, ExpressionSyntax selector)> serviceDescriptors,
        ScanCacheFileData cacheFile,
        CancellationToken cancellationToken
    )
    {
        HashSet<string> excludedAssemblies = options.GlobalOptions.TryGetValue("build_property.ExcludeAssemblyFromCTP", out var excluded)
            ? [.. excluded.Split([';', ','], StringSplitOptions.RemoveEmptyEntries)]
            : [];

        var emitProvider = GetMsBuildBoolean(options.GlobalOptions, "SpecularEmitProvider", true);
        var generateScanReport = GetMsBuildBoolean(options.GlobalOptions, "SpecularGenerateDiagnostics", true);
        var diagnostics = new HashSet<Diagnostic>();

        var environment = new ScanEnvironment(compilation);
        var isAot = StatementGeneration.IsAotCompilation(options);
        var cache = new SpecularCache(compilation, cacheFile, isAot);

        var assemblyRequests = AssemblyCollection.GetAssemblyItems(environment, diagnostics, assemblies, cancellationToken);
        var reflectionRequests = ReflectionCollection.GetReflectionItems(environment, diagnostics, reflection, cancellationToken);
        var serviceDescriptorRequests = ServiceDescriptorCollection.GetServiceDescriptorItems(environment, diagnostics, serviceDescriptors, cancellationToken);
        var attributes = AssemblyProviderConfiguration.ToAssemblyAttributes(diagnostics, assemblyRequests, reflectionRequests, serviceDescriptorRequests).ToArray();

        var assemblySymbols = GetAssemblySymbols(compilation, excludedAssemblies);
        var config = new AssemblyProviderConfiguration(diagnostics, environment, isAot, cache);

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
            cancellationToken
        );

        assemblyRequests = assemblyRequests.AddRange(internalAssemblyRequests);
        reflectionRequests = reflectionRequests.AddRange(internalReflectionRequests);
        serviceDescriptorRequests = serviceDescriptorRequests.AddRange(internalServiceDescriptorRequests);

        var assemblySources = AssemblyCollection.ResolveSources(config, compilation, diagnostics, assemblyRequests, assemblySymbols);
        var reflectionSources = ReflectionCollection.ResolveSources(config, compilation, diagnostics, reflectionRequests, compilation.Assembly, cancellationToken)
           .AddRange(internalReflectionSources);
        var serviceDescriptorSources = ServiceDescriptorCollection
                                      .ResolveSources(config, compilation, diagnostics, serviceDescriptorRequests, compilation.Assembly, cancellationToken)
                                      .AddRange(internalServiceDescriptorSources);

        var privateAssemblies = new SortedSet<string>(StringComparer.Ordinal);
        foreach (var source in assemblySources.Concat(reflectionSources).Concat(serviceDescriptorSources))
        {
            foreach (var name in source.PrivateAssemblies)
            {
                if (assemblySymbols.ContainsKey(name)) _ = privateAssemblies.Add(name);
            }
        }

        var model = new GenerationModel(
            new([.. assemblySources]),
            new([.. reflectionSources]),
            new([.. serviceDescriptorSources]),
            new([.. privateAssemblies]),
            new([.. attributes]),
            isAot,
            emitProvider,
            generateScanReport
        );
        return new(model, [.. diagnostics], cache, options.GlobalOptions);
    }

    private static void Render(SourceProductionContext context, GenerationModel model, Compilation compilation)
    {
        var assemblySymbols = GetAssemblySymbols(compilation, []);
        var privateAssemblies = new HashSet<IAssemblySymbol>(SymbolEqualityComparer.Default);
        foreach (var name in model.PrivateAssemblies)
        {
            if (assemblySymbols.TryGetValue(name, out var symbol)) _ = privateAssemblies.Add(symbol);
        }

        var assemblySources = model.AssemblySources.AsImmutableArray().ToImmutableList();
        var reflectionSources = model.ReflectionSources.AsImmutableArray().ToImmutableList();
        var serviceDescriptorSources = model.ServiceDescriptorSources.AsImmutableArray().ToImmutableList();

        var assemblyProvider = AssemblyProviderBuilder.GetAssemblyProvider(
            assemblySources,
            reflectionSources,
            serviceDescriptorSources,
            privateAssemblies,
            compilation,
            model.IsAot,
            out var cacheHash
        );

        var text = new StringBuilder(assemblyProvider.Length + 4096).Append(Helpers.FileHeader);
        foreach (var @namespace in ProviderUsings)
        {
            _ = text.Append("using ").Append(@namespace).Append(';').Append(CodeText.SourceEndOfLine);
        }

        // Only emit the provider implementation (and the attribute that points at it)
        // when this assembly is configured to do so. The scan-metadata attributes
        // are always emitted so that downstream assemblies can reuse the resolved data.
        if (model.EmitProvider && privateAssemblies.Count != 0 && !model.IsAot)
        {
            _ = text.Append("using System.Runtime.Loader;").Append(CodeText.SourceEndOfLine);
        }

        _ = text.Append(CodeText.SourceEndOfLine);
        foreach (var (key, value) in model.AssemblyAttributes)
        {
            _ = text.Append(Helpers.AddAssemblyAttribute(key, value)).Append(CodeText.SourceEndOfLine);
        }

        _ = text
           .Append("[assembly: Specular.Abstractions.SpecularHashAttribute(")
           .Append(CodeText.Literal(cacheHash))
           .Append(")]")
           .Append(CodeText.SourceEndOfLine);
        if (model.EmitProvider) _ = text.Append(assemblyProvider);

        if (model.GenerateScanReport)
        {
            TryAddScanReport(context, assemblySources, reflectionSources, serviceDescriptorSources);
        }

        context.AddSource("SpecularProvider.g.cs", SourceText.From(text.Append(Helpers.FileFooter).ToString(), Encoding.UTF8));
    }

    private static readonly string[] ProviderUsings =
    [
        "System",
        "System.Collections.Generic",
        "System.Reflection",
        "Microsoft.Extensions.DependencyInjection",
        "Specular",
        "Specular.Abstractions",
    ];

    private static bool GetMsBuildBoolean(AnalyzerConfigOptions options, string propertyName, bool @default) =>
        options.TryGetValue($"build_property.{propertyName}", out var value) && bool.TryParse(value, out var parsedValue) ? parsedValue : @default;

    private static ImmutableDictionary<string, IAssemblySymbol> GetAssemblySymbols(Compilation compilation, HashSet<string> excludedAssemblies) =>
        compilation
           .References
           .Select(compilation.GetAssemblyOrModuleSymbol)
           .Concat([compilation.Assembly])
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

    /// <summary>What a run found, as text only, compared by value: the same results mean the same generated code.</summary>
    private sealed record GenerationModel(
        EquatableArray<ResolvedSourceLocation> AssemblySources,
        EquatableArray<ResolvedSourceLocation> ReflectionSources,
        EquatableArray<ResolvedSourceLocation> ServiceDescriptorSources,
        EquatableArray<string> PrivateAssemblies,
        EquatableArray<(string Key, string Value)> AssemblyAttributes,
        bool IsAot,
        bool EmitProvider,
        bool GenerateScanReport
    );

    // Never equal to the previous run's, so its diagnostics are reported and its cache saved each time, as they were before the split.
    private sealed class AnalysisResult(GenerationModel model, ImmutableArray<Diagnostic> diagnostics, SpecularCache cache, AnalyzerConfigOptions globalOptions)
    {
        public GenerationModel Model { get; } = model;
        public ImmutableArray<Diagnostic> Diagnostics { get; } = diagnostics;
        public SpecularCache Cache { get; } = cache;
        public AnalyzerConfigOptions GlobalOptions { get; } = globalOptions;
    }

    /// <summary>
    ///     Two compilations with the same references, assembly name and options: everything writing the provider reads from symbols (the
    ///     private assemblies it loads, the public types that stand for them under Native AOT, which types they can resolve) is the same.
    /// </summary>
    private sealed class SameReferencesComparer : IEqualityComparer<Compilation>
    {
        public static SameReferencesComparer Instance { get; } = new();

        public bool Equals(Compilation? x, Compilation? y) =>
            ReferenceEquals(x, y)
         || ( x is { }
             && y is { }
             && x.AssemblyName == y.AssemblyName
             && x.Options.Equals(y.Options)
             && x.References.SequenceEqual(y.References) );

        public int GetHashCode(Compilation obj) => obj.AssemblyName?.GetHashCode(StringComparison.Ordinal) ?? 0;
    }

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "A source generator must never crash the build; unexpected exceptions are surfaced as diagnostics.")]
    private static void TryAddScanReport(
        SourceProductionContext context,
        ImmutableList<ResolvedSourceLocation> assemblySources,
        ImmutableList<ResolvedSourceLocation> reflectionSources,
        ImmutableList<ResolvedSourceLocation> serviceDescriptorSources
    )
    {
        try
        {
            var scanReport = ScanReport.ScanReportBuilder.GetScanReport(assemblySources, reflectionSources, serviceDescriptorSources);
            var text = new StringBuilder(scanReport.Length + 512);
            foreach (var @namespace in ProviderUsings)
            {
                _ = text.Append("using ").Append(@namespace).Append(';').Append(CodeText.SourceEndOfLine);
            }

            _ = text
               .Append("using Specular.Diagnostics;")
               .Append(CodeText.SourceEndOfLine)
               .Append(CodeText.SourceEndOfLine)
               .Append(scanReport);
            context.AddSource("SpecularScanReport.g.cs", SourceText.From(text.ToString(), Encoding.UTF8));
        }

        catch (Exception e)
        {
            context.ReportDiagnostic(
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
