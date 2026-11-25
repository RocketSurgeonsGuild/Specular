using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Specular.Analyzers.Descriptors;
using Specular.Scanning;

namespace Specular.Analyzers.AssemblyProviders;

internal static class DataHelpers
{
    public static void HandleInvocationExpressionSyntax(
        HashSet<Diagnostic> diagnostics,
        SemanticModel semanticModel,
        ExpressionSyntax selectorExpression,
        ScanEnvironment environment,
        AssemblySelectionBuilder assemblies,
        TypeSelectionBuilder typeFilters,
        List<IServiceTypeDescriptor> serviceTypeFilters,
        ref int lifetime,
        CancellationToken cancellationToken
    )
    {
        if (cancellationToken.IsCancellationRequested) return;
        if (selectorExpression is not InvocationExpressionSyntax expression)
        {
            if (selectorExpression is not SimpleLambdaExpressionSyntax simpleLambdaExpressionSyntax
             || simpleLambdaExpressionSyntax is { ExpressionBody: null }
             || simpleLambdaExpressionSyntax.ExpressionBody is not InvocationExpressionSyntax body)
            {
                diagnostics.Add(Diagnostic.Create(Diagnostics.MustBeAnExpression, selectorExpression.GetLocation()));
                return;
            }

            expression = body;
        }

        if (expression.Expression is not MemberAccessExpressionSyntax memberAccessExpressionSyntax
         || !memberAccessExpressionSyntax.IsKind(SyntaxKind.SimpleMemberAccessExpression))
        {
            return;
        }

        if (memberAccessExpressionSyntax.Expression is InvocationExpressionSyntax childExpression)
        {
            if (cancellationToken.IsCancellationRequested) return;
            HandleInvocationExpressionSyntax(
                diagnostics,
                semanticModel,
                childExpression,
                environment,
                assemblies,
                typeFilters,
                serviceTypeFilters,
                ref lifetime,
                cancellationToken
            );
        }

        var type = ModelExtensions.GetTypeInfo(semanticModel, memberAccessExpressionSyntax.Expression, cancellationToken);
        if (type.Type is null) return;
        var typeName = type.Type.ToDisplayString();
        if (typeName is "Specular.Abstractions.IReflectionAssemblySelector"
                     or "Specular.Abstractions.IServiceDescriptorAssemblySelector")
        {
            if (cancellationToken.IsCancellationRequested) return;
            _ = HandleCompiledAssemblySelector(semanticModel, expression, memberAccessExpressionSyntax.Name, assemblies);
        }

        if (typeName is "Specular.Abstractions.IReflectionTypeSelector"
                     or "Specular.Abstractions.IServiceDescriptorTypeSelector")
        {
            if (cancellationToken.IsCancellationRequested) return;
            if (!HandleCompiledAssemblySelector(semanticModel, expression, memberAccessExpressionSyntax.Name, assemblies))
                typeFilters.ClassFilter = HandleCompiledImplementationTypeSelector(expression, memberAccessExpressionSyntax.Name);
        }

        if (typeName == "Specular.Abstractions.ITypeFilter")
        {
            if (cancellationToken.IsCancellationRequested) return;
            HandleCompiledTypeFilter(diagnostics, semanticModel, expression, memberAccessExpressionSyntax.Name, typeFilters);
        }

        if (typeName is "Specular.Abstractions.IServiceTypeSelector"
                     or "Specular.Abstractions.IServiceLifetimeSelector")
        {
            if (cancellationToken.IsCancellationRequested) return;
            serviceTypeFilters.AddRange(HandleCompiledServiceTypeFilter(diagnostics, semanticModel, expression, memberAccessExpressionSyntax.Name, environment, cancellationToken));
        }

        if (typeName is "Specular.Abstractions.IServiceLifetimeSelector")
        {
            if (cancellationToken.IsCancellationRequested) return;
            if (HandleCompiledServiceLifetimeFilter(expression, memberAccessExpressionSyntax.Name) is { } filter)
                lifetime = filter;
            return;
        }

        foreach (var argument in expression.ArgumentList.Arguments.Where(argument => argument.Expression is SimpleLambdaExpressionSyntax))
        {
            if (cancellationToken.IsCancellationRequested) return;
            HandleInvocationExpressionSyntax(
                diagnostics,
                semanticModel,
                argument.Expression,
                environment,
                assemblies,
                typeFilters,
                serviceTypeFilters,
                ref lifetime,
                cancellationToken
            );
        }
    }

