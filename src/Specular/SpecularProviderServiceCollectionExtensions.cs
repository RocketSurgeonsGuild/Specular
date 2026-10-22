using Microsoft.Extensions.DependencyInjection;

namespace Specular;

/// <summary>
///     Extension methods for the service collection using the compiled type provider
/// </summary>
public static class SpecularProviderServiceCollectionExtensions
{
    /// <summary>
    ///     Adds all the services with the <see cref="ServiceRegistrationAttribute" /> or
    ///     <see cref="KeyedServiceRegistrationAttribute" /> to the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static IServiceCollection AddSpecularServiceRegistrations(this IServiceCollection services, ISpecularProvider provider)
    {
        // This is implied to ignore abstract and static classes.
        return provider.Scan(
            services,
            z => z
                .FromAssemblies()
                .AddClasses(
                     f => f
                        .WithAnyAttribute(
                             typeof(ServiceRegistrationAttribute),
                             typeof(ServiceRegistrationAttribute<,>),
                             typeof(ServiceRegistrationAttribute<,,>),
                             typeof(ServiceRegistrationAttribute<,,>),
                             typeof(ServiceRegistrationAttribute<,,,>),
                             typeof(ServiceRegistrationAttribute<,,,,>),
                             typeof(ServiceRegistrationAttribute<,,,,,>),
                             typeof(ServiceRegistrationAttribute<,,,,,,>),
                             typeof(ServiceRegistrationAttribute<,,,,,,,>),
                             typeof(KeyedServiceRegistrationAttribute),
                             typeof(KeyedServiceRegistrationAttribute<>),
                             typeof(KeyedServiceRegistrationAttribute<,>),
                             typeof(KeyedServiceRegistrationAttribute<,,>),
                             typeof(KeyedServiceRegistrationAttribute<,,,>),
                             typeof(KeyedServiceRegistrationAttribute<,,,,>),
                             typeof(KeyedServiceRegistrationAttribute<,,,,,>),
                             typeof(KeyedServiceRegistrationAttribute<,,,,,,>),
                             typeof(KeyedServiceRegistrationAttribute<,,,,,,,>)
                         )
                 )
                 .AsSelf()
                 .WithSingletonLifetime()
        );
    }
}
