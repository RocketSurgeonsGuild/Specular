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
        [
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().NotInfoOf(TypeInfoFilter.GenericType).NotAssignableTo<Attribute>().NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("TestProject", 
                        [
                            new("global::Program")]
                    )]
            ), 
            new("OtherProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().NotInfoOf(TypeInfoFilter.GenericType).NotAssignableTo<Attribute>().NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("OtherProject", 
                        [
                            new("global::Program"), 
                            new("global::SpecularScanReport")]
                    ), 
                    new("System.Collections.Immutable", 
                        [
                            new("global::FxResources.System.Collections.Immutable.SR")]
                    ), 
                    new("TestAssembly", 
                        [
                            new("global::TestAssembly.GenericService"), 
                            new("global::TestAssembly.GenericServiceB"), 
                            new("global::TestAssembly.IOther"), 
                            new("global::TestAssembly.IService"), 
                            new("global::TestAssembly.IServiceB"), 
                            new("global::TestAssembly.IValidator"), 
                            new("global::TestAssembly.Nested"), 
                            new("global::TestAssembly.Nested+GenericServiceA"), 
                            new("global::TestAssembly.Nested+MyRecord"), 
                            new("global::TestAssembly.Nested+ServiceA"), 
                            new("global::TestAssembly.Nested+Validator"), 
                            new("global::TestAssembly.Request"), 
                            new("global::TestAssembly.RequestHandler"), 
                            new("global::TestAssembly.Response"), 
                            new("global::TestAssembly.Service"), 
                            new("global::TestAssembly.ServiceB")]
                    ), 
                    new("TestProject", 
                        [
                            new("global::Program")]
                    )]
            )]
    , 
        [
            new("Specular", "z => z.FromAssemblies().AddClasses(f => f.WithAnyAttribute(typeof(ServiceRegistrationAttribute), typeof(ServiceRegistrationAttribute<, >), typeof(ServiceRegistrationAttribute<,, >), typeof(ServiceRegistrationAttribute<,, >), typeof(ServiceRegistrationAttribute<,,, >), typeof(ServiceRegistrationAttribute<,,,, >), typeof(ServiceRegistrationAttribute<,,,,, >), typeof(ServiceRegistrationAttribute<,,,,,, >), typeof(ServiceRegistrationAttribute<,,,,,,, >))).AsSelf().WithSingletonLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Singleton, "global::TestAssembly.GenericService", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.GenericServiceB", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IGenericService<global::System.Decimal>", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IGenericService<global::System.Int32>", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IOther", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IOther", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IRequestHandler<global::TestAssembly.Request,global::TestAssembly.Response>", "global::TestAssembly.RequestHandler"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.RequestHandler", "global::TestAssembly.RequestHandler")]
                    )]
            )]
    );
}