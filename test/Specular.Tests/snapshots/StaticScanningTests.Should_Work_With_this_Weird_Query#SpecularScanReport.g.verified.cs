//HintName: Specular.Analyzers/Specular.Analyzers.SpecularProviderGenerator/SpecularScanReport.g.cs
#nullable enable
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Specular;
[System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute]
internal enum ScannerExpressionKind
{
    Assembly,
    Reflection,
    ServiceDescriptor
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
        new(ScannerExpressionKind.Reflection, "Input0.cs", 25, new ScanReportAssembly[] { }, new ScanReportType[] { new("TestProject", "OptionsA", "typeof(OptionsA)"), new("TestProject", "OptionsB", "typeof(OptionsB)") }),
        new(ScannerExpressionKind.Reflection, "Input1.cs", 12, new ScanReportAssembly[] { }, new ScanReportType[] { new("TestProject", "OptionsA", "typeof(OptionsA)"), new("TestProject", "OptionsB", "typeof(OptionsB)") }),
    };

    public const string MermaidDiagram = @"flowchart TD
    F0[Input0.cs:25] --> E0{Reflection}
    E0 --> E0T0[OptionsA]
    E0 --> E0T1[OptionsB]
    F1[Input1.cs:12] --> E1{Reflection}
    E1 --> E1T0[OptionsA]
    E1 --> E1T1[OptionsB]
";
}
#nullable restore
