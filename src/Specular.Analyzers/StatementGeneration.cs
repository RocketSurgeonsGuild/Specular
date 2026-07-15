using System.Text.RegularExpressions;
using Specular.Analyzers.AssemblyProviders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Specular.Analyzers;

internal static class StatementGeneration
{
    public static InvocationExpressionSyntax GenerateServiceFactory(
        Compilation compilation,
        INamedTypeSymbol serviceType,
        INamedTypeSymbol implementationType,
        string lifetime
    )
    {
        var serviceTypeExpression = GetTypeOfExpression(compilation, serviceType);
        var isImplementationAccessible = compilation.IsSymbolAccessibleWithin(implementationType, compilation.Assembly);

        if (isImplementationAccessible)
        {
            var implementationTypeExpression = SimpleLambdaExpression(Parameter(Identifier("a")))
               .WithExpressionBody(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("a"),
                            GenericName("GetRequiredService")
                               .WithTypeArgumentList(
                                    TypeArgumentList(
                                        SingletonSeparatedList<TypeSyntax>(IdentifierName(Helpers.GetTypeOfName(implementationType)))
                                    )
                                )
                        )
                    )
                );

            return GenerateServiceType(
                compilation,
                serviceType,
                serviceTypeExpression,
                implementationType,
                implementationTypeExpression,
                lifetime
            );
        }
        else
        {
            var isServiceAccessible = compilation.IsSymbolAccessibleWithin(serviceType, compilation.Assembly);
            var baseInvocation = InvocationExpression(
                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("a"), IdentifierName("GetRequiredService"))
                )
               .WithArgumentList(
                    ArgumentList(
                        SingletonSeparatedList(Argument(GetTypeOfExpression(compilation, implementationType, serviceType)))
                    )
                );
            var implementationTypeExpression = SimpleLambdaExpression(Parameter(Identifier("a")))
               .WithExpressionBody(
                    isServiceAccessible
                        ? CastExpression(IdentifierName(Helpers.GetTypeOfName(serviceType)), baseInvocation)
                        : baseInvocation
                );
            return GenerateServiceType(
                compilation,
                serviceType,
                serviceTypeExpression,
                implementationType,
                implementationTypeExpression,
                lifetime
            );
        }
    }

    public static InvocationExpressionSyntax GenerateServiceType(
        Compilation compilation,
        INamedTypeSymbol serviceType,
        INamedTypeSymbol implementationType,
        string lifetime
    )
    {
        var serviceTypeExpression = GetTypeOfExpression(compilation, serviceType);
        var implementationTypeExpression = GetTypeOfExpression(compilation, implementationType, serviceType);
        return GenerateServiceType(
            compilation,
            serviceType,
            serviceTypeExpression,
            implementationType,
            implementationTypeExpression,
            lifetime
        );
    }

    public static InvocationExpressionSyntax GenerateServiceType(
        Compilation compilation,
        INamedTypeSymbol serviceType,
        ExpressionSyntax serviceTypeExpression,
        INamedTypeSymbol implementationType,
        ExpressionSyntax implementationTypeExpression,
        string lifetime
    )
    {
        var isServiceTypeAccessible = compilation.IsSymbolAccessibleWithin(serviceType, compilation.Assembly);
        return (isServiceTypeAccessible, serviceTypeExpression, implementationTypeExpression) switch
        {
            (true, TypeOfExpressionSyntax { Type: { } serviceTypeSyntax }, TypeOfExpressionSyntax { Type: { } implementationTypeSyntax })
                when !IsOpenGenericType(serviceType)
             && !IsOpenGenericType(implementationType)
             && compilation.IsSymbolAccessibleWithin(implementationType, compilation.Assembly)
                => InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("ServiceDescriptor"),
                        GenericName(lifetime)
                           .WithTypeArgumentList(TypeArgumentList(SeparatedList([serviceTypeSyntax, implementationTypeSyntax])))
                    )
                ),
            (true, TypeOfExpressionSyntax { Type: { } serviceTypeSyntax }, SimpleLambdaExpressionSyntax { ExpressionBody: { } })
                when !IsOpenGenericType(serviceType) =>
                InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("ServiceDescriptor"),
                            GenericName(lifetime).WithTypeArgumentList(TypeArgumentList(SeparatedList([serviceTypeSyntax])))
                        )
                    ).WithArgumentList(ArgumentList(SeparatedList([Argument(implementationTypeExpression)]))),
            _ => InvocationExpression(
                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("ServiceDescriptor"), IdentifierName(lifetime))
                )
               .WithArgumentList(ArgumentList(SeparatedList([Argument(serviceTypeExpression), Argument(implementationTypeExpression!)]))),
        };
    }

    public static bool IsOpenGenericType(this INamedTypeSymbol type) =>
        type.IsGenericType && ( type.IsUnboundGenericType || type.TypeArguments.All(z => z.TypeKind == TypeKind.TypeParameter) );

    public static IEnumerable<MemberDeclarationSyntax> AssemblyDeclaration(Compilation compilation, bool isAot, IAssemblySymbol symbol)
    {
        var name = AssemblyVariableName(symbol);

        if (isAot)
        {
            // Native AOT cannot load assemblies dynamically, so obtain the assembly handle statically from a
            // public type in it. Zero-public-type assemblies are skipped upstream, so this is never null here.
            // ReSharper disable once NullableWarningSuppressionIsUsed
            var assemblyExpression = GetAssemblyExpression(compilation, symbol)!;
            yield return PropertyDeclaration(IdentifierName("Assembly"), Identifier(name))
                        .WithModifiers(TokenList(Token(SyntaxKind.PrivateKeyword)))
                        .WithExpressionBody(ArrowExpressionClause(assemblyExpression))
                        .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
            yield break;
        }

        var assemblyName = LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(symbol.Identity.GetDisplayName(true)));

        yield return FieldDeclaration(
                VariableDeclaration(IdentifierName("Assembly"))
                   .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier($"_{name}"))))
            )
           .WithModifiers(TokenList(Token(SyntaxKind.PrivateKeyword)));
        yield return PropertyDeclaration(IdentifierName("Assembly"), Identifier(name))
                    .WithModifiers(TokenList(Token(SyntaxKind.PrivateKeyword)))
                    .WithExpressionBody(
                         ArrowExpressionClause(
                             AssignmentExpression(
                                 SyntaxKind.CoalesceAssignmentExpression,
                                 IdentifierName(Identifier($"_{name}")),
                                 InvocationExpression(
                                         MemberAccessExpression(
                                             SyntaxKind.SimpleMemberAccessExpression,
                                             IdentifierName("_context"),
                                             IdentifierName("LoadFromAssemblyName")
                                         )
                                     )
                                    .WithArgumentList(
                                         ArgumentList(
                                             SingletonSeparatedList(
                                                 Argument(
                                                     ObjectCreationExpression(IdentifierName("AssemblyName"))
                                                        .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(assemblyName))))
                                                 )
                                             )
                                         )
                                     )
                             )
                         )
                     )
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }


    public static ExpressionSyntax? GetAssemblyExpression(Compilation compilation, IAssemblySymbol assembly) =>
        FindTypeInAssembly.FindType(compilation, assembly) is { } keyholdType
            ? MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                GetTypeOfExpression(compilation, keyholdType),
                IdentifierName("Assembly")
            )
            : null;

    public static ExpressionSyntax GetTypeOfExpression(Compilation compilation, INamedTypeSymbol type)
    {
        return type.IsGenericType && type.IsOpenGenericType()
            ? getPrivateType(compilation, type)
            : !compilation.IsSymbolAccessibleWithin(type, compilation.Assembly) && type.IsGenericType && !type.IsOpenGenericType()
                ? PostfixUnaryExpression(
                    SyntaxKind.SuppressNullableWarningExpression,
                    InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                getPrivateType(compilation, type.ConstructUnboundGenericType()),
                                IdentifierName("MakeGenericType")
                            )
                        )
                       .WithArgumentList(
                            // ReSharper disable once NullableWarningSuppressionIsUsed
                            ArgumentList(SeparatedList(type.TypeArguments.Select(t => Argument(getPrivateType(compilation, ( t as INamedTypeSymbol )!)))))
                        )
                )
                : getPrivateType(compilation, type);

        static ExpressionSyntax getPrivateType(Compilation compilation, INamedTypeSymbol type)
        {
            return compilation.IsSymbolAccessibleWithin(type, compilation.Assembly)
                ? TypeOfExpression(ParseTypeName(Helpers.GetTypeOfName(type)))
                : PostfixUnaryExpression(
                    SyntaxKind.SuppressNullableWarningExpression,
                    InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                GetPrivateAssembly(type.ContainingAssembly),
                                IdentifierName("GetType")
                            )
                        )
                       .WithArgumentList(
                            ArgumentList(
                                SingletonSeparatedList(
                                    Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(Helpers.GetFullMetadataName(type))))
                                )
                            )
                        )
                );
        }
    }


    public static ExpressionSyntax GetPrivateAssembly(IAssemblySymbol type) => IdentifierName(AssemblyVariableName(type));

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
             && FindTypeInAssembly.FindType(compilation, candidate.ContainingAssembly) is null)
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

    public static ExpressionSyntax GetTypeOfExpression(
        Compilation compilation,
        INamedTypeSymbol type,
        INamedTypeSymbol relatedType
    )
    {
        if (!type.IsUnboundGenericType)
        {
            return GetTypeOfExpression(compilation, type);
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

        return GetTypeOfExpression(compilation, type);
    }

    private static readonly Regex SpecialCharacterRemover = new("[^\\w\\d]", RegexOptions.Compiled);
}
