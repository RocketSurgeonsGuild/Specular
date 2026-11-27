using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Specular.Analyzers;
using TestAssembly;

namespace Specular.Tests;

/// <summary>
///     A timing and allocation harness for the generator, run by hand (it is explicit) when changing how the generator builds or
///     lays out code: <c>dotnet run --project test/Specular.Tests -- --treenode-filter "/*/*/GeneratorPerformanceTests/*"</c>.
/// </summary>
[Explicit]
[NotInParallel]
public class GeneratorPerformanceTests : GeneratorTest
{
    private const int WarmupIterations = 3;
    private const int MeasuredIterations = 20;

    [Test]
    [Arguments("Microsoft")]
    [Arguments("")]
    public async Task Measure_All_Together(string excludedAssemblies, CancellationToken cancellationToken)
    {
        var item = AssemblyScanningTestData.GetTestData().Select(z => z()).Single(z => z.Name == "all-together");
        var input = await Builder
                         .AddSources(item.Source)
                         .AddReferences(typeof(IService))
                         .AddGlobalOption("build_property.ExcludeAssemblyFromCTP", excludedAssemblies)
                         .Build()
                         .GenerateAsync(cancellationToken);

        var compilation = input.InputCompilation;
        var optionsProvider = new GlobalOptionsProvider(input.GlobalOptions);

        long generatedLength = 0;
        for (var i = 0; i < WarmupIterations; i++) generatedLength = RunOnce(compilation, input.ParseOptions, optionsProvider).GeneratedLength;

        var times = new List<double>();
        var allocations = new List<long>();
        for (var i = 0; i < MeasuredIterations; i++)
        {
            var (elapsed, allocated, _) = RunOnce(compilation, input.ParseOptions, optionsProvider);
            times.Add(elapsed.TotalMilliseconds);
            allocations.Add(allocated);
        }

        times.Sort();
        allocations.Sort();
        var report =
            $"exclude='{excludedAssemblies}' generated={generatedLength:N0} chars | median {times[times.Count / 2]:F1} ms (min {times[0]:F1}, max {times[^1]:F1}) | median allocated {allocations[allocations.Count / 2] / 1024.0 / 1024.0:F1} MB";
        Console.WriteLine(report);
        if (Environment.GetEnvironmentVariable("SPECULAR_PERF_OUT") is { Length: > 0 } outputPath) await File.AppendAllTextAsync(outputPath, report + "\n", cancellationToken);
    }

    private static (TimeSpan Elapsed, long Allocated, long GeneratedLength) RunOnce(
        Compilation compilation,
        CSharpParseOptions parseOptions,
        AnalyzerConfigOptionsProvider optionsProvider
    )
    {
        GeneratorDriver driver = CSharpGeneratorDriver.Create(
            [new SpecularProviderGenerator().AsSourceGenerator()],
            parseOptions: parseOptions,
            optionsProvider: optionsProvider
        );
        GC.Collect();
        GC.WaitForPendingFinalizers();
        var before = GC.GetTotalAllocatedBytes(true);
        var stopwatch = Stopwatch.StartNew();
        driver = driver.RunGenerators(compilation);
        stopwatch.Stop();
        var allocated = GC.GetTotalAllocatedBytes(true) - before;
        var generated = driver.GetRunResult().GeneratedTrees.Sum(z => (long)z.Length);
        return (stopwatch.Elapsed, allocated, generated);
    }

    private sealed class GlobalOptionsProvider(ImmutableDictionary<string, string> globalOptions) : AnalyzerConfigOptionsProvider
    {
        public override AnalyzerConfigOptions GlobalOptions { get; } = new Options(globalOptions);
        public override AnalyzerConfigOptions GetOptions(SyntaxTree tree) => Options.Empty;
        public override AnalyzerConfigOptions GetOptions(AdditionalText textFile) => Options.Empty;

        private sealed class Options(ImmutableDictionary<string, string> values) : AnalyzerConfigOptions
        {
            public static Options Empty { get; } = new([]);
            public override bool TryGetValue(string key, [NotNullWhen(true)] out string? value) => values.TryGetValue(key, out value);
        }
    }
}
