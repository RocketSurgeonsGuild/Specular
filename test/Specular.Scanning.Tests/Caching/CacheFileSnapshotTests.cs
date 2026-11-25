using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Specular.Scanning.Tests.Caching;

// The cache file a run leaves behind, as a snapshot, so a change to its format shows up as a readable diff.
public class CacheFileSnapshotTests
{
    private const string LibrarySource = """
        namespace Lib
        {
            public interface IHandler { }
            public class FirstHandler : IHandler { }
            internal class HiddenHandler : IHandler { }
            public class Outer<T> { public class NestedHandler : IHandler { } }
        }
        """;

    [Test]
    public Task Scan_and_store_entries()
    {
        var library = MetadataReference.CreateFromImage(CacheTestHelpers.EmitLibrary(LibrarySource));
        var compilation = CacheTestHelpers.CreateConsumer(library).AddSyntaxTrees(CSharpSyntaxTree.ParseText("public class ConsumerHandler : Lib.IHandler { }"));
        var cache = ScanCache.Load("SnapshotGen", compilation, []);

        var request = ScanRequest.Build(compilation, static s => s.FromAssemblies().GetTypes(f => f.AssignableTo("Lib.IHandler")));
        _ = Scanner.Scan(compilation, request, cache, CancellationToken.None);
        cache.Set("payload", CacheTestHelpers.GetAssembly(compilation, library), new TestPayload("lib", 3), TestPayloadJsonContext.Default.TestPayload, 1);

        return VerifyJson(cache.Serialize());
    }
}
