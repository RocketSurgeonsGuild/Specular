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
        [
            new("TestProject", "z => z.FromAssemblies()", 
                [
                    new("Specular"), 
                    new("System.Collections.Immutable"), 
                    new("System.ComponentModel"), 
                    new("System.Runtime.Serialization.Formatters"), 
                    new("TestAssembly"), 
                    new("TestProject")]
            ), 
            new("TestProject", "z => z.FromAssemblies().IncludeSystemAssemblies()", 
                [
                    new("Specular"), 
                    new("System"), 
                    new("System.Collections.Immutable"), 
                    new("System.ComponentModel"), 
                    new("System.Core"), 
                    new("System.Private.CoreLib"), 
                    new("System.Runtime"), 
                    new("System.Runtime.Serialization.Formatters"), 
                    new("TestAssembly"), 
                    new("TestProject"), 
                    new("mscorlib"), 
                    new("netstandard")]
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>()", 
                [
                    new("System.Collections.Immutable"), 
                    new("System.ComponentModel"), 
                    new("System.Runtime.Serialization.Formatters"), 
                    new("TestAssembly"), 
                    new("TestProject")]
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<IService>()", 
                [
                    new("Specular"), 
                    new("System.Collections.Immutable"), 
                    new("System.ComponentModel"), 
                    new("System.Runtime.Serialization.Formatters"), 
                    new("TestProject")]
            ), 
            new("TestProject", "z => z.EntryAssembly()", 
                [
                    new("TestProject")]
            ), 
            new("TestProject", "z => z.DependenciesFromAssemblyOf<ServiceRegistrationAttribute>()", 
                [
                    new("TestAssembly"), 
                    new("TestProject")]
            ), 
            new("TestProject", "z => z.DependenciesFromAssemblyOf<IService>()", 
                [
                    new("TestProject")]
            )]
    , 
        [
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().NotKindOf(TypeKindFilter.Delegate, TypeKindFilter.Class).NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("TestAssembly", 
                        [
                            new("global::TestAssembly.IGenericService<>"), 
                            new("global::TestAssembly.IOther"), 
                            new("global::TestAssembly.IRequest<>"), 
                            new("global::TestAssembly.IRequestHandler<,>"), 
                            new("global::TestAssembly.IService"), 
                            new("global::TestAssembly.IServiceB"), 
                            new("global::TestAssembly.IValidator"), 
                            new("global::TestAssembly.IValidator<>")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().NotKindOf(TypeKindFilter.Delegate, TypeKindFilter.Class, TypeKindFilter.Enum).NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("TestAssembly", 
                        [
                            new("global::TestAssembly.IGenericService<>"), 
                            new("global::TestAssembly.IOther"), 
                            new("global::TestAssembly.IRequest<>"), 
                            new("global::TestAssembly.IRequestHandler<,>"), 
                            new("global::TestAssembly.IService"), 
                            new("global::TestAssembly.IServiceB"), 
                            new("global::TestAssembly.IValidator"), 
                            new("global::TestAssembly.IValidator<>")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().NotKindOf(TypeKindFilter.Delegate).NotKindOf(TypeKindFilter.Interface).NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
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
                    new("System.Runtime.Serialization.Formatters", 
                        [
                            new("global::FxResources.System.Runtime.Serialization.Formatters.SR")]
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
                            new("global::Microsoft.CodeAnalysis.EmbeddedAttribute"), 
                            new("global::Program")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().InfoOf(TypeInfoFilter.Abstract).NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("TestAssembly", 
                        [
                            new("global::TestAssembly.IGenericService<>"), 
                            new("global::TestAssembly.IOther"), 
                            new("global::TestAssembly.IRequest<>"), 
                            new("global::TestAssembly.IRequestHandler<,>"), 
                            new("global::TestAssembly.IService"), 
                            new("global::TestAssembly.IServiceB"), 
                            new("global::TestAssembly.IValidator"), 
                            new("global::TestAssembly.IValidator<>")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().InfoOf(TypeInfoFilter.Visible).NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("TestAssembly", 
                        [
                            new("global::TestAssembly.GenericServiceB"), 
                            new("global::TestAssembly.IGenericService<>"), 
                            new("global::TestAssembly.IOther"), 
                            new("global::TestAssembly.IRequest<>"), 
                            new("global::TestAssembly.IRequestHandler<,>"), 
                            new("global::TestAssembly.IService"), 
                            new("global::TestAssembly.IServiceB"), 
                            new("global::TestAssembly.IValidator"), 
                            new("global::TestAssembly.IValidator<>"), 
                            new("global::TestAssembly.Nested"), 
                            new("global::TestAssembly.Nested+GenericServiceA"), 
                            new("global::TestAssembly.Nested+ServiceA"), 
                            new("global::TestAssembly.Service")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().InfoOf(TypeInfoFilter.Sealed).NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("TestAssembly", 
                        [
                            new("global::Microsoft.CodeAnalysis.EmbeddedAttribute")]
                    ), 
                    new("TestProject", 
                        [
                            new("global::Microsoft.CodeAnalysis.EmbeddedAttribute")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().InfoOf(TypeInfoFilter.Static).NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("System.Collections.Immutable", 
                        [
                            new("global::FxResources.System.Collections.Immutable.SR")]
                    ), 
                    new("System.Runtime.Serialization.Formatters", 
                        [
                            new("global::FxResources.System.Runtime.Serialization.Formatters.SR")]
                    ), 
                    new("TestAssembly", 
                        [
                            new("global::TestAssembly.Nested")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().NotInfoOf(TypeInfoFilter.Static).NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("TestAssembly", 
                        [
                            new("global::Microsoft.CodeAnalysis.EmbeddedAttribute"), 
                            new("global::TestAssembly.GenericService"), 
                            new("global::TestAssembly.GenericServiceB"), 
                            new("global::TestAssembly.IGenericService<>"), 
                            new("global::TestAssembly.IOther"), 
                            new("global::TestAssembly.IRequest<>"), 
                            new("global::TestAssembly.IRequestHandler<,>"), 
                            new("global::TestAssembly.IService"), 
                            new("global::TestAssembly.IServiceB"), 
                            new("global::TestAssembly.IValidator"), 
                            new("global::TestAssembly.IValidator<>"), 
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
                            new("global::Microsoft.CodeAnalysis.EmbeddedAttribute"), 
                            new("global::Program")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().InfoOf(TypeInfoFilter.GenericType).NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("TestAssembly", 
                        [
                            new("global::TestAssembly.IGenericService<>"), 
                            new("global::TestAssembly.IRequest<>"), 
                            new("global::TestAssembly.IRequestHandler<,>"), 
                            new("global::TestAssembly.IValidator<>")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().NotInfoOf(TypeInfoFilter.Abstract).NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("System.Collections.Immutable", 
                        [
                            new("global::FxResources.System.Collections.Immutable.SR")]
                    ), 
                    new("System.Runtime.Serialization.Formatters", 
                        [
                            new("global::FxResources.System.Runtime.Serialization.Formatters.SR")]
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
                            new("global::Microsoft.CodeAnalysis.EmbeddedAttribute"), 
                            new("global::Program")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().NotInfoOf(TypeInfoFilter.Visible).NotAssignableTo<Attribute>().NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("System.Collections.Immutable", 
                        [
                            new("global::FxResources.System.Collections.Immutable.SR")]
                    ), 
                    new("System.Runtime.Serialization.Formatters", 
                        [
                            new("global::FxResources.System.Runtime.Serialization.Formatters.SR")]
                    ), 
                    new("TestAssembly", 
                        [
                            new("global::TestAssembly.GenericService"), 
                            new("global::TestAssembly.Nested+MyRecord"), 
                            new("global::TestAssembly.Nested+Validator"), 
                            new("global::TestAssembly.Request"), 
                            new("global::TestAssembly.RequestHandler"), 
                            new("global::TestAssembly.Response"), 
                            new("global::TestAssembly.ServiceB")]
                    ), 
                    new("TestProject", 
                        [
                            new("global::Program")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().DependenciesFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.WithAnyAttribute(typeof(ServiceRegistrationAttribute)))", 
                [
                    new("TestAssembly", 
                        [
                            new("global::TestAssembly.GenericService"), 
                            new("global::TestAssembly.GenericServiceB"), 
                            new("global::TestAssembly.RequestHandler")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().NotInfoOf(TypeInfoFilter.ValueType).NotAssignableTo<Attribute>().NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("System.Collections.Immutable", 
                        [
                            new("global::FxResources.System.Collections.Immutable.SR")]
                    ), 
                    new("System.Runtime.Serialization.Formatters", 
                        [
                            new("global::FxResources.System.Runtime.Serialization.Formatters.SR")]
                    ), 
                    new("TestAssembly", 
                        [
                            new("global::TestAssembly.GenericService"), 
                            new("global::TestAssembly.GenericServiceB"), 
                            new("global::TestAssembly.IGenericService<>"), 
                            new("global::TestAssembly.IOther"), 
                            new("global::TestAssembly.IRequest<>"), 
                            new("global::TestAssembly.IRequestHandler<,>"), 
                            new("global::TestAssembly.IService"), 
                            new("global::TestAssembly.IServiceB"), 
                            new("global::TestAssembly.IValidator"), 
                            new("global::TestAssembly.IValidator<>"), 
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
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().NotInfoOf(TypeInfoFilter.Sealed).NotAssignableTo<Attribute>().NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("System.Collections.Immutable", 
                        [
                            new("global::FxResources.System.Collections.Immutable.SR")]
                    ), 
                    new("System.Runtime.Serialization.Formatters", 
                        [
                            new("global::FxResources.System.Runtime.Serialization.Formatters.SR")]
                    ), 
                    new("TestAssembly", 
                        [
                            new("global::TestAssembly.GenericService"), 
                            new("global::TestAssembly.GenericServiceB"), 
                            new("global::TestAssembly.IGenericService<>"), 
                            new("global::TestAssembly.IOther"), 
                            new("global::TestAssembly.IRequest<>"), 
                            new("global::TestAssembly.IRequestHandler<,>"), 
                            new("global::TestAssembly.IService"), 
                            new("global::TestAssembly.IServiceB"), 
                            new("global::TestAssembly.IValidator"), 
                            new("global::TestAssembly.IValidator<>"), 
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
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().NotInfoOf(TypeInfoFilter.GenericType).NotAssignableTo<Attribute>().NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("System.Collections.Immutable", 
                        [
                            new("global::FxResources.System.Collections.Immutable.SR")]
                    ), 
                    new("System.Runtime.Serialization.Formatters", 
                        [
                            new("global::FxResources.System.Runtime.Serialization.Formatters.SR")]
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
            ), 
            new("TestProject", "z => z.FromAssemblies().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().WithAttribute(typeof(EditorBrowsableAttribute)).NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("Specular", 
                        [
                            new("global::Specular.SpecularSupport")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().WithAttribute<EditorBrowsableAttribute>().NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("Specular", 
                        [
                            new("global::Specular.SpecularSupport")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().WithoutAttribute(typeof(EditorBrowsableAttribute)).NotAssignableTo(typeof(Attribute)).NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("System.Collections.Immutable", 
                        [
                            new("global::FxResources.System.Collections.Immutable.SR")]
                    ), 
                    new("System.Runtime.Serialization.Formatters", 
                        [
                            new("global::FxResources.System.Runtime.Serialization.Formatters.SR")]
                    ), 
                    new("TestAssembly", 
                        [
                            new("global::TestAssembly.GenericService"), 
                            new("global::TestAssembly.GenericServiceB"), 
                            new("global::TestAssembly.IGenericService<>"), 
                            new("global::TestAssembly.IOther"), 
                            new("global::TestAssembly.IRequest<>"), 
                            new("global::TestAssembly.IRequestHandler<,>"), 
                            new("global::TestAssembly.IService"), 
                            new("global::TestAssembly.IServiceB"), 
                            new("global::TestAssembly.IValidator"), 
                            new("global::TestAssembly.IValidator<>"), 
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
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(x => x.NotAssignableTo<ISpecularProvider>().WithoutAttribute<EditorBrowsableAttribute>().NotAssignableTo<Attribute>().NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("System.Collections.Immutable", 
                        [
                            new("global::FxResources.System.Collections.Immutable.SR")]
                    ), 
                    new("System.Runtime.Serialization.Formatters", 
                        [
                            new("global::FxResources.System.Runtime.Serialization.Formatters.SR")]
                    ), 
                    new("TestAssembly", 
                        [
                            new("global::TestAssembly.GenericService"), 
                            new("global::TestAssembly.GenericServiceB"), 
                            new("global::TestAssembly.IGenericService<>"), 
                            new("global::TestAssembly.IOther"), 
                            new("global::TestAssembly.IRequest<>"), 
                            new("global::TestAssembly.IRequestHandler<,>"), 
                            new("global::TestAssembly.IService"), 
                            new("global::TestAssembly.IServiceB"), 
                            new("global::TestAssembly.IValidator"), 
                            new("global::TestAssembly.IValidator<>"), 
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
            ), 
            new("TestProject", "z => z.FromAssemblies().NotFromAssemblyOf<ServiceRegistrationAttribute>().GetTypes(true, x => x.NotAssignableTo<ISpecularProvider>().NotInNamespaces(\"JetBrains.Annotations\", \"Polyfills\", \"System\").NotStartsWith(\"Polyfill\"))", 
                [
                    new("TestAssembly", 
                        [
                            new("global::TestAssembly.GenericServiceB"), 
                            new("global::TestAssembly.IGenericService<>"), 
                            new("global::TestAssembly.IOther"), 
                            new("global::TestAssembly.IRequest<>"), 
                            new("global::TestAssembly.IRequestHandler<,>"), 
                            new("global::TestAssembly.IService"), 
                            new("global::TestAssembly.IServiceB"), 
                            new("global::TestAssembly.IValidator"), 
                            new("global::TestAssembly.IValidator<>"), 
                            new("global::TestAssembly.Nested"), 
                            new("global::TestAssembly.Nested+GenericServiceA"), 
                            new("global::TestAssembly.Nested+ServiceA"), 
                            new("global::TestAssembly.Service")]
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
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableTo<IServiceB>(), false).AsSelf().AsImplementedInterfaces().WithScopedLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IServiceB", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Service", "global::TestAssembly.Service")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableTo<IService>(), false).AsSelf().AsImplementedInterfaces().WithSingletonLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IService", "global::TestAssembly.Nested+ServiceA"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IService", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IService", "global::TestAssembly.ServiceB"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IServiceB", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.Nested+ServiceA", "global::TestAssembly.Nested+ServiceA"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.Service", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.ServiceB", "global::TestAssembly.ServiceB")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableTo<IService>()).As<IService>().WithScopedLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.Nested+ServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.ServiceB")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableTo<IService>()).AsSelf().AsImplementedInterfaces().WithSingletonLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IService", "global::TestAssembly.Nested+ServiceA"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IService", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IService", "global::TestAssembly.ServiceB"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IServiceB", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.Nested+ServiceA", "global::TestAssembly.Nested+ServiceA"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.Service", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.ServiceB", "global::TestAssembly.ServiceB")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableTo<IService>()).AsSelf().AsImplementedInterfaces().WithScopedLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.Nested+ServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.ServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IServiceB", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Nested+ServiceA", "global::TestAssembly.Nested+ServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Service", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.ServiceB", "global::TestAssembly.ServiceB")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableTo(typeof(IGenericService<>))).AsSelfWithInterfaces().WithScopedLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Scoped, "global::TestAssembly.GenericService", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.GenericServiceB", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.Decimal>", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.Int32>", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.String>", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.String>", "global::TestAssembly.Nested+GenericServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.Nested+GenericServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Nested+GenericServiceA", "global::TestAssembly.Nested+GenericServiceA")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableTo(typeof(IGenericService<>))).AsSelf().AsImplementedInterfaces().WithScopedLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Scoped, "global::TestAssembly.GenericService", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.GenericServiceB", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.Decimal>", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.Int32>", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.String>", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.String>", "global::TestAssembly.Nested+GenericServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.Nested+GenericServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Nested+GenericServiceA", "global::TestAssembly.Nested+GenericServiceA")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableTo(typeof(IRequestHandler<, >))).AsImplementedInterfaces().WithSingletonLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Singleton, "global::TestAssembly.IRequestHandler<global::TestAssembly.Request,global::TestAssembly.Response>", "global::TestAssembly.RequestHandler"), 
                            new(ServiceLifetime.Singleton, "global::TestAssembly.RequestHandler", "global::TestAssembly.RequestHandler")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableTo<IService>().AssignableTo<IServiceB>()).AsSelf().AsImplementedInterfaces().WithScopedLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IServiceB", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Service", "global::TestAssembly.Service")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableToAny(typeof(IService), typeof(IServiceB))).AsSelf().AsImplementedInterfaces().WithScopedLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.Nested+ServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.ServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IServiceB", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Nested+ServiceA", "global::TestAssembly.Nested+ServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Service", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.ServiceB", "global::TestAssembly.ServiceB")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableToAny(typeof(IService), typeof(IServiceB))).AsSelf().AsImplementedInterfaces(z => z.AssignableTo<IServiceB>()).WithScopedLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IServiceB", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Nested+ServiceA", "global::TestAssembly.Nested+ServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Service", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.ServiceB", "global::TestAssembly.ServiceB")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableToAny(typeof(IOther))).AsSelf().AsImplementedInterfaces(z => z.AssignableTo(typeof(IGenericService<>))).WithScopedLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Scoped, "global::TestAssembly.GenericService", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.GenericServiceB", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.Decimal>", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.Int32>", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.String>", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.String>", "global::TestAssembly.Nested+GenericServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Nested+GenericServiceA", "global::TestAssembly.Nested+GenericServiceA")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableToAny(typeof(IValidator))).AsImplementedInterfaces(z => z.AssignableTo(typeof(IValidator<>))).WithScopedLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IValidator<global::TestAssembly.Nested+MyRecord>", "global::TestAssembly.Nested+Validator")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableToAny(typeof(IService))).As<IService>().WithScopedLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.Nested+ServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.Service"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IService", "global::TestAssembly.ServiceB")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableToAny(typeof(IOther))).AsSelf().As(typeof(IGenericService<>)).WithScopedLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Scoped, "global::TestAssembly.GenericService", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.GenericServiceB", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.Decimal>", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.Int32>", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.String>", "global::TestAssembly.Nested+GenericServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.Nested+GenericServiceA", "global::TestAssembly.Nested+GenericServiceA")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(x => x.AssignableToAny(typeof(IOther))).As(typeof(IGenericService<>)).WithScopedLifetime()", 
                [
                    new("TestAssembly", 
                        [
                            new(ServiceLifetime.Scoped, "global::TestAssembly.GenericService", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.GenericServiceB", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.Decimal>", "global::TestAssembly.GenericServiceB"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.Int32>", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IGenericService<global::System.String>", "global::TestAssembly.Nested+GenericServiceA"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.GenericService"), 
                            new(ServiceLifetime.Scoped, "global::TestAssembly.IOther", "global::TestAssembly.GenericServiceB")]
                    )]
            ), 
            new("TestProject", "z => z.FromAssemblies().AddClasses(f => f.WithAnyAttribute(typeof(ServiceRegistrationAttribute), typeof(ServiceRegistrationAttribute<, >), typeof(ServiceRegistrationAttribute<,, >), typeof(ServiceRegistrationAttribute<,, >), typeof(ServiceRegistrationAttribute<,,, >), typeof(ServiceRegistrationAttribute<,,,, >), typeof(ServiceRegistrationAttribute<,,,,, >), typeof(ServiceRegistrationAttribute<,,,,,, >), typeof(ServiceRegistrationAttribute<,,,,,,, >))).AsSelf().WithSingletonLifetime()", 
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