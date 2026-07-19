//HintName: Specular.Analyzers/Specular.Analyzers.SpecularProviderGenerator/SpecularProvider.g.cs
#nullable enable
#pragma warning disable CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Specular;
using Specular.Abstractions;

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

    IEnumerable<Type> ISpecularProvider.GetTypes(Func<IReflectionTypeSelector, IEnumerable<Type>> selector, int lineNumber, string filePath, string argumentExpression)
    {
        var items = new List<Type>();
        switch (lineNumber)
        {
            // FilePath: Input0.cs Expression: +FkT4btNbS4PRfcftoFbtA==
            case 16:
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(typeof(global::Specular.SpecularSupport));
                items.Add(typeof(global::Specular.SpecularProviderServiceCollectionExtensions));
                items.Add(typeof(global::Specular.SpecularSupport));
                items.Add(typeof(global::TestAssembly.Nested));
                items.Add(typeof(global::TestAssembly.Nested));
                break;
        }

        return items;
    }

    Microsoft.Extensions.DependencyInjection.IServiceCollection ISpecularProvider.Scan(Microsoft.Extensions.DependencyInjection.IServiceCollection services, Action<IServiceDescriptorAssemblySelector> selector, int lineNumber, string filePath, string argumentExpression)
    {
        return services;
    }
}
#pragma warning restore CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
#nullable restore
