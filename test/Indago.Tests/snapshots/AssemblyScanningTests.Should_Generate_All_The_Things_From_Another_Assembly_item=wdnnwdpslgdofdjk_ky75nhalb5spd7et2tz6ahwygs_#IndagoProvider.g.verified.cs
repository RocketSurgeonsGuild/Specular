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
        return items;
    }

    [global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode", Justification = "Types resolved by string literal are preserved with their public constructors via [DynamicDependency], so this reflection is trim- and AOT-safe."), global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2072:DynamicallyAccessedMembers", Justification = "Types resolved by string literal are preserved with their public constructors via [DynamicDependency], so this reflection is trim- and AOT-safe.")]
    IEnumerable<Type> IIndagoProvider.GetTypes(Func<IReflectionTypeSelector, IEnumerable<Type>> selector, int lineNumber, string filePath, string argumentExpression)
    {
        var items = new List<Type>();
        switch (lineNumber)
        {
            // FilePath: Input0.cs Expression: FZejBoVN5/sP3GKMlJ/eVg==
            case 16:
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
                items.Add(OtherProject.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                items.Add(TestAssembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute")!);
                break;
        }

        return items;
    }

    Microsoft.Extensions.DependencyInjection.IServiceCollection IIndagoProvider.Scan(Microsoft.Extensions.DependencyInjection.IServiceCollection services, Action<IServiceDescriptorAssemblySelector> selector, int lineNumber, string filePath, string argumentExpression)
    {
        return services;
    }

    [global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Microsoft.CodeAnalysis.EmbeddedAttribute", "Indago"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Microsoft.CodeAnalysis.EmbeddedAttribute", "OtherProject"), global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "Microsoft.CodeAnalysis.EmbeddedAttribute", "TestAssembly")]
    private AssemblyLoadContext _context = AssemblyLoadContext.GetLoadContext(typeof(IndagoProvider).Assembly)!;
    private Assembly _Indago;
    private Assembly Indago => _Indago ??= _context.LoadFromAssemblyName(new AssemblyName("Indago, Version=version, Culture=neutral, PublicKeyToken=null"));

    private Assembly _OtherProject;
    private Assembly OtherProject => _OtherProject ??= _context.LoadFromAssemblyName(new AssemblyName("OtherProject, Version=version, Culture=neutral, PublicKeyToken=null"));

    private Assembly _TestAssembly;
    private Assembly TestAssembly => _TestAssembly ??= _context.LoadFromAssemblyName(new AssemblyName("TestAssembly, Version=version, Culture=neutral, PublicKeyToken=null"));
}
#pragma warning restore CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
#nullable restore
