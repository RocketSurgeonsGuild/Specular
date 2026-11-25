using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using DiffEngine;
using EmptyFiles;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Rocket.Surgery.Extensions.Testing.SourceGenerators;
using Specular.Analyzers;

namespace Specular.Tests;

internal static partial class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        FileExtensions.AddTextExtension(Path.GetExtension(VerifyExtensions.CacheFileName));
        TempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        VerifierSettings.ScrubReplace(TempDirectory, "{TempDirectory}");
        VerifierSettings.ScrubReplace(TempDirectory.Replace("\\", "/"), "{TempDirectory}");
        VerifyGeneratorTextContext.Initialize(DiagnosticSeverity.Warning, Customizers.Default, (results, targets, data) => targets.RemoveAll(z => z.Name == "Microsoft.CodeAnalysis.EmbeddedAttribute.cs"), Customizers.ExcludeParseOptions);
        VerifierSettings.DontScrubUserProfile();

        VerifierSettings.AssignTargetAssembly(typeof(ModuleInitializer).Assembly);
        ScrubSolutionAndProjectDirectories();

        VerifierSettings.ScrubInlineGuids();
        VerifierSettings.IgnoreMember<SourceLocation>(z => z.FilePath);

        VerifierSettings.AddExtraSettings(settings => settings.Converters.Add(new ServiceDescriptorConverter()));

        VerifierSettings.RegisterFileConverter<GeneratorTestResultsWithServices>(Convert);
        VerifierSettings.RegisterFileConverter<GeneratorTestResultsWithCacheFiles>(Convert);

        DiffRunner.Disabled = true;
        DerivePathInfo(
            (sourceFile, projectDirectory, type, method) =>
            {
                static string GetTypeName(Type type) => type.IsNested ? $"{type.ReflectedType!.Name}.{type.Name}" : type.Name;

                var typeName = GetTypeName(type);

                var path = Path.Combine(Path.GetDirectoryName(sourceFile)!, "snapshots");
                return new(path, typeName, method.Name);
            }
        );
        VerifierSettings.ScrubLinesWithReplace(
            s => s.Contains("AssemblyMetadata(\"AssemblyProvider.", StringComparison.OrdinalIgnoreCase)
                ? s[..( s.IndexOf('"', s.IndexOf('"') + 1) + 2 )] + "\"{scrubbed}\")]"
                : s
        );
        VerifierSettings.ScrubLinesWithReplace(
            s => s.Contains("SpecularHashAttribute", StringComparison.OrdinalIgnoreCase) && s.IndexOf('"') > -1
                ? s.Replace(s[s.IndexOf('"')..s.LastIndexOf(')')], "\"{scrubbed}\"")
                : s
        );
        VerifierSettings.ScrubLinesWithReplace(
            s => s.Contains("<Compiled_AssemblyProvider_g>", StringComparison.OrdinalIgnoreCase)
                ? s[..( s.IndexOf('"') + 1 )] + "{SpecularProvider}" + s[s.LastIndexOf('"')..]
                : s
        );
        VerifierSettings.AddScrubber(
            (builder, counter) =>
            {
                // The generator's versions, read from the typed dependency information rather than its assembly.
                if (AssemblyInfo.Attributes.FileVersion is { Length: > 0 } version)
                {
                    _ = builder.Replace(version, "version");
                }

                if (AssemblyInfo.Attributes.Version is { Length: > 0 } version2)
                {
                    _ = builder.Replace(version2, "version");
                }

                // regex to replace the version number in this string Version=12.0.0.0,
                var regex = MyRegex();
                var result = regex.Replace(builder.ToString(), "Version=version,");
                _ = builder.Clear();
                _ = builder.Append(result);
            }
        );
    }

    // Verify sorts its directory replacements by length, so when the working directory equals the solution
    // directory (dotnet test from the repo root) {CurrentDirectory} wins the tie, while Rider's working
    // directory is the bin folder and yields {SolutionDirectory}. Engine scrubbers run before Verify's
    // directory pass, so replacing these first makes the output independent of the working directory.
    private static void ScrubSolutionAndProjectDirectories()
    {
        var pairs = new List<(string Find, string Replacement)>();

        void Add(string directory, string replacement)
        {
            var trimmed = directory.TrimEnd('/', '\\');
            if (trimmed.Length <= 1) return;
            // the trailing separator is consumed, matching Verify's own `{SolutionDirectory}src/...` form
            pairs.Add((trimmed.Replace('\\', '/') + "/", replacement));
            pairs.Add((trimmed.Replace('/', '\\') + "\\", replacement));
        }

        var assembly = typeof(ModuleInitializer).Assembly;
        Add(AttributeReader.GetProjectDirectory(assembly), "{ProjectDirectory}");
        if (AttributeReader.TryGetSolutionDirectory(assembly, out var solutionDirectory))
        {
            Add(solutionDirectory, "{SolutionDirectory}");
        }

        VerifierSettings.ScrubReplace(StringComparison.Ordinal, false, [.. pairs.Distinct()]);
    }

    public static string TempDirectory { get; private set; } = null!;
    public const int TestTimeout = 60 * 60 * 1000;

    private class ServiceDescriptorConverter : WriteOnlyJsonConverter<ServiceDescriptor>
    {
        public override void Write(VerifyJsonWriter writer, ServiceDescriptor value)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(nameof(ServiceDescriptor.Lifetime));
            writer.WriteValue(value.Lifetime.ToString());
            writer.WritePropertyName(nameof(ServiceDescriptor.ServiceType));
            writer.WriteValue(value.ServiceType.FullName);
            if (value.IsKeyedService)
            {
                writer.WritePropertyName(nameof(ServiceDescriptor.ServiceKey));
                writer.WriteValue(value.ServiceKey);
                if (value.KeyedImplementationType?.FullName is { } implementationType)
                {
                    writer.WritePropertyName(nameof(ServiceDescriptor.KeyedImplementationType));
                    writer.WriteValue(implementationType);
                }
                else if (value.KeyedImplementationInstance is { } implementationInstance)
                {
                    writer.WritePropertyName(nameof(ServiceDescriptor.KeyedImplementationInstance));
                    writer.WriteValue(implementationInstance.ToString());
                }
                else if (value.KeyedImplementationFactory is { } implementationFactory)
                {
                    writer.WritePropertyName(nameof(ServiceDescriptor.KeyedImplementationFactory));
                    writer.WriteValue(implementationFactory.ToString());
                }
            }
            else
            {
                if (value.ImplementationType?.FullName is { } implementationType)
                {
                    writer.WritePropertyName(nameof(ServiceDescriptor.ImplementationType));
                    writer.WriteValue(implementationType);
                }
                else if (value.ImplementationInstance is { } implementationInstance)
                {
                    writer.WritePropertyName(nameof(ServiceDescriptor.ImplementationInstance));
                    writer.WriteValue(implementationInstance.ToString());
                }
                else if (value.ImplementationFactory is { } implementationFactory)
                {
                    writer.WritePropertyName(nameof(ServiceDescriptor.ImplementationFactory));
                    writer.WriteValue(implementationFactory.ToString());
                }
            }

            writer.WriteEndObject();
        }
    }

    internal static IEnumerable<Diagnostic> OrderDiagnosticResults(this IEnumerable<Diagnostic> diagnostics, DiagnosticSeverity severity) => diagnostics
       .Where(s => s.Severity >= severity)
       .OrderBy(static z => z.Location.GetMappedLineSpan().ToString())
       .ThenBy(static z => z.Severity)
       .ThenBy(static z => z.Id);

    private static ConversionResult Convert(GeneratorTestResultsWithServices result, IReadOnlyDictionary<string, object> context)
    {
        var target = result.Results;
        var services = result.Services;
        var targets = new List<Target>();
        foreach (var item in target.Results)
        {
            targets.AddRange(item.Value.SyntaxTrees.Select(Selector));
        }

        var data = new Dictionary<string, object>
        {
            ["GlobalOptions"] = target.GlobalOptions,
            ["FileOptions"] = target.FileOptions,
            ["References"] = target
                            .FinalCompilation
                            .References
                            .Select(x => x.Display ?? "")
                            .Select(Path.GetFileName)
                            .Distinct()
                            .Order(),

            ["FinalDiagnostics"] = target.FinalDiagnostics.OrderDiagnosticResults(DiagnosticSeverity.Error),
            ["GeneratorDiagnostics"] = target.Results.ToDictionary(
                z => z.Key.FullName!,
                z => z.Value.Diagnostics.OrderDiagnosticResults(DiagnosticSeverity.Error)
            ),
            ["Services"] = services,
        };

        return new(data, targets);
    }

    private static ConversionResult Convert(GeneratorTestResultsWithCacheFiles result, IReadOnlyDictionary<string, object> context)
    {
        var target = result.Results;
        var targets = new List<Target>();
        foreach (var item in target.Results)
        {
            targets.AddRange(item.Value.SyntaxTrees.Select(Selector));
        }

        var generatorDiagnostics = target
                                  .Results
                                  .ToDictionary(
                                       z => z.Key.FullName!,
                                       z => z.Value.Diagnostics.OrderDiagnosticResults(
                                           DiagnosticSeverity.Error
                                       )
                                   );

        var data = new Dictionary<string, object>
        {
            ["GlobalOptions"] = target.GlobalOptions,
            ["FileOptions"] = target.FileOptions,
            ["References"] = target
                            .FinalCompilation
                            .References
                            .Select(x => x.Display ?? "")
                            .Select(Path.GetFileName)
                            .Distinct()
                            .Order(),

            ["FinalDiagnostics"] = target.FinalDiagnostics.OrderDiagnosticResults(DiagnosticSeverity.Error),
            ["GeneratorDiagnostics"] = generatorDiagnostics,
            ["Cache"] = ReadCacheFile(result.TempDirectory),
        };

        return new(data, targets);
    }

    // The cache file, by Specular's own reading of it: the scan results per referenced assembly, the requests each referenced assembly
    // carries, and what each call site generated per referenced assembly. Keys that hash requests or locations are left out (they hash
    // absolute paths and assembly versions); entries are sorted by what they describe instead.
    private static object? ReadCacheFile(string tempDirectory)
    {
        var file = Directory.EnumerateFiles(tempDirectory, VerifyExtensions.CacheFileName, SearchOption.AllDirectories).SingleOrDefault();
        if (file is null) return null;

        using var document = JsonDocument.Parse(File.ReadAllText(file));
        var root = document.RootElement;

        var scans = new List<(string Assembly, string[] Types)>();
        if (root.TryGetProperty("scans", out var scansElement))
        {
            foreach (var request in scansElement.EnumerateObject())
            {
                foreach (var assembly in request.Value.EnumerateObject())
                {
                    var types = assembly.Value.TryGetProperty("types", out var typesElement)
                        ? typesElement.EnumerateArray().Select(z => z.GetProperty("name").GetString() ?? "").ToArray()
                        : [];
                    scans.Add((assembly.Name, types));
                }
            }
        }

        var requests = new SortedDictionary<string, object?>(StringComparer.Ordinal);
        var sources = new List<(string Assembly, string Location, object? Source)>();
        if (root.TryGetProperty("store", out var storeElement))
        {
            foreach (var key in storeElement.EnumerateObject())
            {
                foreach (var assembly in key.Value.EnumerateObject())
                {
                    var data = assembly.Value.TryGetProperty("data", out var dataElement) ? ToObject(dataElement) : null;
                    if (key.Name == "requests")
                    {
                        requests[assembly.Name] = data;
                    }
                    else
                    {
                        var location = dataElement.TryGetProperty("Location", out var locationElement) ? locationElement.GetRawText() : "";
                        sources.Add((assembly.Name, location, data));
                    }
                }
            }
        }

        return new Dictionary<string, object?>
        {
            ["Scans"] = scans
                       .OrderBy(z => z.Assembly, StringComparer.Ordinal)
                       .ThenBy(z => string.Join(",", z.Types), StringComparer.Ordinal)
                       .Select(z => new { z.Assembly, z.Types })
                       .ToArray(),
            ["Requests"] = requests,
            ["Sources"] = sources
                         .OrderBy(z => z.Assembly, StringComparer.Ordinal)
                         .ThenBy(z => z.Location, StringComparer.Ordinal)
                         .Select(z => new { z.Assembly, z.Source })
                         .ToArray(),
        };

        static object? ToObject(JsonElement element) => element.ValueKind switch
        {
            JsonValueKind.Object => element
                                   .EnumerateObject()
                                   // A source location's absolute file path ("f"); its file name is part of the generated code already.
                                   .Where(z => z.Name != "f" || !element.TryGetProperty("e", out _))
                                   .ToDictionary(z => z.Name, z => z.Name == "PrivateAssemblies" ? ToSortedArray(z.Value) : ToObject(z.Value)),
            JsonValueKind.Array => element.EnumerateArray().Select(ToObject).ToArray(),
            JsonValueKind.String => element.GetString(),
            JsonValueKind.Number => element.GetRawText(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Null or JsonValueKind.Undefined => null,
            _ => null,
        };

        // A set, written in hash order.
        static object ToSortedArray(JsonElement element) => element.EnumerateArray().Select(z => z.GetString()).Order(StringComparer.Ordinal).ToArray();
    }

    [GeneratedRegex("Version=(.*?),", RegexOptions.Compiled)]
    private static partial Regex MyRegex();

    private static Target Selector(SyntaxTree source)
    {
        var hintPath = source.FilePath;
        var data = $@"//HintName: {hintPath.Replace("\\", "/")}
{source.GetText()}";
        return new("cs", data.Replace("\r", "", StringComparison.OrdinalIgnoreCase), Path.GetFileNameWithoutExtension(hintPath));
    }
}
