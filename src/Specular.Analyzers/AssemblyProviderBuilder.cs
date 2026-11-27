using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace Specular.Analyzers;

internal static class AssemblyProviderBuilder
{
    private const string TrimJustification =
        "Types resolved by string literal are preserved with their public constructors via [DynamicDependency], so this reflection is trim- and AOT-safe.";

    private const string AssembliesSignature =
        "IEnumerable<Assembly> ISpecularProvider.GetAssemblies(Action<IReflectionAssemblySelector> action, int lineNumber, string filePath, string argumentExpression)";

    private const string TypesSignature =
        "IEnumerable<Type> ISpecularProvider.GetTypes(Func<IReflectionTypeSelector, IEnumerable<Type>> selector, int lineNumber, string filePath, string argumentExpression)";

    private const string ScanSignature =
        "Microsoft.Extensions.DependencyInjection.IServiceCollection ISpecularProvider.Scan(Microsoft.Extensions.DependencyInjection.IServiceCollection services, Action<IServiceDescriptorAssemblySelector> selector, int lineNumber, string filePath, string argumentExpression)";

    // <privateAssembly>.GetType("...") as the generated code writes it: the receiver is a plain identifier, not a member access.
    private static readonly Regex PrivateTypeLookup = new("""(?<![\w.])(\w+)\.GetType\("([^"\\]*)"\)""", RegexOptions.Compiled);

