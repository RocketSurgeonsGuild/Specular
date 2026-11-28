using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Specular.Scanning.Tests.Caching;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Scanning.Scans;

namespace Specular.Scanning.Tests.Scanning;

/// <summary>
///     The assembly scope: which assemblies a selection that does not name them looks in. By default everything but the framework
///     (Microsoft's framework signing keys and the framework names); a project adds exclude and include globs, and an include always wins.
/// </summary>
public class ScanAssemblyScopeTests
{
    private const string DependencyInjection = "Microsoft.Extensions.DependencyInjection.Abstractions";

    // Names that only look like the framework's: the old prefix rule ("System", "Microsoft") left them out.
    private static readonly MetadataReference systemsBilling = ScanCompilation.EmitReference("namespace Billing { public class Invoice; }", "Systems.Billing");
    private static readonly MetadataReference graphHelpers = ScanCompilation.EmitReference("namespace Graph { public class Client; }", "MicrosoftGraphHelpers");
    private static readonly MetadataReference contosoBilling = ScanCompilation.EmitReference("namespace Contoso { public class Billing; }", "Contoso.Billing");
    private static readonly MetadataReference contosoLegacy = ScanCompilation.EmitReference("namespace Contoso { public class Legacy; }", "Contoso.Legacy");

    private static readonly Compilation compilation = ScanCompilation.Create(
        "public class Mine;",
        // System.Collections.Immutable: a framework assembly (Microsoft's key) that is not one of the core assemblies a selection skips itself.
        additionalReferences: [systemsBilling, graphHelpers, contosoBilling, contosoLegacy, MetadataReference.CreateFromFile(typeof(ImmutableArray).Assembly.Location)]
    );

    private static ImmutableArray<string> Scanned(ScanAssemblyScope scope, AssemblyFilterData? filter = null) =>
        AssemblyNames(Scanner.Scan(compilation, Assemblies(filter ?? AllAssemblies), scope));

    private static ScanAssemblyScope Scope(string[]? exclude = null, string[]? include = null) => new(Array(exclude ?? []), Array(include ?? []));

    [Test]
    public async Task The_default_leaves_out_the_framework_but_not_names_that_only_look_like_it()
    {
        var scanned = Scanned(ScanAssemblyScope.Default);

        await scanned.Should().Contain(ScanCompilation.ProjectAssemblyName);
        await scanned.Should().Contain("Systems.Billing");
        await scanned.Should().Contain("MicrosoftGraphHelpers");
        await scanned.Should().Contain("Contoso.Billing");
        await scanned.Should().NotContain(DependencyInjection);
        await scanned.Should().NotContain("System.Runtime");
        await scanned.Should().NotContain("System.Collections.Immutable");
    }

    [Test]
    public async Task Everything_leaves_out_nothing_but_the_core_assemblies_a_selection_skips_itself()
    {
        var scanned = Scanned(ScanAssemblyScope.Everything);

        await scanned.Should().Contain(DependencyInjection);
        await scanned.Should().Contain("System.Collections.Immutable");
        await scanned.Should().NotContain("System.Runtime");
    }

    [Test]
    public async Task An_exclude_glob_leaves_out_every_assembly_it_matches()
    {
        var scanned = Scanned(Scope(exclude: ["contoso.*"]));

        await scanned.Should().NotContain("Contoso.Billing");
        await scanned.Should().NotContain("Contoso.Legacy");
        await scanned.Should().Contain("Systems.Billing");
    }

    [Test]
    public async Task An_include_wins_over_an_exclude_and_over_the_framework_default()
    {
        var scanned = Scanned(Scope(exclude: ["Contoso.*"], include: ["Contoso.Billing", "Microsoft.Extensions.*"]));

        await scanned.Should().Contain("Contoso.Billing");
        await scanned.Should().NotContain("Contoso.Legacy");
        await scanned.Should().Contain(DependencyInjection);
        await scanned.Should().NotContain("System.Collections.Immutable");
    }

    [Test]
    public async Task Including_everything_scans_what_the_default_leaves_out() => await Scanned(Scope(exclude: ["Contoso.*"], include: ["*"])).Should().BeEquivalentTo(Scanned(ScanAssemblyScope.Everything));

    [Test]
    public async Task The_compilations_own_assembly_is_always_in_scope() => await Scanned(Scope(exclude: ["*"])).Should().BeEquivalentTo([ScanCompilation.ProjectAssemblyName]);

    [Test]
    public async Task A_selection_that_names_an_assembly_scans_it_whatever_the_scope_says()
    {
        var scope = Scope(exclude: ["Contoso.*"]);

        await Scanned(scope, FromAssemblies("Contoso.Legacy", DependencyInjection)).Should().BeEquivalentTo(["Contoso.Legacy", DependencyInjection]);
    }

    [Test]
    public async Task Including_system_assemblies_brings_back_the_framework_but_not_the_projects_excludes()
    {
        var scanned = Scanned(Scope(exclude: ["Contoso.*"]), AllAssemblies with { IncludeSystemAssemblies = true });

        await scanned.Should().Contain(DependencyInjection);
        await scanned.Should().Contain("System.Runtime");
        await scanned.Should().NotContain("Contoso.Billing");
    }

    [Test]
    public async Task A_project_gives_its_patterns_through_the_consumers_msbuild_properties()
    {
        var options = CacheTestHelpers.Options(
            ("build_property.MyGenExcludeAssemblies", " Contoso.* ; Systems.?illing,"),
            ("build_property.MyGenIncludeAssemblies", "Contoso.Billing")
        );

        var scope = ScanAssemblyScope.FromOptions(options, "MyGen");

        await scope.Should().BeEqualTo(Scope(exclude: ["Contoso.*", "Systems.?illing"], include: ["Contoso.Billing"]));
        await ScanAssemblyScope.FromOptions(CacheTestHelpers.Options(), "MyGen").Should().BeEqualTo(ScanAssemblyScope.Default);
        await ScanAssemblyScope.FromOptions(options, "OtherGen").Should().BeEqualTo(ScanAssemblyScope.Default);
    }

    [Test]
    [Arguments("System", "System", true)]
    [Arguments("System", "System.Runtime", false)]
    [Arguments("System.*", "System.Runtime", true)]
    [Arguments("System.*", "System", false)]
    [Arguments("Contoso*", "Contoso", true)]
    [Arguments("contoso.?illing", "Contoso.Billing", true)]
    [Arguments("*.Billing", "Contoso.Billing", true)]
    [Arguments("*.Billing", "Contoso.Billing.Tests", false)]
    [Arguments("*", "", true)]
    public async Task Patterns_match_the_whole_name_ignoring_case(string pattern, string name, bool matches) => await Glob.IsMatch(pattern, name).Should().BeEqualTo(matches);
}
