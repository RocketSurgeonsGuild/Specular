using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Specular.Scanning.Tests.Caching;
using Specular.Scanning.Tests.Scanning;
using TUnit.Assertions.Should.Extensions;

namespace Specular.Scanning.Tests.Incremental;

/// <summary>Scans and the typed store under one consumer id, through <c>context.Scan(consumerId, requests, transform)</c>.</summary>
public class ScanTransformTests
{
    private const string ConsumerId = "TransformGen";
    private const string StoreKey = "manifest";

    private const string LibrarySource = """
        namespace Lib;
        public interface IHandler { }
        public class FirstHandler : IHandler { }
        public class SecondHandler : IHandler { }
        public class Outer<T> { public class InnerHandler : IHandler { } }
        """;

    [Test]
    public async Task The_transform_gets_the_results_in_request_order()
    {
        var library = MetadataReference.CreateFromImage(CacheTestHelpers.EmitLibrary(LibrarySource));
        var generator = new StoreGenerator(
            ConsumerId,
            1,
            compilation => ScanRequest.Build(compilation, s => s.FromAssembly("Lib")),
            compilation => ScanRequest.Build(compilation, s => s.FromAssembly("Lib").GetTypes(f => f.EndsWith("Handler")))
        );

        _ = Run(generator, CreateConsumer(library));

        var output = generator.Outputs.Single();
        await output.Results.Count.Should().BeEqualTo(2);
        await output.Results[0].Request.Kind.Should().BeEqualTo(ScanKind.Assemblies);
        await output.Results[1].Request.Kind.Should().BeEqualTo(ScanKind.Types);
        await Scans.TypeNames(output.Results[1]).Should().BeEquivalentTo(["Lib.FirstHandler", "Lib.IHandler", "Lib.Outer`1+InnerHandler", "Lib.SecondHandler"]);
        await output.Manifest.Should().BeEqualTo(new TestPayload("Lib", 4));
    }

    [Test]
    public async Task Scans_and_the_typed_store_share_one_file_and_both_serve_the_next_build()
    {
        using var directory = CacheTestHelpers.CreateTempDirectory();
        var image = CacheTestHelpers.EmitLibrary(LibrarySource);
        var options = new TestOptionsProvider(CacheTestHelpers.Options(("build_property.IntermediateOutputPath", directory.Path)));
        var filter = new RecordingNameFilter("handlers-v1", static type => type.Name.EndsWith("Handler", StringComparison.Ordinal));
        var generator = new StoreGenerator(ConsumerId, 1, compilation => ScanRequest.Build(compilation, s => s.FromAssembly("Lib").GetTypes(f => f.Where(filter))));

        _ = Run(generator, CreateConsumer(MetadataReference.CreateFromImage(image)), options);
        var cacheFile = Path.Combine(directory.Path, "ctp", ConsumerId + ".ctpjson");
        var written = ScanCacheFile.Parse(await File.ReadAllTextAsync(cacheFile));

        await written.Scans!.Count.Should().BeEqualTo(1);
        await written.Store!.Keys.Should().BeEquivalentTo([StoreKey]);
        await generator.Computations.Should().BeEqualTo(1);

        // A new build: a fresh reference to the same assembly, so nothing is remembered in memory and everything comes from the file.
        filter.Visited.Clear();
        _ = Run(
            generator,
            CreateConsumer(MetadataReference.CreateFromImage(image)),
            options,
            CacheTestHelpers.CacheText(ConsumerId, await File.ReadAllTextAsync(cacheFile), Path.GetDirectoryName(cacheFile)!)
        );

        await filter.Visited.Should().NotContain("FirstHandler");
        await generator.Computations.Should().BeEqualTo(1);
        await generator.Outputs[^1].Manifest.Should().BeEqualTo(new TestPayload("Lib", 4));
        await Scans.TypeNames(generator.Outputs[^1].Results.Single()).Should().Contain("Lib.FirstHandler");
        // The second build used both kinds of entry, so its save keeps both.
        var rewritten = ScanCacheFile.Parse(await File.ReadAllTextAsync(cacheFile));
        await rewritten.Scans!.Count.Should().BeEqualTo(1);
        await rewritten.Store!.Keys.Should().BeEquivalentTo([StoreKey]);
    }