    /// <summary>The <c>SpecularProvider</c> class, as text at the top level of the file.</summary>
    [SuppressMessage(
        "Security",
        "CA5351:Do Not Use Broken Cryptographic Algorithms",
        Justification = "MD5 is used only as a stable, non-cryptographic cache key for selector text; it is never used for security."
    )]
    public static string GetAssemblyProvider(
        ImmutableList<ResolvedSourceLocation> assemblyRequests,
        ImmutableList<ResolvedSourceLocation> reflectionRequests,
        ImmutableList<ResolvedSourceLocation> serviceDescriptorRequests,
        HashSet<IAssemblySymbol> privateAssemblies,
        Compilation compilation,
        bool isAot,
        out string cacheHash
    )
    {
        using var hasher = MD5.Create();

        static void addStringToHash(ICryptoTransform hasher, string textToHash)
        {
            var inputBuffer = Encoding.UTF8.GetBytes(textToHash);
            hasher.TransformBlock(inputBuffer, 0, inputBuffer.Length, inputBuffer, 0);
        }

        static void hashAll(ICryptoTransform hasher, IEnumerable<ResolvedSourceLocation> locations, JsonTypeInfo<ResolvedSourceLocation> jsonTypeInfo)
        {
            foreach (var location in locations)
            {
                addStringToHash(hasher, JsonSerializer.Serialize(location, jsonTypeInfo));
            }
        }

        var jsonTypeInfo = Configuration.JsonSourceGenerationContext.Default.ResolvedSourceLocation;
        hashAll(hasher, assemblyRequests, jsonTypeInfo);
        var assembliesBody = MethodBody(assemblyRequests, "return [];", returnsCollection: true);
        hashAll(hasher, reflectionRequests, jsonTypeInfo);
        var typesBody = MethodBody(reflectionRequests, "return [];", returnsCollection: true);
        hashAll(hasher, serviceDescriptorRequests, jsonTypeInfo);
        var scanBody = MethodBody(serviceDescriptorRequests, "return services;", returnsCollection: false);

        // Hashed in the set's own order, as the syntax-built provider did; written in name order.
        foreach (var assembly in privateAssemblies)
        {
            addStringToHash(hasher, assembly.MetadataName);
        }

        var orderedPrivateAssemblies = privateAssemblies.OrderBy(z => z.MetadataName).ToList();

        var assembliesAttributes = new List<string>();
        var typesAttributes = new List<string>();
        var scanAttributes = new List<string>();
        string? contextFieldAttribute = null;
        if (orderedPrivateAssemblies.Count != 0)
        {
            var privateAssemblyByVariable = orderedPrivateAssemblies
                                           .GroupBy(StatementGeneration.AssemblyVariableName, StringComparer.Ordinal)
                                           .ToDictionary(z => z.Key, z => z.First(), StringComparer.Ordinal);
            var dynamicDependencies = CollectDynamicDependencies(privateAssemblyByVariable, assembliesBody, typesBody, scanBody);

            // The provider resolves inaccessible types from string literals via reflection; the trimmer/ILC can't
            // statically prove that, so it reports IL2026/IL2072 (and #pragma is not honoured by the trimmer).
            // Suppress them with [UnconditionalSuppressMessage] (which IS honoured) on the methods that actually
            // reflect. This is safe because every resolved type and its public constructors are rooted via the
            // [DynamicDependency] attributes emitted below.
            var trimSuppressions = TrimSuppressionAttributeList();
            if (UsesPrivateReflection(assembliesBody, privateAssemblyByVariable)) assembliesAttributes.Add(trimSuppressions);
            if (UsesPrivateReflection(typesBody, privateAssemblyByVariable)) typesAttributes.Add(trimSuppressions);
            if (UsesPrivateReflection(scanBody, privateAssemblyByVariable)) scanAttributes.Add(trimSuppressions);

            // Native AOT resolves private assemblies via typeof(PublicType).Assembly (no AssemblyLoadContext), so there is no _context
            // field; the trimming roots are anchored on an always-present provider method instead.
            if (dynamicDependencies.Count > 0)
            {
                if (isAot) typesAttributes.Add(DynamicDependencyAttributeList(dynamicDependencies));
                else contextFieldAttribute = DynamicDependencyAttributeList(dynamicDependencies);
            }
        }

        _ = hasher.TransformFinalBlock([], 0, 0);
        cacheHash = Convert.ToBase64String(hasher.Hash);

        var writer = new SourceWriter(CodeText.SourceEndOfLine, assembliesBody.Length + typesBody.Length + scanBody.Length + 4096)
                    .Line(Helpers.CompilerGeneratedAttributes)
                    .Line("internal sealed class SpecularProvider : ISpecularProvider")
                    .Open()
                    .Line("public static ISpecularProvider Instance { get; } = new SpecularProvider();")
                    .BlankLine()
                    .Line("private SpecularProvider()")
                    .Line("{")
                    .Line("}")
                    .BlankLine();
        WriteMethod(writer, assembliesAttributes, AssembliesSignature, assembliesBody);
        _ = writer.BlankLine();
        WriteMethod(writer, typesAttributes, TypesSignature, typesBody);
        _ = writer.BlankLine();
        WriteMethod(writer, scanAttributes, ScanSignature, scanBody);

        if (orderedPrivateAssemblies.Count != 0)
        {
            var members = new List<(string Text, bool IsField)>();
            if (!isAot) members.Add(("private AssemblyLoadContext _context = AssemblyLoadContext.GetLoadContext(typeof(SpecularProvider).Assembly)!;", true));
            foreach (var assembly in orderedPrivateAssemblies)
            {
                members.AddRange(StatementGeneration.AssemblyDeclaration(compilation, isAot, assembly));
            }

            // Members are separated by a blank line, but a field sits right above whatever follows it.
            _ = writer.BlankLine();
            for (var i = 0; i < members.Count; i++)
            {
                if (i > 0 && !members[i - 1].IsField) _ = writer.BlankLine();
                if (i == 0 && contextFieldAttribute is { }) _ = writer.Line(contextFieldAttribute);
                _ = writer.Line(members[i].Text);
            }
        }

        return writer.Close().ToString();
    }

    private static void WriteMethod(SourceWriter writer, List<string> attributes, string signature, string body)
    {
        foreach (var attribute in attributes)
        {
            _ = writer.Line(attribute);
        }

        _ = writer.Line(signature).Raw(body);
    }

    // GetTypes and GetAssemblies return each call site's items as one collection expression (returnsCollection); Scan adds to the
    // services it was given. A call site's code arrives as one items.Add(...) block per scanned assembly, the form the build cache
    // stores, and is joined below before it becomes that single return. The body is written at the provider's member depth.
    private static string MethodBody(ImmutableList<ResolvedSourceLocation> locations, string returnStatement, bool returnsCollection)
    {
        var writer = new SourceWriter(CodeText.SourceEndOfLine) { Depth = 1 }.Open();
        if (locations.Count > 0)
        {
            var callSites = locations
                           .GroupBy(z => z.Location)
                           .Select(z => z.Aggregate(
                                       new ResolvedSourceLocation(z.First().Location, "", ImmutableHashSet<string>.Empty, [], [], [], z.First().ScannedAssemblyName),
                                       (location, sourceLocation) => location with { Expression = location.Expression + "\n" + sourceLocation.Expression }
                                   )
                            )
                           .ToList();
            SwitchGenerator.WriteSwitchStatement(writer, callSites, returnsCollection ? ToCollectionReturn : BlockStatements);
            _ = writer.BlankLine();
        }

        return writer.Line(returnStatement).Close().ToString();
    }

