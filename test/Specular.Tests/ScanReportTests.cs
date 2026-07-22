using TestAssembly;

namespace Specular.Tests;

[Timeout(ModuleInitializer.TestTimeout)]
public partial class ScanReportTests : GeneratorTest
{
    [Test]
    public async Task Should_Generate_Scan_Report(CancellationToken cancellationToken)
    {
        var item = new TestSource("scan-report", ScanReportSource);
        var result = await Builder
                          .AddSources(item.Source)
                          .AddReferences(typeof(IService))
                          .AddCacheOptions(item.GetTempDirectory())
                          .Build()
                          .GenerateAsync(cancellationToken);

        await Verify(result.AddCacheFiles())
             .AddScrubber(z => z.Replace(item.GetTempDirectory(), "{TempPath}"))
             .UseParameters(item.FileSafeName);
    }

    [Test]
    public async Task Should_Not_Generate_Scan_Report_When_Opted_Out(CancellationToken cancellationToken)
    {
        var item = new TestSource("scan-report-opted-out", ScanReportSource);
        var result = await Builder
                          .AddSources(item.Source)
                          .AddReferences(typeof(IService))
                          .AddCacheOptions(item.GetTempDirectory())
                          .AddGlobalOption("build_property.SpecularGenerateDiagnostics", "false")
                          .Build()
                          .GenerateAsync(cancellationToken);

        await Verify(result.AddCacheFiles())
             .AddScrubber(z => z.Replace(item.GetTempDirectory(), "{TempPath}"))
             .UseParameters(item.FileSafeName);
    }

    // Exercises all three scanner-expression kinds (GetAssemblies/GetTypes/Scan), including a
    // multiline selector and an empty type scan, so the report captures the original scan intent
    // as well as the resolved results.
    private const string ScanReportSource =
        """
        using Specular;
        using Specular.Abstractions;
        using Microsoft.Extensions.DependencyInjection;
        using TestAssembly;

        class Program
        {
            public static void Main(string[] args)
            {
                var services = new ServiceCollection();
                ISpecularProvider provider = SpecularProvider.Instance;
                provider.GetAssemblies(
                    z => z
                        .FromAssemblyOf<IService>()
                );
                provider.GetTypes(
                    z => z
                        .FromAssemblyOf<IService>()
                        .GetTypes(x => x.AssignableTo<IService>())
                );
                provider.GetTypes(z => z.FromAssemblyOf<IService>().GetTypes(x => x.InNamespaces("No.Matching.Namespace")));
                provider.Scan(services, z => z.FromAssemblies().AddClasses(x => x.AssignableTo(typeof(IGenericService<>))).AsSelfWithInterfaces().WithScopedLifetime());
            }
        }
        """;
}
