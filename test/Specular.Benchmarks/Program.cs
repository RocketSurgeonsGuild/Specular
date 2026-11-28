using BenchmarkDotNet.Running;
using Specular.Benchmarks;

if (args is ["--validate"])
{
    GeneratorBenchmarks.Validate();
    return;
}

if (args is ["--steps", var workloadName])
{
    StepTimings.Print(workloadName);
    return;
}

_ = BenchmarkSwitcher.FromAssembly(typeof(GeneratorBenchmarks).Assembly).Run(args);

namespace Specular.Tests
{
    /// <summary>The linked test sources only read this for temp paths, which the benchmarks never use.</summary>
    internal static class ModuleInitializer
    {
        public static string TempDirectory => Path.GetTempPath();
    }
}
