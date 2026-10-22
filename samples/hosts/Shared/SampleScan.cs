using Specular.Samples.Catalog;
using Specular.Samples.Diagnostics;
using Specular.Samples.Notifications;

namespace Specular.Samples;

// Shared by every demonstration host (linked into each project, so each host's own generator resolves the Scan calls below).
//
// Every Scan/GetTypes call is resolved by the source generator at build time (no runtime reflection). The hosts register the
// three sample libraries through Scan, compare what was registered with the expected set (the inspectable contract in
// samples/libraries/README.md), and print one SPECULAR-SAMPLE line that CI looks for (samples/smoke.mjs).

/// <summary>Scans the sample libraries and checks the registrations against the expected set.</summary>
internal static class SampleScan
{
    /// <summary>The prefix of the line every host prints with the result; CI matches on it.</summary>
    public const string ResultPrefix = "SPECULAR-SAMPLE:";

    /// <summary>Set to <c>1</c> to make an app host print the result and exit (with 0 on a match) before it starts its UI.</summary>
    public const string SmokeVariable = "SPECULAR_SAMPLE_SMOKE";

    // Catalog (4, Singleton) + Notifications (4, Transient, AsSelf) + Diagnostics (2, Scoped); the opt-out
    // IExperimentalProbe/ExperimentalProbe must be absent.
    private static readonly string[] ExpectedServices =
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

    /// <summary>
    ///     Registers the sample libraries' services into <paramref name="services" /> and reports whether they match the expected set.
    /// </summary>
    public static SampleScanResult AddSampleServices(this IServiceCollection services, ISpecularProvider provider)
    {
        // Scan into an empty collection so the comparison only sees what Specular registered, not the host's own services.
        var scanned = new ServiceCollection();

        // 1. Catalog — interface-matching, Singleton.
        provider.Scan(
            scanned,
            z => z.FromAssemblyOf<IProductService>()
                  .AddClasses(f => f.InExactNamespaceOf<IProductService>())
                  .AsMatchingInterface()
                  .WithSingletonLifetime()
        );

        // 2. Notifications — attribute-based ([ServiceRegistration]), registered AsSelf, Transient.
        provider.Scan(
            scanned,
            z => z.FromAssemblyOf<NotificationDispatcher>()
                  .AddClasses(f => f.WithAttribute<ServiceRegistrationAttribute>())
                  .AsSelf()
                  .WithTransientLifetime()
        );

        // 3. Diagnostics — interface-matching, Scoped, with the opt-out type filtered out.
        provider.Scan(
            scanned,
            z => z.FromAssemblyOf<IHealthCheck>()
                  .AddClasses(f => f.WithoutAttribute<ExcludeFromSampleScanAttribute>())
                  .AsMatchingInterface()
                  .WithScopedLifetime()
        );

        // ISpecularProvider.EntryAssembly type discovery: the host itself defines no sample services, so this must be empty.
        var entryAssemblyTypes = provider
                                .GetTypes(z => z.EntryAssembly().GetTypes(f => f.AssignableTo<IProductService>()))
                                .Count();

        foreach (var descriptor in scanned)
        {
            services.Add(descriptor);
        }

        var discovered = scanned
                        .Select(static d => $"{Name(d.ServiceType)}=>{Name(d.ImplementationType)} ({d.Lifetime})")
                        .Order(StringComparer.Ordinal)
                        .ToList();

        return new(
            discovered,
            ExpectedServices.Except(discovered, StringComparer.Ordinal).Order(StringComparer.Ordinal).ToList(),
            discovered.Except(ExpectedServices, StringComparer.Ordinal).ToList(),
            entryAssemblyTypes
        );
    }

    /// <summary>
    ///     When <see cref="SmokeVariable" /> is set, scans, prints the result and returns the process exit code; otherwise <see langword="null" />.
    /// </summary>
    public static int? RunSmokeTest(ISpecularProvider provider)
    {
        if (Environment.GetEnvironmentVariable(SmokeVariable) != "1") return null;

        var result = new ServiceCollection().AddSampleServices(provider);
        result.WriteTo(Console.Out, Console.Error);
        return result.IsMatch ? 0 : 1;
    }

    private static string Name(Type? type) => type?.Name ?? "(factory)";
}

/// <summary>What <see cref="SampleScan.AddSampleServices" /> registered, compared with the expected set.</summary>
internal sealed record SampleScanResult(
    IReadOnlyList<string> Discovered,
    IReadOnlyList<string> Missing,
    IReadOnlyList<string> Unexpected,
    int EntryAssemblyTypes
)
{
    /// <summary>Whether the registrations equal the expected set and the entry assembly contributed nothing.</summary>
    public bool IsMatch => Missing.Count == 0 && Unexpected.Count == 0 && EntryAssemblyTypes == 0;

    /// <summary>The single line CI looks for.</summary>
    public string Summary =>
        IsMatch
            ? $"{SampleScan.ResultPrefix} MATCH {Discovered.Count} services; opt-out type absent."
            : $"{SampleScan.ResultPrefix} MISMATCH {Missing.Count} missing, {Unexpected.Count} unexpected, {EntryAssemblyTypes} entry-assembly type(s).";

    /// <summary>Writes the discovered services and the summary; on a mismatch, the differences go to <paramref name="error" />.</summary>
    public void WriteTo(TextWriter output, TextWriter error)
    {
        output.WriteLine($"Specular discovered {Discovered.Count} service(s) across 3 libraries:");
        foreach (var line in Discovered)
        {
            output.WriteLine($"  - {line}");
        }

        if (IsMatch)
        {
            output.WriteLine(Summary);
            return;
        }

        error.WriteLine(Summary);
        foreach (var line in Missing)
        {
            error.WriteLine($"  missing (expected, not discovered): {line}");
        }

        foreach (var line in Unexpected)
        {
            error.WriteLine($"  unexpected (discovered, not expected): {line}");
        }

        if (EntryAssemblyTypes != 0)
        {
            error.WriteLine($"  entry assembly unexpectedly discovered {EntryAssemblyTypes} service(s).");
        }
    }
}
