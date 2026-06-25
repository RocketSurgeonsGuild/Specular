using Microsoft.Extensions.DependencyInjection;

namespace Indago;

/// <summary>
///     Extension methods for the service collection using the compiled type provider
/// </summary>
public static class IndagoProviderServiceCollectionExtensions
{
    /// <summary>
    ///     Adds all the services with the <see cref="ServiceRegistrationAttribute" /> to the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static IServiceCollection AddIndagoServiceRegistrations(this IServiceCollection services, IIndagoProvider provider)
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
                             typeof(ServiceRegistrationAttribute<,,,,,,,>)
                         )
                 )
                 .AsSelf()
                 .WithSingletonLifetime()
        );
    }
}
