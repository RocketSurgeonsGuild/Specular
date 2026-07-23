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
            new("Specular", "z => z.FromAssemblies().AddClasses(f => f.WithAnyAttribute(typeof(ServiceRegistrationAttribute), typeof(ServiceRegistrationAttribute<, >), typeof(ServiceRegistrationAttribute<,, >), typeof(ServiceRegistrationAttribute<,, >), typeof(ServiceRegistrationAttribute<,,, >), typeof(ServiceRegistrationAttribute<,,,, >), typeof(ServiceRegistrationAttribute<,,,,, >), typeof(ServiceRegistrationAttribute<,,,,,, >), typeof(ServiceRegistrationAttribute<,,,,,,, >))).AsSelf().WithSingletonLifetime()", 
                [
                    new("TestProject", 
                        [
                            new(ServiceLifetime.Transient, "global::IService", "global::Nested+ServiceA"), 
                            new(ServiceLifetime.Singleton, "global::IService", "global::ServiceB"), 
                            new(ServiceLifetime.Scoped, "global::IServiceB", "global::Service"), 
                            new(ServiceLifetime.Singleton, "global::IServiceB", "global::ServiceB"), 
                            new(ServiceLifetime.Transient, "global::Nested+ServiceA", "global::Nested+ServiceA"), 
                            new(ServiceLifetime.Scoped, "global::Service", "global::Service"), 
                            new(ServiceLifetime.Singleton, "global::ServiceB", "global::ServiceB")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(f => f.WithAnyAttribute(typeof(ServiceRegistrationAttribute), typeof(ServiceRegistrationAttribute<, >), typeof(ServiceRegistrationAttribute<,, >), typeof(ServiceRegistrationAttribute<,, >), typeof(ServiceRegistrationAttribute<,,, >))).AsSelf().WithSingletonLifetime()", 
                [
                    new("TestProject", 
                        [
                            new(ServiceLifetime.Transient, "global::IService", "global::Nested+ServiceA"), 
                            new(ServiceLifetime.Singleton, "global::IService", "global::ServiceB"), 
                            new(ServiceLifetime.Scoped, "global::IServiceB", "global::Service"), 
                            new(ServiceLifetime.Singleton, "global::IServiceB", "global::ServiceB"), 
                            new(ServiceLifetime.Transient, "global::Nested+ServiceA", "global::Nested+ServiceA"), 
                            new(ServiceLifetime.Scoped, "global::Service", "global::Service"), 
                            new(ServiceLifetime.Singleton, "global::ServiceB", "global::ServiceB")]
                    )]
            )]
    );
}