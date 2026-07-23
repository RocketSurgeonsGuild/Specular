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
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableTo<IService>()).AsImplementedInterfaces().WithSingletonLifetime()", 
                [
                    new("Dependency0Project", 
                        [
                            new(ServiceLifetime.Singleton, "global::RootDependencyProject.IService", "global::Dependency1Project.Service0")]
                    ), 
                    new("Dependency1Project", 
                        [
                            new(ServiceLifetime.Singleton, "global::RootDependencyProject.IService", "global::Dependency1Project.Service1")]
                    ), 
                    new("Dependency2Project", 
                        [
                            new(ServiceLifetime.Singleton, "global::RootDependencyProject.IService", "global::Dependency1Project.Service2")]
                    ), 
                    new("Dependency3Project", 
                        [
                            new(ServiceLifetime.Singleton, "global::RootDependencyProject.IService", "global::Dependency1Project.Service3")]
                    ), 
                    new("Dependency4Project", 
                        [
                            new(ServiceLifetime.Singleton, "global::RootDependencyProject.IService", "global::Dependency1Project.Service4")]
                    )]
            )]
    );
}