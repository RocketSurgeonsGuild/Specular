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
        new(ScannerExpressionKind.ServiceDescriptor, "Input0.cs", 16, new ScanReportAssembly[] { }, new ScanReportType[] { new("Dependency0Project", "Dependency1Project.RequestHandler0", "typeof(Dependency1Project.RequestHandler0)"), new("Dependency1Project", "Dependency1Project.RequestHandler1", "typeof(Dependency1Project.RequestHandler1)"), new("Dependency2Project", "Dependency1Project.RequestHandler2", "typeof(Dependency1Project.RequestHandler2)"), new("Dependency3Project", "Dependency1Project.RequestHandler3", "typeof(Dependency1Project.RequestHandler3)") }),
    };

    public const string MermaidDiagram = @"flowchart TD
    F0[Input0.cs:16] --> E0{ServiceDescriptor}
    E0 --> E0T0[Dependency1Project.RequestHandler0]
    E0 --> E0T1[Dependency1Project.RequestHandler1]
    E0 --> E0T2[Dependency1Project.RequestHandler2]
    E0 --> E0T3[Dependency1Project.RequestHandler3]
";
}
#nullable restore
