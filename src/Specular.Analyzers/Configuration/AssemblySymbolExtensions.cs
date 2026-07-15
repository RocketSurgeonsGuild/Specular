using Microsoft.CodeAnalysis;

namespace Specular.Analyzers.Configuration;

internal static class AssemblySymbolExtensions
{
    public static string? GetCachedVersion(this IAssemblySymbol assembly) => GetSpecularProviderHash(assembly) is { Length: > 0 } hash
        ? hash
        : GetInformationalVersion(assembly);

    public static bool MatchesCachedVersion(this IAssemblySymbol assembly, string? cacheVersion) =>
        assembly.GetCachedVersion() is not { Length: > 0 } version || version == cacheVersion;

    private static string? GetSpecularProviderHash(IAssemblySymbol assembly) =>
        assembly
           .GetAttributes()
           .FirstOrDefault(x => x.AttributeClass?.ToDisplayString() == "Specular.Abstractions.SpecularHashAttribute")
          ?.ConstructorArguments.LastOrDefault()
           .Value?.ToString();

    private static string? GetInformationalVersion(IAssemblySymbol assembly) =>
        assembly
           .GetAttributes()
           .FirstOrDefault(x => x.AttributeClass?.ToDisplayString() == "System.Reflection.AssemblyInformationalVersionAttribute")
          ?.ConstructorArguments.FirstOrDefault()
           .Value?.ToString();
}
