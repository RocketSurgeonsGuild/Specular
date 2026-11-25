using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Specular.Analyzers.Configuration;
using Specular.Analyzers.Descriptors;
using Specular.Scanning;
using NameFilterData = Specular.Analyzers.Configuration.NameFilterData;
using NamespaceFilterData = Specular.Analyzers.Configuration.NamespaceFilterData;
using ScanNameFilter = Specular.Scanning.NameFilterData;
using ScanNamespaceFilter = Specular.Scanning.NamespaceFilterData;
using ScanTypeFilter = Specular.Scanning.TypeFilterData;
using ScanTypeInfoFilter = Specular.Scanning.TypeInfoFilterData;
using ScanTypeKindFilter = Specular.Scanning.TypeKindFilterData;
using TypeInfoFilterData = Specular.Analyzers.Configuration.TypeInfoFilterData;
using TypeKindFilterData = Specular.Analyzers.Configuration.TypeKindFilterData;
using WireTypeFilter = Specular.Analyzers.Configuration.TypeFilterData;

namespace Specular.Analyzers.AssemblyProviders;

/// <summary>
///     The type half of a selector: <see cref="Data" /> is what Specular writes into <c>[AssemblyMetadata]</c> for other assemblies,
///     <see cref="Filter" /> is what Specular.Scanning matches types with.
/// </summary>
internal sealed class TypeSelection
{
    private readonly Compilation _compilation;
    private readonly TypeReferenceResolver _resolver;
    private TypeMatcher? _matcher;

    private TypeSelection(Compilation compilation, TypeReferenceResolver resolver, WireTypeFilter data, ScanTypeFilter filter)
    {
        _compilation = compilation;
        _resolver = resolver;
        Data = data;
        Filter = filter;
    }

    public WireTypeFilter Data { get; }
    public ScanTypeFilter Filter { get; }

    public bool IsMatch(INamedTypeSymbol type) => GetMatcher().IsMatch(type);

    // Built on first use: a run that finds every result in the cache never resolves the filter's types.
    public TypeMatcher GetMatcher() => _matcher ??= new(_compilation, Filter, _resolver);

    /// <summary>
    ///     A selection read from a referenced assembly's <c>[AssemblyMetadata]</c>. Its types resolve as Specular has always resolved
    ///     them there: a public type in the named assembly when this run can see it, otherwise through the compilation.
    /// </summary>
    public static TypeSelection Load(Compilation compilation, WireTypeFilter data, IReadOnlyDictionary<string, IAssemblySymbol> visibleAssemblies) =>
        new(compilation, new(compilation, visibleAssemblies), data, ToFilter(data));

    internal static TypeSelection Create(Compilation compilation, TypeReferenceResolver resolver, WireTypeFilter data, ScanTypeFilter filter) =>
        new(compilation, resolver, data, filter);

    /// <summary>
    ///     A type reference for a type named in <c>[AssemblyMetadata]</c>. Specular writes types nested in a generic type with the
    ///     containing type's display name (<c>Ns.Outer&lt;T&gt;+Inner</c>); metadata lookups need its arity (<c>Ns.Outer`1+Inner</c>).
    /// </summary>
    public static TypeReference ToTypeReference(string assemblyName, string typeName, bool unboundGeneric) =>
        new(assemblyName, GenericDisplayName.Replace(typeName, static match => "`" + ( match.Value.Count(static c => c == ',') + 1 )), unboundGeneric);