    public static ExpressionSyntax? ExtractSyntaxFromMethod(
        ExpressionSyntax expression,
        NameSyntax name
    )
    {
        return (name, expression) switch
        {
            (GenericNameSyntax { TypeArgumentList.Arguments: [var syntax] }, _) => syntax,
            (SimpleNameSyntax, InvocationExpressionSyntax
            {
                ArgumentList.Arguments: [{ Expression: TypeOfExpressionSyntax typeOfExpression }, ..],
            }) => typeOfExpression.Type,
            // lambda's are methods to be handled.
            (_, TypeOfExpressionSyntax typeOfExpression) => typeOfExpression.Type,
            (_, InvocationExpressionSyntax { ArgumentList.Arguments: [{ Expression: LambdaExpressionSyntax }, ..] }) => null,
            (_, InvocationExpressionSyntax { ArgumentList.Arguments: [{ Expression: LiteralExpressionSyntax }, ..] }) => null,
            (_, InvocationExpressionSyntax { ArgumentList.Arguments: [{ Expression: MemberAccessExpressionSyntax }, ..] }) => null,
            (_, InvocationExpressionSyntax
            {
                ArgumentList.Arguments:
                [
                {
                    Expression: InvocationExpressionSyntax
                    {
                        Expression: IdentifierNameSyntax { Identifier.Value: "nameof" },
                    } nameofName,
                },
                    ..,
                ],
            }) => nameofName,
            _ => throw new MustBeAnExpressionException(expression.GetLocation(), expression.ToFullString()),
        };
    }

    // Whether the call was an assembly clause that could be recorded.
    private static bool HandleCompiledAssemblySelector(
        SemanticModel semanticModel,
        InvocationExpressionSyntax expression,
        SimpleNameSyntax name,
        AssemblySelectionBuilder assemblies
    )
    {
        switch (name.ToFullString())
        {
            case "EntryAssembly":
                assemblies.FromAssembly(semanticModel.Compilation.Assembly);
                return true;
            case "FromAssemblies":
                assemblies.FromAssemblies();
                return true;
            case "IncludeSystemAssemblies":
                assemblies.IncludeSystemAssemblies();
                return true;
            default:
                break;
        }

        var typeSyntax = ExtractSyntaxFromMethod(expression, name);
        if (typeSyntax == null)
            return false;

        if (ModelExtensions.GetTypeInfo(semanticModel, typeSyntax).Type is not INamedTypeSymbol namedType) return false;

        switch (name.Identifier.Text)
        {
            case "DependenciesFromAssemblyOf":
                assemblies.DependenciesOf(namedType.ContainingAssembly);
                return true;
            case "NotFromAssemblyOf":
                assemblies.NotFromAssembly(namedType.ContainingAssembly);
                return true;
            case "FromAssemblyOf":
                assemblies.FromAssembly(namedType.ContainingAssembly);
                return true;
            default:
                return false;
        }
    }

    private static ClassFilter HandleCompiledImplementationTypeSelector(
        InvocationExpressionSyntax expression,
        NameSyntax name
    )
    {
        if (name.ToFullString() is "GetTypes" && expression.ArgumentList.Arguments.Count is >= 2 and <= 2)
        {
            foreach (var argument in expression.ArgumentList.Arguments)
            {
                if (argument.Expression is LiteralExpressionSyntax literalExpressionSyntax && literalExpressionSyntax.Token.IsKind(SyntaxKind.TrueKeyword))
                    return ClassFilter.PublicOnly;
            }
        }

        return ClassFilter.All;
    }

