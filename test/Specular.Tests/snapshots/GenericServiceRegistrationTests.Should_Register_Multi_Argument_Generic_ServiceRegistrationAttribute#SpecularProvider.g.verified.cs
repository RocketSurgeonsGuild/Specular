//HintName: Specular.Analyzers/Specular.Analyzers.SpecularProviderGenerator/SpecularProvider.g.cs
#nullable enable
#pragma warning disable CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Specular;
using Specular.Abstractions;

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

    Microsoft.Extensions.DependencyInjection.IServiceCollection ISpecularProvider.Scan(Microsoft.Extensions.DependencyInjection.IServiceCollection services, Action<IServiceDescriptorAssemblySelector> selector, int lineNumber, string filePath, string argumentExpression)
    {
        switch (lineNumber)
        {
            // FilePath: {SolutionDirectory}src/Specular/SpecularProviderServiceCollectionExtensions.cs Expression: QGmxhNh0sTcsPjKev+mVvQ==
            case 20:
                services.Add(ServiceDescriptor.Scoped<global::ThreeServices, global::ThreeServices>());
                services.Add(ServiceDescriptor.Scoped<global::IC>(a => a.GetRequiredService<global::ThreeServices>()));
                services.Add(ServiceDescriptor.Scoped<global::ID>(a => a.GetRequiredService<global::ThreeServices>()));
                services.Add(ServiceDescriptor.Scoped<global::IE>(a => a.GetRequiredService<global::ThreeServices>()));
                services.Add(ServiceDescriptor.Transient<global::TwoServices, global::TwoServices>());
                services.Add(ServiceDescriptor.Transient<global::IA>(a => a.GetRequiredService<global::TwoServices>()));
                services.Add(ServiceDescriptor.Transient<global::IB>(a => a.GetRequiredService<global::TwoServices>()));
                break;
        }

        return services;
    }
}
#pragma warning restore CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
#nullable restore
