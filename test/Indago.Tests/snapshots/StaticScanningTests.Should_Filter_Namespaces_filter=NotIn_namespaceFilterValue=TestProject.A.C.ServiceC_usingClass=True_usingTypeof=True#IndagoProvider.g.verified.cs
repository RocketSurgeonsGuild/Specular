//HintName: Indago.Analyzers/Indago.Analyzers.IndagoProviderGenerator/IndagoProvider.g.cs
#nullable enable
#pragma warning disable CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Indago;
using Indago.Abstractions;

[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ServiceDescriptorTypes","{scrubbed}")]
[assembly: Indago.Abstractions.IndagoHashAttribute("{scrubbed}")]
[System.CodeDom.Compiler.GeneratedCode("Indago.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal sealed class IndagoProvider : IIndagoProvider
{
    public static IIndagoProvider Instance { get; } = new IndagoProvider();

    IEnumerable<Assembly> IIndagoProvider.GetAssemblies(Action<IReflectionAssemblySelector> action, int lineNumber, string filePath, string argumentExpression)
    {
        var items = new List<Assembly>();
        return items;
    }

    IEnumerable<Type> IIndagoProvider.GetTypes(Func<IReflectionTypeSelector, IEnumerable<Type>> selector, int lineNumber, string filePath, string argumentExpression)
    {
        var items = new List<Type>();
        return items;
    }

    Microsoft.Extensions.DependencyInjection.IServiceCollection IIndagoProvider.Scan(Microsoft.Extensions.DependencyInjection.IServiceCollection services, Action<IServiceDescriptorAssemblySelector> selector, int lineNumber, string filePath, string argumentExpression)
    {
        switch (lineNumber)
        {
            // FilePath: Input0.cs Expression: okFkuRa0FxAsvw5sIQvZAA==
            case 33:
                services.Add(ServiceDescriptor.Scoped<global::TestProject.A.Nested.ServiceA, global::TestProject.A.Nested.ServiceA>());
                services.Add(ServiceDescriptor.Scoped<global::TestProject.A.IService>(a => a.GetRequiredService<global::TestProject.A.Nested.ServiceA>()));
                services.Add(ServiceDescriptor.Scoped<global::TestProject.A.Service, global::TestProject.A.Service>());
                services.Add(ServiceDescriptor.Scoped<global::TestProject.A.IService>(a => a.GetRequiredService<global::TestProject.A.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestProject.B.IServiceB>(a => a.GetRequiredService<global::TestProject.A.Service>()));
                services.Add(ServiceDescriptor.Scoped<global::TestProject.B.ServiceB, global::TestProject.B.ServiceB>());
                services.Add(ServiceDescriptor.Scoped<global::TestProject.A.IService>(a => a.GetRequiredService<global::TestProject.B.ServiceB>()));
                break;
        }

        return services;
    }
}
#pragma warning restore CA1002, CA1034, CA1822, CS0105, CS1573, CA5351, CS8618, CS8669, IL2026, IL2072
#nullable restore
