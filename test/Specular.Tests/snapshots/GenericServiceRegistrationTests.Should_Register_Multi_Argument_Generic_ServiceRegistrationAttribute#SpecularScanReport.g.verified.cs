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
                    new("TestProject", 
                        [
                            new(ServiceLifetime.Transient, "global::IA", "global::TwoServices"), 
                            new(ServiceLifetime.Transient, "global::IB", "global::TwoServices"), 
                            new(ServiceLifetime.Scoped, "global::IC", "global::ThreeServices"), 
                            new(ServiceLifetime.Scoped, "global::ID", "global::ThreeServices"), 
                            new(ServiceLifetime.Scoped, "global::IE", "global::ThreeServices"), 
                            new(ServiceLifetime.Scoped, "global::ThreeServices", "global::ThreeServices"), 
                            new(ServiceLifetime.Transient, "global::TwoServices", "global::TwoServices")]
                    )]
            )]
    );
}