using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Specular.Scanning.Tests.Caching;
using Specular.Scanning.Tests.Scanning;
using TUnit.Assertions.Should.Extensions;

namespace Specular.Scanning.Tests.Incremental;

public class IncrementalScanTests
{
    private const string ConsumerId = "IncrementalGen";

    private const string LibrarySource = """
        namespace Lib;
        public interface IHandler { }
        public class FirstHandler : IHandler { }
        public class SecondHandler : IHandler { }
        public class Unrelated { }
        """;

    [Test]
    public async Task Each_request_produces_its_result()
    {
        var library = MetadataReference.CreateFromImage(CacheTestHelpers.EmitLibrary(LibrarySource));
        var generator = new ScanningGenerator(
            ConsumerId,
            compilation => ScanRequest.Build(compilation, s => s.EntryAssembly().FromAssembly("Lib").GetTypes(f => f.EndsWith("Handler"))),
            compilation => ScanRequest.Build(compilation, s => s.FromAssembly("Lib"))
        );

        _ = Run(generator, CreateConsumer(library, "public class ConsumerHandler { }"));

        await generator.Results.Count.Should().BeEqualTo(2);
        await Scans.TypeNames(generator.Results.Single(static z => z.Request.Kind == ScanKind.Types))
                   .Should()
                   .BeEquivalentTo(["ConsumerHandler", "Lib.FirstHandler", "Lib.IHandler", "Lib.SecondHandler"]);
        await Scans.AssemblyNames(generator.Results.Single(static z => z.Request.Kind == ScanKind.Assemblies)).Should().BeEquivalentTo(["Lib"]);
    }

    [Test]
    public async Task An_unrelated_edit_leaves_the_results_unchanged()
    {
        var library = MetadataReference.CreateFromImage(CacheTestHelpers.EmitLibrary(LibrarySource));
        var generator = new ScanningGenerator(ConsumerId, compilation => ScanRequest.Build(compilation, s => s.FromAssemblyOf(Handler(compilation)).GetTypes()));
        var compilation = CreateConsumer(library, "public class ConsumerType { }");

        var driver = Run(generator, compilation);
        driver = driver.RunGenerators(compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText("public class Unrelated { }")));