    private static ScanTypeFilter ToFilter(WireTypeFilter data)
    {
        var assignableTo = ImmutableArray.CreateBuilder<AssignableToFilterData>();
        foreach (var item in orEmpty(data.AssignableToTypeFilters))
        {
            assignableTo.Add(new(item.Include, ImmutableArray.Create(ToTypeReference(item.Assembly, item.Type, item.UnboundGenericType))));
        }

        foreach (var item in orEmpty(data.AssignableToAnyTypeFilters))
        {
            assignableTo.Add(
                new(item.Include, orEmpty(item.Types).Select(static z => ToTypeReference(z.Assembly, z.Type, z.UnboundGenericType)).ToImmutableArray())
            );
        }

        var attributes = ImmutableArray.CreateBuilder<AttributeFilterData>();
        foreach (var item in orEmpty(data.WithAttributeFilters))
        {
            attributes.Add(new(item.Include, false, ImmutableArray.Create(ToTypeReference(item.Assembly, item.Attribute, item.UnboundGenericType))));
        }

        // Every WithAnyAttribute of the selector was written into one list, and has always been read back as one clause.
        if (orEmpty(data.WithAnyAttributeFilters) is { Length: > 0 } anyAttributes)
        {
            attributes.Add(new(true, true, anyAttributes.Select(static z => ToTypeReference(z.Assembly, z.Attribute, z.UnboundGenericType)).ToImmutableArray()));
        }

        var attributeNames = ImmutableArray.CreateBuilder<AttributeNameFilterData>();
        foreach (var item in orEmpty(data.WithAttributeStringFilters))
        {
            attributeNames.Add(new(item.Include, ImmutableArray.Create(item.Attribute)));
        }

        if (orEmpty(data.WithAnyAttributeStringFilters) is { Length: > 0 } anyAttributeNames)
        {
            attributeNames.Add(new(true, anyAttributeNames.Select(static z => z.Attribute).ToImmutableArray()));
        }

        return new(
            data.Filter == ClassFilter.PublicOnly,
            assignableTo.ToImmutable(),
            attributes.ToImmutable(),
            attributeNames.ToImmutable(),
            orEmpty(data.NamespaceFilters).Select(static z => new ScanNamespaceFilter(z.Filter, orEmpty(z.Namespaces))).ToImmutableArray(),
            orEmpty(data.NameFilters).Select(static z => new ScanNameFilter(z.Include, z.Filter, orEmpty(z.Names))).ToImmutableArray(),
            orEmpty(data.TypeKindFilters).Select(static z => new ScanTypeKindFilter(z.Include, orEmpty(z.TypeKinds))).ToImmutableArray(),
            orEmpty(data.TypeInfoFilters).Select(static z => new ScanTypeInfoFilter(z.Include, orEmpty(z.TypeInfos))).ToImmutableArray(),
            EquatableArray<CustomTypeFilter>.Empty
        );

        static ImmutableArray<T> orEmpty<T>(ImmutableArray<T> items) => items.IsDefault ? [] : items;
    }

    private static readonly Regex GenericDisplayName = new("<[^<>]*>", RegexOptions.Compiled);
}

/// <summary>
///     Collects the type clauses of a selector written in source. The <c>[AssemblyMetadata]</c> payload keeps the exact shape and order
///     Specular has always written; the Specular.Scanning filter keeps the clauses in call order.
/// </summary>
internal sealed class TypeSelectionBuilder(ScanEnvironment environment)
{
    private readonly List<(NamespaceFilter Filter, ImmutableHashSet<string> Namespaces)> _namespaces = [];
    private readonly List<(bool Include, TextDirectionFilter Filter, ImmutableHashSet<string> Names)> _names = [];
    private readonly List<(bool Include, ImmutableHashSet<TypeKind> TypeKinds)> _typeKinds = [];
    private readonly List<(bool Include, ImmutableHashSet<TypeInfoFilter> TypeInfos)> _typeInfos = [];
    private readonly List<(bool Include, INamedTypeSymbol Attribute)> _withAttributes = [];
    private readonly List<(bool Include, string Attribute)> _withAttributeNames = [];
    private readonly List<ImmutableHashSet<INamedTypeSymbol>> _withAnyAttributes = [];
    private readonly List<ImmutableHashSet<string>> _withAnyAttributeNames = [];
    private readonly List<(bool Include, INamedTypeSymbol Type)> _assignableTo = [];
    private readonly List<(bool Include, ImmutableHashSet<INamedTypeSymbol> Types)> _assignableToAny = [];

