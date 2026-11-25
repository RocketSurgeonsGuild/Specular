using Microsoft.CodeAnalysis;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Selectors.SelectorCompilation;
using static Specular.Scanning.Tests.Selectors.SelectorTestData;

namespace Specular.Scanning.Tests.Selectors;

public class ScanRequestCacheKeyTests
{
    private static readonly ScanRequest Baseline = new(
        ScanKind.Types,
        new(false, false, Array("App"), Array("Excluded"), Array("Root")),
        new(
            false,
            Array(new AssignableToFilterData(true, Array(new TypeReference("Library", "Library.Contracts.IHandler`1", true)))),
            Array(new AttributeFilterData(true, false, Array(new TypeReference("App", "App.Handlers.MarkerAttribute", false)))),
            Array(new AttributeNameFilterData(false, Array("App.Handlers.OtherAttribute"))),
            Array(new NamespaceFilterData(NamespaceFilter.In, Array("App.Handlers"))),
            Array(new NameFilterData(true, TextDirectionFilter.EndsWith, Array("Handler"))),
            Array(new TypeKindFilterData(true, Array(TypeKind.Class))),
            Array(new TypeInfoFilterData(false, Array(TypeInfoFilter.Abstract))),
            Array<CustomTypeFilter>(new NameLengthFilter(7))
        )
    );

    [Test]
    public async Task Identical_builds_are_equal_and_share_a_key()
    {
        static ScanRequest build() => Build(s => s
                                                .FromAssemblies()
                                                .NotFromAssemblyOf(Type("Library.Contracts.IHandler"))
                                                .GetTypes(
                                                     true,
                                                     f => f
                                                         .AssignableTo(Unbound("Library.Contracts.IHandler`1"))
                                                         .WithAnyAttribute(Type("App.Handlers.MarkerAttribute"), Type("App.Handlers.OtherAttribute"))
                                                         .InNamespaces("App")
                                                         .KindOf(TypeKind.Class)
                                                         .Where(new NameLengthFilter(7))
                                                 )
        );

        var first = build();
        var second = build();

        await ReferenceEquals(first, second).Should().BeFalse();
        await second.Should().BeEqualTo(first);
        await second.GetHashCode().Should().BeEqualTo(first.GetHashCode());
        await second.CacheKey.Should().BeEqualTo(first.CacheKey);
    }

    [Test]
    public async Task The_key_is_pinned() =>
        // A changed literal means every existing build cache entry is invalidated; only update it deliberately.
        // Format 2 (was "t_j8nVLNLqRihEKG-Zz8XeUola4xPPOnwH-uN9kAc9s" under format 1): type references now encode their type arguments
        // (closed generics), and entries cached when unresolved references dropped their clause must not be reused.
        // Format 3 (was "fjWGd6RrfH7paR8Q8Y1sH630ZD48Ye8tUScNUpTlaXU" under format 2): type references encode their array ranks, so a
        // closed generic with an array argument can be named.
        await Baseline.CacheKey.Should().BeEqualTo("LD-Y5dYhdBpOaChQVtT8vnMMvDBIAJ2kjW384r7jcQI");

    [Test]
    public async Task The_key_is_url_safe() => await Baseline.CacheKey.All(c => c is ( >= 'A' and <= 'Z' ) or ( >= 'a' and <= 'z' ) or ( >= '0' and <= '9' ) or '-' or '_').Should().BeTrue();

    [Test]
    public async Task The_key_is_computed_once_per_instance()
    {
        var request = Baseline with { };

        await ReferenceEquals(request.CacheKey, request.CacheKey).Should().BeTrue();
    }

    [Test]
    public async Task A_with_copy_does_not_reuse_a_stale_key()
    {
        var request = Baseline with { };
        _ = request.CacheKey;

        var changed = request with { Kind = ScanKind.Assemblies };
        var unchanged = request with { };

        await changed.CacheKey.Should().NotBeEqualTo(request.CacheKey);
        await unchanged.CacheKey.Should().BeEqualTo(request.CacheKey);
        await unchanged.Should().BeEqualTo(request);
        await unchanged.GetHashCode().Should().BeEqualTo(request.GetHashCode());
    }

