using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     <see cref="TypeFilterData" /> applied to types with the per-clause rules of Specular's <c>CompiledTypeFilter</c>: public-only
///     first, then every clause, then custom filters. References that do not resolve are dropped from their clause, and a clause left
///     with no types is dropped, as Specular's cache reload does.
/// </summary>
internal sealed class TypeMatcher
{
    private readonly Compilation _compilation;
    private readonly TypeFilterData _filter;
    private readonly ImmutableArray<(bool Include, ImmutableArray<INamedTypeSymbol> Types)> _assignableTo;
    private readonly ImmutableArray<(bool Include, bool Any, ImmutableArray<INamedTypeSymbol> Attributes)> _attributes;
    private readonly ImmutableArray<(bool Include, HashSet<string> Names)> _attributeNames;

    public TypeMatcher(Compilation compilation, TypeFilterData filter, TypeReferenceResolver resolver)
    {
        _compilation = compilation;
        _filter = filter;

        var assignableTo = ImmutableArray.CreateBuilder<(bool, ImmutableArray<INamedTypeSymbol>)>();
        foreach (var clause in filter.AssignableTo)
        {
            if (ResolveAll(resolver, clause.Types) is { Length: > 0 } types) assignableTo.Add((clause.Include, types));
        }

        _assignableTo = assignableTo.ToImmutable();

        var attributes = ImmutableArray.CreateBuilder<(bool, bool, ImmutableArray<INamedTypeSymbol>)>();
        foreach (var clause in filter.Attributes)
        {
            if (ResolveAll(resolver, clause.Attributes) is { Length: > 0 } types) attributes.Add((clause.Include, clause.Any, types));
        }

        _attributes = attributes.ToImmutable();

        var attributeNames = ImmutableArray.CreateBuilder<(bool, HashSet<string>)>();
        foreach (var clause in filter.AttributeNames)
        {
            attributeNames.Add((clause.Include, new(clause.AttributeNames, StringComparer.Ordinal)));
        }

        _attributeNames = attributeNames.ToImmutable();
    }

    public bool IsMatch(INamedTypeSymbol type)
    {
        if (_filter.PublicOnly && type.DeclaredAccessibility != Accessibility.Public) return false;

        foreach (var clause in _filter.TypeKinds)
        {
            if (clause.Include != clause.TypeKinds.Contains(type.TypeKind)) return false;
        }

        foreach (var clause in _filter.TypeInfos)
        {
            if (clause.Include != HasAnyInfo(clause.TypeInfos, type)) return false;
        }

        foreach (var clause in _filter.Names)
        {
            if (clause.Include != NameMatchesAny(clause, type.Name)) return false;
        }

        if (_filter.Namespaces.Count > 0)
        {
            var @namespace = type.ContainingNamespace.ToDisplayString();
            foreach (var clause in _filter.Namespaces)
            {
                if (!NamespaceMatches(clause, @namespace)) return false;
            }
        }

        if (_attributeNames.Length > 0 || _attributes.Length > 0)
        {
            var typeAttributes = type.GetAttributes();
            if (!AttributeNamesMatch(typeAttributes) || !AttributesMatch(typeAttributes)) return false;
        }

        foreach (var (include, types) in _assignableTo)
        {
            if (include != types.Any(target => ScanAssignability.HasImplicitGenericConversion(_compilation, target, type))) return false;
        }

        foreach (var custom in _filter.CustomFilters)
        {
            if (!custom.IsMatch(_compilation, type)) return false;
        }

        return true;
    }

    private static ImmutableArray<INamedTypeSymbol> ResolveAll(TypeReferenceResolver resolver, EquatableArray<TypeReference> references)
    {
        var types = ImmutableArray.CreateBuilder<INamedTypeSymbol>(references.Count);
        foreach (var reference in references)
        {
            if (resolver.Resolve(reference) is { } type) types.Add(type);
        }

        return types.ToImmutable();
    }

    private bool AttributeNamesMatch(ImmutableArray<AttributeData> typeAttributes)
    {
        if (_attributeNames.Length == 0) return true;

        var names = typeAttributes.Select(static z => ScanSymbolNames.GetFullMetadataName(z.AttributeClass)).ToList();
        foreach (var (include, attributeNames) in _attributeNames)
        {
            if (include != names.Exists(attributeNames.Contains)) return false;
        }

        return true;
    }

    private bool AttributesMatch(ImmutableArray<AttributeData> typeAttributes)
    {
        foreach (var (include, any, attributes) in _attributes)
        {
            var found = any
                ? typeAttributes.Any(applied => attributes.Any(attribute => SameNameAndNamespace(attribute, applied.AttributeClass)))
                : typeAttributes.Any(applied => attributes.Any(attribute => SymbolEqualityComparer.Default.Equals(applied.AttributeClass, attribute)));
            if (include != found) return false;
        }

        return true;
    }

    // WithAnyAttribute's comparison in Specular: an unbound generic attribute matches every closed use of it.
    private static bool SameNameAndNamespace(INamedTypeSymbol attribute, INamedTypeSymbol? applied) =>
        applied is { }
     && string.Equals(attribute.Name, applied.Name, StringComparison.Ordinal)
     && SymbolEqualityComparer.Default.Equals(attribute.ContainingNamespace, applied.ContainingNamespace);

    private static bool NamespaceMatches(NamespaceFilterData clause, string @namespace) => clause.Filter switch
    {
        NamespaceFilter.Exact => clause.Namespaces.Contains(@namespace),
        NamespaceFilter.In => clause.Namespaces.Any(n => @namespace.StartsWith(n, StringComparison.Ordinal)),
        NamespaceFilter.NotIn => clause.Namespaces.All(n => !@namespace.StartsWith(n, StringComparison.Ordinal)),
        // Specular throws on an unknown value; a scan must not, so the clause just never matches.
        _ => false,
    };

    private static bool HasAnyInfo(EquatableArray<TypeInfoFilter> infos, INamedTypeSymbol type)
    {
        foreach (var info in infos.AsImmutableArray())
        {
            if (HasInfo(info, type)) return true;
        }

        return false;
    }

    private static bool NameMatchesAny(NameFilterData clause, string typeName)
    {
        foreach (var value in clause.Names.AsImmutableArray())
        {
            if (NameMatches(clause.Filter, typeName, value)) return true;
        }

        return false;
    }

    private static bool NameMatches(TextDirectionFilter filter, string typeName, string value) => filter switch
    {
        TextDirectionFilter.StartsWith => typeName.StartsWith(value, StringComparison.Ordinal),
        TextDirectionFilter.EndsWith => typeName.EndsWith(value, StringComparison.Ordinal),
        TextDirectionFilter.Contains => typeName.Contains(value, StringComparison.Ordinal),
        _ => false,
    };

    private static bool HasInfo(TypeInfoFilter info, INamedTypeSymbol type) => info switch
    {
        TypeInfoFilter.Abstract => type.IsAbstract,
        TypeInfoFilter.GenericType => type.IsGenericType,
        TypeInfoFilter.Sealed => type.IsSealed,
        TypeInfoFilter.Visible => type.DeclaredAccessibility == Accessibility.Public,
        TypeInfoFilter.ValueType => type.IsValueType,
        TypeInfoFilter.Static => type.IsStatic,
        // Specular throws on Unknown; a scan must not, so it simply never matches.
        TypeInfoFilter.Unknown => false,
        _ => false,
    };
}
