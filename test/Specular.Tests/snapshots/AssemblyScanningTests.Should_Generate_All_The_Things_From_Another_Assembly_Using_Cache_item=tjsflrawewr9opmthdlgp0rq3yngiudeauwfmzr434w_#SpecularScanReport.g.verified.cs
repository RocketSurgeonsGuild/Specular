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
            new("OtherProject", "z => z.FromAssemblies().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().NotKindOf(TypeKindFilter.Delegate).NotKindOf(TypeKindFilter.Interface).NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("OtherProject", 
                        [
                            new("global::Microsoft.CodeAnalysis.EmbeddedAttribute"), 
                            new("global::Program"), 
                            new("global::SpecularScanReport")]
                    ), 
                    new("Specular", 
                        [
                            new("global::Microsoft.CodeAnalysis.EmbeddedAttribute"), 
                            new("global::Specular.Abstractions.ExcludeFromSpecularAttribute"), 
                            new("{scrubbed}"), 
                            new("global::Specular.Abstractions.TypeInfoFilter"), 
                            new("global::Specular.Abstractions.TypeKindFilter"), 
                            new("global::Specular.Diagnostics.AssemblyScanReport"), 
                            new("global::Specular.Diagnostics.AssemblyScanReportEntry"), 
                            new("global::Specular.Diagnostics.ScanResults"), 
                            new("global::Specular.Diagnostics.ServiceDescriptorAssemblyScanReportEntry"), 
                            new("global::Specular.Diagnostics.ServiceDescriptorScanReport"), 
                            new("global::Specular.Diagnostics.ServiceDescriptorScanReportEntry"), 
                            new("global::Specular.Diagnostics.TypeScanAssemblyReportEntry"), 
                            new("global::Specular.Diagnostics.TypeScanReport"), 
                            new("global::Specular.Diagnostics.TypeScanReportEntry"), 
                            new("global::Specular.RegistrationLifetimeAttribute"), 
                            new("global::Specular.ServiceRegistrationAttribute"), 
                            new("global::Specular.ServiceRegistrationAttribute<,,,,,,,>"), 
                            new("global::Specular.ServiceRegistrationAttribute<,,,,,,>"), 
                            new("global::Specular.ServiceRegistrationAttribute<,,,,,>"), 
                            new("global::Specular.ServiceRegistrationAttribute<,,,,>"), 
                            new("global::Specular.ServiceRegistrationAttribute<,,,>"), 
                            new("global::Specular.ServiceRegistrationAttribute<,,>"), 
                            new("global::Specular.ServiceRegistrationAttribute<,>"), 
                            new("global::Specular.ServiceRegistrationAttribute<>"), 
                            new("global::Specular.SpecularProviderServiceCollectionExtensions"), 
                            new("global::Specular.SpecularSupport"), 
                            new("global::SpecularScanReport")]
                    ), 
                    new("System.Collections.Immutable", 
                        [
                            new("global::FxResources.System.Collections.Immutable.SR")]
                    ), 
                    new("TestAssembly", 
                        [
                            new("global::Microsoft.CodeAnalysis.EmbeddedAttribute"), 
                            new("global::TestAssembly.GenericService"), 
                            new("global::TestAssembly.GenericServiceB"), 
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
                            new("global::Microsoft.CodeAnalysis.EmbeddedAttribute")]
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