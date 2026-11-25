using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Specular.Scanning.Tests.Caching;
using Specular.Scanning.Tests.Scanning;
using TUnit.Assertions.Should.Extensions;

namespace Specular.Scanning.Tests.Incremental;

// Named requests: a tuple of requests in, a tuple of results out, position for position.
public class TupleScanTests
{
    private const string ConsumerId = "TupleGen";

    private const string LibrarySource = """
        namespace Lib;
        public interface IHandler { }
        public interface IPlugin { }
        public class FirstHandler : IHandler { }
        public class SecondHandler : IHandler { }
        public class FirstPlugin : IPlugin { }
        """;

    [Test]
    public async Task A_pair_of_requests_gives_a_pair_of_results_in_the_same_positions()
    {
        (ScanResult Handlers, ScanResult Plugins)? captured = null;
        var generator = new Generator(context =>
        {
            var requests = context.CompilationProvider.Select(static (compilation, _) => (
                ScanRequest.Build(compilation, s => s.FromAssembly("Lib").GetTypes(f => f.AssignableTo("Lib.IHandler").KindOf(TypeKind.Class))),
                ScanRequest.Build(compilation, s => s.FromAssembly("Lib").GetTypes(f => f.AssignableTo("Lib.IPlugin").KindOf(TypeKind.Class)))
            ));
            context.RegisterSourceOutput(context.Scan(ConsumerId, requests), (_, results) => captured = results);
        });

        Run(generator);

        var (handlers, plugins) = captured ?? throw new InvalidOperationException("The output never ran.");
        await Scans.TypeNames(handlers).Should().BeEquivalentTo(["Lib.FirstHandler", "Lib.SecondHandler"]);
        await Scans.TypeNames(plugins).Should().BeEquivalentTo(["Lib.FirstPlugin"]);
    }

    [Test]
    public async Task Eight_requests_each_get_their_own_result()
    {
        // Eight is the most a tuple type declares directly (the eighth element goes through TRest).
        string[] names = ["FirstHandler", "SecondHandler", "FirstPlugin", "IHandler", "IPlugin", "FirstHandler", "IPlugin", "SecondHandler"];
        (ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult)? captured = null;
        var generator = new Generator(context =>
        {
            var requests = context.CompilationProvider.Select((compilation, _) => (
                ByName(compilation, names[0]), ByName(compilation, names[1]), ByName(compilation, names[2]), ByName(compilation, names[3]),
                ByName(compilation, names[4]), ByName(compilation, names[5]), ByName(compilation, names[6]), ByName(compilation, names[7])
            ));
            context.RegisterSourceOutput(context.Scan(ConsumerId, requests), (_, results) => captured = results);
        });

        Run(generator);

        var results = captured ?? throw new InvalidOperationException("The output never ran.");
        ScanResult[] ordered = [results.Item1, results.Item2, results.Item3, results.Item4, results.Item5, results.Item6, results.Item7, results.Item8];
        for (var i = 0; i < names.Length; i++)
        {
            await Scans.TypeNames(ordered[i]).Should().BeEquivalentTo(["Lib." + names[i]]);
        }
    }

    [Test]
    public async Task Requests_built_as_separate_providers_are_scanned_together()
    {
        (ScanResult Handlers, ScanResult Plugins)? captured = null;
        var generator = new Generator(context =>
        {
            // Built independently, as a generator would from different parts of its pipeline, and combined only to scan.
            var handlers = context.CompilationProvider.Select(static (compilation, _) =>
                ScanRequest.Build(compilation, s => s.FromAssembly("Lib").GetTypes(f => f.AssignableTo("Lib.IHandler").KindOf(TypeKind.Class))));
            var plugins = context.CompilationProvider.Select(static (compilation, _) =>
                ScanRequest.Build(compilation, s => s.FromAssembly("Lib").GetTypes(f => f.AssignableTo("Lib.IPlugin").KindOf(TypeKind.Class))));
            context.RegisterSourceOutput(context.Scan(ConsumerId, handlers, plugins), (_, results) => captured = results);
        });

        Run(generator);

        var (handlerResult, pluginResult) = captured ?? throw new InvalidOperationException("The output never ran.");
        await Scans.TypeNames(handlerResult).Should().BeEquivalentTo(["Lib.FirstHandler", "Lib.SecondHandler"]);
        await Scans.TypeNames(pluginResult).Should().BeEquivalentTo(["Lib.FirstPlugin"]);
    }

    [Test]
    public async Task Eight_separate_providers_each_get_their_own_result()
    {
        string[] names = ["FirstHandler", "SecondHandler", "FirstPlugin", "IHandler", "IPlugin", "FirstHandler", "IPlugin", "SecondHandler"];
        (ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult)? captured = null;
        var generator = new Generator(context =>
        {
            var providers = names.Select(name => context.CompilationProvider.Select((compilation, _) => ByName(compilation, name))).ToArray();
            context.RegisterSourceOutput(
                context.Scan(ConsumerId, providers[0], providers[1], providers[2], providers[3], providers[4], providers[5], providers[6], providers[7]),
                (_, results) => captured = results
            );
        });

        Run(generator);

        var results = captured ?? throw new InvalidOperationException("The output never ran.");
        ScanResult[] ordered = [results.Item1, results.Item2, results.Item3, results.Item4, results.Item5, results.Item6, results.Item7, results.Item8];
        for (var i = 0; i < names.Length; i++)
        {
            await Scans.TypeNames(ordered[i]).Should().BeEquivalentTo(["Lib." + names[i]]);
        }
    }

    [Test]
    public async Task The_same_request_in_two_positions_fills_both()
    {
        (ScanResult, ScanResult)? captured = null;
        var generator = new Generator(context =>
        {
            var requests = context.CompilationProvider.Select(static (compilation, _) => (ByName(compilation, "FirstPlugin"), ByName(compilation, "FirstPlugin")));
            context.RegisterSourceOutput(context.Scan(ConsumerId, requests), (_, results) => captured = results);
        });

        Run(generator);

        var (first, second) = captured ?? throw new InvalidOperationException("The output never ran.");
        await first.Should().BeEqualTo(second);
        await Scans.TypeNames(first).Should().BeEquivalentTo(["Lib.FirstPlugin"]);
    }

    private static ScanRequest ByName(Compilation compilation, string name) =>
        ScanRequest.Build(compilation, s => s.FromAssembly("Lib").GetTypes(f => f.StartsWith(name).EndsWith(name)));

    private static void Run(Generator generator)
    {
        var library = MetadataReference.CreateFromImage(CacheTestHelpers.EmitLibrary(LibrarySource));
        var compilation = CacheTestHelpers.CreateConsumer(library).AddSyntaxTrees(CSharpSyntaxTree.ParseText("public class Consumer { }"));
        _ = CSharpGeneratorDriver.Create(generator).RunGenerators(compilation);
    }

    private sealed class Generator(Action<IncrementalGeneratorInitializationContext> initialize) : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context) => initialize(context);
    }
}
