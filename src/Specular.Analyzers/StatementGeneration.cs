using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Specular.Scanning;

namespace Specular.Analyzers;

internal static class StatementGeneration
{
    public static CodeExpression? GenerateServiceFactory(
        Compilation compilation,
        INamedTypeSymbol serviceType,
        INamedTypeSymbol implementationType,
        string lifetime,
        string? key = null
    )
    {
        if (GetTypeOfExpression(compilation, serviceType, true) is not { } serviceTypeExpression) return null;

        var isImplementationAccessible = compilation.IsSymbolAccessibleWithin(implementationType, compilation.Assembly);
        if (isImplementationAccessible)
        {
            var implementationName = Helpers.GetTypeOfName(implementationType);
            var resolve = key is null
                ? $"a.GetRequiredService<{implementationName}>()"
                : $"a.GetRequiredKeyedService<{implementationName}>({key})";
            return GenerateServiceType(compilation, serviceType, serviceTypeExpression, implementationType, KeyAwareLambda(key, resolve), lifetime, key);
        }

        if (GetTypeOfExpression(compilation, implementationType, serviceType, true) is not { } typeofExpression) return null;

        var isServiceAccessible = compilation.IsSymbolAccessibleWithin(serviceType, compilation.Assembly);
        var baseInvocation = key is null
            ? $"a.GetRequiredService({typeofExpression.Text})"
            : $"a.GetRequiredKeyedService({typeofExpression.Text}, {key})";
        var body = isServiceAccessible ? $"({Helpers.GetTypeOfName(serviceType)}){baseInvocation}" : baseInvocation;
        return GenerateServiceType(compilation, serviceType, serviceTypeExpression, implementationType, KeyAwareLambda(key, body), lifetime, key);
    }

    /// <summary>
    ///     Keyed factory delegates target <c>Func&lt;IServiceProvider, object?, TService&gt;</c>, which requires a
    ///     second, unused parameter for the service key (the key itself is embedded as a literal in the body).
    /// </summary>
    private static CodeExpression KeyAwareLambda(string? key, string body) => new(key is null ? $"a => {body}" : $"(a, _) => {body}", IsLambda: true);

    public static CodeExpression? GenerateServiceType(
        Compilation compilation,
        INamedTypeSymbol serviceType,
        INamedTypeSymbol implementationType,
        string lifetime,
        string? key = null
    )
    {
        var serviceTypeExpression = GetTypeOfExpression(compilation, serviceType, true);
        var implementationTypeExpression = GetTypeOfExpression(compilation, implementationType, serviceType, true);
        return serviceTypeExpression is not { } service || implementationTypeExpression is not { } implementation
            ? null
            : GenerateServiceType(compilation, serviceType, service, implementationType, implementation, lifetime, key);
    }

    public static CodeExpression GenerateServiceType(
        Compilation compilation,
        INamedTypeSymbol serviceType,
        CodeExpression serviceTypeExpression,
        INamedTypeSymbol implementationType,
        CodeExpression implementationTypeExpression,
        string lifetime,
        string? key = null
    )
    {
        var methodName = key is null ? lifetime : $"Keyed{lifetime}";
        var isServiceTypeAccessible = compilation.IsSymbolAccessibleWithin(serviceType, compilation.Assembly);
        return isServiceTypeAccessible
         && serviceTypeExpression.TypeOfType is { } serviceTypeName
         && implementationTypeExpression.TypeOfType is { } implementationTypeName
         && !IsOpenGenericType(serviceType)
         && !IsOpenGenericType(implementationType)
         && compilation.IsSymbolAccessibleWithin(implementationType, compilation.Assembly)
            ? new($"ServiceDescriptor.{methodName}<{serviceTypeName}, {implementationTypeName}>({key})")
            : isServiceTypeAccessible
         && serviceTypeExpression.TypeOfType is { } lambdaServiceTypeName
         && implementationTypeExpression.IsLambda
         && !IsOpenGenericType(serviceType)
            ? new(
                key is null
                    ? $"ServiceDescriptor.{methodName}<{lambdaServiceTypeName}>({implementationTypeExpression.Text})"
                    : $"ServiceDescriptor.{methodName}<{lambdaServiceTypeName}>({key}, {implementationTypeExpression.Text})"
            )
            : new(
            key is null
                ? $"ServiceDescriptor.{methodName}({serviceTypeExpression.Text}, {implementationTypeExpression.Text})"
                : $"ServiceDescriptor.{methodName}({serviceTypeExpression.Text}, {key}, {implementationTypeExpression.Text})"
        );
    }

