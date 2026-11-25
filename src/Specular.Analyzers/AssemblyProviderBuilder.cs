using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Specular.Analyzers;

internal static class AssemblyProviderBuilder
{
    [SuppressMessage(
        "Security",
        "CA5351:Do Not Use Broken Cryptographic Algorithms",
        Justification = "MD5 is used only as a stable, non-cryptographic cache key for selector text; it is never used for security."
    )]
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
            ? GenerateMethodBody(AssembliesMethod, dohash(hasher, assemblyRequests, Configuration.JsonSourceGenerationContext.Default.ResolvedSourceLocation), returnsCollection: true)
            : AssembliesMethod;

        var resolvedReflectionDetails = reflectionRequests is { Count: > 0 }
            ? GenerateMethodBody(TypesMethod, dohash(hasher, reflectionRequests, Configuration.JsonSourceGenerationContext.Default.ResolvedSourceLocation), returnsCollection: true)
            : TypesMethod;

        var resolvedServiceDescriptorDetails = serviceDescriptorRequests is { Count: > 0 }
            ? GenerateMethodBody(ScanMethod, dohash(hasher, serviceDescriptorRequests, Configuration.JsonSourceGenerationContext.Default.ResolvedSourceLocation), returnsCollection: false)
            : ScanMethod;

        var privateMembers = privateAssemblies
                            .OrderBy(z =>
                                     {
                                         addStringToHash(hasher, z.MetadataName);
                                         return z.MetadataName;
                                     }
                             )
                            .SelectMany(z => StatementGeneration.AssemblyDeclaration(compilation, isAot, z))
                            .ToList();
        if (privateAssemblies.Count != 0)
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

            // The provider resolves inaccessible types from string literals via reflection; the trimmer/ILC can't
            // statically prove that, so it reports IL2026/IL2072 (and #pragma is not honoured by the trimmer).
            // Suppress them with [UnconditionalSuppressMessage] (which IS honoured) on the methods that actually
            // reflect. This is safe because every resolved type and its public constructors are rooted via the
            // [DynamicDependency] attributes emitted below.
            var trimSuppressions = BuildTrimSuppressionAttributeList();
            if (MethodUsesPrivateReflection(resolvedAssemblyDetails, privateAssemblyByVariable))
                resolvedAssemblyDetails = resolvedAssemblyDetails.AddAttributeLists(trimSuppressions);
            if (MethodUsesPrivateReflection(resolvedReflectionDetails, privateAssemblyByVariable))
                resolvedReflectionDetails = resolvedReflectionDetails.AddAttributeLists(trimSuppressions);
            if (MethodUsesPrivateReflection(resolvedServiceDescriptorDetails, privateAssemblyByVariable))
                resolvedServiceDescriptorDetails = resolvedServiceDescriptorDetails.AddAttributeLists(trimSuppressions);

