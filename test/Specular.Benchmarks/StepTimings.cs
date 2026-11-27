using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Specular.Analyzers;

namespace Specular.Benchmarks;

/// <summary>
///     Where one generator run spends its time, by named pipeline step: <c>dotnet run -c Release --project test/Specular.Benchmarks -- --steps synthetic-t5000-r50</c>.
///     Prints the median over 20 edit reruns after a cold run; the generator's total includes the outputs, which are not steps.
/// </summary>
internal static class StepTimings
{
    private const int Edits = 20;

    public static void Print(string workloadName)
    {
        var workload = Workloads.Get(workloadName);
        var compilation = (Compilation)workload.CreateCompilation();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(
            [new SpecularProviderGenerator().AsSourceGenerator()],
            parseOptions: Workloads.ParseOptions,
            optionsProvider: workload.Options,
            driverOptions: new(IncrementalGeneratorOutputKind.None, trackIncrementalGeneratorSteps: true)
        );
        driver = driver.RunGenerators(compilation);
        Report("cold", driver);

        var tree = compilation.SyntaxTrees.First(z => z.FilePath == workload.EditedTree.FilePath);
        var samples = new Dictionary<string, List<double>>(StringComparer.Ordinal);
        for (var i = 0; i < Edits; i++)
        {
            var text = tree.GetText();
            var edited = tree.WithChangedText(text.WithChanges(new TextChange(new(text.Length, 0), $"\n// edit {i}")));
            compilation = compilation.ReplaceSyntaxTree(tree, edited);
            tree = edited;
            driver = driver.RunGenerators(compilation);
            foreach (var (step, ms) in Steps(driver))
            {
                if (!samples.TryGetValue(step, out var list)) samples[step] = list = [];
                list.Add(ms);
            }
        }

        Console.WriteLine($"edit rerun, median of {Edits}:");
        foreach (var (step, list) in samples)
        {
            list.Sort();
            Console.WriteLine($"  {step,-20} {list[list.Count / 2],8:F2} ms");
        }
    }

    private static void Report(string label, GeneratorDriver driver)
    {
        Console.WriteLine($"{label}:");
        foreach (var (step, ms) in Steps(driver)) Console.WriteLine($"  {step,-20} {ms,8:F2} ms");
    }

    private static IEnumerable<(string Step, double Milliseconds)> Steps(GeneratorDriver driver)
    {
        var result = driver.GetRunResult().Results[0];
        foreach (var (name, runs) in result.TrackedSteps)
        {
            if (!name.StartsWith("Specular", StringComparison.Ordinal)) continue;

            yield return (name, runs.Sum(z => z.ElapsedTime.TotalMilliseconds));
        }

        yield return ("(generator total)", driver.GetTimingInfo().GeneratorTimes[0].ElapsedTime.TotalMilliseconds);
    }
}
