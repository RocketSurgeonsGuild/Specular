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
internal sealed record ScanReportType(string AssemblyName, string TypeName, Type? Type);
[System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal sealed record ScanReportAssembly(string AssemblyName, Assembly? Assembly);
[System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal sealed record ScannerExpression(ScannerExpressionKind Kind, string FilePath, int LineNumber, string SelectorText, IReadOnlyList<ScanReportAssembly> DiscoveredAssemblies, IReadOnlyList<ScanReportType> DiscoveredTypes);
[System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal static class SpecularScanReport
{
    public static IReadOnlyList<ScannerExpression> Entries { get; } = new ScannerExpression[]
    {
        new(ScannerExpressionKind.Assembly, "Input0.cs", 16, "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>()", new ScanReportAssembly[] { new("OtherProject", null), new("Specular", typeof(global::Specular.ISpecularProvider).Assembly), new("System.ComponentModel", typeof(global::System.IServiceProvider).Assembly), new("TestAssembly", typeof(global::TestAssembly.IService).Assembly), new("TestProject", null) }, new ScanReportType[] { }),
    };

    public const string MermaidDiagram = @"flowchart TD
    F0[Input0.cs:16] --> E0{Assembly}
    E0 --> E0A0[OtherProject]
    E0 --> E0A1[Specular]
    E0 --> E0A2[System.ComponentModel]
    E0 --> E0A3[TestAssembly]
    E0 --> E0A4[TestProject]
";
}
#nullable restore
