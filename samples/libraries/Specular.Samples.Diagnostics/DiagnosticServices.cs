namespace Specular.Samples.Diagnostics;

/// <summary>
///     Sample type-level opt-out marker. Specular's built-in <c>ExcludeFromSpecularAttribute</c> targets
///     assemblies only, so per-type opt-out is expressed at the host call site by filtering this
///     marker out: <c>.AddClasses(f =&gt; f.WithoutAttribute&lt;ExcludeFromSampleScanAttribute&gt;())</c>.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ExcludeFromSampleScanAttribute : Attribute;

/// <summary>Reports component health. Discovered by interface-matching (Scoped).</summary>
public interface IHealthCheck
{
    string Status();
}

/// <summary>Collects runtime metrics. Discovered by interface-matching (Transient).</summary>
public interface IMetricsCollector
{
    string Snapshot();
}

/// <inheritdoc />
public sealed class HealthCheck : IHealthCheck
{
    /// <inheritdoc />
    public string Status() => "healthy";
}

/// <inheritdoc />
public sealed class MetricsCollector : IMetricsCollector
{
    /// <inheritdoc />
    public string Snapshot() => "metrics";
}

/// <summary>
///     Experimental probe that should NOT be discovered. It implements an interface (so interface
///     matching would otherwise pick it up) but carries <see cref="ExcludeFromSampleScanAttribute" />,
///     so hosts filter it out — proving the opt-out type is absent from the registered set.
/// </summary>
public interface IExperimentalProbe
{
    string Name { get; }
}

/// <inheritdoc cref="IExperimentalProbe" />
[ExcludeFromSampleScan]
public sealed class ExperimentalProbe : IExperimentalProbe
{
    /// <inheritdoc />
    public string Name => "experimental";
}