    public static bool IsOpenGenericType(this INamedTypeSymbol type) =>
        type.IsGenericType && ( type.IsUnboundGenericType || type.TypeArguments.All(z => z.TypeKind == TypeKind.TypeParameter) );

    /// <summary>The members that give the provider a handle on a private assembly, one line each, and whether each is a field.</summary>
    public static IEnumerable<(string Text, bool IsField)> AssemblyDeclaration(Compilation compilation, bool isAot, IAssemblySymbol symbol)
    {
        var name = AssemblyVariableName(symbol);

        if (isAot)
        {
            // Native AOT cannot load assemblies dynamically, so obtain the assembly handle statically from a
            // public type in it. Zero-public-type assemblies are skipped upstream, so this is never null here.
            // ReSharper disable once NullableWarningSuppressionIsUsed
            var assemblyExpression = GetAssemblyExpression(compilation, symbol)!;
            yield return ($"private Assembly {name} => {assemblyExpression.Value.Text};", false);
            yield break;
        }

        yield return ($"private Assembly _{name};", true);
        yield return ($"private Assembly {name} => _{name} ??= _context.LoadFromAssemblyName(new AssemblyName({CodeText.Literal(symbol.Identity.GetDisplayName(true))}));", false);
    }

    public static CodeExpression? GetAssemblyExpression(Compilation compilation, IAssemblySymbol assembly)
    {
        return FindPublicType(assembly) is not { } keyholdType
            ? null
            : GetTypeOfExpression(compilation, keyholdType, false) is { } typeExpression ? new CodeExpression($"{typeExpression.Text}.Assembly") : null;
    }

    public static CodeExpression? GetTypeOfExpression(Compilation compilation, INamedTypeSymbol type, bool allowPrivateTypeLookup)
    {
        if (type.IsGenericType && type.IsOpenGenericType())
        {
            return getPrivateType(compilation, type, allowPrivateTypeLookup);
        }

        if (!compilation.IsSymbolAccessibleWithin(type, compilation.Assembly) && type.IsGenericType && !type.IsOpenGenericType())
        {
            if (getPrivateType(compilation, type.ConstructUnboundGenericType(), allowPrivateTypeLookup) is not { } genericPrivateType) return null;

            var arguments = new StringBuilder();
            foreach (var argument in type.TypeArguments)
            {
                if (arguments.Length > 0) _ = arguments.Append(", ");
                // ReSharper disable once NullableWarningSuppressionIsUsed
                _ = arguments.Append(getPrivateType(compilation, ( argument as INamedTypeSymbol )!, allowPrivateTypeLookup)!.Value.Text);
            }

            return new($"{genericPrivateType.Text}.MakeGenericType({arguments})!");
        }

        return getPrivateType(compilation, type, allowPrivateTypeLookup);

        static CodeExpression? getPrivateType(Compilation compilation, INamedTypeSymbol type, bool allowPrivateTypeLookup) =>
            compilation.IsSymbolAccessibleWithin(type, compilation.Assembly)
                ? CodeText.TypeOf(Helpers.GetTypeOfName(type))
                : allowPrivateTypeLookup
                    ? new CodeExpression($"{GetPrivateAssembly(type.ContainingAssembly)}.GetType({CodeText.Literal(Helpers.GetFullMetadataName(type))})!")
                    : null;
    }

