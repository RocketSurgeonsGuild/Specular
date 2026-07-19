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
        new(ScannerExpressionKind.ServiceDescriptor, "Input0.cs", 17, new ScanReportAssembly[] { }, new ScanReportType[] { new("DependencyProject", "DependencyProject.Service", "typeof(DependencyProject.Service)"), new("TestProject", "TestProject.Service", "typeof(TestProject.Service)") }),
    };

    public const string MermaidDiagram = @"flowchart TD
    F0[Input0.cs:17] --> E0{ServiceDescriptor}
    E0 --> E0T0[DependencyProject.Service]
    E0 --> E0T1[TestProject.Service]
";
}
#nullable restore