    [Test]
    public async Task Fully_qualified_names_are_restored_from_the_cache_file()
    {
        using var directory = CacheTestHelpers.CreateTempDirectory();
        var image = CacheTestHelpers.EmitLibrary(LibrarySource);
        var options = new TestOptionsProvider(CacheTestHelpers.Options(("build_property.IntermediateOutputPath", directory.Path)));
        var filter = new RecordingNameFilter("handlers-v1", static type => type.Name.EndsWith("Handler", StringComparison.Ordinal));
        var generator = new StoreGenerator(ConsumerId, 1, compilation => ScanRequest.Build(compilation, s => s.FromAssembly("Lib").GetTypes(f => f.Where(filter))));

        _ = Run(generator, CreateConsumer(MetadataReference.CreateFromImage(image)), options);
        var walked = generator.Outputs[^1].Results.Single().Types;
        var cacheFile = Path.Combine(directory.Path, "ctp", ConsumerId + ".ctpjson");
        filter.Visited.Clear();
        _ = Run(
            generator,
            CreateConsumer(MetadataReference.CreateFromImage(image)),
            options,
            CacheTestHelpers.CacheText(ConsumerId, await File.ReadAllTextAsync(cacheFile), Path.GetDirectoryName(cacheFile)!)
        );
        var cached = generator.Outputs[^1].Results.Single().Types;

        await filter.Visited.Should().BeEmpty();
        await cached.Select(static z => z.FullyQualifiedName)
                    .Should()
                    .BeEquivalentTo(["global::Lib.FirstHandler", "global::Lib.IHandler", "global::Lib.Outer<T>.InnerHandler", "global::Lib.SecondHandler"]);
        await cached.Should().BeEqualTo(walked);
    }

    [Test]
    public async Task Typed_store_values_are_remembered_between_runs_without_a_cache_file()
    {
        // The IDE: one reference instance across edits, and no cache file read back or written.
        var library = MetadataReference.CreateFromImage(CacheTestHelpers.EmitLibrary(LibrarySource));
        var generator = new StoreGenerator(ConsumerId, 1, compilation => ScanRequest.Build(compilation, s => s.FromAssembly("Lib")));
        var compilation = CreateConsumer(library);

        var driver = Run(generator, compilation);
        _ = driver.RunGenerators(compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText("public class Edited { }")));

