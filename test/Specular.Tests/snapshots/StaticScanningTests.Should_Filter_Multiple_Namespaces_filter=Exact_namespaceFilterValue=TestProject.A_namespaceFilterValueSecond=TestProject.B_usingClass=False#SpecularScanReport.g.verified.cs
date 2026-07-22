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
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.InExactNamespaces(\"TestProject.A\", \"TestProject.B\")).AsSelf().AsImplementedInterfaces().WithScopedLifetime()", 
                [
                    new("TestProject", 
                        [
                            new(ServiceLifetime.Scoped, "global::TestProject.A.IService", "global::TestProject.A.Nested+ServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestProject.A.IService", "global::TestProject.A.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestProject.A.IService", "global::TestProject.B.ServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestProject.A.Nested+ServiceA", "global::TestProject.A.Nested+ServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestProject.A.Service", "global::TestProject.A.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestProject.B.IServiceB", "global::TestProject.A.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestProject.B.ServiceB", "global::TestProject.B.ServiceB")]
                    )]
            )]
    );
}