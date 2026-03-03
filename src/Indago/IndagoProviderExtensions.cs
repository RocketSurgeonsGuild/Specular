using System.Reflection;
using Indago.Abstractions;

namespace Indago;

/// <summary>
/// Assembly provider extensions
/// </summary>
public static class IndagoProviderExtensions
{
    /// <summary>
    /// Get the assembly provider for the given assembly
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static IIndagoProvider GetIndagoProvider(this Assembly? assembly) =>
        assembly?.GetCustomAttribute<IndagoProviderAttribute>()?.IIndagoProvider
     ?? throw new InvalidOperationException($"No IndagoProviderAttribute found on the assembly {assembly?.GetName().Name}");
}
