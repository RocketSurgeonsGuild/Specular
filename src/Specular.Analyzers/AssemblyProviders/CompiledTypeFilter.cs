using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Specular.Analyzers.Descriptors;

namespace Specular.Analyzers.AssemblyProviders;

internal sealed class CompiledTypeFilter(ClassFilter classFilter, ImmutableList<ITypeFilterDescriptor> typeFilterDescriptors, SourceLocation? sourceLocation = null) : ICompiledTypeFilter<INamedTypeSymbol>
{
    public ImmutableList<ITypeFilterDescriptor> TypeFilterDescriptors { get; } = typeFilterDescriptors;
    public ClassFilter ClassFilter { get; } = classFilter;
    public bool Aborted { get; } = typeFilterDescriptors.OfType<CompiledAbortTypeFilterDescriptor>().Any();
    public string Hash { get; } = sourceLocation?.ExpressionHash ?? Guid.NewGuid().ToString("N");

    public bool IsMatch(Compilation compilation, INamedTypeSymbol targetSymbol)
    {
        if (Aborted) return false;
        if (ClassFilter == ClassFilter.PublicOnly && targetSymbol.DeclaredAccessibility != Accessibility.Public)
        {
            return false;
        }

        return  TypeFilterDescriptors.Count == 0  ?  true  :  TypeFilterDescriptors.All(GetFilterDescriptor);
        bool GetFilterDescriptor(ITypeFilterDescriptor filterDescriptor)
        {
            return filterDescriptor switch
            {
                AssignableToTypeFilterDescriptor { Type: var assignableToType } =>
                    Helpers.HasImplicitGenericConversion(compilation, assignableToType, targetSymbol),
                NotAssignableToTypeFilterDescriptor { Type: var notAssignableToType } =>
                    !Helpers.HasImplicitGenericConversion(compilation, notAssignableToType, targetSymbol),
                AssignableToAnyTypeFilterDescriptor { Types: var assignableToAnyTypes } =>
                    assignableToAnyTypes.Any(z => Helpers.HasImplicitGenericConversion(compilation, z, targetSymbol)),
                NotAssignableToAnyTypeFilterDescriptor { Types: var notAssignableToAnyTypes } =>
                    notAssignableToAnyTypes.All(z => !Helpers.HasImplicitGenericConversion(compilation, z, targetSymbol)),
                WithAttributeFilterDescriptor { Attribute: var attribute } =>
                    targetSymbol.GetAttributes().Any(z => SymbolEqualityComparer.Default.Equals(z.AttributeClass, attribute)),
                WithAnyAttributeFilterDescriptor { Attributes: var attributes } =>
                    handleWithAnyAttributeFilter(attributes, targetSymbol),
                WithoutAttributeFilterDescriptor { Attribute: var attribute } =>
                    targetSymbol.GetAttributes().All(z => !SymbolEqualityComparer.Default.Equals(z.AttributeClass, attribute)),
                WithAttributeStringFilterDescriptor { AttributeClassName: var attribute } =>
                    targetSymbol.GetAttributes().Any(z => Helpers.GetFullMetadataName(z.AttributeClass) == attribute),
                WithAnyAttributeStringFilterDescriptor { AttributeClassNames: var attributes } =>
                    targetSymbol.GetAttributes().Join(attributes, z => Helpers.GetFullMetadataName(z.AttributeClass), z => z, (_, _) => true).Any(),
                WithoutAttributeStringFilterDescriptor { AttributeClassName: var attribute } =>
                    targetSymbol.GetAttributes().All(z => Helpers.GetFullMetadataName(z.AttributeClass) != attribute),
                NamespaceFilterDescriptor { Filter: var filterName, Namespaces: var filterNamespaces } =>
                    handleNamespaceFilter(filterName, filterNamespaces, targetSymbol),
                NameFilterDescriptor { Include: var include, Filter: var filterName, Names: var filterNames } =>
                    handleNameFilter(include, filterName, filterNames, targetSymbol),
                TypeKindFilterDescriptor { Include: var include, TypeKinds: var typeKinds } =>
                    handleKindFilter(include, typeKinds, targetSymbol),
                TypeInfoFilterDescriptor { Include: var include, TypeInfos: var typeInfos } =>
                    handleInfoFilter(include, typeInfos, targetSymbol),
                _ => throw new NotSupportedException(filterDescriptor.GetType().FullName),
            };
        }

        static bool handleWithAnyAttributeFilter(ImmutableHashSet<INamedTypeSymbol> attributes, INamedTypeSymbol targetType)
        {
            var targetAttributes = targetType.GetAttributes();
            foreach (var target in targetAttributes)
            {
                foreach (var attribute in attributes)
                {
                    if (attribute.Name == target.AttributeClass?.Name
                     && SymbolEqualityComparer.Default.Equals(attribute.ContainingNamespace, target.AttributeClass?.ContainingNamespace))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static bool handleNamespaceFilter(NamespaceFilter filterName, ImmutableHashSet<string> filterNamespaces, INamedTypeSymbol type)
        {
            var ns = type.ContainingNamespace.ToDisplayString();
            return filterName switch
            {
                NamespaceFilter.Exact => filterNamespaces.Contains(ns),
                NamespaceFilter.In => filterNamespaces.Any(n => ns.StartsWith(n, StringComparison.Ordinal)),
                NamespaceFilter.NotIn => filterNamespaces.All(n => !ns.StartsWith(n, StringComparison.Ordinal)),
                _ => throw new NotImplementedException(),
            };
        }

        static bool handleNameFilter(bool include, TextDirectionFilter filterName, ImmutableHashSet<string> filterNames, INamedTypeSymbol type)
        {
            return (include, filterName) switch
            {
                (true, TextDirectionFilter.Contains) => filterNames.Any(name => type.Name.Contains(name)),
                (false, TextDirectionFilter.Contains) => !filterNames.Any(name => type.Name.Contains(name)),
                (true, TextDirectionFilter.EndsWith) => filterNames.Any(name => type.Name.EndsWith(name, StringComparison.Ordinal)),
                (false, TextDirectionFilter.EndsWith) => !filterNames.Any(name => type.Name.EndsWith(name, StringComparison.Ordinal)),
                (true, TextDirectionFilter.StartsWith) => filterNames.Any(name => type.Name.StartsWith(name, StringComparison.Ordinal)),
                (false, TextDirectionFilter.StartsWith) => !filterNames.Any(name => type.Name.StartsWith(name, StringComparison.Ordinal)),
                _ => throw new NotImplementedException(),
            };
        }

        static bool handleKindFilter(bool include, ImmutableHashSet<TypeKind> typeKinds, INamedTypeSymbol type) => include switch { true => typeKinds.Any(kind => type.TypeKind == kind), false => typeKinds.All(kind => type.TypeKind != kind) };

        static bool handleInfoFilter(bool include, ImmutableHashSet<TypeInfoFilter> typeKinds, INamedTypeSymbol type)
        {
            return include switch
            {
                true => typeKinds.Any(infoFilter => TypeInfoFilterFunc(infoFilter, type)),
                false => typeKinds.All(infoFilter => !TypeInfoFilterFunc(infoFilter, type)),
            };
        }

        static bool TypeInfoFilterFunc(TypeInfoFilter typeFilter, INamedTypeSymbol type)
        {
            return typeFilter switch
            {
                TypeInfoFilter.Abstract => type.IsAbstract,
                TypeInfoFilter.GenericType => type.IsGenericType,
                //                TypeInfoFilter.GenericTypeDefinition => type is { IsGenericType: true, IsUnboundGenericType: true },
                TypeInfoFilter.Sealed => type.IsSealed,
                TypeInfoFilter.Visible => type.DeclaredAccessibility == Accessibility.Public,
                TypeInfoFilter.ValueType => type.IsValueType,
                TypeInfoFilter.Static => type.IsStatic,
                //                TypeInfoFilter.Nested                => type.ContainingType is {},
                TypeInfoFilter.Unknown => throw new NotSupportedException(typeFilter.ToString()),
                _ => throw new NotSupportedException(typeFilter.ToString()),
            };
        }
    }
}
