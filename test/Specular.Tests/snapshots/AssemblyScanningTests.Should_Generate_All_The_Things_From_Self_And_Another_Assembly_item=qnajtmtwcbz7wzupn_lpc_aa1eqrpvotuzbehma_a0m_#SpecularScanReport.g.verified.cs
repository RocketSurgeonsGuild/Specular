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
        new(ScannerExpressionKind.Reflection, "Input0.cs", 16, new ScanReportAssembly[] { }, new ScanReportType[] { new("OtherProject", "Program", "typeof(Program)"), new("OtherProject", "Specular.SpecularScanReport", ""), new("Specular", "Specular.Abstractions.IReflectionAssemblySelector", "typeof(Specular.Abstractions.IReflectionAssemblySelector)"), new("Specular", "Specular.Abstractions.IReflectionTypeSelector", "typeof(Specular.Abstractions.IReflectionTypeSelector)"), new("Specular", "Specular.Abstractions.IServiceDescriptorAssemblySelector", "typeof(Specular.Abstractions.IServiceDescriptorAssemblySelector)"), new("Specular", "Specular.Abstractions.IServiceDescriptorTypeSelector", "typeof(Specular.Abstractions.IServiceDescriptorTypeSelector)"), new("Specular", "Specular.Abstractions.IServiceLifetimeSelector", "typeof(Specular.Abstractions.IServiceLifetimeSelector)"), new("Specular", "Specular.Abstractions.IServiceTypeSelector", "typeof(Specular.Abstractions.IServiceTypeSelector)"), new("Specular", "Specular.Abstractions.ITypeFilter", "typeof(Specular.Abstractions.ITypeFilter)"), new("Specular", "Specular.SpecularProviderServiceCollectionExtensions", "typeof(Specular.SpecularProviderServiceCollectionExtensions)"), new("Specular", "Specular.SpecularScanReport", ""), new("Specular", "Specular.SpecularSupport", "typeof(Specular.SpecularSupport)"), new("TestAssembly", "TestAssembly.GenericService", "typeof(TestAssembly.GenericService)"), new("TestAssembly", "TestAssembly.GenericServiceB", "typeof(TestAssembly.GenericServiceB)"), new("TestAssembly", "TestAssembly.IGenericService`1", "typeof(TestAssembly.IGenericService<T>)"), new("TestAssembly", "TestAssembly.IOther", "typeof(TestAssembly.IOther)"), new("TestAssembly", "TestAssembly.IRequestHandler`2", "typeof(TestAssembly.IRequestHandler<T, R>)"), new("TestAssembly", "TestAssembly.IRequest`1", "typeof(TestAssembly.IRequest<T>)"), new("TestAssembly", "TestAssembly.IService", "typeof(TestAssembly.IService)"), new("TestAssembly", "TestAssembly.IServiceB", "typeof(TestAssembly.IServiceB)"), new("TestAssembly", "TestAssembly.IValidator", "typeof(TestAssembly.IValidator)"), new("TestAssembly", "TestAssembly.IValidator`1", "typeof(TestAssembly.IValidator<T>)"), new("TestAssembly", "TestAssembly.Nested", "typeof(TestAssembly.Nested)"), new("TestAssembly", "TestAssembly.Nested+GenericServiceA", "typeof(TestAssembly.Nested.GenericServiceA)"), new("TestAssembly", "TestAssembly.Nested+MyRecord", "typeof(TestAssembly.Nested.MyRecord)"), new("TestAssembly", "TestAssembly.Nested+ServiceA", "typeof(TestAssembly.Nested.ServiceA)"), new("TestAssembly", "TestAssembly.Nested+Validator", "typeof(TestAssembly.Nested.Validator)"), new("TestAssembly", "TestAssembly.Request", "typeof(TestAssembly.Request)"), new("TestAssembly", "TestAssembly.RequestHandler", "typeof(TestAssembly.RequestHandler)"), new("TestAssembly", "TestAssembly.Response", "typeof(TestAssembly.Response)"), new("TestAssembly", "TestAssembly.Service", "typeof(TestAssembly.Service)"), new("TestAssembly", "TestAssembly.ServiceB", "typeof(TestAssembly.ServiceB)"), new("TestProject", "Program", "typeof(Program)") }),
    };

    public const string MermaidDiagram = @"flowchart TD
    F0[Input0.cs:16] --> E0{Reflection}
    E0 --> E0T0[Program]
    E0 --> E0T1[Specular.SpecularScanReport]
    E0 --> E0T2[Specular.Abstractions.IReflectionAssemblySelector]
    E0 --> E0T3[Specular.Abstractions.IReflectionTypeSelector]
    E0 --> E0T4[Specular.Abstractions.IServiceDescriptorAssemblySelector]
    E0 --> E0T5[Specular.Abstractions.IServiceDescriptorTypeSelector]
    E0 --> E0T6[Specular.Abstractions.IServiceLifetimeSelector]
    E0 --> E0T7[Specular.Abstractions.IServiceTypeSelector]
    E0 --> E0T8[Specular.Abstractions.ITypeFilter]
    E0 --> E0T9[Specular.SpecularProviderServiceCollectionExtensions]
    E0 --> E0T10[Specular.SpecularScanReport]
    E0 --> E0T11[Specular.SpecularSupport]
    E0 --> E0T12[TestAssembly.GenericService]
    E0 --> E0T13[TestAssembly.GenericServiceB]
    E0 --> E0T14[TestAssembly.IGenericService`1]
    E0 --> E0T15[TestAssembly.IOther]
    E0 --> E0T16[TestAssembly.IRequestHandler`2]
    E0 --> E0T17[TestAssembly.IRequest`1]
    E0 --> E0T18[TestAssembly.IService]
    E0 --> E0T19[TestAssembly.IServiceB]
    E0 --> E0T20[TestAssembly.IValidator]
    E0 --> E0T21[TestAssembly.IValidator`1]
    E0 --> E0T22[TestAssembly.Nested]
    E0 --> E0T23[TestAssembly.Nested+GenericServiceA]
    E0 --> E0T24[TestAssembly.Nested+MyRecord]
    E0 --> E0T25[TestAssembly.Nested+ServiceA]
    E0 --> E0T26[TestAssembly.Nested+Validator]
    E0 --> E0T27[TestAssembly.Request]
    E0 --> E0T28[TestAssembly.RequestHandler]
    E0 --> E0T29[TestAssembly.Response]
    E0 --> E0T30[TestAssembly.Service]
    E0 --> E0T31[TestAssembly.ServiceB]
    E0 --> E0T32[Program]
";
}
#nullable restore
