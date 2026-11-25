namespace Specular.Samples.HandlerRegistry;

/// <summary>The contract the generator looks for; it lists every concrete class implementing it.</summary>
public interface IHandler
{
    /// <summary>Handles a message.</summary>
    /// <param name="message">The message.</param>
    /// <returns>What the handler did.</returns>
    string Handle(string message);
}

/// <summary>Listed: a public class implementing <see cref="IHandler" />.</summary>
public sealed class OrderPlacedHandler : IHandler
{
    /// <inheritdoc />
    public string Handle(string message) => $"order placed: {message}";
}

/// <summary>Listed: a public class implementing <see cref="IHandler" />.</summary>
public sealed class PaymentReceivedHandler : IHandler
{
    /// <inheritdoc />
    public string Handle(string message) => $"payment received: {message}";
}

/// <summary>Not listed: abstract (the generator filters abstract classes out), though its subclasses are.</summary>
public abstract class HandlerBase : IHandler
{
    /// <inheritdoc />
    public abstract string Handle(string message);
}

/// <summary>Listed: implements <see cref="IHandler" /> through its base class.</summary>
public sealed class ShipmentDispatchedHandler : HandlerBase
{
    /// <inheritdoc />
    public override string Handle(string message) => $"shipment dispatched: {message}";
}

/// <summary>
///     Matched by the scan but not listed in the app: it is internal, so the app cannot name it (<c>ScannedType.IsAccessible</c> is false
///     there). The generated file mentions it in a comment.
/// </summary>
internal sealed class AuditHandler : IHandler
{
    /// <inheritdoc />
    public string Handle(string message) => $"audited: {message}";
}
