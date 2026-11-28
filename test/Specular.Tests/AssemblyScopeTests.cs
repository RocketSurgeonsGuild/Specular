using Microsoft.CodeAnalysis;
using TestAssembly;
using TUnit.Assertions.Should.Extensions;

namespace Specular.Tests;

/// <summary>
///     The project's assembly scope as Specular reads it: the SpecularExcludeAssemblies and SpecularIncludeAssemblies properties, which reach
///     the generator as <c>SpecularProviderExcludeAssemblies</c> and <c>SpecularProviderIncludeAssemblies</c>, over the framework default.
/// </summary>
public class AssemblyScopeTests : GeneratorTest
{
    private const string Source = """
        using Specular;
        using Specular.Abstractions;
        using Microsoft.Extensions.DependencyInjection;
        using TestAssembly;

        class Program
        {
            public static void Main()
            {
                ISpecularProvider provider = SpecularProvider.Instance;
                provider.GetAssemblies(z => z.FromAssemblies());
                provider.GetTypes(z => z.FromAssemblies().GetTypes(x => x.AssignableTo<IService>()));
                provider.GetTypes(z => z.FromAssemblyOf<IService>().GetTypes(x => x.AssignableTo<IService>()));
            }
        }
        """;

    private int _runs;

    private async Task<string> GenerateAsync(string? exclude, string? include, CancellationToken cancellationToken, string? cacheDirectory = null)
    {
        // Each run is loaded into the test's load context, so each needs its own assembly name.
        var builder = Builder.WithProjectName($"ScopeProject{++_runs}").AddSources(Source).AddReferences(typeof(IService));
        if (exclude is { }) builder = builder.AddGlobalOption("build_property.SpecularProviderExcludeAssemblies", exclude);
        if (include is { }) builder = builder.AddGlobalOption("build_property.SpecularProviderIncludeAssemblies", include);
        if (cacheDirectory is { })
        {
            builder = File.Exists(Path.Combine(cacheDirectory, "obj/net9.0/ctp/SpecularProvider.ctpjson"))
                ? builder.PopulateCache(cacheDirectory)
                : builder.AddCacheOptions(cacheDirectory);
        }

        var result = await builder.Build().GenerateAsync(cancellationToken);
        await result.FinalDiagnostics.Where(z => z.Severity >= DiagnosticSeverity.Error).Should().BeEmpty();
        return result.FinalCompilation.SyntaxTrees.Single(z => z.FilePath.EndsWith("SpecularProvider.g.cs", StringComparison.Ordinal)).ToString();
    }

    [Test]
    public async Task The_framework_is_out_of_scope_unless_the_project_includes_it(CancellationToken cancellationToken)
    {
        var byDefault = await GenerateAsync(null, null, cancellationToken);
        var included = await GenerateAsync(null, "Microsoft.Extensions.*", cancellationToken);

        // Every provider has a using for the namespace; what counts is a reference to the assembly, by type or by private load.
        await NamesDependencyInjection(byDefault).Should().BeFalse();
        await byDefault.Should().Contain("typeof(global::TestAssembly.IService).Assembly");
        await NamesDependencyInjection(included).Should().BeTrue();
    }

    [Test]
    public async Task An_excluded_assembly_leaves_broad_scans_but_a_selection_that_names_it_still_scans_it(CancellationToken cancellationToken)
    {
        var excluded = await GenerateAsync("test?ssembly", null, cancellationToken);
        var everything = await GenerateAsync("TestAssembly", "*", cancellationToken);

        await excluded.Should().NotContain("typeof(global::TestAssembly.IService).Assembly");
        // Only the FromAssemblyOf<IService>() call still finds TestAssembly's services.
        await CountOf(excluded, "typeof(global::TestAssembly.Service)").Should().BeEqualTo(1);
        await CountOf(everything, "typeof(global::TestAssembly.Service)").Should().BeEqualTo(2);
    }

    [Test]
    public async Task Excluding_an_assembly_overrides_what_an_earlier_build_cached_for_it(CancellationToken cancellationToken)
    {
        var directory = GetTempPath();
        // TestAssembly is a reference, so the first build caches what each call site found in it.
        var first = await GenerateAsync(null, null, cancellationToken, directory);
        var second = await GenerateAsync("TestAssembly", null, cancellationToken, directory);

        await CountOf(first, "typeof(global::TestAssembly.Service)").Should().BeEqualTo(2);
        await CountOf(second, "typeof(global::TestAssembly.Service)").Should().BeEqualTo(1);
    }

    private static bool NamesDependencyInjection(string provider) =>
        provider.Contains("global::Microsoft.Extensions.DependencyInjection.", StringComparison.Ordinal)
     || provider.Contains("MicrosoftExtensionsDependencyInjection", StringComparison.Ordinal);

    private static int CountOf(string text, string value)
    {
        var count = 0;
        for (var index = text.IndexOf(value, StringComparison.Ordinal); index >= 0; index = text.IndexOf(value, index + value.Length, StringComparison.Ordinal)) count++;
        return count;
    }
}
