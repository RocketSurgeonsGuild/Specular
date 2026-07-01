namespace Indago.Abstractions;

/// <summary>
///     Attribute used to define the compiled type provider for a given assembly
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Assembly)]
public sealed class IndagoHashAttribute(string generatedHash) : Attribute
{
    /// <summary>
    ///     The generated hash to be used for cache busting
    /// </summary>
    public string GeneratedHash { get; } = generatedHash;
}
