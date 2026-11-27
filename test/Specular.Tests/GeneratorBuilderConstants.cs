using System.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Rocket.Surgery.Extensions.Testing.SourceGenerators;
using Specular.Analyzers;

namespace Specular.Tests;

internal static class GeneratorBuilderConstants
{
    public static GeneratorTestContextBuilder Builder { get; } = GeneratorTestContextBuilder
                                                                .Create()
                                                                .WithGenerator<SpecularProviderGenerator>()
                                                                .AddReferences(
                                                                     typeof(ActivatorUtilities).Assembly,
                                                                     typeof(IServiceProvider).Assembly,
                                                                     typeof(IServiceCollection).Assembly,
                                                                     typeof(ServiceCollection).Assembly,
                                                                     typeof(ServiceRegistrationAttribute).Assembly,
                                                                     typeof(EditorBrowsableAttribute).Assembly,
                                                                     typeof(Attribute).Assembly
                                                                 )
                 .AddGlobalOption("build_property.SpecularEmitProvider", "true")
                 // The .NET SDK suppresses these by default (NoWarn 1701;1702). The net10.0 tests run with a newer
                 // Microsoft.Extensions.DependencyInjection.Abstractions (from DryIoc.Microsoft.DependencyInjection) than Specular was built against.
                 .WithDiagnosticOption("CS1701", ReportDiagnostic.Suppress)
                 .WithDiagnosticOption("CS1702", ReportDiagnostic.Suppress);
}
