//HintName: Indago.Analyzers/Indago.Analyzers.IndagoProviderGenerator/IndagoProvider.g.cs
#nullable enable
#pragma warning disable CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Indago;
using Indago.Abstractions;
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
[assembly: Indago.Abstractions.IndagoHashAttribute("{scrubbed}")]
[System.CodeDom.Compiler.GeneratedCode("Indago.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal sealed class IndagoProvider : IIndagoProvider
{
    public static IIndagoProvider Instance { get; } = new IndagoProvider();

    private IndagoProvider()
    {
    }

    IEnumerable<Assembly> IIndagoProvider.GetAssemblies(Action<IReflectionAssemblySelector> action, int lineNumber, string filePath, string argumentExpression)
    {
        var items = new List<Assembly>();
        switch (lineNumber)
        {
            // FilePath: Input0.cs Expression: 8IGLe4J/MYHkM0PrcVVuwg==
            case 16:
                items.Add(typeof(global::Indago.IIndagoProvider).Assembly);
                items.Add(OtherProject);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                items.Add(typeof(global::Indago.IIndagoProvider).Assembly);
                items.Add(OtherProject);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                break;
            // FilePath: Input0.cs Expression: Z9rFHKTvVtL4pjKVS65ieA==
            case 17:
                items.Add(typeof(global::Indago.IIndagoProvider).Assembly);
                items.Add(OtherProject);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                items.Add(typeof(global::Indago.IIndagoProvider).Assembly);
                items.Add(OtherProject);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                break;
            // FilePath: Input0.cs Expression: EroujZtERsWBDGh52iQ5LQ==
            case 18:
                items.Add(typeof(global::Indago.IIndagoProvider).Assembly);
                items.Add(OtherProject);
                items.Add(typeof(global::System.IServiceProvider).Assembly);
                items.Add(typeof(global::TestAssembly.IService).Assembly);
                items.Add(TestProject);
                items.Add(typeof(global::Indago.IIndagoProvider).Assembly);
                items.Add(OtherProject);
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
    IEnumerable<Type> IIndagoProvider.GetTypes(Func<IReflectionTypeSelector, IEnumerable<Type>> selector, int lineNumber, string filePath, string argumentExpression)
    {
        var items = new List<Type>();
        switch (lineNumber)
        {
            // FilePath: Input0.cs Expression: h7YOdRK4kWM8suq9w5jcEA==
            case 42:
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Indago.Abstractions.ExcludeFromIndagoAttribute));
                items.Add(typeof(global::Indago.Abstractions.IndagoHashAttribute));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(typeof(global::Indago.RegistrationLifetimeAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<, >));
                items.Add(Indago.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Indago.Abstractions.ExcludeFromIndagoAttribute));
                items.Add(typeof(global::Indago.Abstractions.IndagoHashAttribute));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(typeof(global::Indago.RegistrationLifetimeAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<, >));
                items.Add(Indago.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(OtherProject.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(TestAssembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
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
                items.Add(TestAssembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
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
            // FilePath: Input0.cs Expression: FZejBoVN5/sP3GKMlJ/eVg==
            case 82:
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Indago.Abstractions.IndagoHashAttribute));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<, >));
                items.Add(Indago.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(typeof(global::Indago.Abstractions.IndagoHashAttribute));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<, >));
                items.Add(Indago.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(OtherProject.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(OtherProject.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(TestAssembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(TestAssembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                break;
            // FilePath: Input0.cs Expression: IEL5ckPWEXd/Ak5gZ5DdEg==
            case 102:
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Indago.Abstractions.ExcludeFromIndagoAttribute));
                items.Add(typeof(global::Indago.Abstractions.IndagoHashAttribute));
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.RegistrationLifetimeAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<, >));
                items.Add(Indago.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(typeof(global::Indago.Abstractions.ExcludeFromIndagoAttribute));
                items.Add(typeof(global::Indago.Abstractions.IndagoHashAttribute));
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.RegistrationLifetimeAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<, >));
                items.Add(Indago.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(OtherProject.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(OtherProject.GetType("Program")!);
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
            // FilePath: Input0.cs Expression: yMvGbByGNy7UnkUU8zAHhg==
            case 122:
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Indago.Abstractions.ExcludeFromIndagoAttribute));
                items.Add(typeof(global::Indago.Abstractions.IndagoHashAttribute));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(typeof(global::Indago.RegistrationLifetimeAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<, >));
                items.Add(Indago.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Indago.Abstractions.ExcludeFromIndagoAttribute));
                items.Add(typeof(global::Indago.Abstractions.IndagoHashAttribute));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(typeof(global::Indago.RegistrationLifetimeAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<, >));
                items.Add(Indago.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(OtherProject.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(TestAssembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
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
                items.Add(TestAssembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
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
            // FilePath: Input0.cs Expression: V1zz0pHDKJ/BSBrqm8jiAQ==
            case 132:
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Program));
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
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
            // FilePath: Input0.cs Expression: D3ksYueIthG4SMYim0ZUgQ==
            case 147:
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
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
            // FilePath: Input0.cs Expression: amdUsVIuVJ6Z9uFUw2siUA==
            case 158:
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
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
            // FilePath: Input0.cs Expression: L+7TFBSI3dH5VV4rukSWKg==
            case 169:
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
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
            // FilePath: Input0.cs Expression: ebF1x94ZPF9KfIUWiOTH9w==
            case 200:
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
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
            // FilePath: Input0.cs Expression: 9qYq//oeCy0F7tWGcEnCUw==
            case 211:
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(OtherProject.GetType("Program")!);
                items.Add(OtherProject.GetType("Program")!);
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
            // FilePath: Input0.cs Expression: bKhVPtwmtNPU4oAyExjo+A==
            case 22:
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
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
            // FilePath: Input0.cs Expression: /VBJuhyt3B9JZh0hvnXCgQ==
            case 32:
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
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
            // FilePath: Input0.cs Expression: K7KxgHvx+EGwwlQkuemT3A==
            case 52:
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
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
            // FilePath: Input0.cs Expression: /9IHLslt+gQ8+YOvZs9dwA==
            case 62:
                items.Add(typeof(global::Indago.Abstractions.ExcludeFromIndagoAttribute));
                items.Add(typeof(global::Indago.Abstractions.IndagoHashAttribute));
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(typeof(global::Indago.RegistrationLifetimeAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<, >));
                items.Add(typeof(global::Indago.Abstractions.ExcludeFromIndagoAttribute));
                items.Add(typeof(global::Indago.Abstractions.IndagoHashAttribute));
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(typeof(global::Indago.RegistrationLifetimeAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<, >));
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
            // FilePath: Input0.cs Expression: vC7jRGImSIqrxk4mTImeWA==
            case 72:
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                break;
            // FilePath: Input0.cs Expression: AkSQC22LryNlbH2i5Itc6g==
            case 92:
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(Indago.GetType("MyAssembly")!);
                items.Add(Indago.GetType("MyAssembly+Info")!);
                items.Add(Indago.GetType("MyAssembly+Metadata")!);
                items.Add(Indago.GetType("MyAssembly+Project")!);
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
            // FilePath: Input0.cs Expression: icamNGQlbhR5fqnsVbNH4A==
            case 112:
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<, >));
                items.Add(typeof(global::TestAssembly.IGenericService<>));
                items.Add(typeof(global::TestAssembly.IRequest<>));
                items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                items.Add(typeof(global::TestAssembly.IValidator<>));
                items.Add(typeof(global::TestAssembly.IGenericService<>));
                items.Add(typeof(global::TestAssembly.IRequest<>));
                items.Add(typeof(global::TestAssembly.IRequestHandler<, >));
                items.Add(typeof(global::TestAssembly.IValidator<>));
                break;
            // FilePath: Input0.cs Expression: oMLt/17IrkYji1+jqei7NQ==
            case 180:
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(typeof(global::Indago.IndagoSupport));
                break;
            // FilePath: Input0.cs Expression: +H5FB77BY9RWCbY+SBdYNQ==
            case 190:
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(typeof(global::Indago.IndagoSupport));
                break;
            // FilePath: Input0.cs Expression: QmnB+zKdWqffTt1vZpunVg==
            case 252:
                items.Add(typeof(global::Indago.Abstractions.ExcludeFromIndagoAttribute));
                items.Add(typeof(global::Indago.Abstractions.IndagoHashAttribute));
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(typeof(global::Indago.RegistrationLifetimeAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<, >));
                items.Add(typeof(global::Indago.Abstractions.ExcludeFromIndagoAttribute));
                items.Add(typeof(global::Indago.Abstractions.IndagoHashAttribute));
                items.Add(typeof(global::Indago.Abstractions.IReflectionAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IReflectionTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorAssemblySelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceDescriptorTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceLifetimeSelector));
                items.Add(typeof(global::Indago.Abstractions.IServiceTypeSelector));
                items.Add(typeof(global::Indago.Abstractions.ITypeFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderServiceCollectionExtensions));
                items.Add(typeof(global::Indago.IndagoSupport));
                items.Add(typeof(global::Indago.RegistrationLifetimeAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<>));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<,, >));
                items.Add(typeof(global::Indago.ServiceRegistrationAttribute<, >));
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
    Microsoft.Extensions.DependencyInjection.IServiceCollection IIndagoProvider.Scan(Microsoft.Extensions.DependencyInjection.IServiceCollection services, Action<IServiceDescriptorAssemblySelector> selector, int lineNumber, string filePath, string argumentExpression)
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
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IService), TestAssembly.GetType("TestAssembly.ServiceB")!));
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
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService, global::TestAssembly.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IService, global::TestAssembly.Service>());
                services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IService), TestAssembly.GetType("TestAssembly.ServiceB")!));
                break;
            // FilePath: Input0.cs Expression: iOVNY6dfLzra3vCDdW4AxA==
            case 341:
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Int32>>(a => (global::TestAssembly.IGenericService<global::System.Int32>)a.GetRequiredService(TestAssembly.GetType("TestAssembly.GenericService")!)));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>>(a => a.GetRequiredService<global::TestAssembly.GenericServiceB>()));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>>(a => a.GetRequiredService<global::TestAssembly.Nested.GenericServiceA>()));
                services.Add(ServiceDescriptor.Scoped(TestAssembly.GetType("TestAssembly.GenericService")!, TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.GenericServiceB, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.Nested.GenericServiceA, global::TestAssembly.Nested.GenericServiceA>());
                break;
            // FilePath: Input0.cs Expression: jm/nkIDlrjpFn5pT15jCnw==
            case 347:
                services.Add(ServiceDescriptor.Scoped(typeof(global::TestAssembly.IGenericService<global::System.Int32>), TestAssembly.GetType("TestAssembly.GenericService")!));
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.Decimal>, global::TestAssembly.GenericServiceB>());
                services.Add(ServiceDescriptor.Scoped<global::TestAssembly.IGenericService<global::System.String>, global::TestAssembly.Nested.GenericServiceA>());
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

    [global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Microsoft.CodeAnalysis.EmbeddedAttribute", "Indago"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly", "Indago"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly+Info", "Indago"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly+Metadata", "Indago"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly+Project", "Indago"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Microsoft.CodeAnalysis.EmbeddedAttribute", "OtherProject"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Program", "OtherProject"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Microsoft.CodeAnalysis.EmbeddedAttribute", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly+Info", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly+Metadata", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "MyAssembly+Project", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.GenericService", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Nested+MyRecord", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Nested+Validator", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Request", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.RequestHandler", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.Response", "TestAssembly"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "TestAssembly.ServiceB", "TestAssembly")]
    private AssemblyLoadContext _context = AssemblyLoadContext.GetLoadContext(typeof(IndagoProvider).Assembly)!;
    private Assembly _Indago;
    private Assembly Indago => _Indago ??= _context.LoadFromAssemblyName(new AssemblyName("Indago, Version=version, Culture=neutral, PublicKeyToken=null"));

    private Assembly _OtherProject;
    private Assembly OtherProject => _OtherProject ??= _context.LoadFromAssemblyName(new AssemblyName("OtherProject, Version=version, Culture=neutral, PublicKeyToken=null"));

    private Assembly _TestAssembly;
    private Assembly TestAssembly => _TestAssembly ??= _context.LoadFromAssemblyName(new AssemblyName("TestAssembly, Version=version, Culture=neutral, PublicKeyToken=null"));

    private Assembly _TestProject;
    private Assembly TestProject => _TestProject ??= _context.LoadFromAssemblyName(new AssemblyName("TestProject, Version=version, Culture=neutral, PublicKeyToken=null"));
}
#pragma warning restore CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
#nullable restore
