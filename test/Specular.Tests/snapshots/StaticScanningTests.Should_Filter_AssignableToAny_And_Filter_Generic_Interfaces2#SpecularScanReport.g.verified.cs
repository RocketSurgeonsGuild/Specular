//HintName: Specular.Analyzers/Specular.Analyzers.SpecularProviderGenerator/SpecularScanReport.g.cs
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Specular;
using Specular.Abstractions;
using Specular.Diagnostics;

[System.CodeDom.Compiler.GeneratedCode("Specular.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal sealed class SpecularScanReport
{
    public static ScanResults GetScanResults() => new(
        []
    , 
        []
    , 
        [
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableToAny(typeof(IOther))).AsImplementedInterfaces(z => z.AssignableTo(typeof(IService<>))).WithScopedLifetime()", 
                [
                    new("TestProject", 
                        [
                            new(ServiceLifetime.Scoped, "global::IService<global::System.Decimal>", "global::ServiceB"), 
                            new(ServiceLifetime.Scoped, "global::IService<global::System.Int32>", "global::Service"), 
                            new(ServiceLifetime.Scoped, "global::IService<global::System.String>", "global::Nested+ServiceA"), 
                            new(ServiceLifetime.Scoped, "global::IService<global::System.String>", "global::Service")]
                    )]
            )]
    );
}