    private static void HandleCompiledTypeFilter(
        HashSet<Diagnostic> diagnostics,
        SemanticModel semanticModel,
        InvocationExpressionSyntax expression,
        SimpleNameSyntax name,
        TypeSelectionBuilder filters
    )
    {
        switch (name, GetSyntaxTypeInfo(semanticModel, expression, name))
        {
            case ({ Identifier.Text: "AssignableToAny" or "NotAssignableToAny" }, _):
                filters.AssignableToAny(!name.Identifier.Text.StartsWith("Not", StringComparison.Ordinal), getTypeArguments(name, expression, semanticModel));
                break;
            case ({ Identifier.Text: "AssignableTo" or "NotAssignableTo" }, { } namedType):
                filters.AssignableTo(!name.Identifier.Text.StartsWith("Not", StringComparison.Ordinal), namedType);
                break;
            case ({ Identifier.Text: "WithAttribute" or "WithoutAttribute" }, { } namedType):
                filters.WithAttribute(!name.Identifier.Text.StartsWith("Without", StringComparison.Ordinal), namedType);
                break;
            case ({ Identifier.Text: "WithAttribute" or "WithoutAttribute" }, _):
                addWithAttributeString(diagnostics, name, expression, semanticModel, filters);
                break;
            case ({ Identifier.Text: "WithAnyAttribute" }, { }):
                filters.WithAnyAttribute(getTypeArguments(name, expression, semanticModel));
                break;
            case ({ Identifier.Text: "WithAnyAttribute" }, _):
                filters.WithAnyAttribute(getAttributeNames(name, expression, semanticModel));
                break;
            case ({ Identifier.Text: "InExactNamespaceOf" or "InNamespaceOf" or "NotInNamespaceOf" }, _):
                addNamespaceTypeFilter(diagnostics, name, expression, semanticModel, filters);
                break;
            case ({ Identifier.Text: "InExactNamespaces" or "InNamespaces" or "NotInNamespaces" }, _):
                addNamespaceStringFilter(diagnostics, name, expression, semanticModel, filters);
                break;
            case ({ Identifier.Text: "EndsWith" or "StartsWith" or "Contains" or "NotEndsWith" or "NotStartsWith" or "NotContains" }, _):
                addNameFilter(diagnostics, name, expression, filters);
                break;
            case ({ Identifier.Text: "KindOf" or "NotKindOf" }, _):
                addTypeKindFilter(diagnostics, name, expression, filters);
                break;
            case ({ Identifier.Text: "InfoOf" or "NotInfoOf" }, _):
                addTypeInfoFilter(diagnostics, name, expression, filters);
                break;
            default:
                break;
        }

        static ImmutableHashSet<INamedTypeSymbol> getTypeArguments(SimpleNameSyntax name, InvocationExpressionSyntax expression, SemanticModel semanticModel) =>
            expression
               .ArgumentList.Arguments.Select(z => GetSyntaxTypeInfo(semanticModel, z.Expression, name))
               .OfType<INamedTypeSymbol>()
               .ToImmutableHashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);

        static void addNameFilter(
            HashSet<Diagnostic> diagnostics,
            SimpleNameSyntax name,
            InvocationExpressionSyntax expression,
            TypeSelectionBuilder filters
        )
        {
            var filter = name.Identifier.Text switch
            {
                "EndsWith" or "NotEndsWith" => TextDirectionFilter.EndsWith,
                "StartsWith" or "NotStartsWith" => TextDirectionFilter.StartsWith,
                "Contains" or "NotContains" => TextDirectionFilter.Contains,
                _ => throw new NotSupportedException(
                    $"Not supported name filter. Method: {name.ToFullString()}  {expression.ToFullString()} method."
                ),
            };
            var stringValues = ImmutableHashSet.CreateBuilder<string>();
            foreach (var argument in expression.ArgumentList.Arguments)
            {
                if (getStringValue(argument) is not { Length: > 0 } item)
                {
                    diagnostics.Add(Diagnostic.Create(Diagnostics.MustBeAString, argument.GetLocation()));
                    continue;
                }

                stringValues.Add(item);
            }

            filters.Names(!name.Identifier.Text.StartsWith("Not", StringComparison.Ordinal), filter, stringValues.ToImmutable());
        }

