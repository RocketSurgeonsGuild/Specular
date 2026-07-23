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
            // FilePath: Input0.cs Expression: cXs6mwh4RZ7pIHPh84+ktQ==
            case 21:
                services.Add(ServiceDescriptor.Singleton(DependencyProjectC.GetType("DependencyProjectC.HardReferenceA")!, DependencyProjectC.GetType("DependencyProjectC.HardReferenceA")!));
                services.Add(ServiceDescriptor.Singleton<global::RootDependencyProject.IService>(a => (global::RootDependencyProject.IService)a.GetRequiredService(DependencyProjectC.GetType("DependencyProjectC.HardReferenceA")!)));
                services.Add(ServiceDescriptor.Singleton<global::DependencyProjectC.ServiceC, global::DependencyProjectC.ServiceC>());
                services.Add(ServiceDescriptor.Singleton<global::RootDependencyProject.IService>(a => a.GetRequiredService<global::DependencyProjectC.ServiceC>()));
                services.Add(ServiceDescriptor.Singleton(DependencyProjectD.GetType("DependencyProjectD.HardReferenceA")!, DependencyProjectD.GetType("DependencyProjectD.HardReferenceA")!));
                services.Add(ServiceDescriptor.Singleton<global::RootDependencyProject.IService>(a => (global::RootDependencyProject.IService)a.GetRequiredService(DependencyProjectD.GetType("DependencyProjectD.HardReferenceA")!)));
                services.Add(ServiceDescriptor.Singleton(DependencyProjectD.GetType("DependencyProjectD.HardReferenceC")!, DependencyProjectD.GetType("DependencyProjectD.HardReferenceC")!));
                services.Add(ServiceDescriptor.Singleton<global::RootDependencyProject.IService>(a => (global::RootDependencyProject.IService)a.GetRequiredService(DependencyProjectD.GetType("DependencyProjectD.HardReferenceC")!)));
                services.Add(ServiceDescriptor.Singleton<global::DependencyProjectD.ServiceD, global::DependencyProjectD.ServiceD>());
                services.Add(ServiceDescriptor.Singleton<global::RootDependencyProject.IService>(a => a.GetRequiredService<global::DependencyProjectD.ServiceD>()));
                break;
        }

        return services;
    }

    [global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "DependencyProjectC.HardReferenceA", "DependencyProjectC"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "DependencyProjectD.HardReferenceA", "DependencyProjectD"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "DependencyProjectD.HardReferenceC", "DependencyProjectD")]
    private AssemblyLoadContext _context = AssemblyLoadContext.GetLoadContext(typeof(SpecularProvider).Assembly)!;
    private Assembly _DependencyProjectC;
    private Assembly DependencyProjectC => _DependencyProjectC ??= _context.LoadFromAssemblyName(new AssemblyName("DependencyProjectC, Version=version, Culture=neutral, PublicKeyToken=null"));

    private Assembly _DependencyProjectD;
    private Assembly DependencyProjectD => _DependencyProjectD ??= _context.LoadFromAssemblyName(new AssemblyName("DependencyProjectD, Version=version, Culture=neutral, PublicKeyToken=null"));
}
#pragma warning restore CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
#nullable restore
