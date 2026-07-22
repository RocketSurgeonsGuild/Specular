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
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableTo<IService>()).AsSelf().AsImplementedInterfaces().WithSingletonLifetime()", 
                [
                    new("RootDependencyProject", 
                        [
                            new(ServiceLifetime.Singleton, "global::RootDependencyProject.IService", "global::RootDependencyProject.Service"), 
                            new(ServiceLifetime.Singleton, "global::RootDependencyProject.Service", "global::RootDependencyProject.Service")]
                    )]
            )]
    );
}