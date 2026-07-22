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
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.InNamespaces(\"TestAssembly\")).AsSelf().AsImplementedInterfaces().WithScopedLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Scoped, "global::System.IEquatable<global::TestAssembly.Nested+MyRecord>", "global::TestAssembly.Nested+MyRecord"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.GenericService", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.GenericServiceB", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.Decimal>", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.Int32>", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.String>", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.String>", "global::TestAssembly.Nested+GenericServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.Nested+GenericServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IRequest<global::TestAssembly.Response>", "global::TestAssembly.Request"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IRequestHandler<global::TestAssembly.Request,global::TestAssembly.Response>", "global::TestAssembly.RequestHandler"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.Nested+ServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.ServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IServiceB", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IValidator", "global::TestAssembly.Nested+Validator"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IValidator<global::TestAssembly.Nested+MyRecord>", "global::TestAssembly.Nested+Validator"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Nested+GenericServiceA", "global::TestAssembly.Nested+GenericServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Nested+MyRecord", "global::TestAssembly.Nested+MyRecord"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Nested+ServiceA", "global::TestAssembly.Nested+ServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Nested+Validator", "global::TestAssembly.Nested+Validator"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Request", "global::TestAssembly.Request"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.RequestHandler", "global::TestAssembly.RequestHandler"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Response", "global::TestAssembly.Response"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Service", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.ServiceB", "global::TestAssembly.ServiceB")]
                    )]
            )]
    );
}