    public static string GetPrivateAssembly(IAssemblySymbol type) => AssemblyVariableName(type);

    /// <summary>Detects whether the current compilation targets Native AOT (or is marked AOT-compatible).</summary>
    public static bool IsAotCompilation(AnalyzerConfigOptionsProvider options) =>
        IsTrue(options, "build_property.PublishAot") || IsTrue(options, "build_property.EnableAotAnalyzer");

    private static bool IsTrue(AnalyzerConfigOptionsProvider options, string key) =>
        options.GlobalOptions.TryGetValue(key, out var value) && string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    ///     Under Native AOT an inaccessible type can only be resolved via a public type in its assembly. Returns the
    ///     first type in the closure (the type plus any generic arguments) that has no such anchor, or null if reachable.
    /// </summary>
    public static INamedTypeSymbol? GetUnreachableType(Compilation compilation, INamedTypeSymbol type)
    {
        foreach (var candidate in EnumerateTypeClosure(type))
        {
            if (!compilation.IsSymbolAccessibleWithin(candidate, compilation.Assembly)
             && FindPublicType(candidate.ContainingAssembly) is null)
            {
                return candidate;
            }
        }

        return null;

        static IEnumerable<INamedTypeSymbol> EnumerateTypeClosure(INamedTypeSymbol named)
        {
            yield return named;
            foreach (var argument in named.TypeArguments.OfType<INamedTypeSymbol>())
            {
                foreach (var inner in EnumerateTypeClosure(argument)) yield return inner;
            }
        }
    }

    public static string AssemblyVariableName(IAssemblySymbol symbol) => SpecialCharacterRemover.Replace(symbol.MetadataName, "");

    public static CodeExpression? GetTypeOfExpression(
        Compilation compilation,
        INamedTypeSymbol type,
        INamedTypeSymbol relatedType,
        bool allowPrivateTypeLookup
    )
    {
        if (!type.IsUnboundGenericType)
        {
            return GetTypeOfExpression(compilation, type, allowPrivateTypeLookup);
        }

        if (relatedType.IsGenericType && relatedType.Arity == type.Arity)
        {
            type = type.Construct([.. relatedType.TypeArguments]);
        }
        else
        {
            var baseType = Helpers
                          .GetBaseTypes(compilation, type)
                          .FirstOrDefault(z => z.IsGenericType && compilation.HasImplicitConversion(z, relatedType));
            // ReSharper disable once AccessToModifiedClosure
            baseType ??= type.AllInterfaces.FirstOrDefault(z => z.IsGenericType && compilation.HasImplicitConversion(z, relatedType));

            if (baseType is { })
            {
                type = type.Construct([.. baseType.TypeArguments]);
            }
        }

        return GetTypeOfExpression(compilation, type, allowPrivateTypeLookup);
    }

    /// <summary>
    ///     The first public type in <paramref name="assembly" />, in declaration order, that can stand for it in generated code (an
    ///     obsolete or experimental one would produce warnings or errors). Core assemblies never get one.
    /// </summary>
    private static INamedTypeSymbol? FindPublicType(IAssemblySymbol assembly)
    {
        if (AssemblyMatcher.IsCoreAssembly(assembly.Name)) return null;

        INamedTypeSymbol? found = null;
        _ = TypeWalker.Walk(
            assembly.GlobalNamespace,
            type =>
            {
                if (type.DeclaredAccessibility != Accessibility.Public
                 || type.GetAttributes().Any(z => z is { AttributeClass.MetadataName: "ObsoleteAttribute" or "ExperimentalAttribute" }))
                {
                    return true;
                }

                found = type;
                return false;
            },
            CancellationToken.None
        );
        return found;
    }

    private static readonly Regex SpecialCharacterRemover = new("[^\\w\\d]", RegexOptions.Compiled);
}
