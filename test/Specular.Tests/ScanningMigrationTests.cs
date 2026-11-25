using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;
using Rocket.Surgery.Extensions.Testing.SourceGenerators;
using Serilog;
using TestAssembly;

namespace Specular.Tests;

[Timeout(ModuleInitializer.TestTimeout)]
public class ScanningMigrationTests : GeneratorTest
{
    // Specific assemblies are alternatives: FromAssemblyOf<A>().FromAssemblyOf<B>() selects A and B (it used to select nothing).
    [Test]
    public async Task Should_Select_Several_Specific_Assemblies_Using_FromAssemblyOf(CancellationToken cancellationToken)
    {
        var dependencyA = await CreateServiceDependency(AssemblyLoadContext, Logger, GetTempPath(), "A", cancellationToken);
        var dependencyB = await CreateServiceDependency(AssemblyLoadContext, Logger, GetTempPath(), "B", cancellationToken);
        var dependencyC = await CreateServiceDependency(AssemblyLoadContext, Logger, GetTempPath(), "C", cancellationToken);

        var result = await Builder
                          .AddSources(
                               """
                               using Specular;
                               using Specular.Abstractions;
                               using Microsoft.Extensions.DependencyInjection;
                               using DependencyProjectA;
                               using DependencyProjectB;

                               namespace TestProject
                               {
                                   public static class Program
                                   {
                                       static void Main() { }
                                       static IServiceCollection LoadServices()
                                       {
                                           var services = new ServiceCollection();
                                           ISpecularProvider provider = SpecularProvider.Instance;
                                           provider.Scan(
                                               services,
                                               z => z
                                                   .FromAssemblyOf<IServiceA>()
                                                   .FromAssemblyOf<IServiceB>()
                                                   .AddClasses(true)
                                                   .AsSelf()
                                                   .WithSingletonLifetime()
                                           );
                                           return services;
                                       }
                                   }
                               }
                               """
                           )
                          .AddCompilationReferences(dependencyA, dependencyB, dependencyC)
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        var services = await StaticHelper.ExecuteStaticServiceCollectionMethod(result, "Program", "LoadServices");
        var implementationTypes = services.Select(z => z.ImplementationType?.FullName ?? "").Order().ToArray();
        await Assert.That(implementationTypes).IsEquivalentTo(["DependencyProjectA.ServiceA", "DependencyProjectB.ServiceB"]);
        await Verify(new GeneratorTestResultsWithServices(result, services));
    }

