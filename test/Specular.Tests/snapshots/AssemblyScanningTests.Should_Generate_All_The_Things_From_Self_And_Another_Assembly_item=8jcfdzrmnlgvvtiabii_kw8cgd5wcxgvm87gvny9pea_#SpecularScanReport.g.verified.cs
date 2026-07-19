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
        new(ScannerExpressionKind.Reflection, "Input0.cs", 16, new ScanReportAssembly[] { }, new ScanReportType[] { new("Specular", "Specular.ServiceRegistrationAttribute`1", "typeof(Specular.ServiceRegistrationAttribute<TService>)"), new("Specular", "Specular.ServiceRegistrationAttribute`2", "typeof(Specular.ServiceRegistrationAttribute<TService1, TService2>)"), new("Specular", "Specular.ServiceRegistrationAttribute`3", "typeof(Specular.ServiceRegistrationAttribute<TService1, TService2, TService3>)"), new("Specular", "Specular.ServiceRegistrationAttribute`4", "typeof(Specular.ServiceRegistrationAttribute<TService1, TService2, TService3, TService4>)"), new("Specular", "Specular.ServiceRegistrationAttribute`5", "typeof(Specular.ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>)"), new("Specular", "Specular.ServiceRegistrationAttribute`6", "typeof(Specular.ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6>)"), new("Specular", "Specular.ServiceRegistrationAttribute`7", "typeof(Specular.ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7>)"), new("Specular", "Specular.ServiceRegistrationAttribute`8", "typeof(Specular.ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8>)"), new("TestAssembly", "TestAssembly.IGenericService`1", "typeof(TestAssembly.IGenericService<T>)"), new("TestAssembly", "TestAssembly.IRequestHandler`2", "typeof(TestAssembly.IRequestHandler<T, R>)"), new("TestAssembly", "TestAssembly.IRequest`1", "typeof(TestAssembly.IRequest<T>)"), new("TestAssembly", "TestAssembly.IValidator`1", "typeof(TestAssembly.IValidator<T>)") }),
    };

    public const string MermaidDiagram = @"flowchart TD
    F0[Input0.cs:16] --> E0{Reflection}
    E0 --> E0T0[Specular.ServiceRegistrationAttribute`1]
    E0 --> E0T1[Specular.ServiceRegistrationAttribute`2]
    E0 --> E0T2[Specular.ServiceRegistrationAttribute`3]
    E0 --> E0T3[Specular.ServiceRegistrationAttribute`4]
    E0 --> E0T4[Specular.ServiceRegistrationAttribute`5]
    E0 --> E0T5[Specular.ServiceRegistrationAttribute`6]
    E0 --> E0T6[Specular.ServiceRegistrationAttribute`7]
    E0 --> E0T7[Specular.ServiceRegistrationAttribute`8]
    E0 --> E0T8[TestAssembly.IGenericService`1]
    E0 --> E0T9[TestAssembly.IRequestHandler`2]
    E0 --> E0T10[TestAssembly.IRequest`1]
    E0 --> E0T11[TestAssembly.IValidator`1]
";
}
#nullable restore
