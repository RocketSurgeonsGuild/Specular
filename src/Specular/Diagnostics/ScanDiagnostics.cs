using System.Collections.Immutable;
using Microsoft.Extensions.DependencyInjection;

namespace Specular.Diagnostics;

public sealed record ScanResults(ImmutableList<AssemblyScanReport> AssemblyScanReports, ImmutableList<TypeScanReport> TypeScanReports, ImmutableList<ServiceDescriptorScanReport> ServiceDescriptorScanReports);
public sealed record AssemblyScanReport(string SourceAssemblyName, string SourceExpression, ImmutableList<AssemblyScanReportEntry> Entries);
public sealed record AssemblyScanReportEntry(string AssemblyName);

public sealed record TypeScanReport(string SourceAssemblyName, string SourceExpression, ImmutableList<TypeScanReportEntry> Entries);
public sealed record TypeScanAssemblyReportEntry(string AssemblyName, ImmutableList<TypeScanReportEntry> Entries);
public sealed record TypeScanReportEntry(string TypeName);

public sealed record ServiceDescriptorScanReport(string SourceAssemblyName, string SourceExpression, ImmutableList<ServiceDescriptorAssemblyScanReportEntry> Entries);
public sealed record ServiceDescriptorAssemblyScanReportEntry(string AssemblyName, ImmutableList<ServiceDescriptorScanReportEntry> Entries);
public sealed record ServiceDescriptorScanReportEntry(ServiceLifetime ServiceLifetime, string TypeName);