            if (isAot)
            {
                // Native AOT resolves private assemblies via typeof(PublicType).Assembly (no AssemblyLoadContext),
                // so there is no _context field; anchor the trimming roots on an always-present provider method.
                if (dynamicDependencies.Count > 0)
                    resolvedReflectionDetails = resolvedReflectionDetails.AddAttributeLists(BuildDynamicDependencyAttributeList(dynamicDependencies));
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
                                                                            TypeOfExpression(IdentifierName("SpecularProvider")),
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
        return ClassDeclaration("SpecularProvider")
              .AddAttributeLists(Helpers.CompilerGeneratedAttributes)
              .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.SealedKeyword)))
              .WithBaseList(BaseList(SingletonSeparatedList<BaseTypeSyntax>(SimpleBaseType(IdentifierName("ISpecularProvider")))))
              .AddMembers(InstanceProperty)
              .AddMembers(PrivateConstructor)
              .AddMembers(resolvedAssemblyDetails, resolvedReflectionDetails, resolvedServiceDescriptorDetails)
              .AddMembers(privateMembers.ToArray());
    }

    // Collects (assembly, type) pairs for every `<privateAssembly>.GetType("...")` call the generated
    // provider emits, so the trimmer/Native AOT analyzer keeps those dynamically-resolved types. Only
    // pairs whose type actually resolves in the target assembly are kept, so speculative GetType probes
    // don't produce unresolved-dependency warnings (IL2035/IL2036).
    private static List<(string Assembly, string Type)> CollectDynamicDependencies(
        Dictionary<string, IAssemblySymbol> privateAssemblyByVariable,
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

    private static bool MethodUsesPrivateReflection(MethodDeclarationSyntax method, Dictionary<string, IAssemblySymbol> privateAssemblyByVariable) =>
        method
           .DescendantNodes()
           .OfType<InvocationExpressionSyntax>()
           .Any(invocation => invocation.Expression is MemberAccessExpressionSyntax { Name.Identifier.ValueText: "GetType", Expression: IdentifierNameSyntax receiver }
                 && privateAssemblyByVariable.ContainsKey(receiver.Identifier.ValueText)
            );

    private static AttributeListSyntax BuildTrimSuppressionAttributeList() =>
        AttributeList(
            SeparatedList(
                new[]
                {
                    BuildTrimSuppression("IL2026:RequiresUnreferencedCode"),
                    BuildTrimSuppression("IL2072:DynamicallyAccessedMembers"),
                }
            )
        );

    private static AttributeSyntax BuildTrimSuppression(string checkId) =>
        Attribute(ParseName("global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage"))
           .WithArgumentList(
                AttributeArgumentList(
                    SeparatedList(
                        new[]
                        {
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal("Trimming"))),
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(checkId))),
                            AttributeArgument(
                                    LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        Literal("Types resolved by string literal are preserved with their public constructors via [DynamicDependency], so this reflection is trim- and AOT-safe.")
                                    )
                                )
                               .WithNameEquals(NameEquals(IdentifierName("Justification"))),
                        }
                    )
                )
            );

    private static AttributeListSyntax BuildDynamicDependencyAttributeList(IReadOnlyList<(string Assembly, string Type)> dependencies) =>
        AttributeList(
            SeparatedList(
                dependencies.Select(dependency => Attribute(ParseName("global::System.Diagnostics.CodeAnalysis.DynamicDependency"))
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

    // GetTypes and GetAssemblies return each call site's items as one collection expression (returnsCollection); Scan adds to the
    // services it was given. A call site's code arrives as one items.Add(...) block per scanned assembly, the form the build cache
    // stores, and is joined below before it becomes that single return.
    private static MethodDeclarationSyntax GenerateMethodBody(MethodDeclarationSyntax baseMethod, IEnumerable<ResolvedSourceLocation> locations, bool returnsCollection)
    {
        var item = baseMethod.Body?.Statements.ToArray() ?? Array.Empty<StatementSyntax>();
        var returnStatement = item.OfType<ReturnStatementSyntax>().Take(1).Cast<StatementSyntax>().ToImmutableList();

        return baseMethod
           .WithBody(
                Block(
                    ImmutableList
                       .CreateRange(item.Except(returnStatement))
                       .Add(
                            SwitchGenerator.GenerateSwitchStatement(
                                locations
                                   .GroupBy(z => z.Location)
                                   .Select(z => z.Aggregate(
                                               new ResolvedSourceLocation(
                                                   z.First().Location,
                                                   "",
                                                   ImmutableHashSet<string>.Empty,
                                                   [],
                                                   [],
                                                   [],
                                                   z.First().ScannedAssemblyName
                                               ),
                                               (location, sourceLocation) => new(
                                                   location.Location,
                                                   location.Expression + "\n" + sourceLocation.Expression,
                                                   location.PrivateAssemblies.Concat(sourceLocation.PrivateAssemblies).ToImmutableHashSet(),
                                                   location.DiscoveredTypes.AddRange(sourceLocation.DiscoveredTypes),
                                                   location.DiscoveredAssemblies.AddRange(sourceLocation.DiscoveredAssemblies),
                                                   location.DiscoveredServiceDescriptors.AddRange(sourceLocation.DiscoveredServiceDescriptors),
                                                   location.ScannedAssemblyName
                                               )
                                           )
                                    )
                                   .Select(location => returnsCollection ? location with { Expression = ToCollectionReturn(location.Expression) } : location)
                                   .ToImmutableList()
                            )
                        )
                       .AddRange(returnStatement)
                )
            );
    }

    // items.Add(a); items.Add(b); ... becomes return [a, b, ...];. Comments in the blocks (a type the generator could not name) are
    // kept on the return.
    private static string ToCollectionReturn(string expression)
    {
        var elements = new List<CollectionElementSyntax>();
        var comments = new List<SyntaxTrivia>();
        foreach (var statement in SwitchGenerator.ParseStatements(expression))
        {
            if (statement is ExpressionStatementSyntax
                {
                    Expression: InvocationExpressionSyntax
                    {
                        Expression: MemberAccessExpressionSyntax { Expression: IdentifierNameSyntax { Identifier.ValueText: "items" }, Name.Identifier.ValueText: "Add" },
                        ArgumentList.Arguments: [var argument],
                    },
                })
            {
                elements.Add(ExpressionElement(argument.Expression));
                continue;
            }

            if (statement is not EmptyStatementSyntax)
            {
                throw new InvalidOperationException($"Unexpected statement in a collection call site: {statement}");
            }

            AddComments(comments, statement.GetLeadingTrivia());
            AddComments(comments, statement.GetTrailingTrivia());
        }

        var returnStatement = ReturnStatement(CollectionExpression(SeparatedList(elements))).WithLeadingTrivia(comments);
        return Block(returnStatement).ToFullString();

        static void AddComments(List<SyntaxTrivia> comments, SyntaxTriviaList trivia)
        {
            foreach (var item in trivia)
            {
                if (item.IsKind(SyntaxKind.SingleLineCommentTrivia)) comments.AddRange([item, CarriageReturnLineFeed]);
            }
        }
    }

    // public static ISpecularProvider Instance { get; } = new SpecularProvider();
    // Exposes the generated provider as a compile-time singleton so consumers can reference it
    // directly (SpecularProvider.Instance) instead of resolving it through runtime reflection.
    private static readonly PropertyDeclarationSyntax InstanceProperty =
        PropertyDeclaration(IdentifierName("ISpecularProvider"), Identifier("Instance"))
           .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword)))
           .WithAccessorList(
                AccessorList(
                    SingletonList(
                        AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                           .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                    )
                )
            )
           .WithInitializer(
                EqualsValueClause(
                    ObjectCreationExpression(IdentifierName("SpecularProvider")).WithArgumentList(ArgumentList())
                )
            )
           .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

    // private SpecularProvider() { }
    // The generated provider is a singleton; the only way to obtain it is the static Instance
    // property, so the constructor is private to prevent external instantiation.
    private static readonly ConstructorDeclarationSyntax PrivateConstructor =
        ConstructorDeclaration(Identifier("SpecularProvider"))
           .WithModifiers(TokenList(Token(SyntaxKind.PrivateKeyword)))
           .WithBody(Block());

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
                ExplicitInterfaceSpecifier(IdentifierName("ISpecularProvider"))
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
           .WithBody(Block(ReturnStatement(CollectionExpression())));

    private static readonly MethodDeclarationSyntax ScanMethod =
        MethodDeclaration(ParseName("Microsoft.Extensions.DependencyInjection.IServiceCollection"), Identifier("Scan"))
           .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName("ISpecularProvider")))
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
           .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName("ISpecularProvider")))
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
           .WithBody(Block(ReturnStatement(CollectionExpression())));
}
