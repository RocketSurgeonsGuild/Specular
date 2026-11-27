using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Specular.Benchmarks;

/// <summary>
///     The generator's two costs, run by hand and never in CI:
///     <c>dotnet run -c Release --project test/Specular.Benchmarks -- --filter "*"</c>
///     (add <c>--filter "*EditRerun*"</c> or <c>--filter "*ColdRun*"</c>, or <c>--anyCategories</c>, to narrow it).
///     <list type="bullet">
///         <item><see cref="ColdRun" />: one run on a new compilation with freshly loaded references, as a command-line build pays.</item>
///         <item><see cref="EditRerun" />: the same driver again after a comment is appended to one file, as the IDE pays per keystroke.</item>
///     </list>
///     Each measured invocation is a single generator run; the setup that prepares it (new references, the edited compilation) is not timed.
/// </summary>
[MemoryDiagnoser]
[SimpleJob(RunStrategy.Monitoring, warmupCount: 5, iterationCount: 50, invocationCount: 1)]
public class GeneratorBenchmarks
{
    private Workload _workload = null!;
    private GeneratorDriver _driver = null!;
    private Compilation _compilation = null!;
    private SyntaxTree _editedTree = null!;
    private int _edits;

    public static IEnumerable<string> WorkloadNames => Workloads.Names;

    [ParamsSource(nameof(WorkloadNames))]
    public string Workload { get; set; } = "";

    [GlobalSetup(Target = nameof(ColdRun))]
    public void SetupCold() => _workload = Workloads.Get(Workload);

    [IterationSetup(Target = nameof(ColdRun))]
    public void PrepareCold()
    {
        _compilation = _workload.CreateCompilation();
        // Binding the references is the compiler's work, done before any generator runs in a real build.
        _ = _compilation.SourceModule.ReferencedAssemblySymbols;
        _driver = _workload.CreateDriver();
        Settle();
    }

    [Benchmark]
    public GeneratorDriver ColdRun() => _driver.RunGenerators(_compilation);

    [GlobalSetup(Target = nameof(EditRerun))]
    public void SetupEdit()
    {
        _workload = Workloads.Get(Workload);
        _compilation = _workload.CreateCompilation();
        _editedTree = _compilation.SyntaxTrees.First(z => z.FilePath == _workload.EditedTree.FilePath);
        _driver = _workload.CreateDriver().RunGenerators(_compilation);
    }

    [IterationSetup(Target = nameof(EditRerun))]
    public void PrepareEdit()
    {
        var text = _editedTree.GetText();
        var edited = _editedTree.WithChangedText(text.WithChanges(new TextChange(new(text.Length, 0), $"\n// edit {_edits++}")));
        _compilation = _compilation.ReplaceSyntaxTree(_editedTree, edited);
        _editedTree = edited;
        Settle();
    }

    // A full collection before each measured run, so one run's garbage is not collected on the next run's time.
    private static void Settle()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    [Benchmark]
    public GeneratorDriver EditRerun() => _driver = _driver.RunGenerators(_compilation);

    /// <summary>Checks every workload compiles and generates without errors before spending time measuring it.</summary>
    public static void Validate()
    {
        foreach (var name in Workloads.Names)
        {
            var workload = Workloads.Get(name);
            var compilation = workload.CreateCompilation();
            var driver = workload.CreateDriver().RunGeneratorsAndUpdateCompilation(compilation, out var output, out var generatorDiagnostics);
            var errors = generatorDiagnostics
                        .Concat(output.GetDiagnostics())
                        .Where(z => z.Severity == DiagnosticSeverity.Error)
                        .ToArray();
            var generated = driver.GetRunResult().GeneratedTrees.Sum(z => z.Length);
            Console.WriteLine($"{name}: {compilation.References.Count()} references, {generated:N0} generated chars, {errors.Length} errors");
            foreach (var error in errors.Take(5)) Console.WriteLine($"    {error}");
            if (errors.Length > 0) Environment.ExitCode = 1;
        }
    }
}
