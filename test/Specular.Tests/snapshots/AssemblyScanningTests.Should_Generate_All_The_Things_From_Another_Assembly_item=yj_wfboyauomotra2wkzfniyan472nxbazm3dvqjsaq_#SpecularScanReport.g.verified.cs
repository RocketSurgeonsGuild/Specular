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
        new(ScannerExpressionKind.Reflection, "Input0.cs", 16, new ScanReportAssembly[] { }, new ScanReportType[] { new("OtherProject", "Specular.ScannerExpressionKind", ""), new("Specular", "Specular.Abstractions.TypeInfoFilter", "typeof(Specular.Abstractions.TypeInfoFilter)"), new("Specular", "Specular.Abstractions.TypeKindFilter", "typeof(Specular.Abstractions.TypeKindFilter)"), new("Specular", "Specular.ScannerExpressionKind", "") }),
    };

    public const string MermaidDiagram = @"flowchart TD
    F0[Input0.cs:16] --> E0{Reflection}
    E0 --> E0T0[Specular.ScannerExpressionKind]
    E0 --> E0T1[Specular.Abstractions.TypeInfoFilter]
    E0 --> E0T2[Specular.Abstractions.TypeKindFilter]
    E0 --> E0T3[Specular.ScannerExpressionKind]
";
}
#nullable restore
