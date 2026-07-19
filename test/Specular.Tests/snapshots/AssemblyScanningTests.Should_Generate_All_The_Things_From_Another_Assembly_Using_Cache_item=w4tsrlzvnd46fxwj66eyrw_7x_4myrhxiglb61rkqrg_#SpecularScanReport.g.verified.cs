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
        new(ScannerExpressionKind.Reflection, "{scrubbed}"),
    };

    public const string MermaidDiagram = @"flowchart TD
    F0[Input0.cs:16] --> E0{Reflection}
    E0 --> E0T0[Microsoft.CodeAnalysis.EmbeddedAttribute]
    E0 --> E0T1[Specular.ScanReportAssembly]
    E0 --> E0T2[Specular.ScanReportType]
    E0 --> E0T3[Specular.ScannerExpression]
    E0 --> E0T4[Specular.ScannerExpressionKind]
    E0 --> E0T5[Microsoft.CodeAnalysis.EmbeddedAttribute]
    E0 --> E0T6[Specular.Abstractions.SpecularHashAttribute]
    E0 --> E0T7[Specular.Abstractions.TypeInfoFilter]
    E0 --> E0T8[Specular.Abstractions.TypeKindFilter]
    E0 --> E0T9[Specular.ScanReportAssembly]
    E0 --> E0T10[Specular.ScanReportType]
    E0 --> E0T11[Specular.ScannerExpression]
    E0 --> E0T12[Specular.ScannerExpressionKind]
    E0 --> E0T13[Specular.ServiceRegistrationAttribute`1]
    E0 --> E0T14[Specular.ServiceRegistrationAttribute`2]
    E0 --> E0T15[Specular.ServiceRegistrationAttribute`3]
    E0 --> E0T16[Specular.ServiceRegistrationAttribute`4]
    E0 --> E0T17[Specular.ServiceRegistrationAttribute`5]
    E0 --> E0T18[Specular.ServiceRegistrationAttribute`6]
    E0 --> E0T19[Specular.ServiceRegistrationAttribute`7]
    E0 --> E0T20[Specular.ServiceRegistrationAttribute`8]
    E0 --> E0T21[Microsoft.CodeAnalysis.EmbeddedAttribute]
    E0 --> E0T22[Microsoft.CodeAnalysis.EmbeddedAttribute]
";
}
#nullable restore
