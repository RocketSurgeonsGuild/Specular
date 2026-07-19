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
        new(ScannerExpressionKind.ServiceDescriptor, "{CurrentDirectory}src/Specular/SpecularProviderServiceCollectionExtensions.cs", 19, new ScanReportAssembly[] { }, new ScanReportType[] { new("TestProject", "Nested+ServiceA", "typeof(Nested.ServiceA)"), new("TestProject", "Service", "typeof(Service)"), new("TestProject", "ServiceB", "typeof(ServiceB)") }),
        new(ScannerExpressionKind.ServiceDescriptor, "SpecularProviderServiceCollectionExtensions.cs", 20, new ScanReportAssembly[] { }, new ScanReportType[] { new("TestProject", "Nested+ServiceA", "typeof(Nested.ServiceA)"), new("TestProject", "Service", "typeof(Service)"), new("TestProject", "ServiceB", "typeof(ServiceB)") }),
    };

    public const string MermaidDiagram = @"flowchart TD
    F0[SpecularProviderServiceCollectionExtensions.cs:19] --> E0{ServiceDescriptor}
    E0 --> E0T0[Nested+ServiceA]
    E0 --> E0T1[Service]
    E0 --> E0T2[ServiceB]
    F1[SpecularProviderServiceCollectionExtensions.cs:20] --> E1{ServiceDescriptor}
    E1 --> E1T0[Nested+ServiceA]
    E1 --> E1T1[Service]
    E1 --> E1T2[ServiceB]
";
}
#nullable restore
