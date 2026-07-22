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
            new("TestProject", "z => z.DependenciesFromAssemblyOf(typeof(ServiceA)).AddClasses(x => x.AssignableTo(typeof(IService)), true).AsSelf().AsImplementedInterfaces().WithSingletonLifetime()", 
                [
                    new("DependencyProjectC", 
                        [
                            new(ServiceLifetime.Singleton, "global::DependencyProjectC.HardReferenceA", "global::DependencyProjectC.HardReferenceA"), 
                            new(ServiceLifetime.Singleton, "global::DependencyProjectC.ServiceC", "global::DependencyProjectC.ServiceC"), 
                            new(ServiceLifetime.Singleton, "global::RootDependencyProject.IService", "global::DependencyProjectC.HardReferenceA"), 
                            new(ServiceLifetime.Singleton, "global::RootDependencyProject.IService", "global::DependencyProjectC.ServiceC")]
                    ), 
                    new("DependencyProjectD", 
                        [
                            new(ServiceLifetime.Singleton, "global::DependencyProjectD.HardReferenceA", "global::DependencyProjectD.HardReferenceA"), 
                            new(ServiceLifetime.Singleton, "global::DependencyProjectD.HardReferenceC", "global::DependencyProjectD.HardReferenceC"), 
                            new(ServiceLifetime.Singleton, "global::DependencyProjectD.ServiceD", "global::DependencyProjectD.ServiceD"), 
                            new(ServiceLifetime.Singleton, "global::RootDependencyProject.IService", "global::DependencyProjectD.HardReferenceA"), 
                            new(ServiceLifetime.Singleton, "global::RootDependencyProject.IService", "global::DependencyProjectD.HardReferenceC"), 
                            new(ServiceLifetime.Singleton, "global::RootDependencyProject.IService", "global::DependencyProjectD.ServiceD")]
                    )]
            )]
    );
}