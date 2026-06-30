using Microsoft.Extensions.DependencyInjection;

namespace Indago.Samples.Notifications;

// Attribute-based registration style: each concrete service carries [ServiceRegistration].
// Hosts discover them with a call-site selector that filters on the attribute and registers AsSelf.
// (Indago's shipped AddIndagoServiceRegistrations helper always registers AsSelf/Singleton; to keep
// the demonstrated lifetime explicit and host-controlled, hosts set the lifetime on the Scan call.)

/// <summary>Sends e-mail notifications. Discovered by the ServiceRegistration attribute.</summary>
[ServiceRegistration(ServiceLifetime.Transient)]
public sealed class EmailNotifier
{
    /// <summary>The channel name.</summary>
    public static string Channel => "email";
}

/// <summary>Sends SMS notifications. Discovered by the ServiceRegistration attribute.</summary>
[ServiceRegistration(ServiceLifetime.Transient)]
public sealed class SmsNotifier
{
    /// <summary>The channel name.</summary>
    public static string Channel => "sms";
}

/// <summary>Sends push notifications. Discovered by the ServiceRegistration attribute.</summary>
[ServiceRegistration(ServiceLifetime.Transient)]
public sealed class PushNotifier
{
    /// <summary>The channel name.</summary>
    public static string Channel => "push";
}

/// <summary>Fans a message out across channels. Discovered by the ServiceRegistration attribute.</summary>
[ServiceRegistration(ServiceLifetime.Transient)]
public sealed class NotificationDispatcher
{
    /// <summary>Describes the dispatcher.</summary>
    public static string Describe() => "dispatches email/sms/push";
}
