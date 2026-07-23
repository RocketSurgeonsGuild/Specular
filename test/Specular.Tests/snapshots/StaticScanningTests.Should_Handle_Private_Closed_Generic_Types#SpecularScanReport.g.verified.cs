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
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableTo(typeof(IRequestHandler<, >))).AsImplementedInterfaces().WithSingletonLifetime()", 
                [
                    new("RootDependencyProject", 
                        [
                            new(ServiceLifetime.Singleton, "global::RootDependencyProject.IRequestHandler<global::RootDependencyProject.Request,global::RootDependencyProject.Response>", "global::RootDependencyProject.RequestHandler")]
                    )]
            )]
    );
}