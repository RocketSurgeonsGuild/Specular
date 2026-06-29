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

[assembly: System.Reflection.AssemblyMetadata("AssemblyProvider.ReflectionTypes","{scrubbed}")]
[assembly: Indago.Abstractions.IndagoProviderAttribute(typeof(IndagoProvider), "{scrubbed}")]
[System.CodeDom.Compiler.GeneratedCode("Indago.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
file class IndagoProvider : IIndagoProvider
{
    IEnumerable<Assembly> IIndagoProvider.GetAssemblies(Action<IReflectionAssemblySelector> action, int lineNumber, string filePath, string argumentExpression)
    {
        var items = new List<Assembly>();
        return items;
    }

    IEnumerable<Type> IIndagoProvider.GetTypes(Func<IReflectionTypeSelector, IEnumerable<Type>> selector, int lineNumber, string filePath, string argumentExpression)
    {
        var items = new List<Type>();
        switch (lineNumber)
        {
            // FilePath: Input0.cs Expression: yMvGbByGNy7UnkUU8zAHhg==
            case 16:
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Microsoft.CodeAnalysis.EmbeddedAttribute));
                items.Add(typeof(global::Program));
                items.Add(typeof(global::Indago.Abstractions.ExcludeFromIndagoAttribute));
                items.Add(typeof(global::Indago.Abstractions.IndagoProviderAttribute));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderExtensions));
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
                items.Add(typeof(global::Indago.Abstractions.IndagoProviderAttribute));
                items.Add(typeof(global::Indago.Abstractions.TypeInfoFilter));
                items.Add(typeof(global::Indago.Abstractions.TypeKindFilter));
                items.Add(typeof(global::Indago.IndagoProviderExtensions));
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
        }

        return items;
    }

    Microsoft.Extensions.DependencyInjection.IServiceCollection IIndagoProvider.Scan(Microsoft.Extensions.DependencyInjection.IServiceCollection services, Action<IServiceDescriptorAssemblySelector> selector, int lineNumber, string filePath, string argumentExpression)
    {
        return services;
    }

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
