using System.Collections.Immutable;
using System.Text;
using Specular.Analyzers.Configuration;

namespace Specular.Analyzers.ScanReport;

internal static class ScanReportBuilder
{
    /// <summary>The <c>SpecularScanReport</c> class, as text at the top level of the file.</summary>
    public static string GetScanReport(
        ImmutableList<ResolvedSourceLocation> assemblySources,
        ImmutableList<ResolvedSourceLocation> reflectionSources,
        ImmutableList<ResolvedSourceLocation> serviceDescriptorSources
    )
    {
        var assemblyReports = assemblySources
                             .GroupBy(z => z.Location)
                             .OrderBy(z => z.Key.FilePath, StringComparer.Ordinal)
                             .ThenBy(z => z.Key.LineNumber)
                             .ThenBy(z => z.Key.ExpressionHash, StringComparer.Ordinal)
                             .Select(group =>
                                         new AssemblyReport(
                                             group.Key,
                                             group
                                                .SelectMany(z => z.DiscoveredAssemblies)
                                                .DistinctBy(z => z.Assembly)
                                                  .OrderBy(z => z.Assembly, StringComparer.Ordinal)
                                                  .ToImmutableArray()
                                         )
                              )
                             .ToImmutableArray();
        var typeReports = reflectionSources
                         .GroupBy(z => z.Location)
                         .OrderBy(z => z.Key.FilePath, StringComparer.Ordinal)
                         .ThenBy(z => z.Key.LineNumber)
                         .ThenBy(z => z.Key.ExpressionHash, StringComparer.Ordinal)
                         .Select(group =>
                                     new TypeReport(
                                         group.Key,
                                         group
                                            .Where(z => z.ScannedAssemblyName is { })
                                            .GroupBy(z => z.ScannedAssemblyName!, StringComparer.Ordinal)
                                            .OrderBy(z => z.Key, StringComparer.Ordinal)
                                            .Select(assembly => new TypeAssemblyReport(
                                                        assembly.Key,
                                                        assembly.SelectMany(z => z.DiscoveredTypes).Distinct().OrderBy(z => z.Type, StringComparer.Ordinal).ToImmutableArray()
                                                    )
                                             )
                                            .ToImmutableArray()
                                     )
                          )
                         .ToImmutableArray();
        var serviceReports = serviceDescriptorSources
                            .GroupBy(z => z.Location)
                            .OrderBy(z => z.Key.FilePath, StringComparer.Ordinal)
                            .ThenBy(z => z.Key.LineNumber)
                            .ThenBy(z => z.Key.ExpressionHash, StringComparer.Ordinal)
                            .Select(group =>
                                        new ServiceDescriptorReport(
                                            group.Key,
                                            group
                                               .Where(z => z.ScannedAssemblyName is { })
                                               .GroupBy(z => z.ScannedAssemblyName!, StringComparer.Ordinal)
                                               .OrderBy(z => z.Key, StringComparer.Ordinal)
                                               .Select(assembly => new ServiceDescriptorAssemblyReport(
                                                           assembly.Key,
                                                           assembly
                                                              .SelectMany(z => z.DiscoveredServiceDescriptors)
                                                              .Distinct()
                                                              .OrderBy(z => z.ServiceType, StringComparer.Ordinal)
                                                              .ThenBy(z => z.ImplementationType, StringComparer.Ordinal)
                                                              .ThenBy(z => z.Lifetime, StringComparer.Ordinal)
                                                              .ToImmutableArray()
                                                       )
                                                )
                                               .ToImmutableArray()
                                        )
                             )
                            .ToImmutableArray();

        var text = new StringBuilder(4096)
                  .Append(Helpers.CompilerGeneratedAttributes).Append(EndOfLine)
                  .Append("internal sealed class SpecularScanReport").Append(EndOfLine)
                  .Append('{').Append(EndOfLine)
                  .Append(SourceWriter.Indent(1)).Append("public static ScanResults GetScanResults() => new(");

        // The three arguments are written on the member's line, depth 1; a list that has items opens on a line of its own below it.
        const int memberDepth = 1;
        WriteList(text, assemblyReports, memberDepth, false, WriteAssemblyReport);
        _ = text.Append(',');
        WriteList(text, typeReports, memberDepth, true, WriteTypeReport);
        _ = text.Append(',');
        WriteList(text, serviceReports, memberDepth, true, WriteServiceDescriptorReport);
        return text.Append(");").Append(EndOfLine).Append('}').Append(EndOfLine).ToString();
    }

