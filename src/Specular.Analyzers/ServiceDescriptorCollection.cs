using System.Collections.Immutable;
using System.Globalization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Specular.Analyzers.AssemblyProviders;
using Specular.Analyzers.Configuration;
using Specular.Analyzers.Descriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

// ReSharper disable UseCollectionExpression

namespace Specular.Analyzers;

internal static class ServiceDescriptorCollection
{
    public static IncrementalValueProvider<ImmutableArray<(InvocationExpressionSyntax method, ExpressionSyntax selector, SemanticModel semanticModel)>> Create(
        SyntaxValueProvider valueProvider,
        IncrementalValueProvider<bool> hasAssemblyLoadContext
    ) => valueProvider
        .CreateSyntaxProvider((node, _) => IsValidMethod(node), (syntaxContext, _) => GetServiceDescriptorMethod(syntaxContext))
        .Combine(hasAssemblyLoadContext)
        .Where(z => z is { Right: true, Left: { method: { }, selector: { } } })
        .Select((tuple, _) => tuple.Left)
        .Collect();

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "A source generator must never crash the build; unexpected exceptions are surfaced as diagnostics.")]
    public static ResolvedSourceLocation? ResolveSource(
        AssemblyProviderConfiguration configuration,
        Compilation compilation,
        HashSet<Diagnostic> diagnostics,
        Item item,
        IAssemblySymbol targetAssembly
    )
    {
        try
        {
            return SymbolEqualityComparer.Default.Equals(targetAssembly, compilation.Assembly)
                ? resolvedSourceLocation()
                : configuration.CacheSourceLocation(
                    item.Location,
                    targetAssembly,
                    resolvedSourceLocation
                );

            ResolvedSourceLocation? resolvedSourceLocation()
            {
                var pa = new HashSet<IAssemblySymbol>(SymbolEqualityComparer.Default);
                var reducedTypes = new TypeSymbolVisitor(compilation, item.AssemblyFilter, item.TypeFilter)
                                  .GetReferencedTypes(targetAssembly)
                                  .GetTypes();

                if (reducedTypes.Count == 0) return null;

                var descriptorGeneration = GenerateDescriptors(configuration, diagnostics, reducedTypes, item.ServicesTypeFilter, pa);
                var localBlock = descriptorGeneration.Block.NormalizeWhitespace().ToFullString().Replace("\r", "");
                return new(
                    item.Location,
                    localBlock,
                    pa.Select(z => z.MetadataName).ToImmutableHashSet(),
                    targetAssembly.GetCachedVersion(),
                    [],
                    DiscoveredAssemblies: [],
                    DiscoveredServiceDescriptors: descriptorGeneration.ReportEntries,
                    ScannedAssemblyName: targetAssembly.MetadataName
                );
            }
        }
        catch (Exception e)
        {
            _ = diagnostics.Add(
                Diagnostic.Create(
                    Diagnostics.UnhandledException,
                    null,
                    e.Message,
                    e.StackTrace.Replace("\r", "").Replace("\n", ""),
                    e.GetType().Name,
                    e.ToString()
                )
            );
            return null;
        }
    }

    public static ImmutableList<ResolvedSourceLocation> ResolveSources(
        AssemblyProviderConfiguration configuration,
        Compilation compilation,
        HashSet<Diagnostic> diagnostics,
        IReadOnlyList<Item> items,
        IAssemblySymbol targetAssembly
    )
    {
        if (items.Count == 0) return ImmutableList<ResolvedSourceLocation>.Empty;

        var results = new List<ResolvedSourceLocation>();
        foreach (var item in items)
        {
            if (ResolveSource(configuration, compilation, diagnostics, item, targetAssembly) is not { } location) continue;

            results.Add(location);
        }

        return results.ToImmutableList();
    }

    public sealed record Item
    (
        SourceLocation Location,
        CompiledAssemblyFilter AssemblyFilter,
        CompiledTypeFilter TypeFilter,
        CompiledServiceTypeDescriptors ServicesTypeFilter,
        int Lifetime);

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "A source generator must never crash the build; unexpected exceptions are surfaced as diagnostics.")]
    internal static ImmutableList<Item> GetServiceDescriptorItems(
        Compilation compilation,
        HashSet<Diagnostic> diagnostics,
        IReadOnlyList<(InvocationExpressionSyntax expression, ExpressionSyntax selector, SemanticModel semanticModel)> results,
        CancellationToken cancellationToken
    )
    {
        var items = ImmutableList.CreateBuilder<Item>();
        foreach (var tuple in results)
        {
            try
            {
                (var methodCallSyntax, var selector, _) = tuple;

                var semanticModel = compilation.GetSemanticModel(tuple.expression.SyntaxTree);
                if (!Helpers.IsSpecularProviderCall(semanticModel, methodCallSyntax)) continue;

                List<IAssemblyDescriptor> assemblies = [];
                List<ITypeFilterDescriptor> typeFilters =
                [
                    new TypeKindFilterDescriptor(true, ImmutableHashSet.Create(TypeKind.Class)),
                    new TypeInfoFilterDescriptor(false, ImmutableHashSet.Create(TypeInfoFilter.Abstract, TypeInfoFilter.Static)),
                ];
                List<IServiceTypeDescriptor> serviceDescriptors = [];
                var classFilter = ClassFilter.All;
                var lifetime = 2;

                DataHelpers.HandleInvocationExpressionSyntax(
                    diagnostics,
                    semanticModel,
                    selector,
                    assemblies,
                    typeFilters,
                    serviceDescriptors,
                    ref lifetime,
                    ref classFilter,
                    cancellationToken
                );

                var source = Helpers.CreateSourceLocation(SourceLocationKind.ServiceDescriptor, methodCallSyntax, compilation.Assembly.MetadataName, cancellationToken);
                var assemblyFilter = new CompiledAssemblyFilter(assemblies.ToImmutableList(), source);
                var typeFilter = new CompiledTypeFilter(classFilter, typeFilters.ToImmutableList(), source);
                var serviceDescriptorFilter = new CompiledServiceTypeDescriptors(serviceDescriptors.ToImmutableArray(), lifetime);

                var i = new Item(source, assemblyFilter, typeFilter, serviceDescriptorFilter, lifetime);
                items.Add(i);
            }
            catch (MustBeAnExpressionException e)
            {
                _ = diagnostics.Add(Diagnostic.Create(Diagnostics.MustBeAnExpression, e.Location));
            }
            catch (Exception e)
            {
                _ = diagnostics.Add(
                    Diagnostic.Create(
                        Diagnostics.UnhandledException,
                        null,
                        e.Message,
                        e.StackTrace.Replace("\r", "").Replace("\n", ""),
                        e.GetType().Name,
                        e.ToString()
                    )
                );
            }
        }

        return items.ToImmutable();
    }

    private static ServiceDescriptorGeneration GenerateDescriptors(
        AssemblyProviderConfiguration configuration,
        HashSet<Diagnostic> diagnostics,
        IEnumerable<INamedTypeSymbol> types,
        CompiledServiceTypeDescriptors serviceTypes,
        HashSet<IAssemblySymbol> privateAssemblies
    )
    {
        var compilation = configuration.Compilation;
        var asSelf = serviceTypes.ServiceTypeDescriptors.OfType<SelfServiceTypeDescriptor>().Any() || !serviceTypes.ServiceTypeDescriptors.Any();
        var asImplementedInterfaces = serviceTypes.ServiceTypeDescriptors.OfType<ImplementedInterfacesServiceTypeDescriptor>().ToArray();
        var asMatchingInterface = serviceTypes.ServiceTypeDescriptors.OfType<MatchingInterfaceServiceTypeDescriptor>().Any();
        var asSpecificTypes = serviceTypes.ServiceTypeDescriptors.OfType<CompiledServiceTypeDescriptor>().Select(z => z.Type).Where(z => z is { }).ToArray();
        var registrationLifetimeAttribute = compilation.GetTypeByMetadataName("Specular.RegistrationLifetimeAttribute")!;
        var serviceRegistrationAttribute = compilation.GetTypeByMetadataName("Specular.ServiceRegistrationAttribute")!;
        var serviceRegistrationBaseAttribute = compilation.GetTypeByMetadataName("Specular.ServiceRegistrationBaseAttribute")!;
        var keyedServiceRegistrationBaseAttribute = compilation.GetTypeByMetadataName("Specular.KeyedServiceRegistrationBaseAttribute")!;

        var orderedAsSpecificTypes = asSpecificTypes.OrderBy(Helpers.GetFullMetadataName).ToArray();
        var services = new List<InvocationExpressionSyntax>();
        var reportEntries = ImmutableList.CreateBuilder<ServiceDescriptorScanReportEntryData>();

        foreach (var type in types.OrderBy(Helpers.GetFullMetadataName))
        {
#pragma warning disable RS1024
            var emittedTypes = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
#pragma warning restore RS1024
            var typeIsOpenGeneric = type.IsOpenGenericType();
            if (!compilation.IsSymbolAccessibleWithin(type, compilation.Assembly)) _ = privateAssemblies.Add(type.ContainingAssembly);

            var discoveredLifetime =
                type
                   .GetAttributes()
                   .SingleOrDefault(
                        attribute => attribute.AttributeClass is { }
                         && SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, registrationLifetimeAttribute)
                    );

            var serviceRegistrationAttributes = new List<AttributeData>();
            foreach (var attribute in type.GetAttributes())
            {
                // The generic attributes are a distinct symbol per arity, so they are found through the shared base type, as the keyed ones are
                if (attribute.AttributeClass is { } attributeClass
                 && ( SymbolEqualityComparer.Default.Equals(attributeClass, serviceRegistrationAttribute)
                     || attributeClass.IsGenericType && SymbolEqualityComparer.Default.Equals(attributeClass.BaseType, serviceRegistrationBaseAttribute) ))
                {
                    serviceRegistrationAttributes.Add(attribute);
                }
            }

            // attribute priority for discovered services
            // DiscoveredLifetimeAttribute -> ServiceRegistrationAttribute(lifetime?) -> ServiceRegistrationAttribute()

            // attribute priority for service registrations
            // ServiceRegistrationAttribute(lifetime?, types) -> DiscoveredLifetimeAttribute -> ServiceRegistrationAttribute(lifetime?) -> ServiceRegistrationAttribute()

            // group all attributes and find any common services to create a lifetime map
            var lifetimeValue = serviceTypes.GetLifetime();
            foreach (var attribute in serviceRegistrationAttributes)
            {
                if (attribute.ConstructorArguments.Length == 1)
                {
                    lifetimeValue = GetLifetimeValue(attribute) ?? lifetimeValue;
                }
            }

            lifetimeValue = GetLifetimeValue(discoveredLifetime) ?? lifetimeValue;

            if (configuration.IsAot && StatementGeneration.GetUnreachableType(compilation, type) is { } unreachable)
            {
                _ = diagnostics.Add(
                    Diagnostic.Create(Diagnostics.PrivateTypeUnreachableUnderAot, type.Locations.FirstOrDefault(), Helpers.GetGenericDisplayName(type), type.ContainingAssembly.Identity.Name)
                );
                continue;
            }

            var attributedServiceTypes = new List<(string Lifetime, INamedTypeSymbol Type)>();
            for (var index = 0; index < serviceRegistrationAttributes.Count; index++)
            {
                var attributeData = serviceRegistrationAttributes[index];
                var attributeServiceTypes = GetAttributeServiceTypes(attributeData, 1, type);

                // Prepending the type for every attribute would make a type with several attributes a duplicate of itself
                if (index == 0)
                {
                    attributedServiceTypes.Add((GetRegistrationLifetime(type, registrationLifetimeAttribute) ?? GetLifetimeValue(attributeData) ?? lifetimeValue, type));
                }
                foreach (var serviceType in attributeServiceTypes)
                {
                    attributedServiceTypes.Add((GetRegistrationLifetime(serviceType, registrationLifetimeAttribute) ?? GetLifetimeValue(attributeData) ?? lifetimeValue, serviceType));
                }
            }

            var lifetimeRegistrations = Helpers.GroupInOrder(attributedServiceTypes, static z => Helpers.GetFullMetadataName(z.Type));


            var abort = false;
            foreach (var (_, items) in lifetimeRegistrations)
            {
                if (items.Count <= 1)
                {
                    continue;
                }

                foreach (var item in items)
                {
                    if (!item.Type.DeclaringSyntaxReferences.Any()) continue;

                    _ = diagnostics.Add(Diagnostic.Create(Diagnostics.DuplicateServiceDescriptorAttribute, item.Type.Locations.FirstOrDefault()));
                    abort = true;
                }
            }

            if (abort) return new(Block(), []);

            foreach (var (_, items) in lifetimeRegistrations)
            {
                (var lifetime, var serviceType) = items[0];
                if (emittedTypes.Contains(serviceType)) continue;
                // GetRequiredService<Foo<>>() is not valid C#, so an open generic implementation is registered by type definition
                var statement = !typeIsOpenGeneric && !SymbolEqualityComparer.Default.Equals(serviceType, type)
                    ? StatementGeneration.GenerateServiceFactory(
                        compilation,
                        serviceType,
                        type,
                        lifetime
                    )
                    : StatementGeneration.GenerateServiceType(
                        compilation,
                        serviceType,
                        type,
                        lifetime
                    );
                if (statement is null)
                {
                    _ = diagnostics.Add(Diagnostic.Create(Diagnostics.PrivateTypeUnreachableUnderAot, type.Locations.FirstOrDefault(), Helpers.GetGenericDisplayName(type), type.ContainingAssembly.MetadataName));
                }
                else
                {
                    services.Add(statement);
                    reportEntries.Add(new(lifetime, Helpers.GetGenericDisplayName(serviceType), Helpers.GetGenericDisplayName(type)));
                }

                _ = emittedTypes.Add(serviceType);
            }

            var keyedServiceRegistrationAttributes = new List<AttributeData>();
            foreach (var attribute in type.GetAttributes())
            {
                if (attribute.AttributeClass is { } && SymbolEqualityComparer.Default.Equals(attribute.AttributeClass.BaseType, keyedServiceRegistrationBaseAttribute))
                {
                    keyedServiceRegistrationAttributes.Add(attribute);
                }
            }

            if (keyedServiceRegistrationAttributes.Count > 0)
            {
                var keyedServiceTypes = new List<(ExpressionSyntax KeyExpression, string KeyText, string Lifetime, INamedTypeSymbol Type)>();
                foreach (var attributeData in keyedServiceRegistrationAttributes)
                {
                    var keyExpression = GetKeyExpression(attributeData.ConstructorArguments[0]);
                    var keyText = keyExpression.NormalizeWhitespace().ToFullString();
                    var attributeLifetime = attributeData.ConstructorArguments is [_, { Kind: TypedConstantKind.Enum } lifetimeArgument, ..]
                        ? GetLifetimeValue(lifetimeArgument)
                        : null;

                    var serviceTypesForAttribute = GetAttributeServiceTypes(attributeData, 2, type);

                    keyedServiceTypes.Add((keyExpression, keyText, GetRegistrationLifetime(type, registrationLifetimeAttribute) ?? attributeLifetime ?? GetLifetimeValue(discoveredLifetime) ?? "Singleton", type));
                    foreach (var serviceType in serviceTypesForAttribute)
                    {
                        keyedServiceTypes.Add((keyExpression, keyText, GetRegistrationLifetime(serviceType, registrationLifetimeAttribute) ?? attributeLifetime ?? GetLifetimeValue(discoveredLifetime) ?? "Singleton", serviceType));
                    }
                }

                var keyedRegistrations = Helpers.GroupInOrder(keyedServiceTypes, static z => (Helpers.GetFullMetadataName(z.Type), z.KeyText));

                var keyedAbort = false;
                foreach (var (_, items) in keyedRegistrations)
                {
                    if (items.Count <= 1) continue;

                    foreach (var item in items)
                    {
                        if (!item.Type.DeclaringSyntaxReferences.Any()) continue;

                        _ = diagnostics.Add(Diagnostic.Create(Diagnostics.DuplicateServiceDescriptorAttribute, item.Type.Locations.FirstOrDefault()));
                        keyedAbort = true;
                    }
                }

                if (keyedAbort) return new(Block(), []);

                foreach (var (_, items) in keyedRegistrations)
                {
                    var registration = items[0];
                    var statement = !SymbolEqualityComparer.Default.Equals(registration.Type, type)
                        ? StatementGeneration.GenerateServiceFactory(
                            compilation,
                            registration.Type,
                            type,
                            registration.Lifetime,
                            registration.KeyExpression
                        )
                        : StatementGeneration.GenerateServiceType(
                            compilation,
                            registration.Type,
                            type,
                            registration.Lifetime,
                            registration.KeyExpression
                        );
                    if (statement is null)
                    {
                        _ = diagnostics.Add(Diagnostic.Create(Diagnostics.PrivateTypeUnreachableUnderAot, type.Locations.FirstOrDefault(), Helpers.GetGenericDisplayName(type), type.ContainingAssembly.MetadataName));
                    }
                    else
                    {
                        services.Add(statement);
                        reportEntries.Add(new(registration.Lifetime, Helpers.GetGenericDisplayName(registration.Type), Helpers.GetGenericDisplayName(type)));
                    }

                    // Keyed registrations live in their own key-scoped slot, but the type itself has still been
                    // accounted for: suppress the scan-level AsSelf/AsImplementedInterfaces/etc. defaults for it,
                    // the same way an explicit (non-keyed) ServiceRegistrationAttribute would.
                    _ = emittedTypes.Add(registration.Type);
                }
            }

            if (asSelf && !emittedTypes.Contains(type))
            {
                var statement = StatementGeneration.GenerateServiceType(
                    compilation,
                    type,
                    type,
                    serviceTypes.GetLifetime()
                );
                if (statement is null)
                {
                    _ = diagnostics.Add(Diagnostic.Create(Diagnostics.PrivateTypeUnreachableUnderAot, type.Locations.FirstOrDefault(), Helpers.GetGenericDisplayName(type), type.ContainingAssembly.MetadataName));
                }
                else
                {
                    services.Add(statement);
                    reportEntries.Add(new(serviceTypes.GetLifetime(), Helpers.GetGenericDisplayName(type), Helpers.GetGenericDisplayName(type)));
                }

                if (!compilation.IsSymbolAccessibleWithin(type, compilation.Assembly)) _ = privateAssemblies.Add(type.ContainingAssembly);

                _ = emittedTypes.Add(type);
            }

            if (asMatchingInterface)
            {
                var name = $"I{type.Name}";
                var @interface = type.AllInterfaces.FirstOrDefault(z => z.Name == name);
                if (@interface is { } && !emittedTypes.Contains(@interface))
                {
                    var statement = typeIsOpenGeneric || !asSelf
                        ? StatementGeneration.GenerateServiceType(
                            compilation,
                            @interface,
                            type,
                            serviceTypes.GetLifetime()
                        )
                        : StatementGeneration.GenerateServiceFactory(
                            compilation,
                            @interface,
                            type,
                            serviceTypes.GetLifetime()
                        );
                    if (statement is null)
                    {
                        _ = diagnostics.Add(Diagnostic.Create(Diagnostics.PrivateTypeUnreachableUnderAot, type.Locations.FirstOrDefault(), Helpers.GetGenericDisplayName(type), type.ContainingAssembly.MetadataName));
                    }
                    else
                    {
                        services.Add(statement);
                        reportEntries.Add(new(serviceTypes.GetLifetime(), Helpers.GetGenericDisplayName(type), Helpers.GetGenericDisplayName(type)));
                    }

                    if (!compilation.IsSymbolAccessibleWithin(@interface, compilation.Assembly)) _ = privateAssemblies.Add(type.ContainingAssembly);

                    _ = emittedTypes.Add(@interface);
                }
            }

            if (asImplementedInterfaces is { Length: > 0 })
            {
                // filter here
                var interfaces = new List<INamedTypeSymbol>();
                // todo: filter interfaces by type filter or implemented interfaces filter expression (a reuse of the type filter)
                // It should support open generic types as well (think abstract validator from fluent validation)
                interfaces.AddRange(
                    asImplementedInterfaces is [{ InterfaceFilter: { } filter }]
                        ? type.AllInterfaces.Where(item => filter.IsMatch(compilation, item))
                        : type.AllInterfaces
                );

                foreach (var @interface in Helpers.OrderByFullMetadataName(interfaces))
                {
                    if (emittedTypes.Contains(@interface)) continue;

                    var statement = typeIsOpenGeneric || !asSelf
                        ? StatementGeneration.GenerateServiceType(
                            compilation,
                            @interface,
                            type,
                            serviceTypes.GetLifetime()
                        )
                        : StatementGeneration.GenerateServiceFactory(
                            compilation,
                            @interface,
                            type,
                            serviceTypes.GetLifetime()
                        );
                    if (statement is null)
                    {
                        _ = diagnostics.Add(Diagnostic.Create(Diagnostics.PrivateTypeUnreachableUnderAot, type.Locations.FirstOrDefault(), Helpers.GetGenericDisplayName(type), type.ContainingAssembly.MetadataName));
                    }
                    else
                    {
                        services.Add(statement);
                        reportEntries.Add(new(serviceTypes.GetLifetime(), Helpers.GetGenericDisplayName(@interface), Helpers.GetGenericDisplayName(type)));
                    }

                    if (!compilation.IsSymbolAccessibleWithin(@interface, compilation.Assembly)) _ = privateAssemblies.Add(type.ContainingAssembly);

                    _ = emittedTypes.Add(@interface);
                }
            }

            foreach (var asType in orderedAsSpecificTypes)
            {
                if (emittedTypes.Contains(asType)) continue;

                if (!Helpers.HasImplicitGenericConversion(compilation, asType, type)) continue;

                var closedServiceType = Helpers.GetClosedGenericConversion(compilation, asType, type);
                var statement = !asSelf
                    ? StatementGeneration.GenerateServiceType(
                        compilation,
                        closedServiceType,
                        type,
                        serviceTypes.GetLifetime()
                    )
                    : StatementGeneration.GenerateServiceFactory(
                        compilation,
                        Helpers.GetClosedGenericConversion(compilation, asType, type),
                        type,
                        serviceTypes.GetLifetime()
                    );
                if (statement is null)
                {
                    _ = diagnostics.Add(Diagnostic.Create(Diagnostics.PrivateTypeUnreachableUnderAot, type.Locations.FirstOrDefault(), Helpers.GetGenericDisplayName(type), type.ContainingAssembly.MetadataName));
                }
                else
                {
                    services.Add(statement);
                    reportEntries.Add(new(serviceTypes.GetLifetime(), Helpers.GetGenericDisplayName(closedServiceType), Helpers.GetGenericDisplayName(type)));
                }

                _ = emittedTypes.Add(asType);
            }

            if (emittedTypes.Count == 0 && ( lifetimeRegistrations.Count > 0 || discoveredLifetime is { } ) && !asMatchingInterface)
            {
                foreach (var @interface in Helpers.OrderByFullMetadataName(type.AllInterfaces))
                {
                    if (emittedTypes.Contains(@interface)) continue;

                    var statement = StatementGeneration.GenerateServiceFactory(
                        compilation,
                        @interface,
                        type,
                        lifetimeValue
                    );
                    if (statement is null)
                    {
                        _ = diagnostics.Add(Diagnostic.Create(Diagnostics.PrivateTypeUnreachableUnderAot, type.Locations.FirstOrDefault(), Helpers.GetGenericDisplayName(type), type.ContainingAssembly.MetadataName));
                    }
                    else
                    {
                        services.Add(statement);
                        reportEntries.Add(new(serviceTypes.GetLifetime(), Helpers.GetGenericDisplayName(@interface), Helpers.GetGenericDisplayName(type)));
                    }

                    _ = emittedTypes.Add(@interface);
                }
            }
        }

        var block = Block();
        foreach (var service in services)
        {
            block = block.AddStatements(
                ExpressionStatement(
                    InvocationExpression(
                            MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("services"), IdentifierName("Add"))
                        )
                       .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(service))))
                )
            );
        }

        return new(block, reportEntries.ToImmutable());
    }

    private sealed record ServiceDescriptorGeneration(BlockSyntax Block, ImmutableList<ServiceDescriptorScanReportEntryData> ReportEntries);

    // The service types an attribute registers: a non-empty Type[] passed as its last constructor argument (at typesArgumentIndex),
    // else its generic type arguments, else every interface the implementation type has.
    private static List<INamedTypeSymbol> GetAttributeServiceTypes(AttributeData attributeData, int typesArgumentIndex, INamedTypeSymbol type)
    {
        var serviceTypes = new List<INamedTypeSymbol>();
        var arguments = attributeData.ConstructorArguments;
        if (arguments.Length == typesArgumentIndex + 1
         && arguments[typesArgumentIndex] is { Kind: TypedConstantKind.Array, Values: { Length: > 0 } values })
        {
            foreach (var value in values)
            {
                if (value is { Kind: TypedConstantKind.Type, Value: INamedTypeSymbol serviceType })
                {
                    serviceTypes.Add(serviceType);
                }
            }
        }
        else if (attributeData.AttributeClass is { TypeArguments: { Length: > 0 } typeArguments })
        {
            foreach (var typeArgument in typeArguments)
            {
                if (typeArgument is INamedTypeSymbol serviceType)
                {
                    serviceTypes.Add(serviceType);
                }
            }
        }
        else
        {
            serviceTypes.AddRange(type.AllInterfaces);
        }

        return serviceTypes;
    }

    // The lifetime from the first RegistrationLifetimeAttribute on the service type, if it has one.
    private static string? GetRegistrationLifetime(INamedTypeSymbol serviceType, INamedTypeSymbol registrationLifetimeAttribute)
    {
        foreach (var attribute in serviceType.GetAttributes())
        {
            if (attribute.AttributeClass is { } && SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, registrationLifetimeAttribute))
            {
                return GetLifetimeValue(attribute);
            }
        }

        return null;
    }

    private static string? GetLifetimeValue(AttributeData? attribute) =>
        attribute is { ConstructorArguments: [{ } first, ..] } ? GetLifetimeValue(first) : null;

    private static string? GetLifetimeValue(TypedConstant constant)
    {
        if (constant is not
            {
                Kind: TypedConstantKind.Enum,
                Type: { } discoveredType,
                Value: int discoveredValue,
            })
        {
            return null;
        }

        var value = discoveredType
                   .GetMembers()
                   .OfType<IFieldSymbol>()
                   .First(z => z.ConstantValue is int i && i == discoveredValue);
        return value.Name;
    }

    private static ExpressionSyntax GetKeyExpression(TypedConstant constant) =>
        constant.IsNull
            ? LiteralExpression(SyntaxKind.NullLiteralExpression)
            : constant.Kind switch
            {
                TypedConstantKind.Enum => GetEnumKeyExpression(constant),
                TypedConstantKind.Type => TypeOfExpression(ParseTypeName(Helpers.GetTypeOfName((ISymbol)constant.Value!))),
                TypedConstantKind.Primitive => GetPrimitiveKeyExpression(constant),
                TypedConstantKind.Array or TypedConstantKind.Error or _ => LiteralExpression(SyntaxKind.NullLiteralExpression),
            };

    private static ExpressionSyntax GetEnumKeyExpression(TypedConstant constant)
    {
        var enumType = (INamedTypeSymbol)constant.Type!;
        var member = enumType
                    .GetMembers()
                    .OfType<IFieldSymbol>()
                    .FirstOrDefault(z => z.HasConstantValue && Equals(z.ConstantValue, constant.Value));

        return member is null
            ? CastExpression(ParseTypeName(Helpers.GetTypeOfName(enumType)), LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(Convert.ToInt64(constant.Value, CultureInfo.InvariantCulture))))
            : MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                ParseTypeName(Helpers.GetTypeOfName(enumType)),
                IdentifierName(member.Name)
            );
    }

    [SuppressMessage("Style", "IDE0072:Add missing cases", Justification = "SpecialType has dozens of members; only the ones attribute arguments can carry are meaningful here, the rest fall through to the default.")]
    private static ExpressionSyntax GetPrimitiveKeyExpression(TypedConstant constant)
    {
        var value = constant.Value!;
        return constant.Type?.SpecialType switch
        {
            SpecialType.System_String => LiteralExpression(SyntaxKind.StringLiteralExpression, Literal((string)value)),
            SpecialType.System_Boolean => LiteralExpression((bool)value ? SyntaxKind.TrueLiteralExpression : SyntaxKind.FalseLiteralExpression),
            SpecialType.System_Char => LiteralExpression(SyntaxKind.CharacterLiteralExpression, Literal((char)value)),
            SpecialType.System_Int32 => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((int)value)),
            SpecialType.System_Int64 => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((long)value)),
            SpecialType.System_UInt32 => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((uint)value)),
            SpecialType.System_UInt64 => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((ulong)value)),
            SpecialType.System_Double => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((double)value)),
            SpecialType.System_Single => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((float)value)),
            SpecialType.System_Byte => CastExpression(
                PredefinedType(Token(SyntaxKind.ByteKeyword)),
                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((byte)value))
            ),
            SpecialType.System_SByte => CastExpression(
                PredefinedType(Token(SyntaxKind.SByteKeyword)),
                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((sbyte)value))
            ),
            SpecialType.System_Int16 => CastExpression(
                PredefinedType(Token(SyntaxKind.ShortKeyword)),
                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((short)value))
            ),
            SpecialType.System_UInt16 => CastExpression(
                PredefinedType(Token(SyntaxKind.UShortKeyword)),
                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((ushort)value))
            ),
            _ => LiteralExpression(SyntaxKind.NullLiteralExpression),
        };
    }

    private static (InvocationExpressionSyntax method, ExpressionSyntax selector) GetMethod(SyntaxNode node) =>
        node is InvocationExpressionSyntax
        {
            Expression: MemberAccessExpressionSyntax
            {
                Name.Identifier.Text: "Scan",
            },
            ArgumentList.Arguments: [.., { Expression: { } expression }],
        } invocationExpressionSyntax
            ? (invocationExpressionSyntax, expression)
            : default;

    private static (InvocationExpressionSyntax method, ExpressionSyntax selector, SemanticModel semanticModel) GetServiceDescriptorMethod(GeneratorSyntaxContext context)
    {
        (var method, var selector) = GetMethod(context.Node);
        // Structural detection only; genuine ISpecularProvider validation is deferred to GetServiceDescriptorItems,
        // which runs against the shell-augmented compilation. See SpecularProviderGenerator.CreateSemanticCompilation.
        return method is null || selector is null
            ? default
            : (method, selector, semanticModel: context.SemanticModel);
    }

    private static bool IsValidMethod(SyntaxNode node) => GetMethod(node) is { method: { }, selector: { } };

    private const string IServiceDescriptorAssemblySelector = nameof(IServiceDescriptorAssemblySelector);
}
