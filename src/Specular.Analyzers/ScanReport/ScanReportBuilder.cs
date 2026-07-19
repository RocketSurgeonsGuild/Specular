using System.Collections.Immutable;
using System.Globalization;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Specular.Analyzers.Configuration;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Specular.Analyzers.ScanReport;

// Emits SpecularScanReport.g.cs: a standalone, internal snapshot of every scanner-expression call site
// (GetAssemblies/GetTypes/Scan), the assemblies/types it resolved, and a Mermaid rendering of the same
// data. Deliberately kept separate from AssemblyProviderBuilder/SpecularProvider.g.cs so it works even
// when <SpecularEmitProvider>false</SpecularEmitProvider> is set, and so it introduces no new public API.
internal static class ScanReportBuilder
{
    public static SourceText GetScanReport(
        ImmutableList<ResolvedSourceLocation> assemblySources,
        ImmutableList<ResolvedSourceLocation> reflectionSources,
        ImmutableList<ResolvedSourceLocation> serviceDescriptorSources,
        Compilation compilation,
        ImmutableDictionary<string, IAssemblySymbol> assemblySymbols
    )
    {
        var entries = assemblySources
                     .Concat(reflectionSources)
                     .Concat(serviceDescriptorSources)
                     .GroupBy(z => z.Location)
                     .Select(group => BuildEntry(group, compilation, assemblySymbols))
                     .OrderBy(z => z.FilePath, StringComparer.Ordinal)
                     .ThenBy(z => z.LineNumber)
                     .ThenBy(z => z.ExpressionHash, StringComparer.Ordinal)
                     .ToImmutableArray();

        var sb = new StringBuilder();
        sb.Append(
            """
            #nullable enable
            using System;
            using System.Collections.Generic;
            using System.Reflection;

            namespace Specular;

            [System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute]
            internal enum ScannerExpressionKind
            {
                Assembly, Reflection, ServiceDescriptor
            }

            [System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
            internal sealed record ScanReportType(string AssemblyName, string TypeName, string? Type);

            [System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
            internal sealed record ScanReportAssembly(string AssemblyName, Assembly? Assembly);

            [System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
            internal sealed record ScannerExpression(ScannerExpressionKind Kind, string FilePath, int LineNumber, IReadOnlyList<ScanReportAssembly> DiscoveredAssemblies, IReadOnlyList<ScanReportType> DiscoveredTypes);

            [System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
            internal static class SpecularScanReport
            {
                public static IReadOnlyList<ScannerExpression> Entries { get; } = new ScannerExpression[]
                {

            """
        );

        foreach (var entry in entries)
        {
            sb.Append("        new(ScannerExpressionKind.").Append(entry.Kind).Append(", ");
            sb.Append(ToLiteral(entry.FilePath)).Append(", ");
            sb.Append(entry.LineNumber.ToString(CultureInfo.InvariantCulture)).Append(", ");

            sb.Append("new ScanReportAssembly[] { ");
            sb.Append(string.Join(", ", entry.Assemblies.Select(a => $"new({ToLiteral(a.AssemblyName)}, {ToLiteral(a.Expression)})")));
            sb.Append(" }, ");

            sb.Append("new ScanReportType[] { ");
            sb.Append(string.Join(", ", entry.Types.Select(t => $"new({ToLiteral(t.Assembly)}, {ToLiteral(t.Type)}, {ToLiteral(t.Expression)})")));
            sb.Append(" }),\n");
        }

        sb.Append("    };\n\n");
        sb.Append("    public const string MermaidDiagram = @\"").Append(MermaidReportBuilder.Build(entries).Replace("\"", "\"\"")).Append("\";\n");
        sb.Append("}\n");
        sb.Append("#nullable restore\n");

        return CSharpSyntaxTree.ParseText(sb.ToString()).GetRoot().NormalizeWhitespace().GetText(Encoding.UTF8);
    }

    private static Entry BuildEntry(IGrouping<SourceLocation, ResolvedSourceLocation> group, Compilation compilation, ImmutableDictionary<string, IAssemblySymbol> assemblySymbols)
    {
        var location = group.Key;

        var types = group
                   .SelectMany(z => z.DiscoveredTypes.IsDefault ? Enumerable.Empty<ScanReportTypeData>() : z.DiscoveredTypes)
                   .Distinct()
                   .OrderBy(z => z.Assembly, StringComparer.Ordinal)
                   .ThenBy(z => z.Type, StringComparer.Ordinal)
                   .Select(z => (z.Assembly, z.Type, Expression: ResolveTypeExpression(z, compilation, assemblySymbols)))
                   .ToImmutableArray();

        var assemblies = group
                        .SelectMany(z => z.DiscoveredAssemblies.IsDefault ? Enumerable.Empty<string>() : z.DiscoveredAssemblies)
                        .Distinct(StringComparer.Ordinal)
                        .OrderBy(z => z, StringComparer.Ordinal)
                        .Select(z => (AssemblyName: z, Expression: ResolveAssemblyExpression(z, compilation, assemblySymbols)))
                        .ToImmutableArray();

        return new(location.Kind.ToString(), location.FilePath, location.LineNumber, location.ExpressionHash, assemblies, types);
    }

    private static string? ResolveTypeExpression(ScanReportTypeData data, Compilation compilation, ImmutableDictionary<string, IAssemblySymbol> assemblySymbols)
    {
        var symbol = AssemblyProviderConfiguration.findType(assemblySymbols, compilation, data.Assembly, data.Type);
        return symbol is { } ? $"typeof({symbol.ToDisplayString()})" : "";
    }

    private static string? ResolveAssemblyExpression(string assemblyName, Compilation compilation, ImmutableDictionary<string, IAssemblySymbol> assemblySymbols) =>
        assemblySymbols.TryGetValue(assemblyName, out var assembly) && StatementGeneration.GetAssemblyExpression(compilation, assembly) is { } expression
            ? expression.NormalizeWhitespace().ToFullString()
            : "";

    private static string ToLiteral(string value) => Literal(value).ToFullString();

    internal readonly record struct Entry(
        string Kind,
        string FilePath,
        int LineNumber,
        string ExpressionHash,
        ImmutableArray<(string AssemblyName, string? Expression)> Assemblies,
        ImmutableArray<(string Assembly, string Type, string Expression)> Types
    );
}
