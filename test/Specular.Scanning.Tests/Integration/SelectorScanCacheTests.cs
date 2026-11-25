using Microsoft.CodeAnalysis;
using Specular.Scanning.Tests.Caching;
using Specular.Scanning.Tests.Scanning;
using TUnit.Assertions.Should.Extensions;

namespace Specular.Scanning.Tests.Integration;

// The selector, scanner and cache were built separately; these run them together the way a generator does across builds.
public class SelectorScanCacheTests
{
    private const string ConsumerId = "IntegrationGen";

    private const string LibrarySource = """
        namespace Lib;
        public interface IHandler { }
        public class FirstHandler : IHandler { }
        public class SecondHandler : IHandler { }
        public class NotAHandler { }
        """;

    [Test]
    public async Task A_selector_built_request_scans_like_the_equivalent_hand_built_request()
    {
        var compilation = ScanCompilation.Create();
        var handler = compilation.GetTypeByMetadataName("TestAssembly.IService")
                   ?? throw new InvalidOperationException("TestAssembly.IService is missing.");

        var built = ScanRequest.Build(compilation, s => s.FromAssemblyOf(handler).GetTypes(f => f.AssignableTo(handler)));
        var handWritten = Scans.Types(
            Scans.FromAssemblies("TestAssembly"),
            TypeFilterData.All with { AssignableTo = Scans.Array(new AssignableToFilterData(true, Scans.Array(Scans.Type("TestAssembly.IService")))) }
        );

        await built.Should().BeEqualTo(handWritten);
        await Scanner.Scan(compilation, built).Should().BeEqualTo(Scanner.Scan(compilation, handWritten));
    }

    [Test]
    public async Task A_second_build_against_an_unchanged_reference_is_served_from_the_cache()
    {
        var library = MetadataReference.CreateFromImage(CacheTestHelpers.EmitLibrary(LibrarySource));

        var (firstResult, firstFilter, cacheFile) = Build(library, previousCache: null);
        var (secondResult, secondFilter, _) = Build(library, cacheFile);

        await secondResult.Should().BeEqualTo(firstResult);
        await firstFilter.Visited.Should().Contain("FirstHandler");
        // The library came from the cache, so only the consumer's own types were walked.
        await secondFilter.Visited.Should().NotContain("FirstHandler");
        await secondFilter.Visited.Should().Contain("ConsumerType");
    }

    [Test]
    public async Task Changing_the_reference_invalidates_its_cached_types()
    {
        var before = MetadataReference.CreateFromImage(CacheTestHelpers.EmitLibrary(LibrarySource));
        var after = MetadataReference.CreateFromImage(
            CacheTestHelpers.EmitLibrary(LibrarySource + "public class ThirdHandler : IHandler { }")
        );

        var (_, _, cacheFile) = Build(before, previousCache: null);
        var (result, filter, _) = Build(after, cacheFile);

        await filter.Visited.Should().Contain("ThirdHandler");
        await Scans.TypeNames(result).Should().Contain("Lib.ThirdHandler");
    }

    [Test]
    public async Task A_project_reference_without_an_mvid_is_always_rescanned()
    {
        var library = CacheTestHelpers.CreateLibraryCompilation(LibrarySource).ToMetadataReference();

        var (_, _, cacheFile) = Build(library, previousCache: null);
        var (_, filter, _) = Build(library, cacheFile);

        await filter.Visited.Should().Contain("FirstHandler");
    }

    // One generator run: load the cache the build fed back, scan, and return what Save would write.
    private static (ScanResult Result, RecordingNameFilter Filter, string CacheFile) Build(MetadataReference library, string? previousCache)
    {
        var compilation = CacheTestHelpers.CreateConsumer(library);
        var cache = ScanCache.Load(ConsumerId, compilation, previousCache is null ? [] : [CacheTestHelpers.CacheText(ConsumerId, previousCache)]);
        var filter = new RecordingNameFilter("handlers-v1", static type => type.Name.EndsWith("Handler", StringComparison.Ordinal));

        var request = ScanRequest.Build(compilation, s => s.FromAssemblies().GetTypes(true, f => f.Where(filter)));
        var result = Scanner.Scan(compilation, request, cache, CancellationToken.None);

        return (result, filter, cache.Serialize());
    }
}
