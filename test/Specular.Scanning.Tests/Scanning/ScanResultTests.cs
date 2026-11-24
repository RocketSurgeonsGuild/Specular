using Microsoft.CodeAnalysis;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Scanning.Scans;

namespace Specular.Scanning.Tests.Scanning;

public class ScanResultTests
{
    private const string InternalsSource = """
                                           namespace Ivt
                                           {
                                               public class Open;
                                               internal class Hidden;
                                               internal static class Outer
                                               {
                                                   public class Inner;
                                                   private class Secret;
                                               }
                                           }
                                           """;

    private static ScannedType Single(ScanResult result, string metadataName) => result.Types.Single(z => z.MetadataName == metadataName);

    private static ScanResult ScanLibrary(string friend, TypeFilterData? types = null)
    {
        var library = ScanCompilation.EmitReference(
            $"[assembly: System.Runtime.CompilerServices.InternalsVisibleTo(\"{friend}\")]\n{InternalsSource}",
            "IvtLib"
        );
        return Scanner.Scan(ScanCompilation.Create(additionalReferences: library), Types(FromAssemblies("IvtLib"), types));
    }

    [Test]
    public async Task Scanned_types_describe_the_symbol()
    {
        var result = Scanner.Scan(ScanCompilation.Create(), Types(FromAssemblies("TestAssembly")));

        await Single(result, "TestAssembly.IRequestHandler`2")
             .Should()
             .BeEqualTo(new ScannedType("TestAssembly", "TestAssembly.IRequestHandler`2", TypeKind.Interface, Accessibility.Public, true, 2));
        await Single(result, "TestAssembly.ServiceB")
             .Should()
             .BeEqualTo(new ScannedType("TestAssembly", "TestAssembly.ServiceB", TypeKind.Class, Accessibility.Internal, false, 0));
        await Single(result, "TestAssembly.Nested+MyRecord")
             .Should()
             .BeEqualTo(new ScannedType("TestAssembly", "TestAssembly.Nested+MyRecord", TypeKind.Class, Accessibility.Private, false, 0));
        await Single(result, "TestAssembly.Nested+ServiceA")
             .Should()
             .BeEqualTo(new ScannedType("TestAssembly", "TestAssembly.Nested+ServiceA", TypeKind.Class, Accessibility.Public, true, 0));
    }

    [Test]
    public async Task Inaccessible_matches_are_flagged_on_their_assembly()
    {
        var compilation = ScanCompilation.Create();

        var all = Scanner.Scan(compilation, Types(FromAssemblies("TestAssembly")));
        var publicOnly = Scanner.Scan(compilation, Types(FromAssemblies("TestAssembly"), TypeFilterData.All with { PublicOnly = true }));

        await all.Assemblies.Single().Should().BeEqualTo(new ScannedAssembly("TestAssembly", true));
        await publicOnly.Assemblies.Single().Should().BeEqualTo(new ScannedAssembly("TestAssembly", false));
    }

    [Test]
    public async Task Internal_types_of_a_referenced_assembly_are_accessible_with_InternalsVisibleTo()
    {
        var result = ScanLibrary(ScanCompilation.ProjectAssemblyName);

        await Single(result, "Ivt.Open").IsAccessible.Should().BeTrue();
        await Single(result, "Ivt.Hidden").IsAccessible.Should().BeTrue();
        await Single(result, "Ivt.Outer").IsAccessible.Should().BeTrue();
        await Single(result, "Ivt.Outer+Inner").IsAccessible.Should().BeTrue();
        await Single(result, "Ivt.Outer+Secret").IsAccessible.Should().BeFalse();
    }

    [Test]
    public async Task Internal_types_and_their_nested_types_are_inaccessible_without_InternalsVisibleTo()
    {
        var result = ScanLibrary("SomeoneElse");

        await Single(result, "Ivt.Open").IsAccessible.Should().BeTrue();
        await Single(result, "Ivt.Hidden").IsAccessible.Should().BeFalse();
        await Single(result, "Ivt.Outer").IsAccessible.Should().BeFalse();
        // Declared public, but its container is internal.
        await Single(result, "Ivt.Outer+Inner").IsAccessible.Should().BeFalse();
    }

    [Test]
    public async Task Has_inaccessible_matches_follows_InternalsVisibleTo()
    {
        var types = TypeFilterData.All with { Names = Array(new NameFilterData(true, TextDirectionFilter.EndsWith, Array("Inner", "Hidden"))) };

        await ScanLibrary(ScanCompilation.ProjectAssemblyName, types).Assemblies.Single().HasInaccessibleMatches.Should().BeFalse();
        await ScanLibrary("SomeoneElse", types).Assemblies.Single().HasInaccessibleMatches.Should().BeTrue();
    }

    [Test]
    public async Task Internal_types_of_the_compilation_are_accessible()
    {
        var compilation = ScanCompilation.Create("namespace Mine { internal class Hidden; }");

        var result = Scanner.Scan(compilation, Types(FromAssemblies(ScanCompilation.ProjectAssemblyName)));

        await Single(result, "Mine.Hidden").IsAccessible.Should().BeTrue();
        await result.Assemblies.Single().HasInaccessibleMatches.Should().BeFalse();
    }

