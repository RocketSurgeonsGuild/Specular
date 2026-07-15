using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Specular.Analyzers.Descriptors;

internal sealed record CompiledServiceTypeDescriptors(ImmutableArray<IServiceTypeDescriptor> ServiceTypeDescriptors, int Lifetime)
{
    public string GetLifetime() => Lifetime switch { 1 => "Scoped", 2 => "Transient", _ => "Singleton" };

    public MemberAccessExpressionSyntax GetLifetimeExpression() =>
        MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            IdentifierName("ServiceLifetime"),
            IdentifierName(GetLifetime())
        );
}
