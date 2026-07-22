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
                items.Add(typeof(global::Specular.ISpecularProvider).Assembly);
                items.Add(typeof(global::System.Collections.Frozen.FrozenDictionary).Assembly);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(typeof(global::System.Runtime.Serialization.SerializationEventHandler).Assembly);
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                break;
            // FilePath: Input0.cs Expression: cHMQikM7PE1XjxkGcj0esw==
            case 17:
                items.Add(mscorlib);
                items.Add(netstandard);
                items.Add(typeof(global::Specular.ISpecularProvider).Assembly);
                items.Add(System);
                items.Add(typeof(global::System.Collections.Frozen.FrozenDictionary).Assembly);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(SystemCore);
                items.Add(SystemPrivateCoreLib);
                items.Add(SystemRuntime);
                items.Add(typeof(global::System.Runtime.Serialization.SerializationEventHandler).Assembly);
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                break;
            // FilePath: Input0.cs Expression: Z9rFHKTvVtL4pjKVS65ieA==
            case 18:
                items.Add(typeof(global::System.Collections.Frozen.FrozenDictionary).Assembly);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(typeof(global::System.Runtime.Serialization.SerializationEventHandler).Assembly);
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                break;
            // FilePath: Input0.cs Expression: EroujZtERsWBDGh52iQ5LQ==
            case 19:
                items.Add(typeof(global::Specular.ISpecularProvider).Assembly);
                items.Add(typeof(global::System.Collections.Frozen.FrozenDictionary).Assembly);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(typeof(global::System.Runtime.Serialization.SerializationEventHandler).Assembly);
                items.Add(TestProject);
                break;
            // FilePath: Input0.cs Expression: /wvNd0rNqfzrIH57jso7mA==
            case 20:
                items.Add(TestProject);
                break;
            // FilePath: Input0.cs Expression: ApaVlALM8kzVxm/WHvukVQ==
            case 21:
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                break;
            // FilePath: Input0.cs Expression: y1nZVGCnh7EvCDUbXmZVJg==
            case 22:
                items.Add(TestProject);
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
            case 43:
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
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
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(typeof(global::Specular.SpecularSupport));
                items.Add(Specular.GetType("SpecularScanReport")!);
                items.Add(SystemCollectionsImmutable.GetType("FxResources.System.Collections.Immutable.SR")!);
                items.Add(SystemRuntimeSerializationFormatters.GetType("FxResources.System.Runtime.Serialization.Formatters.SR")!);
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
            // FilePath: Input0.cs Expression: PCRHvamF6J3xug7jJLIREw==
            case 83:
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(TestAssembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                break;
            // FilePath: Input0.cs Expression: fsFGHt8fY/Soe/yfBavpPA==
            case 103:
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
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
            // FilePath: Input0.cs Expression: /W5+1QbQLNSoqBDFr/pvdA==
            case 123:
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(SystemCollectionsImmutable.GetType("FxResources.System.Collections.Immutable.SR")!);
                items.Add(SystemRuntimeSerializationFormatters.GetType("FxResources.System.Runtime.Serialization.Formatters.SR")!);
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
            // FilePath: Input0.cs Expression: 5gXgnlFcQRYQSxrJGJuZeA==
            case 133:
                items.Add(typeof(global::Program));
                items.Add(SystemCollectionsImmutable.GetType("FxResources.System.Collections.Immutable.SR")!);
                items.Add(SystemRuntimeSerializationFormatters.GetType("FxResources.System.Runtime.Serialization.Formatters.SR")!);
                items.Add(TestAssembly.GetType("TestAssembly.GenericService")!);
                items.Add(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!);
                items.Add(TestAssembly.GetType("TestAssembly.Nested+Validator")!);
                items.Add(TestAssembly.GetType("TestAssembly.Request")!);
                items.Add(TestAssembly.GetType("TestAssembly.RequestHandler")!);
                items.Add(TestAssembly.GetType("TestAssembly.Response")!);
                items.Add(TestAssembly.GetType("TestAssembly.ServiceB")!);
                break;
            // FilePath: Input0.cs Expression: s3+YB+Nc72MZzZXRrB9ZBQ==
            case 148:
                items.Add(typeof(global::Program));
                items.Add(SystemCollectionsImmutable.GetType("FxResources.System.Collections.Immutable.SR")!);
                items.Add(SystemRuntimeSerializationFormatters.GetType("FxResources.System.Runtime.Serialization.Formatters.SR")!);
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
            // FilePath: Input0.cs Expression: yj9cWza0VGt3OBTGaxhaYg==
            case 159:
                items.Add(typeof(global::Program));
                items.Add(SystemCollectionsImmutable.GetType("FxResources.System.Collections.Immutable.SR")!);
                items.Add(SystemRuntimeSerializationFormatters.GetType("FxResources.System.Runtime.Serialization.Formatters.SR")!);
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
            // FilePath: Input0.cs Expression: qOfjb0SDPwHcKNlpG9ytrQ==
            case 170:
                items.Add(typeof(global::Program));
                items.Add(SystemCollectionsImmutable.GetType("FxResources.System.Collections.Immutable.SR")!);
                items.Add(SystemRuntimeSerializationFormatters.GetType("FxResources.System.Runtime.Serialization.Formatters.SR")!);
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
            // FilePath: Input0.cs Expression: LS+xdTOmvs+8M+1OjvEf0g==
            case 199:
                items.Add(typeof(global::Program));
                items.Add(SystemCollectionsImmutable.GetType("FxResources.System.Collections.Immutable.SR")!);
                items.Add(SystemRuntimeSerializationFormatters.GetType("FxResources.System.Runtime.Serialization.Formatters.SR")!);
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
            // FilePath: Input0.cs Expression: dODyg69BE0Nso/u2dEJZBw==
            case 210:
                items.Add(typeof(global::Program));
                items.Add(SystemCollectionsImmutable.GetType("FxResources.System.Collections.Immutable.SR")!);
                items.Add(SystemRuntimeSerializationFormatters.GetType("FxResources.System.Runtime.Serialization.Formatters.SR")!);
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
            // FilePath: Input0.cs Expression: 2MpNjixlIXLOLz3efZPnFg==
            case 181:
                items.Add(typeof(global::Specular.SpecularSupport));
                break;
            // FilePath: Input0.cs Expression: EDKAc3dy9Cl22PwVmcqWKw==
            case 190:
                items.Add(typeof(global::Specular.SpecularSupport));
                break;
            // FilePath: Input0.cs Expression: +FkT4btNbS4PRfcftoFbtA==
            case 93:
                items.Add(SystemCollectionsImmutable.GetType("FxResources.System.Collections.Immutable.SR")!);
                items.Add(SystemRuntimeSerializationFormatters.GetType("FxResources.System.Runtime.Serialization.Formatters.SR")!);
                items.Add(typeof(global::TestAssembly.Nested));
                break;
            // FilePath: Input0.cs Expression: cizxMQ3lnxFjX801wlpqbw==
            case 23:
                items.Add(typeof(global::TestAssembly.IGenericService<>));
                items.Add(typeof(global::TestAssembly.IOther));
                items.Add(typeof(global::TestAssembly.IRequest<>));
                items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                items.Add(typeof(global::TestAssembly.IService));
                items.Add(typeof(global::TestAssembly.IServiceB));
                items.Add(typeof(global::TestAssembly.IValidator));
                items.Add(typeof(global::TestAssembly.IValidator<>));
                break;
            // FilePath: Input0.cs Expression: l4cim5mnIPk4MmmE29qzUw==
            case 33:
                items.Add(typeof(global::TestAssembly.IGenericService<>));
                items.Add(typeof(global::TestAssembly.IOther));
                items.Add(typeof(global::TestAssembly.IRequest<>));
                items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                items.Add(typeof(global::TestAssembly.IService));
                items.Add(typeof(global::TestAssembly.IServiceB));
                items.Add(typeof(global::TestAssembly.IValidator));
                items.Add(typeof(global::TestAssembly.IValidator<>));
                break;
            // FilePath: Input0.cs Expression: 9IUdfQbcsMAYS17mByAMdQ==
            case 53:
                items.Add(typeof(global::TestAssembly.IGenericService<>));
                items.Add(typeof(global::TestAssembly.IOther));
                items.Add(typeof(global::TestAssembly.IRequest<>));
                items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                items.Add(typeof(global::TestAssembly.IService));
                items.Add(typeof(global::TestAssembly.IServiceB));
                items.Add(typeof(global::TestAssembly.IValidator));
                items.Add(typeof(global::TestAssembly.IValidator<>));
                break;
            // FilePath: Input0.cs Expression: 8EyoKKcMOOrD7HWlzslgwg==
            case 63:
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
            // FilePath: Input0.cs Expression: /Q4AySzV41Tdwlkpfg/GKA==
            case 113:
                items.Add(typeof(global::TestAssembly.IGenericService<>));
                items.Add(typeof(global::TestAssembly.IRequest<>));
                items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                items.Add(typeof(global::TestAssembly.IValidator<>));
                break;
            // FilePath: Input0.cs Expression: wKWjmvTVoUad3a7CtkcMoA==
            case 144:
                items.Add(TestAssembly.GetType("TestAssembly.GenericService")!);
                items.Add(typeof(global::TestAssembly.GenericServiceB));
                items.Add(TestAssembly.GetType("TestAssembly.RequestHandler")!);
                break;
            // FilePath: Input0.cs Expression: btOVlu+Naw13LAV9tKR6WA==
            case 251:
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

        return items;
    }

    [global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode", Justification = "Types resolved by string literal are preserved with their public constructors via [DynamicDependency], so this reflection is trim- and AOT-safe."), global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2072:DynamicallyAccessedMembers", Justification = "Types resolved by string literal are preserved with their public constructors via [DynamicDependency], so this reflection is trim- and AOT-safe.")]
    Microsoft.Extensions.DependencyInjection.IServiceCollection ISpecularProvider.Scan(Microsoft.Extensions.DependencyInjection.IServiceCollection services, Action<IServiceDescriptorAssemblySelector> selector, int lineNumber, string filePath, string argumentExpression)
    {
        switch (lineNumber)
        {
            // FilePath: Input0.cs Expression: lk0ALym+V2W6xQDXO/AkYw==
            case 261:
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                break;
            // FilePath: Input0.cs Expression: CSc+6hElAjxc8UqHDR2nGg==
            case 267:
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Singleton(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                break;
            // FilePath: Input0.cs Expression: fKfq36ZCDNfEou+qyyLEbw==
            case 273:
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IService), TestAssembly.GetType("TestAssembly.ServiceB")!));
                break;
            // FilePath: Input0.cs Expression: RF0U3ve7NmkBVhOpOoW7bQ==
            case 278:
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Singleton(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                break;
            // FilePath: Input0.cs Expression: H/jKai1h1B6jnFlFcyi7Tg==
            case 284:
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                break;
            // FilePath: Input0.cs Expression: Cig2XJLApDe8z9wMh/IDUA==
            case 290:
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => (global::TestAssembly.IOther)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => (global::TestAssembly.IGenericService<global::System.String>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                break;
            // FilePath: Input0.cs Expression: GC4f7Eu/9wIch8wpSP2wyQ==
            case 295:
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => (global::TestAssembly.IOther)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => (global::TestAssembly.IGenericService<global::System.String>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                break;
            // FilePath: Input0.cs Expression: a8a5Us+LBXHvnbZedXasXw==
            case 301:
                services.Add(ServiceDescriptor.Singleton(TestAssembly.GetType("TestAssembly.RequestHandler")!, TestAssembly.GetType("TestAssembly.RequestHandler")!));
                services.Add(ServiceDescriptor.Singleton(typeof(global::TestAssembly.IRequestHandler<, >).MakeGenericType(TestAssembly.GetType("TestAssembly.Request")!, TestAssembly.GetType("TestAssembly.Response")!)!, a => a.GetRequiredService(TestAssembly.GetType("TestAssembly.RequestHandler")!)));
                break;
            // FilePath: Input0.cs Expression: xbYAbKX/QnnClXoKl6q/Aw==
            case 306:
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                break;
            // FilePath: Input0.cs Expression: e8KAWIaBqxCKvrQ5PGf0qg==
            case 312:
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                break;
            // FilePath: Input0.cs Expression: QR7ilMUHdBOqlpGLxKKLDQ==
            case 318:
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                break;
            // FilePath: Input0.cs Expression: nNzkffztFbvyPeXlZPkOkQ==
            case 324:
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => (global::TestAssembly.IOther)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => (global::TestAssembly.IGenericService<global::System.String>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                break;
            // FilePath: Input0.cs Expression: HH9aVmBgFQ5bSAugUwbZsw==
            case 330:
                services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IValidator<>).MakeGenericType(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!)!, TestAssembly.GetType("TestAssembly.Nested+Validator")!));
                break;
            // FilePath: Input0.cs Expression: nGdrAptlVCEX9H6kHx0auw==
            case 335:
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IService), TestAssembly.GetType("TestAssembly.ServiceB")!));
                break;
            // FilePath: Input0.cs Expression: iOVNY6dfLzra3vCDdW4AxA==
            case 340:
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => (global::TestAssembly.IOther)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                break;
            // FilePath: Input0.cs Expression: jm/nkIDlrjpFn5pT15jCnw==
            case 346:
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => (global::TestAssembly.IOther)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IGenericService<global::System.Int32>), TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>, global::TestAssembly.Nested.GenericServiceA>());
                break;
            // FilePath: Input0.cs Expression: +yQvLCJ7iCTxzCHEjoW5fA==
            case 351:
                services.Add(ServiceDescriptor.Singleton(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IOther>(a => (global::TestAssembly.IOther)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Singleton(TestAssembly.GetType("TestAssembly.RequestHandler")!, TestAssembly.GetType("TestAssembly.RequestHandler")!));
                services.Add(ServiceDescriptor.Singleton(typeof(global::TestAssembly.IRequestHandler<, >).MakeGenericType(TestAssembly.GetType("TestAssembly.Request")!, TestAssembly.GetType("TestAssembly.Response")!)!, a => a.GetRequiredService(TestAssembly.GetType("TestAssembly.RequestHandler")!)));
                break;
            // FilePath: Input0.cs Expression: MeX+mF+ej5803TONO0SaMw==
            case 369:
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IOther>(a => (global::TestAssembly.IOther)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => (global::TestAssembly.IGenericService<global::System.String>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
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
                services.Add(ServiceDescriptor.Singleton(TestAssembly.GetType("TestAssembly.RequestHandler")!, TestAssembly.GetType("TestAssembly.RequestHandler")!));
                services.Add(ServiceDescriptor.Singleton(typeof(global::TestAssembly.IRequestHandler<, >).MakeGenericType(TestAssembly.GetType("TestAssembly.Request")!, TestAssembly.GetType("TestAssembly.Response")!)!, a => a.GetRequiredService(TestAssembly.GetType("TestAssembly.RequestHandler")!)));
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.Response")!, TestAssembly.GetType("TestAssembly.Response")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                break;
            // FilePath: {SolutionDirectory}src/Specular/SpecularProviderServiceCollectionExtensions.cs Expression: +yQvLCJ7iCTxzCHEjoW5fA==
            case 19:
                services.Add(ServiceDescriptor.Singleton(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IOther>(a => (global::TestAssembly.IOther)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IOther>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Singleton(TestAssembly.GetType("TestAssembly.RequestHandler")!, TestAssembly.GetType("TestAssembly.RequestHandler")!));
                services.Add(ServiceDescriptor.Singleton(typeof(global::TestAssembly.IRequestHandler<, >).MakeGenericType(TestAssembly.GetType("TestAssembly.Request")!, TestAssembly.GetType("TestAssembly.Response")!)!, a => a.GetRequiredService(TestAssembly.GetType("TestAssembly.RequestHandler")!)));
                break;
        }

        return services;
    }

    [global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Microsoft.CodeAnalysis.EmbeddedAttribute", "Specular"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "SpecularScanReport", "Specular"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "FxResources.System.Collections.Immutable.SR", "System.Collections.Immutable"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "FxResources.System.Runtime.Serialization.Formatters.SR", "System.Runtime.Serialization.Formatters"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Microsoft.CodeAnalysis.EmbeddedAttribute", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.GenericService", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Nested+MyRecord", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Nested+Validator", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Request", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.RequestHandler", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Response", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.ServiceB", "TestAssembly")]
    private AssemblyLoadContext _context = AssemblyLoadContext.GetLoadContext(typeof(SpecularProvider).Assembly)!;
    private Assembly _mscorlib;
    private Assembly mscorlib => _mscorlib ??= _context.LoadFromAssemblyName(new AssemblyName("mscorlib, Version=version, Culture=neutral, PublicKey=00000000000000000400000000000000"));

    private Assembly _netstandard;
    private Assembly netstandard => _netstandard ??= _context.LoadFromAssemblyName(new AssemblyName("netstandard, Version=version, Culture=neutral, PublicKey=00240000048000009400000006020000002400005253413100040000010001004b86c4cb78549b34bab61a3b1800e23bfeb5b3ec390074041536a7e3cbd97f5f04cf0f857155a8928eaa29ebfd11cfbbad3ba70efea7bda3226c6a8d370a4cd303f714486b6ebc225985a638471e6ef571cc92a4613c00b8fa65d61ccee0cbe5f36330c9a01f4183559f1bef24cc2917c6d913e3a541333a1d05d9bed22b38cb"));

    private Assembly _Specular;
    private Assembly Specular => _Specular ??= _context.LoadFromAssemblyName(new AssemblyName("Specular, Version=version, Culture=neutral, PublicKeyToken=null"));

    private Assembly _System;
    private Assembly System => _System ??= _context.LoadFromAssemblyName(new AssemblyName("System, Version=version, Culture=neutral, PublicKey=00000000000000000400000000000000"));

    private Assembly _SystemCollectionsImmutable;
    private Assembly SystemCollectionsImmutable => _SystemCollectionsImmutable ??= _context.LoadFromAssemblyName(new AssemblyName("System.Collections.Immutable, Version=version, Culture=neutral, PublicKey=002400000480000094000000060200000024000052534131000400000100010007d1fa57c4aed9f0a32e84aa0faefd0de9e8fd6aec8f87fb03766c834c99921eb23be79ad9d5dcc1dd9ad236132102900b723cf980957fc4e177108fc607774f29e8320e92ea05ece4e821c0a5efe8f1645c4c0c93c1ab99285d622caa652c1dfad63d745d6f2de5f17e5eaf0fc4963d261c8a12436518206dc093344d5ad293"));

    private Assembly _SystemCore;
    private Assembly SystemCore => _SystemCore ??= _context.LoadFromAssemblyName(new AssemblyName("System.Core, Version=version, Culture=neutral, PublicKey=00000000000000000400000000000000"));

    private Assembly _SystemPrivateCoreLib;
    private Assembly SystemPrivateCoreLib => _SystemPrivateCoreLib ??= _context.LoadFromAssemblyName(new AssemblyName("System.Private.CoreLib, Version=version, Culture=neutral, PublicKey=00240000048000009400000006020000002400005253413100040000010001008d56c76f9e8649383049f383c44be0ec204181822a6c31cf5eb7ef486944d032188ea1d3920763712ccb12d75fb77e9811149e6148e5d32fbaab37611c1878ddc19e20ef135d0cb2cff2bfec3d115810c3d9069638fe4be215dbf795861920e5ab6f7db2e2ceef136ac23d5dd2bf031700aec232f6c6b1c785b4305c123b37ab"));

    private Assembly _SystemRuntime;
    private Assembly SystemRuntime => _SystemRuntime ??= _context.LoadFromAssemblyName(new AssemblyName("System.Runtime, Version=version, Culture=neutral, PublicKey=002400000480000094000000060200000024000052534131000400000100010007d1fa57c4aed9f0a32e84aa0faefd0de9e8fd6aec8f87fb03766c834c99921eb23be79ad9d5dcc1dd9ad236132102900b723cf980957fc4e177108fc607774f29e8320e92ea05ece4e821c0a5efe8f1645c4c0c93c1ab99285d622caa652c1dfad63d745d6f2de5f17e5eaf0fc4963d261c8a12436518206dc093344d5ad293"));

    private Assembly _SystemRuntimeSerializationFormatters;
    private Assembly SystemRuntimeSerializationFormatters => _SystemRuntimeSerializationFormatters ??= _context.LoadFromAssemblyName(new AssemblyName("System.Runtime.Serialization.Formatters, Version=version, Culture=neutral, PublicKey=002400000480000094000000060200000024000052534131000400000100010007d1fa57c4aed9f0a32e84aa0faefd0de9e8fd6aec8f87fb03766c834c99921eb23be79ad9d5dcc1dd9ad236132102900b723cf980957fc4e177108fc607774f29e8320e92ea05ece4e821c0a5efe8f1645c4c0c93c1ab99285d622caa652c1dfad63d745d6f2de5f17e5eaf0fc4963d261c8a12436518206dc093344d5ad293"));

    private Assembly _TestAssembly;
    private Assembly TestAssembly => _TestAssembly ??= _context.LoadFromAssemblyName(new AssemblyName("TestAssembly, Version=version, Culture=neutral, PublicKeyToken=null"));

    private Assembly _TestProject;
    private Assembly TestProject => _TestProject ??= _context.LoadFromAssemblyName(new AssemblyName("TestProject, Version=version, Culture=neutral, PublicKeyToken=null"));
}
#pragma warning restore CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
#nullable restore
