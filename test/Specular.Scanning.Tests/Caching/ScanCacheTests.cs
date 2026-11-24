using Microsoft.CodeAnalysis;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Caching.CacheTestHelpers;

namespace Specular.Scanning.Tests.Caching;

public class ScanCacheTests
{
    private const string Consumer = "TestConsumer";

    [Test]
    public async Task Misses_When_There_Is_No_Cache_File()
    {
        var (compilation, assembly) = Referencing(EmitLibrary("public class A { }"));
        var cache = ScanCache.Load(Consumer, compilation, []);

        await cache.TryGetTypes("request", assembly, out _).Should().BeFalse();
    }

    [Test]
    public async Task Hits_On_The_Next_Build_When_The_Assembly_Is_Unchanged()
    {
        var image = EmitLibrary("public class A { }");
        var (compilation, assembly) = Referencing(image);
        var types = new EquatableArray<ScannedType>(
            [
                new("Lib", "A", TypeKind.Class, Accessibility.Public, true, 0),
                new("Lib", "N.B`1+C", TypeKind.Interface, Accessibility.Internal, false, 2),
            ]
        );
        var first = ScanCache.Load(Consumer, compilation, []);
        first.SetTypes("request", assembly, types);

        var (nextCompilation, nextAssembly) = Referencing(image);
        var second = ScanCache.Load(Consumer, nextCompilation, [CacheText(Consumer, first.Serialize())]);

        await second.TryGetTypes("request", nextAssembly, out var cached).Should().BeTrue();
        await cached.Should().BeEqualTo(types);
    }

    [Test]
    public async Task Misses_After_The_Assembly_Is_Rebuilt()
    {
        var (compilation, assembly) = Referencing(EmitLibrary("public class A { }"));
        var first = ScanCache.Load(Consumer, compilation, []);
        first.SetTypes("request", assembly, Types("Lib", "A"));

        var (nextCompilation, nextAssembly) = Referencing(EmitLibrary("public class A { } public class B { }"));
        var second = ScanCache.Load(Consumer, nextCompilation, [CacheText(Consumer, first.Serialize())]);

        await second.TryGetTypes("request", nextAssembly, out _).Should().BeFalse();
    }

    [Test]
    public async Task Misses_For_A_Different_Request_Key()
    {
        var image = EmitLibrary("public class A { }");
        var (compilation, assembly) = Referencing(image);
        var first = ScanCache.Load(Consumer, compilation, []);
        first.SetTypes("request", assembly, Types("Lib", "A"));

        var (nextCompilation, nextAssembly) = Referencing(image);
        var second = ScanCache.Load(Consumer, nextCompilation, [CacheText(Consumer, first.Serialize())]);

        await second.TryGetTypes("other-request", nextAssembly, out _).Should().BeFalse();
    }

    [Test]
    public async Task Hits_Within_The_Same_Run_After_Set()
    {
        var (compilation, assembly) = Referencing(EmitLibrary("public class A { }"));
        var cache = ScanCache.Load(Consumer, compilation, []);
        cache.SetTypes("request", assembly, Types("Lib", "A"));

        await cache.TryGetTypes("request", assembly, out var cached).Should().BeTrue();
        await cached.Should().BeEqualTo(Types("Lib", "A"));
    }

    [Test]
    public async Task Never_Caches_A_Compilation_Reference()
    {
        var reference = CreateLibraryCompilation("public class A { }").ToMetadataReference();
        var compilation = CreateConsumer(reference);
        var assembly = GetAssembly(compilation, reference);
        var cache = ScanCache.Load(Consumer, compilation, []);

        cache.SetTypes("request", assembly, Types("Lib", "A"));
        cache.Set("store", assembly, new TestPayload("x", 1), TestPayloadJsonContext.Default.TestPayload, 1);

        await cache.TryGetTypes("request", assembly, out _).Should().BeFalse();
        await cache.TryGet("store", assembly, TestPayloadJsonContext.Default.TestPayload, 1, out _).Should().BeFalse();
        await cache.Serialize().Should().NotContain("Lib");
    }

    [Test]
    public async Task Never_Caches_The_Compilations_Own_Assembly()
    {
        var compilation = CreateConsumer();
        var cache = ScanCache.Load(Consumer, compilation, []);

        cache.SetTypes("request", compilation.Assembly, Types("Consumer", "ConsumerType"));

        await cache.TryGetTypes("request", compilation.Assembly, out _).Should().BeFalse();
        await cache.Serialize().Should().NotContain("Consumer");
    }