    [Test]
    public async Task Types_are_ordered_by_assembly_then_metadata_name_ordinally()
    {
        var compilation = ScanCompilation.Create("namespace AProject { public class First; }");
        var result = Scanner.Scan(compilation, Types(AllAssemblies));

        var keys = result.Types.Select(static z => z.AssemblyName + "|" + z.MetadataName).ToList();
        var sorted = keys.Order(StringComparer.Ordinal).ToList();
        var names = TypeNames(result);

        await string.Join("\n", keys).Should().BeEqualTo(string.Join("\n", sorted));
        // Ordinal puts '`' after 'H'; a culture-aware sort would not.
        await names.IndexOf("TestAssembly.IRequestHandler`2").Should().BeLessThan(names.IndexOf("TestAssembly.IRequest`1"));
        await result.Types[result.Types.Count - 1].AssemblyName.Should().BeEqualTo(ScanCompilation.ProjectAssemblyName);
    }

    [Test]
    public async Task Identical_compilations_produce_equal_results()
    {
        const string source = "namespace Same { public class One; internal class Two; }";

        var first = Scanner.Scan(ScanCompilation.Create(source), Types(AllAssemblies));
        var second = Scanner.Scan(ScanCompilation.Create(source), Types(AllAssemblies));

        await ( first == second ).Should().BeTrue();
        await ( first.GetHashCode() == second.GetHashCode() ).Should().BeTrue();
        await first.Types.Count.Should().BeGreaterThan(0);
    }

    [Test]
    public async Task Unrelated_source_changes_do_not_change_a_referenced_assembly_scan()
    {
        var request = Types(FromAssemblies("TestAssembly"));

        var first = Scanner.Scan(ScanCompilation.Create("public class Before;"), request);
        var second = Scanner.Scan(ScanCompilation.Create("public class After { public int Value; }"), request);

        await ( first == second ).Should().BeTrue();
    }

    [Test]
    public async Task Resolve_round_trips_to_the_same_symbols()
    {
        var compilation = ScanCompilation.Create(
            """
            namespace G
            {
                public class Outer<T>
                {
                    public class Inner;
                }
            }
            """
        );

        var result = Scanner.Scan(compilation, Types(AllAssemblies));
        var symbols = result.Resolve(compilation);

        // Types nested in generics use the resolvable metadata form, not Specular.Analyzers' "G.Outer<T>+Inner".
        await TypeNames(result).Should().Contain("G.Outer`1+Inner");

        await symbols.Assemblies.Select(static z => z.MetadataName).Should().BeEquivalentTo(AssemblyNames(result));
        await symbols.Assemblies.Contains(compilation.Assembly, SymbolEqualityComparer.Default).Should().BeTrue();
        await symbols.Types.Length.Should().BeEqualTo(result.Types.Count);
        foreach (var (symbol, scanned) in symbols.Types.Zip(result.Types))
        {
            await symbol.ContainingAssembly.MetadataName.Should().BeEqualTo(scanned.AssemblyName);
            var nameParts = scanned.MetadataName.Split('.', '+');
            await symbol.MetadataName.Should().BeEqualTo(nameParts[^1]);
        }

        await symbols.Types.Contains(compilation.GetTypeByMetadataName("G.Outer`1+Inner")!, SymbolEqualityComparer.Default).Should().BeTrue();
        await symbols.Types.Contains(compilation.GetTypeByMetadataName("TestAssembly.Nested+MyRecord")!, SymbolEqualityComparer.Default).Should().BeTrue();
    }

    [Test]
    public async Task Run_returns_the_resolved_symbols()
    {
        var compilation = ScanCompilation.Create();

        var symbols = Scanner.Run(
            compilation,
            Types(FromAssemblies("TestAssembly"), TypeFilterData.All with { TypeKinds = Array(new TypeKindFilterData(true, Array(TypeKind.Interface))) })
        );

        await symbols.Assemblies.Select(static z => z.MetadataName).Should().BeEquivalentTo(["TestAssembly"]);
        await symbols.Types.Select(static z => z.MetadataName)
                     .Should()
                     .BeEquivalentTo(
                          ["IGenericService`1", "IKeyedService", "IOther", "IRequestHandler`2", "IRequest`1", "IService", "IServiceB", "IValidator", "IValidator`1"]
                      );
    }

    [Test]
    public async Task Resolve_skips_entries_that_no_longer_resolve()
    {
        var before = ScanCompilation.Create("namespace Changing { public class Stays; public class Goes; }");
        var result = Scanner.Scan(before, Types(AllAssemblies));

        var after = CSharpCompilationWithout(
            ScanCompilation.Create("namespace Changing { public class Stays; }"),
            typeof(TestAssembly.IService).Assembly.Location
        );
        var symbols = result.Resolve(after);

        var names = symbols.Types.Select(static z => z.ToDisplayString()).ToList();
        await names.Should().Contain("Changing.Stays");
        await names.Should().NotContain("Changing.Goes");
        await names.Where(static z => z.StartsWith("TestAssembly.", StringComparison.Ordinal)).Should().BeEmpty();
        await symbols.Assemblies.Select(static z => z.MetadataName).Should().NotContain("TestAssembly");
    }

    [Test]
    public async Task An_assemblies_scan_has_no_types()
    {
        var compilation = ScanCompilation.Create();
        var result = Scanner.Scan(compilation, Assemblies(AllAssemblies));

        await result.Types.Should().BeEmpty();
        await result.Resolve(compilation).Types.Should().BeEmpty();
        await result.Assemblies.All(static z => !z.HasInaccessibleMatches).Should().BeTrue();
    }

    private static Compilation CSharpCompilationWithout(Compilation compilation, string referencePath) =>
        compilation.RemoveReferences(compilation.References.OfType<PortableExecutableReference>().Where(z => z.FilePath == referencePath));
}
