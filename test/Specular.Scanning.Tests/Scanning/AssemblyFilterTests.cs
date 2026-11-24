using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Scanning.Scans;

namespace Specular.Scanning.Tests.Scanning;

public class AssemblyFilterTests
{
    private const string DependencyInjection = "Microsoft.Extensions.DependencyInjection.Abstractions";

    [Test]
    public async Task All_assemblies_skip_system_assemblies_by_default()
    {
        var result = Scanner.Scan(ScanCompilation.Create(), Assemblies(AllAssemblies));

        await AssemblyNames(result).Should().BeEquivalentTo([DependencyInjection, "Specular", "TestAssembly", "TestProject"]);
        await result.Types.Should().BeEmpty();
    }

    [Test]
    public async Task Including_system_assemblies_adds_the_core_assemblies()
    {
        var result = Scanner.Scan(ScanCompilation.Create(), Assemblies(AllAssemblies with { IncludeSystemAssemblies = true }));

        await AssemblyNames(result)
           .Should()
           .BeEquivalentTo([DependencyInjection, "Specular", "System.Private.CoreLib", "System.Runtime", "TestAssembly", "TestProject"]);
    }

    [Test]
    public async Task A_specific_assembly_is_the_only_match()
    {
        var result = Scanner.Scan(ScanCompilation.Create(), Assemblies(FromAssemblies("TestAssembly")));

        await AssemblyNames(result).Should().BeEquivalentTo(["TestAssembly"]);
    }

    [Test]
    public async Task The_compilations_own_assembly_can_be_selected()
    {
        var result = Scanner.Scan(ScanCompilation.Create(), Assemblies(FromAssemblies(ScanCompilation.ProjectAssemblyName)));

        await AssemblyNames(result).Should().BeEquivalentTo([ScanCompilation.ProjectAssemblyName]);
    }

    [Test]
    public async Task A_specific_system_assembly_still_needs_system_assemblies_included()
    {
        var compilation = ScanCompilation.Create();

        var without = Scanner.Scan(compilation, Assemblies(FromAssemblies("System.Runtime")));
        var with = Scanner.Scan(compilation, Assemblies(FromAssemblies("System.Runtime") with { IncludeSystemAssemblies = true }));

        await AssemblyNames(without).Should().BeEmpty();
        await AssemblyNames(with).Should().BeEquivalentTo(["System.Runtime"]);
    }

    [Test]
    public async Task Every_specific_assembly_clause_must_hold_like_Specular()
    {
        // Specular ANDs its assembly descriptors, so two FromAssemblyOf calls naming different assemblies select nothing.
        var result = Scanner.Scan(ScanCompilation.Create(), Assemblies(FromAssemblies("TestAssembly", "Specular")));

        await AssemblyNames(result).Should().BeEmpty();
    }

    [Test]
    public async Task Not_assemblies_are_excluded_from_all_assemblies()
    {
        var result = Scanner.Scan(ScanCompilation.Create(), Assemblies(AllAssemblies with { NotAssemblies = Array("TestAssembly", "Specular") }));

        await AssemblyNames(result).Should().BeEquivalentTo([DependencyInjection, "TestProject"]);
    }

    [Test]
    public async Task Not_assemblies_alone_select_every_other_assembly_like_Specular()
    {
        var result = Scanner.Scan(ScanCompilation.Create(), Assemblies(AssemblyFilterData.Empty with { NotAssemblies = Array("TestAssembly") }));

        await AssemblyNames(result).Should().BeEquivalentTo([DependencyInjection, "Specular", "TestProject"]);
    }

    [Test]
    public async Task Not_assemblies_override_a_specific_assembly()
    {
        var result = Scanner.Scan(ScanCompilation.Create(), Assemblies(FromAssemblies("TestAssembly") with { NotAssemblies = Array("TestAssembly") }));

        await AssemblyNames(result).Should().BeEmpty();
    }

    [Test]
    public async Task An_unknown_not_assembly_excludes_nothing()
    {
        var result = Scanner.Scan(ScanCompilation.Create(), Assemblies(AllAssemblies with { NotAssemblies = Array("Missing") }));

        await AssemblyNames(result).Should().BeEquivalentTo([DependencyInjection, "Specular", "TestAssembly", "TestProject"]);
    }

    [Test]
    public async Task Dependencies_of_select_the_assemblies_that_reference_it()
    {
        var compilation = ScanCompilation.Create();

        var ofSpecular = Scanner.Scan(compilation, Assemblies(AssemblyFilterData.Empty with { DependenciesOf = Array("Specular") }));
        var ofTestAssembly = Scanner.Scan(compilation, Assemblies(AssemblyFilterData.Empty with { DependenciesOf = Array("TestAssembly") }));

        await AssemblyNames(ofSpecular).Should().BeEquivalentTo(["TestAssembly", "TestProject"]);
        await AssemblyNames(ofTestAssembly).Should().BeEquivalentTo(["TestProject"]);
    }

    [Test]
    public async Task Unknown_specific_or_dependency_assemblies_match_nothing()
    {
        var compilation = ScanCompilation.Create();

        var specific = Scanner.Scan(compilation, Assemblies(FromAssemblies("Missing")));
        var dependencies = Scanner.Scan(compilation, Assemblies(AssemblyFilterData.Empty with { DependenciesOf = Array("Missing") }));

        await AssemblyNames(specific).Should().BeEmpty();
        await AssemblyNames(dependencies).Should().BeEmpty();
    }

    [Test]
    public async Task An_empty_filter_matches_nothing()
    {
        var result = Scanner.Scan(ScanCompilation.Create(), Assemblies(AssemblyFilterData.Empty));

        await AssemblyNames(result).Should().BeEmpty();
    }

    [Test]
    public async Task Assemblies_are_ordered_by_name_ordinally()
    {
        var lowercase = ScanCompilation.EmitReference("namespace Lower { public class Thing; }", "alpha");
        var result = Scanner.Scan(ScanCompilation.Create(additionalReferences: lowercase), Assemblies(AllAssemblies));

        // Ordinal puts upper case before lower case; a culture-aware sort would put "alpha" first.
        await string.Join(",", AssemblyNames(result))
                    .Should()
                    .BeEqualTo($"{DependencyInjection},Specular,TestAssembly,TestProject,alpha");
    }

    [Test]
    public async Task A_types_scan_reports_every_passing_assembly_even_without_matches()
    {
        var result = Scanner.Scan(
            ScanCompilation.Create(),
            Types(AllAssemblies, TypeFilterData.All with { Names = Array(new NameFilterData(true, TextDirectionFilter.StartsWith, Array("IRequestHandler"))) })
        );

        await AssemblyNames(result).Should().BeEquivalentTo([DependencyInjection, "Specular", "TestAssembly", "TestProject"]);
        await TypeNames(result).Should().BeEquivalentTo(["TestAssembly.IRequestHandler`2"]);
    }
}
