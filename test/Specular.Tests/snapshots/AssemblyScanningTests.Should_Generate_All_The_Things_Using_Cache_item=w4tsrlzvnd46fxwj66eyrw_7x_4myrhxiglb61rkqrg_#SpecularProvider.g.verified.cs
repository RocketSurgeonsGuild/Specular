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

[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
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

    [global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode", Justification = "Types resolved by string literal are preserved with their public constructors via [DynamicDependency], so this reflection is trim- and AOT-safe."), global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2072:DynamicallyAccessedMembers", Justification = "Types resolved by string literal are preserved with their public constructors via [DynamicDependency], so this reflection is trim- and AOT-safe.")]
    IEnumerable<Type> ISpecularProvider.GetTypes(Func<IReflectionTypeSelector, IEnumerable<Type>> selector, int lineNumber, string filePath, string argumentExpression)
    {
        var items = new List<Type>();
        switch (lineNumber)
        {
            // FilePath: Input0.cs Expression: PCRHvamF6J3xug7jJLIREw==
            case 16:
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(Specular.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(typeof(global::Specular.Abstractions.SpecularHashAttribute));
                items.Add(typeof(global::Specular.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Specular.Abstractions.TypeKindFilter));
                items.Add(Specular.GetType("Specular.ScannerExpression")!);
                items.Add(Specular.GetType("Specular.ScannerExpressionKind")!);
                items.Add(Specular.GetType("Specular.ScanReportAssembly")!);
                items.Add(Specular.GetType("Specular.ScanReportType")!);
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

        return items;
    }

    Microsoft.Extensions.DependencyInjection.IServiceCollection ISpecularProvider.Scan(Microsoft.Extensions.DependencyInjection.IServiceCollection services, Action<IServiceDescriptorAssemblySelector> selector, int lineNumber, string filePath, string argumentExpression)
    {
        return services;
    }

    [global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Microsoft.CodeAnalysis.EmbeddedAttribute", "Specular"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Specular.ScanReportAssembly", "Specular"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Specular.ScanReportType", "Specular"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Specular.ScannerExpression", "Specular"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Specular.ScannerExpressionKind", "Specular"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Microsoft.CodeAnalysis.EmbeddedAttribute", "TestAssembly")]
    private AssemblyLoadContext _context = AssemblyLoadContext.GetLoadContext(typeof(SpecularProvider).Assembly)!;
    private Assembly _Specular;
    private Assembly Specular => _Specular ??= _context.LoadFromAssemblyName(new AssemblyName("Specular, Version=version, Culture=neutral, PublicKeyToken=null"));

    private Assembly _TestAssembly;
    private Assembly TestAssembly => _TestAssembly ??= _context.LoadFromAssemblyName(new AssemblyName("TestAssembly, Version=version, Culture=neutral, PublicKeyToken=null"));
}
#pragma warning restore CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
#nullable restore