    private const string EndOfLine = CodeText.SourceEndOfLine;

    // A collection expression: [] where it is, or one element per line, its brackets a level below the line that holds it and its
    // elements a level below those.
    private static void WriteList<T>(StringBuilder text, ImmutableArray<T> items, int lineDepth, bool afterComma, Action<StringBuilder, T, int> writeItem)
    {
        if (items.IsEmpty)
        {
            _ = text.Append(afterComma ? " []" : "[]");
            return;
        }

        var bracketIndent = SourceWriter.Indent(lineDepth + 1);
        _ = text.Append(EndOfLine).Append(bracketIndent).Append('[').Append(EndOfLine);
        for (var i = 0; i < items.Length; i++)
        {
            _ = text.Append(SourceWriter.Indent(lineDepth + 2));
            writeItem(text, items[i], lineDepth + 2);
            if (i < items.Length - 1) _ = text.Append(',');
            _ = text.Append(EndOfLine);
        }

        _ = text.Append(bracketIndent).Append(']');
    }

    private static void WriteAssemblyReport(StringBuilder text, AssemblyReport report, int depth)
    {
        _ = text.Append("new(").Append(CodeText.Literal(report.Location.SourceAssemblyName)).Append(", ").Append(CodeText.Literal(report.Location.SourceExpression)).Append(',');
        WriteList(text, report.Assemblies, depth, true, static (text, item, _) => text.Append("new(").Append(CodeText.Literal(item.Assembly)).Append(')'));
        _ = text.Append(')');
    }

    private static void WriteTypeReport(StringBuilder text, TypeReport report, int depth)
    {
        _ = text.Append("new(").Append(CodeText.Literal(report.Location.SourceAssemblyName)).Append(", ").Append(CodeText.Literal(report.Location.SourceExpression)).Append(',');
        WriteList(
            text,
            report.Assemblies,
            depth,
            true,
            static (text, assembly, depth) =>
            {
                _ = text.Append("new(").Append(CodeText.Literal(assembly.AssemblyName)).Append(',');
                WriteList(text, assembly.Types, depth, true, static (text, item, _) => text.Append("new(").Append(CodeText.Literal(item.Type)).Append(')'));
                _ = text.Append(')');
            }
        );
        _ = text.Append(')');
    }

    private static void WriteServiceDescriptorReport(StringBuilder text, ServiceDescriptorReport report, int depth)
    {
        _ = text.Append("new(").Append(CodeText.Literal(report.Location.SourceAssemblyName)).Append(", ").Append(CodeText.Literal(report.Location.SourceExpression)).Append(',');
        WriteList(
            text,
            report.Assemblies,
            depth,
            true,
            static (text, assembly, depth) =>
            {
                _ = text.Append("new(").Append(CodeText.Literal(assembly.AssemblyName)).Append(',');
                WriteList(
                    text,
                    assembly.Entries,
                    depth,
                    true,
                    static (text, item, _) => text
                                             .Append("new(ServiceLifetime.")
                                             .Append(item.Lifetime)
                                             .Append(", ")
                                             .Append(CodeText.Literal(item.ServiceType))
                                             .Append(", ")
                                             .Append(CodeText.Literal(item.ImplementationType))
                                             .Append(')')
                );
                _ = text.Append(')');
            }
        );
        _ = text.Append(')');
    }

    private sealed record AssemblyReport(SourceLocation Location, ImmutableArray<AssemblyScanReportEntryData> Assemblies);

    private sealed record TypeReport(SourceLocation Location, ImmutableArray<TypeAssemblyReport> Assemblies);

    private sealed record TypeAssemblyReport(string AssemblyName, ImmutableArray<TypeScanReportEntryData> Types);

    private sealed record ServiceDescriptorReport(SourceLocation Location, ImmutableArray<ServiceDescriptorAssemblyReport> Assemblies);

    private sealed record ServiceDescriptorAssemblyReport(string AssemblyName, ImmutableArray<ServiceDescriptorScanReportEntryData> Entries);
}
