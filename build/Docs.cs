using Microsoft.Extensions.Logging;
using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.FileSystem;
using ModularPipelines.Modules;
using Rocket.Surgery.ModularPipelines.Extensions;
using Rocket.Surgery.ModularPipelines.Extensions.Mise;
using Rocket.Surgery.ModularPipelines.Extensions.Modules;

namespace Build;

/// <summary>
///     Builds the docs site (<c>mise run docs:build</c>) into <c>artifacts/docs</c> as soon as the solution is built, alongside the tests.
/// </summary>
/// <remarks>
///     It stands in for the library's <see cref="DocsModule" />, which Build.cs leaves out. That one also waits for the coverage report
///     so it can copy the coverage badges into the site, which put the docs build after the tests, on the pipeline's critical path. The
///     Specular docs don't show the badges. Drop this module once the library builds the docs without waiting for coverage.
/// </remarks>
[DependsOn<BuildSolution>]
public class SiteDocsModule(DocsModule.Settings settings, ArtifactSettings artifactSettings) : Module<DocsModule.Result?>
{
    protected override void Configure(ModuleConfigurationBuilder module) => module.WithSkipWhen(
        _ => SkipDecision.When(!settings.DocsEnabled || !settings.DocsDirectory.Exists, "Docs directory does not exist, skipping docs build")
    );

    protected override async Task<DocsModule.Result?> ExecuteAsync(IModuleContext context, CancellationToken cancellationToken)
    {
        var result = await context.Tools.Mise.Run(settings.DocsBuildTask, new() { WorkingDirectory = settings.DocsDirectory }, cancellationToken);

        var docsOutputDirectory = artifactSettings.ArtifactsDirectory / "docs";
        await settings.DocsOutputDirectory.CopyToAsync(docsOutputDirectory, cancellationToken: cancellationToken);
        return result.ExitCode == 0 ? new DocsModule.Result(docsOutputDirectory) : null;
    }
}
