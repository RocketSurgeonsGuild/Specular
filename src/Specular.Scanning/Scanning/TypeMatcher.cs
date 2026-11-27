#if SPECULAR_SCANNING_EXPLICIT_USINGS
using System;
using System.Collections.Generic;
using System.Linq;
#endif
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     <see cref="TypeFilterData" /> applied to types with the per-clause rules of Specular's <c>CompiledTypeFilter</c>: public-only
///     first, then every clause, then custom filters. A reference that does not resolve stands for a type nothing is assignable to and
///     no type carries: an include clause keeps the references that did resolve (so one with none left matches nothing) and an exclude
///     clause excludes nothing for it. Unlike Specular's cache reload, a clause is never dropped, so a typo cannot widen a scan to every
///     type; the references are reported instead.
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

        var unresolved = new UnresolvedCollector();
        var assignableTo = ImmutableArray.CreateBuilder<(bool, ImmutableArray<INamedTypeSymbol>)>(filter.AssignableTo.Count);
        foreach (var clause in filter.AssignableTo)
        {
            var types = ResolveAll(resolver, clause.Types, unresolved);
            assignableTo.Add((clause.Include, types));
            MatchesNothing |= clause.Include && types.IsEmpty;
        }

        _assignableTo = assignableTo.MoveToImmutable();

        var attributes = ImmutableArray.CreateBuilder<(bool, bool, ImmutableArray<INamedTypeSymbol>)>(filter.Attributes.Count);
        foreach (var clause in filter.Attributes)
        {
            var types = ResolveAll(resolver, clause.Attributes, unresolved);
            attributes.Add((clause.Include, clause.Any, types));
            MatchesNothing |= clause.Include && types.IsEmpty;
        }

        _attributes = attributes.MoveToImmutable();
        UnresolvedTypes = unresolved.ToImmutable();

        var attributeNames = ImmutableArray.CreateBuilder<(bool, HashSet<string>)>();
        foreach (var clause in filter.AttributeNames)
        {
            attributeNames.Add((clause.Include, new(clause.AttributeNames, StringComparer.Ordinal)));
        }

        _attributeNames = attributeNames.ToImmutable();
    }

    /// <summary>Each distinct reference that did not resolve, in the order it first appears: assignability clauses, then attribute clauses.</summary>
    public ImmutableArray<TypeReference> UnresolvedTypes { get; }

    /// <summary>Whether an include clause was left with no types, so no type can match and walking an assembly is pointless.</summary>
    public bool MatchesNothing { get; }

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
            var @namespace = ScanSymbolNames.GetNamespaceName(type.ContainingNamespace);
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

    private static ImmutableArray<INamedTypeSymbol> ResolveAll(TypeReferenceResolver resolver, EquatableArray<TypeReference> references, UnresolvedCollector unresolved)
    {
        var types = ImmutableArray.CreateBuilder<INamedTypeSymbol>(references.Count);
        foreach (var reference in references)
        {
            if (resolver.Resolve(reference) is { } type)
            {
                types.Add(type);
            }
            else
            {
                unresolved.Add(reference);
            }
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
                ? typeAttributes.Any(applied => attributes.Any(attribute => IsClosedGeneric(attribute) ? SameType(attribute, applied.AttributeClass) : SameNameAndNamespace(attribute, applied.AttributeClass)))
                : typeAttributes.Any(applied => attributes.Any(attribute => SameType(attribute, applied.AttributeClass)));
            if (include != found) return false;
        }

        return true;
    }

    private static bool SameType(INamedTypeSymbol attribute, INamedTypeSymbol? applied) => SymbolEqualityComparer.Default.Equals(applied, attribute);

    // Only a reference with type arguments resolves to a constructed type; those compare exactly, even under WithAnyAttribute.
    private static bool IsClosedGeneric(INamedTypeSymbol attribute) => !attribute.IsDefinition && !attribute.IsUnboundGenericType;

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

    /// <summary>Collects unresolved references once each, keeping first-seen order so equal requests report equal lists.</summary>
    private sealed class UnresolvedCollector
    {
        private readonly HashSet<TypeReference> _seen = [];
        private readonly ImmutableArray<TypeReference>.Builder _ordered = ImmutableArray.CreateBuilder<TypeReference>();

        public void Add(TypeReference reference)
        {
            if (_seen.Add(reference)) _ordered.Add(reference);
        }

        public ImmutableArray<TypeReference> ToImmutable() => _ordered.ToImmutable();
    }
}