    public static IEnumerable<Func<KeyVariant>> Variants()
    {
        var types = Baseline.Types!;
        var assemblies = Baseline.Assemblies;
        var @int = new TypeReference("", "System.Int32", false);
        static TypeReference closed(params TypeReference[] typeArguments) =>
            new("Library", "Library.Contracts.IHandler`1", false) { TypeArguments = Array(typeArguments) };
        yield return () => new("Kind", Baseline with { Kind = ScanKind.Assemblies });
        yield return () => new("Types null", Baseline with { Types = null });
        yield return () => new("AllAssemblies", Baseline with { Assemblies = assemblies with { AllAssemblies = true } });
        yield return () => new("IncludeSystemAssemblies", Baseline with { Assemblies = assemblies with { IncludeSystemAssemblies = true } });
        yield return () => new("Assemblies", Baseline with { Assemblies = assemblies with { Assemblies = Array("App", "Other") } });
        yield return () => new("Assemblies order", Baseline with { Assemblies = assemblies with { Assemblies = Array("Excluded"), NotAssemblies = Array("App") } });
        yield return () => new("NotAssemblies", Baseline with { Assemblies = assemblies with { NotAssemblies = Array("Other") } });
        yield return () => new("DependenciesOf", Baseline with { Assemblies = assemblies with { DependenciesOf = Array<string>() } });
        yield return () => new("PublicOnly", Baseline with { Types = types with { PublicOnly = true } });
        yield return () => new(
            "AssignableTo include",
            Baseline with { Types = types with { AssignableTo = Array(types.AssignableTo[0] with { Include = false }) } }
        );
        yield return () => new(
            "TypeReference assembly",
            Baseline with
            {
                Types = types with
                {
                    AssignableTo = Array(new AssignableToFilterData(true, Array(new TypeReference("", "Library.Contracts.IHandler`1", true)))),
                },
            }
        );
        yield return () => new(
            "TypeReference split",
            Baseline with
            {
                Types = types with
                {
                    AssignableTo = Array(new AssignableToFilterData(true, Array(new TypeReference("LibraryL", "ibrary.Contracts.IHandler`1", true)))),
                },
            }
        );
        yield return () => new(
            "TypeReference unbound",
            Baseline with
            {
                Types = types with
                {
                    AssignableTo = Array(new AssignableToFilterData(true, Array(new TypeReference("Library", "Library.Contracts.IHandler`1", false)))),
                },
            }
        );
        yield return () => new("TypeReference type argument", Baseline with { Types = types with { AssignableTo = Array(new AssignableToFilterData(true, Array(closed(@int)))) } });
        yield return () => new(
            "TypeReference two type arguments",
            Baseline with { Types = types with { AssignableTo = Array(new AssignableToFilterData(true, Array(closed(@int, @int)))) } }
        );
        yield return () => new(
            "TypeReference nested type argument",
            Baseline with { Types = types with { AssignableTo = Array(new AssignableToFilterData(true, Array(closed(closed(@int))))) } }
        );
        yield return () => new(
            "TypeReference type argument then sibling",
            // Same strings as "nested type argument" in a different shape: [IHandler<int>, int] next to [IHandler<IHandler<int>>].
            Baseline with { Types = types with { AssignableTo = Array(new AssignableToFilterData(true, Array(closed(@int), @int))) } }
        );
        yield return () => new(
            "Attribute type argument",
            Baseline with { Types = types with { Attributes = Array(types.Attributes[0] with { Attributes = Array(types.Attributes[0].Attributes[0] with { TypeArguments = Array(@int) }) }) } }
        );
        yield return () => new("Attributes any", Baseline with { Types = types with { Attributes = Array(types.Attributes[0] with { Any = true }) } });
        yield return () => new("Attributes include", Baseline with { Types = types with { Attributes = Array(types.Attributes[0] with { Include = false }) } });
        yield return () => new("AttributeNames", Baseline with { Types = types with { AttributeNames = Array(new AttributeNameFilterData(true, Array("App.Handlers.OtherAttribute"))) } });
        yield return () => new("Namespaces filter", Baseline with { Types = types with { Namespaces = Array(new NamespaceFilterData(NamespaceFilter.Exact, Array("App.Handlers"))) } });
        yield return () => new("Namespaces", Baseline with { Types = types with { Namespaces = Array(new NamespaceFilterData(NamespaceFilter.In, Array("App"))) } });
        yield return () => new("Names filter", Baseline with { Types = types with { Names = Array(new NameFilterData(true, TextDirectionFilter.Contains, Array("Handler"))) } });
        yield return () => new("Names split", Baseline with { Types = types with { Names = Array(new NameFilterData(true, TextDirectionFilter.EndsWith, Array("Hand", "ler"))) } });
        yield return () => new(
            "Names clause split",
            Baseline with
            {
                Types = types with
                {
                    Names = Array(
                        new NameFilterData(true, TextDirectionFilter.EndsWith, Array("Hand")),
                        new NameFilterData(true, TextDirectionFilter.EndsWith, Array("ler"))
                    ),
                },
            }
        );
        yield return () => new("TypeKinds", Baseline with { Types = types with { TypeKinds = Array(new TypeKindFilterData(true, Array(TypeKind.Interface))) } });
        yield return () => new("TypeInfos", Baseline with { Types = types with { TypeInfos = Array(new TypeInfoFilterData(false, Array(TypeInfoFilter.Sealed))) } });
        yield return () => new("Custom filter key", Baseline with { Types = types with { CustomFilters = Array<CustomTypeFilter>(new NameLengthFilter(8)) } });
        yield return () => new("Custom filter type", Baseline with { Types = types with { CustomFilters = Array<CustomTypeFilter>(new OtherNameLengthFilter(7)) } });
        yield return () => new("No custom filter", Baseline with { Types = types with { CustomFilters = Array<CustomTypeFilter>() } });
    }

    [Test]
    [MethodDataSource(nameof(Variants))]
    public async Task Changing_any_field_changes_the_key(KeyVariant variant)
    {
        await variant.Request.Should().NotBeEqualTo(Baseline);
        await variant.Request.CacheKey.Should().NotBeEqualTo(Baseline.CacheKey);
    }

    [Test]
    public async Task Every_variant_has_its_own_key()
    {
        var keys = Variants().Select(v => v().Request.CacheKey).Append(Baseline.CacheKey).ToArray();

        await keys.Distinct(StringComparer.Ordinal).Count().Should().BeEqualTo(keys.Length);
    }

    public sealed record KeyVariant(string Name, ScanRequest Request)
    {
        public override string ToString() => Name;
    }
}