    // A closed generic in a selector matches that closed type only, including one with an array type argument (IHandler<string[]>).
    [Test]
    public async Task Should_Match_Closed_Generics_Exactly(CancellationToken cancellationToken)
    {
        var result = await Builder
                          .AddSources(
                               """
                               using Specular;
                               using Specular.Abstractions;
                               using Microsoft.Extensions.DependencyInjection;

                               namespace TestProject
                               {
                                   public interface IHandler<T> { }
                                   public class StringHandler : IHandler<string> { }
                                   public class IntHandler : IHandler<int> { }
                                   public class ArrayHandler : IHandler<string[]> { }

                                   public static class Program
                                   {
                                       static void Main() { }
                                       static IServiceCollection LoadServices()
                                       {
                                           var services = new ServiceCollection();
                                           ISpecularProvider provider = SpecularProvider.Instance;
                                           provider.Scan(
                                               services,
                                               z => z
                                                   .FromAssemblyOf<StringHandler>()
                                                   .AddClasses(x => x.AssignableTo<IHandler<string>>())
                                                   .AsSelf()
                                                   .WithSingletonLifetime()
                                           );
                                           provider.Scan(
                                               services,
                                               z => z
                                                   .FromAssemblyOf<StringHandler>()
                                                   .AddClasses(x => x.AssignableTo(typeof(IHandler<string[]>)))
                                                   .AsSelf()
                                                   .WithScopedLifetime()
                                           );
                                           return services;
                                       }
                                   }
                               }
                               """
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        var services = await StaticHelper.ExecuteStaticServiceCollectionMethod(result, "Program", "LoadServices");
        var registrations = services.Select(z => $"{z.Lifetime} {z.ImplementationType?.FullName}").Order().ToArray();
        await Assert.That(registrations).IsEquivalentTo(["Scoped TestProject.ArrayHandler", "Singleton TestProject.StringHandler"]);
        await Verify(new GeneratorTestResultsWithServices(result, services));
    }

    // A selector written inside a generic method can name a type parameter (IHandler<T>), which no type reference can: Specular matches
    // it against the symbol itself. Resolved at compile time, IHandler<T> is a type nothing in the project implements, so it finds nothing.
    [Test]
    public async Task Should_Match_A_Type_Parameter_Argument_Against_The_Symbol(CancellationToken cancellationToken)
    {
        var result = await Builder
                          .AddSources(
                               """
                               using Specular;
                               using Specular.Abstractions;
                               using Microsoft.Extensions.DependencyInjection;

                               namespace TestProject
                               {
                                   public interface IHandler<T> { }
                                   public class StringHandler : IHandler<string> { }
                                   public class ArrayHandler : IHandler<string[]> { }

                                   public static class Program
                                   {
                                       static void Main() { }
                                       static IServiceCollection LoadServices()
                                       {
                                           var services = new ServiceCollection();
                                           Register<string>(services);
                                           ISpecularProvider provider = SpecularProvider.Instance;
                                           provider.Scan(
                                               services,
                                               z => z
                                                   .FromAssemblyOf<StringHandler>()
                                                   .AddClasses(x => x.AssignableTo(typeof(IHandler<string[]>)))
                                                   .AsSelf()
                                                   .WithScopedLifetime()
                                           );
                                           return services;
                                       }

                                       static void Register<T>(IServiceCollection services)
                                       {
                                           ISpecularProvider provider = SpecularProvider.Instance;
                                           provider.Scan(
                                               services,
                                               z => z
                                                   .FromAssemblyOf<StringHandler>()
                                                   .AddClasses(x => x.AssignableTo(typeof(IHandler<T>)))
                                                   .AsSelf()
                                                   .WithSingletonLifetime()
                                           );
                                       }
                                   }
                               }
                               """
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        var services = await StaticHelper.ExecuteStaticServiceCollectionMethod(result, "Program", "LoadServices");
        var registrations = services.Select(z => $"{z.Lifetime} {z.ImplementationType?.FullName}").Order().ToArray();
        await Assert.That(registrations).IsEquivalentTo(["Scoped TestProject.ArrayHandler"]);
        await Verify(new GeneratorTestResultsWithServices(result, services));
    }

    // The cache file written by one run is read back by the next: a cached result for a referenced assembly is used as is.
    [Test]
    public async Task Should_Read_The_Cache_File_Back_On_The_Next_Run(CancellationToken cancellationToken)
    {
        const string source = """
                              using Specular;
                              using Specular.Abstractions;
                              using TestAssembly;

                              public static class Program
                              {
                                  static void Main()
                                  {
                                      ISpecularProvider provider = SpecularProvider.Instance;
                                      provider.GetTypes(z => z.FromAssemblyOf<IService>().GetTypes(x => x.AssignableTo<IService>()));
                                  }
                              }
                              """;
        var tempPath = GetTempPath();
        var cacheFile = Path.Combine(tempPath, "obj/net9.0/ctp/SpecularProvider.ctpjson");

        var first = await Builder
                         .AddSources(source)
                         .AddReferences(typeof(IService))
                         .AddCacheOptions(tempPath)
                         .Build()
                         .GenerateAsync(cancellationToken);
        first.AssertGenerationWasSuccessful();
        await Assert.That(File.Exists(cacheFile)).IsTrue();

        const string scanned = "typeof(global::TestAssembly.Service)";
        const string fromCache = "typeof(global::TestAssembly.Service /* from the cache */)";
        await Assert.That(GetProvider(first)).Contains(scanned);
        var cache = await File.ReadAllTextAsync(cacheFile, cancellationToken);
        await Assert.That(cache).Contains(scanned);
        await File.WriteAllTextAsync(cacheFile, cache.Replace(scanned, fromCache, StringComparison.Ordinal), cancellationToken);

        // A second load context: both runs emit an assembly with the same name.
        using var secondLoadContext = new CollectibleTestAssemblyLoadContext();
        var second = await Builder
                          .WithAssemblyLoadContext(secondLoadContext)
                          .AddSources(source)
                          .AddReferences(typeof(IService))
                          .PopulateCache(tempPath)
                          .Build()
                          .GenerateAsync(cancellationToken);
        second.AssertGenerationWasSuccessful();

        await Assert.That(GetProvider(second)).Contains(fromCache);
        // Apart from the content hash, which covers the edited text.
        await Assert.That(WithoutHash(GetProvider(second).Replace(fromCache, scanned, StringComparison.Ordinal))).IsEqualTo(WithoutHash(GetProvider(first)));
        // Entries read during a run are written back, so the next build still finds them.
        await Assert.That(await File.ReadAllTextAsync(cacheFile, cancellationToken)).Contains(fromCache);

        static string GetProvider(GeneratorTestResults results) =>
            results
               .Results
               .SelectMany(z => z.Value.SyntaxTrees)
               .Single(z => z.FilePath.EndsWith("SpecularProvider.g.cs", StringComparison.Ordinal))
               .GetText(CancellationToken.None)
               .ToString();

        static string WithoutHash(string provider) =>
            string.Join('\n', provider.Split('\n').Where(line => !line.Contains("SpecularHashAttribute", StringComparison.Ordinal)));
    }

    private static async Task<GeneratorTestResults> CreateServiceDependency(
        AssemblyLoadContext context,
        ILogger logger,
        string tempPath,
        string suffix,
        CancellationToken cancellationToken
    )
    {
        var generator = await GeneratorTestContextBuilder
                             .Create()
                             .WithProjectName("DependencyProject" + suffix)
                             .WithLogger(logger)
                             .WithAssemblyLoadContext(context)
                             .AddSources(
                                  $$"""
                                    namespace DependencyProject{{suffix}}
                                    {
                                        public interface IService{{suffix}} { }
                                        public class Service{{suffix}} : IService{{suffix}} { }
                                    }
                                    """
                              )
                             .AddGlobalOption("build_property.IntermediateOutputPath", tempPath)
                             .Build()
                             .GenerateAsync(cancellationToken);
        generator.AssertCompilationWasSuccessful();
        generator.AssertGenerationWasSuccessful();
        return generator;
    }

    private static class StaticHelper
    {
        public static async Task<IServiceCollection> ExecuteStaticServiceCollectionMethod(Assembly? assembly, string className, string methodName)
        {
            if (assembly is null) return new ServiceCollection();

            var @class = assembly.GetTypes().FirstOrDefault(z => z.IsClass && z.Name == className)!;
            await Assert.That(@class).IsNotNull();

            var method = @class.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            await Assert.That(method).IsNotNull();

            return ( method!.Invoke(null, []) as IServiceCollection )!;
        }
    }
}
