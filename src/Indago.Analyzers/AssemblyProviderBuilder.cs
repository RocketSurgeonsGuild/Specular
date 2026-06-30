using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Indago.Analyzers;

internal static class AssemblyProviderBuilder
{
    public static TypeDeclarationSyntax GetAssemblyProvider(
        ImmutableList<ResolvedSourceLocation> assemblyRequests,
        ImmutableList<ResolvedSourceLocation> reflectionRequests,
        ImmutableList<ResolvedSourceLocation> serviceDescriptorRequests,
        HashSet<IAssemblySymbol> privateAssemblies,
        Configuration.AssemblyProviderConfiguration configuration,
        out string cacheHash
    )
    {
        var compilation = configuration.Compilation;
        var isAot = configuration.IsAot;
        using var hasher = MD5.Create();

        static void addStringToHash(ICryptoTransform hasher, string textToHash)
        {
            var inputBuffer = Encoding.UTF8.GetBytes(textToHash);
            hasher.TransformBlock(inputBuffer, 0, inputBuffer.Length, inputBuffer, 0);
        }

        static IEnumerable<T> dohash<T>(ICryptoTransform hasher, IEnumerable<T> enumerable, JsonTypeInfo<T> jsonTypeInfo)
        {
            foreach (var item in enumerable)
            {
                addStringToHash(hasher, JsonSerializer.Serialize(item, jsonTypeInfo));
                yield return item;
            }
        }

        var resolvedAssemblyDetails = assemblyRequests is { Count: > 0 }
            ? GenerateMethodBody(AssembliesMethod, dohash(hasher, assemblyRequests, Configuration.JsonSourceGenerationContext.Default.ResolvedSourceLocation))
            : AssembliesMethod;

        var resolvedReflectionDetails = reflectionRequests is { Count: > 0 }
            ? GenerateMethodBody(TypesMethod, dohash(hasher, reflectionRequests, Configuration.JsonSourceGenerationContext.Default.ResolvedSourceLocation))
            : TypesMethod;

        var resolvedServiceDescriptorDetails = serviceDescriptorRequests is { Count: > 0 }
            ? GenerateMethodBody(ScanMethod, dohash(hasher, serviceDescriptorRequests, Configuration.JsonSourceGenerationContext.Default.ResolvedSourceLocation))
            : ScanMethod;

        var privateMembers = privateAssemblies
                            .OrderBy(
                                 z =>
                                 {
                                     addStringToHash(hasher, z.MetadataName);
                                     return z.MetadataName;
                                 }
                             )
                            .SelectMany(z => StatementGeneration.AssemblyDeclaration(compilation, isAot, z))
                            .ToList();
        if (privateAssemblies.Any())
        {
            var privateAssemblyByVariable = privateAssemblies
                                           .GroupBy(StatementGeneration.AssemblyVariableName, StringComparer.Ordinal)
                                           .ToDictionary(z => z.Key, z => z.First(), StringComparer.Ordinal);
            var dynamicDependencies = CollectDynamicDependencies(
                privateAssemblyByVariable,
                resolvedAssemblyDetails,
                resolvedReflectionDetails,
                resolvedServiceDescriptorDetails
            );

            if (isAot)
            {
                // Native AOT resolves private assemblies via typeof(PublicType).Assembly (no AssemblyLoadContext),
                // so there is no _context field; anchor the trimming roots on an always-present provider method.
                if (dynamicDependencies.Count > 0)
                    resolvedReflectionDetails = resolvedReflectionDetails.WithAttributeLists(SingletonList(BuildDynamicDependencyAttributeList(dynamicDependencies)));
            }
            else
            {
                var contextField = FieldDeclaration(
                        VariableDeclaration(IdentifierName("AssemblyLoadContext"))
                           .WithVariables(
                                SingletonSeparatedList(
                                    VariableDeclarator(Identifier("_context"))
                                       .WithInitializer(
                                            EqualsValueClause(
                                                PostfixUnaryExpression(
                                                    SyntaxKind.SuppressNullableWarningExpression,
                                                    InvocationExpression(
                                                            MemberAccessExpression(
                                                                SyntaxKind.SimpleMemberAccessExpression,
                                                                IdentifierName("AssemblyLoadContext"),
                                                                IdentifierName("GetLoadContext")
                                                            )
                                                        )
                                                       .WithArgumentList(
                                                            ArgumentList(
                                                                SingletonSeparatedList(
                                                                    Argument(
                                                                        MemberAccessExpression(
                                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                                            TypeOfExpression(IdentifierName("IndagoProvider")),
                                                                            IdentifierName("Assembly")
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        )
                                                )
                                            )
                                        )
                                )
                            )
                    )
                   .WithModifiers(TokenList(Token(SyntaxKind.PrivateKeyword)));

                if (dynamicDependencies.Count > 0)
                    contextField = contextField.WithAttributeLists(SingletonList(BuildDynamicDependencyAttributeList(dynamicDependencies)));

                privateMembers.Insert(0, contextField);
            }
        }

        _ = hasher.TransformFinalBlock([], 0, 0);
        cacheHash = Convert.ToBase64String(hasher.Hash);
        return ClassDeclaration("IndagoProvider")
              .AddAttributeLists(Helpers.CompilerGeneratedAttributes)
              .WithModifiers(TokenList(Token(SyntaxKind.FileKeyword)))
              .WithBaseList(BaseList(SingletonSeparatedList<BaseTypeSyntax>(SimpleBaseType(IdentifierName("IIndagoProvider")))))
              .AddMembers(resolvedAssemblyDetails, resolvedReflectionDetails, resolvedServiceDescriptorDetails)
              .AddMembers(privateMembers.ToArray());
    }

    // Collects (assembly, type) pairs for every `<privateAssembly>.GetType("...")` call the generated
    // provider emits, so the trimmer/Native AOT analyzer keeps those dynamically-resolved types. Only
    // pairs whose type actually resolves in the target assembly are kept, so speculative GetType probes
    // don't produce unresolved-dependency warnings (IL2035/IL2036).
    private static IReadOnlyList<(string Assembly, string Type)> CollectDynamicDependencies(
        IReadOnlyDictionary<string, IAssemblySymbol> privateAssemblyByVariable,
        params MethodDeclarationSyntax[] methods
    )
    {
        var found = new HashSet<(string Assembly, string Type)>();
        foreach (var method in methods)
        {
            foreach (var invocation in method.DescendantNodes().OfType<InvocationExpressionSyntax>())
            {
                if (invocation.Expression is not MemberAccessExpressionSyntax { Name.Identifier.ValueText: "GetType", Expression: IdentifierNameSyntax receiver }) continue;
                if (!privateAssemblyByVariable.TryGetValue(receiver.Identifier.ValueText, out var assemblySymbol)) continue;
                if (invocation.ArgumentList.Arguments.Count != 1) continue;
                if (invocation.ArgumentList.Arguments[0].Expression is not LiteralExpressionSyntax { Token.Value: string typeName }) continue;
                if (assemblySymbol.GetTypeByMetadataName(typeName) is null) continue;

                _ = found.Add((assemblySymbol.Identity.Name, typeName));
            }
        }

        return found
              .OrderBy(z => z.Assembly, StringComparer.Ordinal)
              .ThenBy(z => z.Type, StringComparer.Ordinal)
              .ToList();
    }

    private static AttributeListSyntax BuildDynamicDependencyAttributeList(IReadOnlyList<(string Assembly, string Type)> dependencies) =>
        AttributeList(
            SeparatedList(
                dependencies.Select(
                    dependency => Attribute(ParseName("global::System.Diagnostics.CodeAnalysis.DynamicDependency"))
                       .WithArgumentList(
                            AttributeArgumentList(
                                SeparatedList(
                                    new[]
                                    {
                                        AttributeArgument(
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                ParseName("global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes"),
                                                IdentifierName("All")
                                            )
                                        ),
                                        AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(dependency.Type))),
                                        AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(dependency.Assembly))),
                                    }
                                )
                            )
                        )
                )
            )
        );

    private static MethodDeclarationSyntax GenerateMethodBody(MethodDeclarationSyntax baseMethod, IEnumerable<ResolvedSourceLocation> locations)
    {
        var item = baseMethod.Body?.Statements.ToArray() ?? Array.Empty<StatementSyntax>();
        var returnStatement = item.OfType<ReturnStatementSyntax>().Take(1).Cast<StatementSyntax>();

        return baseMethod
           .WithBody(
                Block(
                    ImmutableList.CreateRange(item.Except(returnStatement))
                    .Add(
                        SwitchGenerator.GenerateSwitchStatement(
                                locations
                                     .GroupBy(z => z.Location)
                                     .Select(
                                          z => z.Aggregate(
                                              new ResolvedSourceLocation(z.First().Location, "", ImmutableHashSet<string>.Empty, null),
                                              (location, sourceLocation) => new(
                                                  location.Location,
                                                  location.Expression + "\n" + sourceLocation.Expression,
                                                  location.PrivateAssemblies.Concat(sourceLocation.PrivateAssemblies).ToImmutableHashSet(),
                                                  null
                                              )
                                          )
                                      )
                                      .ToImmutableList()

                            ))
                            .AddRange(returnStatement)
                    )


            );
    }

    private static StatementSyntax GetCollectionVariable(TypeSyntax type) => LocalDeclarationStatement(
        VariableDeclaration(IdentifierName(Identifier(TriviaList(), SyntaxKind.VarKeyword, "var", "var", TriviaList())))
           .WithVariables(
                SingletonSeparatedList(
                    VariableDeclarator(Identifier("items"))
                       .WithInitializer(
                            EqualsValueClause(
                                ObjectCreationExpression(
                                        GenericName(Identifier("List"))
                                           .WithTypeArgumentList(TypeArgumentList(SingletonSeparatedList(type)))
                                    )
                                   .WithArgumentList(ArgumentList())
                            )
                        )
                )
            )
    );

    private const string IReflectionAssemblySelector = nameof(IReflectionAssemblySelector);

    private const string IReflectionTypeSelector = nameof(IReflectionTypeSelector);
    private const string IServiceDescriptorAssemblySelector = nameof(IServiceDescriptorAssemblySelector);

    private static readonly MethodDeclarationSyntax TypesMethod =
        MethodDeclaration(
                GenericName(Identifier("IEnumerable"))
                   .WithTypeArgumentList(
                        TypeArgumentList(SingletonSeparatedList<TypeSyntax>(IdentifierName("Type")))
                    ),
                Identifier("GetTypes")
            )
           .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(IdentifierName("IIndagoProvider"))
            )
           .AddParameterListParameters(
                Parameter(Identifier("selector"))
                   .WithType(
                        GenericName(Identifier("Func"))
                           .AddTypeArgumentListArguments(
                                IdentifierName(IReflectionTypeSelector),
                                GenericName(Identifier("IEnumerable"))
                                   .WithTypeArgumentList(
                                        TypeArgumentList(
                                            SingletonSeparatedList<TypeSyntax>(IdentifierName("Type"))
                                        )
                                    )
                            )
                    ),
                Parameter(Identifier("lineNumber"))
                   .WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                Parameter(Identifier("filePath"))
                   .WithType(PredefinedType(Token(SyntaxKind.StringKeyword))),
                Parameter(Identifier("argumentExpression"))
                   .WithType(PredefinedType(Token(SyntaxKind.StringKeyword)))
            )
           .WithBody(
                Block(
                    GetCollectionVariable(IdentifierName("Type")),
                    ReturnStatement(IdentifierName("items"))
                )
            );

    private static readonly MethodDeclarationSyntax ScanMethod =
        MethodDeclaration(ParseName("Microsoft.Extensions.DependencyInjection.IServiceCollection"), Identifier("Scan"))
           .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName("IIndagoProvider")))
           .AddParameterListParameters(
                Parameter(Identifier("services")).WithType(ParseName("Microsoft.Extensions.DependencyInjection.IServiceCollection")),
                Parameter(Identifier("selector"))
                   .WithType(GenericName(Identifier("Action")).AddTypeArgumentListArguments(IdentifierName(IServiceDescriptorAssemblySelector))),
                Parameter(Identifier("lineNumber")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                Parameter(Identifier("filePath")).WithType(PredefinedType(Token(SyntaxKind.StringKeyword))),
                Parameter(Identifier("argumentExpression")).WithType(PredefinedType(Token(SyntaxKind.StringKeyword)))
            )
           .WithBody(Block(ReturnStatement(IdentifierName("services"))));

    private static readonly MethodDeclarationSyntax AssembliesMethod =
        MethodDeclaration(
                GenericName(Identifier("IEnumerable"))
                   .WithTypeArgumentList(TypeArgumentList(SingletonSeparatedList<TypeSyntax>(IdentifierName("Assembly")))),
                Identifier("GetAssemblies")
            )
           .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName("IIndagoProvider")))
           .AddParameterListParameters(
                Parameter(Identifier("action"))
                   .WithType(
                        GenericName(Identifier("Action"))
                           .WithTypeArgumentList(
                                TypeArgumentList(
                                    SingletonSeparatedList<TypeSyntax>(
                                        IdentifierName(IReflectionAssemblySelector)
                                    )
                                )
                            )
                    ),
                Parameter(Identifier("lineNumber")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                Parameter(Identifier("filePath")).WithType(PredefinedType(Token(SyntaxKind.StringKeyword))),
                Parameter(Identifier("argumentExpression")).WithType(PredefinedType(Token(SyntaxKind.StringKeyword)))
            )
           .WithBody(
                Block(
                    GetCollectionVariable(IdentifierName("Assembly")),
                    ReturnStatement(IdentifierName("items"))
                )
            );
}
