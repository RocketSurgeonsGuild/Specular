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
        new(ScannerExpressionKind.Reflection, "Input0.cs", 16, new ScanReportAssembly[] { }, new ScanReportType[] { new("Specular", "Specular.Abstractions.IReflectionAssemblySelector", "typeof(Specular.Abstractions.IReflectionAssemblySelector)"), new("Specular", "Specular.Abstractions.IReflectionTypeSelector", "typeof(Specular.Abstractions.IReflectionTypeSelector)"), new("Specular", "Specular.Abstractions.IServiceDescriptorAssemblySelector", "typeof(Specular.Abstractions.IServiceDescriptorAssemblySelector)"), new("Specular", "Specular.Abstractions.IServiceDescriptorTypeSelector", "typeof(Specular.Abstractions.IServiceDescriptorTypeSelector)"), new("Specular", "Specular.Abstractions.IServiceLifetimeSelector", "typeof(Specular.Abstractions.IServiceLifetimeSelector)"), new("Specular", "Specular.Abstractions.IServiceTypeSelector", "typeof(Specular.Abstractions.IServiceTypeSelector)"), new("Specular", "Specular.Abstractions.ITypeFilter", "typeof(Specular.Abstractions.ITypeFilter)"), new("Specular", "Specular.Abstractions.TypeInfoFilter", "typeof(Specular.Abstractions.TypeInfoFilter)"), new("Specular", "Specular.Abstractions.TypeKindFilter", "typeof(Specular.Abstractions.TypeKindFilter)"), new("Specular", "Specular.ScannerExpressionKind", ""), new("TestAssembly", "TestAssembly.IGenericService`1", "typeof(TestAssembly.IGenericService<T>)"), new("TestAssembly", "TestAssembly.IOther", "typeof(TestAssembly.IOther)"), new("TestAssembly", "TestAssembly.IRequestHandler`2", "typeof(TestAssembly.IRequestHandler<T, R>)"), new("TestAssembly", "TestAssembly.IRequest`1", "typeof(TestAssembly.IRequest<T>)"), new("TestAssembly", "TestAssembly.IService", "typeof(TestAssembly.IService)"), new("TestAssembly", "TestAssembly.IServiceB", "typeof(TestAssembly.IServiceB)"), new("TestAssembly", "TestAssembly.IValidator", "typeof(TestAssembly.IValidator)"), new("TestAssembly", "TestAssembly.IValidator`1", "typeof(TestAssembly.IValidator<T>)") }),
    };

    public const string MermaidDiagram = @"flowchart TD
    F0[Input0.cs:16] --> E0{Reflection}
    E0 --> E0T0[Specular.Abstractions.IReflectionAssemblySelector]
    E0 --> E0T1[Specular.Abstractions.IReflectionTypeSelector]
    E0 --> E0T2[Specular.Abstractions.IServiceDescriptorAssemblySelector]
    E0 --> E0T3[Specular.Abstractions.IServiceDescriptorTypeSelector]
    E0 --> E0T4[Specular.Abstractions.IServiceLifetimeSelector]
    E0 --> E0T5[Specular.Abstractions.IServiceTypeSelector]
    E0 --> E0T6[Specular.Abstractions.ITypeFilter]
    E0 --> E0T7[Specular.Abstractions.TypeInfoFilter]
    E0 --> E0T8[Specular.Abstractions.TypeKindFilter]
    E0 --> E0T9[Specular.ScannerExpressionKind]
    E0 --> E0T10[TestAssembly.IGenericService`1]
    E0 --> E0T11[TestAssembly.IOther]
    E0 --> E0T12[TestAssembly.IRequestHandler`2]
    E0 --> E0T13[TestAssembly.IRequest`1]
    E0 --> E0T14[TestAssembly.IService]
    E0 --> E0T15[TestAssembly.IServiceB]
    E0 --> E0T16[TestAssembly.IValidator]
    E0 --> E0T17[TestAssembly.IValidator`1]
";
}
#nullable restore
