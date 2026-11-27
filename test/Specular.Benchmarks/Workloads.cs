using System.Collections.Immutable;
using System.ComponentModel;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Specular.Analyzers;
using Specular.Tests;
using TestAssembly;

namespace Specular.Benchmarks;

/// <summary>What one benchmark case compiles: the app's sources, its references and the generator options.</summary>
public sealed class Workload(string name, ImmutableArray<SyntaxTree> sources, Func<ImmutableArray<MetadataReference>> references, string excludedAssemblies)
{
    public string Name { get; } = name;

    public ImmutableArray<SyntaxTree> Sources { get; } = sources;

    /// <summary>The tree the edit-rerun benchmark appends a comment to.</summary>
    public SyntaxTree EditedTree => Sources[0];

    public AnalyzerConfigOptionsProvider Options { get; } = new GlobalOptionsProvider(
        ImmutableDictionary<string, string>.Empty
           .Add("build_property.SpecularEmitProvider", "true")
           .Add("build_property.ExcludeAssemblyFromCTP", excludedAssemblies)
    );

    /// <summary>
    ///     A compilation over freshly loaded references. Specular remembers referenced assemblies' results per
    ///     <see cref="MetadataReference" /> instance, so new instances start it with an empty memo, as a command-line build does.
    /// </summary>
    public CSharpCompilation CreateCompilation() => CSharpCompilation.Create(
        "BenchmarkApp",
        Sources,
        references(),
        new(
            OutputKind.ConsoleApplication,
            nullableContextOptions: NullableContextOptions.Enable,
            specificDiagnosticOptions: [new("CS1701", ReportDiagnostic.Suppress), new("CS1702", ReportDiagnostic.Suppress)]
        )
    );

    public GeneratorDriver CreateDriver() => CSharpGeneratorDriver.Create(
        [new SpecularProviderGenerator().AsSourceGenerator()],
        parseOptions: Workloads.ParseOptions,
        optionsProvider: Options
    );

    public override string ToString() => Name;
}

public static class Workloads
{
    public static CSharpParseOptions ParseOptions { get; } = new(LanguageVersion.Preview);

    /// <summary>Every case the benchmarks run, by name.</summary>
    public static IEnumerable<string> Names =>
    [
        "all-together",
        .. from types in SweepTypes from requests in SweepRequests select $"synthetic-t{types}-r{requests}",
        "aspnet-exclude-none",
        "aspnet-exclude-microsoft",
    ];

    public static Workload Get(string name) => name switch
    {
        "all-together" => AllTogether(),
        "aspnet-exclude-none" => AspNet(""),
        "aspnet-exclude-microsoft" => AspNet("Microsoft"),
        _ when name.StartsWith("synthetic-t", StringComparison.Ordinal) => ParseSynthetic(name),
        _ => throw new ArgumentOutOfRangeException(nameof(name), name, null),
    };

    private static readonly int[] SweepTypes = [100, 1_000, 5_000];
    private static readonly int[] SweepRequests = [5, 50];
    private const int LibraryTypes = 200;
    private const int InheritedRequests = 5;
    private const int AspNetTypes = 1_000;
    private const int AspNetRequests = 50;

