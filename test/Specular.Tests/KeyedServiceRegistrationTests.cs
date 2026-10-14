using Rocket.Surgery.Extensions.Testing.SourceGenerators;

namespace Specular.Tests;

[Timeout(ModuleInitializer.TestTimeout)]
public class KeyedServiceRegistrationTests : GeneratorTest
{
    private GeneratorTestContextBuilder WithSpecularServiceRegistrationsShim() =>
        Builder.AddSource(
            "SpecularProviderServiceCollectionExtensions.cs",
            """
            using Specular;
            using Specular.Abstractions;

            namespace Microsoft.Extensions.DependencyInjection;

            public static class SpecularProviderServiceCollectionExtensions
            {
                public static IServiceCollection AddSpecularServiceRegistrations(this IServiceCollection services, ISpecularProvider provider)
                {
                    return provider.Scan(
                        services,
                        z => z
                            .FromAssemblies()
                            .AddClasses(
                                 f => f
                                    .WithAnyAttribute(
                                         typeof(ServiceRegistrationAttribute),
                                         typeof(KeyedServiceRegistrationAttribute),
                                         typeof(KeyedServiceRegistrationAttribute<>),
                                         typeof(KeyedServiceRegistrationAttribute<,>)
                                     )
                             )
                             .AsSelf()
                             .WithSingletonLifetime()
                    );
                }
            }
            """
        );

    [Test]
    public async Task Should_Support_KeyedServiceRegistrationAttributes(CancellationToken cancellationToken)
    {
        var result = await WithSpecularServiceRegistrationsShim()
                          .AddSources(
                               """
                               using System;
                               using Specular;
                               using Specular.Abstractions;
                               using Microsoft.Extensions.DependencyInjection;

                               public interface IService { }
                               public interface IServiceB { }

                               [KeyedServiceRegistration("service-b-key", ServiceLifetime.Scoped, typeof(IServiceB))]
                               public class Service : IService, IServiceB { }

                               public static class Nested
                               {
                                   [KeyedServiceRegistration("service-a-key", ServiceLifetime.Transient)]
                                   public class ServiceA : IService { }
                               }

                               [KeyedServiceRegistration("service-c-key", ServiceLifetime.Singleton)]
                               public class ServiceC : IService, IServiceB { }
                               """
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        await Verify(result);
    }

    [Test]
    public async Task Should_Support_Keyed_Generic_Sugar_Attribute(CancellationToken cancellationToken)
    {
        var result = await WithSpecularServiceRegistrationsShim()
                          .AddSources(
                               """
                               using Specular;
                               using Specular.Abstractions;
                               using Microsoft.Extensions.DependencyInjection;

                               public interface IService { }

                               [KeyedServiceRegistration<IService>("service-key", ServiceLifetime.Scoped)]
                               public class Service : IService { }
                               """
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        await Verify(result);
    }

    [Test]
    public async Task Should_Allow_Distinct_Keys_For_The_Same_Service_Type(CancellationToken cancellationToken)
    {
        var result = await WithSpecularServiceRegistrationsShim()
                          .AddSources(
                               """
                               using Specular;
                               using Specular.Abstractions;
                               using Microsoft.Extensions.DependencyInjection;

                               public interface IService { }

                               [KeyedServiceRegistration("key-one", ServiceLifetime.Singleton, typeof(IService))]
                               [KeyedServiceRegistration("key-two", ServiceLifetime.Singleton, typeof(IService))]
                               public class Service : IService { }
                               """
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        await Verify(result);
    }

    [Test]
    public async Task Should_Report_Diagnostic_For_Duplicate_KeyedServiceRegistrationAttributes(CancellationToken cancellationToken)
    {
        var result = await WithSpecularServiceRegistrationsShim()
                          .AddSources(
                               """
                               using Specular;
                               using Specular.Abstractions;
                               using Microsoft.Extensions.DependencyInjection;

                               public interface IService { }

                               [KeyedServiceRegistration("same-key", ServiceLifetime.Scoped, typeof(IService))]
                               [KeyedServiceRegistration("same-key", ServiceLifetime.Singleton, typeof(IService))]
                               public class Service : IService { }
                               """
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        await Verify(result);
    }

    [Test]
    public async Task Should_Support_RegistrationLifetime_Override_For_Keyed_Registrations(CancellationToken cancellationToken)
    {
        var result = await WithSpecularServiceRegistrationsShim()
                          .AddSources(
                               """
                               using Specular;
                               using Specular.Abstractions;
                               using Microsoft.Extensions.DependencyInjection;

                               [RegistrationLifetime(ServiceLifetime.Scoped)]
                               public interface IService { }

                               [KeyedServiceRegistration("service-key", ServiceLifetime.Singleton, typeof(IService))]
                               public class Service : IService { }
                               """
                           )
                          .AddCacheOptions(GetTempPath())
                          .Build()
                          .GenerateAsync(cancellationToken);

        await Verify(result);
    }
}