    private readonly ImmutableArray<AssignableToFilterData>.Builder _scanAssignableTo = ImmutableArray.CreateBuilder<AssignableToFilterData>();
    private readonly ImmutableArray<AttributeFilterData>.Builder _scanAttributes = ImmutableArray.CreateBuilder<AttributeFilterData>();
    private readonly ImmutableArray<AttributeNameFilterData>.Builder _scanAttributeNames = ImmutableArray.CreateBuilder<AttributeNameFilterData>();
    private readonly ImmutableArray<ScanNamespaceFilter>.Builder _scanNamespaces = ImmutableArray.CreateBuilder<ScanNamespaceFilter>();
    private readonly ImmutableArray<ScanNameFilter>.Builder _scanNames = ImmutableArray.CreateBuilder<ScanNameFilter>();
    private readonly ImmutableArray<ScanTypeKindFilter>.Builder _scanTypeKinds = ImmutableArray.CreateBuilder<ScanTypeKindFilter>();
    private readonly ImmutableArray<ScanTypeInfoFilter>.Builder _scanTypeInfos = ImmutableArray.CreateBuilder<ScanTypeInfoFilter>();
    private readonly ImmutableArray<CustomTypeFilter>.Builder _scanCustomFilters = ImmutableArray.CreateBuilder<CustomTypeFilter>();

    public ClassFilter ClassFilter { get; set; } = ClassFilter.All;

    public void AssignableTo(bool include, INamedTypeSymbol type)
    {
        _assignableTo.Add((include, type));
        AddTypes(ExactTypeFilterKind.AssignableTo, include, [type]);
    }

    public void AssignableToAny(bool include, ImmutableHashSet<INamedTypeSymbol> types)
    {
        _assignableToAny.Add((include, types));
        AddTypes(ExactTypeFilterKind.AssignableTo, include, types);
    }

    public void WithAttribute(bool include, INamedTypeSymbol attribute)
    {
        _withAttributes.Add((include, attribute));
        AddTypes(ExactTypeFilterKind.WithAttribute, include, [attribute]);
    }

    public void WithAnyAttribute(ImmutableHashSet<INamedTypeSymbol> attributes)
    {
        _withAnyAttributes.Add(attributes);
        AddTypes(ExactTypeFilterKind.WithAnyAttribute, true, attributes);
    }

    public void WithAttribute(bool include, string attributeFullName)
    {
        _withAttributeNames.Add((include, attributeFullName));
        _scanAttributeNames.Add(new(include, ImmutableArray.Create(attributeFullName)));
    }

    public void WithAnyAttribute(ImmutableHashSet<string> attributeFullNames)
    {
        _withAnyAttributeNames.Add(attributeFullNames);
        _scanAttributeNames.Add(new(true, Sorted(attributeFullNames)));
    }

    public void Namespaces(NamespaceFilter filter, ImmutableHashSet<string> namespaces)
    {
        _namespaces.Add((filter, namespaces));
        _scanNamespaces.Add(new(filter, Sorted(namespaces)));
    }

    public void Names(bool include, TextDirectionFilter filter, ImmutableHashSet<string> names)
    {
        _names.Add((include, filter, names));
        _scanNames.Add(new(include, filter, Sorted(names)));
    }

    public void KindOf(bool include, ImmutableHashSet<TypeKind> typeKinds)
    {
        _typeKinds.Add((include, typeKinds));
        _scanTypeKinds.Add(new(include, typeKinds.OrderBy(static z => z).ToImmutableArray()));
    }

    public void InfoOf(bool include, ImmutableHashSet<TypeInfoFilter> typeInfos)
    {
        _typeInfos.Add((include, typeInfos));
        _scanTypeInfos.Add(new(include, typeInfos.OrderBy(static z => z).ToImmutableArray()));
    }

