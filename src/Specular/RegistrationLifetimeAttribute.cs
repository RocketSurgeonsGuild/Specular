using Microsoft.Extensions.DependencyInjection;

namespace Specular;

/// <summary>
///     Defines the lifetime that should be used service registrations created using the <see cref="ISpecularProvider" />
/// </summary>
/// <remarks>
///     Constructor to specify the lifetime
/// </remarks>
/// <param name="lifetime"></param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class RegistrationLifetimeAttribute(ServiceLifetime lifetime) : Attribute
{

    /// <summary>
    ///     The lifetime
    /// </summary>
    public ServiceLifetime Lifetime { get; } = lifetime;
}
