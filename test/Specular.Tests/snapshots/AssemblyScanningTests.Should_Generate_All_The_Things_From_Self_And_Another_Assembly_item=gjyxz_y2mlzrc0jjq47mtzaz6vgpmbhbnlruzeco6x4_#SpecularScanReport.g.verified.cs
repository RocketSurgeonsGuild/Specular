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
    E0 --> E0T1[Program]
    E0 --> E0T2[Specular.ScanReportAssembly]
    E0 --> E0T3[Specular.ScanReportType]
    E0 --> E0T4[Specular.ScannerExpression]
    E0 --> E0T5[Specular.ScannerExpressionKind]
    E0 --> E0T6[Microsoft.CodeAnalysis.EmbeddedAttribute]
    E0 --> E0T7[Specular.Abstractions.ExcludeFromSpecularAttribute]
    E0 --> E0T8[Specular.Abstractions.IReflectionAssemblySelector]
    E0 --> E0T9[Specular.Abstractions.IReflectionTypeSelector]
    E0 --> E0T10[Specular.Abstractions.IServiceDescriptorAssemblySelector]
    E0 --> E0T11[Specular.Abstractions.IServiceDescriptorTypeSelector]
    E0 --> E0T12[Specular.Abstractions.IServiceLifetimeSelector]
    E0 --> E0T13[Specular.Abstractions.IServiceTypeSelector]
    E0 --> E0T14[Specular.Abstractions.ITypeFilter]
    E0 --> E0T15[Specular.Abstractions.SpecularHashAttribute]
    E0 --> E0T16[Specular.Abstractions.TypeInfoFilter]
    E0 --> E0T17[Specular.Abstractions.TypeKindFilter]
    E0 --> E0T18[Specular.RegistrationLifetimeAttribute]
    E0 --> E0T19[Specular.ScanReportAssembly]
    E0 --> E0T20[Specular.ScanReportType]
    E0 --> E0T21[Specular.ScannerExpression]
    E0 --> E0T22[Specular.ScannerExpressionKind]
    E0 --> E0T23[Specular.ServiceRegistrationAttribute]
    E0 --> E0T24[Specular.ServiceRegistrationAttribute`1]
    E0 --> E0T25[Specular.ServiceRegistrationAttribute`2]
    E0 --> E0T26[Specular.ServiceRegistrationAttribute`3]
    E0 --> E0T27[Specular.ServiceRegistrationAttribute`4]
    E0 --> E0T28[Specular.ServiceRegistrationAttribute`5]
    E0 --> E0T29[Specular.ServiceRegistrationAttribute`6]
    E0 --> E0T30[Specular.ServiceRegistrationAttribute`7]
    E0 --> E0T31[Specular.ServiceRegistrationAttribute`8]
    E0 --> E0T32[Microsoft.CodeAnalysis.EmbeddedAttribute]
    E0 --> E0T33[TestAssembly.GenericService]
    E0 --> E0T34[TestAssembly.GenericServiceB]
    E0 --> E0T35[TestAssembly.IGenericService`1]
    E0 --> E0T36[TestAssembly.IOther]
    E0 --> E0T37[TestAssembly.IRequestHandler`2]
    E0 --> E0T38[TestAssembly.IRequest`1]
    E0 --> E0T39[TestAssembly.IService]
    E0 --> E0T40[TestAssembly.IServiceB]
    E0 --> E0T41[TestAssembly.IValidator]
    E0 --> E0T42[TestAssembly.IValidator`1]
    E0 --> E0T43[TestAssembly.Nested+GenericServiceA]
    E0 --> E0T44[TestAssembly.Nested+MyRecord]
    E0 --> E0T45[TestAssembly.Nested+ServiceA]
    E0 --> E0T46[TestAssembly.Nested+Validator]
    E0 --> E0T47[TestAssembly.Request]
    E0 --> E0T48[TestAssembly.RequestHandler]
    E0 --> E0T49[TestAssembly.Response]
    E0 --> E0T50[TestAssembly.Service]
    E0 --> E0T51[TestAssembly.ServiceB]
    E0 --> E0T52[Microsoft.CodeAnalysis.EmbeddedAttribute]
    E0 --> E0T53[Program]
";
}
#nullable restore
