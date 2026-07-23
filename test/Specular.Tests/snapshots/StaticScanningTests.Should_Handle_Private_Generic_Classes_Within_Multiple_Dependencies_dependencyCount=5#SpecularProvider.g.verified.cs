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
        return items;
    }

    IEnumerable<Type> ISpecularProvider.GetTypes(Func<IReflectionTypeSelector, IEnumerable<Type>> selector, int lineNumber, string filePath, string argumentExpression)
    {
        var items = new List<Type>();
        return items;
    }

    [global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode", Justification = "Types resolved by string literal are preserved with their public constructors via [DynamicDependency], so this reflection is trim- and AOT-safe."), global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2072:DynamicallyAccessedMembers", Justification = "Types resolved by string literal are preserved with their public constructors via [DynamicDependency], so this reflection is trim- and AOT-safe.")]
    Microsoft.Extensions.DependencyInjection.IServiceCollection ISpecularProvider.Scan(Microsoft.Extensions.DependencyInjection.IServiceCollection services, Action<IServiceDescriptorAssemblySelector> selector, int lineNumber, string filePath, string argumentExpression)
    {
        switch (lineNumber)
        {
            // FilePath: Input0.cs Expression: a8a5Us+LBXHvnbZedXasXw==
            case 16:
                services.Add(ServiceDescriptor.Singleton<global::RootDependencyProject.IRequestHandler<global::Dependency1Project.Request0, global::Dependency1Project.Response0>, global::Dependency1Project.RequestHandler0>());
                services.Add(ServiceDescriptor.Singleton(typeof(global::RootDependencyProject.IRequestHandler<, >).MakeGenericType(Dependency1Project.GetType("Dependency1Project.Request1")!, Dependency1Project.GetType("Dependency1Project.Response1")!)!, Dependency1Project.GetType("Dependency1Project.RequestHandler1")!));
                services.Add(ServiceDescriptor.Singleton<global::RootDependencyProject.IRequestHandler<global::Dependency1Project.Request2, global::Dependency1Project.Response2>, global::Dependency1Project.RequestHandler2>());
                services.Add(ServiceDescriptor.Singleton(typeof(global::RootDependencyProject.IRequestHandler<, >).MakeGenericType(Dependency3Project.GetType("Dependency1Project.Request3")!, Dependency3Project.GetType("Dependency1Project.Response3")!)!, Dependency3Project.GetType("Dependency1Project.RequestHandler3")!));
                services.Add(ServiceDescriptor.Singleton<global::RootDependencyProject.IRequestHandler<global::Dependency1Project.Request4, global::Dependency1Project.Response4>, global::Dependency1Project.RequestHandler4>());
                break;
        }

        return services;
    }

    [global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Dependency1Project.Request1", "Dependency1Project"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Dependency1Project.RequestHandler1", "Dependency1Project"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Dependency1Project.Response1", "Dependency1Project"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Dependency1Project.Request3", "Dependency3Project"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Dependency1Project.RequestHandler3", "Dependency3Project"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Dependency1Project.Response3", "Dependency3Project")]
    private AssemblyLoadContext _context = AssemblyLoadContext.GetLoadContext(typeof(SpecularProvider).Assembly)!;
    private Assembly _Dependency1Project;
    private Assembly Dependency1Project => _Dependency1Project ??= _context.LoadFromAssemblyName(new AssemblyName("Dependency1Project, Version=version, Culture=neutral, PublicKeyToken=null"));

    private Assembly _Dependency3Project;
    private Assembly Dependency3Project => _Dependency3Project ??= _context.LoadFromAssemblyName(new AssemblyName("Dependency3Project, Version=version, Culture=neutral, PublicKeyToken=null"));
}
#pragma warning restore CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
#nullable restore