        await generator.Computations.Should().BeEqualTo(1);
        await generator.Outputs[^1].Manifest.Should().BeEqualTo(new TestPayload("Lib", 4));
    }

    [Test]
    public async Task A_remembered_value_is_still_saved()
    {
        using var directory = CacheTestHelpers.CreateTempDirectory();
        var options = new TestOptionsProvider(CacheTestHelpers.Options(("build_property.IntermediateOutputPath", directory.Path)));
        var library = MetadataReference.CreateFromImage(CacheTestHelpers.EmitLibrary(LibrarySource));
        var generator = new StoreGenerator(ConsumerId, 1, compilation => ScanRequest.Build(compilation, s => s.FromAssembly("Lib")));
        var compilation = CreateConsumer(library);

        // The second run reads nothing from the file: the value comes from memory, and the save must not drop it.
        var driver = Run(generator, compilation, options);
        _ = driver.RunGenerators(compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText("public class Edited { }")));
        var written = ScanCacheFile.Parse(await File.ReadAllTextAsync(Path.Combine(directory.Path, "ctp", ConsumerId + ".ctpjson")));

        await generator.Computations.Should().BeEqualTo(1);
        await written.Store!.Keys.Should().BeEquivalentTo([StoreKey]);
    }

    [Test]
    public async Task Remembered_values_are_kept_apart_by_data_version_and_consumer_id()
    {
        var library = MetadataReference.CreateFromImage(CacheTestHelpers.EmitLibrary(LibrarySource));
        var compilation = CreateConsumer(library);
        static ScanRequest request(Compilation c) => ScanRequest.Build(c, s => s.FromAssembly("Lib"));
        var first = new StoreGenerator(ConsumerId, 1, request);
        var newerVersion = new StoreGenerator(ConsumerId, 2, request);
        var otherConsumer = new StoreGenerator("OtherTransformGen", 1, request);

        _ = Run(first, compilation);
        _ = Run(newerVersion, compilation);
        _ = Run(otherConsumer, compilation);

        await first.Computations.Should().BeEqualTo(1);
        await newerVersion.Computations.Should().BeEqualTo(1);
        await otherConsumer.Computations.Should().BeEqualTo(1);
    }

    [Test]
    public async Task A_remembered_value_is_a_fresh_copy_on_every_read()
    {
        var library = MetadataReference.CreateFromImage(CacheTestHelpers.EmitLibrary(LibrarySource));
        var compilation = CreateConsumer(library);
        var generator = new StoreGenerator(ConsumerId, 1, c => ScanRequest.Build(c, s => s.FromAssembly("Lib")));

        var driver = Run(generator, compilation);
        driver = driver.RunGenerators(compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText("public class Edited { }")));
        _ = driver.RunGenerators(compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText("public class EditedAgain { }")));

        await generator.Computations.Should().BeEqualTo(1);
        await generator.Reads.Count.Should().BeEqualTo(3);
        await ReferenceEquals(generator.Reads[1], generator.Reads[2]).Should().BeFalse();
        await generator.Reads[2].Should().BeEqualTo(generator.Reads[1]);
    }

    [Test]
    public async Task The_output_step_is_unchanged_when_the_transform_returns_an_equal_value()
    {
        var library = MetadataReference.CreateFromImage(CacheTestHelpers.EmitLibrary(LibrarySource));
        var generator = new StoreGenerator(ConsumerId, 1, compilation => ScanRequest.Build(compilation, s => s.FromAssembly("Lib")));
        var compilation = CreateConsumer(library);

        var driver = Run(generator, compilation);
        driver = driver.RunGenerators(compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText("public class Edited { }")));

        var reasons = driver.GetRunResult().Results.Single().TrackedSteps[ScanTrackingNames.Output].SelectMany(static z => z.Outputs).Select(static z => z.Reason).ToList();
        await reasons.Count.Should().BeEqualTo(1);
        await reasons.All(static reason => reason is IncrementalStepRunReason.Cached or IncrementalStepRunReason.Unchanged).Should().BeTrue();
        await generator.Outputs.Count.Should().BeEqualTo(1);
    }

    [Test]
    public async Task Rejects_an_invalid_consumer_id()
    {
        var generator = new StoreGenerator("bad/id", 1, compilation => ScanRequest.Build(compilation, s => s.FromAssembly("Lib")));

        var driver = Run(generator, CreateConsumer());

        await ( driver.GetRunResult().Results.Single().Exception is ArgumentException ).Should().BeTrue();
    }

    private static CSharpCompilation CreateConsumer(params MetadataReference[] references) => CacheTestHelpers.CreateConsumer(references);

    private static GeneratorDriver Run(
        IIncrementalGenerator generator,
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

    private sealed record StoreOutput(EquatableArray<ScanResult> Results, TestPayload Manifest);

    // A generator that scans and derives a value per referenced assembly, keeping both in its one cache file.
    private sealed class StoreGenerator(string consumerId, int dataVersion, params Func<Compilation, ScanRequest>[] requests) : IIncrementalGenerator
    {
        private int _computations;

        public int Computations => Volatile.Read(ref _computations);

        public List<StoreOutput> Outputs { get; } = [];

        public List<TestPayload> Reads { get; } = [];

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var scanRequests = context
                              .CompilationProvider
                              .SelectMany((compilation, _) => requests.Select(request => request(compilation)).ToImmutableArray());

            var output = context.Scan(
                consumerId,
                scanRequests,
                scan =>
                {
                    var library = scan.Compilation.SourceModule.ReferencedAssemblySymbols.Single(static z => z.Name == "Lib");
                    var types = library.GlobalNamespace.GetNamespaceMembers().Single(static z => z.Name == "Lib").GetTypeMembers();
                    if (!scan.Cache.TryGet(StoreKey, library, TestPayloadJsonContext.Default.TestPayload, dataVersion, out var manifest))
                    {
                        _ = Interlocked.Increment(ref _computations);
                        manifest = new(library.Name, types.Length);
                        scan.Cache.Set(StoreKey, library, manifest, TestPayloadJsonContext.Default.TestPayload, dataVersion);
                    }

                    Reads.Add(manifest);
                    return new StoreOutput(scan.Results, manifest);
                }
            );

            context.RegisterSourceOutput(output, (_, value) => Outputs.Add(value));
        }
    }

    private sealed class TestOptionsProvider(AnalyzerConfigOptions globalOptions) : AnalyzerConfigOptionsProvider
    {
        public override AnalyzerConfigOptions GlobalOptions { get; } = globalOptions;

        public override AnalyzerConfigOptions GetOptions(SyntaxTree tree) => GlobalOptions;

        public override AnalyzerConfigOptions GetOptions(AdditionalText textFile) => GlobalOptions;
    }
}