        var steps = driver.GetRunResult().Results.Single().TrackedSteps[ScanTrackingNames.Results];
        var reasons = steps.SelectMany(static z => z.Outputs).Select(static z => z.Reason).ToList();
        await reasons.Count.Should().BeGreaterThan(0);
        await reasons.All(static reason => reason is IncrementalStepRunReason.Cached or IncrementalStepRunReason.Unchanged).Should().BeTrue();
    }

    [Test]
    public async Task A_reference_is_not_rescanned_while_it_is_unchanged()
    {
        var library = MetadataReference.CreateFromImage(CacheTestHelpers.EmitLibrary(LibrarySource));
        var filter = new RecordingNameFilter("handlers-v1", static type => type.Name.EndsWith("Handler", StringComparison.Ordinal));
        var generator = new ScanningGenerator(ConsumerId, compilation => ScanRequest.Build(compilation, s => s.EntryAssembly().FromAssembly("Lib").GetTypes(f => f.Where(filter))));
        var compilation = CreateConsumer(library, "public class ConsumerHandler { }");

        var driver = Run(generator, compilation);
        await filter.Visited.Should().Contain("FirstHandler");

        filter.Visited.Clear();
        _ = driver.RunGenerators(compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText("public class SecondConsumerHandler { }")));

        await filter.Visited.Should().NotContain("FirstHandler");
        await filter.Visited.Should().Contain("SecondConsumerHandler");
        await Scans.TypeNames(generator.Results.Single()).Should().Contain("Lib.FirstHandler");
    }

    [Test]
    public async Task The_cache_file_is_written_and_serves_the_next_build()
    {
        using var directory = CacheTestHelpers.CreateTempDirectory();
        var image = CacheTestHelpers.EmitLibrary(LibrarySource);
        var options = new TestOptionsProvider(CacheTestHelpers.Options(("build_property.IntermediateOutputPath", directory.Path)));
        var filter = new RecordingNameFilter("handlers-v1", static type => type.Name.EndsWith("Handler", StringComparison.Ordinal));
        var generator = new ScanningGenerator(ConsumerId, compilation => ScanRequest.Build(compilation, s => s.EntryAssembly().FromAssembly("Lib").GetTypes(f => f.Where(filter))));

        _ = Run(generator, CreateConsumer(MetadataReference.CreateFromImage(image), "public class ConsumerHandler { }"), options);
        var cacheFile = Path.Combine(directory.Path, "ctp", ConsumerId + ".ctpjson");
        await File.Exists(cacheFile).Should().BeTrue();

        // A new build: a fresh reference to the same assembly (so nothing is remembered in memory), fed the file the last build wrote.
        filter.Visited.Clear();
        _ = Run(
            generator,
            CreateConsumer(MetadataReference.CreateFromImage(image), "public class ConsumerHandler { }"),
            options,
            CacheTestHelpers.CacheText(ConsumerId, await File.ReadAllTextAsync(cacheFile), Path.GetDirectoryName(cacheFile)!)
        );

        await filter.Visited.Should().NotContain("FirstHandler");
        await Scans.TypeNames(generator.Results.Single()).Should().Contain("Lib.FirstHandler");
    }

    [Test]
    public async Task Nothing_is_written_outside_msbuild()
    {
        var generator = new ScanningGenerator(ConsumerId, compilation => ScanRequest.Build(compilation, s => s.EntryAssembly().GetTypes()));

        var driver = Run(generator, CreateConsumer(MetadataReference.CreateFromImage(CacheTestHelpers.EmitLibrary(LibrarySource)), "public class A { }"));

        await driver.GetRunResult().Diagnostics.Should().BeEmpty();
        await generator.Results.Count.Should().BeEqualTo(1);
    }

    private static INamedTypeSymbol Handler(Compilation compilation) =>
        compilation.GetTypeByMetadataName("Lib.IHandler") ?? throw new InvalidOperationException("Lib.IHandler is missing.");

    private static CSharpCompilation CreateConsumer(MetadataReference library, string source) =>
        CacheTestHelpers.CreateConsumer(library).AddSyntaxTrees(CSharpSyntaxTree.ParseText(source));

    private static GeneratorDriver Run(
        ScanningGenerator generator,
        Compilation compilation,
        AnalyzerConfigOptionsProvider? options = null,
        params AdditionalText[] additionalTexts
    ) =>
        CSharpGeneratorDriver
           .Create(
                [generator.AsSourceGenerator()],
                additionalTexts,
                optionsProvider: options,
                driverOptions: new(IncrementalGeneratorOutputKind.None, trackIncrementalGeneratorSteps: true)
            )
           .RunGenerators(compilation);

    // A generator as a third-party author would write it: build requests from the compilation, scan, use the results.
    private sealed class ScanningGenerator(string consumerId, params Func<Compilation, ScanRequest>[] requests) : IIncrementalGenerator
    {
        public List<ScanResult> Results { get; } = [];

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var scanRequests = context
                              .CompilationProvider
                              .SelectMany((compilation, _) => requests.Select(request => request(compilation)).ToImmutableArray());

            context.RegisterSourceOutput(
                context.Scan(consumerId, scanRequests).Collect(),
                (_, results) =>
                {
                    Results.Clear();
                    Results.AddRange(results);
                }
            );
        }
    }

    private sealed class TestOptionsProvider(AnalyzerConfigOptions globalOptions) : AnalyzerConfigOptionsProvider
    {
        public override AnalyzerConfigOptions GlobalOptions { get; } = globalOptions;

        public override AnalyzerConfigOptions GetOptions(SyntaxTree tree) => GlobalOptions;

        public override AnalyzerConfigOptions GetOptions(AdditionalText textFile) => GlobalOptions;
    }
}
