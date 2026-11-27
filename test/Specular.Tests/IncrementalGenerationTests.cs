using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Specular.Analyzers;
using TestAssembly;

namespace Specular.Tests;

/// <summary>
///     The provider and the scan report are written again only when a run's scan results change: an edit that leaves them alone (in the
///     IDE, most keystrokes) stops after the scan.
/// </summary>
public class IncrementalGenerationTests : GeneratorTest
{
    private const string ModelStep = "SpecularModel";

    [Test]
    public async Task Should_Not_Write_The_Provider_Again_When_The_Results_Are_Unchanged(CancellationToken cancellationToken)
    {
        var (driver, compilation, tree) = await RunOnce(cancellationToken);

        // At the end of the file: a new line above a call site moves it, and its line number is part of the generated code.
        var text = tree.GetText(cancellationToken);
        var edited = tree.WithChangedText(text.WithChanges(new TextChange(new(text.Length, 0), "\n// an edit")));
        driver = driver.RunGenerators(compilation.ReplaceSyntaxTree(tree, edited), cancellationToken);

        var reasons = ModelReasons(driver);
        await Assert.That(reasons).IsNotEmpty();
        await Assert.That(reasons).DoesNotContain(IncrementalStepRunReason.Modified);
        await Assert.That(reasons).DoesNotContain(IncrementalStepRunReason.New);
    }

    [Test]
    public async Task Should_Write_The_Provider_Again_When_A_Scanned_Type_Is_Added(CancellationToken cancellationToken)
    {
        var (driver, compilation, _) = await RunOnce(cancellationToken);

        var added = CSharpSyntaxTree.ParseText(
            "public class AddedService : TestAssembly.IService { }",
            (CSharpParseOptions)compilation.SyntaxTrees.First().Options,
            cancellationToken: cancellationToken
        );
        driver = driver.RunGenerators(compilation.AddSyntaxTrees(added), cancellationToken);

        await Assert.That(ModelReasons(driver)).Contains(IncrementalStepRunReason.Modified);
    }

    private static List<IncrementalStepRunReason> ModelReasons(GeneratorDriver driver) =>
        [.. driver.GetRunResult().Results[0].TrackedSteps[ModelStep].SelectMany(z => z.Outputs).Select(z => z.Reason)];

    private async Task<(GeneratorDriver Driver, Compilation Compilation, SyntaxTree Tree)> RunOnce(CancellationToken cancellationToken)
    {
        var item = AssemblyScanningTestData.GetTestData().Select(z => z()).Single(z => z.Name == "all-together");
        var input = await Builder
                         .AddSources(item.Source)
                         .AddReferences(typeof(IService))
                         .AddGlobalOption("build_property.ExcludeAssemblyFromCTP", "Microsoft")
                         .Build()
                         .GenerateAsync(cancellationToken);

        GeneratorDriver driver = CSharpGeneratorDriver.Create(
            [new SpecularProviderGenerator().AsSourceGenerator()],
            parseOptions: input.ParseOptions,
            optionsProvider: new GlobalOptionsProvider(input.GlobalOptions),
            driverOptions: new(IncrementalGeneratorOutputKind.None, trackIncrementalGeneratorSteps: true)
        );
        driver = driver.RunGenerators(input.InputCompilation, cancellationToken);
        var tree = input.InputCompilation.SyntaxTrees.First(z => z.ToString().Contains("class Program", StringComparison.Ordinal));
        return (driver, input.InputCompilation, tree);
    }
}