        static void addNamespaceTypeFilter(
            HashSet<Diagnostic> diagnostics,
            SimpleNameSyntax name,
            InvocationExpressionSyntax expression,
            SemanticModel semanticModel,
            TypeSelectionBuilder filters
        )
        {
            var filter = name.Identifier.Text switch
            {
                "InExactNamespaceOf" => NamespaceFilter.Exact,
                "InNamespaceOf" => NamespaceFilter.In,
                "NotInNamespaceOf" => NamespaceFilter.NotIn,
                _ => throw new NotSupportedException(
                    $"Not supported namespace filter. Method: {name.ToFullString()}  {expression.ToFullString()} method."
                ),
            };

            var namespaces = ImmutableHashSet.CreateBuilder<string>();
            foreach (var argument in expression.ArgumentList.Arguments)
            {
                if (argument.Expression is not TypeOfExpressionSyntax typeOfExpressionSyntax
                 || ModelExtensions.GetTypeInfo(semanticModel, typeOfExpressionSyntax.Type).Type is not { } type)
                {
                    diagnostics.Add(Diagnostic.Create(Diagnostics.MustBeAnExpression, argument.GetLocation()));
                    continue;
                }

                namespaces.Add(type.ContainingNamespace.ToDisplayString());
            }

            if (expression.Expression is MemberAccessExpressionSyntax { Name: GenericNameSyntax { TypeArgumentList.Arguments: [var arg] } }
             && ModelExtensions.GetTypeInfo(semanticModel, arg).Type is { } type2)
            {
                namespaces.Add(type2.ContainingNamespace.ToDisplayString());
            }

            filters.Namespaces(filter, namespaces.ToImmutable());
        }

        static void addNamespaceStringFilter(
            HashSet<Diagnostic> diagnostics,
            SimpleNameSyntax name,
            InvocationExpressionSyntax expression,
            SemanticModel semanticModel,
            TypeSelectionBuilder filters
        )
        {
            var filter = name.Identifier.Text switch
            {
                "InExactNamespaces" => NamespaceFilter.Exact,
                "InNamespaces" => NamespaceFilter.In,
                "NotInNamespaces" => NamespaceFilter.NotIn,
                _ => throw new NotSupportedException(
                    $"Not supported namespace filter. Method: {name.ToFullString()}  {expression.ToFullString()} method."
                ),
            };

            var namespaces = ImmutableHashSet.CreateBuilder<string>();
            foreach (var argument in expression.ArgumentList.Arguments)
            {
                if (argument.Expression is MemberAccessExpressionSyntax
                    {
                        Name.Identifier.Text: "Namespace", Expression: TypeOfExpressionSyntax typeOfExpressionSyntax,
                    }
                 && GetSyntaxTypeInfo(semanticModel, typeOfExpressionSyntax, name) is { } type)
                {
                    namespaces.Add(type.ContainingNamespace.ToDisplayString());
                    continue;
                }

                if (getStringValue(argument) is { Length: > 0 } item)
                {
                    namespaces.Add(item);
                    continue;
                }

                diagnostics.Add(Diagnostic.Create(Diagnostics.MustBeAString, argument.GetLocation()));
            }

            filters.Namespaces(filter, namespaces.ToImmutable());
        }

        static void addWithAttributeString(
            HashSet<Diagnostic> diagnostics,
            SimpleNameSyntax name,
            InvocationExpressionSyntax expression,
            SemanticModel semanticModel,
            TypeSelectionBuilder filters
        )
        {
            if (expression is not { ArgumentList.Arguments: [var argument] }) return;
            var include = !name.Identifier.Text.StartsWith("Without", StringComparison.Ordinal);
            if (argument.Expression is MemberAccessExpressionSyntax
                {
                    Name.Identifier.Text: "FullName", Expression: TypeOfExpressionSyntax typeOfExpressionSyntax,
                }
             && GetSyntaxTypeInfo(semanticModel, typeOfExpressionSyntax, name) is { } type)
            {
                filters.WithAttribute(include, Helpers.GetFullMetadataName(type));
                return;
            }

            if (getStringValue(argument) is { Length: > 0 } item)
            {
                filters.WithAttribute(include, item);
                return;
            }

            diagnostics.Add(Diagnostic.Create(Diagnostics.MustBeAString, argument.GetLocation()));
        }