    [Test]
    public async Task Drops_Entries_Not_Used_In_This_Run()
    {
        var keptImage = EmitLibrary("public class A { }", "Kept");
        var removedImage = EmitLibrary("public class B { }", "Removed");
        var keptReference = MetadataReference.CreateFromImage(keptImage);
        var removedReference = MetadataReference.CreateFromImage(removedImage);
        var compilation = CreateConsumer(keptReference, removedReference);
        var kept = GetAssembly(compilation, keptReference);
        var removed = GetAssembly(compilation, removedReference);
        var first = ScanCache.Load(Consumer, compilation, []);
        first.SetTypes("used", kept, Types("Kept", "A"));
        first.SetTypes("unused-request", kept, Types("Kept", "A"));
        first.SetTypes("used", removed, Types("Removed", "B"));
        first.Set("used-store", kept, new TestPayload("kept", 1), TestPayloadJsonContext.Default.TestPayload, 1);
        first.Set("unused-store", kept, new TestPayload("gone", 1), TestPayloadJsonContext.Default.TestPayload, 1);

        var nextReference = MetadataReference.CreateFromImage(keptImage);
        var nextCompilation = CreateConsumer(nextReference);
        var nextKept = GetAssembly(nextCompilation, nextReference);
        var second = ScanCache.Load(Consumer, nextCompilation, [CacheText(Consumer, first.Serialize())]);
        _ = second.TryGetTypes("used", nextKept, out _);
        _ = second.TryGet("used-store", nextKept, TestPayloadJsonContext.Default.TestPayload, 1, out _);
        var written = ScanCacheFile.Parse(second.Serialize());

        await written.Scans!.Keys.Should().BeEquivalentTo(["used"]);
        await written.Scans["used"]!.Keys.Should().BeEquivalentTo(["Kept"]);
        await written.Store!.Keys.Should().BeEquivalentTo(["used-store"]);
    }

    [Test]
    public async Task Serializes_Deterministically_Regardless_Of_Set_Order()
    {
        var (compilation, assembly) = Referencing(EmitLibrary("public class A { }"));
        var first = ScanCache.Load(Consumer, compilation, []);
        first.SetTypes("b", assembly, Types("Lib", "A"));
        first.SetTypes("a", assembly, Types("Lib", "A"));
        var second = ScanCache.Load(Consumer, compilation, []);
        second.SetTypes("a", assembly, Types("Lib", "A"));
        second.SetTypes("b", assembly, Types("Lib", "A"));

        await first.Serialize().Should().BeEqualTo(second.Serialize());
    }

    [Test]
    public async Task Typed_Store_Round_Trips_Across_Builds()
    {
        var image = EmitLibrary("public class A { }");
        var (compilation, assembly) = Referencing(image);
        var first = ScanCache.Load(Consumer, compilation, []);
        first.Set("metadata", assembly, new TestPayload("value", 42), TestPayloadJsonContext.Default.TestPayload, 3);

        var (nextCompilation, nextAssembly) = Referencing(image);
        var second = ScanCache.Load(Consumer, nextCompilation, [CacheText(Consumer, first.Serialize())]);

        await second.TryGet("metadata", nextAssembly, TestPayloadJsonContext.Default.TestPayload, 3, out var value).Should().BeTrue();
        await value.Should().BeEqualTo(new TestPayload("value", 42));
    }

    [Test]
    public async Task Typed_Store_Misses_When_The_Data_Version_Changes()
    {
        var image = EmitLibrary("public class A { }");
        var (compilation, assembly) = Referencing(image);
        var first = ScanCache.Load(Consumer, compilation, []);
        first.Set("metadata", assembly, new TestPayload("value", 42), TestPayloadJsonContext.Default.TestPayload, 3);

        var (nextCompilation, nextAssembly) = Referencing(image);
        var second = ScanCache.Load(Consumer, nextCompilation, [CacheText(Consumer, first.Serialize())]);

        await second.TryGet("metadata", nextAssembly, TestPayloadJsonContext.Default.TestPayload, 4, out _).Should().BeFalse();
    }

    [Test]
    public async Task Typed_Store_Misses_After_The_Assembly_Is_Rebuilt()
    {
        var (compilation, assembly) = Referencing(EmitLibrary("public class A { }"));
        var first = ScanCache.Load(Consumer, compilation, []);
        first.Set("metadata", assembly, new TestPayload("value", 42), TestPayloadJsonContext.Default.TestPayload, 3);

        var (nextCompilation, nextAssembly) = Referencing(EmitLibrary("public class A { } public class B { }"));
        var second = ScanCache.Load(Consumer, nextCompilation, [CacheText(Consumer, first.Serialize())]);

        await second.TryGet("metadata", nextAssembly, TestPayloadJsonContext.Default.TestPayload, 3, out _).Should().BeFalse();
    }

