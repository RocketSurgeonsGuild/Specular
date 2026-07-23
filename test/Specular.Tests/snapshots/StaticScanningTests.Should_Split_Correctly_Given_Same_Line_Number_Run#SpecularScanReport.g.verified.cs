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
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableTo(typeof(IService)), false).AsSelf().AsImplementedInterfaces().WithSingletonLifetime()", 
                [
                    new("TestProject", 
                        [
                            new(ServiceLifetime.Singleton, "global::IService", "global::Service"), 
                            new(ServiceLifetime.Singleton, "global::Service", "global::Service")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableTo<IServiceB>(), false).AsSelf().AsImplementedInterfaces().WithScopedLifetime()", 
                [
                    new("TestProject", 
                        [
                            new(ServiceLifetime.Scoped, "global::IServiceB", "global::ServiceB"), 
                            new(ServiceLifetime.Scoped, "global::ServiceB", "global::ServiceB")]
                    )]
            )]
    );
}