//HintName: Specular.Analyzers/Specular.Analyzers.SpecularProviderGenerator/SpecularProvider.g.cs
#nullable enable
#pragma warning disable CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Specular;
using Specular.Abstractions;
using System.Runtime.Loader;

[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.GetAssemblies","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.GetAssemblies","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.GetAssemblies","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.GetAssemblies","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.GetAssemblies","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.GetAssemblies","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: Specular.Abstractions.SpecularHashAttribute("{scrubbed}")]
[System.CodeDom.Compiler.GeneratedCode("Specular.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal sealed class SpecularProvider : ISpecularProvider
{
    public static ISpecularProvider Instance { get; } = new SpecularProvider();

    private SpecularProvider()
    {
    }

    IEnumerable<Assembly> ISpecularProvider.GetAssemblies(Action<IReflectionAssemblySelector> action, int lineNumber, string filePath, string argumentExpression)
    {
        var items = new List<Assembly>();
        switch (lineNumber)
        {
            // FilePath: Input0.cs Expression: 8IGLe4J/MYHkM0PrcVVuwg==
            case 16:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: 8IGLe4J/MYHkM0PrcVVuwg==
                    case "Input0.cs":
                        items.Add(OtherProject);
                        items.Add(typeof(global::Specular.ISpecularProvider).Assembly);
                        items.Add(typeof(global::System.IServiceProvider).Assembly);
                        items.Add(typeof(global::TestAssembly.IService).Assembly);
                        items.Add(TestProject);
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: Z9rFHKTvVtL4pjKVS65ieA==
            case 17:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: Z9rFHKTvVtL4pjKVS65ieA==
                    case "Input0.cs":
                        items.Add(OtherProject);
                        items.Add(typeof(global::Specular.ISpecularProvider).Assembly);
                        items.Add(typeof(global::System.IServiceProvider).Assembly);
                        items.Add(typeof(global::TestAssembly.IService).Assembly);
                        items.Add(TestProject);
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: EroujZtERsWBDGh52iQ5LQ==
            case 18:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: EroujZtERsWBDGh52iQ5LQ==
                    case "Input0.cs":
                        items.Add(OtherProject);
                        items.Add(typeof(global::Specular.ISpecularProvider).Assembly);
                        items.Add(typeof(global::System.IServiceProvider).Assembly);
                        items.Add(typeof(global::TestAssembly.IService).Assembly);
                        items.Add(TestProject);
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: /wvNd0rNqfzrIH57jso7mA==
            case 19:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: /wvNd0rNqfzrIH57jso7mA==
                    case "Input0.cs":
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: ApaVlALM8kzVxm/WHvukVQ==
            case 20:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: ApaVlALM8kzVxm/WHvukVQ==
                    case "Input0.cs":
                        items.Add(OtherProject);
                        items.Add(TestProject);
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: y1nZVGCnh7EvCDUbXmZVJg==
            case 21:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: y1nZVGCnh7EvCDUbXmZVJg==
                    case "Input0.cs":
                        items.Add(OtherProject);
                        items.Add(TestProject);
                        break;
                }

                break;
        }

        return items;
    }

    [global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode", Justification = "Types resolved by string literal are preserved with their public constructors via [DynamicDependency], so this reflection is trim- and AOT-safe."), global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2072:DynamicallyAccessedMembers", Justification = "Types resolved by string literal are preserved with their public constructors via [DynamicDependency], so this reflection is trim- and AOT-safe.")]
    IEnumerable<Type> ISpecularProvider.GetTypes(Func<IReflectionTypeSelector, IEnumerable<Type>> selector, int lineNumber, string filePath, string argumentExpression)
    {
        var items = new List<Type>();
        switch (lineNumber)
        {
            // FilePath: Input0.cs Expression: 4VaAjHpBsAH5j0EYKpAqQw==
            case 42:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: 4VaAjHpBsAH5j0EYKpAqQw==
                    case "Input0.cs":
                        items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                        items.Add(typeof(global::Program));
                        items.Add(OtherProject.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                        items.Add(OtherProject.GetType("Program")!);
                        items.Add(Specular.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                        items.Add(typeof(global::Specular.Abstractions.ExcludeFromSpecularAttribute));
                        items.Add(typeof(global::Specular.Abstractions.SpecularHashAttribute));
                        items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ScanResults));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorAssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanAssemblyReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReportEntry));
                        items.Add(typeof(global::Specular.RegistrationLifetimeAttribute));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<>));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<, >));
                        items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                        items.Add(typeof(global::Specular.SpecularSupport));
                        items.Add(TestAssembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                        items.Add(TestAssembly.GetType("TestAssembly.GenericService")!);
                        items.Add(typeof(global::TestAssembly.GenericServiceB));
                        items.Add(typeof(global::TestAssembly.Nested));
                        items.Add(typeof(global::TestAssembly.Nested.GenericServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!);
                        items.Add(typeof(global::TestAssembly.Nested.ServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+Validator")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Request")!);
                        items.Add(TestAssembly.GetType("TestAssembly.RequestHandler")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Response")!);
                        items.Add(typeof(global::TestAssembly.Service));
                        items.Add(TestAssembly.GetType("TestAssembly.ServiceB")!);
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: PCRHvamF6J3xug7jJLIREw==
            case 82:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: PCRHvamF6J3xug7jJLIREw==
                    case "Input0.cs":
                        items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                        items.Add(OtherProject.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                        items.Add(Specular.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                        items.Add(typeof(global::Specular.Abstractions.SpecularHashAttribute));
                        items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ScanResults));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorAssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanAssemblyReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReportEntry));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<>));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<, >));
                        items.Add(TestAssembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: fsFGHt8fY/Soe/yfBavpPA==
            case 102:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: fsFGHt8fY/Soe/yfBavpPA==
                    case "Input0.cs":
                        items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                        items.Add(typeof(global::Program));
                        items.Add(OtherProject.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                        items.Add(OtherProject.GetType("Program")!);
                        items.Add(Specular.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                        items.Add(typeof(global::Specular.Abstractions.ExcludeFromSpecularAttribute));
                        items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                        items.Add(typeof(global::Specular.Abstractions.SpecularHashAttribute));
                        items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ScanResults));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorAssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanAssemblyReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReportEntry));
                        items.Add(typeof(global::Specular.RegistrationLifetimeAttribute));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<>));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<, >));
                        items.Add(TestAssembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                        items.Add(TestAssembly.GetType("TestAssembly.GenericService")!);
                        items.Add(typeof(global::TestAssembly.GenericServiceB));
                        items.Add(typeof(global::TestAssembly.IGenericService<>));
                        items.Add(typeof(global::TestAssembly.IOther));
                        items.Add(typeof(global::TestAssembly.IRequest<>));
                        items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                        items.Add(typeof(global::TestAssembly.IService));
                        items.Add(typeof(global::TestAssembly.IServiceB));
                        items.Add(typeof(global::TestAssembly.IValidator));
                        items.Add(typeof(global::TestAssembly.IValidator<>));
                        items.Add(typeof(global::TestAssembly.Nested.GenericServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!);
                        items.Add(typeof(global::TestAssembly.Nested.ServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+Validator")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Request")!);
                        items.Add(TestAssembly.GetType("TestAssembly.RequestHandler")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Response")!);
                        items.Add(typeof(global::TestAssembly.Service));
                        items.Add(TestAssembly.GetType("TestAssembly.ServiceB")!);
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: /W5+1QbQLNSoqBDFr/pvdA==
            case 122:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: /W5+1QbQLNSoqBDFr/pvdA==
                    case "Input0.cs":
                        items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                        items.Add(typeof(global::Program));
                        items.Add(OtherProject.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                        items.Add(OtherProject.GetType("Program")!);
                        items.Add(Specular.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                        items.Add(typeof(global::Specular.Abstractions.ExcludeFromSpecularAttribute));
                        items.Add(typeof(global::Specular.Abstractions.SpecularHashAttribute));
                        items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ScanResults));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorAssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanAssemblyReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReportEntry));
                        items.Add(typeof(global::Specular.RegistrationLifetimeAttribute));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<>));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<, >));
                        items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                        items.Add(typeof(global::Specular.SpecularSupport));
                        items.Add(TestAssembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                        items.Add(TestAssembly.GetType("TestAssembly.GenericService")!);
                        items.Add(typeof(global::TestAssembly.GenericServiceB));
                        items.Add(typeof(global::TestAssembly.Nested));
                        items.Add(typeof(global::TestAssembly.Nested.GenericServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!);
                        items.Add(typeof(global::TestAssembly.Nested.ServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+Validator")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Request")!);
                        items.Add(TestAssembly.GetType("TestAssembly.RequestHandler")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Response")!);
                        items.Add(typeof(global::TestAssembly.Service));
                        items.Add(TestAssembly.GetType("TestAssembly.ServiceB")!);
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: 5gXgnlFcQRYQSxrJGJuZeA==
            case 132:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: 5gXgnlFcQRYQSxrJGJuZeA==
                    case "Input0.cs":
                        items.Add(typeof(global::Program));
                        items.Add(OtherProject.GetType("Program")!);
                        items.Add(TestAssembly.GetType("TestAssembly.GenericService")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+Validator")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Request")!);
                        items.Add(TestAssembly.GetType("TestAssembly.RequestHandler")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Response")!);
                        items.Add(TestAssembly.GetType("TestAssembly.ServiceB")!);
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: s3+YB+Nc72MZzZXRrB9ZBQ==
            case 147:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: s3+YB+Nc72MZzZXRrB9ZBQ==
                    case "Input0.cs":
                        items.Add(typeof(global::Program));
                        items.Add(OtherProject.GetType("Program")!);
                        items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ScanResults));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorAssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanAssemblyReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReportEntry));
                        items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                        items.Add(typeof(global::Specular.SpecularSupport));
                        items.Add(TestAssembly.GetType("TestAssembly.GenericService")!);
                        items.Add(typeof(global::TestAssembly.GenericServiceB));
                        items.Add(typeof(global::TestAssembly.IGenericService<>));
                        items.Add(typeof(global::TestAssembly.IOther));
                        items.Add(typeof(global::TestAssembly.IRequest<>));
                        items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                        items.Add(typeof(global::TestAssembly.IService));
                        items.Add(typeof(global::TestAssembly.IServiceB));
                        items.Add(typeof(global::TestAssembly.IValidator));
                        items.Add(typeof(global::TestAssembly.IValidator<>));
                        items.Add(typeof(global::TestAssembly.Nested));
                        items.Add(typeof(global::TestAssembly.Nested.GenericServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!);
                        items.Add(typeof(global::TestAssembly.Nested.ServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+Validator")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Request")!);
                        items.Add(TestAssembly.GetType("TestAssembly.RequestHandler")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Response")!);
                        items.Add(typeof(global::TestAssembly.Service));
                        items.Add(TestAssembly.GetType("TestAssembly.ServiceB")!);
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: yj9cWza0VGt3OBTGaxhaYg==
            case 158:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: yj9cWza0VGt3OBTGaxhaYg==
                    case "Input0.cs":
                        items.Add(typeof(global::Program));
                        items.Add(OtherProject.GetType("Program")!);
                        items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                        items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                        items.Add(typeof(global::Specular.SpecularSupport));
                        items.Add(TestAssembly.GetType("TestAssembly.GenericService")!);
                        items.Add(typeof(global::TestAssembly.GenericServiceB));
                        items.Add(typeof(global::TestAssembly.IGenericService<>));
                        items.Add(typeof(global::TestAssembly.IOther));
                        items.Add(typeof(global::TestAssembly.IRequest<>));
                        items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                        items.Add(typeof(global::TestAssembly.IService));
                        items.Add(typeof(global::TestAssembly.IServiceB));
                        items.Add(typeof(global::TestAssembly.IValidator));
                        items.Add(typeof(global::TestAssembly.IValidator<>));
                        items.Add(typeof(global::TestAssembly.Nested));
                        items.Add(typeof(global::TestAssembly.Nested.GenericServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!);
                        items.Add(typeof(global::TestAssembly.Nested.ServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+Validator")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Request")!);
                        items.Add(TestAssembly.GetType("TestAssembly.RequestHandler")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Response")!);
                        items.Add(typeof(global::TestAssembly.Service));
                        items.Add(TestAssembly.GetType("TestAssembly.ServiceB")!);
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: qOfjb0SDPwHcKNlpG9ytrQ==
            case 169:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: qOfjb0SDPwHcKNlpG9ytrQ==
                    case "Input0.cs":
                        items.Add(typeof(global::Program));
                        items.Add(OtherProject.GetType("Program")!);
                        items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ScanResults));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorAssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanAssemblyReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReportEntry));
                        items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                        items.Add(typeof(global::Specular.SpecularSupport));
                        items.Add(TestAssembly.GetType("TestAssembly.GenericService")!);
                        items.Add(typeof(global::TestAssembly.GenericServiceB));
                        items.Add(typeof(global::TestAssembly.IOther));
                        items.Add(typeof(global::TestAssembly.IService));
                        items.Add(typeof(global::TestAssembly.IServiceB));
                        items.Add(typeof(global::TestAssembly.IValidator));
                        items.Add(typeof(global::TestAssembly.Nested));
                        items.Add(typeof(global::TestAssembly.Nested.GenericServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!);
                        items.Add(typeof(global::TestAssembly.Nested.ServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+Validator")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Request")!);
                        items.Add(TestAssembly.GetType("TestAssembly.RequestHandler")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Response")!);
                        items.Add(typeof(global::TestAssembly.Service));
                        items.Add(TestAssembly.GetType("TestAssembly.ServiceB")!);
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: LS+xdTOmvs+8M+1OjvEf0g==
            case 200:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: LS+xdTOmvs+8M+1OjvEf0g==
                    case "Input0.cs":
                        items.Add(typeof(global::Program));
                        items.Add(OtherProject.GetType("Program")!);
                        items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ScanResults));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorAssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanAssemblyReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReportEntry));
                        items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                        items.Add(TestAssembly.GetType("TestAssembly.GenericService")!);
                        items.Add(typeof(global::TestAssembly.GenericServiceB));
                        items.Add(typeof(global::TestAssembly.IGenericService<>));
                        items.Add(typeof(global::TestAssembly.IOther));
                        items.Add(typeof(global::TestAssembly.IRequest<>));
                        items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                        items.Add(typeof(global::TestAssembly.IService));
                        items.Add(typeof(global::TestAssembly.IServiceB));
                        items.Add(typeof(global::TestAssembly.IValidator));
                        items.Add(typeof(global::TestAssembly.IValidator<>));
                        items.Add(typeof(global::TestAssembly.Nested));
                        items.Add(typeof(global::TestAssembly.Nested.GenericServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!);
                        items.Add(typeof(global::TestAssembly.Nested.ServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+Validator")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Request")!);
                        items.Add(TestAssembly.GetType("TestAssembly.RequestHandler")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Response")!);
                        items.Add(typeof(global::TestAssembly.Service));
                        items.Add(TestAssembly.GetType("TestAssembly.ServiceB")!);
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: dODyg69BE0Nso/u2dEJZBw==
            case 211:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: dODyg69BE0Nso/u2dEJZBw==
                    case "Input0.cs":
                        items.Add(typeof(global::Program));
                        items.Add(OtherProject.GetType("Program")!);
                        items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ScanResults));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorAssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanAssemblyReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReportEntry));
                        items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                        items.Add(TestAssembly.GetType("TestAssembly.GenericService")!);
                        items.Add(typeof(global::TestAssembly.GenericServiceB));
                        items.Add(typeof(global::TestAssembly.IGenericService<>));
                        items.Add(typeof(global::TestAssembly.IOther));
                        items.Add(typeof(global::TestAssembly.IRequest<>));
                        items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                        items.Add(typeof(global::TestAssembly.IService));
                        items.Add(typeof(global::TestAssembly.IServiceB));
                        items.Add(typeof(global::TestAssembly.IValidator));
                        items.Add(typeof(global::TestAssembly.IValidator<>));
                        items.Add(typeof(global::TestAssembly.Nested));
                        items.Add(typeof(global::TestAssembly.Nested.GenericServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!);
                        items.Add(typeof(global::TestAssembly.Nested.ServiceA));
                        items.Add(TestAssembly.GetType("TestAssembly.Nested+Validator")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Request")!);
                        items.Add(TestAssembly.GetType("TestAssembly.RequestHandler")!);
                        items.Add(TestAssembly.GetType("TestAssembly.Response")!);
                        items.Add(typeof(global::TestAssembly.Service));
                        items.Add(TestAssembly.GetType("TestAssembly.ServiceB")!);
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: cizxMQ3lnxFjX801wlpqbw==
            case 22:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: cizxMQ3lnxFjX801wlpqbw==
                    case "Input0.cs":
                        items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                        items.Add(typeof(global::TestAssembly.IGenericService<>));
                        items.Add(typeof(global::TestAssembly.IOther));
                        items.Add(typeof(global::TestAssembly.IRequest<>));
                        items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                        items.Add(typeof(global::TestAssembly.IService));
                        items.Add(typeof(global::TestAssembly.IServiceB));
                        items.Add(typeof(global::TestAssembly.IValidator));
                        items.Add(typeof(global::TestAssembly.IValidator<>));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: l4cim5mnIPk4MmmE29qzUw==
            case 32:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: l4cim5mnIPk4MmmE29qzUw==
                    case "Input0.cs":
                        items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                        items.Add(typeof(global::TestAssembly.IGenericService<>));
                        items.Add(typeof(global::TestAssembly.IOther));
                        items.Add(typeof(global::TestAssembly.IRequest<>));
                        items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                        items.Add(typeof(global::TestAssembly.IService));
                        items.Add(typeof(global::TestAssembly.IServiceB));
                        items.Add(typeof(global::TestAssembly.IValidator));
                        items.Add(typeof(global::TestAssembly.IValidator<>));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: 9IUdfQbcsMAYS17mByAMdQ==
            case 52:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: 9IUdfQbcsMAYS17mByAMdQ==
                    case "Input0.cs":
                        items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                        items.Add(typeof(global::TestAssembly.IGenericService<>));
                        items.Add(typeof(global::TestAssembly.IOther));
                        items.Add(typeof(global::TestAssembly.IRequest<>));
                        items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                        items.Add(typeof(global::TestAssembly.IService));
                        items.Add(typeof(global::TestAssembly.IServiceB));
                        items.Add(typeof(global::TestAssembly.IValidator));
                        items.Add(typeof(global::TestAssembly.IValidator<>));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: 8EyoKKcMOOrD7HWlzslgwg==
            case 62:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: 8EyoKKcMOOrD7HWlzslgwg==
                    case "Input0.cs":
                        items.Add(typeof(global::Specular.Abstractions.ExcludeFromSpecularAttribute));
                        items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                        items.Add(typeof(global::Specular.Abstractions.SpecularHashAttribute));
                        items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ScanResults));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorAssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanAssemblyReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReportEntry));
                        items.Add(typeof(global::Specular.RegistrationLifetimeAttribute));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<>));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<, >));
                        items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                        items.Add(typeof(global::Specular.SpecularSupport));
                        items.Add(typeof(global::TestAssembly.GenericServiceB));
                        items.Add(typeof(global::TestAssembly.IGenericService<>));
                        items.Add(typeof(global::TestAssembly.IOther));
                        items.Add(typeof(global::TestAssembly.IRequest<>));
                        items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                        items.Add(typeof(global::TestAssembly.IService));
                        items.Add(typeof(global::TestAssembly.IServiceB));
                        items.Add(typeof(global::TestAssembly.IValidator));
                        items.Add(typeof(global::TestAssembly.IValidator<>));
                        items.Add(typeof(global::TestAssembly.Nested));
                        items.Add(typeof(global::TestAssembly.Nested.GenericServiceA));
                        items.Add(typeof(global::TestAssembly.Nested.ServiceA));
                        items.Add(typeof(global::TestAssembly.Service));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: 0fBeWD2wBlWIjLnT36cIow==
            case 72:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: 0fBeWD2wBlWIjLnT36cIow==
                    case "Input0.cs":
                        items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: +FkT4btNbS4PRfcftoFbtA==
            case 92:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: +FkT4btNbS4PRfcftoFbtA==
                    case "Input0.cs":
                        items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                        items.Add(typeof(global::Specular.SpecularSupport));
                        items.Add(typeof(global::TestAssembly.Nested));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: /Q4AySzV41Tdwlkpfg/GKA==
            case 112:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: /Q4AySzV41Tdwlkpfg/GKA==
                    case "Input0.cs":
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<>));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<, >));
                        items.Add(typeof(global::TestAssembly.IGenericService<>));
                        items.Add(typeof(global::TestAssembly.IRequest<>));
                        items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                        items.Add(typeof(global::TestAssembly.IValidator<>));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: Edc9cxDaKw/uRcTfQ0BxWQ==
            case 180:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: Edc9cxDaKw/uRcTfQ0BxWQ==
                    case "Input0.cs":
                        items.Add(typeof(global::Specular.SpecularSupport));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: cNBTr21Y/0DD1+cR1se7TQ==
            case 190:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: cNBTr21Y/0DD1+cR1se7TQ==
                    case "Input0.cs":
                        items.Add(typeof(global::Specular.SpecularSupport));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: btOVlu+Naw13LAV9tKR6WA==
            case 252:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: btOVlu+Naw13LAV9tKR6WA==
                    case "Input0.cs":
                        items.Add(typeof(global::Specular.Abstractions.ExcludeFromSpecularAttribute));
                        items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                        items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                        items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                        items.Add(typeof(global::Specular.Abstractions.SpecularHashAttribute));
                        items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                        items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.AssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ScanResults));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorAssemblyScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.ServiceDescriptorScanReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanAssemblyReportEntry));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReport));
                        items.Add(typeof(global::Specular.Diagnostics.TypeScanReportEntry));
                        items.Add(typeof(global::Specular.RegistrationLifetimeAttribute));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<>));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,, >));
                        items.Add(typeof(global::Specular.ServiceRegistrationAttribute<, >));
                        items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                        items.Add(typeof(global::Specular.SpecularSupport));
                        items.Add(typeof(global::TestAssembly.GenericServiceB));
                        items.Add(typeof(global::TestAssembly.IGenericService<>));
                        items.Add(typeof(global::TestAssembly.IOther));
                        items.Add(typeof(global::TestAssembly.IRequest<>));
                        items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                        items.Add(typeof(global::TestAssembly.IService));
                        items.Add(typeof(global::TestAssembly.IServiceB));
                        items.Add(typeof(global::TestAssembly.IValidator));
                        items.Add(typeof(global::TestAssembly.IValidator<>));
                        items.Add(typeof(global::TestAssembly.Nested));
                        items.Add(typeof(global::TestAssembly.Nested.GenericServiceA));
                        items.Add(typeof(global::TestAssembly.Nested.ServiceA));
                        items.Add(typeof(global::TestAssembly.Service));
                        break;
                }

                break;
        }

        return items;
    }

    [global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode", Justification = "Types resolved by string literal are preserved with their public constructors via [DynamicDependency], so this reflection is trim- and AOT-safe."), global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2072:DynamicallyAccessedMembers", Justification = "Types resolved by string literal are preserved with their public constructors via [DynamicDependency], so this reflection is trim- and AOT-safe.")]
    Microsoft.Extensions.DependencyInjection.IServiceCollection ISpecularProvider.Scan(Microsoft.Extensions.DependencyInjection.IServiceCollection services, Action<IServiceDescriptorAssemblySelector> selector, int lineNumber, string filePath, string argumentExpression)
    {
        switch (lineNumber)
        {
            // FilePath: Input0.cs Expression: lk0ALym+V2W6xQDXO/AkYw==
            case 262:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: lk0ALym+V2W6xQDXO/AkYw==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: CSc+6hElAjxc8UqHDR2nGg==
            case 268:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: CSc+6hElAjxc8UqHDR2nGg==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                        services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                        services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Service, global::TestAssembly.Service>());
                        services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        services.Add(ServiceDescriptor.Singleton(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                        services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: fKfq36ZCDNfEou+qyyLEbw==
            case 274:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: fKfq36ZCDNfEou+qyyLEbw==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService, global::TestAssembly.Nested.ServiceA>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService, global::TestAssembly.Service>());
                        services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IService), TestAssembly.GetType("TestAssembly.ServiceB")!));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: RF0U3ve7NmkBVhOpOoW7bQ==
            case 279:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: RF0U3ve7NmkBVhOpOoW7bQ==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                        services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                        services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Service, global::TestAssembly.Service>());
                        services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        services.Add(ServiceDescriptor.Singleton(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                        services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: H/jKai1h1B6jnFlFcyi7Tg==
            case 285:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: H/jKai1h1B6jnFlFcyi7Tg==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: Cig2XJLApDe8z9wMh/IDUA==
            case 291:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: Cig2XJLApDe8z9wMh/IDUA==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => (global::TestAssembly.IGenericService<global::System.String>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => (global::TestAssembly.IOther)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: GC4f7Eu/9wIch8wpSP2wyQ==
            case 296:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: GC4f7Eu/9wIch8wpSP2wyQ==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => (global::TestAssembly.IGenericService<global::System.String>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => (global::TestAssembly.IOther)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: a8a5Us+LBXHvnbZedXasXw==
            case 302:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: a8a5Us+LBXHvnbZedXasXw==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Singleton(typeof(global::TestAssembly.IRequestHandler<, >).MakeGenericType(TestAssembly.GetType("TestAssembly.Request")!, TestAssembly.GetType("TestAssembly.Response")!)!, TestAssembly.GetType("TestAssembly.RequestHandler")!));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: xbYAbKX/QnnClXoKl6q/Aw==
            case 307:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: xbYAbKX/QnnClXoKl6q/Aw==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: e8KAWIaBqxCKvrQ5PGf0qg==
            case 313:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: e8KAWIaBqxCKvrQ5PGf0qg==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: QR7ilMUHdBOqlpGLxKKLDQ==
            case 319:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: QR7ilMUHdBOqlpGLxKKLDQ==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: nNzkffztFbvyPeXlZPkOkQ==
            case 325:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: nNzkffztFbvyPeXlZPkOkQ==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => (global::TestAssembly.IGenericService<global::System.String>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: HH9aVmBgFQ5bSAugUwbZsw==
            case 331:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: HH9aVmBgFQ5bSAugUwbZsw==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IValidator<>).MakeGenericType(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!)!, TestAssembly.GetType("TestAssembly.Nested+Validator")!));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: nGdrAptlVCEX9H6kHx0auw==
            case 336:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: nGdrAptlVCEX9H6kHx0auw==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService, global::TestAssembly.Nested.ServiceA>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService, global::TestAssembly.Service>());
                        services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IService), TestAssembly.GetType("TestAssembly.ServiceB")!));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: iOVNY6dfLzra3vCDdW4AxA==
            case 341:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: iOVNY6dfLzra3vCDdW4AxA==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: jm/nkIDlrjpFn5pT15jCnw==
            case 347:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: jm/nkIDlrjpFn5pT15jCnw==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IGenericService<global::System.Int32>), TestAssembly.GetType("TestAssembly.GenericService")!));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>, global::TestAssembly.GenericServiceB>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>, global::TestAssembly.Nested.GenericServiceA>());
                        break;
                }

                break;
            // FilePath: Input0.cs Expression: MeX+mF+ej5803TONO0SaMw==
            case 366:
                switch (System.IO.Path.GetFileName(filePath))
                {
                    // FilePath: Input0.cs Expression: MeX+mF+ej5803TONO0SaMw==
                    case "Input0.cs":
                        services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => (global::TestAssembly.IGenericService<global::System.String>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => (global::TestAssembly.IOther)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                        services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!, TestAssembly.GetType("TestAssembly.Nested+MyRecord")!));
                        services.Add(ServiceDescriptor.Scoped(typeof(global::System.IEquatable<>).MakeGenericType(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!)!, a => a.GetRequiredService(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!)));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                        services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.Nested+Validator")!, TestAssembly.GetType("TestAssembly.Nested+Validator")!));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IValidator>(a => (global::TestAssembly.IValidator)a.GetRequiredService(TestAssembly.GetType("TestAssembly.Nested+Validator")!)));
                        services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IValidator<>).MakeGenericType(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!)!, a => a.GetRequiredService(TestAssembly.GetType("TestAssembly.Nested+Validator")!)));
                        services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.Request")!, TestAssembly.GetType("TestAssembly.Request")!));
                        services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IRequest<>).MakeGenericType(TestAssembly.GetType("TestAssembly.Response")!)!, a => a.GetRequiredService(TestAssembly.GetType("TestAssembly.Request")!)));
                        services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.RequestHandler")!, TestAssembly.GetType("TestAssembly.RequestHandler")!));
                        services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IRequestHandler<, >).MakeGenericType(TestAssembly.GetType("TestAssembly.Request")!, TestAssembly.GetType("TestAssembly.Response")!)!, a => a.GetRequiredService(TestAssembly.GetType("TestAssembly.RequestHandler")!)));
                        services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.Response")!, TestAssembly.GetType("TestAssembly.Response")!));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                        services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                        services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                        break;
                }

                break;
        }

        return services;
    }

    [global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Microsoft.CodeAnalysis.EmbeddedAttribute", "OtherProject"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Program", "OtherProject"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Microsoft.CodeAnalysis.EmbeddedAttribute", "Specular"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Microsoft.CodeAnalysis.EmbeddedAttribute", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.GenericService", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Nested+MyRecord", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Nested+Validator", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Request", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.RequestHandler", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Response", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.ServiceB", "TestAssembly")]
    private AssemblyLoadContext _context = AssemblyLoadContext.GetLoadContext(typeof(SpecularProvider).Assembly)!;
    private Assembly _OtherProject;
    private Assembly OtherProject => _OtherProject ??= _context.LoadFromAssemblyName(new AssemblyName("OtherProject, Version=version, Culture=neutral, PublicKeyToken=null"));

    private Assembly _Specular;
    private Assembly Specular => _Specular ??= _context.LoadFromAssemblyName(new AssemblyName("Specular, Version=version, Culture=neutral, PublicKeyToken=null"));

    private Assembly _TestAssembly;
    private Assembly TestAssembly => _TestAssembly ??= _context.LoadFromAssemblyName(new AssemblyName("TestAssembly, Version=version, Culture=neutral, PublicKeyToken=null"));

    private Assembly _TestProject;
    private Assembly TestProject => _TestProject ??= _context.LoadFromAssemblyName(new AssemblyName("TestProject, Version=version, Culture=neutral, PublicKeyToken=null"));
}
#pragma warning restore CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
#nullable restore
