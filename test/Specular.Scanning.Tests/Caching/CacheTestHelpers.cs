using System.Collections.Immutable;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace Specular.Scanning.Tests.Caching;

internal static class CacheTestHelpers
{
    private static readonly ImmutableArray<MetadataReference> FrameworkReferences =
    [
        MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
    ];

    /// <summary>Emits a small library; different <paramref name="source" /> yields a different MVID (deterministic emit).</summary>
    public static byte[] EmitLibrary(string source, string assemblyName = "Lib")
    {
        var compilation = CreateLibraryCompilation(source, assemblyName);
        using var stream = new MemoryStream();
        var result = compilation.Emit(stream);
        return result.Success
            ? stream.ToArray()
            : throw new InvalidOperationException(string.Join(Environment.NewLine, result.Diagnostics.Select(d => d.ToString())));
    }

    public static CSharpCompilation CreateLibraryCompilation(string source, string assemblyName = "Lib") =>
        CSharpCompilation.Create(
            assemblyName,
            [CSharpSyntaxTree.ParseText(source)],
            FrameworkReferences,
            new(OutputKind.DynamicallyLinkedLibrary, deterministic: true)
        );

    public static CSharpCompilation CreateConsumer(params MetadataReference[] references) =>
        CSharpCompilation.Create(
            "Consumer",
            [CSharpSyntaxTree.ParseText("public class ConsumerType { }")],
            FrameworkReferences.AddRange(references),
            new(OutputKind.DynamicallyLinkedLibrary)
        );

    public static IAssemblySymbol GetAssembly(Compilation compilation, MetadataReference reference) =>
        compilation.GetAssemblyOrModuleSymbol(reference) as IAssemblySymbol
     ?? throw new InvalidOperationException("The reference is not an assembly of the compilation.");

    public static EquatableArray<ScannedType> Types(string assemblyName, params string[] metadataNames) =>
        metadataNames.Select(name => new ScannedType(assemblyName, name, TypeKind.Class, Accessibility.Public, true, 0)).ToImmutableArray();

    public static AdditionalText CacheText(string consumerId, string? content, string directory = "/obj/ctp") =>
        new InMemoryAdditionalText(Path.Combine(directory, consumerId + ".ctpjson"), content);

    /// <summary>Gives a lambda a delegate type so exception assertions can chain off it.</summary>
    public static Action Act(Action action) => action;

    /// <summary>Lets tests write JSON with single quotes.</summary>
    public static string Json(string singleQuoted) => singleQuoted.Replace('\'', '"');

    public static AnalyzerConfigOptions Options(params (string Key, string Value)[] values) => new DictionaryAnalyzerConfigOptions(values);

    public static TempDirectory CreateTempDirectory() => new();

    internal sealed class TempDirectory : IDisposable
    {
        public TempDirectory()
        {
            Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "specular-scanning-tests", Guid.NewGuid().ToString("N"));
            _ = Directory.CreateDirectory(Path);
        }

        public string Path { get; }

        public void Dispose()
        {
            if (Directory.Exists(Path))
            {
                Directory.Delete(Path, true);
            }
        }
    }

    private sealed class InMemoryAdditionalText(string path, string? content) : AdditionalText
    {
        public override string Path { get; } = path;

        public override SourceText? GetText(CancellationToken cancellationToken = default) =>
            content is null ? null : SourceText.From(content, Encoding.UTF8);
    }

    private sealed class DictionaryAnalyzerConfigOptions((string Key, string Value)[] values) : AnalyzerConfigOptions
    {
        private readonly Dictionary<string, string> _values = values.ToDictionary(z => z.Key, z => z.Value, StringComparer.OrdinalIgnoreCase);

        // A non-nullable out parameter avoids NotNullWhenAttribute, which InternalsVisibleTo makes ambiguous with the library's Polyfill copy.
        public override bool TryGetValue(string key, out string value)
        {
            if (_values.TryGetValue(key, out var found))
            {
                value = found;
                return true;
            }

            value = string.Empty;
            return false;
        }
    }
}

internal sealed record TestPayload(string Name, int Count);

[JsonSerializable(typeof(TestPayload))]
internal sealed partial class TestPayloadJsonContext : JsonSerializerContext;