    private static StatementLines BlockStatements(string blocks) => new(SwitchGenerator.BlockStatements(blocks), false);

    // items.Add(a); items.Add(b); ... becomes return [a, b, ...];. Comments in the blocks (a type the generator could not name) are
    // kept above the return.
    private static StatementLines ToCollectionReturn(string blocks)
    {
        const string addPrefix = "items.Add(";
        const string addSuffix = ");";
        var elements = new List<string>();
        var lines = new List<string>();
        foreach (var statement in SwitchGenerator.BlockStatements(blocks))
        {
            if (statement.StartsWith(addPrefix, StringComparison.Ordinal) && statement.EndsWith(addSuffix, StringComparison.Ordinal))
            {
                elements.Add(statement.Substring(addPrefix.Length, statement.Length - addPrefix.Length - addSuffix.Length));
                continue;
            }

            if (!statement.StartsWith(";", StringComparison.Ordinal))
            {
                throw new InvalidOperationException($"Unexpected statement in a collection call site: {statement}");
            }

            if (statement[1..].Trim() is { Length: > 0 } comment) lines.Add(comment);
        }

        if (elements.Count == 0)
        {
            lines.Add("return [];");
            return new(lines, true);
        }

        lines.Add("return");
        lines.Add("[");
        for (var i = 0; i < elements.Count; i++)
        {
            lines.Add(i < elements.Count - 1 ? $"    {elements[i]}," : $"    {elements[i]}");
        }

        lines.Add("];");
        return new(lines, true);
    }

    // Collects (assembly, type) pairs for every `<privateAssembly>.GetType("...")` call the generated
    // provider emits, so the trimmer/Native AOT analyzer keeps those dynamically-resolved types. Only
    // pairs whose type actually resolves in the target assembly are kept, so speculative GetType probes
    // don't produce unresolved-dependency warnings (IL2035/IL2036).
    private static List<(string Assembly, string Type)> CollectDynamicDependencies(
        Dictionary<string, IAssemblySymbol> privateAssemblyByVariable,
        params string[] methodBodies
    )
    {
        var found = new HashSet<(string Assembly, string Type)>();
        foreach (var body in methodBodies)
        {
            foreach (Match match in PrivateTypeLookup.Matches(body))
            {
                if (!privateAssemblyByVariable.TryGetValue(match.Groups[1].Value, out var assemblySymbol)) continue;

                var typeName = match.Groups[2].Value;
                if (assemblySymbol.GetTypeByMetadataName(typeName) is null) continue;

                _ = found.Add((assemblySymbol.Identity.Name, typeName));
            }
        }

        return found
              .OrderBy(z => z.Assembly, StringComparer.Ordinal)
              .ThenBy(z => z.Type, StringComparer.Ordinal)
              .ToList();
    }

    private static bool UsesPrivateReflection(string methodBody, Dictionary<string, IAssemblySymbol> privateAssemblyByVariable)
    {
        foreach (Match match in PrivateTypeLookup.Matches(methodBody))
        {
            if (privateAssemblyByVariable.ContainsKey(match.Groups[1].Value)) return true;
        }

        return false;
    }

    private static string TrimSuppressionAttributeList() =>
        $"[{TrimSuppression("IL2026:RequiresUnreferencedCode")}, {TrimSuppression("IL2072:DynamicallyAccessedMembers")}]";

    private static string TrimSuppression(string checkId) =>
        $"global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage(\"Trimming\", {CodeText.Literal(checkId)}, Justification = {CodeText.Literal(TrimJustification)})";

    private static string DynamicDependencyAttributeList(IReadOnlyList<(string Assembly, string Type)> dependencies) =>
        "["
      + string.Join(
            ", ",
            dependencies.Select(dependency =>
                                    "global::System.Diagnostics.CodeAnalysis.DynamicDependency(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "
                                  + $"{CodeText.Literal(dependency.Type)}, {CodeText.Literal(dependency.Assembly)})"
            )
        )
      + "]";
}
