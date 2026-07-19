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
    E0 --> E0T1[Specular.Abstractions.ExcludeFromSpecularAttribute]
    E0 --> E0T2[Specular.Abstractions.SpecularHashAttribute]
    E0 --> E0T3[Specular.Abstractions.TypeInfoFilter]
    E0 --> E0T4[Specular.Abstractions.TypeKindFilter]
    E0 --> E0T5[Specular.RegistrationLifetimeAttribute]
    E0 --> E0T6[Specular.ScanReportAssembly]
    E0 --> E0T7[Specular.ScanReportType]
    E0 --> E0T8[Specular.ScannerExpression]
    E0 --> E0T9[Specular.ScannerExpressionKind]
    E0 --> E0T10[Specular.ServiceRegistrationAttribute]
    E0 --> E0T11[Specular.ServiceRegistrationAttribute`1]
    E0 --> E0T12[Specular.ServiceRegistrationAttribute`2]
    E0 --> E0T13[Specular.ServiceRegistrationAttribute`3]
    E0 --> E0T14[Specular.ServiceRegistrationAttribute`4]
    E0 --> E0T15[Specular.ServiceRegistrationAttribute`5]
    E0 --> E0T16[Specular.ServiceRegistrationAttribute`6]
    E0 --> E0T17[Specular.ServiceRegistrationAttribute`7]
    E0 --> E0T18[Specular.ServiceRegistrationAttribute`8]
    E0 --> E0T19[Specular.SpecularProviderServiceCollectionExtensions]
    E0 --> E0T20[Specular.SpecularScanReport]
    E0 --> E0T21[Specular.SpecularSupport]
    E0 --> E0T22[Microsoft.CodeAnalysis.EmbeddedAttribute]
    E0 --> E0T23[TestAssembly.GenericService]
    E0 --> E0T24[TestAssembly.GenericServiceB]
    E0 --> E0T25[TestAssembly.Nested]
    E0 --> E0T26[TestAssembly.Nested+GenericServiceA]
    E0 --> E0T27[TestAssembly.Nested+MyRecord]
    E0 --> E0T28[TestAssembly.Nested+ServiceA]
    E0 --> E0T29[TestAssembly.Nested+Validator]
    E0 --> E0T30[TestAssembly.Request]
    E0 --> E0T31[TestAssembly.RequestHandler]
    E0 --> E0T32[TestAssembly.Response]
    E0 --> E0T33[TestAssembly.Service]
    E0 --> E0T34[TestAssembly.ServiceB]
    E0 --> E0T35[Microsoft.CodeAnalysis.EmbeddedAttribute]
    E0 --> E0T36[Program]
";
}
#nullable restore