    public TypeSelection Build() =>
        TypeSelection.Create(
            environment.Compilation,
            environment.Resolver,
            BuildData(),
            new(
                ClassFilter == ClassFilter.PublicOnly,
                _scanAssignableTo.ToImmutable(),
                _scanAttributes.ToImmutable(),
                _scanAttributeNames.ToImmutable(),
                _scanNamespaces.ToImmutable(),
                _scanNames.ToImmutable(),
                _scanTypeKinds.ToImmutable(),
                _scanTypeInfos.ToImmutable(),
                _scanCustomFilters.ToImmutable()
            )
        );

    // Sets enumerate in hash order, which for strings changes between processes; the request's cache key must not.
    private static ImmutableArray<string> Sorted(ImmutableHashSet<string> values) => values.OrderBy(static z => z, StringComparer.Ordinal).ToImmutableArray();

    // A clause whose types all resolve back to the very same symbols is a plain Specular.Scanning clause; a closed generic such as
    // IHandler<int> or IHandler<string[]> keeps its type arguments. Anything that does not round-trip (a type argument a reference
    // cannot name, such as a type parameter or dynamic; a type the compilation cannot find by name; no types at all) is matched against
    // the symbols themselves, exactly as before.
    private void AddTypes(ExactTypeFilterKind kind, bool include, IEnumerable<INamedTypeSymbol> types)
    {
        var symbols = types.ToImmutableArray();
        var references = new List<(TypeReference Reference, string Key)>(symbols.Length);
        foreach (var symbol in symbols)
        {
            if (ToTypeReference(kind, symbol) is not { } reference) break;
            references.Add((reference, symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)));
        }

        if (symbols.Length == 0 || references.Count != symbols.Length)
        {
            _scanCustomFilters.Add(new ExactTypeFilter(kind, include, symbols));
            return;
        }

