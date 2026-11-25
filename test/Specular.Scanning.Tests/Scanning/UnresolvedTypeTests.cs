using Microsoft.CodeAnalysis;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Scanning.Scans;

namespace Specular.Scanning.Tests.Scanning;

public class UnresolvedTypeTests
{
    private static readonly TypeReference missingHandler = Type("TestAssembly.IMissingHandler");
    private static readonly TypeReference missingService = Type("Missing.IService", "");
    private static readonly TypeReference missingAttribute = Type("Specular.MissingAttribute", "Specular");

    private static ScanRequest TestAssemblyScan(TypeFilterData types) => Types(FromAssemblies(ScanCompilation.TestAssemblyName), types);

    [Test]
    public async Task Each_distinct_unresolved_reference_is_listed_once_in_request_order()
    {
        var types = TypeFilterData.All with
        {
            AssignableTo = Array(
                new AssignableToFilterData(true, Array(missingHandler, Type("TestAssembly.IService"))),
                new AssignableToFilterData(false, Array(missingService, missingHandler))
            ),
            Attributes = Array(new AttributeFilterData(false, true, Array(missingAttribute, missingService))),
        };

        var result = Scanner.Scan(ScanCompilation.Create(), TestAssemblyScan(types));

        await result.UnresolvedTypes.Should().BeEqualTo(Array(missingHandler, missingService, missingAttribute));
    }

    [Test]
    public async Task Nothing_is_listed_when_every_reference_resolves()
    {
        var types = TypeFilterData.All with
        {
            AssignableTo = Array(new AssignableToFilterData(true, Array(Type("TestAssembly.IService")))),
            Attributes = Array(new AttributeFilterData(false, false, Array(Type("Specular.ServiceRegistrationAttribute", "Specular")))),
        };

        var result = Scanner.Scan(ScanCompilation.Create(), TestAssemblyScan(types));

        await result.UnresolvedTypes.Count.Should().BeEqualTo(0);
    }

    [Test]
    public async Task An_assemblies_scan_lists_nothing()
    {
        var request = Assemblies(AllAssemblies) with
        {
            Types = TypeFilterData.All with { AssignableTo = Array(new AssignableToFilterData(true, Array(missingHandler))) },
        };

        await Scanner.Scan(ScanCompilation.Create(), request).UnresolvedTypes.Count.Should().BeEqualTo(0);
    }

    [Test]
    public async Task UnresolvedTypes_defaults_to_empty_and_takes_part_in_equality()
    {
        var result = new ScanResult(TestAssemblyScan(TypeFilterData.All), EquatableArray<ScannedAssembly>.Empty, EquatableArray<ScannedType>.Empty);
        var reported = result with { UnresolvedTypes = Array(missingHandler) };

        await result.UnresolvedTypes.Count.Should().BeEqualTo(0);
        await reported.Should().NotBeEqualTo(result);
        await ( reported with { } ).Should().BeEqualTo(reported);
        await ( reported with { } ).GetHashCode().Should().BeEqualTo(reported.GetHashCode());
    }

    [Test]
    public async Task An_include_clause_with_nothing_resolved_matches_nothing_but_still_lists_the_assembly()
    {
        var types = TypeFilterData.All with { AssignableTo = Array(new AssignableToFilterData(true, Array(missingHandler))) };

        var result = Scanner.Scan(ScanCompilation.Create(), TestAssemblyScan(types));

        await result.Types.Count.Should().BeEqualTo(0);
        await result.Assemblies.Should().BeEqualTo(Array(new ScannedAssembly(ScanCompilation.TestAssemblyName, false)));
    }

    [Test]
    public async Task A_request_with_an_unresolved_reference_neither_reads_nor_writes_the_cache()
    {
        // The result depends on the unresolved name, which can start resolving (or stop being ambiguous) without any scanned
        // assembly's MVID changing, so neither a cached result nor this one can be trusted across builds.
        var cached = new ScannedType("TestAssembly", "TestAssembly.FromCache", TypeKind.Class, Accessibility.Public, true, 0);
        var cache = new FakeAssemblyScanCache { Entries = { ["TestAssembly"] = Array(cached) } };
        var types = TypeFilterData.All with { AssignableTo = Array(new AssignableToFilterData(false, Array(missingHandler))) };

        var result = Scanner.Scan(ScanCompilation.Create(), TestAssemblyScan(types), cache, CancellationToken.None);

        await cache.Lookups.Should().BeEmpty();
        await cache.Stored.Should().BeEmpty();
        await TypeNames(result).Should().Contain("TestAssembly.Service");
        await TypeNames(result).Should().NotContain("TestAssembly.FromCache");
        await result.UnresolvedTypes.Should().BeEqualTo(Array(missingHandler));
    }

    [Test]
    public async Task A_cache_hit_still_resolves_the_request_against_the_compilation()
    {
        var cached = new ScannedType("TestAssembly", "TestAssembly.FromCache", TypeKind.Class, Accessibility.Public, true, 0);
        var cache = new FakeAssemblyScanCache { Entries = { ["TestAssembly"] = Array(cached) } };
        var types = TypeFilterData.All with { AssignableTo = Array(new AssignableToFilterData(true, Array(Type("TestAssembly.IService")))) };

        var result = Scanner.Scan(ScanCompilation.Create(), TestAssemblyScan(types), cache, CancellationToken.None);

        await TypeNames(result).Should().BeEquivalentTo(["TestAssembly.FromCache"]);
        await result.UnresolvedTypes.Count.Should().BeEqualTo(0);
    }

    [Test]
    public async Task A_name_defined_in_two_referenced_assemblies_is_unresolved_unless_the_assembly_is_named()
    {
        var first = ScanCompilation.EmitReference("namespace Dup { public interface IThing; public class FirstThing : IThing; }", "FirstLib");
        var second = ScanCompilation.EmitReference("namespace Dup { public interface IThing; public class SecondThing : IThing; }", "SecondLib");
        var compilation = ScanCompilation.Create(additionalReferences: [first, second]);
        var assemblies = FromAssemblies("FirstLib", "SecondLib");
        var ambiguous = Type("Dup.IThing", "");

        var byName = Scanner.Scan(compilation, Types(assemblies, TypeFilterData.All with { AssignableTo = Array(new AssignableToFilterData(true, Array(ambiguous))) }));
        var byAssembly = Scanner.Scan(
            compilation,
            Types(assemblies, TypeFilterData.All with { AssignableTo = Array(new AssignableToFilterData(true, Array(Type("Dup.IThing", "FirstLib")))) })
        );
        var firstThing = ( (IAssemblySymbol)compilation.GetAssemblyOrModuleSymbol(first)! ).GetTypeByMetadataName("Dup.IThing")!;
        var bySymbol = Scanner.Scan(compilation, ScanRequest.Build(compilation, s => s.FromAssemblies().GetTypes(f => f.AssignableTo(firstThing))));

        await byName.Types.Count.Should().BeEqualTo(0);
        await byName.UnresolvedTypes.Should().BeEqualTo(Array(ambiguous));
        await QualifiedNames(byAssembly).Should().BeEquivalentTo(["FirstLib:Dup.FirstThing", "FirstLib:Dup.IThing"]);
        await byAssembly.UnresolvedTypes.Count.Should().BeEqualTo(0);
        await QualifiedNames(bySymbol).Should().BeEquivalentTo(["FirstLib:Dup.FirstThing", "FirstLib:Dup.IThing"]);
    }

    private static string[] QualifiedNames(ScanResult result) => [.. result.Types.Select(static z => $"{z.AssemblyName}:{z.MetadataName}")];
}
