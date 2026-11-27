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
        //
        // Resolving the call sites against referenced assemblies is its own step: it depends only on the references and the requests,
        // so it takes the requests in their serialized form (the one referenced assemblies carry in [AssemblyMetadata]) and a
        // compilation that only counts as new when its references do. An edit that leaves both alone reuses its results, and the build
        // cache it owns is saved only when it runs.
        var requests = context
                      .CompilationProvider
                      .Combine(context.AnalyzerConfigOptionsProvider)
                      .Combine(collectionProvider)
                      .Select(
                           static (tuple, cancellationToken) => BindRequests(
                               tuple.Left.Left,
                               tuple.Left.Right,
                               tuple.Right.assemblies,
                               tuple.Right.reflection,
                               tuple.Right.serviceDescriptors,
                               cancellationToken
                           )
                       );

        var referenced = requests
                        .WithComparer(SameReferencedInputComparer.Instance)
                        .Combine(cacheFileProvider)
                        .Select(static (tuple, cancellationToken) => ResolveReferenced(tuple.Left, tuple.Right, cancellationToken))
                        .WithTrackingName(ReferencedTrackingName);

        context.RegisterImplementationSourceOutput(referenced, static (_, result) => result.Cache.Save(result.GlobalOptions));

        var analysis = requests.Combine(referenced).Select(static (tuple, cancellationToken) => Analyze(tuple.Left, tuple.Right, cancellationToken));

        context.RegisterImplementationSourceOutput(
            analysis,
            static (context, result) =>
            {
                foreach (var diagnostic in result.Diagnostics)
                {
                    context.ReportDiagnostic(diagnostic);
                }
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

    /// <summary>The tracking name of the step that resolves the call sites against referenced assemblies.</summary>
    internal const string ReferencedTrackingName = "SpecularReferenced";

    private static RequestsResult BindRequests(
        Compilation compilation,
        AnalyzerConfigOptionsProvider options,
        ImmutableArray<(InvocationExpressionSyntax method, ExpressionSyntax selector)> assemblies,
        ImmutableArray<(InvocationExpressionSyntax method, ExpressionSyntax selector)> reflection,
        ImmutableArray<(InvocationExpressionSyntax method, ExpressionSyntax selector)> serviceDescriptors,
        CancellationToken cancellationToken
    )
    {
        var excluded = options.GlobalOptions.TryGetValue("build_property.ExcludeAssemblyFromCTP", out var value) ? value : "";
        var diagnostics = new HashSet<Diagnostic>();
        var environment = new ScanEnvironment(compilation);
        var assemblyRequests = AssemblyCollection.GetAssemblyItems(environment, diagnostics, assemblies, cancellationToken);
        var reflectionRequests = ReflectionCollection.GetReflectionItems(environment, diagnostics, reflection, cancellationToken);
        var serviceDescriptorRequests = ServiceDescriptorCollection.GetServiceDescriptorItems(environment, diagnostics, serviceDescriptors, cancellationToken);
        var attributes = AssemblyProviderConfiguration.ToAssemblyAttributes(diagnostics, assemblyRequests, reflectionRequests, serviceDescriptorRequests).ToArray();
        // Unsorted, in binding order: the referenced step resolves own requests in the order the provider has always used.
        var serialized = AssemblyProviderConfiguration.SerializeRequests(diagnostics, [], reflectionRequests, serviceDescriptorRequests).ToArray();

        return new(
            compilation,
            environment,
            options.GlobalOptions,
            assemblyRequests,
            reflectionRequests,
            serviceDescriptorRequests,
            attributes,
            diagnostics,
            new(new([.. serialized]), StatementGeneration.IsAotCompilation(options), excluded)
        );
    }

    private static ReferencedResult ResolveReferenced(RequestsResult requests, ScanCacheFileData cacheFile, CancellationToken cancellationToken)
    {
        var compilation = requests.Compilation;
        var diagnostics = new HashSet<Diagnostic>();
        var cache = new SpecularCache(compilation, cacheFile, requests.Key.IsAot);
        var config = new AssemblyProviderConfiguration(diagnostics, requests.Environment, requests.Key.IsAot, cache);
        var assemblySymbols = GetAssemblySymbols(compilation, ParseExcluded(requests.Key.ExcludedAssemblies));
        var all = assemblySymbols;

        var (internalAssemblyRequests, internalReflectionRequests, reflectionSources, internalServiceDescriptorRequests, serviceDescriptorSources) =
            config.FromAssemblyAttributes(ref assemblySymbols, requests.Reflection, requests.ServiceDescriptors, diagnostics, cancellationToken);

        var internalRequests = AssemblyProviderConfiguration
                              .SerializeRequests(diagnostics, internalAssemblyRequests, internalReflectionRequests, internalServiceDescriptorRequests)
                              .ToArray();
        var excludedFromResolution = all.Keys.Where(name => !assemblySymbols.ContainsKey(name)).ToImmutableArray();
        return new(internalRequests, reflectionSources, serviceDescriptorSources, excludedFromResolution, [.. diagnostics], cache, requests.GlobalOptions);
    }

    private static AnalysisResult Analyze(RequestsResult requests, ReferencedResult referenced, CancellationToken cancellationToken)
    {
        var compilation = requests.Compilation;
        var emitProvider = GetMsBuildBoolean(requests.GlobalOptions, "SpecularEmitProvider", true);
        var generateScanReport = GetMsBuildBoolean(requests.GlobalOptions, "SpecularGenerateDiagnostics", true);
        var isAot = requests.Key.IsAot;
        var diagnostics = new HashSet<Diagnostic>(requests.Diagnostics);
        diagnostics.UnionWith(referenced.Diagnostics);

        // Scans of the compilation's own assembly are never cached, so this cache is only a place for them to pass through.
        var cache = new SpecularCache(compilation, SpecularCache.Parse(null), isAot);
        var config = new AssemblyProviderConfiguration(diagnostics, requests.Environment, isAot, cache);
        var assemblySymbols = GetAssemblySymbols(compilation, ParseExcluded(requests.Key.ExcludedAssemblies)).RemoveRange(referenced.ExcludedFromResolution);
        var (internalAssemblyRequests, internalReflectionRequests, internalServiceDescriptorRequests) = config.LoadRequests(referenced.InternalRequests, assemblySymbols);

        var assemblyRequests = requests.Assemblies.AddRange(internalAssemblyRequests);
        var reflectionRequests = requests.Reflection.AddRange(internalReflectionRequests);
        var serviceDescriptorRequests = requests.ServiceDescriptors.AddRange(internalServiceDescriptorRequests);

        var assemblySources = AssemblyCollection.ResolveSources(config, compilation, diagnostics, assemblyRequests, assemblySymbols);
        var reflectionSources = ReflectionCollection.ResolveSources(config, compilation, diagnostics, reflectionRequests, compilation.Assembly, cancellationToken)
           .AddRange(referenced.ReflectionSources);
        var serviceDescriptorSources = ServiceDescriptorCollection
                                      .ResolveSources(config, compilation, diagnostics, serviceDescriptorRequests, compilation.Assembly, cancellationToken)
                                      .AddRange(referenced.ServiceDescriptorSources);

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
            new([.. requests.Attributes]),
            isAot,
            emitProvider,
            generateScanReport
        );
        return new(model, [.. diagnostics]);
    }

    private static HashSet<string> ParseExcluded(string excluded) => [.. excluded.Split([';', ','], StringSplitOptions.RemoveEmptyEntries)];

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

    // Never equal to the previous run's, so its diagnostics are reported each time.
    private sealed class AnalysisResult(GenerationModel model, ImmutableArray<Diagnostic> diagnostics)
    {
        public GenerationModel Model { get; } = model;
        public ImmutableArray<Diagnostic> Diagnostics { get; } = diagnostics;
    }

    /// <summary>What resolving against referenced assemblies depends on besides the references: compared by value.</summary>
    private sealed record ReferencedKey(EquatableArray<(string Key, string Value)> Requests, bool IsAot, string ExcludedAssemblies);

    private sealed class RequestsResult(
        Compilation compilation,
        ScanEnvironment environment,
        AnalyzerConfigOptions globalOptions,
        ImmutableList<AssemblyCollection.Item> assemblies,
        ImmutableList<ReflectionCollection.Item> reflection,
        ImmutableList<ServiceDescriptorCollection.Item> serviceDescriptors,
        (string Key, string Value)[] attributes,
        HashSet<Diagnostic> diagnostics,
        ReferencedKey key
    )
    {
        public Compilation Compilation { get; } = compilation;
        public ScanEnvironment Environment { get; } = environment;
        public AnalyzerConfigOptions GlobalOptions { get; } = globalOptions;
        public ImmutableList<AssemblyCollection.Item> Assemblies { get; } = assemblies;
        public ImmutableList<ReflectionCollection.Item> Reflection { get; } = reflection;
        public ImmutableList<ServiceDescriptorCollection.Item> ServiceDescriptors { get; } = serviceDescriptors;
        public (string Key, string Value)[] Attributes { get; } = attributes;
        public HashSet<Diagnostic> Diagnostics { get; } = diagnostics;
        public ReferencedKey Key { get; } = key;
    }

    // A new instance each time the step runs, so the cache it holds is saved once per run of the step.
    private sealed class ReferencedResult(
        (string Key, string Value)[] internalRequests,
        ImmutableList<ResolvedSourceLocation> reflectionSources,
        ImmutableList<ResolvedSourceLocation> serviceDescriptorSources,
        ImmutableArray<string> excludedFromResolution,
        ImmutableArray<Diagnostic> diagnostics,
        SpecularCache cache,
        AnalyzerConfigOptions globalOptions
    )
    {
        public (string Key, string Value)[] InternalRequests { get; } = internalRequests;
        public ImmutableList<ResolvedSourceLocation> ReflectionSources { get; } = reflectionSources;
        public ImmutableList<ResolvedSourceLocation> ServiceDescriptorSources { get; } = serviceDescriptorSources;
        public ImmutableArray<string> ExcludedFromResolution { get; } = excludedFromResolution;
        public ImmutableArray<Diagnostic> Diagnostics { get; } = diagnostics;
        public SpecularCache Cache { get; } = cache;
        public AnalyzerConfigOptions GlobalOptions { get; } = globalOptions;
    }

    /// <summary>
    ///     Two compilations with the same references, assembly name and options: everything writing the provider reads from symbols (the
    ///     private assemblies it loads, the public types that stand for them under Native AOT, which types they can resolve) is the same.
    /// </summary>
    /// <summary>
    ///     The bound requests count as unchanged while their serialized form and the compilation's references are: resolving against
    ///     referenced assemblies then gives the same results, so that step reuses its last ones.
    /// </summary>
    private sealed class SameReferencedInputComparer : IEqualityComparer<RequestsResult>
    {
        public static SameReferencedInputComparer Instance { get; } = new();

        public bool Equals(RequestsResult? x, RequestsResult? y) =>
            ReferenceEquals(x, y) || ( x is { } && y is { } && x.Key.Equals(y.Key) && SameReferencesComparer.Instance.Equals(x.Compilation, y.Compilation) );

        public int GetHashCode(RequestsResult obj) => obj.Key.GetHashCode();
    }

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