        static ImmutableHashSet<string> getAttributeNames(
            SimpleNameSyntax name,
            InvocationExpressionSyntax expression,
            SemanticModel semanticModel
        )
        {
            var results = ImmutableHashSet.CreateBuilder<string>();
            foreach (var argument in expression.ArgumentList.Arguments)
            {
                if (argument.Expression is MemberAccessExpressionSyntax
                    {
                        Name.Identifier.Text: "FullName", Expression: TypeOfExpressionSyntax typeOfExpressionSyntax,
                    }
                 && GetSyntaxTypeInfo(semanticModel, typeOfExpressionSyntax, name) is { } type)
                {
                    results.Add(Helpers.GetFullMetadataName(type));
                }

                if (getStringValue(argument) is { Length: > 0 } item)
                {
                    results.Add(item);
                }
            }

            return results.ToImmutable();
        }

        static void addTypeKindFilter(
            HashSet<Diagnostic> diagnostics,
            SimpleNameSyntax name,
            InvocationExpressionSyntax expression,
            TypeSelectionBuilder filters
        )
        {
            var include = name.Identifier.Text switch { "KindOf" => true, _ => false };

            var typeKinds = ImmutableHashSet.CreateBuilder<TypeKind>();
            foreach (var argument in expression.ArgumentList.Arguments)
            {
                if (getStringValue(argument) is not { Length: > 0 } item)
                {
                    diagnostics.Add(Diagnostic.Create(Diagnostics.MustBeAString, argument.GetLocation()));
                    continue;
                }

                typeKinds.Add(Enum.TryParse<TypeKind>(item, ignoreCase: false, out var result) ? result : TypeKind.Unknown);
            }

            filters.KindOf(include, typeKinds.ToImmutable());
        }

        static void addTypeInfoFilter(
            HashSet<Diagnostic> diagnostics,
            SimpleNameSyntax name,
            InvocationExpressionSyntax expression,
            TypeSelectionBuilder filters
        )
        {
            var include = name.Identifier.Text switch { "InfoOf" => true, _ => false };

            var typeInfos = ImmutableHashSet.CreateBuilder<TypeInfoFilter>();
            foreach (var argument in expression.ArgumentList.Arguments)
            {
                if (getStringValue(argument) is not { Length: > 0 } item)
                {
                    diagnostics.Add(Diagnostic.Create(Diagnostics.MustBeAString, argument.GetLocation()));
                    continue;
                }

                typeInfos.Add(Enum.TryParse<TypeInfoFilter>(item, ignoreCase: false, out var result) ? result : TypeInfoFilter.Unknown);
            }

            filters.InfoOf(include, typeInfos.ToImmutable());
        }

