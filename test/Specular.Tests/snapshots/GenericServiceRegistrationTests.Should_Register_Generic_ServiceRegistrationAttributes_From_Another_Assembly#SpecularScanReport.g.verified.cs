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
            new("Specular", "z => z.FromAssemblies().AddClasses(f => f.WithAnyAttribute(typeof(ServiceRegistrationAttribute), typeof(ServiceRegistrationAttribute<, >), typeof(ServiceRegistrationAttribute<,, >), typeof(ServiceRegistrationAttribute<,, >), typeof(ServiceRegistrationAttribute<,,, >), typeof(ServiceRegistrationAttribute<,,,, >), typeof(ServiceRegistrationAttribute<,,,,, >), typeof(ServiceRegistrationAttribute<,,,,,, >), typeof(ServiceRegistrationAttribute<,,,,,,, >), typeof(KeyedServiceRegistrationAttribute), typeof(KeyedServiceRegistrationAttribute<>), typeof(KeyedServiceRegistrationAttribute<, >), typeof(KeyedServiceRegistrationAttribute<,, >), typeof(KeyedServiceRegistrationAttribute<,,, >), typeof(KeyedServiceRegistrationAttribute<,,,, >), typeof(KeyedServiceRegistrationAttribute<,,,,, >), typeof(KeyedServiceRegistrationAttribute<,,,,,, >), typeof(KeyedServiceRegistrationAttribute<,,,,,,, >))).AsSelf().WithSingletonLifetime()", 
                [
                    new("GenericLibrary", 
                        [
                            new(ServiceLifetime.Scoped, "global::GenericLib.IOther", "global::GenericLib.InternalThing"), 
                            new(ServiceLifetime.Transient, "global::GenericLib.IOther", "global::GenericLib.MixedThing"), 
                            new(ServiceLifetime.Transient, "global::GenericLib.IThing", "global::GenericLib.MixedThing"), 
                            new(ServiceLifetime.Singleton, "global::GenericLib.IThing", "global::GenericLib.PublicThing"), 
                            new(ServiceLifetime.Transient, "global::GenericLib.IThird", "global::GenericLib.MixedThing"), 
                            new(ServiceLifetime.Scoped, "global::GenericLib.InternalThing", "global::GenericLib.InternalThing"), 
                            new(ServiceLifetime.Transient, "global::GenericLib.MixedThing", "global::GenericLib.MixedThing"), 
                            new(ServiceLifetime.Singleton, "global::GenericLib.PublicThing", "global::GenericLib.PublicThing")]
                    )]
            )]
    );
}