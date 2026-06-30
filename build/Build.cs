#:package Microsoft.VisualStudio.SolutionPersistence
#:package Sourcy.Git
#:package Sourcy.DotNet
#:package Rocket.Surgery.ModularPipelines.Extensions
#:property ImportConventions=true
#:property JsonSerializerIsReflectionEnabledByDefault=true

using System.Runtime.InteropServices;
using Build;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModularPipelines;
using ModularPipelines.Configuration;
using ModularPipelines.Context;
using ModularPipelines.DotNet;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.DotNet.Options;
using ModularPipelines.FileSystem;
using ModularPipelines.Models;
using ModularPipelines.Modules;
using ModularPipelines.Options;
using ModularPipelines.Plugins;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.ModularPipelines.Extensions;
using Rocket.Surgery.ModularPipelines.Extensions.Mise;

var pipelineBuilder = Pipeline.CreateBuilder(args);
PluginRegistry.Register(new ConventionsPlugin(ConventionContextBuilder.Create(Imports.Instance)
.AddIfMissing(nameof(MyAssembly.Project.BuildScriptsRoot), MyAssembly.Project.BuildScriptsRoot)));
await pipelineBuilder.Build().RunAsync();

// Repo-root + native-AOT toolchain helpers shared by every AOT-publish module (T008).
internal static class SampleBuild
{
    // The pipeline runs from the repo root; walk up to the enclosing git working tree to be robust.
    public static string RepoRoot { get; } = FindRepoRoot();

    private static string FindRepoRoot()
    {
        var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (dir is not null && !Directory.Exists(Path.Combine(dir.FullName, ".git")))
        {
            dir = dir.Parent;
        }

        return (dir ?? new DirectoryInfo(Directory.GetCurrentDirectory())).FullName;
    }

    // CI-real / local skip-with-reason (D4): genuinely publish on CI Linux; locally skip-with-reason
    // unless the operator opts in (the osx-arm64 Native AOT link needs Homebrew openssl@3/brotli on
    // the linker path). A skip NEVER reports as pass (FR-016, aot-publish-step contract rule 4).
    public static SkipDecision NativeAotSkip(string host)
    {
        if (OperatingSystem.IsLinux() || Environment.GetEnvironmentVariable("INDAGO_AOT_LOCAL") == "1")
        {
            return SkipDecision.Of(false, $"[{host}] Native AOT toolchain available.");
        }

        return SkipDecision.Skip(
            $"[{host}] Native AOT link prerequisites unsatisfied on {RuntimeInformation.RuntimeIdentifier} "
            + "(Homebrew openssl@3/brotli not on the linker path). Published for real in CI (Linux); "
            + "set INDAGO_AOT_LOCAL=1 to run locally.");
    }
}

// T008: shared AOT-publish helper. Each demonstration host publishes its scoped, in-csproj
// zero-warning property set (D4-pinned: PublishAot + TrimmerSingleWarn=false + TreatWarningsAsErrors)
// for net10.0 × current RID. A failed publish (or any promoted IL2xxx/IL3xxx) throws ⇒ the named
// module FAILs the build; an unsatisfied toolchain SKIPs-with-reason (aot-publish-step contract).
public abstract class AotPublishModule : Module<CommandResult>
{
    protected abstract string HostName { get; }

    protected abstract string ProjectPath { get; }

    protected virtual string Framework => "net10.0";

    protected override ModuleConfiguration Configure() =>
        ModuleConfiguration.Create().WithSkipWhen(() => SampleBuild.NativeAotSkip(HostName)).Build();

    protected override async Task<CommandResult?> ExecuteAsync(IModuleContext context, CancellationToken cancellationToken)
    {
        var rid = RuntimeInformation.RuntimeIdentifier;
        var project = Path.Combine(SampleBuild.RepoRoot, ProjectPath);
        context.Logger.LogInformation("AOT publish [{Host}] {Framework} × {Rid} (zero-warning policy)", HostName, Framework, rid);

        var result = await context.DotNet().Publish(
            new DotNetPublishOptions
            {
                ProjectSolution = project,
                Configuration = "Release",
                Framework = Framework,
                Runtime = rid,
            },
            new CommandExecutionOptions(),
            cancellationToken);

        context.Logger.LogInformation("AOT publish [{Host}] PASS — zero trim/AOT warnings.", HostName);
        return result;
    }
}

// T019: Console host AOT-publish named module (US3 guardrail).
public sealed class ConsoleAotPublishModule : AotPublishModule
{
    protected override string HostName => "Indago.Samples.Console";

    protected override string ProjectPath =>
        Path.Combine("samples", "hosts", "Indago.Samples.Console", "Indago.Samples.Console.csproj");
}

// T034: Web host AOT-publish named module (US4). Reuses the T008 helper + T009 property set.
public sealed class WebAotPublishModule : AotPublishModule
{
    protected override string HostName => "Indago.Samples.Web";

    protected override string ProjectPath =>
        Path.Combine("samples", "hosts", "Indago.Samples.Web", "Indago.Samples.Web.csproj");
}

// T023: negative-fixture INVERTED assertion (US3 / D2). Publishes the fixture WITHOUT
// warnings-as-errors and without throwing, then asserts the pinned Indago-attributable IL2072 is
// present: warning present ⇒ PASS, clean/absent ⇒ FAIL the build (the regression detector itself is
// guarded). The trim warning is emitted during analysis (before the native link), so this is
// observable locally even when the local Native AOT link is unavailable (negative-fixture-step).
public sealed class NegativeFixtureAotPublishModule : Module<string>
{
    private const string ExpectedWarning = "IL2072";
    private const string FixtureCsproj = "Indago.Samples.NegativeFixture.csproj";

    protected override async Task<string?> ExecuteAsync(IModuleContext context, CancellationToken cancellationToken)
    {
        var rid = RuntimeInformation.RuntimeIdentifier;
        var project = Path.Combine(
            SampleBuild.RepoRoot, "samples", "fixtures", "Indago.Samples.NegativeFixture", FixtureCsproj);
        context.Logger.LogInformation(
            "Negative-fixture inverted assertion: publishing {Project} — MUST emit {Warning}", project, ExpectedWarning);

        var result = await context.DotNet().Publish(
            new DotNetPublishOptions
            {
                ProjectSolution = project,
                Configuration = "Release",
                Framework = "net10.0",
                Runtime = rid,
            },
            new CommandExecutionOptions { ThrowOnNonZeroExitCode = false },
            cancellationToken);

        var output = $"{result.StandardOutput}\n{result.StandardError}";
        var present = output.Contains(ExpectedWarning, StringComparison.Ordinal)
            && output.Contains(FixtureCsproj, StringComparison.Ordinal);

        if (present)
        {
            context.Logger.LogInformation(
                "Negative-fixture inverted assertion PASS — expected {Warning} present.", ExpectedWarning);
            return ExpectedWarning;
        }

        throw new InvalidOperationException(
            $"Negative-fixture inverted assertion FAILED: the pinned Indago-attributable {ExpectedWarning} "
            + "was NOT present in the publish output. The regression detector can no longer prove trim/AOT "
            + "warnings are caught — investigate whether Indago became more trim-safe or the fixture stopped "
            + "triggering (see contracts/negative-fixture-step.contract.md).");
    }
}
