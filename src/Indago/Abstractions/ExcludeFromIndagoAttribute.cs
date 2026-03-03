namespace Indago.Abstractions;

/// <summary>
/// Exclude the given assembly from compiled type provider resolution.
/// </summary>
/// <remarks>This assembly will still have access to compiled types, but nothing will be resolved internally.</remarks>
[PublicAPI]
[AttributeUsage(AttributeTargets.Assembly)]
public class ExcludeFromIndagoAttribute() : Attribute;
