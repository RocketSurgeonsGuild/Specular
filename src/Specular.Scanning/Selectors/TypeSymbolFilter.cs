using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using static Specular.Scanning.SelectorArguments;

namespace Specular.Scanning;

/// <summary>Records each <see cref="ITypeSymbolFilter" /> call as one clause, in call order.</summary>
internal sealed class TypeSymbolFilter : ITypeSymbolFilter
{
    private readonly ImmutableArray<AssignableToFilterData>.Builder _assignableTo = ImmutableArray.CreateBuilder<AssignableToFilterData>();
    private readonly ImmutableArray<AttributeFilterData>.Builder _attributes = ImmutableArray.CreateBuilder<AttributeFilterData>();
    private readonly ImmutableArray<AttributeNameFilterData>.Builder _attributeNames = ImmutableArray.CreateBuilder<AttributeNameFilterData>();
    private readonly ImmutableArray<NamespaceFilterData>.Builder _namespaces = ImmutableArray.CreateBuilder<NamespaceFilterData>();
    private readonly ImmutableArray<NameFilterData>.Builder _names = ImmutableArray.CreateBuilder<NameFilterData>();
    private readonly ImmutableArray<TypeKindFilterData>.Builder _typeKinds = ImmutableArray.CreateBuilder<TypeKindFilterData>();
    private readonly ImmutableArray<TypeInfoFilterData>.Builder _typeInfos = ImmutableArray.CreateBuilder<TypeInfoFilterData>();
    private readonly ImmutableArray<CustomTypeFilter>.Builder _customFilters = ImmutableArray.CreateBuilder<CustomTypeFilter>();
    private bool _completed;

    public ITypeSymbolFilter AssignableTo(ITypeSymbol type) => AddAssignableTo(true, [ToTypeReference(type, nameof(type))]);

    public ITypeSymbolFilter AssignableTo(string metadataName) => AddAssignableTo(true, [ToTypeReference(metadataName, nameof(metadataName))]);

    public ITypeSymbolFilter AssignableToAny(ITypeSymbol type, params ITypeSymbol[] types) =>
        AddAssignableTo(true, Convert(type, types, nameof(type), nameof(types), ToTypeReference));

    public ITypeSymbolFilter NotAssignableTo(ITypeSymbol type) => AddAssignableTo(false, [ToTypeReference(type, nameof(type))]);

    public ITypeSymbolFilter NotAssignableTo(string metadataName) => AddAssignableTo(false, [ToTypeReference(metadataName, nameof(metadataName))]);

    public ITypeSymbolFilter NotAssignableToAny(ITypeSymbol type, params ITypeSymbol[] types) =>
        AddAssignableTo(false, Convert(type, types, nameof(type), nameof(types), ToTypeReference));

    public ITypeSymbolFilter EndsWith(string value, params string[] values) =>
        AddName(true, TextDirectionFilter.EndsWith, Strings(value, values, nameof(value), nameof(values)));

    public ITypeSymbolFilter NotEndsWith(string value, params string[] values) =>
        AddName(false, TextDirectionFilter.EndsWith, Strings(value, values, nameof(value), nameof(values)));

    public ITypeSymbolFilter StartsWith(string value, params string[] values) =>
        AddName(true, TextDirectionFilter.StartsWith, Strings(value, values, nameof(value), nameof(values)));

    public ITypeSymbolFilter NotStartsWith(string value, params string[] values) =>
        AddName(false, TextDirectionFilter.StartsWith, Strings(value, values, nameof(value), nameof(values)));

    public ITypeSymbolFilter Contains(string value, params string[] values) =>
        AddName(true, TextDirectionFilter.Contains, Strings(value, values, nameof(value), nameof(values)));

    public ITypeSymbolFilter NotContains(string value, params string[] values) =>
        AddName(false, TextDirectionFilter.Contains, Strings(value, values, nameof(value), nameof(values)));

    public ITypeSymbolFilter InExactNamespaceOf(ITypeSymbol type, params ITypeSymbol[] types) =>
        AddNamespace(NamespaceFilter.Exact, Convert(type, types, nameof(type), nameof(types), Namespace));

    public ITypeSymbolFilter InExactNamespaces(string first, params string[] namespaces) =>
        AddNamespace(NamespaceFilter.Exact, Strings(first, namespaces, nameof(first), nameof(namespaces)));

    public ITypeSymbolFilter InNamespaceOf(ITypeSymbol type, params ITypeSymbol[] types) =>
        AddNamespace(NamespaceFilter.In, Convert(type, types, nameof(type), nameof(types), Namespace));

    public ITypeSymbolFilter InNamespaces(string first, params string[] namespaces) =>
        AddNamespace(NamespaceFilter.In, Strings(first, namespaces, nameof(first), nameof(namespaces)));