        static string? getStringValue(ArgumentSyntax argument)
        {
            return argument.Expression switch
            {
                LiteralExpressionSyntax { Token.RawKind: (int)SyntaxKind.StringLiteralToken, Token.ValueText: { Length: > 0 } result } => result,
                MemberAccessExpressionSyntax { Name.Identifier.Text: var text } => text,
                InvocationExpressionSyntax
                {
                    Expression: IdentifierNameSyntax { Identifier.Text: "nameof" },
                    ArgumentList.Arguments: [{ Expression: IdentifierNameSyntax { Identifier.Text: { Length: > 0 } text } }],
                } => text,
                _ => null,
            };
        }
    }

    private static IEnumerable<IServiceTypeDescriptor> HandleCompiledServiceTypeFilter(
        HashSet<Diagnostic> diagnostics,
        SemanticModel semanticModel,
        InvocationExpressionSyntax expression,
        SimpleNameSyntax name,
        ScanEnvironment environment,
        CancellationToken cancellationToken
    )
    {
        if (name.ToFullString() == "AsSelf")
        {
            yield return new SelfServiceTypeDescriptor();
            yield break;
        }

        if (name.ToFullString() == "AsSelfWithInterfaces")
        {
            yield return new SelfServiceTypeDescriptor();
        }

        if (name.ToFullString() is "AsImplementedInterfaces" or "AsSelfWithInterfaces")
        {
            TypeSelection? interfaceFilter = null;
            if (expression is
                {
                    ArgumentList.Arguments: [{ Expression: SimpleLambdaExpressionSyntax { ExpressionBody: InvocationExpressionSyntax expressionBody } }],
                })
            {
                var interfaceFilters = new TypeSelectionBuilder(environment);
                var lifetime = 0;
                HandleInvocationExpressionSyntax(
                    diagnostics,
                    semanticModel,
                    expressionBody,
                    environment,
                    new(),
                    interfaceFilters,
                    [],
                    ref lifetime,
                    cancellationToken
                );
                interfaceFilter = interfaceFilters.Build();
            }

            yield return new ImplementedInterfacesServiceTypeDescriptor(interfaceFilter);
            yield break;
        }

        if (name.ToFullString() == "AsMatchingInterface")
        {
            yield return new MatchingInterfaceServiceTypeDescriptor();
            yield break;
        }

        if (name is { Identifier.Value: "As" })
        {
            var typeSyntax = Helpers.ExtractSyntaxFromMethod(expression, name);
            if (typeSyntax == null) yield break;

            var typeInfo = semanticModel.GetTypeInfo(typeSyntax, cancellationToken).Type;
            switch (typeInfo)
            {
                case INamedTypeSymbol nts:
                    yield return new CompiledServiceTypeDescriptor(nts);
                    yield break;
                default:
                    diagnostics.Add(Diagnostic.Create(Diagnostics.UnhandledSymbol, name.GetLocation()));
                    yield break;
            }
        }

        if (name is GenericNameSyntax { Identifier.Value: "As", TypeArgumentList.Arguments.Count: 1 })
        {
            var typeSyntax = Helpers.ExtractSyntaxFromMethod(expression, name);
            if (typeSyntax == null) yield break;

            var typeInfo = semanticModel.GetTypeInfo(typeSyntax, cancellationToken).Type;
            switch (typeInfo)
            {
                case INamedTypeSymbol nts:
                    yield return new CompiledServiceTypeDescriptor(nts);
                    yield break;
                default:
                    diagnostics.Add(Diagnostic.Create(Diagnostics.UnhandledSymbol, name.GetLocation()));
                    yield break;
            }
        }
    }

    private static int? HandleCompiledServiceLifetimeFilter(
        InvocationExpressionSyntax expression,
        NameSyntax name
    )
    {
        return name.ToFullString() == "WithLifetime"
         && expression is { ArgumentList.Arguments: [{ Expression: MemberAccessExpressionSyntax { Name: { } lifetimeName } }] }
            ? lifetimeName.Identifier.Text switch { "Scoped" => 1, "Transient" => 2, "Singleton" => 0, _ => throw new ArgumentOutOfRangeException(nameof(expression)) }
            : name.ToFullString() switch
            {
                "WithSingletonLifetime" => 0, // Singleton
                "WithScopedLifetime" => 1, // Scoped
                "WithTransientLifetime" => 2, // Transient
                _ => null,
            };
    }

    private static INamedTypeSymbol? GetSyntaxTypeInfo(
        SemanticModel semanticModel,
        ExpressionSyntax expression,
        NameSyntax name
    ) =>
        ExtractSyntaxFromMethod(expression, name) is not { } type
     || ModelExtensions.GetTypeInfo(semanticModel, type).Type is not INamedTypeSymbol { Kind: not SymbolKind.ErrorType } nts
            ? null
            : nts;
}

#pragma warning disable RCS1194 // Implement exception constructors
#pragma warning disable CA1032 // Implement standard exception constructors
public class MustBeAnExpressionException(Location location, string expression) : Exception($"The expression {expression} must be a constant expression.")
#pragma warning restore CA1032 // Implement standard exception constructors
#pragma warning restore RCS1194 // Implement exception constructors
{
    public Location Location { get; } = location;
}
