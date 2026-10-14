using System.Collections.Immutable;
using Microsoft.Extensions.DependencyInjection;

namespace Specular;

/// <summary>
///     Attribute used to define the service registration of a given type
/// </summary>
/// <remarks>
///     Constructor to specify the service type including optional runtime
/// </remarks>
/// <param name="serviceTypes"></param>
/// <param name="lifetime"></param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public abstract class ServiceRegistrationBaseAttribute(ServiceLifetime lifetime, params Type[] serviceTypes) : Attribute
{
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
///     Attribute used to define the service registration of a given type
/// </summary>
/// <remarks>
///     Constructor to specify the service type including optional runtime
/// </remarks>
/// <param name="serviceTypes"></param>
/// <param name="lifetime"></param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class ServiceRegistrationAttribute(ServiceLifetime lifetime, params Type[] serviceTypes) : ServiceRegistrationBaseAttribute(lifetime, serviceTypes);

/// <summary>
///     Attribute used to define the service registration of a given type
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <remarks>
///     Constructor to specify the lifetime
/// </remarks>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class ServiceRegistrationAttribute<TService>(ServiceLifetime lifetime) : ServiceRegistrationBaseAttribute(lifetime, typeof(TService));

/// <summary>
///     Attribute used to define the service registration of a given type
/// </summary>
/// <typeparam name="TService1"></typeparam>
/// <typeparam name="TService2"></typeparam>
/// <remarks>
///     Constructor to specify the lifetime
/// </remarks>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class ServiceRegistrationAttribute<TService1, TService2>(ServiceLifetime lifetime) : ServiceRegistrationBaseAttribute(lifetime, typeof(TService1), typeof(TService2));

/// <summary>
///     Attribute used to define the service registration of a given type
/// </summary>
/// <typeparam name="TService1"></typeparam>
/// <typeparam name="TService2"></typeparam>
/// <typeparam name="TService3"></typeparam>
/// <remarks>
///     Constructor to specify the lifetime
/// </remarks>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3>(ServiceLifetime lifetime) : ServiceRegistrationBaseAttribute(
    lifetime,
    typeof(TService1),
    typeof(TService2),
    typeof(TService3)
    );

/// <summary>
///     Attribute used to define the service registration of a given type
/// </summary>
/// <typeparam name="TService1"></typeparam>
/// <typeparam name="TService2"></typeparam>
/// <typeparam name="TService3"></typeparam>
/// <typeparam name="TService4"></typeparam>
/// <remarks>
///     Constructor to specify the lifetime
/// </remarks>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4>(ServiceLifetime lifetime) : ServiceRegistrationBaseAttribute(
    lifetime,
    typeof(TService1),
    typeof(TService2),
    typeof(TService3),
    typeof(TService4)
);

/// <summary>
///     Attribute used to define the service registration of a given type
/// </summary>
/// <typeparam name="TService1"></typeparam>
/// <typeparam name="TService2"></typeparam>
/// <typeparam name="TService3"></typeparam>
/// <typeparam name="TService4"></typeparam>
/// <typeparam name="TService5"></typeparam>
/// <remarks>
///     Constructor to specify the lifetime
/// </remarks>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>(ServiceLifetime lifetime) : ServiceRegistrationBaseAttribute(
    lifetime,
    typeof(TService1),
    typeof(TService2),
    typeof(TService3),
    typeof(TService4),
    typeof(TService5)
);

/// <summary>
///     Attribute used to define the service registration of a given type
/// </summary>
/// <typeparam name="TService1"></typeparam>
/// <typeparam name="TService2"></typeparam>
/// <typeparam name="TService3"></typeparam>
/// <typeparam name="TService4"></typeparam>
/// <typeparam name="TService5"></typeparam>
/// <typeparam name="TService6"></typeparam>
/// <remarks>
///     Constructor to specify the lifetime
/// </remarks>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6>(ServiceLifetime lifetime) : ServiceRegistrationBaseAttribute(
    lifetime,
    typeof(TService1),
    typeof(TService2),
    typeof(TService3),
    typeof(TService4),
    typeof(TService5),
    typeof(TService6)
);

/// <summary>
///     Attribute used to define the service registration of a given type
/// </summary>
/// <typeparam name="TService1"></typeparam>
/// <typeparam name="TService2"></typeparam>
/// <typeparam name="TService3"></typeparam>
/// <typeparam name="TService4"></typeparam>
/// <typeparam name="TService5"></typeparam>
/// <typeparam name="TService6"></typeparam>
/// <typeparam name="TService7"></typeparam>
/// <remarks>
///     Constructor to specify the lifetime
/// </remarks>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7>(ServiceLifetime lifetime) : ServiceRegistrationBaseAttribute(
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
///     Attribute used to define the service registration of a given type
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
///     Constructor to specify the lifetime
/// </remarks>
/// <param name="lifetime"></param>
[PublicAPI]
public sealed class ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8>(ServiceLifetime lifetime) : ServiceRegistrationBaseAttribute(
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