    private static Workload ParseSynthetic(string name)
    {
        var parts = name["synthetic-t".Length..].Split("-r");
        return Synthetic(name, int.Parse(parts[0], System.Globalization.CultureInfo.InvariantCulture), int.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture));
    }

    // The sample every scanning test runs: every kind of request against the test fixture assembly.
    private static Workload AllTogether()
    {
        var item = AssemblyScanningTestData.GetTestData().Select(z => z()).Single(z => z.Name == "all-together");
        return new("all-together", [Parse(item.Source, "Program.cs")], () => [.. HostReferences(), Load(typeof(IService))], "Microsoft");
    }

    // A large app: generated services and scans, plus a library that declares scans of its own (inherited requests).
    private static Workload Synthetic(string name, int types, int requests)
    {
        var library = Library.Value;
        return new(
            name,
            [Parse(ProgramSource(requests, "Synthetic"), "Program.cs"), Parse(TypesSource("Synthetic", types), "Types.cs")],
            () => [.. HostReferences(), MetadataReference.CreateFromImage(library)],
            "Microsoft"
        );
    }

    // The same app shape against the full ASP.NET Core reference set (~200 assemblies), to stress walking references.
    private static Workload AspNet(string excluded) => new(
        excluded.Length == 0 ? "aspnet-exclude-none" : "aspnet-exclude-microsoft",
        [Parse(ProgramSource(AspNetRequests, "Synthetic"), "Program.cs"), Parse(TypesSource("Synthetic", AspNetTypes), "Types.cs")],
        () =>
        [
            .. Basic.Reference.Assemblies.AspNet100.ReferenceInfos.All.Select(z => (MetadataReference)MetadataReference.CreateFromImage(z.ImageBytes, filePath: z.FileName)),
            Load(typeof(ISpecularProvider)),
            MetadataReference.CreateFromImage(Library.Value),
        ],
        excluded
    );

    /// <summary>A library with its own services and scans, compiled once with the generator so it carries its requests in [AssemblyMetadata].</summary>
    private static readonly Lazy<ImmutableArray<byte>> Library = new(() =>
    {
        var compilation = CSharpCompilation.Create(
            "Synthetic.Library",
            [Parse(LibrarySource(InheritedRequests), "Library.cs"), Parse(TypesSource("Synthetic.Library", LibraryTypes), "Types.cs")],
            HostReferences(),
            new(OutputKind.DynamicallyLinkedLibrary, nullableContextOptions: NullableContextOptions.Enable)
        );
        var driver = CSharpGeneratorDriver.Create(
            [new SpecularProviderGenerator().AsSourceGenerator()],
            parseOptions: ParseOptions,
            optionsProvider: new GlobalOptionsProvider(
                ImmutableDictionary<string, string>.Empty.Add("build_property.SpecularEmitProvider", "true").Add("build_property.ExcludeAssemblyFromCTP", "Microsoft")
            )
        );
        _ = driver.RunGeneratorsAndUpdateCompilation(compilation, out var output, out _);
        using var stream = new MemoryStream();
        var result = output.Emit(stream);
        return  !result.Success 
            ? throw new InvalidOperationException(
                "The synthetic library did not compile:\n" + string.Join("\n", result.Diagnostics.Where(z => z.Severity == DiagnosticSeverity.Error))
            )
            :   [.. stream.ToArray()];
    });

    private static IEnumerable<MetadataReference> HostReferences() =>
    [
        Load(typeof(object)),
        Load(typeof(Enumerable)),
        Load(typeof(ImmutableList<>)),
        Load(typeof(Attribute)),
        Load(typeof(EditorBrowsableAttribute)),
        Load(typeof(ActivatorUtilities)),
        Load(typeof(IServiceProvider)),
        Load(typeof(IServiceCollection)),
        Load(typeof(ServiceCollection)),
        Load(typeof(ISpecularProvider)),
        .. AppDomain.CurrentDomain.GetAssemblies()
                    .Where(z => !z.IsDynamic && z.GetName().Name is "System.Runtime" or "netstandard" or "System.Collections" or "System.ComponentModel")
                    .Select(z => (MetadataReference)MetadataReference.CreateFromFile(z.Location)),
    ];

    // From the file each time: a new instance per compilation, so nothing is remembered between cold runs.
    private static MetadataReference Load(Type type) => MetadataReference.CreateFromFile(type.Assembly.Location);

    private static SyntaxTree Parse(string source, string path) => CSharpSyntaxTree.ParseText(source, ParseOptions, path, Encoding.UTF8);

    private static string ProgramSource(int requests, string rootNamespace)
    {
        var calls = new StringBuilder();
        for (var r = 0; r < requests; r++)
        {
            _ = ( r % 3 ) switch
            {
                0 => calls.AppendLine($"        provider.GetTypes(z => z.FromAssemblies().GetTypes(x => x.AssignableTo<{rootNamespace}.N0.IHandler{r % 10}>()));"),
                1 => calls.AppendLine(
                    $"        provider.Scan(services, z => z.FromAssemblyOf<Program>().AddClasses(x => x.InNamespaces(\"{rootNamespace}.N{r % 10}\"), false).AsImplementedInterfaces().WithScopedLifetime());"
                ),
                _ => calls.AppendLine($"        provider.GetTypes(z => z.FromAssemblyOf<Program>().GetTypes(x => x.EndsWith(\"{r % 10}\")));"),
            };
        }

        return $$"""
            using Specular;
            using Specular.Abstractions;
            using Microsoft.Extensions.DependencyInjection;

            public class Program
            {
                public static void Main()
                {
                    var services = new ServiceCollection();
                    ISpecularProvider provider = SpecularProvider.Instance;
            {{calls}}
                }
            }
            """;
    }

    private static string LibrarySource(int requests)
    {
        var calls = new StringBuilder();
        for (var r = 0; r < requests; r++)
        {
            _ = calls.AppendLine(
                r % 2 == 0
                    ? $"        provider.GetTypes(z => z.FromAssemblies().GetTypes(x => x.AssignableTo<Synthetic.Library.N0.IHandler{r % 10}>()));"
                    : $"        provider.Scan(services, z => z.FromAssemblies().AddClasses(x => x.EndsWith(\"{r % 10}\"), false).AsImplementedInterfaces().WithScopedLifetime());"
            );
        }

        return $$"""
            using Specular;
            using Specular.Abstractions;
            using Microsoft.Extensions.DependencyInjection;

            namespace Synthetic.Library;

            public static class LibraryRegistrations
            {
                public static void Register(IServiceCollection services)
                {
                    ISpecularProvider provider = SpecularProvider.Instance;
            {{calls}}
                }
            }
            """;
    }

    // Ten namespaces of handler interfaces, services that implement them, and some records, enums and abstract classes for kind filters.
    private static string TypesSource(string rootNamespace, int types)
    {
        var source = new StringBuilder();
        for (var n = 0; n < 10; n++)
        {
            _ = source.AppendLine($"namespace {rootNamespace}.N{n}").AppendLine("{");
            if (n == 0)
            {
                for (var h = 0; h < 10; h++) _ = source.AppendLine($"    public interface IHandler{h} {{ }}");
            }

            for (var i = n; i < types; i += 10)
            {
                _ = ( i % 7 ) switch
                {
                    0 => source.AppendLine($"    public record Message{i}(int Value);"),
                    1 => source.AppendLine($"    public enum Kind{i} {{ A, B }}"),
                    2 => source.AppendLine($"    public abstract class Base{i} : {rootNamespace}.N0.IHandler{i % 10} {{ }}"),
                    3 => source.AppendLine($"    internal sealed class Internal{i} : {rootNamespace}.N0.IHandler{i % 10} {{ }}"),
                    _ => source.AppendLine($"    public class Service{i} : {rootNamespace}.N0.IHandler{i % 10}, System.IDisposable {{ public void Dispose() {{ }} }}"),
                };
            }

            _ = source.AppendLine("}");
        }

        return source.ToString();
    }
}

/// <summary>Global analyzer options for driving the generator directly.</summary>
internal sealed class GlobalOptionsProvider(ImmutableDictionary<string, string> globalOptions) : AnalyzerConfigOptionsProvider
{
    public override AnalyzerConfigOptions GlobalOptions { get; } = new Options(globalOptions);
    public override AnalyzerConfigOptions GetOptions(SyntaxTree tree) => Options.Empty;
    public override AnalyzerConfigOptions GetOptions(AdditionalText textFile) => Options.Empty;

    private sealed class Options(ImmutableDictionary<string, string> values) : AnalyzerConfigOptions
    {
        public static Options Empty { get; } = new([]);
        public override bool TryGetValue(string key, [NotNullWhen(true)] out string? value) => values.TryGetValue(key, out value);
    }
}
