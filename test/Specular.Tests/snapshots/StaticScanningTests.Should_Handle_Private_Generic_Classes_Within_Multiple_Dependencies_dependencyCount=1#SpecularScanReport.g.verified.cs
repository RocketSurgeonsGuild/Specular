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
                    new("Dependency0Project", 
                        [
                            new(ServiceLifetime.Singleton, "global::RootDependencyProject.IRequestHandler<global::Dependency1Project.Request0,global::Dependency1Project.Response0>", "global::Dependency1Project.RequestHandler0")]
                    )]
            )]
    );
}