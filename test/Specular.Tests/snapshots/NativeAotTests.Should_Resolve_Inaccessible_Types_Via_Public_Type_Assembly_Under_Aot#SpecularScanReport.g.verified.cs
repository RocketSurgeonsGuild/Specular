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
            new("TestProject", "z => z.FromAssemblyOf<AotLib.IThing>().AddClasses(x => x.AssignableTo<AotLib.IThing>()).AsSelf().WithSingletonLifetime()", 
                [
                    new("AotLibrary", 
                        [
                            new(ServiceLifetime.Singleton, "global::AotLib.InternalThing", "global::AotLib.InternalThing"), 
                            new(ServiceLifetime.Singleton, "global::AotLib.PublicThing", "global::AotLib.PublicThing")]
                    )]
            )]
    );
}