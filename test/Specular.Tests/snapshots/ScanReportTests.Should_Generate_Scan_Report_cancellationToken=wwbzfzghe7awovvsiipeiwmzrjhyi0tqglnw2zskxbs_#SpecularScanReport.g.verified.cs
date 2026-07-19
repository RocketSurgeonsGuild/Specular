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
        new(ScannerExpressionKind.Assembly, "Input0.cs", 12, new ScanReportAssembly[] { new("TestAssembly", "typeof(global::TestAssembly.IService).Assembly") }, new ScanReportType[] { }),
        new(ScannerExpressionKind.Reflection, "Input0.cs", 13, new ScanReportAssembly[] { }, new ScanReportType[] { new("TestAssembly", "TestAssembly.IService", "typeof(TestAssembly.IService)"), new("TestAssembly", "TestAssembly.Nested+ServiceA", "typeof(TestAssembly.Nested.ServiceA)"), new("TestAssembly", "TestAssembly.Service", "typeof(TestAssembly.Service)"), new("TestAssembly", "TestAssembly.ServiceB", "typeof(TestAssembly.ServiceB)") }),
        new(ScannerExpressionKind.ServiceDescriptor, "Input0.cs", 14, new ScanReportAssembly[] { }, new ScanReportType[] { new("TestAssembly", "TestAssembly.GenericService", "typeof(TestAssembly.GenericService)"), new("TestAssembly", "TestAssembly.GenericServiceB", "typeof(TestAssembly.GenericServiceB)"), new("TestAssembly", "TestAssembly.Nested+GenericServiceA", "typeof(TestAssembly.Nested.GenericServiceA)") }),
    };

    public const string MermaidDiagram = @"flowchart TD
    F0[Input0.cs:12] --> E0{Assembly}
    E0 --> E0A0[TestAssembly]
    F1[Input0.cs:13] --> E1{Reflection}
    E1 --> E1T0[TestAssembly.IService]
    E1 --> E1T1[TestAssembly.Nested+ServiceA]
    E1 --> E1T2[TestAssembly.Service]
    E1 --> E1T3[TestAssembly.ServiceB]
    F2[Input0.cs:14] --> E2{ServiceDescriptor}
    E2 --> E2T0[TestAssembly.GenericService]
    E2 --> E2T1[TestAssembly.GenericServiceB]
    E2 --> E2T2[TestAssembly.Nested+GenericServiceA]
";
}
#nullable restore