    public ITypeSymbolFilter NotInNamespaceOf(ITypeSymbol type, params ITypeSymbol[] types) =>
        AddNamespace(NamespaceFilter.NotIn, Convert(type, types, nameof(type), nameof(types), Namespace));

    public ITypeSymbolFilter NotInNamespaces(string first, params string[] namespaces) =>
        AddNamespace(NamespaceFilter.NotIn, Strings(first, namespaces, nameof(first), nameof(namespaces)));

    public ITypeSymbolFilter WithAttribute(ITypeSymbol attributeType) =>
        AddAttribute(true, false, [ToTypeReference(attributeType, nameof(attributeType))]);

    public ITypeSymbolFilter WithAttribute(string attributeFullName) =>
        AddAttributeName(true, [NotNullOrEmpty(attributeFullName, nameof(attributeFullName))]);

    public ITypeSymbolFilter WithAnyAttribute(ITypeSymbol attributeType, params ITypeSymbol[] attributeTypes) =>
        AddAttribute(true, true, Convert(attributeType, attributeTypes, nameof(attributeType), nameof(attributeTypes), ToTypeReference));

    public ITypeSymbolFilter WithAnyAttribute(string attributeFullName, params string[] attributeFullNames) =>
        AddAttributeName(true, Strings(attributeFullName, attributeFullNames, nameof(attributeFullName), nameof(attributeFullNames)));

    public ITypeSymbolFilter WithoutAttribute(ITypeSymbol attributeType) =>
        AddAttribute(false, false, [ToTypeReference(attributeType, nameof(attributeType))]);

    public ITypeSymbolFilter WithoutAttribute(string attributeFullName) =>
        AddAttributeName(false, [NotNullOrEmpty(attributeFullName, nameof(attributeFullName))]);

    public ITypeSymbolFilter KindOf(TypeKind typeKind, params TypeKind[] typeKinds) =>
        Add(_typeKinds, new(true, Values(typeKind, typeKinds, nameof(typeKind), nameof(typeKinds))));

    public ITypeSymbolFilter NotKindOf(TypeKind typeKind, params TypeKind[] typeKinds) =>
        Add(_typeKinds, new(false, Values(typeKind, typeKinds, nameof(typeKind), nameof(typeKinds))));

    public ITypeSymbolFilter InfoOf(TypeInfoFilter typeInfoFilter, params TypeInfoFilter[] typeInfoFilters) =>
        Add(_typeInfos, new(true, Values(typeInfoFilter, typeInfoFilters, nameof(typeInfoFilter), nameof(typeInfoFilters))));

    public ITypeSymbolFilter NotInfoOf(TypeInfoFilter typeInfoFilter, params TypeInfoFilter[] typeInfoFilters) =>
        Add(_typeInfos, new(false, Values(typeInfoFilter, typeInfoFilters, nameof(typeInfoFilter), nameof(typeInfoFilters))));

    public ITypeSymbolFilter Where(CustomTypeFilter filter)
    {
        ThrowIfNull(filter, nameof(filter));
        return Add(_customFilters, filter);
    }

    /// <summary>Snapshots the clauses; later calls throw so a filter captured past <c>GetTypes</c> cannot silently do nothing.</summary>
    public TypeFilterData Complete(bool publicOnly)
    {
        _completed = true;
        return new(
            publicOnly,
            _assignableTo.ToImmutable(),
            _attributes.ToImmutable(),
            _attributeNames.ToImmutable(),
            _namespaces.ToImmutable(),
            _names.ToImmutable(),
            _typeKinds.ToImmutable(),
            _typeInfos.ToImmutable(),
            _customFilters.ToImmutable()
        );
    }

    private ITypeSymbolFilter AddAssignableTo(bool include, ImmutableArray<TypeReference> types) => Add(_assignableTo, new(include, types));

    private ITypeSymbolFilter AddAttribute(bool include, bool any, ImmutableArray<TypeReference> attributes) =>
        Add(_attributes, new(include, any, attributes));

    private ITypeSymbolFilter AddAttributeName(bool include, ImmutableArray<string> attributeNames) =>
        Add(_attributeNames, new(include, attributeNames));

    private ITypeSymbolFilter AddNamespace(NamespaceFilter filter, ImmutableArray<string> namespaces) => Add(_namespaces, new(filter, namespaces));

    private ITypeSymbolFilter AddName(bool include, TextDirectionFilter filter, ImmutableArray<string> names) => Add(_names, new(include, filter, names));

    private TypeSymbolFilter Add<T>(ImmutableArray<T>.Builder clauses, T clause)
    {
        if (_completed) throw new InvalidOperationException("The type filter can only be used inside the GetTypes callback.");
        clauses.Add(clause);
        return this;
    }
}
