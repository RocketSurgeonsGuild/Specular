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
                items.Add(OtherProject);
                items.Add(typeof(global::Specular.ISpecularProvider).Assembly);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                items.Add(OtherProject);
                items.Add(typeof(global::Specular.ISpecularProvider).Assembly);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                break;
            // FilePath: Input0.cs Expression: Z9rFHKTvVtL4pjKVS65ieA==
            case 17:
                items.Add(OtherProject);
                items.Add(typeof(global::Specular.ISpecularProvider).Assembly);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                items.Add(OtherProject);
                items.Add(typeof(global::Specular.ISpecularProvider).Assembly);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                break;
            // FilePath: Input0.cs Expression: EroujZtERsWBDGh52iQ5LQ==
            case 18:
                items.Add(OtherProject);
                items.Add(typeof(global::Specular.ISpecularProvider).Assembly);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                items.Add(OtherProject);
                items.Add(typeof(global::Specular.ISpecularProvider).Assembly);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                break;
            // FilePath: Input0.cs Expression: ApaVlALM8kzVxm/WHvukVQ==
            case 20:
                items.Add(OtherProject);
                items.Add(TestProject);
                items.Add(OtherProject);
                items.Add(TestProject);
                break;
            // FilePath: Input0.cs Expression: y1nZVGCnh7EvCDUbXmZVJg==
            case 21:
                items.Add(OtherProject);
                items.Add(TestProject);
                items.Add(OtherProject);
                items.Add(TestProject);
                break;
            // FilePath: Input0.cs Expression: /wvNd0rNqfzrIH57jso7mA==
            case 19:
                items.Add(OtherProject);
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
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.Abstractions.ExcludeFromSpecularAttribute));
                items.Add(typeof(global::Specular.Abstractions.SpecularHashAttribute));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
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
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.Abstractions.ExcludeFromSpecularAttribute));
                items.Add(typeof(global::Specular.Abstractions.SpecularHashAttribute));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
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
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
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
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
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
            case 82:
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Specular.Abstractions.SpecularHashAttribute));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<, >));
                items.Add(typeof(global::Specular.Abstractions.SpecularHashAttribute));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<, >));
                break;
            // FilePath: Input0.cs Expression: fsFGHt8fY/Soe/yfBavpPA==
            case 102:
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
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
            case 122:
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.Abstractions.ExcludeFromSpecularAttribute));
                items.Add(typeof(global::Specular.Abstractions.SpecularHashAttribute));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
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
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.Abstractions.ExcludeFromSpecularAttribute));
                items.Add(typeof(global::Specular.Abstractions.SpecularHashAttribute));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
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
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
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
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
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
            case 132:
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Program));
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
                items.Add(TestAssembly.GetType("TestAssembly.GenericService")!);
                items.Add(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!);
                items.Add(TestAssembly.GetType("TestAssembly.Nested+Validator")!);
                items.Add(TestAssembly.GetType("TestAssembly.Request")!);
                items.Add(TestAssembly.GetType("TestAssembly.RequestHandler")!);
                items.Add(TestAssembly.GetType("TestAssembly.Response")!);
                items.Add(TestAssembly.GetType("TestAssembly.ServiceB")!);
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
                items.Add(TestAssembly.GetType("TestAssembly.GenericService")!);
                items.Add(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!);
                items.Add(TestAssembly.GetType("TestAssembly.Nested+Validator")!);
                items.Add(TestAssembly.GetType("TestAssembly.Request")!);
                items.Add(TestAssembly.GetType("TestAssembly.RequestHandler")!);
                items.Add(TestAssembly.GetType("TestAssembly.Response")!);
                items.Add(TestAssembly.GetType("TestAssembly.ServiceB")!);
                break;
            // FilePath: Input0.cs Expression: s3+YB+Nc72MZzZXRrB9ZBQ==
            case 147:
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Program));
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(typeof(global::Specular.SpecularSupport));
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(typeof(global::Specular.SpecularSupport));
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
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
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
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
            case 158:
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Program));
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(typeof(global::Specular.SpecularSupport));
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(typeof(global::Specular.SpecularSupport));
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
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
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
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
            case 169:
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Program));
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(typeof(global::Specular.SpecularSupport));
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(typeof(global::Specular.SpecularSupport));
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
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
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
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
            case 200:
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Program));
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
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
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
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
            case 211:
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Program));
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
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
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
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
            // FilePath: Input0.cs Expression: cizxMQ3lnxFjX801wlpqbw==
            case 22:
                items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
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
            case 32:
                items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
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
            case 52:
                items.Add(typeof(global::Specular.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Specular.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Specular.Abstractions.ITypeFilter));
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
            case 62:
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
            // FilePath: Input0.cs Expression: 0fBeWD2wBlWIjLnT36cIow==
            case 72:
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                break;
            // FilePath: Input0.cs Expression: +FkT4btNbS4PRfcftoFbtA==
            case 92:
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(typeof(global::Specular.SpecularSupport));
                items.Add(Specular.GetType("MyAssembly")!);
                items.Add(Specular.GetType("MyAssembly+Info")!);
                items.Add(Specular.GetType("MyAssembly+Metadata")!);
                items.Add(Specular.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(typeof(global::Specular.SpecularSupport));
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::TestAssembly.Nested));
                items.Add(TestAssembly.GetType("MyAssembly")!);
                items.Add(TestAssembly.GetType("MyAssembly+Info")!);
                items.Add(TestAssembly.GetType("MyAssembly+Metadata")!);
                items.Add(TestAssembly.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::TestAssembly.Nested));
                break;
            // FilePath: Input0.cs Expression: /Q4AySzV41Tdwlkpfg/GKA==
            case 112:
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Specular.ServiceRegistrationAttribute<, >));
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
                items.Add(typeof(global::TestAssembly.IGenericService<>));
                items.Add(typeof(global::TestAssembly.IRequest<>));
                items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                items.Add(typeof(global::TestAssembly.IValidator<>));
                break;
            // FilePath: Input0.cs Expression: Edc9cxDaKw/uRcTfQ0BxWQ==
            case 180:
                items.Add(typeof(global::Specular.SpecularSupport));
                items.Add(typeof(global::Specular.SpecularSupport));
                break;
            // FilePath: Input0.cs Expression: cNBTr21Y/0DD1+cR1se7TQ==
            case 190:
                items.Add(typeof(global::Specular.SpecularSupport));
                items.Add(typeof(global::Specular.SpecularSupport));
                break;
            // FilePath: Input0.cs Expression: btOVlu+Naw13LAV9tKR6WA==
            case 252:
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
            case 262:
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                break;
            // FilePath: Input0.cs Expression: CSc+6hElAjxc8UqHDR2nGg==
            case 268:
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Singleton(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Singleton(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                break;
            // FilePath: Input0.cs Expression: fKfq36ZCDNfEou+qyyLEbw==
            case 274:
                break;
            // FilePath: Input0.cs Expression: RF0U3ve7NmkBVhOpOoW7bQ==
            case 279:
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Singleton(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Singleton(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                services.Add(ServiceDescriptor.Singleton<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                break;
            // FilePath: Input0.cs Expression: H/jKai1h1B6jnFlFcyi7Tg==
            case 285:
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                break;
            // FilePath: Input0.cs Expression: Cig2XJLApDe8z9wMh/IDUA==
            case 291:
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
            // FilePath: Input0.cs Expression: GC4f7Eu/9wIch8wpSP2wyQ==
            case 296:
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
            // FilePath: Input0.cs Expression: a8a5Us+LBXHvnbZedXasXw==
            case 302:
                services.Add(ServiceDescriptor.Singleton(typeof(global::TestAssembly.IRequestHandler<, >).MakeGenericType(TestAssembly.GetType("TestAssembly.Request")!, TestAssembly.GetType("TestAssembly.Response")!)!, TestAssembly.GetType("TestAssembly.RequestHandler")!));
                services.Add(ServiceDescriptor.Singleton(typeof(global::TestAssembly.IRequestHandler<, >).MakeGenericType(TestAssembly.GetType("TestAssembly.Request")!, TestAssembly.GetType("TestAssembly.Response")!)!, TestAssembly.GetType("TestAssembly.RequestHandler")!));
                break;
            // FilePath: Input0.cs Expression: xbYAbKX/QnnClXoKl6q/Aw==
            case 307:
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                break;
            // FilePath: Input0.cs Expression: e8KAWIaBqxCKvrQ5PGf0qg==
            case 313:
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Nested.ServiceA>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService>(a => (global::TestAssembly.IService)a.GetRequiredService(TestAssembly.GetType("TestAssembly.ServiceB")!)));
                break;
            // FilePath: Input0.cs Expression: QR7ilMUHdBOqlpGLxKKLDQ==
            case 319:
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.ServiceA, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Service, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IServiceB>(a => a.GetRequiredService<global::TestAssembly.Service>()));
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.ServiceB")!, TestAssembly.GetType("TestAssembly.ServiceB")!));
                break;
            // FilePath: Input0.cs Expression: nNzkffztFbvyPeXlZPkOkQ==
            case 325:
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => (global::TestAssembly.IGenericService<global::System.String>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => (global::TestAssembly.IGenericService<global::System.String>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                break;
            // FilePath: Input0.cs Expression: HH9aVmBgFQ5bSAugUwbZsw==
            case 331:
                services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IValidator<>).MakeGenericType(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!)!, TestAssembly.GetType("TestAssembly.Nested+Validator")!));
                services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IValidator<>).MakeGenericType(TestAssembly.GetType("TestAssembly.Nested+MyRecord")!)!, TestAssembly.GetType("TestAssembly.Nested+Validator")!));
                break;
            // FilePath: Input0.cs Expression: nGdrAptlVCEX9H6kHx0auw==
            case 336:
                break;
            // FilePath: Input0.cs Expression: iOVNY6dfLzra3vCDdW4AxA==
            case 341:
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                break;
            // FilePath: Input0.cs Expression: jm/nkIDlrjpFn5pT15jCnw==
            case 347:
                break;
            // FilePath: Input0.cs Expression: MeX+mF+ej5803TONO0SaMw==
            case 366:
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

        return services;
    }

    [global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Program", "OtherProject"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly", "Specular"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly+Info", "Specular"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly+Metadata", "Specular"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly+Project", "Specular"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly+Info", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly+Metadata", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly+Project", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.GenericService", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Nested+MyRecord", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Nested+Validator", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Request", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.RequestHandler", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Response", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.ServiceB", "TestAssembly")]
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
