using Microsoft.Extensions.DependencyInjection;
using Specular.Samples.Catalog;
using Specular.Samples.Diagnostics;
using Specular.Samples.Notifications;

// Specular.Samples.Console — proves compile-time scanning of the three sample libraries end-to-end.
//
// The host performs Specular's COMPILE-TIME scanning (no runtime reflection): each provider.Scan call
// below is resolved by the source generator at build time into a concrete ISpecularProvider. We read
// the produced ServiceDescriptors back, compare to a hard-coded expected set, print the result, and
// exit nonzero on mismatch so the build pipeline can treat the exit code as pass/fail.

var provider = SpecularProvider.Instance;


var services = new ServiceCollection();

// 1. Catalog — interface-matching, Singleton.
provider.Scan(
    services,
    z => z.FromAssemblyOf<IProductService>()
          .AddClasses(f => f.InExactNamespaceOf<IProductService>())
          .AsMatchingInterface()
          .WithSingletonLifetime()
);

// 2. Notifications — attribute-based ([ServiceRegistration]), registered AsSelf, Transient.
provider.Scan(
    services,
    z => z.FromAssemblyOf<NotificationDispatcher>()
          .AddClasses(f => f.WithAttribute<ServiceRegistrationAttribute>())
          .AsSelf()
          .WithTransientLifetime()
);

// 3. Diagnostics — interface-matching, Scoped, with the opt-out type filtered out.
provider.Scan(
    services,
    z => z.FromAssemblyOf<IHealthCheck>()
          .AddClasses(f => f.WithoutAttribute<ExcludeFromSampleScanAttribute>())
          .AsMatchingInterface()
          .WithScopedLifetime()
);

// Also exercise ISpecularProvider.EntryAssembly type discovery (US2 AC3): the entry assembly defines
// no discoverable sample services, so its discovered service set MUST be empty.
var entryTypes = provider
                .GetTypes(z => z.EntryAssembly().GetTypes(f => f.AssignableTo<IProductService>()))
                .ToList();

var actual = services
            .Select(static d => $"{Name(d.ServiceType)}=>{Name(d.ImplementationType)} ({d.Lifetime})")
            .OrderBy(static s => s, StringComparer.Ordinal)
            .ToList();

// Expected discovered set (the inspectable contract — see samples/libraries/README.md).
// Catalog (4, Singleton) + Notifications (4, Transient, AsSelf) + Diagnostics (2, Scoped); the
// opt-out type IExperimentalProbe/ExperimentalProbe MUST be absent.
var expected = sourceArray.OrderBy(static s => s, StringComparer.Ordinal)
    .ToList();

Console.WriteLine($"Specular discovered {actual.Count} service(s) across 3 libraries:");
foreach (var line in actual)
{
    Console.WriteLine($"  - {line}");
}

Console.WriteLine($"Entry-assembly discovered service count: {entryTypes.Count}");

var missing = expected.Except(actual, StringComparer.Ordinal).ToList();
var unexpected = actual.Except(expected, StringComparer.Ordinal).ToList();

if (missing.Count == 0 && unexpected.Count == 0 && entryTypes.Count == 0)
{
    Console.WriteLine("MATCH: discovered service set equals the expected set; opt-out type absent.");
    return 0;
}

Console.Error.WriteLine("MISMATCH: discovered service set does not equal the expected set.");
foreach (var m in missing)
{
    Console.Error.WriteLine($"  missing (expected, not discovered): {m}");
}

foreach (var u in unexpected)
{
    Console.Error.WriteLine($"  unexpected (discovered, not expected): {u}");
}

if (entryTypes.Count != 0)
{
    Console.Error.WriteLine($"  entry assembly unexpectedly discovered {entryTypes.Count} service(s).");
}

return 1;

static string Name(Type? type) => type?.Name ?? "(factory)";

internal partial class Program
{
    private static readonly string[] sourceArray =
[
    // Catalog — interface-matching, Singleton.
    "IProductService=>ProductService (Singleton)",
    "ICategoryService=>CategoryService (Singleton)",
    "IInventoryService=>InventoryService (Singleton)",
    "IPricingService=>PricingService (Singleton)",
    // Notifications — attribute-based, AsSelf, Transient.
    "EmailNotifier=>EmailNotifier (Transient)",
    "SmsNotifier=>SmsNotifier (Transient)",
    "PushNotifier=>PushNotifier (Transient)",
    "NotificationDispatcher=>NotificationDispatcher (Transient)",
    // Diagnostics — interface-matching, Scoped (opt-out ExperimentalProbe excluded).
    "IHealthCheck=>HealthCheck (Scoped)",
    "IMetricsCollector=>MetricsCollector (Scoped)",
];
}
