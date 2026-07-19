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
        new(ScannerExpressionKind.Reflection, "Input0.cs", 16, new ScanReportAssembly[] { }, new ScanReportType[] { new("Specular", "Specular.ScanReportAssembly", ""), new("Specular", "Specular.ScanReportType", ""), new("Specular", "Specular.ScannerExpression", ""), new("Specular", "Specular.ScannerExpressionKind", ""), new("Specular", "Specular.SpecularScanReport", ""), new("TestAssembly", "TestAssembly.GenericService", "typeof(TestAssembly.GenericService)"), new("TestAssembly", "TestAssembly.Nested+MyRecord", "typeof(TestAssembly.Nested.MyRecord)"), new("TestAssembly", "TestAssembly.Nested+Validator", "typeof(TestAssembly.Nested.Validator)"), new("TestAssembly", "TestAssembly.Request", "typeof(TestAssembly.Request)"), new("TestAssembly", "TestAssembly.RequestHandler", "typeof(TestAssembly.RequestHandler)"), new("TestAssembly", "TestAssembly.Response", "typeof(TestAssembly.Response)"), new("TestAssembly", "TestAssembly.ServiceB", "typeof(TestAssembly.ServiceB)"), new("TestProject", "Program", "typeof(Program)") }),
    };

    public const string MermaidDiagram = @"flowchart TD
    F0[Input0.cs:16] --> E0{Reflection}
    E0 --> E0T0[Specular.ScanReportAssembly]
    E0 --> E0T1[Specular.ScanReportType]
    E0 --> E0T2[Specular.ScannerExpression]
    E0 --> E0T3[Specular.ScannerExpressionKind]
    E0 --> E0T4[Specular.SpecularScanReport]
    E0 --> E0T5[TestAssembly.GenericService]
    E0 --> E0T6[TestAssembly.Nested+MyRecord]
    E0 --> E0T7[TestAssembly.Nested+Validator]
    E0 --> E0T8[TestAssembly.Request]
    E0 --> E0T9[TestAssembly.RequestHandler]
    E0 --> E0T10[TestAssembly.Response]
    E0 --> E0T11[TestAssembly.ServiceB]
    E0 --> E0T12[Program]
";
}
#nullable restore
