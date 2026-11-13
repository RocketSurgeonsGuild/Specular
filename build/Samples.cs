
#:package Microsoft.VisualStudio.SolutionPersistence
#:package Sourcy.Git
#:package Sourcy.DotNet
#:package Rocket.Surgery.ModularPipelines.Extensions
#:property ImportConventions=true
#:property JsonSerializerIsReflectionEnabledByDefault=true

using System.Collections.Immutable;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using ModularPipelines.Attributes;
using ModularPipelines.Configuration;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.DotNet.Options;
using ModularPipelines.FileSystem;
using ModularPipelines.Models;
using ModularPipelines.Modules;
using ModularPipelines.Options;
using Rocket.Surgery.ModularPipelines.Extensions;
using Rocket.Surgery.ModularPipelines.Extensions.Modules;
using File = ModularPipelines.FileSystem.File;

namespace Build;

[DependsOn<BuildSolution>]
// T008: shared AOT-publish helper. Each demonstration host publishes its scoped, in-csproj
// zero-warning property set (D4-pinned: PublishAot + TrimmerSingleWarn=false + TreatWarningsAsErrors)
// for each target framework × current RID. A failed publish (or any promoted IL2xxx/IL3xxx) throws ⇒ the named
// module FAILs the build; an unsatisfied toolchain SKIPs-with-reason (aot-publish-step contract).
public class AotPublishModule() : Module<ImmutableList<CommandResult>>
{

    private readonly IReadOnlyCollection<FileInfo> projects = [
        Sourcy.DotNet.Projects.Specular_Samples_Console,
        Sourcy.DotNet.Projects.Specular_Samples_Web
    ];
    private readonly IReadOnlyCollection<string> frameworks = ["net10.0", "net11.0"];
    protected override ModuleConfiguration Configure() => ModuleConfiguration
                                                         .Create()
                                                         .WithRetryCount(3)
                                                         // On CI, samples.yml publishes these same hosts for both frameworks
                                                         // (Linux and Windows, under the same in-csproj zero-warning policy) and
                                                         // also runs them. Publishing them here too only took CPU from the tests,
                                                         // which run alongside this module on the build runner.
                                                         .WithSkipWhen(() => SkipDecision.Of(
                                                             IsOnCI(),
                                                             "samples.yml publishes and runs the Native AOT hosts on CI"
                                                         ))
                                                         .Build();

    private static bool IsOnCI() =>
        Environment.GetEnvironmentVariable("CI") is not null ||
        Environment.GetEnvironmentVariable("GITHUB_ACTIONS") is not null;
    protected override async Task<ImmutableList<CommandResult>?> ExecuteAsync(IModuleContext context, CancellationToken cancellationToken)
    {
        var rid = RuntimeInformation.RuntimeIdentifier;

        // Frameworks run one after another: two publishes of the same project would race on its project.assets.json.
        var results = ImmutableList.CreateBuilder<CommandResult>();
        foreach (var framework in frameworks)
        {
            results.AddRange(await Task.WhenAll(projects.Select(project =>
            {
                var projectPath = new File(project.FullName);
                var hostname = $"{projectPath.NameWithoutExtension} ({framework})";
                return context.SubModule(hostname, async () =>
                {
                    context.Logger.LogInformation("AOT publish [{Host}] × {Rid} (zero-warning policy)", hostname, rid);

                    var result = await context.DotNet().Publish(
                        new DotNetPublishOptions
                        {
                            ProjectSolution = projectPath,
                            Configuration = "Release",
                            Framework = framework,
                            Runtime = rid,
                        },
                        new CommandExecutionOptions(),
                        cancellationToken);

                    context.Logger.LogInformation("AOT publish [{Host}] PASS — zero trim/AOT warnings.", hostname);
                    return result;
                });
            })));
        }
        return results.ToImmutable();
    }
}
