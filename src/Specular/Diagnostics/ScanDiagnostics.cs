using System.Collections.Immutable;
using Microsoft.Extensions.DependencyInjection;

namespace Specular.Diagnostics;

/// <summary>Contains the compile-time results for all Specular scans in an assembly.</summary>
[PublicAPI]
public sealed record ScanResults(ImmutableList<AssemblyScanReport> AssemblyScanReports, ImmutableList<TypeScanReport> TypeScanReports, ImmutableList<ServiceDescriptorScanReport> ServiceDescriptorScanReports);

/// <summary>Contains the result of an assembly scan expression.</summary>
[PublicAPI]
public sealed record AssemblyScanReport(string SourceAssemblyName, string SourceExpression, ImmutableList<AssemblyScanReportEntry> Entries);

/// <summary>Identifies an assembly selected by an assembly scan expression.</summary>
[PublicAPI]
public sealed record AssemblyScanReportEntry(string AssemblyName);

/// <summary>Contains the result of a type scan expression.</summary>
[PublicAPI]
public sealed record TypeScanReport(string SourceAssemblyName, string SourceExpression, ImmutableList<TypeScanAssemblyReportEntry> Entries);

/// <summary>Contains the types selected from one assembly by a type scan expression.</summary>
[PublicAPI]
public sealed record TypeScanAssemblyReportEntry(string AssemblyName, ImmutableList<TypeScanReportEntry> Entries);

/// <summary>Identifies a type selected by a type scan expression.</summary>
[PublicAPI]
public sealed record TypeScanReportEntry(string TypeName);

/// <summary>Contains the registrations produced by a service descriptor scan expression.</summary>
[PublicAPI]
public sealed record ServiceDescriptorScanReport(string SourceAssemblyName, string SourceExpression, ImmutableList<ServiceDescriptorAssemblyScanReportEntry> Entries);

/// <summary>Contains the registrations produced from one scanned assembly.</summary>
[PublicAPI]
public sealed record ServiceDescriptorAssemblyScanReportEntry(string AssemblyName, ImmutableList<ServiceDescriptorScanReportEntry> Entries);

/// <summary>Describes one service descriptor produced by a service descriptor scan.</summary>
[PublicAPI]
public sealed record ServiceDescriptorScanReportEntry(ServiceLifetime ServiceLifetime, string ServiceTypeName, string ImplementationTypeName);