        var sorted = references
                    .OrderBy(static z => z.Reference.AssemblyName, StringComparer.Ordinal)
                    .ThenBy(static z => z.Reference.MetadataName, StringComparer.Ordinal)
                    .ThenBy(static z => z.Reference.UnboundGeneric)
                    .ThenBy(static z => z.Key, StringComparer.Ordinal)
                    .Select(static z => z.Reference)
                    .ToImmutableArray();
        switch (kind)
        {
            case ExactTypeFilterKind.AssignableTo:
                _scanAssignableTo.Add(new(include, sorted));
                break;
            case ExactTypeFilterKind.WithAttribute:
                _scanAttributes.Add(new(include, false, sorted));
                break;
            case ExactTypeFilterKind.WithAnyAttribute:
                _scanAttributes.Add(new(include, true, sorted));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
        }
    }

    // WithAnyAttribute compares attribute classes by name and namespace, so its references name the generic definition (no type
    // arguments) and only have to resolve to the same definition.
    private TypeReference? ToTypeReference(ExactTypeFilterKind kind, INamedTypeSymbol symbol)
    {
        TypeReference reference;
        try
        {
            reference = SelectorArguments.ToTypeReference(symbol, nameof(symbol));
        }
        catch (ArgumentException)
        {
            return null;
        }

        if (kind == ExactTypeFilterKind.WithAnyAttribute)
        {
            reference = reference with { TypeArguments = EquatableArray<TypeReference>.Empty };
            return environment.Resolver.Resolve(reference) is { } definition
             && SymbolEqualityComparer.Default.Equals(definition.OriginalDefinition, symbol.OriginalDefinition)
                ? reference
                : null;
        }

        return SymbolEqualityComparer.Default.Equals(environment.Resolver.Resolve(reference), symbol) ? reference : null;
    }

    // The payload Specular has always written: each kind of clause in its own list, ordered as below so equal selectors write equal
    // attributes (and the same content hash).
    private WireTypeFilter BuildData() => new(
        ClassFilter,
        _namespaces
           .Select(static z => new NamespaceFilterData(z.Filter, z.Namespaces.OrderBy(z => z).ToImmutableArray()))
           .OrderBy(static z => string.Join(",", z.Namespaces.OrderBy(static z => z)))
           .ThenBy(static z => z.Filter)
           .Select(static z => z with { Namespaces = z.Namespaces.OrderBy(z => z).ToImmutableArray() })
           .ToImmutableArray(),
        _names
           .Select(static z => new NameFilterData(z.Include, z.Filter, z.Names.OrderBy(z => z).ToImmutableArray()))
           .OrderBy(static z => string.Join(",", z.Names.OrderBy(static z => z)))
           .ThenBy(static z => z.Filter)
           .ToImmutableArray(),
        _typeKinds
           .Select(static z => new TypeKindFilterData(z.Include, z.TypeKinds.OrderBy(z => z).ToImmutableArray()))
           .OrderBy(static z => string.Join(",", z.TypeKinds.OrderBy(static z => z).Select(static z => z.ToString())))
           .ThenBy(static z => z.Include)
           .ToImmutableArray(),
        _typeInfos
           .Select(static z => new TypeInfoFilterData(z.Include, z.TypeInfos.OrderBy(z => z).ToImmutableArray()))
           .OrderBy(static z => string.Join(",", z.TypeInfos.OrderBy(static z => z).Select(static z => z.ToString())))
           .ThenBy(static z => z.Include)
           .ToImmutableArray(),
        _withAttributes
           .Select(static z => new WithAttributeData(
                       z.Include,
                       z.Attribute.ContainingAssembly.MetadataName,
                       Helpers.GetFullMetadataName(z.Attribute),
                       z.Attribute.IsUnboundGenericType
                   )
            )
           .OrderBy(static z => z.Assembly)
           .ThenBy(static z => z.Attribute)
           .ThenBy(static z => z.Include)
           .ToImmutableArray(),
        _withAttributeNames
           .Select(static z => new WithAttributeStringData(z.Include, z.Attribute))
           .OrderBy(static z => z.Attribute)
           .ThenBy(static z => z.Include)
           .ToImmutableArray(),
        _withAnyAttributes
           .SelectMany(static z => z)
           .Select(static attribute => new WithAttributeData(
                       true,
                       attribute.ContainingAssembly.MetadataName,
                       Helpers.GetFullMetadataName(attribute),
                       attribute.IsUnboundGenericType
                   )
            )
           .OrderBy(static z => z.Assembly)
           .ThenBy(static z => z.Attribute)
           .ThenBy(static z => z.Include)
           .ToImmutableArray(),
        _withAnyAttributeNames
           .SelectMany(static z => z)
           .Select(static z => new WithAttributeStringData(true, z))
           .OrderBy(static z => z.Attribute)
           .ThenBy(static z => z.Include)
           .ToImmutableArray(),
        _assignableTo
           .Select(static z => new AssignableToTypeData(
                       z.Include,
                       z.Type.ContainingAssembly.MetadataName,
                       Helpers.GetFullMetadataName(z.Type),
                       z.Type.IsUnboundGenericType
                   )
            )
           .OrderBy(static z => z.Assembly)
           .ThenBy(static z => z.Type)
           .ThenBy(static z => z.Include)
           .ToImmutableArray(),
        _assignableToAny
           .Select(static z => new AssignableToAnyTypeData(
                       z.Include,
                       z
                          .Types.Select(static t => new AnyTypeData(t.ContainingAssembly.MetadataName, Helpers.GetFullMetadataName(t), t.IsUnboundGenericType))
                          .OrderBy(static t => t.Assembly)
                          .ThenBy(static t => t.Type)
                          .ToImmutableArray()
                   )
            )
           .OrderBy(static z => string.Join(",", z.Types.Select(static t => t.ToString())))
           .ThenBy(static z => z.Include)
           .ToImmutableArray()
    );
}
