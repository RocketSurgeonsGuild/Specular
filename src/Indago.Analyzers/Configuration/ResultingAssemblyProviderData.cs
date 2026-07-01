using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.Configuration;

public class ResultingAssemblyProviderData
{
    public void AddExpressionData(IAssemblySymbol assembly, CompiledAssemblyProviderData data)
    {
        if (assembly is null) throw new ArgumentNullException(nameof(assembly));

        if (_assemblyData.TryGetValue(assembly.MetadataName, out _)) return;

        _assemblyData.Add(assembly.MetadataName, data);
    }

    public bool NoExpressions(IAssemblySymbol assembly)
    {
        return assembly is null
            ? throw new ArgumentNullException(nameof(assembly))
            : _expressionlessAssemblies.Add(assembly.MetadataName);
    }

    public void AddSourceLocation(IAssemblySymbol assembly, ResolvedSourceLocation resolvedSource)
    {
        if (assembly is null) throw new ArgumentNullException(nameof(assembly));
        if (resolvedSource is null) throw new ArgumentNullException(nameof(resolvedSource));

        var cacheKey = GetCacheFileHash(resolvedSource.Location);
        if (!_sourceLocations.TryGetValue(cacheKey, out _)) _sourceLocations.Add(cacheKey, new(resolvedSource.Location));

        _sourceLocations[cacheKey].AddSource(assembly.MetadataName, resolvedSource);
    }

    public GeneratedAssemblyProviderData ToGeneratedAssemblyProviderData() => new(
        _assemblyData.ToImmutableDictionary(),
        _expressionlessAssemblies.ToImmutableHashSet(),
        _sourceLocations.ToImmutableDictionary(
            x => x.Key,
            x => new GeneratedLocationAssemblyResolvedSourceCollection(x.Value.SourceLocation, x.Value.ResolvedSources.ToImmutableDictionary())
        )
    );

    [SuppressMessage("Security", "CA5351:Do Not Use Broken Cryptographic Algorithms", Justification = "MD5 is used only as a non-cryptographic, stable cache key for selector text; not used for security.")]
    internal static string GetCacheFileHash(SourceLocation location)
    {
        using var hasher = MD5.Create();
        addStringToHash(hasher, location.FileName);
        addStringToHash(hasher, location.ExpressionHash);
        addStringToHash(hasher, location.LineNumber.ToString(System.Globalization.CultureInfo.InvariantCulture));
        _ = hasher.TransformFinalBlock([], 0, 0);
        return Convert.ToBase64String(hasher.Hash);

        static void addStringToHash(ICryptoTransform cryptoTransform, string textToHash)
        {
            var inputBuffer = Encoding.UTF8.GetBytes(textToHash);
            _ = cryptoTransform.TransformBlock(inputBuffer, 0, inputBuffer.Length, inputBuffer, 0);
        }
    }

    private readonly Dictionary<string, CompiledAssemblyProviderData> _assemblyData = [];
    private readonly HashSet<string> _expressionlessAssemblies = [with(StringComparer.OrdinalIgnoreCase)];
    private readonly Dictionary<string, ResultingLocationAssemblyResolvedSourceCollection> _sourceLocations = [];
}
