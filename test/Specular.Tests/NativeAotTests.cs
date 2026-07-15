using Microsoft.CodeAnalysis;

namespace Specular.Tests;

[Timeout(ModuleInitializer.TestTimeout)]
public class NativeAotTests : GeneratorTest
{
    [Test]
    public async Task Should_Resolve_Inaccessible_Types_Via_Public_Type_Assembly_Under_Aot(CancellationToken cancellationToken)
    {
        using var assemblyLoadContext = new CollectibleTestAssemblyLoadContext();
        var other = await Builder
                         .WithProjectName("AotLibrary")
                         .AddSources(
                              """
                              namespace AotLib;

                              public interface IThing { }
                              public class PublicThing : IThing { }
                              internal class InternalThing : IThing { }
                              """
                          )
                         .AddCacheOptions(GetTempPath())
                         .Build()
                         .GenerateAsync(cancellationToken);

        other.FinalDiagnostics.Where(x => x.Severity >= DiagnosticSeverity.Error).ShouldBeEmpty();

        var result = await Builder
                          .AddCompilationReferences(other)
                          .AddSources(
                               """
                               using Specular;
                               using Microsoft.Extensions.DependencyInjection;

                               class Program
                               {
                                   static void Main() { }

                                   static IServiceCollection Load()
                                   {
                                       var services = new ServiceCollection();
                                       var provider = SpecularProvider.Instance;
                                       provider.Scan(
                                           services,
                                           z => z
                                              .FromAssemblyOf<AotLib.IThing>()
                                              .AddClasses(x => x.AssignableTo<AotLib.IThing>())
                                              .AsSelf()
                                              .WithSingletonLifetime()
                                       );
                                       return services;
                                   }
                               }
                               """
                           )
                          .WithDiagnosticSeverity(DiagnosticSeverity.Info)
                          .AddGlobalOption("build_property.PublishAot", "true")
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        result.FinalDiagnostics.Where(x => x.Severity >= DiagnosticSeverity.Error).ShouldBeEmpty();
        await Verify(result);
    }

    [Test]
    public async Task Should_Skip_And_Emit_Info_Diagnostic_When_Assembly_Has_No_Public_Types_Under_Aot(CancellationToken cancellationToken)
    {
        using var assemblyLoadContext = new CollectibleTestAssemblyLoadContext();
        var other = await Builder
                         .WithProjectName("SecretLibrary")
                         .AddSources(
                              """
                              namespace SecretLib;

                              internal interface ISecret { }
                              internal class SecretService : ISecret { }
                              """
                          )
                         .AddCacheOptions(GetTempPath())
                         .Build()
                         .GenerateAsync(cancellationToken);

        other.FinalDiagnostics.Where(x => x.Severity >= DiagnosticSeverity.Error).ShouldBeEmpty();

        var result = await Builder
                          .AddCompilationReferences(other)
                          .AddSources(
                               """
                               using Specular;
                               using System;
                               using System.Collections.Generic;

                               class Program
                               {
                                   static void Main() { }

                                   static IEnumerable<Type> Load()
                                   {
                                       var provider = SpecularProvider.Instance;
                                       return provider.GetTypes(z => z.FromAssemblies().GetTypes(x => x.InNamespaces("SecretLib")));
                                   }
                               }
                               """
                           )
                          .WithDiagnosticSeverity(DiagnosticSeverity.Info)
                          .AddGlobalOption("build_property.EnableAotAnalyzer", "true")
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        result.FinalDiagnostics.Where(x => x.Severity >= DiagnosticSeverity.Error).ShouldBeEmpty();
        result.FinalDiagnostics.ShouldContain(d => d.Id == "RSGD0008" && d.Severity == DiagnosticSeverity.Info);
        await Verify(result);
    }
}
