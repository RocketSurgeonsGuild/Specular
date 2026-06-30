#:package Microsoft.VisualStudio.SolutionPersistence
#:package Sourcy.Git
#:package Sourcy.DotNet
#:package Rocket.Surgery.ModularPipelines.Extensions
#:include ./Samples.cs
#:property ImportConventions=true
#:property JsonSerializerIsReflectionEnabledByDefault=true
#:property ExperimentalFileBasedProgramEnableTransitiveDirectives=true

using System.Collections.Immutable;
using Build;
using ModularPipelines;
using ModularPipelines.Context;
using ModularPipelines.Models;
using ModularPipelines.Modules;
using ModularPipelines.Plugins;
using Rocket.Surgery.ModularPipelines.Extensions;
using Rocket.Surgery.ModularPipelines.Extensions.Modules;

var pipelineBuilder = Pipeline.CreateBuilder(args);
PluginRegistry.Register(new ConventionsPlugin(ConventionContextBuilder.Create(Imports.Instance)
.AddIfMissing(nameof(MyAssembly.Project.BuildScriptsRoot), MyAssembly.Project.BuildScriptsRoot), m => m != typeof(RemoveUnusedDependenciesModule)));
try
{
    await pipelineBuilder.Build().RunAsync();
}
catch (AggregateException ex)
{
}

// class GenerateApiDocs : Module<ImmutableList<CommandResult>>
// {
//     protected override async Task<ImmutableList<CommandResult>> ExecuteAsync(IModuleContext context, CancellationToken cancellationToken)
//     {

//         var result = await context.SubModule("Docs", async () =>
//         {
//             return await context.DotNet().Run(
//                 new DotNetRunOptions
//                 {
//                     ProjectSolution = new File(MyAssembly.Project.BuildScriptsRoot, "Docs/Docs.csproj"),
//                     Configuration = "Release",
//                     Arguments = ["--no-build", "--no-restore", "--", "generate"]
//                 },
//                 new CommandExecutionOptions(),
//                 cancellationToken);
//         });
//         return [.. result];
//     }
// }
