using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Specular.Analyzers.Configuration;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Specular.Analyzers.ScanReport;

internal static class ScanReportBuilder
{
    public static ClassDeclarationSyntax GetScanReport(
        ImmutableList<ResolvedSourceLocation> assemblySources,
        ImmutableList<ResolvedSourceLocation> reflectionSources,
        ImmutableList<ResolvedSourceLocation> serviceDescriptorSources
    )
    {
        var assemblyReports = assemblySources
                             .GroupBy(z => z.Location)
                             .OrderBy(z => z.Key.FilePath, StringComparer.Ordinal)
                             .ThenBy(z => z.Key.LineNumber)
                             .ThenBy(z => z.Key.ExpressionHash, StringComparer.Ordinal)
                             .Select(group =>
                                         new AssemblyReport(
                                             group.Key,
                                             group.SelectMany(z => z.DiscoveredAssemblies).Distinct(StringComparer.Ordinal).OrderBy(z => z, StringComparer.Ordinal).ToImmutableArray()
                                         )
                              )
                             .ToImmutableArray();
        var typeReports = reflectionSources
                         .GroupBy(z => z.Location)
                         .OrderBy(z => z.Key.FilePath, StringComparer.Ordinal)
                         .ThenBy(z => z.Key.LineNumber)
                         .ThenBy(z => z.Key.ExpressionHash, StringComparer.Ordinal)
                         .Select(group =>
                                     new TypeReport(
                                         group.Key,
                                         group
                                            .Where(z => z.ScannedAssemblyName is { })
                                            .GroupBy(z => z.ScannedAssemblyName!, StringComparer.Ordinal)
                                            .OrderBy(z => z.Key, StringComparer.Ordinal)
                                            .Select(assembly => new TypeAssemblyReport(
                                                        assembly.Key,
                                                        assembly.SelectMany(z => z.DiscoveredTypes).Distinct().OrderBy(z => z.Type, StringComparer.Ordinal).ToImmutableArray()
                                                    )
                                             )
                                            .ToImmutableArray()
                                     )
                          )
                         .ToImmutableArray();
        var serviceReports = serviceDescriptorSources
                            .GroupBy(z => z.Location)
                            .OrderBy(z => z.Key.FilePath, StringComparer.Ordinal)
                            .ThenBy(z => z.Key.LineNumber)
                            .ThenBy(z => z.Key.ExpressionHash, StringComparer.Ordinal)
                            .Select(group =>
                                        new ServiceDescriptorReport(
                                            group.Key,
                                            group
                                               .Where(z => z.ScannedAssemblyName is { })
                                               .GroupBy(z => z.ScannedAssemblyName!, StringComparer.Ordinal)
                                               .OrderBy(z => z.Key, StringComparer.Ordinal)
                                               .Select(assembly => new ServiceDescriptorAssemblyReport(
                                                           assembly.Key,
                                                           assembly
                                                              .SelectMany(z => z.DiscoveredServiceDescriptors)
                                                              .Distinct()
                                                              .OrderBy(z => z.ServiceType, StringComparer.Ordinal)
                                                              .ThenBy(z => z.ImplementationType, StringComparer.Ordinal)
                                                              .ThenBy(z => z.Lifetime, StringComparer.Ordinal)
                                                              .ToImmutableArray()
                                                       )
                                                )
                                               .ToImmutableArray()
                                        )
                             )
                            .ToImmutableArray();

        var reportDelcaration = ClassDeclaration("SpecularScanReport")
                               .AddAttributeLists(Helpers.CompilerGeneratedAttributes)
                               .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.SealedKeyword)))
                               .WithMembers(
                                    SingletonList<MemberDeclarationSyntax>(
                                        MethodDeclaration(IdentifierName("ScanResults"), Identifier("GetScanResults"))
                                           .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword)))
                                           .WithExpressionBody(
                                                ArrowExpressionClause(
                                                    ImplicitObjectCreationExpression()
                                                       .WithArgumentList(
                                                            ArgumentList(
                                                                SeparatedList(
                                                                    [
                                                                        Argument(CreateAssemblyReports(assemblyReports)),
                                                                        Argument(CreateTypeReports(typeReports)),
                                                                        Argument(CreateServiceDescriptorReports(serviceReports))
                                                                    ]
                                                                )
                                                            )
                                                        )
                                                )
                                            )
                                    )
                                );

        return reportDelcaration.NormalizeWhitespace();
    }

    private static ExpressionSyntax CreateAssemblyReports(ImmutableArray<AssemblyReport> reports)
    {
        return CollectionExpression(
            SeparatedList<CollectionElementSyntax>(
                reports.Select(report => ExpressionElement(
                                   ImplicitObjectCreationExpression()
                                      .AddArgumentListArguments(
                                           Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(report.Location.SourceAssemblyName))),
                                           Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(report.Location.SourceExpression))),
                                           Argument(
                                               CollectionExpression(
                                                   SeparatedList<CollectionElementSyntax>(
                                                       report.Assemblies.Select(assembly => ExpressionElement(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(assembly))))
                                                   )
                                               )
                                           )
                                       )
                               )
                )
            )
        );
    }

    private static ExpressionSyntax CreateTypeReports(ImmutableArray<TypeReport> reports)
    {
        return CollectionExpression(
            SeparatedList<CollectionElementSyntax>(
                reports.Select(report => ExpressionElement(
                                   ImplicitObjectCreationExpression()
                                      .AddArgumentListArguments(
                                           Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(report.Location.SourceAssemblyName))),
                                           Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(report.Location.SourceExpression))),
                                           Argument(
                                               CollectionExpression(
                                                   SeparatedList<CollectionElementSyntax>(
                                                       report
                                                          .Assemblies
                                                          .Select(assembly =>
                                                                  {
                                                                      var typeReports = assembly
                                                                                       .Types
                                                                                       .Select(type => ExpressionElement(
                                                                                                   ImplicitObjectCreationExpression()
                                                                                                      .AddArgumentListArguments(
                                                                                                           Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(type.Type)))
                                                                                                       )
                                                                                               )
                                                                                        );
                                                                      return ExpressionElement(
                                                                          ImplicitObjectCreationExpression()
                                                                             .AddArgumentListArguments(
                                                                                  Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(assembly.AssemblyName))),
                                                                                  Argument(CollectionExpression(SeparatedList<CollectionElementSyntax>(typeReports)))
                                                                              )
                                                                      );
                                                                  }
                                                           )
                                                   )
                                               )
                                           )
                                       )
                               )
                )
            )
        );
    }

    private static ExpressionSyntax CreateServiceDescriptorReports(ImmutableArray<ServiceDescriptorReport> reports)
    {
        return CollectionExpression(
            SeparatedList<CollectionElementSyntax>(
                reports.Select(report => ExpressionElement(
                                   ImplicitObjectCreationExpression()
                                      .AddArgumentListArguments(
                                           Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(report.Location.SourceAssemblyName))),
                                           Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(report.Location.SourceExpression))),
                                           Argument(
                                               CollectionExpression(
                                                   SeparatedList<CollectionElementSyntax>(
                                                       report.Assemblies
                                                             .Select(assembly =>
                                                                     {
                                                                         var typeReports = assembly
                                                                                          .Entries.Select(type => ExpressionElement(
                                                                                                              ImplicitObjectCreationExpression()
                                                                                                                 .AddArgumentListArguments(
                                                                                                                      Argument(
                                                                                                                          MemberAccessExpression(
                                                                                                                              SyntaxKind.SimpleMemberAccessExpression,
                                                                                                                              IdentifierName("ServiceLifetime"),
                                                                                                                              IdentifierName(type.Lifetime)
                                                                                                                          )
                                                                                                                      ),
                                                                                                                      Argument(
                                                                                                                          LiteralExpression(
                                                                                                                              SyntaxKind.StringLiteralExpression,
                                                                                                                              Literal(type.ServiceType)
                                                                                                                          )
                                                                                                                      ),
                                                                                                                      Argument(
                                                                                                                          LiteralExpression(
                                                                                                                              SyntaxKind.StringLiteralExpression,
                                                                                                                              Literal(type.ImplementationType)
                                                                                                                          )
                                                                                                                      )
                                                                                                                  )
                                                                                                          )
                                                                                           );
                                                                         return ExpressionElement(
                                                                             ImplicitObjectCreationExpression()
                                                                                .AddArgumentListArguments(
                                                                                     Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(assembly.AssemblyName))),
                                                                                     Argument(CollectionExpression(SeparatedList<CollectionElementSyntax>(typeReports)))
                                                                                 )
                                                                         );
                                                                     }
                                                              )
                                                   )
                                               )
                                           )
                                       )
                               )
                )
            )
        );
    }

    private sealed record AssemblyReport(SourceLocation Location, ImmutableArray<string> Assemblies);

    private sealed record TypeReport(SourceLocation Location, ImmutableArray<TypeAssemblyReport> Assemblies);

    private sealed record TypeAssemblyReport(string AssemblyName, ImmutableArray<ScanReportTypeData> Types);

    private sealed record ServiceDescriptorReport(SourceLocation Location, ImmutableArray<ServiceDescriptorAssemblyReport> Assemblies);

    private sealed record ServiceDescriptorAssemblyReport(string AssemblyName, ImmutableArray<ServiceDescriptorScanReportEntryData> Entries);
}
