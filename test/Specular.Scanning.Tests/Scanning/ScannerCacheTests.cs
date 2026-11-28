using Microsoft.CodeAnalysis;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Scanning.Scans;

namespace Specular.Scanning.Tests.Scanning;

public class ScannerCacheTests
{
    [Test]
    public async Task A_miss_walks_the_assembly_and_stores_its_matches()
    {
        var cache = new FakeAssemblyScanCache();
        var result = Scanner.Scan(ScanCompilation.Create(), Types(AllAssemblies), ScanAssemblyScope.Everything, cache, CancellationToken.None);

        await cache.Lookups.Should().BeEquivalentTo(["Microsoft.Extensions.DependencyInjection.Abstractions", "Specular", "TestAssembly"]);
        await cache.Stored.Select(static z => z.Assembly).Should().BeEquivalentTo(cache.Lookups);

        var stored = cache.Stored.Single(static z => z.Assembly == "TestAssembly").Types;
        var scanned = result.Types.Where(static z => z.AssemblyName == "TestAssembly").ToList();
        await stored.Should().BeEquivalentTo(scanned);
        await stored.Count.Should().BeGreaterThan(0);
    }

    [Test]
    public async Task The_compilations_own_assembly_is_never_cached()
    {
        var cache = new FakeAssemblyScanCache();
        _ = Scanner.Scan(ScanCompilation.Create("public class Mine;"), Types(FromAssemblies(ScanCompilation.ProjectAssemblyName)), ScanAssemblyScope.Everything, cache, CancellationToken.None);

        await cache.Lookups.Should().BeEmpty();
        await cache.Stored.Should().BeEmpty();
    }

    [Test]
    public async Task An_assemblies_scan_never_consults_the_cache()
    {
        var cache = new FakeAssemblyScanCache();
        _ = Scanner.Scan(ScanCompilation.Create(), Assemblies(AllAssemblies), ScanAssemblyScope.Everything, cache, CancellationToken.None);

        await cache.Lookups.Should().BeEmpty();
        await cache.Stored.Should().BeEmpty();
    }

    [Test]
    public async Task Only_assemblies_that_pass_the_filter_are_looked_up()
    {
        var cache = new FakeAssemblyScanCache();
        _ = Scanner.Scan(ScanCompilation.Create(), Types(FromAssemblies("TestAssembly")), ScanAssemblyScope.Everything, cache, CancellationToken.None);

        await cache.Lookups.Should().BeEquivalentTo(["TestAssembly"]);
    }

    [Test]
    public async Task A_hit_is_used_without_walking_the_assembly()
    {
        var cached = new ScannedType("TestAssembly", "TestAssembly.FromCache", TypeKind.Class, Accessibility.Internal, false, 0);
        var cache = new FakeAssemblyScanCache { Entries = { ["TestAssembly"] = Array(cached) } };
        var custom = new RecordingNameFilter("everything", static _ => true);

        var result = Scanner.Scan(
            ScanCompilation.Create(),
            Types(FromAssemblies("TestAssembly"), TypeFilterData.All with { CustomFilters = Array<CustomTypeFilter>(custom) }),
            cache,
            CancellationToken.None
        );

        await TypeNames(result).Should().BeEquivalentTo(["TestAssembly.FromCache"]);
        await result.Assemblies.Single().Should().BeEqualTo(new ScannedAssembly("TestAssembly", true));
        await custom.Visited.Should().BeEmpty();
        await cache.Stored.Should().BeEmpty();
    }

    [Test]
    public async Task Hits_and_walked_assemblies_are_merged_in_order()
    {
        var cached = new ScannedType("Specular", "Specular.FromCache", TypeKind.Class, Accessibility.Public, true, 0);
        var cache = new FakeAssemblyScanCache { Entries = { ["Specular"] = Array(cached) } };

        var result = Scanner.Scan(ScanCompilation.Create("public class Mine;"), Types(AllAssemblies), ScanAssemblyScope.Everything, cache, CancellationToken.None);

        var names = TypeNames(result);
        await names.Should().Contain("Specular.FromCache");
        await names.Should().Contain("TestAssembly.Service");
        await names.Should().Contain("Mine");
        await names.Where(static z => z.StartsWith("Specular.", StringComparison.Ordinal)).Should().BeEquivalentTo(["Specular.FromCache"]);
        await cache.Stored.Select(static z => z.Assembly).Should().BeEquivalentTo(["Microsoft.Extensions.DependencyInjection.Abstractions", "TestAssembly"]);
    }
}
