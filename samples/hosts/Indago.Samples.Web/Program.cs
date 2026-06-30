using Indago.Samples.Catalog;
using Indago.Samples.Diagnostics;
using Indago.Samples.Notifications;

// Indago.Samples.Web — proves Indago's COMPILE-TIME scanning in a minimal-API host (US4).
//
// At startup the host scans the three sample libraries (no runtime reflection — each Scan call is
// resolved by the source generator at build time), asserts the discovered service set equals a
// hard-coded expected set, and FAILS FAST (nonzero exit) on mismatch before the server starts. A
// demonstration endpoint (`/services`) reports the discovered registrations. Native-AOT publishable
// zero-warning (the AOT guardrail is the WebAotPublishModule pipeline step).

var builder = WebApplication.CreateSlimBuilder(args);

var provider = IIndagoProvider.EntryAssembly;

// 1. Catalog — interface-matching, Singleton.
provider.Scan(
    builder.Services,
    z => z.FromAssemblyOf<IProductService>()
          .AddClasses(f => f.InExactNamespaceOf<IProductService>())
          .AsMatchingInterface()
          .WithSingletonLifetime()
);

// 2. Notifications — attribute-based ([ServiceRegistration]), registered AsSelf, Transient.
provider.Scan(
    builder.Services,
    z => z.FromAssemblyOf<NotificationDispatcher>()
          .AddClasses(f => f.WithAttribute<ServiceRegistrationAttribute>())
          .AsSelf()
          .WithTransientLifetime()
);

// 3. Diagnostics — interface-matching, Scoped, with the opt-out type filtered out.
provider.Scan(
    builder.Services,
    z => z.FromAssemblyOf<IHealthCheck>()
          .AddClasses(f => f.WithoutAttribute<ExcludeFromSampleScanAttribute>())
          .AsMatchingInterface()
          .WithScopedLifetime()
);

var discovered = builder.Services
                       .Where(static d => DescribesSample(d.ServiceType))
                       .Select(static d => $"{Name(d.ServiceType)}=>{Name(d.ImplementationType)} ({d.Lifetime})")
                       .OrderBy(static s => s, StringComparer.Ordinal)
                       .ToList();

var expected = ExpectedServices.OrderBy(static s => s, StringComparer.Ordinal).ToList();

var missing = expected.Except(discovered, StringComparer.Ordinal).ToList();
var unexpected = discovered.Except(expected, StringComparer.Ordinal).ToList();

if (missing.Count != 0 || unexpected.Count != 0)
{
    // Fail fast BEFORE serving so a CI smoke test / health probe sees a non-zero exit on mismatch.
    Console.Error.WriteLine("MISMATCH: Indago discovered service set does not equal the expected set.");
    foreach (var m in missing)
    {
        Console.Error.WriteLine($"  missing (expected, not discovered): {m}");
    }

    foreach (var u in unexpected)
    {
        Console.Error.WriteLine($"  unexpected (discovered, not expected): {u}");
    }

    return 1;
}

var app = builder.Build();

// Demonstration endpoint: report the discovered registrations (plain text keeps this AOT/trim-clean
// without a source-generated JSON context).
app.MapGet("/services", () => string.Join('\n', discovered));
app.MapGet("/", () => $"Indago.Samples.Web — discovered {discovered.Count} services. See /services.");

Console.WriteLine($"Indago.Samples.Web: discovered {discovered.Count} services; opt-out type absent. Serving.");
app.Run();

return 0;

static string Name(Type? type) => type?.Name ?? "(factory)";

// Only count the sample libraries' registrations, ignoring framework/host services.
static bool DescribesSample(Type serviceType) =>
    serviceType.Namespace is { } ns && ns.StartsWith("Indago.Samples.", StringComparison.Ordinal);

internal partial class Program
{
    // Expected discovered set (the inspectable contract — see samples/libraries/README.md):
    // Catalog (4, Singleton) + Notifications (4, Transient, AsSelf) + Diagnostics (2, Scoped); the
    // opt-out ExperimentalProbe MUST be absent.
    private static readonly string[] ExpectedServices =
    [
        "IProductService=>ProductService (Singleton)",
        "ICategoryService=>CategoryService (Singleton)",
        "IInventoryService=>InventoryService (Singleton)",
        "IPricingService=>PricingService (Singleton)",
        "EmailNotifier=>EmailNotifier (Transient)",
        "SmsNotifier=>SmsNotifier (Transient)",
        "PushNotifier=>PushNotifier (Transient)",
        "NotificationDispatcher=>NotificationDispatcher (Transient)",
        "IHealthCheck=>HealthCheck (Scoped)",
        "IMetricsCollector=>MetricsCollector (Scoped)",
    ];
}
