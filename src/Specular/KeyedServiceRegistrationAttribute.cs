using System.Collections.Immutable;
using Microsoft.Extensions.DependencyInjection;

namespace Specular;

/// <summary>
///     Attribute used to define the keyed service registration of a given type. Behaves the same as
///     <see cref="ServiceRegistrationAttribute" /> except the resulting registration is a
///     <see href="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services">keyed service</see>.
/// </summary>
/// <remarks>
///     Constructor to specify the key, service type including optional runtime
/// </remarks>
/// <param name="key"></param>
/// <param name="lifetime"></param>
/// <param name="serviceTypes"></param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public abstract class KeyedServiceRegistrationBaseAttribute(object? key, ServiceLifetime lifetime, params Type[] serviceTypes) : Attribute
{
    /// <summary>
    ///     The key the service is registered against
    /// </summary>
    public object? Key { get; } = key;

    /// <summary>
    ///     The service type
    /// </summary>
    public ImmutableArray<Type> ServiceTypes { get; } = [.. serviceTypes];

    /// <summary>
    ///     The lifetime
    /// </summary>
    public ServiceLifetime Lifetime { get; } = lifetime;
}


/// <summary>
///     Attribute used to define the keyed service registration of a given type. Behaves the same as
///     <see cref="ServiceRegistrationAttribute" /> except the resulting registration is a
///     <see href="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services">keyed service</see>.
/// </summary>
/// <remarks>
///     Constructor to specify the key, service type including optional runtime
/// </remarks>
/// <param name="key"></param>
/// <param name="lifetime"></param>
/// <param name="serviceTypes"></param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class KeyedServiceRegistrationAttribute(object? key, ServiceLifetime lifetime, params Type[] serviceTypes) : KeyedServiceRegistrationBaseAttribute(key, lifetime, serviceTypes);

/// <summary>
///     Attribute used to define the keyed service registration of a given type
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <remarks>
///     Constructor to specify the key and lifetime
/// </remarks>
/// <param name="key"></param>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class KeyedServiceRegistrationAttribute<TService>(object? key, ServiceLifetime lifetime) : KeyedServiceRegistrationBaseAttribute(key, lifetime, typeof(TService));

/// <summary>
///     Attribute used to define the keyed service registration of a given type
/// </summary>
/// <typeparam name="TService1"></typeparam>
/// <typeparam name="TService2"></typeparam>
/// <remarks>
///     Constructor to specify the key and lifetime
/// </remarks>
/// <param name="key"></param>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class KeyedServiceRegistrationAttribute<TService1, TService2>(object? key, ServiceLifetime lifetime) : KeyedServiceRegistrationBaseAttribute(
    key,
    lifetime,
    typeof(TService1),
    typeof(TService2)
);

/// <summary>
///     Attribute used to define the keyed service registration of a given type
/// </summary>
/// <typeparam name="TService1"></typeparam>
/// <typeparam name="TService2"></typeparam>
/// <typeparam name="TService3"></typeparam>
/// <remarks>
///     Constructor to specify the key and lifetime
/// </remarks>
/// <param name="key"></param>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3>(object? key, ServiceLifetime lifetime) : KeyedServiceRegistrationBaseAttribute(
    key,
    lifetime,
    typeof(TService1),
    typeof(TService2),
    typeof(TService3)
);

/// <summary>
///     Attribute used to define the keyed service registration of a given type
/// </summary>
/// <typeparam name="TService1"></typeparam>
/// <typeparam name="TService2"></typeparam>
/// <typeparam name="TService3"></typeparam>
/// <typeparam name="TService4"></typeparam>
/// <remarks>
///     Constructor to specify the key and lifetime
/// </remarks>
/// <param name="key"></param>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4>(object? key, ServiceLifetime lifetime) : KeyedServiceRegistrationBaseAttribute(
    key,
    lifetime,
    typeof(TService1),
    typeof(TService2),
    typeof(TService3),
    typeof(TService4)
);

/// <summary>
///     Attribute used to define the keyed service registration of a given type
/// </summary>
/// <typeparam name="TService1"></typeparam>
/// <typeparam name="TService2"></typeparam>
/// <typeparam name="TService3"></typeparam>
/// <typeparam name="TService4"></typeparam>
/// <typeparam name="TService5"></typeparam>
/// <remarks>
///     Constructor to specify the key and lifetime
/// </remarks>
/// <param name="key"></param>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>(object? key, ServiceLifetime lifetime) : KeyedServiceRegistrationBaseAttribute(
    key,
    lifetime,
    typeof(TService1),
    typeof(TService2),
    typeof(TService3),
    typeof(TService4),
    typeof(TService5)
);

/// <summary>
///     Attribute used to define the keyed service registration of a given type
/// </summary>
/// <typeparam name="TService1"></typeparam>
/// <typeparam name="TService2"></typeparam>
/// <typeparam name="TService3"></typeparam>
/// <typeparam name="TService4"></typeparam>
/// <typeparam name="TService5"></typeparam>
/// <typeparam name="TService6"></typeparam>
/// <remarks>
///     Constructor to specify the key and lifetime
/// </remarks>
/// <param name="key"></param>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6>(object? key, ServiceLifetime lifetime) : KeyedServiceRegistrationBaseAttribute(
    key,
    lifetime,
    typeof(TService1),
    typeof(TService2),
    typeof(TService3),
    typeof(TService4),
    typeof(TService5),
    typeof(TService6)
);

/// <summary>
///     Attribute used to define the keyed service registration of a given type
/// </summary>
/// <typeparam name="TService1"></typeparam>
/// <typeparam name="TService2"></typeparam>
/// <typeparam name="TService3"></typeparam>
/// <typeparam name="TService4"></typeparam>
/// <typeparam name="TService5"></typeparam>
/// <typeparam name="TService6"></typeparam>
/// <typeparam name="TService7"></typeparam>
/// <remarks>
///     Constructor to specify the key and lifetime
/// </remarks>
/// <param name="key"></param>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7>(object? key, ServiceLifetime lifetime) : KeyedServiceRegistrationBaseAttribute(
    key,
    lifetime,
    typeof(TService1),
    typeof(TService2),
    typeof(TService3),
    typeof(TService4),
    typeof(TService5),
    typeof(TService6),
    typeof(TService7)
);

/// <summary>
///     Attribute used to define the keyed service registration of a given type
/// </summary>
/// <typeparam name="TService1"></typeparam>
/// <typeparam name="TService2"></typeparam>
/// <typeparam name="TService3"></typeparam>
/// <typeparam name="TService4"></typeparam>
/// <typeparam name="TService5"></typeparam>
/// <typeparam name="TService6"></typeparam>
/// <typeparam name="TService7"></typeparam>
/// <typeparam name="TService8"></typeparam>
/// <remarks>
///     Constructor to specify the key and lifetime
/// </remarks>
/// <param name="key"></param>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8>(object? key, ServiceLifetime lifetime) : KeyedServiceRegistrationBaseAttribute(
    key,
    lifetime,
    typeof(TService1),
    typeof(TService2),
    typeof(TService3),
    typeof(TService4),
    typeof(TService5),
    typeof(TService6),
    typeof(TService7),
    typeof(TService8)
);
