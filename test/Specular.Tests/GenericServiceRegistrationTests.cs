using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Specular.Tests;

[Timeout(ModuleInitializer.TestTimeout)]
public class GenericServiceRegistrationTests : GeneratorTest
{
    // Calls the shipped AddSpecularServiceRegistrations (not a copy of it), because that is the method the attribute
    // filter and the generated provider have to cooperate on.
    private const string ProgramSource = """
                                         using Specular;
                                         using Specular.Abstractions;
                                         using Microsoft.Extensions.DependencyInjection;

                                         public static class Program
                                         {
                                             static void Main() { }

                                             static IServiceCollection LoadServices()
                                             {
                                                 var services = new ServiceCollection();
                                                 services.AddSpecularServiceRegistrations(SpecularProvider.Instance);
                                                 return services;
                                             }
                                         }
                                         """;

    [Test]
    public async Task Should_Register_Single_Generic_ServiceRegistrationAttribute(CancellationToken cancellationToken)
    {
        var result = await Builder
                          .AddSources(
                               """
                               using Specular;
                               using Microsoft.Extensions.DependencyInjection;

                               public interface IFoo { }
                               public interface IBar { }
                               public interface IBaz { }

                               [ServiceRegistration<IFoo>(ServiceLifetime.Singleton)]
                               public class Foo : IFoo, IBar { }

                               [ServiceRegistration<IBar>(ServiceLifetime.Scoped)]
                               public class Bar : IBar { }

                               [ServiceRegistration<IBaz>(ServiceLifetime.Transient)]
                               public class Baz : IBaz { }
                               """,
                               ProgramSource
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        var services = LoadServices(result);

        Describe(services, "IFoo").ShouldBe(("Foo", ServiceLifetime.Singleton));
        Describe(services, "IBar").ShouldBe(("Bar", ServiceLifetime.Scoped));
        Describe(services, "IBaz").ShouldBe(("Baz", ServiceLifetime.Transient));

        await Verify(new GeneratorTestResultsWithServices(result, services));
    }

    [Test]
    public async Task Should_Register_Multi_Argument_Generic_ServiceRegistrationAttribute(CancellationToken cancellationToken)
    {
        var result = await Builder
                          .AddSources(
                               """
                               using Specular;
                               using Microsoft.Extensions.DependencyInjection;

                               public interface IA { }
                               public interface IB { }
                               public interface IC { }
                               public interface ID { }
                               public interface IE { }
                               public interface IUnlisted { }

                               [ServiceRegistration<IA, IB>(ServiceLifetime.Transient)]
                               public class TwoServices : IA, IB, IUnlisted { }

                               [ServiceRegistration<IC, ID, IE>(ServiceLifetime.Scoped)]
                               public class ThreeServices : IC, ID, IE, IUnlisted { }
                               """,
                               ProgramSource
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        var services = LoadServices(result);

        Describe(services, "IA").ShouldBe(("TwoServices", ServiceLifetime.Transient));
        Describe(services, "IB").ShouldBe(("TwoServices", ServiceLifetime.Transient));
        Describe(services, "IC").ShouldBe(("ThreeServices", ServiceLifetime.Scoped));
        Describe(services, "ID").ShouldBe(("ThreeServices", ServiceLifetime.Scoped));
        Describe(services, "IE").ShouldBe(("ThreeServices", ServiceLifetime.Scoped));
        services.Count(z => z.ServiceType.Name == "IUnlisted").ShouldBe(0);

        await Verify(new GeneratorTestResultsWithServices(result, services));
    }

    [Test]
    public async Task Should_Register_The_Same_Services_For_The_Generic_And_Non_Generic_Forms(CancellationToken cancellationToken)
    {
        var generic = await SummarizeServices(
            """
            using Specular;
            using Microsoft.Extensions.DependencyInjection;

            public interface IFoo { }
            public interface IBar { }

            [ServiceRegistration<IFoo, IBar>(ServiceLifetime.Scoped)]
            public class Service : IFoo, IBar { }
            """,
            cancellationToken
        );
        var nonGeneric = await SummarizeServices(
            """
            using Specular;
            using Microsoft.Extensions.DependencyInjection;

            public interface IFoo { }
            public interface IBar { }

            [ServiceRegistration(ServiceLifetime.Scoped, typeof(IFoo), typeof(IBar))]
            public class Service : IFoo, IBar { }
            """,
            cancellationToken
        );

        generic.ShouldNotBeEmpty();
        generic.ShouldBe(nonGeneric);
    }

    [Test]
    public async Task Should_Apply_RegistrationLifetime_On_The_Implementation_The_Same_For_The_Generic_And_Non_Generic_Forms(CancellationToken cancellationToken)
    {
        var generic = await SummarizeServices(
            """
            using Specular;
            using Microsoft.Extensions.DependencyInjection;

            public interface IFoo { }

            [ServiceRegistration<IFoo>(ServiceLifetime.Singleton)]
            [RegistrationLifetime(ServiceLifetime.Scoped)]
            public class Service : IFoo { }
            """,
            cancellationToken
        );
        var nonGeneric = await SummarizeServices(
            """
            using Specular;
            using Microsoft.Extensions.DependencyInjection;

            public interface IFoo { }

            [ServiceRegistration(ServiceLifetime.Singleton, typeof(IFoo))]
            [RegistrationLifetime(ServiceLifetime.Scoped)]
            public class Service : IFoo { }
            """,
            cancellationToken
        );

        generic.ShouldNotBeEmpty();
        generic.ShouldBe(nonGeneric);
    }

    [Test]
    public async Task Should_Support_RegistrationLifetime_Override_For_Generic_ServiceRegistrationAttribute(CancellationToken cancellationToken)
    {
        var result = await Builder
                          .AddSources(
                               """
                               using Specular;
                               using Microsoft.Extensions.DependencyInjection;

                               [RegistrationLifetime(ServiceLifetime.Scoped)]
                               public interface IFoo { }

                               [ServiceRegistration<IFoo>(ServiceLifetime.Singleton)]
                               public class Foo : IFoo { }
                               """,
                               ProgramSource
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        var services = LoadServices(result);

        Describe(services, "IFoo").ShouldBe(("Foo", ServiceLifetime.Scoped));

        await Verify(new GeneratorTestResultsWithServices(result, services));
    }

    [Test]
    public async Task Should_Report_Diagnostic_For_Duplicate_Generic_ServiceRegistrationAttributes(CancellationToken cancellationToken)
    {
        var result = await Builder
                          .AddSources(
                               """
                               using Specular;
                               using Microsoft.Extensions.DependencyInjection;

                               public interface IService { }

                               [ServiceRegistration<IService>(ServiceLifetime.Scoped)]
                               [ServiceRegistration<IService>(ServiceLifetime.Singleton)]
                               public class Service : IService { }
                               """,
                               ProgramSource
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        await Verify(result);
    }

    [Test]
    public async Task Should_Register_Multiple_Generic_ServiceRegistrationAttributes_On_A_Single_Type(CancellationToken cancellationToken)
    {
        var result = await Builder
                          .AddSources(
                               """
                               using Specular;
                               using Microsoft.Extensions.DependencyInjection;

                               public interface IMyService { }
                               public interface IAlternate { }

                               [ServiceRegistration<IMyService>(ServiceLifetime.Singleton)]
                               [ServiceRegistration<IAlternate>(ServiceLifetime.Transient)]
                               public class DualService : IMyService, IAlternate { }
                               """,
                               ProgramSource
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        var services = LoadServices(result);

        Describe(services, "IMyService").ShouldBe(("DualService", ServiceLifetime.Singleton));
        Describe(services, "IAlternate").ShouldBe(("DualService", ServiceLifetime.Transient));
        services.Count(z => z.ServiceType.Name == "DualService").ShouldBe(1);

        await Verify(new GeneratorTestResultsWithServices(result, services));
    }

    [Test]
    public async Task Should_Register_Mixed_Generic_And_NonGeneric_ServiceRegistrationAttributes_On_A_Single_Type(CancellationToken cancellationToken)
    {
        var result = await Builder
                          .AddSources(
                               """
                               using Specular;
                               using Microsoft.Extensions.DependencyInjection;

                               public interface IFoo { }
                               public interface IBar { }
                               public interface IBaz { }

                               [ServiceRegistration<IFoo>(ServiceLifetime.Singleton)]
                               [ServiceRegistration(ServiceLifetime.Scoped, typeof(IBar))]
                               [ServiceRegistration<IBaz>(ServiceLifetime.Transient)]
                               public class Service : IFoo, IBar, IBaz { }
                               """,
                               ProgramSource
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        var services = LoadServices(result);

        Describe(services, "IFoo").ShouldBe(("Service", ServiceLifetime.Singleton));
        Describe(services, "IBar").ShouldBe(("Service", ServiceLifetime.Scoped));
        Describe(services, "IBaz").ShouldBe(("Service", ServiceLifetime.Transient));
        services.Count(z => z.ServiceType.Name == "Service").ShouldBe(1);

        await Verify(new GeneratorTestResultsWithServices(result, services));
    }

    [Test]
    public async Task Should_Register_Multiple_NonGeneric_ServiceRegistrationAttributes_On_A_Single_Type(CancellationToken cancellationToken)
    {
        var result = await Builder
                          .AddSources(
                               """
                               using Specular;
                               using Microsoft.Extensions.DependencyInjection;

                               public interface IFoo { }
                               public interface IBar { }

                               [ServiceRegistration(ServiceLifetime.Singleton, typeof(IFoo))]
                               [ServiceRegistration(ServiceLifetime.Transient, typeof(IBar))]
                               public class Service : IFoo, IBar { }
                               """,
                               ProgramSource
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        var services = LoadServices(result);

        Describe(services, "IFoo").ShouldBe(("Service", ServiceLifetime.Singleton));
        Describe(services, "IBar").ShouldBe(("Service", ServiceLifetime.Transient));
        services.Count(z => z.ServiceType.Name == "Service").ShouldBe(1);

        await Verify(new GeneratorTestResultsWithServices(result, services));
    }

    [Test]
    public async Task Should_Report_Diagnostic_For_Duplicate_Mixed_ServiceRegistrationAttributes(CancellationToken cancellationToken)
    {
        var result = await Builder
                          .AddSources(
                               """
                               using Specular;
                               using Microsoft.Extensions.DependencyInjection;

                               public interface IService { }

                               [ServiceRegistration(ServiceLifetime.Scoped, typeof(IService))]
                               [ServiceRegistration<IService>(ServiceLifetime.Singleton)]
                               public class Service : IService { }
                               """,
                               ProgramSource
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        await Verify(result);
    }

    [Test]
    public async Task Should_Register_Open_Generic_Types_By_Their_Type_Definitions(CancellationToken cancellationToken)
    {
        var result = await Builder
                          .AddSources(
                               """
                               using Specular;
                               using Microsoft.Extensions.DependencyInjection;

                               public interface IRepository<T> { }

                               [ServiceRegistration(ServiceLifetime.Scoped, typeof(IRepository<>))]
                               public class Repository<T> : IRepository<T> { }
                               """,
                               ProgramSource
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        var services = LoadServices(result);

        var descriptor = services.Where(z => z.ServiceType.Name == "IRepository`1").ShouldHaveSingleItem();
        descriptor.Lifetime.ShouldBe(ServiceLifetime.Scoped);
        descriptor.ImplementationType!.Name.ShouldBe("Repository`1");

        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        scope
           .ServiceProvider.GetRequiredService(descriptor.ServiceType.MakeGenericType(typeof(string)))
           .GetType()
           .GetGenericTypeDefinition()
           .Name.ShouldBe("Repository`1");

        await Verify(new GeneratorTestResultsWithServices(result, services));
    }

    [Test]
    public async Task Should_Register_Generic_ServiceRegistrationAttributes_From_Another_Assembly(CancellationToken cancellationToken)
    {
        var other = await Builder
                         .WithProjectName("GenericLibrary")
                         .AddSources(
                              """
                              using Specular;
                              using Microsoft.Extensions.DependencyInjection;

                              namespace GenericLib;

                              public interface IThing { }
                              public interface IOther { }
                              public interface IThird { }

                              [ServiceRegistration<IThing>(ServiceLifetime.Singleton)]
                              public class PublicThing : IThing { }

                              [ServiceRegistration<IOther>(ServiceLifetime.Scoped)]
                              internal class InternalThing : IOther { }

                              [ServiceRegistration<IThing, IThird>(ServiceLifetime.Transient)]
                              [ServiceRegistration(ServiceLifetime.Transient, typeof(IOther))]
                              public class MixedThing : IThing, IThird, IOther { }
                              """
                          )
                         .AddCacheOptions(GetTempPath())
                         .Build()
                         .GenerateAsync(cancellationToken);

        other.FinalDiagnostics.Where(x => x.Severity >= DiagnosticSeverity.Error).ShouldBeEmpty();

        var result = await Builder
                          .AddCompilationReferences(other)
                          .AddSources(ProgramSource)
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        await Verify(result);
    }

    [Test]
    public async Task Should_Register_Generic_ServiceRegistrationAttributes_From_Another_Assembly_Under_Aot(CancellationToken cancellationToken)
    {
        var other = await Builder
                         .WithProjectName("AotLibrary")
                         .AddSources(
                              """
                              using Specular;
                              using Microsoft.Extensions.DependencyInjection;

                              namespace AotLib;

                              public interface IThing { }
                              public interface IOther { }

                              [ServiceRegistration<IThing>(ServiceLifetime.Singleton)]
                              public class PublicThing : IThing { }

                              [ServiceRegistration<IOther>(ServiceLifetime.Scoped)]
                              internal class InternalThing : IOther { }
                              """
                          )
                         .AddCacheOptions(GetTempPath())
                         .Build()
                         .GenerateAsync(cancellationToken);

        other.FinalDiagnostics.Where(x => x.Severity >= DiagnosticSeverity.Error).ShouldBeEmpty();

        var result = await Builder
                          .AddCompilationReferences(other)
                          .AddSources(ProgramSource)
                          .WithDiagnosticSeverity(DiagnosticSeverity.Info)
                          .AddGlobalOption("build_property.PublishAot", "true")
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        await Verify(result);
    }

    // A generated assembly is always named TestProject, so every build needs a load context of its own.
    private async Task<string[]> SummarizeServices(string source, CancellationToken cancellationToken)
    {
        using var loadContext = new CollectibleTestAssemblyLoadContext();
        var result = await Builder
                          .WithAssemblyLoadContext(loadContext)
                          .AddSources(source, ProgramSource)
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        return [.. LoadServices(result).Select(Summarize)];
    }

    private static IServiceCollection LoadServices(Assembly? assembly)
    {
        assembly.ShouldNotBeNull();
        var program = assembly.GetTypes().FirstOrDefault(z => z.IsClass && z.Name == "Program");
        program.ShouldNotBeNull();

        var method = program.GetMethod("LoadServices", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        method.ShouldNotBeNull();

        return (IServiceCollection)method.Invoke(null, [])!;
    }

    private static (string Implementation, ServiceLifetime Lifetime) Describe(IServiceCollection services, string serviceTypeName)
    {
        var descriptor = services.Where(z => z.ServiceType.Name == serviceTypeName && !z.IsKeyedService).ShouldHaveSingleItem();
        // Non-self registrations are forwarding factories, so the implementation is read from the resolved instance.
        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        return (scope.ServiceProvider.GetRequiredService(descriptor.ServiceType).GetType().Name, descriptor.Lifetime);
    }

    private static string Summarize(ServiceDescriptor descriptor) => $"{descriptor.ServiceType.Name}:{descriptor.Lifetime}";
}