    [Test]
    public async Task Typed_Store_Misses_When_The_Payload_Does_Not_Fit_The_Type()
    {
        var image = EmitLibrary("public class A { }");
        var (compilation, assembly) = Referencing(image);
        var mvid = ModuleVersionIds.Get(compilation, assembly);
        var text = Json($"{{'schema':{ScanCacheFile.SchemaVersion},'store':{{'metadata':{{'Lib':{{'mvid':'{mvid}','version':1,'data':[1,2]}}}}}}}}");
        var cache = ScanCache.Load(Consumer, compilation, [CacheText(Consumer, text)]);

        await cache.TryGet("metadata", assembly, TestPayloadJsonContext.Default.TestPayload, 1, out _).Should().BeFalse();
    }

    [Test]
    public async Task Only_Reads_The_Cache_File_Of_Its_Own_Consumer()
    {
        var image = EmitLibrary("public class A { }");
        var (compilation, assembly) = Referencing(image);
        var other = ScanCache.Load("OtherConsumer", compilation, []);
        other.SetTypes("request", assembly, Types("Lib", "Other"));
        var mine = ScanCache.Load(Consumer, compilation, []);
        mine.SetTypes("request", assembly, Types("Lib", "Mine"));
        AdditionalText[] texts = [CacheText("OtherConsumer", other.Serialize()), CacheText(Consumer, mine.Serialize())];

        var (nextCompilation, nextAssembly) = Referencing(image);
        var cache = ScanCache.Load(Consumer, nextCompilation, texts);
        var onlyOther = ScanCache.Load(Consumer, nextCompilation, [CacheText("OtherConsumer", other.Serialize())]);

        await cache.TryGetTypes("request", nextAssembly, out var types).Should().BeTrue();
        await types.Should().BeEqualTo(Types("Lib", "Mine"));
        await onlyOther.TryGetTypes("request", nextAssembly, out _).Should().BeFalse();
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("{\"schema\":1,")]
    [Arguments("{\"schema\":-1,\"scans\":{}}")]
    public async Task Ignores_Unreadable_Cache_Files(string? text)
    {
        var (compilation, assembly) = Referencing(EmitLibrary("public class A { }"));

        var cache = ScanCache.Load(Consumer, compilation, [CacheText(Consumer, text)]);

        await cache.TryGetTypes("request", assembly, out _).Should().BeFalse();
        await cache.TryGet("metadata", assembly, TestPayloadJsonContext.Default.TestPayload, 1, out _).Should().BeFalse();
    }

    [Test]
    public async Task Rejects_Invalid_Consumer_Ids()
    {
        var compilation = CreateConsumer();

        await Act(() => ScanCache.Load("bad/id", compilation, [])).Should().Throw<ArgumentException>();
        await Act(() => ScanCache.Load("", compilation, [])).Should().Throw<ArgumentException>();
    }

    [Test]
    public async Task Supports_Concurrent_Use()
    {
        var references = Enumerable
                        .Range(0, 8)
                        .Select(i => MetadataReference.CreateFromImage(EmitLibrary($"public class A{i} {{ }}", $"Lib{i}")))
                        .ToArray();
        var compilation = CreateConsumer(references);
        var assemblies = references.Select(reference => GetAssembly(compilation, reference)).ToArray();
        var cache = ScanCache.Load(Consumer, compilation, []);

        Parallel.For(
            0,
            64,
            i =>
            {
                var assembly = assemblies[i % assemblies.Length];
                cache.SetTypes($"request{i}", assembly, Types(assembly.Name, $"A{i}"));
                cache.Set($"store{i}", assembly, new TestPayload(assembly.Name, i), TestPayloadJsonContext.Default.TestPayload, 1);
            }
        );
        var written = ScanCacheFile.Parse(cache.Serialize());

        await written.Scans!.Count.Should().BeEqualTo(64);
        await written.Store!.Count.Should().BeEqualTo(64);
    }

    [Test]
    public async Task Save_Writes_The_Consumer_File_Into_The_Intermediate_Output_Path()
    {
        using var temp = CreateTempDirectory();
        var (compilation, assembly) = Referencing(EmitLibrary("public class A { }"));
        var cache = ScanCache.Load(Consumer, compilation, []);
        cache.SetTypes("request", assembly, Types("Lib", "A"));

        cache.Save(Options(("build_property.IntermediateOutputPath", Path.Combine(temp.Path, "obj"))));

        var path = Path.Combine(temp.Path, "obj", "ctp", Consumer + ".ctpjson");
        await File.Exists(path).Should().BeTrue();
        await File.ReadAllText(path).Should().BeEqualTo(cache.Serialize());
    }

    private static (Compilation Compilation, IAssemblySymbol Assembly) Referencing(byte[] image)
    {
        var reference = MetadataReference.CreateFromImage(image);
        var compilation = CreateConsumer(reference);
        return (compilation, GetAssembly(compilation, reference));
    }
}
