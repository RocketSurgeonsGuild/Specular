using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;

namespace Specular.Scanning.Tests.Scanning;

/// <summary>In-memory compilations that reference the real <c>TestAssembly</c> fixture.</summary>
internal static class ScanCompilation
{
    public const string ProjectAssemblyName = "TestProject";
    public const string TestAssemblyName = "TestAssembly";

    private static readonly CSharpParseOptions parseOptions = new(LanguageVersion.Preview);

    /// <summary>A small, fixed reference set so whole-compilation scans stay fast and their results predictable.</summary>
    public static ImmutableArray<MetadataReference> References { get; } = CreateReferences();

    public static CSharpCompilation Create(string source = "", string assemblyName = ProjectAssemblyName, params MetadataReference[] additionalReferences) =>
        CSharpCompilation.Create(
            assemblyName,
            [CSharpSyntaxTree.ParseText(source, parseOptions)],
            References.AddRange(additionalReferences),
            new(OutputKind.DynamicallyLinkedLibrary, nullableContextOptions: NullableContextOptions.Enable)
        );

    /// <summary>Compiles <paramref name="source" /> to an image so it is scanned as a metadata reference, like a package or project output.</summary>
    public static MetadataReference EmitReference(string source, string assemblyName, params MetadataReference[] additionalReferences)
    {
        var compilation = Create(source, assemblyName, additionalReferences);
        using var stream = new MemoryStream();
        var result = compilation.Emit(stream);
        return !result.Success
            ? throw new InvalidOperationException(string.Join(Environment.NewLine, result.Diagnostics.Select(static z => z.ToString())))
            : (MetadataReference)MetadataReference.CreateFromImage(stream.ToArray());
    }

    private static ImmutableArray<MetadataReference> CreateReferences()
    {
        var runtimeDirectory = Path.GetDirectoryName(typeof(object).Assembly.Location)!;
        return
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(Path.Combine(runtimeDirectory, "System.Runtime.dll")),
            MetadataReference.CreateFromFile(typeof(TestAssembly.IService).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(ServiceRegistrationAttribute).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(ServiceLifetime).Assembly.Location),
        ];
    }
}

/// <summary>Builds <see cref="ScanRequest" />s directly, without the selector API.</summary>
internal static class Scans
{
    public static AssemblyFilterData AllAssemblies { get; } = AssemblyFilterData.Empty with { AllAssemblies = true };

    public static EquatableArray<T> Array<T>(params T[] items) => ImmutableArray.Create(items);

    public static AssemblyFilterData FromAssemblies(params string[] names) => AssemblyFilterData.Empty with { Assemblies = Array(names) };

    public static ScanRequest Assemblies(AssemblyFilterData filter) => new(ScanKind.Assemblies, filter, null);

    public static ScanRequest Types(AssemblyFilterData filter, TypeFilterData? types = null) => new(ScanKind.Types, filter, types ?? TypeFilterData.All);

    public static TypeReference Type(string metadataName, string assemblyName = ScanCompilation.TestAssemblyName, bool unboundGeneric = false) =>
        new(assemblyName, metadataName, unboundGeneric);

    public static ImmutableArray<string> AssemblyNames(ScanResult result) => [.. result.Assemblies.Select(static z => z.Name)];

    public static ImmutableArray<string> TypeNames(ScanResult result) => [.. result.Types.Select(static z => z.MetadataName)];

    /// <summary>Scans only <c>TestAssembly</c> and keeps the types declared in its own namespace (dropping compiler-embedded attributes).</summary>
    public static ImmutableArray<string> TestAssemblyTypes(TypeFilterData types, Compilation? compilation = null)
    {
        var result = Scanner.Scan(compilation ?? ScanCompilation.Create(), Types(FromAssemblies(ScanCompilation.TestAssemblyName), types));
        return [.. TypeNames(result).Where(static z => z.StartsWith("TestAssembly.", StringComparison.Ordinal))];
    }

    /// <summary>Scans only the compilation's own assembly.</summary>
    public static ImmutableArray<string> ProjectTypes(Compilation compilation, TypeFilterData types)
    {
        var result = Scanner.Scan(compilation, Types(FromAssemblies(compilation.Assembly.MetadataName), types));
        return [.. TypeNames(result).Where(static z => !z.StartsWith("Microsoft.CodeAnalysis.", StringComparison.Ordinal) && !z.StartsWith("System.", StringComparison.Ordinal))];
    }
}

/// <summary>A custom filter that records what it was asked about.</summary>
internal sealed class RecordingNameFilter(string cacheKey, Func<INamedTypeSymbol, bool> predicate) : CustomTypeFilter
{
    public List<string> Visited { get; } = [];

    public override string CacheKey => cacheKey;

    public override bool IsMatch(Compilation compilation, INamedTypeSymbol type)
    {
        Visited.Add(type.Name);
        return predicate(type);
    }
}

/// <summary>An <see cref="IAssemblyScanCache" /> that serves canned entries and records every call.</summary>
internal sealed class FakeAssemblyScanCache : IAssemblyScanCache
{
    public Dictionary<string, EquatableArray<ScannedType>> Entries { get; } = [with(StringComparer.Ordinal)];
    public List<string> Lookups { get; } = [];
    public List<(string Assembly, EquatableArray<ScannedType> Types)> Stored { get; } = [];

    public bool TryGetTypes(ScanRequest request, IAssemblySymbol assembly, out EquatableArray<ScannedType> types)
    {
        Lookups.Add(assembly.MetadataName);
        return Entries.TryGetValue(assembly.MetadataName, out types);
    }

    public void SetTypes(ScanRequest request, IAssemblySymbol assembly, EquatableArray<ScannedType> types) => Stored.Add((assembly.MetadataName, types));
}
