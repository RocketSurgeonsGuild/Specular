namespace Indago.Abstractions;

/// <summary>
///     Attribute used to define the compiled type provider for a given assembly
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Assembly)]
public sealed class IndagoProviderAttribute(
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)]
    Type type,
    string generatedHash) : Attribute
{
    /// <summary>
    ///     The generated hash to be used for cache busting
    /// </summary>
    public string GeneratedHash { get; } = generatedHash;

    /// <summary>
    ///     The assembly provider
    /// </summary>
    public IIndagoProvider IIndagoProvider => _IndagoProvider.Value;

    /// <summary>
    ///     The type
    /// </summary>
    [property: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)]
    public Type Type => type;

    // ReSharper disable once NullableWarningSuppressionIsUsed
    private readonly Lazy<IIndagoProvider> _IndagoProvider = new(() => (IIndagoProvider)Activator.CreateInstance(type)!);
}
