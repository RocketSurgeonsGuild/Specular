using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Selectors.SelectorCompilation;
using static Specular.Scanning.Tests.Selectors.SelectorTestData;

namespace Specular.Scanning.Tests.Selectors;

public class AssemblySelectorTests
{
    [Test]
    public async Task Without_GetTypes_it_is_an_assemblies_scan()
    {
        var request = Build(s => s.FromAssemblies());

        await request.Kind.Should().BeEqualTo(ScanKind.Assemblies);
        await request.Types.Should().BeNull();
        await request.Assemblies.Should().BeEqualTo(AssemblyFilterData.Empty with { AllAssemblies = true });
    }

    [Test]
    public async Task An_empty_selection_matches_nothing()
    {
        var request = Build(_ => { });

        await request.Should().BeEqualTo(new ScanRequest(ScanKind.Assemblies, AssemblyFilterData.Empty, null));
    }

    [Test]
    public async Task EntryAssembly_adds_the_compilation_assembly()
    {
        var request = Build(s => s.EntryAssembly());

        await request.Assemblies.Assemblies.Should().BeEqualTo(Array(AppAssemblyName));
        await request.Should().BeEqualTo(Build(s => s.FromAssembly(Compilation.Assembly)));
    }

    [Test]
    public async Task FromAssembly_overloads_add_the_assembly_metadata_name()
    {
        var bySymbol = Build(s => s.FromAssembly(LibraryAssembly));

        await bySymbol.Assemblies.Should().BeEqualTo(AssemblyFilterData.Empty with { Assemblies = Array(LibraryAssemblyName) });
        await bySymbol.Should().BeEqualTo(Build(s => s.FromAssembly(LibraryAssemblyName)));
        await bySymbol.Should().BeEqualTo(Build(s => s.FromAssemblyOf(Type("Library.Contracts.IHandler"))));
    }

    [Test]
    public async Task NotFromAssembly_overloads_exclude_the_assembly()
    {
        var bySymbol = Build(s => s.NotFromAssembly(LibraryAssembly));

        await bySymbol.Assemblies.Should().BeEqualTo(AssemblyFilterData.Empty with { NotAssemblies = Array(LibraryAssemblyName) });
        await bySymbol.Should().BeEqualTo(Build(s => s.NotFromAssembly(LibraryAssemblyName)));
        await bySymbol.Should().BeEqualTo(Build(s => s.NotFromAssemblyOf(Type("Library.Contracts.IHandler"))));
    }

    [Test]
    public async Task DependenciesFromAssembly_overloads_add_the_dependency_root()
    {
        var bySymbol = Build(s => s.DependenciesFromAssembly(LibraryAssembly));

        await bySymbol.Assemblies.Should().BeEqualTo(AssemblyFilterData.Empty with { DependenciesOf = Array(LibraryAssemblyName) });
        await bySymbol.Should().BeEqualTo(Build(s => s.DependenciesFromAssembly(LibraryAssemblyName)));
        await bySymbol.Should().BeEqualTo(Build(s => s.DependenciesFromAssemblyOf(Type("Library.Contracts.IHandler"))));
    }

    [Test]
    public async Task IncludeSystemAssemblies_sets_the_flag()
    {
        var request = Build(s => s.IncludeSystemAssemblies());

        await request.Assemblies.Should().BeEqualTo(AssemblyFilterData.Empty with { IncludeSystemAssemblies = true });
    }

    [Test]
    public async Task Chained_selectors_combine_in_call_order()
    {
        var request = Build(s => s
                                .FromAssemblies()
                                .FromAssembly("Second")
                                .FromAssembly("First")
                                .NotFromAssembly("Excluded")
                                .DependenciesFromAssembly("Root")
                                .IncludeSystemAssemblies()
        );

        await request.Assemblies.Should().BeEqualTo(
            new AssemblyFilterData(true, true, Array("Second", "First"), Array("Excluded"), Array("Root"))
        );
    }

    [Test]
    public async Task Repeated_assemblies_are_listed_once()
    {
        var request = Build(s => s.EntryAssembly().FromAssembly(AppAssemblyName).FromAssembly(Compilation.Assembly));

        await request.Assemblies.Assemblies.Should().BeEqualTo(Array(AppAssemblyName));
    }

    [Test]
    public async Task Build_rejects_null_arguments()
    {
        await Act(() => ScanRequest.Build(null!, s => s.FromAssemblies())).Should().Throw<ArgumentNullException>();
        await Act(() => ScanRequest.Build(Compilation, null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public async Task Assembly_selectors_reject_null_and_empty_arguments()
    {
        await Act(() => Build(s => s.FromAssembly((Microsoft.CodeAnalysis.IAssemblySymbol)null!))).Should().Throw<ArgumentNullException>();
        await Act(() => Build(s => s.FromAssembly((string)null!))).Should().Throw<ArgumentNullException>();
        await Act(() => Build(s => s.FromAssembly(""))).Should().Throw<ArgumentException>();
        await Act(() => Build(s => s.FromAssemblyOf(null!))).Should().Throw<ArgumentNullException>();
        await Act(() => Build(s => s.NotFromAssembly(""))).Should().Throw<ArgumentException>();
        await Act(() => Build(s => s.NotFromAssemblyOf(null!))).Should().Throw<ArgumentNullException>();
        await Act(() => Build(s => s.DependenciesFromAssembly(""))).Should().Throw<ArgumentException>();
        await Act(() => Build(s => s.DependenciesFromAssemblyOf(null!))).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public async Task The_selector_cannot_be_used_after_Build_returns()
    {
        IAssemblySymbolSelector? captured = null;
        _ = Build(s => captured = s);

        await Act(() => captured!.FromAssemblies()).Should().Throw<InvalidOperationException>();
    }
}
